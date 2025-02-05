namespace srd5 {
    public partial struct Spells {
        /* TODO */
        public static readonly Spell ArcaneEye = new Spell(Spells.ID.ARCANE_EYE, SpellSchool.DIVINATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_HOUR, 30, 0, doNothing);
        /* TODO */
        public static readonly Spell Banishment = new Spell(Spells.ID.BANISHMENT, SpellSchool.ABJURATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell BlackTentacles = new Spell(Spells.ID.BLACK_TENTACLES, SpellSchool.CONJURATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 90, VSM, SpellDuration.ONE_MINUTE, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell Blight = new Spell(Spells.ID.BLIGHT, SpellSchool.NECROMANCY, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 30, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Compulsion = new Spell(Spells.ID.COMPULSION, SpellSchool.ENCHANTMENT, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 30, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Confusion = new Spell(Spells.ID.CONFUSION, SpellSchool.ENCHANTMENT, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 90, VSM, SpellDuration.ONE_MINUTE, 10, 0, doNothing);
        /* TODO */
        public static readonly Spell ConjureMinorElementals = new Spell(Spells.ID.CONJURE_MINOR_ELEMENTALS, SpellSchool.CONJURATION, SpellLevel.FOURTH, CastingTime.ONE_MINUTE, 90, VS, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ConjureWoodlandBeings = new Spell(Spells.ID.CONJURE_WOODLAND_BEINGS, SpellSchool.CONJURATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ControlWater = new Spell(Spells.ID.CONTROL_WATER, SpellSchool.TRANSMUTATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 300, VSM, SpellDuration.TEN_MINUTES, 100, 0, doNothing);
        /* TODO */
        public static readonly Spell DeathWard = new Spell(Spells.ID.DEATH_WARD, SpellSchool.ABJURATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 0, VS, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell DimensionDoor = new Spell(Spells.ID.DIMENSION_DOOR, SpellSchool.CONJURATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 500, V, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Divination = new Spell(Spells.ID.DIVINATION, SpellSchool.DIVINATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell DominateBeast = new Spell(Spells.ID.DOMINATE_BEAST, SpellSchool.ENCHANTMENT, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 60, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Fabricate = new Spell(Spells.ID.FABRICATE, SpellSchool.TRANSMUTATION, SpellLevel.FOURTH, CastingTime.TEN_MINUTES, 120, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell FaithfulHound = new Spell(Spells.ID.FAITHFUL_HOUND, SpellSchool.CONJURATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell FireShield = new Spell(Spells.ID.FIRE_SHIELD, SpellSchool.EVOCATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.TEN_MINUTES, 5, 0, doNothing);
        /* TODO */
        public static readonly Spell FreedomOfMovement = new Spell(Spells.ID.FREEDOM_OF_MOVEMENT, SpellSchool.ABJURATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell GiantInsect = new Spell(Spells.ID.GIANT_INSECT, SpellSchool.TRANSMUTATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 30, VS, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell GreaterInvisibility = new Spell(Spells.ID.GREATER_INVISIBILITY, SpellSchool.ILLUSION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell GuardianOfFaith = new Spell(Spells.ID.GUARDIAN_OF_FAITH, SpellSchool.CONJURATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 30, V, SpellDuration.EIGHT_HOURS, 10, 0, doNothing);
        /* TODO */
        public static readonly Spell HallucinatoryTerrain = new Spell(Spells.ID.HALLUCINATORY_TERRAIN, SpellSchool.ILLUSION, SpellLevel.FOURTH, CastingTime.TEN_MINUTES, 300, VSM, SpellDuration.ONE_DAY, 150, 0, doNothing);
        /* TODO */
        public static readonly Spell IceStorm = new Spell(Spells.ID.ICE_STORM, SpellSchool.EVOCATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 300, VSM, SpellDuration.INSTANTANEOUS, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell LocateCreature = new Spell(Spells.ID.LOCATE_CREATURE, SpellSchool.DIVINATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell PhantasmalKiller = new Spell(Spells.ID.PHANTASMAL_KILLER, SpellSchool.ILLUSION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 120, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Polymorph = new Spell(Spells.ID.POLYMORPH, SpellSchool.TRANSMUTATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell PrivateSanctum = new Spell(Spells.ID.PRIVATE_SANCTUM, SpellSchool.ABJURATION, SpellLevel.FOURTH, CastingTime.TEN_MINUTES, 120, VSM, SpellDuration.ONE_DAY, 100, 0, doNothing);
        /* TODO */
        public static readonly Spell ResilientSphere = new Spell(Spells.ID.RESILIENT_SPHERE, SpellSchool.EVOCATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell SecretChest = new Spell(Spells.ID.SECRET_CHEST, SpellSchool.CONJURATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell StoneShape = new Spell(Spells.ID.STONE_SHAPE, SpellSchool.TRANSMUTATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Stoneskin = new Spell(Spells.ID.STONESKIN, SpellSchool.ABJURATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell WallOfFire = new Spell(Spells.ID.WALL_OF_FIRE, SpellSchool.EVOCATION, SpellLevel.FOURTH, CastingTime.ONE_ACTION, 120, VSM, SpellDuration.ONE_MINUTE, 60, 0, doNothing);
    }
}