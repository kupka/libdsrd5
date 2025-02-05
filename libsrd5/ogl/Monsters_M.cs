namespace srd5 {
    public partial struct Attacks {
        public static Attack MageDaggerMelee {
            get {
                return new Attack("Dagger", 5, new Damage(DamageType.PIERCING, "1d4+2"), 5);
            }
        }
        public static Attack MageDaggerRanged {
            get {
                return new Attack("Dagger", 5, new Damage(DamageType.PIERCING, "1d4+2"), 20, 60);
            }
        }
        public static Attack MagmaMephitClaws {
            get {
                return new Attack("Claws", 3, new Damage(DamageType.SLASHING, "1d4+1"), 5, new Damage(DamageType.FIRE, "1d4"));
            }
        }
        public static readonly AttackEffect MagminTouchEffect = delegate (Combattant attacker, Combattant target) {
            if (target.IsImmune(DamageType.FIRE)) return;
            if (target.HasEffect(Effect.MAGMIN_IGNITE)) return;
            target.AddEffect(Effect.MAGMIN_IGNITE);
            // TODO: Add means to remove effect ("Until a creature takes an action to douse the fire...")
        };
        public static Attack MagminTouch {
            get {
                return new Attack("Touch", 4, new Damage(DamageType.FIRE, "2d6"), 5, null, MagminTouchEffect);
            }
        }
        public static readonly AttackEffect MammothStompEffect = delegate (Combattant attacker, Combattant target) {
            if (!target.HasCondition(ConditionType.PRONE)) return;
            int amount = new Dice("4d10+7").Roll(); // FIXME: Cannot crit because attack roll is not available here
            target.TakeDamage(attacker, DamageType.BLUDGEONING, amount);
        };
        public static Attack MammothStomp {
            get {
                return new Attack("Stomp", 10, new Damage(DamageType.BLUDGEONING, "1d1-1"), 5, null, MammothStompEffect);
            }
        }
        public static Attack MammothGore {
            get {
                return new Attack("Gore", 10, new Damage(DamageType.PIERCING, "4d8+7"), 10);
            }
        }
        public static Attack ManticoreBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d8+3"), 5);
            }
        }
        public static Attack ManticoreClaw {
            get {
                return new Attack("Claw", 5, new Damage(DamageType.SLASHING, "1d6+3"), 5);
            }
        }
        public static Attack ManticoreTailSpike {
            get {
                return new Attack("Tail Spike", 5, new Damage(DamageType.PIERCING, "1d8+3"), 5, 100, 200);
            }
        }
        public static readonly AttackEffect MarilithTailEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 19, Size.MEDIUM, true, MarilithTail);
            // TODO: the marilith can automatically hit the target with its tail
        };
        public static Attack MarilithTail {
            get {
                return new Attack("Tail", 9, new Damage(DamageType.BLUDGEONING, "2d10+4"), 5, null, MarilithTailEffect);
            }
        }
        public static Attack MarilithLongsword {
            get {
                return new Attack("Longsword", 9, new Damage(DamageType.SLASHING, "2d8+4"), 5);
            }
        }
        public static readonly AttackEffect MastiffBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target.HasEffect(Effects.Immunity(ConditionType.PRONE))) return;
            if (target.DC(MastiffBite, 11, AbilityType.STRENGTH)) return;
            target.AddCondition(ConditionType.PRONE);
        };
        public static Attack MastiffBite {
            get {
                return new Attack("Bite", 3, new Damage(DamageType.PIERCING, "1d6+1"), 5, null, MastiffBiteEffect);
            }
        }
        public static Attack MedusaLongbow {
            get {
                return new Attack("Longbow", 5, new Damage(DamageType.PIERCING, "1d8+2"), 150, 600, new Damage(DamageType.POISON, "2d6"));
            }
        }
        public static Attack MedusaSnakeHair {
            get {
                return new Attack("Snake Hair", 5, new Damage(DamageType.PIERCING, "1d4+2"), 5, new Damage(DamageType.POISON, "4d6"));
            }
        }
        public static Attack MedusaShortsword {
            get {
                return new Attack("Shortsword", 5, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack MerfolkSpearMelee {
            get {
                return new Attack("Spear", 2, new Damage(DamageType.PIERCING, "1d8"), 5);
            }
        }
        public static Attack MerfolkSpearRanged {
            get {
                return new Attack("Spear", 2, new Damage(DamageType.PIERCING, "1d6"), 20, 60);
            }
        }
        public static readonly AttackEffect MerrowHarpoonEffect = delegate (Combattant attacker, Combattant target) {
            if (target.Size > Size.HUGE) return;
            // TODO: it must succeed on a Strength contest against the merrow or be pulled up to 20 feet toward the merrow.
        };
        public static Attack MerrowHarpoonMelee {
            get {
                return new Attack("Harpoon", 6, new Damage(DamageType.PIERCING, "2d6+4"), 5);
            }
        }
        public static Attack MerrowHarpoonRanged {
            get {
                return new Attack("Harpoon", 6, new Damage(DamageType.PIERCING, "2d6+4"), 20, 60, null, MerrowHarpoonEffect);
            }
        }
        public static Attack MerrowBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "1d8+4"), 5);
            }
        }
        public static Attack MerrowClaws {
            get {
                return new Attack("Claws", 6, new Damage(DamageType.SLASHING, "2d4+4"), 5);
            }
        }
        public static readonly AttackEffect MimicPseudopodEffect = delegate (Combattant attacker, Combattant target) {
            // TODO: f the mimic is in object form, the target is subjected to its Adhesive trait
        };
        public static Attack MimicPseudopod {
            get {
                return new Attack("Pseudopod", 5, new Damage(DamageType.BLUDGEONING, "1d8+3"), 5, null, MimicPseudopodEffect);
            }
        }
        public static Attack MimicBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d8+3"), 5, new Damage(DamageType.ACID, "1d8"));
            }
        }
        public static Attack MinotaurGreataxe {
            get {
                return new Attack("Greataxe", 6, new Damage(DamageType.SLASHING, "2d12+4"), 5);
            }
        }
        public static Attack MinotaurGore {
            get {
                return new Attack("Gore", 6, new Damage(DamageType.PIERCING, "2d8+4"), 5);
            }
        }
        public static Attack MinotaurSkeletonGreataxe {
            get {
                return new Attack("Greataxe", 6, new Damage(DamageType.SLASHING, "2d12+4"), 5);
            }
        }
        public static Attack MinotaurSkeletonGore {
            get {
                return new Attack("Gore", 6, new Damage(DamageType.PIERCING, "2d8+4"), 5);
            }
        }
        public static Attack MuleHooves {
            get {
                return new Attack("Hooves", 2, new Damage(DamageType.BLUDGEONING, "1d4+2"), 5);
            }
        }
        public static readonly AttackEffect MummyRottingFistEffect = delegate (Combattant attacker, Combattant target) {
            if (target.DC(MummyRottingFist, 12, AbilityType.CONSTITUTION)) return;
            if (target.HasEffect(Effect.CURSE_MUMMY_ROT)) return;
            target.AddEffect(Effect.CURSE_MUMMY_ROT);
        };
        public static Attack MummyRottingFist {
            get {
                return new Attack("Rotting Fist", 5, new Damage(DamageType.BLUDGEONING, "2d6+3"), 5, new Damage(DamageType.NECROTIC, "3d6"), MummyRottingFistEffect);
            }
        }
        public static readonly AttackEffect MummyLordRottingFistEffect = delegate (Combattant attacker, Combattant target) {
            if (target.DC(MummyRottingFist, 16, AbilityType.CONSTITUTION)) return;
            if (target.HasEffect(Effect.CURSE_MUMMY_ROT)) return;
            target.AddEffect(Effect.CURSE_MUMMY_ROT);
        };
        public static Attack MummyLordRottingFist {
            get {
                return new Attack("Rotting Fist", 9, new Damage(DamageType.BLUDGEONING, "3d6+4"), 5, new Damage(DamageType.NECROTIC, "6d6"), MummyLordRottingFistEffect);
            }
        }
    }

    public partial struct Monsters {
        /* TODO */
        public static Monster Mage {
            get {
                Monster mage = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.MAGE, Alignment.UNALIGNED, 9, 14, 11, 17, 12, 11, 12, "9d8", 40, 6,
                    new Attack[] { Attacks.MageDaggerMelee }, new Attack[] { Attacks.MageDaggerRanged }, Size.MEDIUM
                );
                mage.AddProficiency(Proficiency.INTELLIGENCE);
                mage.AddProficiency(Proficiency.WISDOM);
                mage.AddProficiency(Proficiency.ARCANA);
                mage.AddProficiency(Proficiency.HISTORY);
                mage.AddFeat(Feat.SPELLCASTING_MAGE);
                return mage;
            }
        }

        /* TODO */
        public static Monster MagmaMephit {
            get {
                Monster magmaMephit = new Monster(
                    Monsters.Type.ELEMENTAL, Monsters.ID.MAGMA_MEPHIT, Alignment.NEUTRAL_EVIL, 8, 12, 12, 7, 10, 10, 11, "5d6+5", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.MagmaMephitClaws }, new Attack[] { }, Size.SMALL
                );
                magmaMephit.AddProficiency(Proficiency.STEALTH);
                magmaMephit.AddEffect(Effect.VULNERABILITY_COLD);
                magmaMephit.AddEffect(Effect.IMMUNITY_FIRE);
                magmaMephit.AddEffect(Effect.IMMUNITY_POISON);
                magmaMephit.AddEffect(Effect.IMMUNITY_POISONED);
                magmaMephit.AddFeat(Feat.DEATH_BURST_MAGMA_MEPHIT);
                magmaMephit.AddFeat(Feat.FALSE_APPEARANCE);
                magmaMephit.AddFeat(Feat.INNATE_SPELLCASTING_MAGMA_MEPHIT);
                return magmaMephit;
            }
        }

        /* TODO */
        public static Monster Magmin {
            get {
                Monster magmin = new Monster(
                    Monsters.Type.ELEMENTAL, Monsters.ID.MAGMIN, Alignment.CHAOTIC_NEUTRAL, 7, 15, 12, 8, 11, 10, 14, "2d6+2", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.MagminTouch }, new Attack[] { }, Size.SMALL
                );
                magmin.AddEffect(Effect.RESISTANCE_NONMAGIC);
                magmin.AddEffect(Effect.IMMUNITY_FIRE);
                magmin.AddFeat(Feat.DEATH_BURST_MAGMIN);
                magmin.AddFeat(Feat.IGNITED_ILLUMINATION);
                return magmin;
            }
        }

        /* TODO */
        public static Monster Mammoth {
            get {
                Monster mammoth = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.MAMMOTH, Alignment.UNALIGNED, 24, 9, 21, 3, 11, 6, 13, "11d12+55", 40, 6,
                    new Attack[] { Attacks.MammothGore, Attacks.MammothStomp }, new Attack[] { }, Size.HUGE
                );
                mammoth.AddFeat(Feat.TRAMPLING_CHARGE_MAMMOTH);
                return mammoth;
            }
        }

        /* TODO */
        public static Monster Manticore {
            get {
                Monster manticore = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.MANTICORE, Alignment.LAWFUL_EVIL, 17, 16, 17, 7, 12, 8, 14, "8d10+24", 40, 3,
                    new Attack[] { Attacks.ManticoreBite, Attacks.ManticoreClaw }, new Attack[] { Attacks.ManticoreTailSpike }, Size.LARGE
                );
                manticore.AddFeat(Feat.TAIL_SPIKE_REGROWTH);
                return manticore;
            }
        }

        /* TODO */
        public static Monster Marilith {
            get {
                Monster marilith = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.MARILITH, Alignment.CHAOTIC_EVIL, 18, 20, 20, 18, 16, 20, 18, "18d10+90", 40, 16,
                    new Attack[] { Attacks.MarilithLongsword, Attacks.MarilithTail }, new Attack[] { }, Size.LARGE
                );
                marilith.AddProficiency(Proficiency.STRENGTH);
                marilith.AddProficiency(Proficiency.CONSTITUTION);
                marilith.AddProficiency(Proficiency.WISDOM);
                marilith.AddProficiency(Proficiency.CHARISMA);
                marilith.AddEffect(Effect.RESISTANCE_COLD);
                marilith.AddEffect(Effect.RESISTANCE_FIRE);
                marilith.AddEffect(Effect.RESISTANCE_LIGHTNING);
                marilith.AddEffect(Effect.RESISTANCE_NONMAGIC);
                marilith.AddEffect(Effect.IMMUNITY_POISON);
                marilith.AddEffect(Effect.IMMUNITY_POISONED);
                marilith.AddFeat(Feat.MAGIC_RESISTANCE);
                marilith.AddFeat(Feat.MAGIC_WEAPONS);
                marilith.AddFeat(Feat.REACTIVE);
                return marilith;
            }
        }

        /* TODO */
        public static Monster Mastiff {
            get {
                Monster mastiff = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.MASTIFF, Alignment.UNALIGNED, 13, 14, 12, 3, 12, 7, 12, "1d8+1", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.MastiffBite }, new Attack[] { }, Size.MEDIUM
                );
                mastiff.AddProficiency(Proficiency.PERCEPTION);
                mastiff.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                return mastiff;
            }
        }

        /* TODO */
        public static Monster Medusa {
            get {
                Monster medusa = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.MEDUSA, Alignment.LAWFUL_EVIL, 10, 15, 16, 12, 13, 15, 15, "17d8+51", 40, 6,
                    new Attack[] { Attacks.MedusaSnakeHair, Attacks.MedusaShortsword }, new Attack[] { Attacks.MedusaLongbow }, Size.MEDIUM
                );
                medusa.AddProficiency(Proficiency.DECEPTION);
                medusa.AddProficiency(Proficiency.INSIGHT);
                medusa.AddProficiency(Proficiency.PERCEPTION);
                medusa.AddProficiency(Proficiency.STEALTH);
                medusa.AddFeat(Feat.PETRIFYING_GAZE_MEDUSA);
                return medusa;
            }
        }

        /* TODO */
        public static Monster Merfolk {
            get {
                Monster merfolk = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.MERFOLK, Alignment.NEUTRAL, 10, 13, 12, 11, 11, 12, 11, "2d8+2", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.MerfolkSpearMelee }, new Attack[] { Attacks.MerfolkSpearRanged }, Size.MEDIUM
                );
                merfolk.AddProficiency(Proficiency.PERCEPTION);
                merfolk.AddFeat(Feat.AMPHIBIOUS);
                return merfolk;
            }
        }

        /* TODO */
        public static Monster Merrow {
            get {
                Monster merrow = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.MERROW, Alignment.CHAOTIC_EVIL, 18, 10, 15, 8, 10, 9, 13, "6d10+12", 40, 2,
                    new Attack[] { Attacks.MerrowBite, Attacks.MerrowClaws, Attacks.MerrowHarpoonMelee }, new Attack[] { Attacks.MerrowHarpoonRanged }, Size.LARGE
                );
                merrow.AddFeat(Feat.AMPHIBIOUS);
                return merrow;
            }
        }

        /* TODO */
        public static Monster Mimic {
            get {
                Monster mimic = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.MIMIC, Alignment.NEUTRAL, 17, 12, 15, 5, 13, 8, 12, "9d8+18", 40, 2,
                    new Attack[] { Attacks.MimicPseudopod, Attacks.MimicBite }, new Attack[] { }, Size.MEDIUM
                );
                mimic.AddProficiency(Proficiency.STEALTH);
                mimic.AddEffect(Effect.IMMUNITY_ACID);
                mimic.AddEffect(Effect.IMMUNITY_PRONE);
                mimic.AddFeat(Feat.SHAPECHANGER_MIMIC);
                mimic.AddFeat(Feat.ADHESIVE);
                mimic.AddFeat(Feat.FALSE_APPEARANCE);
                mimic.AddFeat(Feat.GRAPPLER);
                return mimic;
            }
        }

        /* TODO */
        public static Monster Minotaur {
            get {
                Monster minotaur = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.MINOTAUR, Alignment.CHAOTIC_EVIL, 18, 11, 16, 6, 16, 9, 14, "9d10+27", 40, 3,
                    new Attack[] { Attacks.MinotaurGreataxe, Attacks.MinotaurGore }, new Attack[] { }, Size.LARGE
                );
                minotaur.AddProficiency(Proficiency.PERCEPTION);
                minotaur.AddFeat(Feat.CHARGE_MINOTAUR);
                minotaur.AddFeat(Feat.LABYRINTHINE_RECALL);
                minotaur.AddFeat(Feat.RECKLESS);
                return minotaur;
            }
        }

        /* TODO */
        public static Monster MinotaurSkeleton {
            get {
                Monster minotaurSkeleton = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.MINOTAUR_SKELETON, Alignment.LAWFUL_EVIL, 18, 11, 15, 6, 8, 5, 12, "9d10+18", 40, 2,
                    new Attack[] { Attacks.MinotaurSkeletonGreataxe, Attacks.MinotaurSkeletonGore }, new Attack[] { }, Size.LARGE
                );
                minotaurSkeleton.AddEffect(Effect.VULNERABILITY_BLUDGEONING);
                minotaurSkeleton.AddEffect(Effect.IMMUNITY_POISON);
                minotaurSkeleton.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                minotaurSkeleton.AddEffect(Effect.IMMUNITY_POISONED);
                minotaurSkeleton.AddFeat(Feat.CHARGE_MINOTAUR_SKELETON);
                return minotaurSkeleton;
            }
        }

        /* TODO */
        public static Monster Mule {
            get {
                Monster mule = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.MULE, Alignment.UNALIGNED, 14, 10, 13, 2, 10, 5, 10, "2d8+2", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.MuleHooves }, new Attack[] { }, Size.MEDIUM
                );
                mule.AddFeat(Feat.BEAST_OF_BURDEN);
                mule.AddFeat(Feat.SURE_FOOTED);
                return mule;
            }
        }

        /* TODO */
        public static Monster Mummy {
            get {
                Monster mummy = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.MUMMY, Alignment.LAWFUL_EVIL, 16, 8, 15, 6, 10, 12, 11, "9d8+18", 40, 3,
                    new Attack[] { Attacks.MummyRottingFist }, new Attack[] { }, Size.MEDIUM
                );
                mummy.AddProficiency(Proficiency.WISDOM);
                mummy.AddEffect(Effect.VULNERABILITY_FIRE);
                mummy.AddEffect(Effect.RESISTANCE_NONMAGIC);
                mummy.AddEffect(Effect.IMMUNITY_NECROTIC);
                mummy.AddEffect(Effect.IMMUNITY_POISON);
                mummy.AddEffect(Effect.IMMUNITY_CHARMED);
                mummy.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                mummy.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                mummy.AddEffect(Effect.IMMUNITY_PARALYZED);
                mummy.AddEffect(Effect.IMMUNITY_POISONED);
                return mummy;
            }
        }

        /* TODO */
        public static Monster MummyLord {
            get {
                Monster mummyLord = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.MUMMY_LORD, Alignment.LAWFUL_EVIL, 18, 10, 17, 11, 18, 16, 17, "13d8+39", 40, 15,
                    new Attack[] { Attacks.MummyLordRottingFist }, new Attack[] { }, Size.MEDIUM
                );
                mummyLord.AddProficiency(Proficiency.CONSTITUTION);
                mummyLord.AddProficiency(Proficiency.INTELLIGENCE);
                mummyLord.AddProficiency(Proficiency.WISDOM);
                mummyLord.AddProficiency(Proficiency.CHARISMA);
                mummyLord.AddProficiency(Proficiency.HISTORY);
                mummyLord.AddProficiency(Proficiency.RELIGION);
                mummyLord.AddEffect(Effect.VULNERABILITY_FIRE);
                mummyLord.AddEffect(Effect.IMMUNITY_NECROTIC);
                mummyLord.AddEffect(Effect.IMMUNITY_POISON);
                mummyLord.AddEffect(Effect.IMMUNITY_NONMAGIC);
                mummyLord.AddEffect(Effect.IMMUNITY_CHARMED);
                mummyLord.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                mummyLord.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                mummyLord.AddEffect(Effect.IMMUNITY_PARALYZED);
                mummyLord.AddEffect(Effect.IMMUNITY_POISONED);
                mummyLord.AddFeat(Feat.MAGIC_RESISTANCE);
                mummyLord.AddFeat(Feat.REJUVENATION_MUMMY_LORD);
                mummyLord.AddFeat(Feat.SPELLCASTING_MUMMY_LORD);
                return mummyLord;
            }
        }

    }
}