namespace srd5 {
    public partial struct Attacks {
        public static Attack KillerWhaleBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "5d6+4"), 5);
            }
        }
        public static Attack KnightGreatsword {
            get {
                return new Attack("Greatsword", 5, new Damage(DamageType.SLASHING, "2d6+3"), 5);
            }
        }
        public static Attack KnightHeavyCrossbow {
            get {
                return new Attack("Heavy Crossbow", 2, new Damage(DamageType.PIERCING, "1d10"), 5, 100, 400);
            }
        }
        public static Attack KoboldDagger {
            get {
                return new Attack("Dagger", 4, new Damage(DamageType.PIERCING, "1d4+2"), 5);
            }
        }
        public static Attack KoboldSling {
            get {
                return new Attack("Sling", 4, new Damage(DamageType.BLUDGEONING, "1d4+2"), 5, 30, 120);
            }
        }
        public static readonly AttackEffect KrakenBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (!target.HasCondition(ConditionType.GRAPPLED_DC18)) return;
            if (target.Size > Size.LARGE) return;
            // If the target is a Large or smaller creature grappled by the kraken, that creature is swallowed, and the grapple ends.
            attacker.RemoveEffect(Effect.GRAPPLING);
            target.RemoveCondition(ConditionType.GRAPPLED_DC18);
            target.AddEffect(Effect.KRAKEN_SWALLOW);
            // While swallowed, the creature is blinded and restrained
            target.AddConditions(ConditionType.BLINDED, ConditionType.RESTRAINED);
            // TODO: it has total cover against attacks and other effects outside the kraken
            // and it takes 42 (12d6) acid damage at the start of each of the kraken's turns
            attacker.AddStartOfTurnEvent(delegate () {
                if (!(target.HasEffect(Effect.KRAKEN_SWALLOW))) return true;
                target.TakeDamage(attacker, DamageType.ACID, "12d6");
                return false;
            });
            // TODO: if the kraken takes 50 damage or more on a single turn from a creature inside it, 
            // the kraken must succeed on a DC 25 Constitution saving throw at the end of that turn or 
            // regurgitate all swallowed creatures, which fall prone in a space within 10 feet of the kraken. 
            // If the kraken dies, a swallowed creature is no longer restrained by it and can escape from 
            // the corpse using 15 feet of movement, exiting prone.
        };
        public static Attack KrakenBite {
            get {
                return new Attack("Bite", 7, new Damage(DamageType.PIERCING, "3d8+10"), 5, null, KrakenBiteEffect);
            }
        }

        public static readonly AttackEffect KrakenTentacleEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 18, Size.GARGANTUAN, true, null, 10);
        };
        public static Attack KrakenTentacle {
            get {
                return new Attack("Tentacle", 7, new Damage(DamageType.BLUDGEONING, "3d6+10"), 30, null, KrakenTentacleEffect);
            }
        }
    }

    public partial struct Monsters {
        /* TODO */
        public static Monster KillerWhale {
            get {
                Monster killerWhale = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.KILLER_WHALE, Alignment.UNALIGNED, 19, 10, 13, 3, 12, 7, 12, "12d12+12", 40, 3,
                    new Attack[] { Attacks.KillerWhaleBite }, new Attack[] { }, Size.HUGE
                );
                killerWhale.AddProficiency(Proficiency.PERCEPTION);
                killerWhale.AddFeat(Feat.ECHOLOCATION);
                killerWhale.AddFeat(Feat.HOLD_BREATH_30MIN);
                killerWhale.AddFeat(Feat.KEEN_HEARING);
                return killerWhale;
            }
        }

        /* TODO */
        public static Monster Knight {
            get {
                Monster knight = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.KNIGHT, Alignment.UNALIGNED, 16, 11, 14, 11, 11, 15, 18, "8d8+16", 40, 3,
                    new Attack[] { Attacks.KnightGreatsword }, new Attack[] { Attacks.KnightHeavyCrossbow }, Size.MEDIUM
                );
                knight.AddProficiency(Proficiency.CONSTITUTION);
                knight.AddProficiency(Proficiency.WISDOM);
                knight.AddFeat(Feat.BRAVE);
                return knight;
            }
        }

        /* TODO */
        public static Monster Kobold {
            get {
                Monster kobold = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.KOBOLD, Alignment.LAWFUL_EVIL, 7, 15, 9, 8, 7, 8, 12, "2d6-2", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.KoboldDagger }, new Attack[] { Attacks.KoboldSling }, Size.SMALL
                );
                kobold.AddFeat(Feat.SUNLIGHT_SENSITIVITY);
                kobold.AddFeat(Feat.PACK_TACTICS);
                return kobold;
            }
        }

        /* TODO */
        public static Monster Kraken {
            get {
                Monster kraken = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.KRAKEN, Alignment.CHAOTIC_EVIL, 30, 11, 25, 22, 18, 20, 18, "27d20+189", 40, 23,
                    new Attack[] { Attacks.KrakenBite, Attacks.KrakenTentacle }, new Attack[] { }, Size.GARGANTUAN
                );
                kraken.AddProficiency(Proficiency.STRENGTH);
                kraken.AddProficiency(Proficiency.DEXTERITY);
                kraken.AddProficiency(Proficiency.CONSTITUTION);
                kraken.AddProficiency(Proficiency.INTELLIGENCE);
                kraken.AddProficiency(Proficiency.WISDOM);
                kraken.AddEffect(Effect.IMMUNITY_LIGHTNING);
                kraken.AddEffect(Effect.IMMUNITY_NONMAGIC);
                kraken.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                kraken.AddEffect(Effect.IMMUNITY_PARALYZED);
                kraken.AddFeat(Feat.AMPHIBIOUS);
                kraken.AddFeat(Feat.FREEDOM_OF_MOVEMENT);
                kraken.AddFeat(Feat.SIEGE_MONSTER);
                return kraken;
            }
        }
    }
}