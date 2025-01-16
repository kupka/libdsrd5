using System;

namespace srd5 {
    public class HitPointMaxiumModifier : GuidClass {
        public enum RemovedByEffect {
            GREATER_RESTORATION,
            REMOVE_DISEASE,
            REMOVE_CURSE,
            AFTER_24_HOURS,
            LONG_REST
        }

        public int Amount { get; private set; }
        public RemovedByEffect RemovedBy { get; private set; }

        public HitPointMaxiumModifier(int amount, RemovedByEffect removedBy) {
            Amount = amount;
            RemovedBy = removedBy;
        }

    }

    public abstract class Combattant {
        public int Speed { get; internal set; } = 30;
        public virtual string Name { get; set; }
        public Alignment Alignment { get; set; }
        public Ability Strength { get; internal set; } = new Ability(AbilityType.STRENGTH, 10);
        public Ability Dexterity { get; internal set; } = new Ability(AbilityType.DEXTERITY, 10);
        public Ability Constitution { get; internal set; } = new Ability(AbilityType.CONSTITUTION, 10);
        public Ability Intelligence { get; internal set; } = new Ability(AbilityType.INTELLIGENCE, 10);
        public Ability Wisdom { get; internal set; } = new Ability(AbilityType.WISDOM, 10);
        public Ability Charisma { get; internal set; } = new Ability(AbilityType.CHARISMA, 10);
        public virtual int ArmorClass { get; internal set; }
        public int ArmorClassModifier { get; internal set; }
        public int HitPoints { get; set; }
        public virtual int HitPointsMax {
            get {
                return Math.Max(0, hitPointsMax + HitPointMaxiumModifiersSum);
            }
            internal set {
                hitPointsMax = value;
            }
        }
        private int hitPointsMax;
        public Attack[] MeleeAttacks { get; internal set; } = new Attack[0];
        public Attack[] RangedAttacks { get; internal set; } = new Attack[0];
        public Attack BonusAttack { get; internal set; }
        public Size Size { get; internal set; }
        public Effect[] Effects { get { return effects; } }
        private Effect[] effects = new Effect[0];
        public Feat[] Feats { get { return feats; } }
        private Feat[] feats = new Feat[0];
        public ConditionType[] Conditions { get { return conditions; } }
        private ConditionType[] conditions = new ConditionType[0];
        public Proficiency[] Proficiencies { get { return proficiencies; } }
        protected Proficiency[] proficiencies = new Proficiency[0];
        public int EffectiveLevel { get; protected set; }
        public bool Dead { get; internal set; } = false;
        public AvailableSpells[] AvailableSpells {
            get {
                return availableSpells;
            }
        }
        private AvailableSpells[] availableSpells = new AvailableSpells[0];
        public abstract int ProficiencyBonus {
            get;
        }
        public HitPointMaxiumModifier[] HitPointMaxiumModifiers {
            get {
                return hitPointMaxiumModifiers;
            }
        }
        public int HitPointMaxiumModifiersSum {
            get {
                int value = 0;
                foreach (HitPointMaxiumModifier modifier in hitPointMaxiumModifiers) {
                    value += modifier.Amount;
                }
                return value;
            }
        }
        private HitPointMaxiumModifier[] hitPointMaxiumModifiers = new HitPointMaxiumModifier[0];
        public void AddHitPointMaximumModifiers(params HitPointMaxiumModifier[] modifiers) {
            foreach (HitPointMaxiumModifier modifier in modifiers)
                Utils.PushUnique<HitPointMaxiumModifier>(ref hitPointMaxiumModifiers, modifier);
            if (HitPointsMax < HitPoints) HitPoints = HitPointsMax;
        }

        public void RemoveHitPointsMaximumModifiers(params HitPointMaxiumModifier[] modifiers) {
            foreach (HitPointMaxiumModifier modifier in modifiers)
                Utils.RemoveSingle<HitPointMaxiumModifier>(ref hitPointMaxiumModifiers, modifier);
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

        public bool HasEffect(Effect effect) {
            return Array.IndexOf(effects, effect) > -1;
        }

        public void AddFeat(Feat feat) {
            if (Utils.PushUnique<Feat>(ref feats, feat))
                feat.Apply(this);
        }

        public void AddFeats(params Feat[] feats) {
            foreach (Feat feat in feats) {
                AddFeat(feat);
            }
        }

        public bool HasFeat(Feat feat) {
            return Array.IndexOf(feats, feat) > -1;
        }

        public bool AddCondition(ConditionType condition) {
            // don't add if immune
            if (HasEffect(srd5.Effects.Immunity(condition))) return false;
            if (Utils.PushUnique<ConditionType>(ref conditions, condition))
                condition.Apply(this);
            GlobalEvents.ChangedCondition(this, condition);
            return true;
        }

        public void AddConditions(params ConditionType[] conditions) {
            foreach (ConditionType condition in conditions) {
                AddCondition(condition);
            }
        }

        public void RemoveCondition(ConditionType condition) {
            RemoveResult result = Utils.RemoveSingle<ConditionType>(ref conditions, condition);
            if (result == RemoveResult.REMOVED_AND_GONE)
                condition.Unapply(this);
            GlobalEvents.ChangedCondition(this, condition, true);
        }

        public void RemoveConditions(params ConditionType[] conditions) {
            foreach (ConditionType condition in conditions) {
                RemoveCondition(condition);
            }
        }

        public bool HasCondition(ConditionType condition) {
            return Array.IndexOf(conditions, condition) > -1;
        }

        internal void AddAvailableSpells(AvailableSpells spells) {
            Utils.Push<AvailableSpells>(ref availableSpells, spells);
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

        public bool IsProficient(Skill skill) {
            return IsProficient(skill.Proficiency());
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

        public int TakeDamage(DamageType type, string amount) {
            return TakeDamage(type, new Dices(amount).Roll());
        }

        /// <summary>
        /// Apply the correct amount of damage of the given type to this Combattant, taking immunities, resistances and vulnerabilities into account.
        /// </summary>
        public int TakeDamage(DamageType type, int amount, Spells.DCEffect dCEffect = Spells.DCEffect.NO_EFFECT, int dc = 0, AbilityType dcAbility = AbilityType.NONE, object dcSource = null) {
            if (amount < 0) throw new Srd5ArgumentException("Amount must be a positive integer or zero");
            if (IsImmune(type)) return 0;
            if (IsResistant(type)) amount /= 2;
            if (IsVulnerable(type)) amount *= 2;
            if (dc > 0) {
                bool success = DC(dcSource, dc, dcAbility);
                if (success && dCEffect == Spells.DCEffect.HALVES_DAMAGE)
                    amount /= 2;
                else if (success && dCEffect == Spells.DCEffect.NULLIFIES_DAMAGE)
                    return 0;
            }
            if (HitPoints == 0) {
                // TODO: Implement Death Saves
                return amount;
            }
            GlobalEvents.ReceivedDamage(this, amount, type);
            HitPoints = Math.Max(0, HitPoints - amount);
            OnDamageTaken();
            if (HitPoints == 0) AddCondition(ConditionType.UNCONSCIOUS);
            return amount;
        }

        /// <summary>
        /// Heals the specified amount of damage. The healed hitpoints cannot exceed the maximum hitpoints of this combattant.
        /// </summary>
        public void HealDamage(int amount) {
            if (amount < 0) throw new Srd5ArgumentException("Amount must be a positive integer or zero");
            if (HasEffect(Effect.CANNOT_REGAIN_HITPOINTS)) return; // Cannot regain hit points
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
        public bool DC(object source, int dc, AbilityType type, out int finalValue, bool advantage = false, bool disadvantage = false) {
            if (source != null) {
                if (source is Spells.ID && HasFeat(Feat.MAGIC_RESISTANCE))
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
            return this.dc(source, dc, additionalModifiers, d20, ability, advantage, disadvantage, out finalValue);
        }

        public bool DC(object source, int dc, AbilityType type, bool advantage = false, bool disadvantage = false) {
            return DC(source, dc, type, out _, advantage, disadvantage);
        }



        public bool DC(object source, int dc, Skill skill, out int finalValue, bool advantage = false, bool disadvantage = false) {
            Ability ability = GetAbility(skill.Ability());
            Dice d20 = srd5.Dice.D20;
            int additionalModifiers = 0;
            if (IsProficient(skill)) {
                additionalModifiers += ProficiencyBonus;
            }
            return this.dc(source, dc, additionalModifiers, d20, ability, advantage, disadvantage, out finalValue);
        }

        public bool DC(object source, int dc, Skill skill, bool advantage = false, bool disadvantage = false) {
            return DC(source, dc, skill, out int finalValue, advantage, disadvantage);
        }

        private bool dc(object source, int dc, int additionalModifiers, Dice d20, Ability ability, bool advantage, bool disadvantage, out int finalValue) {
            if (advantage && !disadvantage) {
                d20 = srd5.Dice.D20Advantage;
            }
            if (disadvantage && !advantage) {
                d20 = srd5.Dice.D20Disadvantage;
            }
            Dices.onDiceRolled(d20);
            finalValue = d20.Value + ability.Modifier + additionalModifiers;
            bool success = finalValue >= dc;
            if (d20.Value == 20) success = true;
            if (d20.Value == 1) success = false;
            if (ability.Type == AbilityType.STRENGTH && HasEffect(Effect.FAIL_STRENGTH_CHECK)) success = false;
            if (ability.Type == AbilityType.CONSTITUTION && HasEffect(Effect.FAIL_CONSTITUTION_CHECK)) success = false;
            if (ability.Type == AbilityType.DEXTERITY && HasEffect(Effect.FAIL_DEXERITY_CHECK)) success = false;
            if (HasEffect(Effect.LEGENDARY_RESISTANCE) && !success) { // Allow to turn fail into success
                success = true;
                RemoveEffect(Effect.LEGENDARY_RESISTANCE);
                GlobalEvents.ActivateEffect(this, Effect.LEGENDARY_RESISTANCE);
            }
            GlobalEvents.RolledDC(this, source, ability, dc, d20.Value, success);
            return success;
        }

        /// <summary>
        /// Try to escape from a grapple. This method determines the most favorable skill check (Athletics or Acrobatics)
        /// by checking proficiencies and comparing Strength and Dexterity abilities.
        /// </summary>
        public bool EscapeFromGrapple() {
            // check proficiency or skills
            bool success = false;
            int dc = 0;
            int athelicsFavor = Strength.Modifier;
            int acrobaticsFavor = Dexterity.Modifier;
            // determine grappled condition
            ConditionType grappled = ConditionType.GRAPPLED_DC12;
            foreach (ConditionType condition in Conditions) {
                if (Enum.GetName(typeof(ConditionType), condition).IndexOf("GRAPPLED_") == 0) {
                    grappled = condition;
                    dc = (int)condition - (int)ConditionType.GRAPPLED_DC12 + 12;
                    break;
                }
            }
            if (dc == 0) return false; // not grappled
            if (IsProficient(Skill.ATHLETICS)) {
                athelicsFavor += ProficiencyBonus;
            }
            if (IsProficient(Skill.ACROBATICS)) {
                acrobaticsFavor += ProficiencyBonus;
            }
            if (athelicsFavor > acrobaticsFavor) {
                success = DC(null, dc, Skill.ATHLETICS);
            } else {
                success = DC(null, dc, Skill.ACROBATICS);
            }
            if (success) {
                RemoveCondition(grappled);
            }
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
                if (endOfTurnEvents[i]()) {
                    endOfTurnEvents[i] = null;
                }
            }
        }

        internal TurnEvent[] StartOfTurnEvents = new TurnEvent[0];

        /// <summary>
        /// Adds a piece of code to be evaluated at the start of this combattatant's turn
        /// </summary>
        public void AddStartOfTurnEvent(TurnEvent turnEvent) {
            Utils.Push<TurnEvent>(ref StartOfTurnEvents, turnEvent);
        }

        public void OnStartOfTurn() {
            for (int i = 0; i < StartOfTurnEvents.Length; i++) {
                if (StartOfTurnEvents[i] == null) continue;
                if (StartOfTurnEvents[i]()) {
                    StartOfTurnEvents[i] = null;
                }
            }
        }

        private TurnEvent[] damageTakenEvents = new TurnEvent[0];

        /// <summary>
        /// Adds a piece of code to be evaluated when this combattatant takes damage
        /// </summary>
        public void AddDamageTakenEvent(TurnEvent turnEvent) {
            Utils.Push<TurnEvent>(ref damageTakenEvents, turnEvent);
        }

        public void OnDamageTaken() {
            for (int i = 0; i < damageTakenEvents.Length; i++) {
                if (damageTakenEvents[i] == null) continue;
                if (damageTakenEvents[i]()) {
                    damageTakenEvents[i] = null;
                }
            }
        }

        /// <summary>
        /// Trys to attack the target Combattant with the specified attack. Returns true on hit, false on miss.
        /// </summary>
        public bool Attack(Attack attack, Combattant target, int distance, bool ranged = false, bool spell = false, Spells.DCEffect dCEffect = Spells.DCEffect.NO_EFFECT, int dc = 0) {
            // check locked target
            if (attack.LockedTarget != null && attack.LockedTarget != target) return false;
            // check range / reach
            if (ranged && attack.RangeLong < distance) return false;
            if (!ranged && attack.Reach < distance) return false;
            // special effects
            if (spell && ranged && target.HasFeat(Feat.REFLECTIVE_CARAPACE)) {
                GlobalEvents.ActivateFeat(target, Feat.REFLECTIVE_CARAPACE);
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
                int times = 2;
                if (attack.HasProperty(srd5.Attack.Property.TRIPLE_DICE_ON_CRIT)) times = 3;
                target.TakeDamage(attack.Damage.Type, attack.Damage.Dices.RollCritical(times));
                if (attack.AdditionalDamage != null) target.TakeDamage(attack.AdditionalDamage.Type, attack.AdditionalDamage.Dices.RollCritical(times));
            } else {
                target.TakeDamage(attack.Damage.Type, attack.Damage.Dices.Roll());
                if (attack.AdditionalDamage != null) target.TakeDamage(attack.AdditionalDamage.Type, attack.AdditionalDamage.Dices.Roll());
            }
            // Hit effect
            attack.ApplyEffectOnHit(this, target);
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

        internal void Die() {
            // TODO: Implement what happens when this Combattant dies
            Dead = true;
        }
    }

    /// <summary>
    /// Describes an event that shall be executed at the end or beginning 
    /// of this combattant's turn. 
    /// The event is considered finished when the delegate returns true.
    /// </summary>
    public delegate bool TurnEvent();

    /// <summary>
    /// Describes an effect that modifies this combattant's attack roll. 
    /// The effect is considered finished when the delegate returns true.
    /// </summary>
    public delegate bool AttackModifyingEffect(ref bool advantage, ref bool disadvantage, ref Attack attack, ref Combattant target);
}