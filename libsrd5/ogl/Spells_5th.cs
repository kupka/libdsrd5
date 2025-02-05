namespace srd5 {
    public partial struct Spells {
        /* TODO */
        public static readonly Spell AnimateObjects = new Spell(Spells.ID.ANIMATE_OBJECTS, SpellSchool.TRANSMUTATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 120, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell AntilifeShell = new Spell(Spells.ID.ANTILIFE_SHELL, SpellSchool.ABJURATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_HOUR, 10, 0, doNothing);
        /* TODO */
        public static readonly Spell ArcaneHand = new Spell(Spells.ID.ARCANE_HAND, SpellSchool.EVOCATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 120, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Awaken = new Spell(Spells.ID.AWAKEN, SpellSchool.TRANSMUTATION, SpellLevel.FIFTH, CastingTime.EIGHT_HOURS, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Cloudkill = new Spell(Spells.ID.CLOUDKILL, SpellSchool.CONJURATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 120, VS, SpellDuration.TEN_MINUTES, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell Commune = new Spell(Spells.ID.COMMUNE, SpellSchool.DIVINATION, SpellLevel.FIFTH, CastingTime.ONE_MINUTE, 0, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell CommuneWithNature = new Spell(Spells.ID.COMMUNE_WITH_NATURE, SpellSchool.DIVINATION, SpellLevel.FIFTH, CastingTime.ONE_MINUTE, 0, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ConeOfCold = new Spell(Spells.ID.CONE_OF_COLD, SpellSchool.EVOCATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.INSTANTANEOUS, 60, 0, doNothing);
        /* TODO */
        public static readonly Spell ConjureElemental = new Spell(Spells.ID.CONJURE_ELEMENTAL, SpellSchool.CONJURATION, SpellLevel.FIFTH, CastingTime.ONE_MINUTE, 90, VSM, SpellDuration.ONE_HOUR, 10, 0, doNothing);
        /* TODO */
        public static readonly Spell ContactOtherPlane = new Spell(Spells.ID.CONTACT_OTHER_PLANE, SpellSchool.DIVINATION, SpellLevel.FIFTH, CastingTime.ONE_MINUTE, 0, V, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Contagion = new Spell(Spells.ID.CONTAGION, SpellSchool.NECROMANCY, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 0, VS, SpellDuration.SEVEN_DAYS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Creation = new Spell(Spells.ID.CREATION, SpellSchool.ILLUSION, SpellLevel.FIFTH, CastingTime.ONE_MINUTE, 30, VSM, SpellDuration.SPECIAL, 5, 0, doNothing);
        /* TODO */
        public static readonly Spell DispelEvilandGood = new Spell(Spells.ID.DISPEL_EVIL_AND_GOOD, SpellSchool.ABJURATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell DominatePerson = new Spell(Spells.ID.DOMINATE_PERSON, SpellSchool.ENCHANTMENT, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 60, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Dream = new Spell(Spells.ID.DREAM, SpellSchool.ILLUSION, SpellLevel.FIFTH, CastingTime.ONE_MINUTE, -1, VSM, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell FlameStrike = new Spell(Spells.ID.FLAME_STRIKE, SpellSchool.EVOCATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.INSTANTANEOUS, 40, 0, doNothing);
        /* TODO */
        public static readonly Spell Geas = new Spell(Spells.ID.GEAS, SpellSchool.ENCHANTMENT, SpellLevel.FIFTH, CastingTime.ONE_MINUTE, 60, V, SpellDuration.THIRTY_DAYS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell GreaterRestoration = new Spell(Spells.ID.GREATER_RESTORATION, SpellSchool.ABJURATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Hallow = new Spell(Spells.ID.HALLOW, SpellSchool.EVOCATION, SpellLevel.FIFTH, CastingTime.ONE_DAY, 0, VSM, SpellDuration.UNTIL_DISPELLED, 60, 0, doNothing);
        /* TODO */
        public static readonly Spell HoldMonster = new Spell(Spells.ID.HOLD_MONSTER, SpellSchool.ENCHANTMENT, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 90, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell InsectPlague = new Spell(Spells.ID.INSECT_PLAGUE, SpellSchool.CONJURATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 300, VSM, SpellDuration.TEN_MINUTES, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell LegendLore = new Spell(Spells.ID.LEGEND_LORE, SpellSchool.DIVINATION, SpellLevel.FIFTH, CastingTime.TEN_MINUTES, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell MassCureWounds = new Spell(Spells.ID.MASS_CURE_WOUNDS, SpellSchool.CONJURATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 60, VS, SpellDuration.INSTANTANEOUS, 30, 0, doNothing);
        /* TODO */
        public static readonly Spell Mislead = new Spell(Spells.ID.MISLEAD, SpellSchool.ILLUSION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 0, S, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ModifyMemory = new Spell(Spells.ID.MODIFY_MEMORY, SpellSchool.ENCHANTMENT, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 30, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Passwall = new Spell(Spells.ID.PASSWALL, SpellSchool.TRANSMUTATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell PlanarBinding = new Spell(Spells.ID.PLANAR_BINDING, SpellSchool.ABJURATION, SpellLevel.FIFTH, CastingTime.ONE_HOUR, 60, VSM, SpellDuration.ONE_DAY, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell RaiseDead = new Spell(Spells.ID.RAISE_DEAD, SpellSchool.NECROMANCY, SpellLevel.FIFTH, CastingTime.ONE_HOUR, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Reincarnate = new Spell(Spells.ID.REINCARNATE, SpellSchool.TRANSMUTATION, SpellLevel.FIFTH, CastingTime.ONE_HOUR, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Scrying = new Spell(Spells.ID.SCRYING, SpellSchool.DIVINATION, SpellLevel.FIFTH, CastingTime.TEN_MINUTES, 0, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Seeming = new Spell(Spells.ID.SEEMING, SpellSchool.ILLUSION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 30, VS, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Telekinesis = new Spell(Spells.ID.TELEKINESIS, SpellSchool.TRANSMUTATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 60, VS, SpellDuration.TEN_MINUTES, 30, 0, doNothing);
        /* TODO */
        public static readonly Spell TelepathicBond = new Spell(Spells.ID.TELEPATHIC_BOND, SpellSchool.DIVINATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell TeleportationCircle = new Spell(Spells.ID.TELEPORTATION_CIRCLE, SpellSchool.CONJURATION, SpellLevel.FIFTH, CastingTime.ONE_MINUTE, 10, VM, SpellDuration.ONE_ROUND, 10, 0, doNothing);
        /* TODO */
        public static readonly Spell TreeStride = new Spell(Spells.ID.TREE_STRIDE, SpellSchool.CONJURATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell WallOfForce = new Spell(Spells.ID.WALL_OF_FORCE, SpellSchool.EVOCATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 120, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell WallOfStone = new Spell(Spells.ID.WALL_OF_STONE, SpellSchool.EVOCATION, SpellLevel.FIFTH, CastingTime.ONE_ACTION, 120, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
    }
}