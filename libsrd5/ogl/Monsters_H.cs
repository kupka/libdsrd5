namespace srd5 {
    public partial struct Attacks {
        public static readonly AttackEffect HalfRedDragonVeteranLongswordEffect = delegate (Combattant attacker, Combattant target) {
        };
        public static Attack HalfRedDragonVeteranLongsword {
            get {
                return new Attack("Longsword", 5, new Damage(DamageType.SLASHING, "1d10+3"), 5);
            }
        }
        public static Attack HalfRedDragonVeteranShortsword {
            get {
                return new Attack("Shortsword", 5, new Damage(DamageType.PIERCING, "1d6+3"), 5);
            }
        }
        public static Attack HalfRedDragonVeteranHeavyCrossbow {
            get {
                return new Attack("Heavy Crossbow", 3, new Damage(DamageType.PIERCING, "1d10+1"), 5, 100, 400);
            }
        }
        public static Attack HarpyClaws {
            get {
                return new Attack("Claws", 3, new Damage(DamageType.SLASHING, "2d4+1"), 5);
            }
        }
        public static Attack HarpyClub {
            get {
                return new Attack("Club", 3, new Damage(DamageType.BLUDGEONING, "1d4+1"), 5);
            }
        }
        public static Attack HawkTalons {
            get {
                return new Attack("Talons", 5, new Damage(DamageType.SLASHING, 1), 5);
            }
        }
        public static Attack HellHoundBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d8+3"), 5, new Damage(DamageType.FIRE, "2d6"));
            }
        }
        public static Attack HezrouBite {
            get {
                return new Attack("Bite", 7, new Damage(DamageType.PIERCING, "2d10+4"), 5);
            }
        }
        public static Attack HezrouClaws {
            get {
                return new Attack("Claws", 7, new Damage(DamageType.SLASHING, "2d6+4"), 5);
            }
        }
        public static Attack HillGiantGreatclub {
            get {
                return new Attack("Greatclub", 8, new Damage(DamageType.BLUDGEONING, "3d8+5"), 10);
            }
        }
        public static Attack HillGiantRock {
            get {
                return new Attack("Rock", 8, new Damage(DamageType.BLUDGEONING, "3d10+5"), 5, 60, 240);
            }
        }
        public static Attack HippogriffBeak {
            get {
                return new Attack("Beak", 5, new Damage(DamageType.PIERCING, "1d10+3"), 5);
            }
        }
        public static Attack HippogriffClaws {
            get {
                return new Attack("Claws", 5, new Damage(DamageType.SLASHING, "2d6+3"), 5);
            }
        }
        public static Attack HobgoblinLongsword {
            get {
                return new Attack("Longsword", 3, new Damage(DamageType.SLASHING, "1d10+1"), 5);
            }
        }
        public static Attack HobgoblinLongbow {
            get {
                return new Attack("Longbow", 3, new Damage(DamageType.PIERCING, "1d8+1"), 5, 150, 600);
            }
        }
        public static readonly AttackEffect HomunculusBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target.IsImmune(DamageType.POISON)) return;
            if (target.DC(HomunculusBite, 10, AbilityType.CONSTITUTION, out int dc)) return;
            if (dc < 6) {
                target.AddEffect(Effect.HOMUNCULUS_POISON_UNCONCIOUSNESS);
                int duration = Die.D10.Value * 10;
                int turnsPassed = 0;
                target.AddEndOfTurnEvent(delegate () {
                    if (++turnsPassed > duration) {
                        target.RemoveEffect(Effect.HOMUNCULUS_POISON_UNCONCIOUSNESS);
                        return true;
                    }
                    return false;
                });
            } else {
                target.AddEffect(Effect.HOMUNCULUS_POISON);
                int turnsPassed = 0;
                target.AddEndOfTurnEvent(delegate () {
                    if (++turnsPassed > 10) {
                        target.RemoveEffect(Effect.HOMUNCULUS_POISON);
                        return true;
                    }
                    return false;
                });
            }
        };
        public static Attack HomunculusBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, 1), 5, null, HomunculusBiteEffect);
            }
        }
        public static readonly AttackEffect HornedDevilTailEffect = delegate (Combattant attacker, Combattant target) {
            // do not affect undead and constructs
            if (target is Monster monster && (monster.Type == Monsters.Type.UNDEAD || monster.Type == Monsters.Type.CONSTRUCT)) return;
            if (target.DC(BeardedDevilGlaive, 17, AbilityType.CONSTITUTION)) return;
            // TODO: Any creature can take an action to stanch the wound with a successful DC 12 Wisdom (Medicine) check.
            // The wound also closes if the target receives magical healing.
            // add infernal wound affect if newly applied
            if (!target.HasEffect(Effect.INFERNAL_WOUND_HORNED_DEVIL)) {
                target.AddStartOfTurnEvent(delegate () {
                    foreach (Effect effect in target.Effects) {
                        if (effect != Effect.INFERNAL_WOUND_HORNED_DEVIL) continue;
                        target.TakeDamage(attacker, DamageType.TRUE_DAMAGE, "3d6");
                    }
                    return target.HasEffect(Effect.INFERNAL_WOUND_HORNED_DEVIL);
                });
            }
            // increase infernal wound stack by one
            target.AddEffect(Effect.INFERNAL_WOUND_HORNED_DEVIL);
        };
        public static Attack HornedDevilTail {
            get {
                return new Attack("Tail", 10, new Damage(DamageType.PIERCING, "1d8+6"), 5, null, HornedDevilTailEffect);
            }
        }
        public static readonly AttackEffect HornedDevilHurlFlameEffect = delegate (Combattant attacker, Combattant target) {
            // TODO: If the target is a flammable object that isn't being worn or carried, it also catches fire.
        };
        public static Attack HornedDevilHurlFlame {
            get {
                return new Attack("Hurl Flame", 7, new Damage(DamageType.FIRE, "4d6"), 150, 150, null, HornedDevilHurlFlameEffect);
            }
        }
        public static Attack HornedDevilFork {
            get {
                return new Attack("Fork", 10, new Damage(DamageType.PIERCING, "2d8+6"), 10);
            }
        }
        public static Attack HunterSharkBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "2d8+4"), 5);
            }
        }
        public static Attack HydraBite {
            get {
                return new Attack("Bite", 8, new Damage(DamageType.PIERCING, "1d10+5"), 10);
            }
        }
        public static Attack HyenaBite {
            get {
                return new Attack("Bite", 2, new Damage(DamageType.PIERCING, "1d6"), 5);
            }
        }
    }

    public partial struct Monsters {
        /* TODO */
        public static Monster HalfRedDragonVeteran {
            get {
                Monster halfRedDragonVeteran = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.HALF_RED_DRAGON_VETERAN, Alignment.UNALIGNED, 16, 13, 14, 10, 11, 10, 18, "10d8+20", 40, 5,
                    new Attack[] { Attacks.HalfRedDragonVeteranLongsword, Attacks.HalfRedDragonVeteranShortsword }, new Attack[] { Attacks.HalfRedDragonVeteranHeavyCrossbow }, Size.MEDIUM
                );
                halfRedDragonVeteran.AddEffect(Effect.RESISTANCE_FIRE);
                return halfRedDragonVeteran;
            }
        }

        /* TODO */
        public static Monster Harpy {
            get {
                Monster harpy = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.HARPY, Alignment.CHAOTIC_EVIL, 12, 13, 12, 7, 10, 13, 11, "7d8+7", 40, 1,
                    new Attack[] { Attacks.HarpyClaws, Attacks.HarpyClub }, new Attack[] { }, Size.MEDIUM
                );
                return harpy;
            }
        }

        /* TODO */
        public static Monster Hawk {
            get {
                Monster hawk = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.HAWK, Alignment.UNALIGNED, 5, 16, 8, 2, 14, 6, 13, "1d4-1", 40, 0,
                    new Attack[] { Attacks.HawkTalons }, new Attack[] { }, Size.TINY
                );
                hawk.AddProficiency(Proficiency.PERCEPTION);
                hawk.AddFeat(Feat.KEEN_SIGHT);
                return hawk;
            }
        }

        /* TODO */
        public static Monster HellHound {
            get {
                Monster hellHound = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.HELL_HOUND, Alignment.LAWFUL_EVIL, 17, 12, 14, 6, 13, 6, 15, "7d8+14", 40, 3,
                    new Attack[] { Attacks.HellHoundBite }, new Attack[] { }, Size.MEDIUM
                );
                hellHound.AddProficiency(Proficiency.PERCEPTION);
                hellHound.AddEffect(Effect.IMMUNITY_FIRE);
                hellHound.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                hellHound.AddFeat(Feat.PACK_TACTICS);
                return hellHound;
            }
        }

        /* TODO */
        public static Monster Hezrou {
            get {
                Monster hezrou = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.HEZROU, Alignment.CHAOTIC_EVIL, 19, 17, 20, 5, 12, 13, 16, "13d10+65", 40, 8,
                    new Attack[] { Attacks.HezrouBite, Attacks.HezrouClaws }, new Attack[] { }, Size.LARGE
                );
                hezrou.AddProficiency(Proficiency.STRENGTH);
                hezrou.AddProficiency(Proficiency.CONSTITUTION);
                hezrou.AddProficiency(Proficiency.WISDOM);
                hezrou.AddEffect(Effect.RESISTANCE_COLD);
                hezrou.AddEffect(Effect.RESISTANCE_FIRE);
                hezrou.AddEffect(Effect.RESISTANCE_LIGHTNING);
                hezrou.AddEffect(Effect.RESISTANCE_NONMAGIC);
                hezrou.AddEffect(Effect.IMMUNITY_POISON);
                hezrou.AddEffect(Effect.IMMUNITY_POISONED);
                hezrou.AddFeat(Feat.MAGIC_RESISTANCE);
                hezrou.AddFeat(Feat.STENCH_HEZROU);
                return hezrou;
            }
        }

        /* TODO */
        public static Monster HillGiant {
            get {
                Monster hillGiant = new Monster(
                    Monsters.Type.GIANT, Monsters.ID.HILL_GIANT, Alignment.CHAOTIC_EVIL, 21, 8, 19, 5, 9, 6, 13, "10d12+40", 40, 5,
                    new Attack[] { Attacks.HillGiantGreatclub }, new Attack[] { Attacks.HillGiantRock }, Size.HUGE
                );
                hillGiant.AddProficiency(Proficiency.PERCEPTION);
                return hillGiant;
            }
        }

        /* TODO */
        public static Monster Hippogriff {
            get {
                Monster hippogriff = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.HIPPOGRIFF, Alignment.UNALIGNED, 17, 13, 13, 2, 12, 8, 11, "3d10+3", 40, 1,
                    new Attack[] { Attacks.HippogriffBeak, Attacks.HippogriffClaws }, new Attack[] { }, Size.LARGE
                );
                hippogriff.AddProficiency(Proficiency.PERCEPTION);
                hippogriff.AddFeat(Feat.KEEN_SIGHT);
                return hippogriff;
            }
        }

        /* TODO */
        public static Monster Hobgoblin {
            get {
                Monster hobgoblin = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.HOBGOBLIN, Alignment.LAWFUL_EVIL, 13, 12, 12, 10, 10, 9, 18, "2d8+2", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.HobgoblinLongsword }, new Attack[] { Attacks.HobgoblinLongbow }, Size.MEDIUM
                );
                hobgoblin.AddFeat(Feat.MARTIAL_ADVANTAGE);
                return hobgoblin;
            }
        }

        /* TODO */
        public static Monster Homunculus {
            get {
                Monster homunculus = new Monster(
                    Monsters.Type.CONSTRUCT, Monsters.ID.HOMUNCULUS, Alignment.NEUTRAL, 4, 15, 11, 10, 10, 7, 13, "2d4", 40, 0,
                    new Attack[] { }, new Attack[] { }, Size.TINY
                );
                homunculus.AddEffect(Effect.IMMUNITY_POISON);
                homunculus.AddEffect(Effect.IMMUNITY_CHARMED);
                homunculus.AddEffect(Effect.IMMUNITY_POISONED);
                homunculus.AddFeat(Feat.TELEPATHIC_BOND_HOMUNCULUS);
                return homunculus;
            }
        }

        /* TODO */
        public static Monster HornedDevil {
            get {
                Monster hornedDevil = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.HORNED_DEVIL, Alignment.LAWFUL_EVIL, 22, 17, 21, 12, 16, 17, 18, "17d10+85", 40, 11,
                    new Attack[] { Attacks.HornedDevilFork, Attacks.HornedDevilTail }, new Attack[] { Attacks.HornedDevilHurlFlame }, Size.LARGE
                );
                hornedDevil.AddProficiency(Proficiency.STRENGTH);
                hornedDevil.AddProficiency(Proficiency.DEXTERITY);
                hornedDevil.AddProficiency(Proficiency.WISDOM);
                hornedDevil.AddProficiency(Proficiency.CHARISMA);
                hornedDevil.AddEffect(Effect.RESISTANCE_COLD);
                hornedDevil.AddEffect(Effect.RESISTANCE_NONMAGIC);
                hornedDevil.AddEffect(Effect.IMMUNITY_FIRE);
                hornedDevil.AddEffect(Effect.IMMUNITY_POISON);
                hornedDevil.AddEffect(Effect.IMMUNITY_POISONED);
                hornedDevil.AddFeat(Feat.DEVILS_SIGHT);
                hornedDevil.AddFeat(Feat.MAGIC_RESISTANCE);
                return hornedDevil;
            }
        }

        /* TODO */
        public static Monster HunterShark {
            get {
                Monster hunterShark = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.HUNTER_SHARK, Alignment.UNALIGNED, 18, 13, 15, 1, 10, 4, 12, "6d10+12", 40, 2,
                    new Attack[] { Attacks.HunterSharkBite }, new Attack[] { }, Size.LARGE
                );
                hunterShark.AddProficiency(Proficiency.PERCEPTION);
                hunterShark.AddFeat(Feat.BLOOD_FRENZY);
                hunterShark.AddFeat(Feat.WATER_BREATHING);
                return hunterShark;
            }
        }

        /* TODO */
        public static Monster Hydra {
            get {
                Monster hydra = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.HYDRA, Alignment.UNALIGNED, 20, 12, 20, 2, 10, 7, 15, "15d12+75", 40, 8,
                    new Attack[] { Attacks.HydraBite }, new Attack[] { }, Size.HUGE
                );
                hydra.AddProficiency(Proficiency.PERCEPTION);
                hydra.AddFeat(Feat.HOLD_BREATH_1HOUR);
                hydra.AddFeat(Feat.MULTIPLE_HEADS);
                hydra.AddFeat(Feat.REACTIVE_HEADS);
                hydra.AddFeat(Feat.WAKEFUL);
                return hydra;
            }
        }

        /* TODO */
        public static Monster Hyena {
            get {
                Monster hyena = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.HYENA, Alignment.UNALIGNED, 11, 13, 12, 2, 12, 5, 11, "1d8+1", 40, 0,
                    new Attack[] { Attacks.HyenaBite }, new Attack[] { }, Size.MEDIUM
                );
                hyena.AddProficiency(Proficiency.PERCEPTION);
                hyena.AddFeat(Feat.PACK_TACTICS);
                return hyena;
            }
        }
    }
}