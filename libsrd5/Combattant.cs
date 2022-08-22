using System;

namespace srd5 {
    public class Attack {
        public string Name { get; set; }
        public int AttackBonus { get; internal set; }
        public Damage Damage { get; internal set; }
        public Damage AdditionalDamage { get; internal set; }
        public int Reach { get; internal set; }
        public int RangeNormal { get; internal set; }
        public int RangeLong { get; internal set; }
        public Attack(string name, int attackBonus, Damage damage, int reach = 5, int rangeNormal = 0, int rangeLong = 0, Damage additionalDamage = null) {
            Name = name;
            AttackBonus = attackBonus;
            Damage = damage;
            AdditionalDamage = additionalDamage;
            Reach = reach;
            RangeNormal = rangeNormal;
            RangeLong = rangeLong;
        }

        public static Attack FromWeapon(int attackBonus, string damageString, Weapon weapon, Damage additionalDamage = null) {
            return new Attack(weapon.Name, attackBonus,
                new Damage(weapon.Damage.Type, damageString), weapon.Reach, weapon.RangeNormal, weapon.RangeLong, additionalDamage);
        }
    }

    public class AvailableSpells {
        public CharacterClass CharacterClass { get; internal set; }
        public Spell[] KnownSpells {
            get {
                return knownSpells;
            }
        }
        private Spell[] knownSpells = new Spell[0];
        public Spell[] PreparedSpells {
            get {
                return preparedSpells;
            }
        }
        private Spell[] preparedSpells = new Spell[0];
        public Spell[] BonusPreparedSpells { get; internal set; } = new Spell[0];
        public int[] SlotsMax { get; internal set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] SlotsCurrent { get; internal set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public AvailableSpells(AbilityType abilityType) {
            CharacterClass clazz = new CharacterClass();
            clazz.SpellCastingAbility = abilityType;
            clazz.MustPrepareSpells = false;
            CharacterClass = clazz;
        }
        public AvailableSpells(CharacterClass clazz) {
            CharacterClass = clazz;
        }

        public void AddKnownSpell(params Spell[] spells) {
            Utils.Push<Spell>(ref knownSpells, spells);
        }

        public void AddPreparedSpell(params Spell[] spells) {
            foreach (Spell spell in spells)
                if (Array.IndexOf(knownSpells, spell) == -1) return;
            Utils.Push<Spell>(ref preparedSpells, spells);
        }

        /// <summary>
        /// Calculates the spell cast DC for the sheet. Assumes that this object belongs to this sheet.
        /// </summary>
        public int GetSpellCastDC(CharacterSheet sheet) {
            int dc = 8;
            dc += sheet.GetAbility(CharacterClass.SpellCastingAbility).Modifier;
            dc += sheet.ProficiencyBonus;
            return dc;
        }

        /// <summary>
        /// Calculates the spell cast DC for the Monster. Assumes that this object belongs to this Monster.
        /// </summary>
        public int GetSpellCastDC(Combattant combattant) {
            if (combattant is CharacterSheet) return GetSpellCastDC((CharacterSheet)combattant);
            Monster monster = (Monster)combattant;
            return monster.SpellCastDC;
        }

        /// <summary>
        /// Get the spellcasting modifier for the Combattant. Assumes that this object belongs to this sheet.
        /// <summary>
        public int GetSpellcastingModifier(Combattant combattant) {
            AbilityType spellAbility = CharacterClass.SpellCastingAbility;
            return combattant.GetAbility(spellAbility).Modifier;
        }

    }

    public abstract class Combattant {
        public int Speed { get; internal set; } = 30;
        public string Name { get; set; }
        public Ability Strength { get; internal set; } = new Ability(AbilityType.STRENGTH, 10);
        public Ability Dexterity { get; internal set; } = new Ability(AbilityType.DEXTERITY, 10);
        public Ability Constitution { get; internal set; } = new Ability(AbilityType.CONSTITUTION, 10);
        public Ability Intelligence { get; internal set; } = new Ability(AbilityType.INTELLIGENCE, 10);
        public Ability Wisdom { get; internal set; } = new Ability(AbilityType.WISDOM, 10);
        public Ability Charisma { get; internal set; } = new Ability(AbilityType.CHARISMA, 10);
        public virtual int ArmorClass { get; internal set; }
        public int ArmorClassModifier { get; internal set; }
        public int HitPoints { get; set; }
        public virtual int HitPointsMax { get; internal set; }
        public Attack[] MeleeAttacks { get; internal set; } = new Attack[0];
        public Attack[] RangedAttacks { get; internal set; } = new Attack[0];
        public Attack BonusAttack { get; internal set; }
        public Size Size { get; internal set; }
        public Effect[] Effects { get { return effects; } }
        private Effect[] effects = new Effect[0];
        public ConditionType[] Conditions { get { return conditions; } }
        private ConditionType[] conditions = new ConditionType[0];
        public Proficiency[] Proficiencies { get { return proficiencies; } }
        protected Proficiency[] proficiencies = new Proficiency[0];
        public int EffectiveLevel { get; protected set; }
        public AvailableSpells[] AvailableSpells {
            get {
                return availableSpells;
            }
        }
        private AvailableSpells[] availableSpells = new AvailableSpells[0];
        public abstract int ProficiencyBonus {
            get;
        }

        public void AddEffect(Effect effect) {
            bool pushed = Utils.PushUnique<Effect>(ref effects, effect);
            if (pushed)
                effect.Apply(this);
        }

        public void AddEffects(params Effect[] effects) {
            foreach (Effect effect in effects) {
                AddEffect(effect);
            }
        }

        public void RemoveEffect(Effect effect) {
            RemoveResult result = Utils.RemoveSingle<Effect>(ref effects, effect);
            if (result == RemoveResult.NOT_FOUND) return;
            if (result == RemoveResult.REMOVED_AND_GONE)
                effect.Unapply(this);
        }

        public bool AddCondition(ConditionType condition) {
            // don't add if immune
            if (HasEffect(srd5.Effects.Immunity(condition))) return false;
            if (Utils.PushUnique<ConditionType>(ref conditions, condition))
                condition.Apply(this);
            GlobalEvents.ChangedCondition(this, condition);
            return true;
        }
        public void RemoveCondition(ConditionType condition) {
            RemoveResult result = Utils.RemoveSingle<ConditionType>(ref conditions, condition);
            if (result == RemoveResult.REMOVED_AND_GONE)
                condition.Unapply(this);
            GlobalEvents.ChangedCondition(this, condition, true);
        }

        public bool HasCondition(ConditionType condition) {
            return Array.IndexOf(conditions, condition) >= 0;
        }

        internal void AddAvailableSpells(AvailableSpells spells) {
            Utils.Push<AvailableSpells>(ref availableSpells, spells);
        }

        public bool HasEffect(Effect type) {
            return Array.IndexOf(effects, type) >= 0;
        }

        public bool IsImmune(DamageType type) {
            return HasEffect(srd5.Effects.Immunity(type));
        }

        public bool IsResistant(DamageType type) {
            return HasEffect(srd5.Effects.Resistance(type));
        }

        public bool IsVulnerable(DamageType type) {
            return HasEffect(srd5.Effects.Vulnerability(type));
        }

        public void AddProficiency(Proficiency proficiency) {
            Utils.PushUnique<Proficiency>(ref proficiencies, proficiency);
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

        public bool IsProficient(AbilityType abilityType) {
            if (abilityType == AbilityType.NONE) return false;
            Proficiency proficiency = (Proficiency)Enum.Parse(typeof(Proficiency), abilityType.ToString());
            return IsProficient(proficiency);
        }

        /// <summary>
        /// Apply the correct amount of damage of the given type to this Combattant, taking immunities, resistances and vulnerabilities into account.
        /// </summary>
        public void TakeDamage(DamageType type, int amount) {
            if (amount < 0) throw new Srd5ArgumentException("Amount must be a positive integer or zero");
            if (IsImmune(type)) return;
            if (IsResistant(type)) amount /= 2;
            if (IsVulnerable(type)) amount *= 2;
            if (HitPoints == 0) {
                // TODO: Implement Death Saves
                return;
            }
            GlobalEvents.ReceivedDamage(this, amount, type);
            HitPoints = Math.Max(0, HitPoints - amount);
            if (HitPoints == 0) AddCondition(ConditionType.UNCONSCIOUS);
        }

        /// <summary>
        /// Heals the specified amount of damage. The healed hitpoints cannot exceed the maximum hitpoints of this combattant.
        /// </summary>
        public void HealDamage(int amount) {
            if (amount < 0) throw new Srd5ArgumentException("Amount must be a positive integer or zero");
            if (HitPoints == 0) RemoveCondition(ConditionType.UNCONSCIOUS);
            GlobalEvents.ReceivedHealing(this, amount);
            HitPoints = Math.Min(HitPoints + amount, HitPointsMax);
        }

        /// <summary>
        /// Retrieves the Ability value by AbilityType.
        /// </summary>
        public Ability GetAbility(AbilityType type) {
            switch (type) {
                case AbilityType.STRENGTH:
                    return Strength;
                case AbilityType.CONSTITUTION:
                    return Constitution;
                case AbilityType.DEXTERITY:
                    return Dexterity;
                case AbilityType.INTELLIGENCE:
                    return Intelligence;
                case AbilityType.WISDOM:
                    return Wisdom;
                case AbilityType.CHARISMA:
                    return Charisma;
                default:
                    throw new Srd5ArgumentException("No value for this AbilityType");
            }
        }

        /// <summary>
        /// Roll a DC (difficulty check) against the specified Ability
        /// </summary>
        public bool DC(object source, int dc, AbilityType type, bool advantage = false, bool disadvantage = false) {
            if (source != null) {
                if (source is Spells.ID && HasEffect(Effect.MAGIC_RESISTANCE))
                    advantage = true;
            }
            Ability ability = GetAbility(type);
            Dice d20 = srd5.Dice.D20;
            int additionalModifiers = 0;
            if (HasEffect(Effect.RESISTANCE)) {
                additionalModifiers += Dice.D4.Value;
                RemoveEffect(Effect.RESISTANCE);
                GlobalEvents.ActivateEffect(this, Effect.RESISTANCE);
            }
            if (IsProficient(type)) {
                additionalModifiers += ProficiencyBonus;
            }
            if (advantage && !disadvantage) {
                d20 = srd5.Dice.D20Advantage;
            }
            if (disadvantage && !advantage) {
                d20 = srd5.Dice.D20Disadvantage;
            }
            Dices.onDiceRolled(d20);
            bool success = d20.Value + ability.Modifier + additionalModifiers >= dc;
            if (d20.Value == 20) success = true;
            if (d20.Value == 1) success = false;
            if (type == AbilityType.STRENGTH && HasEffect(Effect.FAIL_STRENGTH_CHECK)) success = false;
            if (type == AbilityType.DEXTERITY && HasEffect(Effect.FAIL_DEXERITY_CHECK)) success = false;
            if (HasEffect(Effect.LEGENDARY_RESISTANCE) && !success) { // Allow to turn fail into success
                success = true;
                RemoveEffect(Effect.LEGENDARY_RESISTANCE);
                GlobalEvents.ActivateEffect(this, Effect.LEGENDARY_RESISTANCE);
            }
            GlobalEvents.RolledDC(this, ability, dc, d20.Value, success);
            return success;
        }


        private TurnEvent[] endOfTurnEvents = new TurnEvent[0];

        /// <summary>
        /// Adds a piece of code to be evaluated at the end of this combattatant's turn
        /// </summary>
        public void AddEndOfTurnEvent(TurnEvent turnEvent) {
            Utils.Push<TurnEvent>(ref endOfTurnEvents, turnEvent);
        }

        public void OnEndOfTurn() {
            for (int i = 0; i < endOfTurnEvents.Length; i++) {
                if (endOfTurnEvents[i] == null) continue;
                if (endOfTurnEvents[i](this)) {
                    endOfTurnEvents[i] = null;
                }
            }
        }

        private TurnEvent[] startOfTurnEvents = new TurnEvent[0];

        /// <summary>
        /// Adds a piece of code to be evaluated at the start of this combattatant's turn
        /// </summary>
        public void AddStartOfTurnEvent(TurnEvent turnEvent) {
            Utils.Push<TurnEvent>(ref startOfTurnEvents, turnEvent);
        }

        public void OnStartOfTurn() {
            for (int i = 0; i < startOfTurnEvents.Length; i++) {
                if (startOfTurnEvents[i] == null) continue;
                if (startOfTurnEvents[i](this)) {
                    startOfTurnEvents[i] = null;
                }
            }
        }


        /// <summary>
        /// Trys to attack the target Combattant with the specified attack. Returns true on hit, false on miss.
        /// </summary>
        public bool Attack(Attack attack, Combattant target, int distance, bool ranged = false, bool spell = false) {
            // check range / reach
            if (ranged && attack.RangeLong < distance) return false;
            if (!ranged && attack.Reach < distance) return false;
            // special effects
            if (spell && ranged && target.HasEffect(Effect.REFLECTIVE_CARAPACE)) {
                GlobalEvents.ActivateEffect(target, Effect.REFLECTIVE_CARAPACE);
                if (Dice.D6.Value == 6) { // reflect back on 6
                    this.TakeDamage(attack.Damage.Type, attack.Damage.Dices.Roll());
                }
                return false;
            }
            int attackRoll = Dice.D20.Value;
            // Determine advantage and disadvantage
            bool hasAdvantage = attackAdvantageEffect(attack, target, distance, ranged, spell);
            bool hasDisadvantage = attackDisadvantageEffect(attack, target, distance, ranged, spell);
            applyAttackModifyingEffects(ref hasAdvantage, ref hasDisadvantage, ref attack, ref target);
            if (hasAdvantage && !hasDisadvantage)
                attackRoll = Dice.D20Advantage.Value;
            else if (hasDisadvantage && !hasAdvantage)
                attackRoll = Dice.D20Disadvantage.Value;
            bool criticalHit = attackRoll == 20;
            bool criticalMiss = attackRoll == 1;
            if (criticalMiss) {
                GlobalEvents.RolledAttack(this, attack, target, attackRoll, false);
                return false;
            }
            int modifiedAttack = attackRoll + attack.AttackBonus;
            if (!criticalHit && modifiedAttack < target.ArmorClass) {
                GlobalEvents.RolledAttack(this, attack, target, attackRoll, false);
                return false;
            }
            // Check if auto critical hit conditions apply
            if (HasEffect(Effect.AUTOMATIC_CRIT_ON_HIT)) criticalHit = true;
            if (target.HasEffect(Effect.AUTOMATIC_CRIT_ON_BEING_HIT_WITHIN_5_FT) && distance <= 5) criticalHit = true;
            GlobalEvents.RolledAttack(this, attack, target, attackRoll, true, criticalHit);
            if (criticalHit) {
                target.TakeDamage(attack.Damage.Type, attack.Damage.Dices.RollCritical());
                if (attack.AdditionalDamage != null) target.TakeDamage(attack.AdditionalDamage.Type, attack.AdditionalDamage.Dices.RollCritical());
            } else {
                target.TakeDamage(attack.Damage.Type, attack.Damage.Dices.Roll());
                if (attack.AdditionalDamage != null) target.TakeDamage(attack.AdditionalDamage.Type, attack.AdditionalDamage.Dices.Roll());
            }
            return true;
        }

        private AttackModifyingEffect[] attackModifyingEffects = new AttackModifyingEffect[0];

        public void AddAttackModifyingEffect(AttackModifyingEffect effect) {
            Utils.Push<AttackModifyingEffect>(ref attackModifyingEffects, effect);
        }

        private void applyAttackModifyingEffects(ref bool advantage, ref bool disadvantage, ref Attack attack, ref Combattant target) {
            for (int i = 0; i < attackModifyingEffects.Length; i++) {
                if (attackModifyingEffects[i] == null) continue;
                if (attackModifyingEffects[i](ref advantage, ref disadvantage, ref attack, ref target)) {
                    attackModifyingEffects[i] = null;
                }
            }
        }

        /// <summary>
        /// Function to return true or false if some specfic effect is in place that grants this
        /// combattant advantage in their attack roll against the target.
        /// </summary>
        private bool attackAdvantageEffect(Attack attack, Combattant target, int distance, bool ranged, bool spell) {
            bool advantage = HasEffect(Effect.ADVANTAGE_ON_ATTACK);
            advantage = advantage || target.HasEffect(Effect.ADVANTAGE_ON_BEING_ATTACKED);
            return advantage;
        }

        /// <summary>
        /// Function to return true or false if some specfic effect is in place that grants this
        /// combattant disadvantage in their attack roll against the target.
        /// </summary>
        private bool attackDisadvantageEffect(Attack attack, Combattant target, int distance, bool ranged, bool spell) {
            bool disadvantage = HasEffect(Effect.DISADVANTAGE_ON_ATTACK);
            disadvantage = disadvantage || target.HasEffect(Effect.DISADVANTAGE_ON_BEING_ATTACKED);
            disadvantage = disadvantage || (ranged && (distance <= 5 || distance > attack.RangeNormal));
            return disadvantage;
        }
    }

    /// <summary>
    /// Describes an event that shall be executed at the end or beginning 
    /// of this combattant's turn. 
    /// The event is considered finished when the delegate returns true.
    /// </summary>
    public delegate bool TurnEvent(Combattant combattant);

    /// <summary>
    /// Describes an effect that modifies this combattant's attack roll. 
    /// The effect is considered finished when the delegate returns true.
    /// </summary>
    public delegate bool AttackModifyingEffect(ref bool advantage, ref bool disadvantage, ref Attack attack, ref Combattant target);
}