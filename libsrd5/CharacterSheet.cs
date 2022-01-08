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
        public Item[] Bag { get { return bag; } }
        private Item[] bag = new Item[0];

        public void AddToBag(params Item[] items) {
            Utils.Push<Item>(ref bag, items);
        }

        public void RemoveFromBag(Item item) {
            Utils.RemoveSingle<Item>(ref bag, item);
        }
    }

    public class CharacterSheet : Combattant {
        public CharacterRace Race { get { return race; } }
        private CharacterRace race;
        public CharacterLevel[] Levels { get { return levels; } }
        private CharacterLevel[] levels = new CharacterLevel[0];
        public Proficiency[] Proficiencies { get { return proficiencies; } }
        private Proficiency[] proficiencies = new Proficiency[0];
        public Feat[] Feats { get { return feats; } }
        private Feat[] feats = new Feat[0];
        public Dice[] HitDice { get { return hitDice; } }
        private Dice[] hitDice = new Dice[0];
        public CharacterInventory Inventory { get; internal set; } = new CharacterInventory();
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
                if (Inventory.OffHand != null && Inventory.OffHand is Shield) {
                    Shield shield = (Shield)Inventory.OffHand;
                    ac += shield.AC;
                }
                return ac + ArmorClassModifier;
            }
        }

        public override int HitPointsMax {
            get {
                int hp = 0;
                int additionalHp = HasEffect(Effect.ADDITIONAL_HP_PER_LEVEL) ? 1 : 0;
                foreach (Dice dice in hitDice) {
                    hp += dice.Value + Constitution.Modifier + additionalHp;
                }
                return hp;
            }
        }

        public int Proficiency {
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
                    bonus += Proficiency;
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
            if (classic) {
                Dices dices = new Dices("3d6");
                Strength.BaseValue = Math.Max(dices.Roll(), dices.Roll());
                Constitution.BaseValue = Math.Max(dices.Roll(), dices.Roll());
                Dexterity.BaseValue = Math.Max(dices.Roll(), dices.Roll());
                Wisdom.BaseValue = Math.Max(dices.Roll(), dices.Roll());
                Intelligence.BaseValue = Math.Max(dices.Roll(), dices.Roll());
                Charisma.BaseValue = Math.Max(dices.Roll(), dices.Roll());
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
                modifier += Proficiency;
            if (IsDoubleProficient(skill.Proficiency()))
                modifier += Proficiency;
            return modifier;
        }

        public void Equip(Weapon weapon) {
            // don't equip a weapon that is already equipped in one hand
            if (weapon.Equals(Inventory.MainHand) || weapon.Equals(Inventory.OffHand)) return;
            GlobalEvents.ChangeEquipment(this, weapon, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
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

        internal void RecalculateAttacks() {
            string dmgString;
            if (Inventory.MainHand == null) {
                // unarmed attack
                dmgString = concatDamageString("1d1", Strength.Modifier);
                MeleeAttacks = Utils.Expand<Attack>(new Attack("Unarmed", AttackProficiency, new Damage(DamageType.BLUDGEONING, dmgString)), Attacks);
                RangedAttacks = new Attack[0];
                return;
            }
            Weapon weapon = Inventory.MainHand;
            int modifier = calculateModifier(weapon);
            dmgString = concatDamageString(weapon.Damage.Dices.ToString(), modifier);
            if (weapon.HasProperty(WeaponProperty.VERSATILE) && Inventory.OffHand == null) {
                // increase damage dice because versatile weapon is used two-handed
                dmgString = dmgString.Replace("d10", "d12");
                dmgString = dmgString.Replace("d8", "d10");
                dmgString = dmgString.Replace("d6", "d8");
                dmgString = dmgString.Replace("d4", "d6");
            }
            if (weapon.HasProperty(WeaponProperty.AMMUNITION)) {
                RangedAttacks = Utils.Expand<Attack>(Attack.FromWeapon(AttackProficiency, dmgString, weapon), Attacks);
                MeleeAttacks = new Attack[0];
            } else {
                MeleeAttacks = Utils.Expand<Attack>(Attack.FromWeapon(AttackProficiency, dmgString, weapon), Attacks);
                RangedAttacks = new Attack[0];
            }
            if (Inventory.OffHand == null || !(Inventory.OffHand is Weapon)) return;
            weapon = (Weapon)Inventory.OffHand;
            modifier = calculateModifier(weapon);
            dmgString = concatDamageString(weapon.Damage.Dices.ToString(), modifier);
            if (modifier > 0) modifier = 0; // only negative modifiers for bonus action
            BonusAttack = Attack.FromWeapon(AttackProficiency, dmgString, weapon);
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

        public void Equip(Shield shield) {
            if (shield == null) return;
            if (Inventory.MainHand != null && Inventory.MainHand.HasProperty(WeaponProperty.TWO_HANDED)) {
                Unequip(Inventory.MainHand);
            }
            Unequip(Inventory.OffHand);
            GlobalEvents.ChangeEquipment(this, shield, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Inventory.OffHand = shield;
            RecalculateAttacks();
        }

        public void Equip(Armor armor) {
            if (armor == null) return;
            Unequip(Inventory.Armor);
            GlobalEvents.ChangeEquipment(this, armor, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Inventory.Armor = armor;
            // calculate speed penality if applicable
            if (!HasEffect(Effect.NO_SPEED_PENALITY_FOR_HEAVY_ARMOR) && armor.Strength > Strength.Value) {
                AddEffect(Effect.HEAVY_ARMOR_SPEED_PENALITY);
            }
        }

        public void Equip(Ring ring) {
            if (ring == null) return;
            if (Inventory.RingLeft == null)
                Inventory.RingLeft = ring;
            else if (Inventory.RingRight == null)
                Inventory.RingRight = ring;
            else {
                Unequip(Inventory.RingLeft);
                Inventory.RingLeft = ring;
            }
            GlobalEvents.ChangeEquipment(this, ring, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            addEffects(ring);
        }

        public void Equip(Helmet helmet) {
            if (helmet == null) return;
            Unequip(Inventory.Helmet);
            GlobalEvents.ChangeEquipment(this, helmet, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Inventory.Helmet = helmet;
            addEffects(helmet);
        }

        public void Equip(Boots boots) {
            if (boots == null) return;
            Unequip(Inventory.Boots);
            GlobalEvents.ChangeEquipment(this, boots, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Inventory.Boots = boots;
            addEffects(boots);
        }

        public void Equip(Amulet amulet) {
            if (amulet == null) return;
            GlobalEvents.ChangeEquipment(this, amulet, GlobalEvents.EquipmentChanged.Events.EQUIPPED);
            Unequip(Inventory.Amulet);
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

        public void Unequip(Item item) {
            if (item == null) return;
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
            RecalculateAttacks();
        }

        public bool IsProficient(Proficiency proficiency) {
            return Array.IndexOf(proficiencies, proficiency) > -1;
        }

        public bool IsDoubleProficient(Proficiency proficiency) {
            try {
                Effect doubleProficiency = (Effect)Enum.Parse(typeof(Effect), "DOUBLE_PROFICIENCY_BONUS_" + proficiency.ToString());
                return HasEffect(doubleProficiency);
            } catch (ArgumentException) {
                return false;
            }

        }

        public bool IsProficient(Item item) {
            if (item == null) return true;
            foreach (Proficiency proficiency in item.Proficiencies) {
                if (IsProficient(proficiency))
                    return true;
            }
            return false;
        }

        public void AddLevel(CharacterClass characterClass) {
            Dice dice = Dice.Get(characterClass.HitDice);
            int additionalHp = HasEffect(Effect.ADDITIONAL_HP_PER_LEVEL) ? 1 : 0;
            EffectiveLevel++;
            foreach (CharacterLevel level in levels) {
                if (level.Class.Class == characterClass.Class) {
                    foreach (Feat feat in characterClass.Feats[level.Levels]) {
                        AddFeat(feat);
                    }
                    level.Levels++;
                    Utils.Push<Dice>(ref hitDice, dice);
                    HitPoints += dice.Value + additionalHp + Constitution.Modifier;
                    updateAvailableSpells(level);
                    RecalculateAttacks();
                    return;
                }
            }
            CharacterLevel newLevel = new CharacterLevel();
            newLevel.Class = characterClass;
            foreach (Feat feat in characterClass.Feats[0]) {
                AddFeat(feat);
            }
            newLevel.Levels = 1;
            if (levels.Length == 0) { // maximum hitpoints when this is the first level
                dice.Value = dice.MaxValue;
            }
            Utils.Push<CharacterLevel>(ref levels, newLevel);
            Utils.Push<Dice>(ref hitDice, dice);
            HitPoints += dice.Value + additionalHp + Constitution.Modifier;
            foreach (Proficiency proficiency in characterClass.Proficiencies) {
                if (!IsProficient(proficiency)) {
                    Utils.Push<Proficiency>(ref proficiencies, proficiency);
                }
            }
            updateAvailableSpells(newLevel);
            RecalculateAttacks();
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
                spells = new AvailableSpells(level.Class);
                spells.CharacterClass = level.Class;
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

        public void AddProficiency(Proficiency proficiency) {
            Utils.PushUnique<Proficiency>(ref proficiencies, proficiency);
        }

        private void SetRace(CharacterRace race) {
            this.race = race;
            foreach (Feat feat in race.RacialFeats) {
                AddFeat(feat);
            }
            Speed = race.Speed;
            Size = race.Size;
        }

        public void AddFeat(Feat feat) {
            if (Utils.PushUnique<Feat>(ref feats, feat))
                feat.Apply(this);
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

    }
}