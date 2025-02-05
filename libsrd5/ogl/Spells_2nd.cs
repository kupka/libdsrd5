using System;

namespace srd5 {
    public partial struct Spells {
        /* TODO */
        public static readonly Spell AcidArrow = new Spell(Spells.ID.ACID_ARROW, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 90, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Aid = new Spell(Spells.ID.AID, SpellSchool.ABJURATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell AlterSelf = new Spell(Spells.ID.ALTER_SELF, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell AnimalMessenger = new Spell(Spells.ID.ANIMAL_MESSENGER, SpellSchool.ENCHANTMENT, SpellLevel.SECOND, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_DAY, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ArcaneLock = new Spell(Spells.ID.ARCANE_LOCK, SpellSchool.ABJURATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.UNTIL_DISPELLED, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ArcanistsMagicAura = new Spell(Spells.ID.ARCANISTS_MAGIC_AURA, SpellSchool.ILLUSION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_DAY, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Augury = new Spell(Spells.ID.AUGURY, SpellSchool.DIVINATION, SpellLevel.SECOND, CastingTime.ONE_MINUTE, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Barkskin = new Spell(Spells.ID.BARKSKIN, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell BlindnessDeafness = new Spell(Spells.ID.BLINDNESS_DEAFNESS, SpellSchool.NECROMANCY, SpellLevel.SECOND, CastingTime.ONE_ACTION, 30, V, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Blur = new Spell(Spells.ID.BLUR, SpellSchool.ILLUSION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, V, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell BrandingSmite = new Spell(Spells.ID.BRANDING_SMITE, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.BONUS_ACTION, 0, V, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell CalmEmotions = new Spell(Spells.ID.CALM_EMOTIONS, SpellSchool.ENCHANTMENT, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VS, SpellDuration.ONE_MINUTE, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell ContinualFlame = new Spell(Spells.ID.CONTINUAL_FLAME, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.UNTIL_DISPELLED, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Darkness = new Spell(Spells.ID.DARKNESS, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VM, SpellDuration.TEN_MINUTES, 15, 0, doNothing);
        /* TODO */
        public static readonly Spell Darkvision = new Spell(Spells.ID.DARKVISION, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell DetectThoughts = new Spell(Spells.ID.DETECT_THOUGHTS, SpellSchool.DIVINATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell EnhanceAbility = new Spell(Spells.ID.ENHANCE_ABILITY, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell EnlargeReduce = new Spell(Spells.ID.ENLARGE_REDUCE, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Enthrall = new Spell(Spells.ID.ENTHRALL, SpellSchool.ENCHANTMENT, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell FindSteed = new Spell(Spells.ID.FIND_STEED, SpellSchool.CONJURATION, SpellLevel.SECOND, CastingTime.TEN_MINUTES, 30, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell FindTraps = new Spell(Spells.ID.FIND_TRAPS, SpellSchool.DIVINATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 120, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell FlameBlade = new Spell(Spells.ID.FLAME_BLADE, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.BONUS_ACTION, 0, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell FlamingSphere = new Spell(Spells.ID.FLAMING_SPHERE, SpellSchool.CONJURATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell GentleRepose = new Spell(Spells.ID.GENTLE_REPOSE, SpellSchool.NECROMANCY, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.TEN_DAYS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell GustofWind = new Spell(Spells.ID.GUST_OF_WIND, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_MINUTE, 60, 0, doNothing);
        /* TODO */
        public static readonly Spell HeatMetal = new Spell(Spells.ID.HEAT_METAL, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);

        public static readonly Spell HoldPerson = new Spell(
            ID.HOLD_PERSON, SpellSchool.ENCHANTMENT, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VSM,
            SpellDuration.ONE_MINUTE, 0, 20, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                // one target per slot above 2nd
                for (int i = 0; i < (int)slot - 1 && i < targets.Length; i++) {
                    Combattant target = targets[i];
                    // only affect humanoid monsters
                    if (target is Monster monster) {
                        if (monster.Type != Monsters.Type.HUMANOID) {
                            GlobalEvents.AffectBySpell(caster, ID.HOLD_PERSON, monster, false);
                            continue;
                        }
                    }
                    // Wisdom save
                    if (!target.DC(ID.HOLD_PERSON, dc, AbilityType.WISDOM) && target.AddCondition(ConditionType.PARALYZED)) {
                        GlobalEvents.AffectBySpell(caster, ID.HOLD_PERSON, target, true);
                        target.AddEndOfTurnEvent(delegate () {
                            bool success = target.DC(ID.HOLD_PERSON, dc, AbilityType.WISDOM);
                            if (success) {
                                target.RemoveCondition(ConditionType.PARALYZED);
                            }
                            return success;
                        });
                    } else {
                        GlobalEvents.AffectBySpell(caster, ID.HOLD_PERSON, target, false);
                    }
                }
            }
        );

        /* TODO */
        public static readonly Spell Invisibility = new Spell(Spells.ID.INVISIBILITY, SpellSchool.ILLUSION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Knock = new Spell(Spells.ID.KNOCK, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, V, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell LesserRestoration = new Spell(Spells.ID.LESSER_RESTORATION, SpellSchool.ABJURATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Levitate = new Spell(Spells.ID.LEVITATE, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell LocateAnimalsorPlants = new Spell(Spells.ID.LOCATE_ANIMALS_OR_PLANTS, SpellSchool.DIVINATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell LocateObject = new Spell(Spells.ID.LOCATE_OBJECT, SpellSchool.DIVINATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell MagicMouth = new Spell(Spells.ID.MAGIC_MOUTH, SpellSchool.ILLUSION, SpellLevel.SECOND, CastingTime.ONE_MINUTE, 30, VSM, SpellDuration.UNTIL_DISPELLED, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell MagicWeapon = new Spell(Spells.ID.MAGIC_WEAPON, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.BONUS_ACTION, 0, VS, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell MirrorImage = new Spell(Spells.ID.MIRROR_IMAGE, SpellSchool.ILLUSION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell MistyStep = new Spell(Spells.ID.MISTY_STEP, SpellSchool.CONJURATION, SpellLevel.SECOND, CastingTime.BONUS_ACTION, 0, V, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Moonbeam = new Spell(Spells.ID.MOONBEAM, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 120, VSM, SpellDuration.ONE_MINUTE, 5, 0, doNothing);
        /* TODO */
        public static readonly Spell PassWithoutTrace = new Spell(Spells.ID.PASS_WITHOUT_TRACE, SpellSchool.ABJURATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell PrayerofHealing = new Spell(Spells.ID.PRAYER_OF_HEALING, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.TEN_MINUTES, 30, V, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ProtectionfromPoison = new Spell(Spells.ID.PROTECTION_FROM_POISON, SpellSchool.ABJURATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell RayofEnfeeblement = new Spell(Spells.ID.RAY_OF_ENFEEBLEMENT, SpellSchool.NECROMANCY, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell RopeTrick = new Spell(Spells.ID.ROPE_TRICK, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ScorchingRay = new Spell(Spells.ID.SCORCHING_RAY, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 120, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell SeeInvisibility = new Spell(Spells.ID.SEE_INVISIBILITY, SpellSchool.DIVINATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Shatter = new Spell(Spells.ID.SHATTER, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.INSTANTANEOUS, 10, 0, doNothing);
        /* TODO */
        public static readonly Spell Silence = new Spell(Spells.ID.SILENCE, SpellSchool.ILLUSION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 120, VS, SpellDuration.TEN_MINUTES, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell SpiderClimb = new Spell(Spells.ID.SPIDER_CLIMB, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell SpikeGrowth = new Spell(Spells.ID.SPIKE_GROWTH, SpellSchool.TRANSMUTATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 150, VSM, SpellDuration.TEN_MINUTES, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell SpiritualWeapon = new Spell(Spells.ID.SPIRITUAL_WEAPON, SpellSchool.EVOCATION, SpellLevel.SECOND, CastingTime.BONUS_ACTION, 60, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Suggestion = new Spell(Spells.ID.SUGGESTION, SpellSchool.ENCHANTMENT, SpellLevel.SECOND, CastingTime.ONE_ACTION, 30, VM, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell WardingBond = new Spell(Spells.ID.WARDING_BOND, SpellSchool.ABJURATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Web = new Spell(Spells.ID.WEB, SpellSchool.CONJURATION, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_HOUR, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell ZoneofTruth = new Spell(Spells.ID.ZONE_OF_TRUTH, SpellSchool.ENCHANTMENT, SpellLevel.SECOND, CastingTime.ONE_ACTION, 60, VS, SpellDuration.TEN_MINUTES, 15, 0, doNothing);
    }
}