namespace srd5 {
    public partial struct Spells {
        /* TODO */
        public static readonly Spell AstralProjection = new Spell(Spells.ID.ASTRAL_PROJECTION, SpellSchool.NECROMANCY, SpellLevel.NINETH, CastingTime.ONE_HOUR, 10, VSM, SpellDuration.SPECIAL, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Foresight = new Spell(Spells.ID.FORESIGHT, SpellSchool.DIVINATION, SpellLevel.NINETH, CastingTime.ONE_MINUTE, 0, VSM, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Gate = new Spell(Spells.ID.GATE, SpellSchool.CONJURATION, SpellLevel.NINETH, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Imprisonment = new Spell(Spells.ID.IMPRISONMENT, SpellSchool.ABJURATION, SpellLevel.NINETH, CastingTime.ONE_MINUTE, 30, VSM, SpellDuration.UNTIL_DISPELLED, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell MassHeal = new Spell(Spells.ID.MASS_HEAL, SpellSchool.CONJURATION, SpellLevel.NINETH, CastingTime.ONE_ACTION, 60, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell MeteorSwarm = new Spell(Spells.ID.METEOR_SWARM, SpellSchool.EVOCATION, SpellLevel.NINETH, CastingTime.ONE_ACTION, 5280, VS, SpellDuration.INSTANTANEOUS, 40, 0, doNothing);
        /* TODO */
        public static readonly Spell PowerWordKill = new Spell(Spells.ID.POWER_WORD_KILL, SpellSchool.ENCHANTMENT, SpellLevel.NINETH, CastingTime.ONE_ACTION, 60, V, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell PrismaticWall = new Spell(Spells.ID.PRISMATIC_WALL, SpellSchool.ABJURATION, SpellLevel.NINETH, CastingTime.ONE_ACTION, 60, VS, SpellDuration.TEN_MINUTES, 90, 0, doNothing);
        /* TODO */
        public static readonly Spell Shapechange = new Spell(Spells.ID.SHAPECHANGE, SpellSchool.TRANSMUTATION, SpellLevel.NINETH, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell StormOfVengeance = new Spell(Spells.ID.STORM_OF_VENGEANCE, SpellSchool.CONJURATION, SpellLevel.NINETH, CastingTime.ONE_ACTION, 1000, VS, SpellDuration.ONE_MINUTE, 360, 0, doNothing);
        /* TODO */
        public static readonly Spell TimeStop = new Spell(Spells.ID.TIME_STOP, SpellSchool.TRANSMUTATION, SpellLevel.NINETH, CastingTime.ONE_ACTION, 0, V, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell TruePolymorph = new Spell(Spells.ID.TRUE_POLYMORPH, SpellSchool.TRANSMUTATION, SpellLevel.NINETH, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell TrueResurrection = new Spell(Spells.ID.TRUE_RESURRECTION, SpellSchool.NECROMANCY, SpellLevel.NINETH, CastingTime.ONE_HOUR, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Weird = new Spell(Spells.ID.WEIRD, SpellSchool.ILLUSION, SpellLevel.NINETH, CastingTime.ONE_ACTION, 120, VS, SpellDuration.ONE_MINUTE, 30, 0, doNothing);
        /* TODO */
        public static readonly Spell Wish = new Spell(Spells.ID.WISH, SpellSchool.CONJURATION, SpellLevel.NINETH, CastingTime.ONE_ACTION, 0, V, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
    }
}