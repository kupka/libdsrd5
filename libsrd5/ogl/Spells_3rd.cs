namespace srd5 {
    public partial struct Spells {
        /* TODO */
        public static readonly Spell AnimateDead = new Spell(Spells.ID.ANIMATE_DEAD, SpellSchool.NECROMANCY, SpellLevel.THIRD, CastingTime.ONE_MINUTE, 10, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell BeaconOfHope = new Spell(Spells.ID.BEACON_OF_HOPE, SpellSchool.ABJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 30, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell BestowCurse = new Spell(Spells.ID.BESTOW_CURSE, SpellSchool.NECROMANCY, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Blink = new Spell(Spells.ID.BLINK, SpellSchool.TRANSMUTATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell CallLightning = new Spell(Spells.ID.CALL_LIGHTNING, SpellSchool.CONJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 120, VS, SpellDuration.TEN_MINUTES, 5, 0, doNothing);
        /* TODO */
        public static readonly Spell Clairvoyance = new Spell(Spells.ID.CLAIRVOYANCE, SpellSchool.DIVINATION, SpellLevel.THIRD, CastingTime.TEN_MINUTES, 5280, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ConjureAnimals = new Spell(Spells.ID.CONJURE_ANIMALS, SpellSchool.CONJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 60, VS, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Counterspell = new Spell(Spells.ID.COUNTERSPELL, SpellSchool.ABJURATION, SpellLevel.THIRD, CastingTime.REACTION, 60, S, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell CreateFoodandWater = new Spell(Spells.ID.CREATE_FOOD_AND_WATER, SpellSchool.CONJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 30, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Daylight = new Spell(Spells.ID.DAYLIGHT, SpellSchool.EVOCATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 60, VS, SpellDuration.ONE_HOUR, 60, 0, doNothing);
        /* TODO */
        public static readonly Spell DispelMagic = new Spell(Spells.ID.DISPEL_MAGIC, SpellSchool.ABJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 120, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Fear = new Spell(Spells.ID.FEAR, SpellSchool.ILLUSION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_MINUTE, 30, 0, doNothing);
        /* TODO */
        public static readonly Spell Fireball = new Spell(Spells.ID.FIREBALL, SpellSchool.EVOCATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 150, VSM, SpellDuration.INSTANTANEOUS, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell Fly = new Spell(Spells.ID.FLY, SpellSchool.TRANSMUTATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell GaseousForm = new Spell(Spells.ID.GASEOUS_FORM, SpellSchool.TRANSMUTATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell GlyphofWarding = new Spell(Spells.ID.GLYPH_OF_WARDING, SpellSchool.ABJURATION, SpellLevel.THIRD, CastingTime.ONE_HOUR, 0, VSM, SpellDuration.UNTIL_DISPELLED, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Haste = new Spell(Spells.ID.HASTE, SpellSchool.TRANSMUTATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell HypnoticPattern = new Spell(Spells.ID.HYPNOTIC_PATTERN, SpellSchool.ILLUSION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 120, SM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell LightningBolt = new Spell(Spells.ID.LIGHTNING_BOLT, SpellSchool.EVOCATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.INSTANTANEOUS, 100, 0, doNothing);
        /* TODO */
        public static readonly Spell MagicCircle = new Spell(Spells.ID.MAGIC_CIRCLE, SpellSchool.ABJURATION, SpellLevel.THIRD, CastingTime.ONE_MINUTE, 10, VSM, SpellDuration.ONE_HOUR, 10, 0, doNothing);
        /* TODO */
        public static readonly Spell MajorImage = new Spell(Spells.ID.MAJOR_IMAGE, SpellSchool.ILLUSION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 120, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell MassHealingWord = new Spell(Spells.ID.MASS_HEALING_WORD, SpellSchool.EVOCATION, SpellLevel.THIRD, CastingTime.BONUS_ACTION, 60, V, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell MeldIntoStone = new Spell(Spells.ID.MELD_INTO_STONE, SpellSchool.TRANSMUTATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VS, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Nondetection = new Spell(Spells.ID.NONDETECTION, SpellSchool.ABJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell PhantomSteed = new Spell(Spells.ID.PHANTOM_STEED, SpellSchool.ILLUSION, SpellLevel.THIRD, CastingTime.ONE_MINUTE, 30, VS, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell PlantGrowth = new Spell(Spells.ID.PLANT_GROWTH, SpellSchool.TRANSMUTATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 150, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ProtectionFromEnergy = new Spell(Spells.ID.PROTECTION_FROM_ENERGY, SpellSchool.ABJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell RemoveCurse = new Spell(Spells.ID.REMOVE_CURSE, SpellSchool.ABJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Revivify = new Spell(Spells.ID.REVIVIFY, SpellSchool.CONJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Sending = new Spell(Spells.ID.SENDING, SpellSchool.EVOCATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, -1, VSM, SpellDuration.ONE_ROUND, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell SleetStorm = new Spell(Spells.ID.SLEET_STORM, SpellSchool.CONJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 150, VSM, SpellDuration.ONE_MINUTE, 40, 0, doNothing);
        /* TODO */
        public static readonly Spell Slow = new Spell(Spells.ID.SLOW, SpellSchool.TRANSMUTATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 120, VSM, SpellDuration.ONE_MINUTE, 40, 0, doNothing);
        /* TODO */
        public static readonly Spell SpeakwithDead = new Spell(Spells.ID.SPEAK_WITH_DEAD, SpellSchool.NECROMANCY, SpellLevel.THIRD, CastingTime.ONE_ACTION, 10, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell SpeakwithPlants = new Spell(Spells.ID.SPEAK_WITH_PLANTS, SpellSchool.TRANSMUTATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VS, SpellDuration.TEN_MINUTES, 30, 0, doNothing);
        /* TODO */
        public static readonly Spell SpiritGuardians = new Spell(Spells.ID.SPIRIT_GUARDIANS, SpellSchool.CONJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell StinkingCloud = new Spell(Spells.ID.STINKING_CLOUD, SpellSchool.CONJURATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 90, VSM, SpellDuration.ONE_MINUTE, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell TinyHut = new Spell(Spells.ID.TINY_HUT, SpellSchool.EVOCATION, SpellLevel.THIRD, CastingTime.ONE_MINUTE, 0, VSM, SpellDuration.EIGHT_HOURS, 10, 0, doNothing);
        /* TODO */
        public static readonly Spell Tongues = new Spell(Spells.ID.TONGUES, SpellSchool.DIVINATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell VampiricTouch = new Spell(Spells.ID.VAMPIRIC_TOUCH, SpellSchool.NECROMANCY, SpellLevel.THIRD, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell WaterBreathing = new Spell(Spells.ID.WATER_BREATHING, SpellSchool.TRANSMUTATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_DAY, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell WaterWalk = new Spell(Spells.ID.WATER_WALK, SpellSchool.TRANSMUTATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell WindWall = new Spell(Spells.ID.WIND_WALL, SpellSchool.EVOCATION, SpellLevel.THIRD, CastingTime.ONE_ACTION, 120, VSM, SpellDuration.ONE_MINUTE, 50, 0, doNothing);
    }
}