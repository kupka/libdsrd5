namespace srd5 {
    public partial struct Attacks {
        public static readonly AttackEffect VampireUnarmedStrikeEffect = delegate (Combattant attacker, Combattant target) {
            // TODO: Instead of dealing damage, the vampire can grapple the target (escape DC 18).
        };
        public static Attack VampireUnarmedStrike {
            get {
                return new Attack("Unarmed Strike", 9, new Damage(DamageType.BLUDGEONING, "1d8+4"), 5, null, VampireUnarmedStrikeEffect);
            }
        }
        public static readonly AttackEffect VampireBiteEffect = delegate (Combattant attacker, Combattant target) {
            int damage = target.TakeDamage(attacker, DamageType.NECROTIC, "3d6");
            target.AddHitPointMaximumModifiers(new HitPointMaxiumModifier(-damage, HitPointMaxiumModifier.RemovedByEffect.LONG_REST));
            attacker.HealDamage(damage);
            // TODO:  The target dies if this effect reduces its hit point maximum to 0. 
            // A humanoid slain in this way and then buried in the ground rises the following night as a vampire spawn under the vampire's control.
        };
        public static Attack VampireBite {
            get {
                return new Attack("Bite", 9, new Damage(DamageType.PIERCING, "1d6+4"), 5, null, VampireBiteEffect);
            }
        }
        public static Attack VampireBatFormBite {
            get {
                return new Attack("Bite", 9, new Damage(DamageType.PIERCING, "1d6+4"), 5, null, VampireBiteEffect);
            }
        }
        public static readonly AttackEffect VampireSpawnBiteEffect = delegate (Combattant attacker, Combattant target) {
            int damage = target.TakeDamage(attacker, DamageType.NECROTIC, "2d6");
            target.AddHitPointMaximumModifiers(new HitPointMaxiumModifier(-damage, HitPointMaxiumModifier.RemovedByEffect.LONG_REST));
            attacker.HealDamage(damage);
        };
        public static Attack VampireSpawnBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "1d6+6"), 5, null, VampireSpawnBiteEffect);
            }
        }
        public static readonly AttackEffect VampireSpawnClawsEffect = delegate (Combattant attacker, Combattant target) {
            // TODO: Instead of dealing damage, the vampire can grapple the target (escape DC 13).
        };
        public static Attack VampireSpawnClaws {
            get {
                return new Attack("Claws", 6, new Damage(DamageType.SLASHING, "2d4+3"), 5, null, VampireSpawnClawsEffect);
            }
        }
        public static Attack VeteranLongsword {
            get {
                return new Attack("Longsword", 5, new Damage(DamageType.SLASHING, "1d10+3"), 5);
            }
        }
        public static Attack VeteranShortsword {
            get {
                return new Attack("Shortsword", 5, new Damage(DamageType.PIERCING, "1d6+3"), 5);
            }
        }
        public static Attack VeteranHeavyCrossbow {
            get {
                return new Attack("Heavy Crossbow", 3, new Damage(DamageType.PIERCING, "1d10+1"), 5, 100, 400);
            }
        }
        public static Attack VioletFungusRottingTouch {
            get {
                return new Attack("Rotting Touch", 2, new Damage(DamageType.NECROTIC, "1d8"), 10);
            }
        }
        public static Attack VrockBeak {
            get {
                return new Attack("Beak", 6, new Damage(DamageType.PIERCING, "2d6+3"), 5);
            }
        }
        public static Attack VrockTalons {
            get {
                return new Attack("Talons", 6, new Damage(DamageType.SLASHING, "2d10+3"), 5);
            }
        }
        public static Attack VultureBeak {
            get {
                return new Attack("Beak", 2, new Damage(DamageType.PIERCING, "1d4"), 5);
            }
        }

    }

    public partial struct Monsters {

        /* TODO */
        public static Monster Vampire {
            get {
                Monster vampire = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.VAMPIRE_VAMPIRE_FORM, Alignment.LAWFUL_EVIL, 18, 18, 18, 17, 15, 18, 16, "17d8+68", 40, 13,
                    new Attack[] { Attacks.VampireUnarmedStrike, Attacks.VampireBite }, new Attack[] { }, Size.MEDIUM
                );
                vampire.AddProficiency(Proficiency.DEXTERITY);
                vampire.AddProficiency(Proficiency.WISDOM);
                vampire.AddProficiency(Proficiency.CHARISMA);
                vampire.AddProficiency(Proficiency.PERCEPTION);
                vampire.AddProficiency(Proficiency.STEALTH);
                vampire.AddEffect(Effect.RESISTANCE_NECROTIC);
                vampire.AddEffect(Effect.RESISTANCE_NONMAGIC);
                vampire.AddFeat(Feat.SHAPECHANGER_VAMPIRE);
                vampire.AddFeat(Feat.LEGENDARY_RESISTANCE);
                vampire.AddFeat(Feat.MISTY_ESCAPE);
                vampire.AddFeat(Feat.REGENERATION);
                vampire.AddFeat(Feat.SPIDER_CLIMB);
                vampire.AddFeat(Feat.VAMPIRE_WEAKNESSES);
                return vampire;
            }
        }

        /* TODO */
        public static Monster VampireBatForm {
            get {
                Monster vampireBatForm = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.VAMPIRE_BAT_FORM, Alignment.LAWFUL_EVIL, 18, 18, 18, 17, 15, 18, 16, "17d8+68", 40, 13,
                    new Attack[] { Attacks.VampireBatFormBite }, new Attack[] { }, Size.MEDIUM
                );
                vampireBatForm.AddProficiency(Proficiency.DEXTERITY);
                vampireBatForm.AddProficiency(Proficiency.WISDOM);
                vampireBatForm.AddProficiency(Proficiency.CHARISMA);
                vampireBatForm.AddProficiency(Proficiency.PERCEPTION);
                vampireBatForm.AddProficiency(Proficiency.STEALTH);
                vampireBatForm.AddEffect(Effect.RESISTANCE_NECROTIC);
                vampireBatForm.AddEffect(Effect.RESISTANCE_NONMAGIC);
                vampireBatForm.AddFeat(Feat.SHAPECHANGER_VAMPIRE);
                vampireBatForm.AddFeat(Feat.LEGENDARY_RESISTANCE);
                vampireBatForm.AddFeat(Feat.MISTY_ESCAPE);
                vampireBatForm.AddFeat(Feat.REGENERATION);
                vampireBatForm.AddFeat(Feat.SPIDER_CLIMB);
                vampireBatForm.AddFeat(Feat.VAMPIRE_WEAKNESSES);
                return vampireBatForm;
            }
        }

        /* TODO */
        public static Monster VampireMistForm {
            get {
                Monster vampireMistForm = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.VAMPIRE_MIST_FORM, Alignment.LAWFUL_EVIL, 18, 18, 18, 17, 15, 18, 16, "17d8+68", 40, 13,
                    new Attack[] { }, new Attack[] { }, Size.MEDIUM
                );
                vampireMistForm.AddProficiency(Proficiency.DEXTERITY);
                vampireMistForm.AddProficiency(Proficiency.WISDOM);
                vampireMistForm.AddProficiency(Proficiency.CHARISMA);
                vampireMistForm.AddProficiency(Proficiency.PERCEPTION);
                vampireMistForm.AddProficiency(Proficiency.STEALTH);
                vampireMistForm.AddEffect(Effect.RESISTANCE_NECROTIC);
                vampireMistForm.AddEffect(Effect.RESISTANCE_NONMAGIC);
                vampireMistForm.AddFeat(Feat.SHAPECHANGER_VAMPIRE);
                vampireMistForm.AddFeat(Feat.LEGENDARY_RESISTANCE);
                vampireMistForm.AddFeat(Feat.MISTY_ESCAPE);
                vampireMistForm.AddFeat(Feat.REGENERATION);
                vampireMistForm.AddFeat(Feat.SPIDER_CLIMB);
                vampireMistForm.AddFeat(Feat.VAMPIRE_WEAKNESSES);
                return vampireMistForm;
            }
        }

        /* TODO */
        public static Monster VampireSpawn {
            get {
                Monster vampireSpawn = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.VAMPIRE_SPAWN, Alignment.NEUTRAL_EVIL, 16, 16, 16, 11, 10, 12, 15, "11d8+33", 40, 5,
                    new Attack[] { Attacks.VampireSpawnBite, Attacks.VampireSpawnClaws }, new Attack[] { }, Size.MEDIUM
                );
                vampireSpawn.AddProficiency(Proficiency.DEXTERITY);
                vampireSpawn.AddProficiency(Proficiency.WISDOM);
                vampireSpawn.AddProficiency(Proficiency.PERCEPTION);
                vampireSpawn.AddProficiency(Proficiency.STEALTH);
                vampireSpawn.AddEffect(Effect.RESISTANCE_NECROTIC);
                vampireSpawn.AddEffect(Effect.RESISTANCE_NONMAGIC);
                vampireSpawn.AddFeat(Feat.REGENERATION);
                vampireSpawn.AddFeat(Feat.SPIDER_CLIMB);
                vampireSpawn.AddFeat(Feat.VAMPIRE_WEAKNESSES);
                return vampireSpawn;
            }
        }

        /* TODO */
        public static Monster Veteran {
            get {
                Monster veteran = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.VETERAN, Alignment.UNALIGNED, 16, 13, 14, 10, 11, 10, 17, "9d8+18", 40, 3,
                    new Attack[] { Attacks.VeteranLongsword, Attacks.VeteranShortsword }, new Attack[] { Attacks.VeteranHeavyCrossbow }, Size.MEDIUM
                );
                veteran.AddProficiency(Proficiency.ATHLETICS);
                veteran.AddProficiency(Proficiency.PERCEPTION);
                return veteran;
            }
        }

        /* TODO */
        public static Monster VioletFungus {
            get {
                Monster violetFungus = new Monster(
                    Monsters.Type.PLANT, Monsters.ID.VIOLET_FUNGUS, Alignment.UNALIGNED, 3, 1, 10, 1, 3, 1, 5, "4d8", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.VioletFungusRottingTouch }, new Attack[] { }, Size.MEDIUM
                );
                violetFungus.AddEffect(Effect.IMMUNITY_BLINDED);
                violetFungus.AddEffect(Effect.IMMUNITY_BLINDED);
                violetFungus.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                violetFungus.AddFeat(Feat.FALSE_APPEARANCE);
                return violetFungus;
            }
        }

        /* TODO */
        public static Monster Vrock {
            get {
                Monster vrock = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.VROCK, Alignment.CHAOTIC_EVIL, 17, 15, 18, 8, 13, 8, 15, "11d10+44", 40, 6,
                    new Attack[] { Attacks.VrockBeak, Attacks.VrockTalons }, new Attack[] { }, Size.LARGE
                );
                vrock.AddProficiency(Proficiency.DEXTERITY);
                vrock.AddProficiency(Proficiency.WISDOM);
                vrock.AddProficiency(Proficiency.CHARISMA);
                vrock.AddEffect(Effect.RESISTANCE_COLD);
                vrock.AddEffect(Effect.RESISTANCE_FIRE);
                vrock.AddEffect(Effect.RESISTANCE_LIGHTNING);
                vrock.AddEffect(Effect.RESISTANCE_NONMAGIC);
                vrock.AddEffect(Effect.IMMUNITY_POISON);
                vrock.AddEffect(Effect.IMMUNITY_POISONED);
                vrock.AddFeat(Feat.MAGIC_RESISTANCE);
                return vrock;
            }
        }

        /* TODO */
        public static Monster Vulture {
            get {
                Monster vulture = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.VULTURE, Alignment.UNALIGNED, 7, 10, 13, 2, 12, 4, 10, "1d8+1", 40, 0,
                    new Attack[] { Attacks.VultureBeak }, new Attack[] { }, Size.MEDIUM
                );
                vulture.AddProficiency(Proficiency.PERCEPTION);
                vulture.AddFeat(Feat.KEEN_SIGHT_AND_SMELL);
                vulture.AddFeat(Feat.PACK_TACTICS);
                return vulture;
            }
        }


    }
}