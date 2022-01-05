namespace srd5 {
    public enum Size {
        TINY,
        SMALL,
        MEDIUM,
        LARGE,
        HUGE,
        GARGANTUAN
    }

    public enum MonsterType {
        ABERRATION,
        BEAST,
        CELESTIAL,
        CONSTRUCT,
        DRAGON,
        ELEMENTAL,
        FEY,
        FIEND,
        GIANT,
        HUMANOID,
        MONSTROSITY,
        OOZE,
        PLANT,
        UNDEAD
    }

    public struct Attacks {
        public static readonly Attack[] None = new Attack[0];
        public static readonly Attack BoarTusk = new Attack("Tusk", 3, new Damage(DamageType.SLASHING, "1d6+1"));
        public static readonly Attack ClayGolemSlam = new Attack("Slam", 8, new Damage(DamageType.BLUDGEONING, "2d10+5"), 5);
        public static readonly Attack GiantBadgerBite = new Attack("Bite", 3, new Damage(DamageType.PIERCING, "1d6+1"));
        public static readonly Attack GiantBadgerClaws = new Attack("Claws", 3, new Damage(DamageType.SLASHING, "2d4+1"));
        public static readonly Attack GoblinScimitar = new Attack("Scimitar", 4, new Damage(DamageType.SLASHING, "1d6+2"));
        public static readonly Attack GoblinShortbow = new Attack("Shortbow", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5, 80, 320);
        public static readonly Attack NightHagClaws = new Attack("Claws", 7, new Damage(DamageType.SLASHING, "2d8+4"));
        public static readonly Attack OgreGreatclub = new Attack("Greatclub", 6, new Damage(DamageType.BLUDGEONING, "2d8+4"));
        public static readonly Attack OgreJavelin = new Attack("Javelin", 6, new Damage(DamageType.PIERCING, "2d6+4"), 5, 30, 120);
        public static readonly Attack OrcGreataxe = new Attack("Greataxe", 5, new Damage(DamageType.SLASHING, "1d12+3"));
        public static readonly Attack OrcJavelin = new Attack("Javelin", 5, new Damage(DamageType.PIERCING, "1d6+3"), 5, 30, 120);
        public static readonly Attack ShadowStrengthDrain = new Attack("Strength Drain", 4, new Damage(DamageType.NECROTIC, "2d6+2"));

    }

    public struct Monsters {
        public static Monster Boar {
            get {
                return new Monster(
                    MonsterType.BEAST, "Boar", 13, 11, 12, 2, 9, 5, 11, "2d8+2", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.BoarTusk }, Attacks.None, Size.MEDIUM
                );
            }
        }

        public static Monster GiantBadger {
            get {
                return new Monster(
                    MonsterType.BEAST, "Giant Badger", 13, 10, 15, 2, 12, 5, 10, "2d8+4", 30, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GiantBadgerBite, Attacks.GiantBadgerClaws }, Attacks.None, Size.MEDIUM
                );
            }
        }

        public static Monster Goblin {
            get {
                return new Monster(
                    MonsterType.HUMANOID, "Goblin", 8, 14, 10, 10, 8, 8, 15, "2d6", 30, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GoblinScimitar }, new Attack[] { Attacks.GoblinShortbow }, Size.SMALL
                );
            }
        }

        public static Monster Ogre {
            get {
                return new Monster(
                    MonsterType.GIANT, "Ogre", 19, 8, 16, 5, 7, 7, 11, "7d10+21", 40, 2,
                    new Attack[] { Attacks.OgreGreatclub }, new Attack[] { Attacks.OgreJavelin }, Size.LARGE
                );
            }
        }

        public static Monster NightHag {
            get {
                Monster hag = new Monster(
                    MonsterType.FIEND, "Night Hag", 18, 15, 16, 16, 14, 16, 17, "15d8+45", 30, 5,
                    new Attack[] { Attacks.NightHagClaws }, Attacks.None, Size.MEDIUM, 14
                );
                AvailableSpells spells = new AvailableSpells(AbilityType.CHARISMA);
                spells.AddKnownSpell(Spells.MagicMissile, Spells.DetectMagic);
                spells.SlotsCurrent[1] = 999;
                hag.AddAvailableSpells(spells);
                return hag;
            }
        }

        public static Monster Orc {
            get {
                Monster orc = new Monster(
                    MonsterType.HUMANOID, "Orc", 16, 12, 16, 7, 11, 10, 13, "2d8+6", 30, ChallengeRating.HALF,
                    new Attack[] { Attacks.OrcGreataxe }, new Attack[] { Attacks.OrcJavelin }, Size.MEDIUM, 0
                );
                return orc;
            }
        }

        public static Monster ClayGolem {
            get {
                Monster golem = new Monster(
                    MonsterType.CONSTRUCT, "Clay Golem", 20, 9, 18, 3, 8, 1, 14, "14d10+56", 20, 9,
                    new Attack[] { Attacks.ClayGolemSlam, Attacks.ClayGolemSlam }, Attacks.None, Size.LARGE, 0
                );
                golem.AddEffects(Effect.IMMUNITY_ACID, Effect.IMMUNITY_POISON, Effect.IMMUNITY_PSYCHIC, Effect.IMMUNITY_NONMAGIC);
                golem.AddEffects(Effect.IMMUNITY_CHARMED, Effect.IMMUNITY_EXHAUSTION, Effect.IMMUNITY_FRIGHTENED, Effect.IMMUNITY_PARALYZED, Effect.IMMUNITY_PETRIFIED, Effect.IMMUNITY_POISONED);
                return golem;
            }
        }

        public static Monster Shadow {
            get {
                Monster shadow = new Monster(
                    MonsterType.UNDEAD, "Shadow", 6, 14, 13, 6, 10, 8, 12, "3d8+3", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.ShadowStrengthDrain }, Attacks.None, Size.MEDIUM, 0
                );
                shadow.AddEffects(Effect.VULNERABILITY_RADIANT);
                shadow.AddEffects(Effect.RESISTANCE_ACID, Effect.RESISTANCE_COLD, Effect.RESISTANCE_LIGHTNING, Effect.RESISTANCE_THUNDER, Effect.RESISTANCE_NONMAGIC);
                shadow.AddEffects(Effect.IMMUNITY_NECROTIC, Effect.IMMUNITY_POISON);
                shadow.AddEffects(Effect.IMMUNITY_EXHAUSTION, Effect.IMMUNITY_FRIGHTENED, Effect.IMMUNITY_GRAPPLED, Effect.IMMUNITY_PARALYZED, Effect.IMMUNITY_PETRIFIED, Effect.IMMUNITY_POISONED, Effect.IMMUNITY_PRONE, Effect.IMMUNITY_RESTRAINED);
                return shadow;
            }
        }
    }
}