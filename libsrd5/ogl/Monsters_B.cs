namespace srd5 {
    public partial struct Attacks {
        public static Attack BaboonBite {
            get {
                return new Attack("Bite", 1, new Damage(DamageType.PIERCING, "1d4-1"), 5);
            }
        }
        public static Attack BadgerBite {
            get {
                return new Attack("Bite", 2, new Damage(DamageType.PIERCING, 1), 5);
            }
        }
        public static Attack BalorLongsword {
            get {
                return new Attack("Longsword", 14, new Damage(DamageType.SLASHING, "3d8+8"), 10, new Damage(DamageType.LIGHTNING, "3d8")).WithProperties(Attack.Property.TRIPLE_DICE_ON_CRIT);
            }
        }
        public static AttackEffect BalorWhipEffect = delegate (Combattant attacker, Combattant target) {
            // TODO: THE TARGET MUST SUCCEED ON A DC 20 STRENGTH SAVING THROW OR BE PULLED UP TO 25 FEET TOWARD THE BALOR
            if (target.DC(BalorWhip, 20, AbilityType.STRENGTH)) return;
        };
        public static Attack BalorWhip {
            get {
                return new Attack("Whip", 14, new Damage(DamageType.SLASHING, "2d6+8"), 30, new Damage(DamageType.FIRE, "3d6"), BalorWhipEffect);
            }
        }
        public static Attack BanditScimitar {
            get {
                return new Attack("Scimitar", 3, new Damage(DamageType.SLASHING, "1d6+1"), 5);
            }
        }
        public static Attack BanditLightCrossbow {
            get {
                return new Attack("Light Crossbow", 3, new Damage(DamageType.PIERCING, "1d8+1"), 5, 80, 320);
            }
        }
        public static Attack BanditCaptainScimitar {
            get {
                return new Attack("Scimitar", 5, new Damage(DamageType.SLASHING, "1d6+3"), 5);
            }
        }
        public static Attack BanditCaptainDaggerMelee {
            get {
                return new Attack("Dagger", 5, new Damage(DamageType.PIERCING, "1d4+3"), 5);
            }
        }
        public static Attack BanditCaptainDaggerRanged {
            get {
                return new Attack("Dagger", 5, new Damage(DamageType.PIERCING, "1d4+3"), 5, 20, 60);
            }
        }
        public static Attack BarbedDevilClaw {
            get {
                return new Attack("Claw", 6, new Damage(DamageType.PIERCING, "1d6+3"), 5);
            }
        }
        public static Attack BarbedDevilHurlFlame {
            get {
                return new Attack("Hurl Flame", 5, new Damage(DamageType.FIRE, "3d6"), 150, 150);
            }
        }
        public static Attack BarbedDevilTail {
            get {
                return new Attack("Tail", 6, new Damage(DamageType.PIERCING, "2d6+3"), 5);
            }
        }
        public static Attack BasiliskBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "2d6+3"), 5, new Damage(DamageType.POISON, "2d6"));
            }
        }
        public static Attack BatBite {
            get {
                return new Attack("Bite", 2, new Damage(DamageType.PIERCING, 1), 5);
            }
        }
        public static AttackEffect BeardedDevilBeardEffect = delegate (Combattant attacker, Combattant target) {
            if (target.DC(BeardedDevilBeard, 12, AbilityType.CONSTITUTION)) return;
            if (target.IsImmune(DamageType.POISON)) return;
            target.AddEffect(Effect.BEARDED_DEVIL_POISON);
            int turn = 0;
            target.AddEndOfTurnEvent(delegate () {
                bool success = target.DC(BeardedDevilBeard, 12, AbilityType.CONSTITUTION);
                if (turn++ > 9 || success) {
                    target.RemoveEffect(Effect.BEARDED_DEVIL_POISON);
                }
                return success;
            });
        };
        public static Attack BeardedDevilBeard {
            get {
                return new Attack("Beard", 5, new Damage(DamageType.PIERCING, "1d8+2"), 5);
            }
        }
        public static readonly AttackEffect BeardedDevilGlaiveEffect = delegate (Combattant attacker, Combattant target) {
            // do not affect undead and constructs
            if (target is Monster monster) {
                if (monster.Type == Monsters.Type.UNDEAD || monster.Type == Monsters.Type.CONSTRUCT) return;
            }
            if (target.DC(BeardedDevilGlaive, 12, AbilityType.CONSTITUTION)) return;
            // add infernal wound affect if newly applied
            if (!target.HasEffect(Effect.INFERNAL_WOUND_BEARDED_DEVIL)) {
                target.AddStartOfTurnEvent(delegate () {
                    foreach (Effect effect in target.Effects) {
                        if (effect != Effect.INFERNAL_WOUND_BEARDED_DEVIL) continue;
                        target.TakeDamage(DamageType.TRUE_DAMAGE, "1d10");
                    }
                    return target.HasEffect(Effect.INFERNAL_WOUND_BEARDED_DEVIL);
                });
            }
            // increase infernal wound stack by one
            target.AddEffect(Effect.INFERNAL_WOUND_BEARDED_DEVIL);
        };
        public static Attack BeardedDevilGlaive {
            get {
                return new Attack("Glaive", 5, new Damage(DamageType.SLASHING, "1d10+3"), 10, null, BeardedDevilGlaiveEffect);
            }
        }
        public static Attack BehirBite {
            get {
                return new Attack("Bite", 10, new Damage(DamageType.PIERCING, "3d10+6"), 10);
            }
        }
        public static readonly AttackEffect BehirConstrictEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 16, Monsters.Behir.Size + 1);
        };
        public static Attack BehirConstrict {
            get {
                return new Attack("Constrict", 10, new Damage(DamageType.BLUDGEONING, "2d10+6"), 5, new Damage(DamageType.SLASHING, "2d10+6"), BehirConstrictEffect);
            }
        }
        public static Attack BerserkerGreataxe {
            get {
                return new Attack("Greataxe", 5, new Damage(DamageType.SLASHING, "1d12+3"), 5);
            }
        }
        public static Attack BlackBearBite {
            get {
                return new Attack("Bite", 3, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack BlackBearClaws {
            get {
                return new Attack("Claws", 3, new Damage(DamageType.SLASHING, "2d4+2"), 5);
            }
        }
        public static Attack BlackDragonWyrmlingBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d10+2"), 5, new Damage(DamageType.ACID, "1d4"));
            }
        }
        public static readonly AttackEffect BlackPuddingPseudopodEffect = delegate (Combattant attacker, Combattant target) {
            if (target is CharacterSheet sheet) {
                Armor armor = sheet.Inventory.Armor;
                // permanently reduce armor ac by 1 if armor isn't magical. armor is destroyed if reduced to 10 or below by this.
                if (armor == null || armor.HasProperty(ArmorProperty.MAGIC)) return;
                armor.AC--;
                if (armor.AC < 11) {
                    sheet.Unequip(armor);
                    armor.Destroy();
                }
            }
        };
        public static Attack BlackPuddingPseudopod {
            get {
                return new Attack("Pseudopod", 5, new Damage(DamageType.BLUDGEONING, "1d6+3"), 5, new Damage(DamageType.ACID, "4d8"), BlackPuddingPseudopodEffect);
            }
        }
        public static Attack BlinkDogBite {
            get {
                return new Attack("Bite", 3, new Damage(DamageType.PIERCING, "1d6+1"), 5);
            }
        }
        public static Attack BloodHawkBeak {
            get {
                return new Attack("Beak", 4, new Damage(DamageType.PIERCING, "1d4+2"), 5);
            }
        }
        public static Attack BlueDragonWyrmlingBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d10+3"), 5, new Damage(DamageType.LIGHTNING, "1d6"));
            }
        }
        public static Attack BoarTusk {
            get {
                return new Attack("Tusk", 3, new Damage(DamageType.SLASHING, "1d6+1"), 5);
            }
        }
        public static Attack BoneDevilClaw {
            get {
                return new Attack("Claw", 8, new Damage(DamageType.SLASHING, "1d8+4"), 10);
            }
        }
        public static readonly AttackEffect BoneDevilStingEffect = delegate (Combattant attacker, Combattant target) {
            if (target.DC(BoneDevilSting, 14, AbilityType.CONSTITUTION)) return;
            if (target.IsImmune(DamageType.POISON)) return;
            target.AddEffect(Effect.BONE_DEVIL_POISON);
            target.AddEndOfTurnEvent(delegate () {
                bool success = target.DC(BoneDevilSting, 14, AbilityType.CONSTITUTION);
                if (success) {
                    target.RemoveEffect(Effect.BONE_DEVIL_POISON);
                }
                return success;
            });
        };
        public static Attack BoneDevilSting {
            get {
                return new Attack("Sting", 8, new Damage(DamageType.PIERCING, "2d8+4"), 10, new Damage(DamageType.POISON, "5d6"), BoneDevilStingEffect);
            }
        }
        public static Attack BrassDragonWyrmlingBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d10+2"), 5);
            }
        }
        public static Attack BronzeDragonWyrmlingBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d10+3"), 5);
            }
        }
        public static Attack BrownBearBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d8+4"), 5);
            }
        }
        public static Attack BrownBearClaws {
            get {
                return new Attack("Claws", 5, new Damage(DamageType.SLASHING, "2d6+4"), 5);
            }
        }
        public static Attack BugbearMorningstar {
            get {
                return new Attack("Morningstar", 4, new Damage(DamageType.PIERCING, "2d8+2"), 5);
            }
        }
        public static Attack BugbearJavelinMelee {
            get {
                return new Attack("Javelin", 4, new Damage(DamageType.PIERCING, "2d6+2"), 5);
            }
        }
        public static Attack BugbearJavelinRanged {
            get {
                return new Attack("Javelin", 4, new Damage(DamageType.PIERCING, "2d6+2"), 5, 30, 120);
            }
        }
        public static Attack BuletteBite {
            get {
                return new Attack("Bite", 7, new Damage(DamageType.PIERCING, "4d12+4"), 5);
            }
        }
    }

    public partial struct Monsters {
        /* TODO */
        public static Monster Baboon {
            get {
                Monster baboon = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.BABOON, Alignment.UNALIGNED, 8, 14, 11, 4, 12, 6, 12, "1d6", 40, 0,
                    new Attack[] { Attacks.BaboonBite }, new Attack[] { }, Size.SMALL
                );
                baboon.AddFeat(Feat.PACK_TACTICS);
                return baboon;
            }
        }

        /* TODO */
        public static Monster Badger {
            get {
                Monster badger = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.BADGER, Alignment.UNALIGNED, 4, 11, 12, 2, 12, 5, 10, "1d4+1", 40, 0,
                    new Attack[] { Attacks.BadgerBite }, new Attack[] { }, Size.TINY
                );
                badger.AddFeat(Feat.KEEN_SMELL);
                return badger;
            }
        }

        /* TODO */
        public static Monster Balor {
            get {
                Monster balor = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.BALOR, Alignment.CHAOTIC_EVIL, 26, 15, 22, 20, 16, 22, 19, "21d12+126", 40, 19,
                    new Attack[] { Attacks.BalorLongsword, Attacks.BalorWhip }, new Attack[] { }, Size.HUGE
                );
                balor.AddProficiency(Proficiency.STRENGTH);
                balor.AddProficiency(Proficiency.CONSTITUTION);
                balor.AddProficiency(Proficiency.WISDOM);
                balor.AddProficiency(Proficiency.CHARISMA);
                balor.AddEffect(Effect.RESISTANCE_COLD);
                balor.AddEffect(Effect.RESISTANCE_LIGHTNING);
                balor.AddEffect(Effect.RESISTANCE_NONMAGIC);
                balor.AddEffect(Effect.IMMUNITY_FIRE);
                balor.AddEffect(Effect.IMMUNITY_POISON);
                balor.AddEffect(Effect.IMMUNITY_POISONED);
                balor.AddFeat(Feat.DEATH_THROES);
                balor.AddFeat(Feat.FIRE_AURA);
                balor.AddFeat(Feat.MAGIC_RESISTANCE);
                balor.AddFeat(Feat.MAGIC_WEAPONS);
                return balor;
            }
        }

        /* TODO */
        public static Monster Bandit {
            get {
                Monster bandit = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.BANDIT, Alignment.UNALIGNED, 11, 12, 12, 10, 10, 10, 12, "2d8+2", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.BanditScimitar }, new Attack[] { Attacks.BanditLightCrossbow }, Size.MEDIUM
                );
                return bandit;
            }
        }

        /* TODO */
        public static Monster BanditCaptain {
            get {
                Monster banditCaptain = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.BANDIT_CAPTAIN, Alignment.UNALIGNED, 15, 16, 14, 14, 11, 14, 15, "10d8+20", 40, 2,
                    new Attack[] { Attacks.BanditCaptainScimitar, Attacks.BanditCaptainDaggerMelee }, new Attack[] { Attacks.BanditCaptainDaggerRanged }, Size.MEDIUM
                );
                banditCaptain.AddProficiency(Proficiency.STRENGTH);
                banditCaptain.AddProficiency(Proficiency.DEXTERITY);
                banditCaptain.AddProficiency(Proficiency.WISDOM);
                banditCaptain.AddProficiency(Proficiency.ATHLETICS);
                banditCaptain.AddProficiency(Proficiency.DECEPTION);
                return banditCaptain;
            }
        }

        /* TODO */
        public static Monster BarbedDevil {
            get {
                Monster barbedDevil = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.BARBED_DEVIL, Alignment.LAWFUL_EVIL, 16, 17, 18, 12, 14, 14, 15, "13d8+52", 40, 5,
                    new Attack[] { Attacks.BarbedDevilClaw, Attacks.BarbedDevilTail }, new Attack[] { Attacks.BarbedDevilHurlFlame }, Size.MEDIUM
                );
                barbedDevil.AddProficiency(Proficiency.STRENGTH);
                barbedDevil.AddProficiency(Proficiency.CONSTITUTION);
                barbedDevil.AddProficiency(Proficiency.WISDOM);
                barbedDevil.AddProficiency(Proficiency.CHARISMA);
                barbedDevil.AddProficiency(Proficiency.DECEPTION);
                barbedDevil.AddProficiency(Proficiency.INSIGHT);
                barbedDevil.AddProficiency(Proficiency.PERCEPTION);
                barbedDevil.AddEffect(Effect.RESISTANCE_COLD);
                barbedDevil.AddEffect(Effect.RESISTANCE_NONMAGIC);
                barbedDevil.AddEffect(Effect.IMMUNITY_FIRE);
                barbedDevil.AddEffect(Effect.IMMUNITY_POISON);
                barbedDevil.AddEffect(Effect.IMMUNITY_POISONED);
                barbedDevil.AddFeat(Feat.BARBED_HIDE);
                barbedDevil.AddFeat(Feat.DEVILS_SIGHT);
                barbedDevil.AddFeat(Feat.MAGIC_RESISTANCE);
                return barbedDevil;
            }
        }

        /* TODO */
        public static Monster Basilisk {
            get {
                Monster basilisk = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.BASILISK, Alignment.UNALIGNED, 16, 8, 15, 2, 8, 7, 12, "8d8+16", 40, 3,
                    new Attack[] { Attacks.BasiliskBite }, new Attack[] { }, Size.MEDIUM
                );
                basilisk.AddFeat(Feat.PETRIFYING_GAZE_BASILISK);
                return basilisk;
            }
        }

        /* TODO */
        public static Monster Bat {
            get {
                Monster bat = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.BAT, Alignment.UNALIGNED, 2, 15, 8, 2, 12, 4, 12, "1d4-1", 40, 0,
                    new Attack[] { Attacks.BatBite }, new Attack[] { }, Size.TINY
                );
                bat.AddFeat(Feat.ECHOLOCATION);
                bat.AddFeat(Feat.KEEN_HEARING);
                return bat;
            }
        }

        /* TODO */
        public static Monster BeardedDevil {
            get {
                Monster beardedDevil = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.BEARDED_DEVIL, Alignment.LAWFUL_EVIL, 16, 15, 15, 9, 11, 11, 13, "8d8+16", 40, 3,
                    new Attack[] { Attacks.BeardedDevilBeard, Attacks.BeardedDevilGlaive }, new Attack[] { }, Size.MEDIUM
                );
                beardedDevil.AddProficiency(Proficiency.STRENGTH);
                beardedDevil.AddProficiency(Proficiency.CONSTITUTION);
                beardedDevil.AddProficiency(Proficiency.WISDOM);
                beardedDevil.AddEffect(Effect.RESISTANCE_COLD);
                beardedDevil.AddEffect(Effect.RESISTANCE_NONMAGIC);
                beardedDevil.AddEffect(Effect.IMMUNITY_FIRE);
                beardedDevil.AddEffect(Effect.IMMUNITY_POISON);
                beardedDevil.AddEffect(Effect.IMMUNITY_POISONED);
                beardedDevil.AddFeat(Feat.DEVILS_SIGHT);
                beardedDevil.AddFeat(Feat.MAGIC_RESISTANCE);
                beardedDevil.AddFeat(Feat.STEADFAST);
                return beardedDevil;
            }
        }

        /* TODO */
        public static Monster Behir {
            get {
                Monster behir = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.BEHIR, Alignment.NEUTRAL_EVIL, 23, 16, 18, 7, 14, 12, 17, "16d12+64", 40, 11,
                    new Attack[] { Attacks.BehirBite, Attacks.BehirConstrict }, new Attack[] { }, Size.HUGE
                );
                behir.AddProficiency(Proficiency.PERCEPTION);
                behir.AddProficiency(Proficiency.STEALTH);
                behir.AddEffect(Effect.IMMUNITY_LIGHTNING);
                return behir;
            }
        }

        /* TODO */
        public static Monster Berserker {
            get {
                Monster berserker = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.BERSERKER, Alignment.UNALIGNED, 16, 12, 17, 9, 11, 9, 13, "9d8+27", 40, 2,
                    new Attack[] { Attacks.BerserkerGreataxe }, new Attack[] { }, Size.MEDIUM
                );
                berserker.AddFeat(Feat.RECKLESS);
                return berserker;
            }
        }

        /* TODO */
        public static Monster BlackBear {
            get {
                Monster blackBear = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.BLACK_BEAR, Alignment.UNALIGNED, 15, 10, 14, 2, 12, 7, 11, "3d8+6", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.BlackBearBite, Attacks.BlackBearClaws }, new Attack[] { }, Size.MEDIUM
                );
                blackBear.AddFeat(Feat.KEEN_SMELL);
                return blackBear;
            }
        }

        /* TODO */
        public static Monster BlackDragonWyrmling {
            get {
                Monster blackDragonWyrmling = new Monster(
                    Monsters.Type.DRAGON, Monsters.ID.BLACK_DRAGON_WYRMLING, Alignment.CHAOTIC_EVIL, 15, 14, 13, 10, 11, 13, 17, "6d8+6", 40, 2,
                    new Attack[] { Attacks.BlackDragonWyrmlingBite }, new Attack[] { }, Size.MEDIUM
                );
                blackDragonWyrmling.AddProficiency(Proficiency.DEXTERITY);
                blackDragonWyrmling.AddProficiency(Proficiency.CONSTITUTION);
                blackDragonWyrmling.AddProficiency(Proficiency.WISDOM);
                blackDragonWyrmling.AddProficiency(Proficiency.CHARISMA);
                blackDragonWyrmling.AddProficiency(Proficiency.PERCEPTION);
                blackDragonWyrmling.AddProficiency(Proficiency.STEALTH);
                blackDragonWyrmling.AddEffect(Effect.IMMUNITY_ACID);
                blackDragonWyrmling.AddFeat(Feat.AMPHIBIOUS);
                return blackDragonWyrmling;
            }
        }

        /* TODO */
        public static Monster BlackPudding {
            get {
                Monster blackPudding = new Monster(
                    Monsters.Type.OOZE, Monsters.ID.BLACK_PUDDING, Alignment.UNALIGNED, 16, 5, 16, 1, 6, 1, 7, "10d10+30", 40, 4,
                    new Attack[] { Attacks.BlackPuddingPseudopod }, new Attack[] { }, Size.LARGE
                );
                blackPudding.AddEffect(Effect.IMMUNITY_ACID);
                blackPudding.AddEffect(Effect.IMMUNITY_COLD);
                blackPudding.AddEffect(Effect.IMMUNITY_LIGHTNING);
                blackPudding.AddEffect(Effect.IMMUNITY_SLASHING);
                blackPudding.AddEffect(Effect.IMMUNITY_BLINDED);
                blackPudding.AddEffect(Effect.IMMUNITY_CHARMED);
                blackPudding.AddEffect(Effect.IMMUNITY_BLINDED);
                blackPudding.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                blackPudding.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                blackPudding.AddEffect(Effect.IMMUNITY_PRONE);
                blackPudding.AddFeat(Feat.AMORPHOUS);
                blackPudding.AddFeat(Feat.CORROSIVE_FORM);
                blackPudding.AddFeat(Feat.SPIDER_CLIMB);
                return blackPudding;
            }
        }

        /* TODO */
        public static Monster BlinkDog {
            get {
                Monster blinkDog = new Monster(
                    Monsters.Type.FEY, Monsters.ID.BLINK_DOG, Alignment.LAWFUL_GOOD, 12, 17, 12, 10, 13, 11, 13, "4d8+4", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.BlinkDogBite }, new Attack[] { }, Size.MEDIUM
                );
                blinkDog.AddProficiency(Proficiency.PERCEPTION);
                blinkDog.AddProficiency(Proficiency.STEALTH);
                blinkDog.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                return blinkDog;
            }
        }

        /* TODO */
        public static Monster BloodHawk {
            get {
                Monster bloodHawk = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.BLOOD_HAWK, Alignment.UNALIGNED, 6, 14, 10, 3, 14, 5, 12, "2d6", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.BloodHawkBeak }, new Attack[] { }, Size.SMALL
                );
                bloodHawk.AddProficiency(Proficiency.PERCEPTION);
                bloodHawk.AddFeat(Feat.KEEN_SIGHT);
                bloodHawk.AddFeat(Feat.PACK_TACTICS);
                return bloodHawk;
            }
        }

        /* TODO */
        public static Monster BlueDragonWyrmling {
            get {
                Monster blueDragonWyrmling = new Monster(
                    Monsters.Type.DRAGON, Monsters.ID.BLUE_DRAGON_WYRMLING, Alignment.LAWFUL_EVIL, 17, 10, 15, 12, 11, 15, 17, "8d8+16", 40, 3,
                    new Attack[] { Attacks.BlueDragonWyrmlingBite }, new Attack[] { }, Size.MEDIUM
                );
                blueDragonWyrmling.AddProficiency(Proficiency.DEXTERITY);
                blueDragonWyrmling.AddProficiency(Proficiency.CONSTITUTION);
                blueDragonWyrmling.AddProficiency(Proficiency.WISDOM);
                blueDragonWyrmling.AddProficiency(Proficiency.CHARISMA);
                blueDragonWyrmling.AddProficiency(Proficiency.PERCEPTION);
                blueDragonWyrmling.AddProficiency(Proficiency.STEALTH);
                blueDragonWyrmling.AddEffect(Effect.IMMUNITY_LIGHTNING);
                return blueDragonWyrmling;
            }
        }

        /* TODO */
        public static Monster Boar {
            get {
                Monster boar = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.BOAR, Alignment.UNALIGNED, 13, 11, 12, 2, 9, 5, 11, "2d8+2", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.BoarTusk }, new Attack[] { }, Size.MEDIUM
                );
                boar.AddFeat(Feat.CHARGE_BOAR);
                boar.AddFeat(Feat.RELENTLESS_7);
                return boar;
            }
        }

        /* TODO */
        public static Monster BoneDevil {
            get {
                Monster boneDevil = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.BONE_DEVIL, Alignment.LAWFUL_EVIL, 18, 16, 18, 13, 14, 16, 19, "15d10+60", 40, 9,
                    new Attack[] { Attacks.BoneDevilClaw, Attacks.BoneDevilSting }, new Attack[] { }, Size.LARGE
                );
                boneDevil.AddProficiency(Proficiency.INTELLIGENCE);
                boneDevil.AddProficiency(Proficiency.WISDOM);
                boneDevil.AddProficiency(Proficiency.CHARISMA);
                boneDevil.AddProficiency(Proficiency.DECEPTION);
                boneDevil.AddProficiency(Proficiency.INSIGHT);
                boneDevil.AddEffect(Effect.RESISTANCE_COLD);
                boneDevil.AddEffect(Effect.RESISTANCE_NONMAGIC);
                boneDevil.AddEffect(Effect.IMMUNITY_FIRE);
                boneDevil.AddEffect(Effect.IMMUNITY_POISON);
                boneDevil.AddEffect(Effect.IMMUNITY_POISONED);
                boneDevil.AddFeat(Feat.DEVILS_SIGHT);
                boneDevil.AddFeat(Feat.MAGIC_RESISTANCE);
                return boneDevil;
            }
        }

        /* TODO */
        public static Monster BrassDragonWyrmling {
            get {
                Monster brassDragonWyrmling = new Monster(
                    Monsters.Type.DRAGON, Monsters.ID.BRASS_DRAGON_WYRMLING, Alignment.CHAOTIC_GOOD, 15, 10, 13, 10, 11, 13, 16, "3d8+3", 40, 1,
                    new Attack[] { Attacks.BrassDragonWyrmlingBite }, new Attack[] { }, Size.MEDIUM
                );
                brassDragonWyrmling.AddProficiency(Proficiency.DEXTERITY);
                brassDragonWyrmling.AddProficiency(Proficiency.CONSTITUTION);
                brassDragonWyrmling.AddProficiency(Proficiency.WISDOM);
                brassDragonWyrmling.AddProficiency(Proficiency.CHARISMA);
                brassDragonWyrmling.AddProficiency(Proficiency.PERCEPTION);
                brassDragonWyrmling.AddProficiency(Proficiency.STEALTH);
                brassDragonWyrmling.AddEffect(Effect.IMMUNITY_FIRE);
                return brassDragonWyrmling;
            }
        }

        /* TODO */
        public static Monster BronzeDragonWyrmling {
            get {
                Monster bronzeDragonWyrmling = new Monster(
                    Monsters.Type.DRAGON, Monsters.ID.BRONZE_DRAGON_WYRMLING, Alignment.LAWFUL_GOOD, 17, 10, 15, 12, 11, 15, 17, "5d8+10", 40, 2,
                    new Attack[] { Attacks.BronzeDragonWyrmlingBite }, new Attack[] { }, Size.MEDIUM
                );
                bronzeDragonWyrmling.AddProficiency(Proficiency.DEXTERITY);
                bronzeDragonWyrmling.AddProficiency(Proficiency.CONSTITUTION);
                bronzeDragonWyrmling.AddProficiency(Proficiency.WISDOM);
                bronzeDragonWyrmling.AddProficiency(Proficiency.CHARISMA);
                bronzeDragonWyrmling.AddProficiency(Proficiency.PERCEPTION);
                bronzeDragonWyrmling.AddProficiency(Proficiency.STEALTH);
                bronzeDragonWyrmling.AddEffect(Effect.IMMUNITY_LIGHTNING);
                bronzeDragonWyrmling.AddFeat(Feat.AMPHIBIOUS);
                return bronzeDragonWyrmling;
            }
        }

        /* TODO */
        public static Monster BrownBear {
            get {
                Monster brownBear = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.BROWN_BEAR, Alignment.UNALIGNED, 19, 10, 16, 2, 13, 7, 11, "4d10+12", 40, 1,
                    new Attack[] { Attacks.BrownBearBite, Attacks.BrownBearClaws }, new Attack[] { }, Size.LARGE
                );
                brownBear.AddProficiency(Proficiency.PERCEPTION);
                brownBear.AddFeat(Feat.KEEN_SMELL);
                return brownBear;
            }
        }

        /* TODO */
        public static Monster Bugbear {
            get {
                Monster bugbear = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.BUGBEAR, Alignment.CHAOTIC_EVIL, 15, 14, 13, 8, 11, 9, 16, "5d8+5", 40, 1,
                    new Attack[] { Attacks.BugbearMorningstar, Attacks.BugbearJavelinMelee }, new Attack[] { Attacks.BugbearJavelinRanged }, Size.MEDIUM
                );
                bugbear.AddProficiency(Proficiency.STEALTH);
                bugbear.AddProficiency(Proficiency.SURVIVAL);
                bugbear.AddFeat(Feat.BRUTE);
                bugbear.AddFeat(Feat.SURPRISE_ATTACK_BUGBEAR);
                return bugbear;
            }
        }

        /* TODO */
        public static Monster Bulette {
            get {
                Monster bulette = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.BULETTE, Alignment.UNALIGNED, 19, 11, 21, 2, 10, 5, 17, "9d10+45", 40, 5,
                    new Attack[] { Attacks.BuletteBite }, new Attack[] { }, Size.LARGE
                );
                bulette.AddProficiency(Proficiency.PERCEPTION);
                bulette.AddFeat(Feat.STANDING_LEAP_BULETTE);
                return bulette;
            }
        }
    }
}