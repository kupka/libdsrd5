using System;

namespace srd5 {
    public enum Effect {
        // Vulnerability against DamageType
        VULNERABILITY_ACID,
        VULNERABILITY_BLUDGEONING,
        VULNERABILITY_COLD,
        VULNERABILITY_FIRE,
        VULNERABILITY_FORCE,
        VULNERABILITY_LIGHTNING,
        VULNERABILITY_NECROTIC,
        VULNERABILITY_PIERCING,
        VULNERABILITY_POISON,
        VULNERABILITY_PSYCHIC,
        VULNERABILITY_RADIANT,
        VULNERABILITY_SLASHING,
        VULNERABILITY_THUNDER,
        VULNERABILITY_TRUE_DAMAGE, // should never be applied
        // Resistances against DamageType
        RESISTANCE_ACID,
        RESISTANCE_BLUDGEONING,
        RESISTANCE_COLD,
        RESISTANCE_FIRE,
        RESISTANCE_FORCE,
        RESISTANCE_LIGHTNING,
        RESISTANCE_NECROTIC,
        RESISTANCE_PIERCING,
        RESISTANCE_POISON,
        RESISTANCE_PSYCHIC,
        RESISTANCE_RADIANT,
        RESISTANCE_SLASHING,
        RESISTANCE_THUNDER,
        RESISTANCE_NONMAGIC,
        RESISTANCE_DAMAGE_FROM_SPELLS,
        RESISTANCE_TRUE_DAMAGE, // should never be applied
        // Immunities against DamageType
        IMMUNITY_ACID,
        IMMUNITY_BLUDGEONING,
        IMMUNITY_COLD,
        IMMUNITY_FIRE,
        IMMUNITY_FORCE,
        IMMUNITY_LIGHTNING,
        IMMUNITY_NECROTIC,
        IMMUNITY_PIERCING,
        IMMUNITY_POISON,
        IMMUNITY_PSYCHIC,
        IMMUNITY_RADIANT,
        IMMUNITY_SLASHING,
        IMMUNITY_THUNDER,
        IMMUNITY_NONMAGIC,
        IMMUNITY_DAMAGE_FROM_SPELLS,
        IMMUNITY_TRUE_DAMAGE, // should never be applied
        // Immunities against Condition
        IMMUNITY_BLINDED,
        IMMUNITY_CHARMED,
        IMMUNITY_DEAFENED,
        IMMUNITY_EXHAUSTION,
        IMMUNITY_FRIGHTENED,
        IMMUNITY_GRAPPLED,
        IMMUNITY_INCAPACITATED,
        IMMUNITY_INVISIBLE,
        IMMUNITY_PARALYZED,
        IMMUNITY_PETRIFIED,
        IMMUNITY_POISONED,
        IMMUNITY_PRONE,
        IMMUNITY_RESTRAINED,
        IMMUNITY_SLEEP,
        IMMUNITY_STUNNED,
        IMMUNITY_UNCONSCIOUS,

        // Advantage on Save Throws
        ADVANTAGE_SAVE_POISON,
        ADVANTAGE_SAVE_CHARM,

        // Attack modifiers
        ADVANTAGE_ON_ATTACK,
        DISADVANTAGE_ON_ATTACK,
        ADVANTAGE_ON_BEING_ATTACKED,
        DISADVANTAGE_ON_BEING_ATTACKED,
        AUTOMATIC_CRIT_ON_HIT,
        AUTOMATIC_CRIT_ON_BEING_HIT_WITHIN_5_FT,
        ONE_EXTRA_ATTACK,
        TWO_EXTRA_ATTACKS,
        THREE_EXTRA_ATTACKS,

        // Movement
        NO_SPEED_PENALITY_FOR_HEAVY_ARMOR,
        HEAVY_ARMOR_SPEED_PENALITY,
        SWIMMING_SPEED_40,
        IGNORE_SNOW_PENALITY,

        // Abilities
        CONSTITUION_19,
        INTELLIGENCE_19,


        // Misc./Special
        DOUBLE_PROFICIENCY_BONUS_HISTORY,
        ADDITIONAL_HP_PER_LEVEL,
        DARKVISION,
        PROTECTION,
        CANNOT_TAKE_ACTIONS,
        CANNOT_TAKE_REACTIONS,
        FAIL_STRENGTH_CHECK,
        FAIL_DEXERITY_CHECK,
        FAIL_CONSTITUTION_CHECK,
        CANNOT_REGENERATE_HITPOINTS,
        GRAPPLING,

        // Spell Effects
        ENTANGLE,
        FAIRIE_FIRE,
        JUMP,
        LIGHT,
        LONGSTRIDER,
        RAY_OF_FROST,
        RESISTANCE,

        // Attack Effects
        ABOLETH_DISEASE_TENTACLE,
        BEARDED_DEVIL_POISON,
        BONE_DEVIL_POISON,
        ATTACHED_TO_TARGET,
        COUATL_POISON,
        DROW_POISON,
        DEATH_DOG_DISEASE,
        ERINYES_POISON,
        ETTERCAP_POISON,
        ETTERCAP_WEB,
        FIRE_ELEMENTAL_TOUCH,
        GHAST_CLAWS_PARALYZATION,
        GHOUL_CLAWS_PARALYZATION,
        GIANT_RAT_DISEASED_BITE,
        GIANT_SPIDER_WEB,
        HOMUNCULUS_POISON,
        HOMUNCULUS_POISON_UNCONCIOUSNESS,
        INFERNAL_WOUND_BEARDED_DEVIL,
        INFERNAL_WOUND_HORNED_DEVIL,
        UNABLE_TO_BREATHE,

        // Feat Effects
        LEGENDARY_RESISTANCE
    }

    public static class Effects {
        public static Effect Resistance(DamageType damage) {
            string name = "RESISTANCE_" + Enum.GetName(typeof(DamageType), damage);
            return (Effect)Enum.Parse(typeof(Effect), name);
        }

        public static Effect Immunity(DamageType damage) {
            string name = "IMMUNITY_" + Enum.GetName(typeof(DamageType), damage);
            return (Effect)Enum.Parse(typeof(Effect), name);
        }

        public static Effect Vulnerability(DamageType damage) {
            string name = "VULNERABILITY_" + Enum.GetName(typeof(DamageType), damage);
            return (Effect)Enum.Parse(typeof(Effect), name);
        }

        public static Effect Immunity(ConditionType condition) {
            string conditionName = Enum.GetName(typeof(ConditionType), condition);
            if (conditionName.IndexOf("EXHAUSTED") == 0) conditionName = "EXHAUSTION";
            if (conditionName.IndexOf("GRAPPLED") == 0) conditionName = "GRAPPLED";
            string name = "IMMUNITY_" + conditionName;
            return (Effect)Enum.Parse(typeof(Effect), name);
        }
    }

    public static class EffectExtension {
        public static void Apply(this Effect effect, Combattant combattant) {
            switch (effect) {
                case Effect.HEAVY_ARMOR_SPEED_PENALITY:
                    combattant.Speed -= 10;
                    break;
                case Effect.CONSTITUION_19:
                case Effect.INTELLIGENCE_19:
                    applyAbilityEffect(effect, combattant);
                    break;
                case Effect.PROTECTION:
                    combattant.ArmorClassModifier++;
                    break;
                case Effect.ONE_EXTRA_ATTACK:
                case Effect.TWO_EXTRA_ATTACKS:
                case Effect.THREE_EXTRA_ATTACKS:
                    ((CharacterSheet)combattant).RecalculateAttacks();
                    break;
                case Effect.LONGSTRIDER:
                    combattant.Speed += 10;
                    break;
                case Effect.ENTANGLE:
                    combattant.AddCondition(ConditionType.RESTRAINED);
                    break;
                case Effect.FAIRIE_FIRE:
                    combattant.AddEffect(Effect.ADVANTAGE_ON_BEING_ATTACKED);
                    break;
                case Effect.IMMUNITY_TRUE_DAMAGE:
                case Effect.RESISTANCE_TRUE_DAMAGE:
                case Effect.VULNERABILITY_TRUE_DAMAGE:
                    throw new Srd5ArgumentException("Do not aply True Damage effects.");
                case Effect.COUATL_POISON:
                    combattant.AddCondition(ConditionType.POISONED);
                    combattant.AddCondition(ConditionType.UNCONSCIOUS);
                    break;
                case Effect.BEARDED_DEVIL_POISON:
                case Effect.BONE_DEVIL_POISON:
                case Effect.DEATH_DOG_DISEASE:
                case Effect.DROW_POISON:
                case Effect.ERINYES_POISON:
                case Effect.ETTERCAP_POISON:
                case Effect.HOMUNCULUS_POISON:
                    combattant.AddCondition(ConditionType.POISONED);
                    break;
                case Effect.ETTERCAP_WEB:
                    combattant.AddCondition(ConditionType.RESTRAINED);
                    break;
                case Effect.FIRE_ELEMENTAL_TOUCH:
                    combattant.AddStartOfTurnEvent(delegate (Combattant combattant1) {
                        if (!combattant1.HasEffect(Effect.FIRE_ELEMENTAL_TOUCH)) return true;
                        combattant1.TakeDamage(DamageType.FIRE, "1d10");
                        return false;
                    });
                    break;
                case Effect.GHAST_CLAWS_PARALYZATION:
                case Effect.GHOUL_CLAWS_PARALYZATION:
                    combattant.AddCondition(ConditionType.PARALYZED);
                    int turn = 0;
                    combattant.AddEndOfTurnEvent(delegate (Combattant combattant1) {
                        bool success = combattant1.DC(Effect.GHAST_CLAWS_PARALYZATION, 10, AbilityType.CONSTITUTION);
                        if (turn++ > 9) success = true;
                        if (success) combattant1.RemoveEffect(Effect.GHAST_CLAWS_PARALYZATION);
                        return success;
                    });
                    break;
                case Effect.GIANT_RAT_DISEASED_BITE:
                    // TODO: contract a disease. 
                    // Until the disease is cured, the target can't regain hit points except by magical means, 
                    // and the target's hit point maximum decreases by 3 (1d6) every 24 hours. 
                    // If the target's hit point maximum drops to 0 as a result of this disease, the target dies.
                    break;
                case Effect.HOMUNCULUS_POISON_UNCONCIOUSNESS:
                    combattant.AddConditions(ConditionType.POISONED, ConditionType.UNCONSCIOUS);
                    break;
            }
        }

        public static void Unapply(this Effect effect, Combattant combattant) {
            switch (effect) {
                case Effect.HEAVY_ARMOR_SPEED_PENALITY:
                    combattant.Speed += 10;
                    break;
                case Effect.CONSTITUION_19:
                case Effect.INTELLIGENCE_19:
                    unapplyAbilityEffect(effect, combattant);
                    break;
                case Effect.PROTECTION:
                    combattant.ArmorClassModifier--;
                    break;
                case Effect.ONE_EXTRA_ATTACK:
                case Effect.TWO_EXTRA_ATTACKS:
                case Effect.THREE_EXTRA_ATTACKS:
                    ((CharacterSheet)combattant).RecalculateAttacks();
                    break;
                case Effect.LONGSTRIDER:
                    combattant.Speed -= 10;
                    break;
                case Effect.ENTANGLE:
                    combattant.RemoveCondition(ConditionType.RESTRAINED);
                    break;
                case Effect.FAIRIE_FIRE:
                    combattant.RemoveEffect(Effect.ADVANTAGE_ON_BEING_ATTACKED);
                    break;
                case Effect.COUATL_POISON:
                    combattant.RemoveCondition(ConditionType.POISONED);
                    combattant.RemoveCondition(ConditionType.UNCONSCIOUS);
                    break;
                case Effect.BEARDED_DEVIL_POISON:
                case Effect.BONE_DEVIL_POISON:
                case Effect.DEATH_DOG_DISEASE:
                case Effect.DROW_POISON:
                case Effect.ERINYES_POISON:
                case Effect.ETTERCAP_POISON:
                case Effect.HOMUNCULUS_POISON:
                    combattant.RemoveCondition(ConditionType.POISONED);
                    break;
                case Effect.ETTERCAP_WEB:
                    combattant.RemoveCondition(ConditionType.RESTRAINED);
                    break;
                case Effect.GHAST_CLAWS_PARALYZATION:
                case Effect.GHOUL_CLAWS_PARALYZATION:
                    combattant.RemoveCondition(ConditionType.PARALYZED);
                    break;
                case Effect.HOMUNCULUS_POISON_UNCONCIOUSNESS:
                    combattant.RemoveConditions(ConditionType.POISONED, ConditionType.UNCONSCIOUS);
                    break;
            }
        }

        private static void applyAbilityEffect(Effect effect, Combattant combattant) {
            switch (effect) {
                case Effect.INTELLIGENCE_19:
                    combattant.Intelligence.AddMinimumBaseValue(19);
                    break;
                case Effect.CONSTITUION_19:
                    combattant.Constitution.AddMinimumBaseValue(19);
                    break;
            }
        }

        private static void unapplyAbilityEffect(Effect effect, Combattant combattant) {
            switch (effect) {
                case Effect.INTELLIGENCE_19:
                    combattant.Intelligence.RemoveMinimumBaseValue(19);
                    break;
                case Effect.CONSTITUION_19:
                    combattant.Constitution.RemoveMinimumBaseValue(19);
                    break;
            }
        }
    }
}