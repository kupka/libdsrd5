using System;
using System.Collections;
using System.Runtime.InteropServices;

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
        CONSTITUTION_19,
        INTELLIGENCE_19,

        // Death Saves Effects when not stabilized
        FIGHTING_DEATH,
        FIGHTING_DEATH_SAVE_FAIL_1,
        FIGHTING_DEATH_SAVE_FAIL_2,
        FIGHTING_DEATH_SAVE_SUCCESS_1,
        FIGHTING_DEATH_SAVE_SUCCESS_2,
        FIGHTING_DEATH_STABILIZED,

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
        CANNOT_REGAIN_HITPOINTS,
        GRAPPLING,

        // Spell Effects
        SPELL_BANE,
        SPELL_BLESS,
        SPELL_ENTANGLE,
        SPELL_FAIRIE_FIRE,
        SPELL_JUMP,
        SPELL_LIGHT,
        SPELL_LONGSTRIDER,
        SPELL_RAY_OF_FROST,
        SPELL_RESISTANCE,
        SPELL_GUIDANCE,
        SPELL_DIVINE_FAVOR,
        SPELL_HIDEOUS_LAUGHTER,
        SPELL_COMMAND_GROVEL,
        SPELL_FEATHER_FALL,
        SPELL_PROTECTION_FROM_EVIL_AND_GOOD,
        SPELL_SHIELD,
        SPELL_SHIELD_OF_FAITH,
        // Curses
        CURSE_MUMMY_ROT,
        CURSE_RAKSHASA,
        CURSE_WEREBEAR,
        CURSE_WEREBOAR,
        CURSE_WERERAT,
        CURSE_WERETIGER,
        CURSE_WEREWOLF,
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
        FIRE_ELEMENTAL_IGNITE,
        GHAST_CLAWS_PARALYZATION,
        GHOUL_CLAWS_PARALYZATION,
        GIANT_RAT_DISEASED_BITE,
        GIANT_SPIDER_WEB,
        HOMUNCULUS_POISON,
        HOMUNCULUS_POISON_UNCONCIOUSNESS,
        INFERNAL_WOUND_BEARDED_DEVIL,
        INFERNAL_WOUND_HORNED_DEVIL,
        KRAKEN_SWALLOW,
        LICH_PARALYZATION,
        MAGMIN_IGNITE,
        OTYUGH_DISEASE,
        PHASE_SPIDER_POISON,
        PSEUDO_DRAGON_POISON,
        PSEUDO_DRAGON_POISON_UNCONSCIOUS,
        QUASIT_POISON,
        PIT_FIEND_POISON,
        RUG_SMOTHER,
        SPRITE_POISON,
        SPRITE_POISON_UNCONCIOUS,
        STIRGE_BLOOD_DRAIN_EFFECT,
        STIRGE_BLOOD_DRAINING,
        UNABLE_TO_BREATHE,

        // Feat Effects
        LEGENDARY_RESISTANCE,
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
            int turn = 0;
            int duration;
            int dc;
            switch (effect) {
                case Effect.HEAVY_ARMOR_SPEED_PENALITY:
                    combattant.Speed -= 10;
                    break;
                case Effect.CONSTITUTION_19:
                case Effect.INTELLIGENCE_19:
                    applyAbilityEffect(effect, combattant);
                    break;
                case Effect.PROTECTION:
                    combattant.ArmorClassModifier++;
                    break;
                case Effect.ONE_EXTRA_ATTACK:
                case Effect.TWO_EXTRA_ATTACKS:
                case Effect.THREE_EXTRA_ATTACKS:
                    if (combattant is CharacterSheet sheet) {
                        sheet.RecalculateAttacks();
                    };
                    break;
                case Effect.SPELL_LONGSTRIDER:
                    combattant.Speed += 10;
                    break;
                case Effect.SPELL_ENTANGLE:
                    combattant.AddCondition(ConditionType.RESTRAINED);
                    break;
                case Effect.SPELL_FAIRIE_FIRE:
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
                case Effect.FIRE_ELEMENTAL_IGNITE:
                    combattant.AddStartOfTurnEvent(delegate () {
                        if (!combattant.HasEffect(effect)) return true;
                        combattant.TakeDamage(effect, DamageType.FIRE, "1d10");
                        return false;
                    });
                    break;
                case Effect.GHAST_CLAWS_PARALYZATION:
                case Effect.GHOUL_CLAWS_PARALYZATION:
                case Effect.LICH_PARALYZATION:
                    combattant.AddCondition(ConditionType.PARALYZED);
                    dc = 10;
                    duration = 10; // one minute 
                    if (effect == Effect.LICH_PARALYZATION) dc = 18;
                    combattant.AddEndOfTurnEvent(delegate () {
                        if (!combattant.HasEffect(effect)) return true;
                        bool success = combattant.DC(effect, dc, AbilityType.CONSTITUTION);
                        if (turn++ >= duration) success = true;
                        if (success) combattant.RemoveEffect(effect);
                        return success;
                    });
                    break;
                case Effect.GIANT_RAT_DISEASED_BITE:
                    // TODO: contract a disease. 
                    // Until the disease is cured, the target can't regain hit points except by magical means, 
                    // and the target's hit point maximum decreases by 3 (1d6) every 24 hours. 
                    // If the target's hit point maximum drops to 0 as a result of this disease, the target dies.
                    break;
                case Effect.CURSE_MUMMY_ROT:
                    // TODO: The cursed target can't regain hit points, and its hit point maximum decreases by 10 
                    // (3d6) for every 24 hours that elapse. If the curse reduces the target's hit point maximum to 0, 
                    // the target dies, and its body turns to dust. The curse lasts until removed by the remove curse 
                    // spell or other magic.
                    combattant.AddEffect(Effect.CANNOT_REGAIN_HITPOINTS);
                    break;
                case Effect.HOMUNCULUS_POISON_UNCONCIOUSNESS:
                    combattant.AddConditions(ConditionType.POISONED, ConditionType.UNCONSCIOUS);
                    break;
                case Effect.MAGMIN_IGNITE:
                    combattant.AddStartOfTurnEvent(delegate () {
                        if (!combattant.HasEffect(effect)) return true;
                        combattant.TakeDamage(effect, DamageType.FIRE, "1d6");
                        return false;
                    });
                    break;
                case Effect.OTYUGH_DISEASE:
                    combattant.AddCondition(ConditionType.POISONED);
                    // TODO: Every 24 hours that elapse, the target must repeat the saving throw, 
                    // reducing its hit point maximum by 5 (1d10) on a failure. The disease is cured on a success. 
                    // The target dies if the disease reduces its hit point maximum to 0. 
                    // This reduction to the target's hit point maximum lasts until the disease is cured.
                    break;
                case Effect.PHASE_SPIDER_POISON:
                    combattant.AddConditions(ConditionType.POISONED, ConditionType.PARALYZED);
                    break;
                case Effect.PIT_FIEND_POISON:
                    combattant.AddCondition(ConditionType.POISONED);
                    combattant.AddEffect(Effect.CANNOT_REGAIN_HITPOINTS);
                    combattant.AddStartOfTurnEvent(delegate () {
                        if (!combattant.HasEffect(effect)) return true;
                        combattant.TakeDamage(effect, DamageType.POISON, "6d6");
                        return false;
                    });
                    combattant.AddEndOfTurnEvent(delegate () {
                        if (!combattant.HasEffect(effect)) return true;
                        if (combattant.DC(effect, 21, AbilityType.CONSTITUTION)) {
                            combattant.RemoveEffect(effect);
                            return true;
                        }
                        return false;
                    });
                    break;
                case Effect.PSEUDO_DRAGON_POISON:
                case Effect.SPRITE_POISON:
                    combattant.AddCondition(ConditionType.POISONED);
                    break;
                case Effect.PSEUDO_DRAGON_POISON_UNCONSCIOUS:
                case Effect.SPRITE_POISON_UNCONCIOUS:
                    combattant.AddCondition(ConditionType.UNCONSCIOUS);
                    // TODO: If the saving throw fails by 5 or more, the target falls unconscious for the same duration, 
                    // or until it takes damage or another creature uses an action to shake it awake.
                    break;
                case Effect.QUASIT_POISON:
                    combattant.AddCondition(ConditionType.POISONED);
                    dc = 10;
                    duration = 10; // one minute
                    combattant.AddEndOfTurnEvent(delegate () {
                        if (!combattant.HasEffect(effect)) return true;
                        bool success = combattant.DC(effect, dc, AbilityType.CONSTITUTION);
                        if (turn++ >= duration) success = true;
                        if (success) combattant.RemoveEffect(effect);
                        return success;
                    });
                    break;
                case Effect.RUG_SMOTHER:
                    if (!combattant.HasEffect(Effect.IMMUNITY_BLINDED)) combattant.AddCondition(ConditionType.BLINDED);
                    combattant.AddStartOfTurnEvent(delegate () {
                        if (combattant.HasCondition(ConditionType.GRAPPLED_DC13)) {
                            combattant.TakeDamage(effect, DamageType.BLUDGEONING, "2d6+3");
                            return false;
                        } else {
                            return true;
                        }
                    });
                    break;
                case Effect.FIGHTING_DEATH:
                    combattant.AddStartOfTurnEvent(delegate () {
                        // Don't roll if stabilized
                        if (combattant.HasEffect(Effect.FIGHTING_DEATH_STABILIZED)) {
                            combattant.RemoveEffect(Effect.FIGHTING_DEATH);
                            return true;
                        }
                        // Death Save is DC10 with no Ability
                        int deathSaveRoll = Die.D20.Value;
                        bool success = deathSaveRoll > 9;
                        GlobalEvents.RolledDC(combattant, Effect.FIGHTING_DEATH, new Ability(AbilityType.NONE, 9), 10, deathSaveRoll, success);
                        if (success) {
                            // Critical success counts as 2 successes
                            if (deathSaveRoll == 20) {
                                if (combattant.HasEffect(Effect.FIGHTING_DEATH_SAVE_SUCCESS_1) || combattant.HasEffect(Effect.FIGHTING_DEATH_SAVE_SUCCESS_2)) {
                                    combattant.RemoveEffect(Effect.FIGHTING_DEATH);
                                    return true;
                                } else {
                                    combattant.AddEffect(Effect.FIGHTING_DEATH_SAVE_SUCCESS_2);
                                    return false;
                                }
                            } else if (combattant.HasEffect(Effect.FIGHTING_DEATH_SAVE_SUCCESS_2)) {
                                // third success => stabilized
                                combattant.RemoveEffect(Effect.FIGHTING_DEATH);
                                return true;
                            } else if (combattant.HasEffect(Effect.FIGHTING_DEATH_SAVE_SUCCESS_1)) {
                                combattant.AddEffect(Effect.FIGHTING_DEATH_SAVE_SUCCESS_2);
                                return false;
                            } else {
                                combattant.AddEffect(Effect.FIGHTING_DEATH_SAVE_SUCCESS_1);
                                return false;
                            }
                        } else {
                            // Critical fail counts as 2 fails
                            if (deathSaveRoll == 1) {
                                if (combattant.HasEffect(Effect.FIGHTING_DEATH_SAVE_FAIL_1) || combattant.HasEffect(Effect.FIGHTING_DEATH_SAVE_FAIL_2)) {
                                    combattant.RemoveEffect(Effect.FIGHTING_DEATH);
                                    combattant.Die();
                                    return true;
                                } else {
                                    combattant.AddEffect(Effect.FIGHTING_DEATH_SAVE_FAIL_2);
                                    return false;
                                }
                            } else if (combattant.HasEffect(Effect.FIGHTING_DEATH_SAVE_FAIL_2)) {
                                // third fail => dead
                                combattant.RemoveEffect(Effect.FIGHTING_DEATH);
                                combattant.Die();
                                return true;
                            } else if (combattant.HasEffect(Effect.FIGHTING_DEATH_SAVE_FAIL_1)) {
                                combattant.AddEffect(Effect.FIGHTING_DEATH_SAVE_FAIL_2);
                                return false;
                            } else {
                                combattant.AddEffect(Effect.FIGHTING_DEATH_SAVE_FAIL_1);
                                return false;
                            }
                        }
                    });
                    break;
                case Effect.SPELL_COMMAND_GROVEL:
                case Effect.SPELL_HIDEOUS_LAUGHTER:
                    combattant.AddEffect(Effect.CANNOT_TAKE_ACTIONS);
                    break;
            }
        }

        public static void Unapply(this Effect effect, Combattant combattant) {
            switch (effect) {
                case Effect.HEAVY_ARMOR_SPEED_PENALITY:
                    combattant.Speed += 10;
                    break;
                case Effect.CONSTITUTION_19:
                case Effect.INTELLIGENCE_19:
                    unapplyAbilityEffect(effect, combattant);
                    break;
                case Effect.PROTECTION:
                    combattant.ArmorClassModifier--;
                    break;
                case Effect.ONE_EXTRA_ATTACK:
                case Effect.TWO_EXTRA_ATTACKS:
                case Effect.THREE_EXTRA_ATTACKS:
                    if (combattant is CharacterSheet sheet) {
                        sheet.RecalculateAttacks();
                    };
                    break;
                case Effect.SPELL_LONGSTRIDER:
                    combattant.Speed -= 10;
                    break;
                case Effect.SPELL_ENTANGLE:
                    combattant.RemoveCondition(ConditionType.RESTRAINED);
                    break;
                case Effect.SPELL_FAIRIE_FIRE:
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
                case Effect.LICH_PARALYZATION:
                    combattant.RemoveCondition(ConditionType.PARALYZED);
                    break;
                case Effect.HOMUNCULUS_POISON_UNCONCIOUSNESS:
                    combattant.RemoveConditions(ConditionType.POISONED, ConditionType.UNCONSCIOUS);
                    break;
                case Effect.OTYUGH_DISEASE:
                    combattant.RemoveCondition(ConditionType.POISONED);
                    // TODO: Every 24 hours that elapse, the target must repeat the saving throw, 
                    // reducing its hit point maximum by 5 (1d10) on a failure. The disease is cured on a success. 
                    // The target dies if the disease reduces its hit point maximum to 0. 
                    // This reduction to the target's hit point maximum lasts until the disease is cured.
                    break;
                case Effect.PHASE_SPIDER_POISON:
                    combattant.RemoveConditions(ConditionType.POISONED, ConditionType.PARALYZED);
                    break;
                case Effect.PIT_FIEND_POISON:
                    combattant.RemoveCondition(ConditionType.POISONED);
                    combattant.RemoveEffect(Effect.CANNOT_REGAIN_HITPOINTS);
                    break;
                case Effect.PSEUDO_DRAGON_POISON:
                    combattant.RemoveCondition(ConditionType.POISONED);
                    break;
                case Effect.PSEUDO_DRAGON_POISON_UNCONSCIOUS:
                    combattant.RemoveCondition(ConditionType.UNCONSCIOUS);
                    break;
                case Effect.QUASIT_POISON:
                    combattant.RemoveCondition(ConditionType.POISONED);
                    break;
                case Effect.RUG_SMOTHER:
                    combattant.RemoveCondition(ConditionType.BLINDED);
                    break;
                case Effect.CURSE_MUMMY_ROT:
                    combattant.RemoveEffect(Effect.CANNOT_REGAIN_HITPOINTS);
                    break;
                case Effect.FIGHTING_DEATH:
                    combattant.RemoveEffects(Effect.FIGHTING_DEATH_SAVE_FAIL_1,
                                             Effect.FIGHTING_DEATH_SAVE_FAIL_2,
                                             Effect.FIGHTING_DEATH_SAVE_SUCCESS_1,
                                             Effect.FIGHTING_DEATH_SAVE_SUCCESS_2,
                                             Effect.FIGHTING_DEATH_STABILIZED);
                    break;
                case Effect.SPELL_COMMAND_GROVEL:
                case Effect.SPELL_HIDEOUS_LAUGHTER:
                    combattant.RemoveEffect(Effect.CANNOT_TAKE_ACTIONS);
                    break;
            }
        }

        private static void applyAbilityEffect(Effect effect, Combattant combattant) {
            switch (effect) {
                case Effect.INTELLIGENCE_19:
                    combattant.Intelligence.AddMinimumBaseValue(19);
                    break;
                case Effect.CONSTITUTION_19:
                    combattant.Constitution.AddMinimumBaseValue(19);
                    break;
            }
        }

        private static void unapplyAbilityEffect(Effect effect, Combattant combattant) {
            switch (effect) {
                case Effect.INTELLIGENCE_19:
                    combattant.Intelligence.RemoveMinimumBaseValue(19);
                    break;
                case Effect.CONSTITUTION_19:
                    combattant.Constitution.RemoveMinimumBaseValue(19);
                    break;
            }
        }

        public static bool IsCurse(this Effect effect) {
            return Enum.GetName(typeof(Effect), effect).StartsWith("CURSE_");
        }
    }
}