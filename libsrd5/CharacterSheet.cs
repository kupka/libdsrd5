using System;

namespace srd5 {
    public class CharacterLevel {
        public int Levels { get; internal set; }
        public CharacterClass Class { get; internal set; }
    }

    public class CharacterInventory {
        public Weapon MainHand { get; internal set; }
        public Item OffHand { get; internal set; }
        public Armor Armor { get; internal set; }
        public Helmet Helmet { get; internal set; }
        public Amulet Amulet { get; internal set; }
        public Ring RingRight { get; internal set; }
        public Ring RingLeft { get; internal set; }
        public Boots Boots { get; internal set; }
        public Item[] Currentquipment {
            get {
                Item[] allSlots = new Item[] { MainHand, OffHand, Armor, Helmet, Amulet, RingRight, RingLeft, Boots };
                int countNulls = 0;
                foreach (Item item in allSlots) {
                    if (item == null) countNulls++;
                }
                Item[] equiped = new Item[allSlots.Length - countNulls];
                int i = 0;
                foreach (Item item in allSlots) {
                    if (item != null) equiped[i++] = item;
                }
                return equiped;
            }
        }
        public Item[] Bag { get { return bag; } }
        private Item[] bag = new Item[0];
        private readonly CharacterSheet owner;

        public CharacterInventory(CharacterSheet owner) {
            this.owner = owner;
        }

        public void AddToBag(params Item[] items) {
            Utils.Push<Item>(ref bag, items);
            foreach (Item item in items)
                GlobalEvents.ChangeEquipment(owner, item, GlobalEvents.EquipmentChanged.Events.PUT_IN_BAG);
        }

        public void RemoveFromBag(Item item) {
            RemoveResult result = Utils.RemoveSingle<Item>(ref bag, item);
            if (result == RemoveResult.REMOVED_AND_GONE)
                GlobalEvents.ChangeEquipment(owner, item, GlobalEvents.EquipmentChanged.Events.REMOVED_FROM_BAG);
        }
    }

    public class CharacterSheet : Combattant {
        public CharacterRace Race { get { return race; } }
        private CharacterRace race;
        public CharacterLevel[] Levels { get { return levels; } }
        private CharacterLevel[] levels = new CharacterLevel[0];
        public int Experience { get; set; } = 0;
        public Die[] HitDice { get { return hitDice; } }
        private Die[] hitDice = new Die[0];
        public CharacterInventory Inventory { get; internal set; }
        public int AbilityPoints { get; internal set; }
        public int Attacks {
            get {
                if (HasEffect(Effect.THREE_EXTRA_ATTACKS))
                    return 4;
                else if (HasEffect(Effect.TWO_EXTRA_ATTACKS))
                    return 3;
                else if (HasEffect(Effect.ONE_EXTRA_ATTACK))
                    return 2;
                else
                    return 1;
            }
        }
        public override int ArmorClass {
            get {
                int ac = 10 + Dexterity.Modifier;
                if (Inventory.Armor != null) {
                    ac = Inventory.Armor.AC + Math.Min(Inventory.Armor.MaxDexBonus, Dexterity.Modifier);
                }
                if (Inventory.OffHand != null && Inventory.OffHand is Shield shield1) {
                    Shield shield = shield1;
                    ac += shield.AC;
                }
                return ac + ArmorClassModifier;
            }
        }

        public override int HitPointsMax {
            get {
                int hp = 0;
                int additionalHp = HasEffect(Effect.ADDITIONAL_HP_PER_LEVEL) ? 1 : 0;
                foreach (Die die in hitDice) {
                    hp += die.Value + Constitution.Modifier + additionalHp;
                }
                hp += HitPointMaxiumModifiersSum;
                return Math.Max(0, hp);
            }
        }

        public override int ProficiencyBonus {
            get {
                return (2 + ((EffectiveLevel - 1) / 4));
            }
        }

        public int AttackProficiency {
            get {
                Weapon mainhand = Inventory.MainHand;
                int bonus = 0;
                // calculate base proficiency bonus if character is proficient
                if (mainhand == null || IsProficient(mainhand)) {
                    bonus += ProficiencyBonus;
                }
                // get bonus from strength or dex
                if (mainhand == null) { // unarmed, use strength
                    bonus += Strength.Modifier;
                } else if (mainhand.HasProperty(WeaponProperty.FINESSE)) { // check whether dex is better than str
                    if (Strength.Modifier > Dexterity.Modifier)
                        bonus += Strength.Modifier;
                    else
                        bonus += Dexterity.Modifier;
                    // ranged weapons use dex
                } else if (mainhand.HasProperty(WeaponProperty.AMMUNITION)) {
                    bonus += Dexterity.Modifier;
                } else {
                    bonus += Strength.Modifier;
                }
                // bonus from magic weapons +1/+2/+3
                if (mainhand != null && mainhand.HasProperty(WeaponProperty.PLUS_3))
                    bonus += 3;
                else if (mainhand != null && mainhand.HasProperty(WeaponProperty.PLUS_2))
                    bonus += 2;
                else if (mainhand != null && mainhand.HasProperty(WeaponProperty.PLUS_1))
                    bonus += 1;

                // get bonus from feats etc.                
                return bonus;
            }
        }

        /// <summary>
        /// Creates a Charactersheet from the given race. If classic is true, the all abilities are rolled with advantage. 
        /// Otherwise, abilitypoints for spending are assigned.
        /// </summary>
        public CharacterSheet(Race race, bool classic = false) {
            Inventory = new CharacterInventory(this);
            if (classic) {
                Dice dice = new Dice("3d6");
                Strength.BaseValue = Math.Max(dice.Roll(), dice.Roll());
                Constitution.BaseValue = Math.Max(dice.Roll(), dice.Roll());
                Dexterity.BaseValue = Math.Max(dice.Roll(), dice.Roll());
                Wisdom.BaseValue = Math.Max(dice.Roll(), dice.Roll());
                Intelligence.BaseValue = Math.Max(dice.Roll(), dice.Roll());
                Charisma.BaseValue = Math.Max(dice.Roll(), dice.Roll());
            } else {
                AbilityPoints = 14;
            }
            SetRace(race.CharacterRace());
        }

        public int GetSkillModifier(Skill skill) {
            int modifier = 0;
            switch (skill.Ability()) {
                case AbilityType.STRENGTH:
                    modifier += Strength.Modifier;
                    break;
                case AbilityType.CHARISMA:
                    modifier += Charisma.Modifier;
                    break;
                case AbilityType.DEXTERITY:
                    modifier += Dexterity.Modifier;
                    break;
                case AbilityType.INTELLIGENCE:
                    modifier += Intelligence.Modifier;
                    break;
                case AbilityType.WISDOM:
                    modifier += Wisdom.Modifier;
                    break;
            }
            if (IsProficient(skill.Proficiency()))
                modifier += ProficiencyBonus;
            if (IsDoubleProficient(skill.Proficiency()))
                modifier += ProficiencyBonus;
            return modifier;
        }

        internal void RecalculateAttacks() {
            string dmgString;
            if (Inventory.MainHand == null) {
                // unarmed attack
                dmgString = concatDamageString("1d1", Strength.Modifier);
                MeleeAttacks = Utils.Expand<Attack>(new Attack("Unarmed", AttackProficiency, new Damage(DamageType.BLUDGEONING, dmgString), 5), Attacks);
                RangedAttacks = new Attack[0];
                return;
            }
            Weapon weapon = Inventory.MainHand;
            int modifier = calculateModifier(weapon);
            dmgString = concatDamageString(weapon.Damage.Dice.ToString(), modifier);
            if (weapon.HasProperty(WeaponProperty.VERSATILE) && Inventory.OffHand == null) {
                // increase damage dice because versatile weapon is used two-handed
                dmgString = dmgString.Replace("d10", "d12");
                dmgString = dmgString.Replace("d8", "d10");
                dmgString = dmgString.Replace("d6", "d8");
                dmgString = dmgString.Replace("d4", "d6");
            }
            if (weapon.HasProperty(WeaponProperty.AMMUNITION)) {
                RangedAttacks = Utils.Expand<Attack>(srd5.Attack.FromWeapon(AttackProficiency, dmgString, weapon), Attacks);
                MeleeAttacks = new Attack[0];
            } else {
                MeleeAttacks = Utils.Expand<Attack>(srd5.Attack.FromWeapon(AttackProficiency, dmgString, weapon), Attacks);
                RangedAttacks = new Attack[0];
            }
            if (Inventory.OffHand == null || !(Inventory.OffHand is Weapon)) return;
            weapon = (Weapon)Inventory.OffHand;
            modifier = calculateModifier(weapon);
            dmgString = concatDamageString(weapon.Damage.Dice.ToString(), modifier);
            if (modifier > 0) modifier = 0; // only negative modifiers for bonus action
            BonusAttack = srd5.Attack.FromWeapon(AttackProficiency, dmgString, weapon);
        }

        private int calculateModifier(Weapon weapon) {
            int modifier = Strength.Modifier;
            if (weapon.HasProperty(WeaponProperty.FINESSE)) {
                if (Dexterity.Modifier > modifier)
                    modifier = Dexterity.Modifier;
            } else if (weapon.HasProperty(WeaponProperty.AMMUNITION)) {
                modifier = Dexterity.Modifier;
            }
            // Modifier for magic weapons +1/+2/+3
            if (weapon.HasProperty(WeaponProperty.PLUS_3))
                modifier += 3;
            else if (weapon.HasProperty(WeaponProperty.PLUS_2))
                modifier += 2;
            else if (weapon.HasProperty(WeaponProperty.PLUS_1))
                modifier += 1;
            return modifier;
        }

        private string concatDamageString(string damageString, int modifier) {
            if (modifier > 0)
                return damageString + "+" + modifier;
            else if (modifier < 0)
                return damageString + modifier;
            return damageString;
        }

        private void equip(Weapon weapon) {
            // don't equip a weapon that is already equipped in one hand
            if (weapon.Equals(Inventory.MainHand) || weapon.Equals(Inventory.OffHand)) return;
            GlobalEvents.ChangeEquipment(this, weapon, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Inventory.RemoveFromBag(weapon);
            if (weapon.HasProperty(WeaponProperty.TWO_HANDED)) {
                Unequip(Inventory.OffHand);
                Unequip(Inventory.MainHand);
                Inventory.MainHand = weapon;
            } else if (Inventory.MainHand == null) {
                Inventory.MainHand = weapon;
            } else if (Inventory.MainHand.HasProperty(WeaponProperty.TWO_HANDED)) {
                Unequip(Inventory.MainHand);
                Inventory.MainHand = weapon;
            } else if (Inventory.OffHand == null && weapon.HasProperty(WeaponProperty.LIGHT)) {
                Inventory.OffHand = weapon;
            } else {
                Unequip(Inventory.MainHand);
                Inventory.MainHand = weapon;
            }
            RecalculateAttacks();
        }

        private void equip(Shield shield) {
            if (shield.Equals(Inventory.OffHand)) return;
            if (Inventory.MainHand != null && Inventory.MainHand.HasProperty(WeaponProperty.TWO_HANDED)) {
                Unequip(Inventory.MainHand);
            }
            Unequip(Inventory.OffHand);
            GlobalEvents.ChangeEquipment(this, shield, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Inventory.RemoveFromBag(shield);
            Inventory.OffHand = shield;
            RecalculateAttacks();
        }

        private void equip(Armor armor) {
            if (armor.Equals(Inventory.Armor)) return;
            Unequip(Inventory.Armor);
            GlobalEvents.ChangeEquipment(this, armor, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Inventory.RemoveFromBag(armor);
            Inventory.Armor = armor;
            // calculate speed penality if applicable
            if (!HasEffect(Effect.NO_SPEED_PENALITY_FOR_HEAVY_ARMOR) && armor.Strength > Strength.Value) {
                AddEffect(Effect.HEAVY_ARMOR_SPEED_PENALITY);
            }
        }

        private void equip(Ring ring) {
            if (ring.Equals(Inventory.RingLeft) || ring.Equals(Inventory.RingRight)) return;
            if (Inventory.RingLeft == null)
                Inventory.RingLeft = ring;
            else if (Inventory.RingRight == null)
                Inventory.RingRight = ring;
            else {
                Unequip(Inventory.RingLeft);
                Inventory.RingLeft = ring;
            }
            GlobalEvents.ChangeEquipment(this, ring, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Inventory.RemoveFromBag(ring);
            addEffects(ring);
        }

        private void equip(Helmet helmet) {
            if (helmet.Equals(Inventory.Helmet)) return;
            Unequip(Inventory.Helmet);
            GlobalEvents.ChangeEquipment(this, helmet, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Inventory.RemoveFromBag(helmet);
            Inventory.Helmet = helmet;
            addEffects(helmet);
        }

        private void equip(Boots boots) {
            if (boots.Equals(Inventory.Boots)) return;
            Unequip(Inventory.Boots);
            GlobalEvents.ChangeEquipment(this, boots, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Inventory.RemoveFromBag(boots);
            Inventory.Boots = boots;
            addEffects(boots);
        }

        private void equip(Amulet amulet) {
            if (amulet.Equals(Inventory.Amulet)) return;
            GlobalEvents.ChangeEquipment(this, amulet, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Unequip(Inventory.Amulet);
            Inventory.RemoveFromBag(amulet);
            Inventory.Amulet = amulet;
            addEffects(amulet);
        }

        private void addEffects(MagicItem item) {
            foreach (Effect effect in item.Effects) {
                AddEffect(effect);
            }
        }

        private void removeEffects(MagicItem item) {
            foreach (Effect effect in item.Effects) {
                RemoveEffect(effect);
            }
        }

        /// <summary>
        /// Equips an item in its appropriate inventory slot. If the slot is currently occupied, the item there is unequipped first.
        /// </summary>
        public void Equip(Item item) {
            if (item == null || item.Destroyed) return;
            if (item is Weapon weapon) {
                equip(weapon);
            } else if (item is Armor armor) {
                equip(armor);
            } else if (item is Shield shield) {
                equip(shield);
            } else if (item is Ring ring) {
                equip(ring);
            } else if (item is Amulet amulet) {
                equip(amulet);
            } else if (item is Helmet helmet) {
                equip(helmet);
            } else if (item is Boots boots) {
                equip(boots);
            }
        }

        /// <summary>
        /// Unequips an equipped item.
        /// </summary>
        public void Unequip(Item item) {
            if (item == null || Array.IndexOf(Inventory.Currentquipment, item) == -1) return;
            GlobalEvents.ChangeEquipment(this, item, GlobalEvents.EquipmentChanged.Events.UNEQUIPPED);
            switch (item.Type) {
                case ItemType.WEAPON:
                case ItemType.SHIELD:
                    if (item.Equals(Inventory.MainHand))
                        Inventory.MainHand = null;
                    else if (item.Equals(Inventory.OffHand))
                        Inventory.OffHand = null;
                    break;
                case ItemType.ARMOR:
                    Inventory.Armor = null;
                    RemoveEffect(Effect.HEAVY_ARMOR_SPEED_PENALITY);
                    break;
                case ItemType.HELMET:
                    Inventory.Helmet = null;
                    break;
                case ItemType.RING:
                    if (item.Equals(Inventory.RingLeft)) {
                        Inventory.RingLeft = null;

                    } else if (item.Equals(Inventory.RingRight)) {
                        Inventory.RingRight = null;
                    }
                    break;
                case ItemType.AMULET:
                    Inventory.Amulet = null;
                    break;
                case ItemType.BOOTS:
                    Inventory.Boots = null;
                    break;
            }
            if (item is MagicItem) {
                MagicItem magicItem = (MagicItem)(object)item;
                removeEffects(magicItem);
            }
            // Put item into bag
            Inventory.AddToBag(item);
            RecalculateAttacks();
        }

        public void AddLevel(CharacterClass characterClass) {
            Die die = srd5.Die.Get(characterClass.HitDie);
            int additionalHp = HasEffect(Effect.ADDITIONAL_HP_PER_LEVEL) ? 1 : 0;
            EffectiveLevel++;
            foreach (CharacterLevel level in levels) {
                if (level.Class.Class == characterClass.Class) {
                    foreach (Feat feat in characterClass.Feats[level.Levels]) {
                        AddFeat(feat);
                    }
                    level.Levels++;
                    Utils.Push<Die>(ref hitDice, die);
                    HitPoints += die.Value + additionalHp + Constitution.Modifier;
                    updateAvailableSpells(level);
                    RecalculateAttacks();
                    return;
                }
            }
            CharacterLevel newLevel = new CharacterLevel {
                Class = characterClass
            };
            foreach (Feat feat in characterClass.Feats[0]) {
                AddFeat(feat);
            }
            newLevel.Levels = 1;
            if (levels.Length == 0) { // maximum hitpoints when this is the first level
                die.Value = die.MaxValue;
            }
            Utils.Push<CharacterLevel>(ref levels, newLevel);
            Utils.Push<Die>(ref hitDice, die);
            HitPoints += die.Value + additionalHp + Constitution.Modifier;
            foreach (Proficiency proficiency in characterClass.Proficiencies) {
                if (!IsProficient(proficiency)) {
                    Utils.Push<Proficiency>(ref proficiencies, proficiency);
                }
            }
            updateAvailableSpells(newLevel);
            RecalculateAttacks();
        }

        public void AddLevels(params CharacterClass[] classes) {
            foreach (CharacterClass characterClass in classes) {
                AddLevel(characterClass);
            }
        }

        private void updateAvailableSpells(CharacterLevel level) {
            if (level.Class.SpellCastingAbility == AbilityType.NONE) return;
            AvailableSpells spells = null;
            // find the applicable entry for this class
            foreach (AvailableSpells available in AvailableSpells) {
                if (available.CharacterClass.Equals(level.Class)) {
                    spells = available;
                }
            }
            // If no such entry is available yet, add one
            if (spells == null) {
                spells = new AvailableSpells(level.Class) {
                    CharacterClass = level.Class
                };
                AddAvailableSpells(spells);
            }
            // Set the slots according to the new level
            spells.SlotsMax = level.Class.SpellSlots[level.Levels];
            // update cantrip count
            spells.SlotsCurrent[0] = spells.SlotsMax[0];
        }

        public void LongRest() {
            // replenish spell slots
            foreach (AvailableSpells availableSpells in AvailableSpells) {
                for (int i = 0; i < 10; i++) {
                    availableSpells.SlotsCurrent[i] = availableSpells.SlotsMax[i];
                }
            }
            if (HitPoints < HitPointsMax) HealDamage(HitPointsMax - HitPoints);
            RecalculateAttacks();
        }

        private void SetRace(CharacterRace race) {
            this.race = race;
            foreach (Feat feat in race.RacialFeats) {
                AddFeat(feat);
            }
            Speed = race.Speed;
            Size = race.Size;
        }

        public void IncreaseAbility(AbilityType type) {
            if (AbilityPoints == 0) return;
            AbilityPoints--;
            switch (type) {
                case AbilityType.STRENGTH:
                    Strength.BaseValue++;
                    break;
                case AbilityType.DEXTERITY:
                    Dexterity.BaseValue++;
                    break;
                case AbilityType.CONSTITUTION:
                    Constitution.BaseValue++;
                    break;
                case AbilityType.WISDOM:
                    Wisdom.BaseValue++;
                    break;
                case AbilityType.INTELLIGENCE:
                    Intelligence.BaseValue++;
                    break;
                case AbilityType.CHARISMA:
                    Charisma.BaseValue++;
                    break;
            }
        }

        public void Consume(Consumable item) {
            if (item == null || item.Charges == 0 || item.Destroyed) return;
            GlobalEvents.ChangeEquipment(this, item, GlobalEvents.EquipmentChanged.Events.USED);
            item.ConsumableEffect(this, item);
            if (item.Destroyed)
                Inventory.RemoveFromBag(item);
        }

        public void Use(Usable item, int expendedCharges, Battleground ground = null, params Combattant[] targets) {
            expendedCharges = Math.Max(1, expendedCharges); // cannot expend less than a single charge
            if (item == null || item.Charges < expendedCharges || item.Destroyed) return;
            GlobalEvents.ChangeEquipment(this, item, GlobalEvents.EquipmentChanged.Events.USED);
            item.UsableEffect(ground, this, item, expendedCharges, targets);
            if (item.Destroyed)
                Inventory.RemoveFromBag(item);
        }
    }
}