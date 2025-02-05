namespace srd5 {
    public partial struct Attacks {
        public static Attack GargoyleBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack GargoyleClaws {
            get {
                return new Attack("Claws", 4, new Damage(DamageType.SLASHING, "1d6+2"), 5);
            }
        }
        public static Attack GelatinousCubePseudopod {
            get {
                return new Attack("Pseudopod", 4, new Damage(DamageType.ACID, "3d6"), 5);
            }
        }
        public static readonly AttackEffect GhastClawsEffect = delegate (Combattant attacker, Combattant target) {
            if (target is Monster monster && monster.Type == Monsters.Type.UNDEAD) return;
            bool success = target.DC(GhastClaws, 10, AbilityType.CONSTITUTION);
            if (success) return;
            target.AddEffect(Effect.GHAST_CLAWS_PARALYZATION);
        };
        public static Attack GhastClaws {
            get {
                return new Attack("Claws", 5, new Damage(DamageType.SLASHING, "2d6+3"), 5, null, GhastClawsEffect);
            }
        }
        public static Attack GhastBite {
            get {
                return new Attack("Bite", 3, new Damage(DamageType.PIERCING, "2d8+3"), 5);
            }
        }
        public static Attack GhostWitheringTouch {
            get {
                return new Attack("Withering Touch", 5, new Damage(DamageType.NECROTIC, "4d6+3"), 5);
            }
        }
        public static readonly AttackEffect GhoulClawsEffect = delegate (Combattant attacker, Combattant target) {
            if (target is CharacterSheet sheet) {
                if (sheet.Race.Race == Race.HALF_ELF || sheet.Race.Race == Race.HIGH_ELF) return;
            }
            if (target is Monster monster && monster.Type == Monsters.Type.UNDEAD) return;
            bool success = target.DC(GhoulClaws, 10, AbilityType.CONSTITUTION);
            if (success) return;
            target.AddEffect(Effect.GHOUL_CLAWS_PARALYZATION);
        };
        public static Attack GhoulClaws {
            get {
                return new Attack("Claws", 4, new Damage(DamageType.SLASHING, "2d4+2"), 5, null, GhoulClawsEffect);
            }
        }
        public static Attack GhoulBite {
            get {
                return new Attack("Bite", 2, new Damage(DamageType.PIERCING, "2d6+2"), 5);
            }
        }
        public static Attack GiantApeFist {
            get {
                return new Attack("Fist", 9, new Damage(DamageType.BLUDGEONING, "3d10+6"), 10);
            }
        }
        public static Attack GiantApeRock {
            get {
                return new Attack("Rock", 9, new Damage(DamageType.BLUDGEONING, "7d6+6"), 5, 50, 100);
            }
        }
        public static Attack GiantBadgerBite {
            get {
                return new Attack("Bite", 3, new Damage(DamageType.PIERCING, "1d6+1"), 5);
            }
        }
        public static Attack GiantBadgerClaws {
            get {
                return new Attack("Claws", 3, new Damage(DamageType.SLASHING, "2d4+1"), 5);
            }
        }
        public static Attack GiantBatBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack GiantBoarTusk {
            get {
                return new Attack("Tusk", 5, new Damage(DamageType.SLASHING, "2d6+3"), 5);
            }
        }
        public static readonly AttackEffect GiantCentipedeBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target.IsImmune(DamageType.POISON)) return;
            if (target.DC(GiantCentipedeBite, 11, AbilityType.CONSTITUTION)) return;
            target.TakeDamage(attacker, DamageType.POISON, "3d6");
            // TODO: "If the poison damage reduces the target to 0 hit points, the target is stable but poisoned for 1 hour, even after regaining hit points, and is paralyzed while poisoned in this way."
        };
        public static Attack GiantCentipedeBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d4+2"), 5, null, GiantCentipedeBiteEffect);
            }
        }
        public static readonly AttackEffect GiantConstrictorSnakeConstrictEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 16, Monsters.GiantConstrictorSnake.Size + 1, true);
        };
        public static Attack GiantConstrictorSnakeConstrict {
            get {
                return new Attack("Constrict", 6, new Damage(DamageType.BLUDGEONING, "2d8+4"), 5, null, GiantConstrictorSnakeConstrictEffect);
            }
        }
        public static Attack GiantConstrictorSnakeBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "2d6+4"), 10);
            }
        }
        public static readonly AttackEffect GiantCrabClawEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 11, Monsters.GiantCrab.Size + 1, true, null, 2);
        };
        public static Attack GiantCrabClaw {
            get {
                return new Attack("Claw", 3, new Damage(DamageType.BLUDGEONING, "1d6+1"), 5, null, GiantCrabClawEffect);
            }
        }
        public static readonly AttackEffect GiantCrocodileBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 16, Monsters.GiantCrocodile.Size + 1, true, GiantCrocodileBite);
        };
        public static Attack GiantCrocodileBite {
            get {
                return new Attack("Bite", 8, new Damage(DamageType.PIERCING, "3d10+5"), 5, null, GiantCrocodileBiteEffect);
            }
        }
        public static readonly AttackEffect GiantCrocodileTailEffect = delegate (Combattant attacker, Combattant target) {
            if (target.DC(GiantCrocodileTail, 16, AbilityType.STRENGTH)) return;
            target.AddCondition(ConditionType.PRONE);
        };
        public static Attack GiantCrocodileTail {
            get {
                return new Attack("Tail", 8, new Damage(DamageType.BLUDGEONING, "2d8+5"), 5, null, GiantCrocodileTailEffect);
            }
        }
        public static Attack GiantEagleBeak {
            get {
                return new Attack("Beak", 5, new Damage(DamageType.PIERCING, "1d6+3"), 5);
            }
        }
        public static Attack GiantEagleTalons {
            get {
                return new Attack("Talons", 5, new Damage(DamageType.SLASHING, "2d6+3"), 5);
            }
        }
        public static readonly AttackEffect GiantElkHoovesEffect = delegate (Combattant attacker, Combattant target) {
            if (!target.HasCondition(ConditionType.PRONE)) return;
            int amount = new Dice("4d8+4").Roll(); // FIXME: Cannot crit because attack roll is not available here
            target.TakeDamage(attacker, DamageType.BLUDGEONING, amount);
        };
        public static Attack GiantElkHooves {
            get {
                return new Attack("Hooves", 6, new Damage(DamageType.BLUDGEONING, 0), 5, null, GiantElkHoovesEffect);
            }
        }
        public static Attack GiantElkRam {
            get {
                return new Attack("Ram", 6, new Damage(DamageType.BLUDGEONING, "2d6+4"), 10);
            }
        }
        public static Attack GiantFireBeetleBite {
            get {
                return new Attack("Bite", 1, new Damage(DamageType.SLASHING, "1d6-1"), 5);
            }
        }
        public static readonly AttackEffect GiantFrogBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 11, Monsters.GiantFrog.Size + 1, true, GiantFrogBite);
        };
        public static Attack GiantFrogBite {
            get {
                return new Attack("Bite", 3, new Damage(DamageType.PIERCING, "1d6+1"), 5, null, GiantFrogBiteEffect);
            }
        }
        public static Attack GiantGoatRam {
            get {
                return new Attack("Ram", 5, new Damage(DamageType.BLUDGEONING, "2d4+3"), 5);
            }
        }
        public static Attack GiantHyenaBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "2d6+3"), 5);
            }
        }
        public static Attack GiantLizardBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d8+2"), 5);
            }
        }
        public static readonly AttackEffect GiantOctopusTentaclesEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 11, Monsters.GiantOctopus.Size + 1, true, GiantOctopusTentacles);
        };
        public static Attack GiantOctopusTentacles {
            get {
                return new Attack("Tentacles", 15, new Damage(DamageType.BLUDGEONING, "2d6+3"), 5, null, GiantOctopusTentaclesEffect);
            }
        }
        public static Attack GiantOwlTalons {
            get {
                return new Attack("Talons", 3, new Damage(DamageType.SLASHING, "2d6+1"), 5);
            }
        }
        public static readonly AttackEffect GiantPoisonousSnakeBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, GiantPoisonousSnakeBite, "3d6", 11);
        };
        public static Attack GiantPoisonousSnakeBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "1d4+4"), 5, null, GiantPoisonousSnakeBiteEffect);
            }
        }
        public static Attack GiantRatBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d4+2"), 5);
            }
        }
        public static readonly AttackEffect GiantRatDiseasedBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target.DC(GiantRatDiseasedBite, 10, AbilityType.CONSTITUTION)) return;
            target.AddEffect(Effect.GIANT_RAT_DISEASED_BITE);
        };
        public static Attack GiantRatDiseasedBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d4+2"), 5, null, GiantRatDiseasedBiteEffect);
            }
        }
        public static readonly AttackEffect GiantScorpionClawEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 12, Monsters.GiantScorpion.Size + 1, false, null, 2);
        };
        public static Attack GiantScorpionClaw {
            get {
                return new Attack("Claw", 4, new Damage(DamageType.BLUDGEONING, "1d8+2"), 5, null, GiantScorpionClawEffect);
            }
        }
        public static readonly AttackEffect GiantScorpionStingEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, GiantScorpionSting, "4d10", 12);
        };
        public static Attack GiantScorpionSting {
            get {
                return new Attack("Sting", 4, new Damage(DamageType.PIERCING, "1d10+2"), 5, null, GiantScorpionStingEffect);
            }
        }
        public static Attack GiantSeaHorseRam {
            get {
                return new Attack("Ram", 3, new Damage(DamageType.BLUDGEONING, "1d6+1"), 5);
            }
        }
        public static Attack GiantSharkBite {
            get {
                return new Attack("Bite", 9, new Damage(DamageType.PIERCING, "3d10+6"), 5);
            }
        }
        public static readonly AttackEffect GiantSpiderBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, GiantSpiderBite, "2d8", 11);
            // TODO: If the poison damage reduces the target to 0 hit points, the target is stable but poisoned for 1 hour, even after regaining hit points, and is paralyzed while poisoned in this way.
        };
        public static Attack GiantSpiderBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d8+3"), 5, null, GiantSpiderBiteEffect);
            }
        }
        public static readonly AttackEffect GiantSpiderWebEffect = delegate (Combattant attacker, Combattant target) {
            if (target.HasEffect(Effect.IMMUNITY_RESTRAINED)) return;
            target.AddEffect(Effect.GIANT_SPIDER_WEB);
            // TODO: As an action, the restrained target can make a DC 12 Strength check, bursting the webbing on a success.
            // The webbing can also be attacked and destroyed (AC 10; hp 5; vulnerability to fire damage; immunity to bludgeoning, poison, and psychic damage)
        };
        public static Attack GiantSpiderWeb {
            get {
                return new Attack("Web", 5, new Damage(DamageType.BLUDGEONING, 0), 5, null, GiantSpiderWebEffect);
            }
        }
        public static readonly AttackEffect GiantToadBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 13, Monsters.GiantToad.Size + 1, true, GiantToadBite);
        };
        public static Attack GiantToadBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d10+2"), 5, new Damage(DamageType.POISON, "1d10"), GiantToadBiteEffect);
            }
        }
        public static Attack GiantVultureBeak {
            get {
                return new Attack("Beak", 4, new Damage(DamageType.PIERCING, "2d4+2"), 5);
            }
        }
        public static Attack GiantVultureTalons {
            get {
                return new Attack("Talons", 4, new Damage(DamageType.SLASHING, "2d6+2"), 5);
            }
        }
        public static readonly AttackEffect GiantWaspStingEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, GiantWaspSting, "3d6", 11);
            // TODO: If the poison damage reduces the target to 0 hit points, the target is stable but poisoned for 1 hour, even after regaining hit points, and is paralyzed while poisoned in this way.
        };
        public static Attack GiantWaspSting {
            get {
                return new Attack("Sting", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5, null, GiantWaspStingEffect);
            }
        }
        public static Attack GiantWeaselBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d4+3"), 5);
            }
        }
        public static readonly AttackEffect GiantWolfSpiderBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, GiantWolfSpiderBite, "2d6", 11);
            // TODO: If the poison damage reduces the target to 0 hit points, the target is stable but poisoned for 1 hour, even after regaining hit points, and is paralyzed while poisoned in this way.
        };
        public static Attack GiantWolfSpiderBite {
            get {
                return new Attack("Bite", 3, new Damage(DamageType.PIERCING, "1d6+1"), 5, null, GiantWolfSpiderBiteEffect);
            }
        }
        public static readonly AttackEffect GibberingMoutherBitesEffect = delegate (Combattant attacker, Combattant target) {
            if (target.Size > Size.MEDIUM) return;
            if (target.DC(GibberingMoutherBites, 10, AbilityType.STRENGTH)) return;
            target.AddCondition(ConditionType.PRONE);
            // TODO:  If the target is killed by this damage, it is absorbed into the mouther.
        };
        public static Attack GibberingMoutherBites {
            get {
                return new Attack("Bites", 2, new Damage(DamageType.PIERCING, "5d6"), 5, null, GibberingMoutherBitesEffect);
            }
        }
        public static readonly AttackEffect GlabrezuPincerEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 15, Size.MEDIUM, false, null, 2);
        };
        public static Attack GlabrezuPincer {
            get {
                return new Attack("Pincer", 9, new Damage(DamageType.BLUDGEONING, "2d10+5"), 5, null, GlabrezuPincerEffect);
            }
        }
        public static Attack GlabrezuFist {
            get {
                return new Attack("Fist", 9, new Damage(DamageType.BLUDGEONING, "2d4+2"), 5);
            }
        }
        public static Attack GladiatorSpear {
            get {
                return new Attack("Spear", 7, new Damage(DamageType.PIERCING, "2d6+4"), 5);
            }
        }
        public static readonly AttackEffect GladiatorShieldBashEffect = delegate (Combattant attacker, Combattant target) {
            if (target.Size > Size.MEDIUM) return;
            if (target.DC(GladiatorShieldBash, 15, AbilityType.STRENGTH)) return;
            target.AddCondition(ConditionType.PRONE);
        };
        public static Attack GladiatorShieldBash {
            get {
                return new Attack("Shield Bash", 7, new Damage(DamageType.BLUDGEONING, "2d4+4"), 5, null, GladiatorShieldBashEffect);
            }
        }
        public static Attack GnollSpearMelee {
            get {
                return new Attack("Spear", 4, new Damage(DamageType.PIERCING, "1d8+2"), 5);
            }
        }
        public static Attack GnollSpearRanged {
            get {
                return new Attack("Spear", 4, new Damage(DamageType.PIERCING, "1d6+2"), 20, 60);
            }
        }
        public static Attack GnollBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d4+2"), 5);
            }
        }
        public static Attack GnollLongbow {
            get {
                return new Attack("Longbow", 3, new Damage(DamageType.PIERCING, "1d8+1"), 5, 150, 600);
            }
        }
        public static Attack GoatRam {
            get {
                return new Attack("Ram", 3, new Damage(DamageType.BLUDGEONING, "1d4+1"), 5);
            }
        }
        public static Attack GoblinScimitar {
            get {
                return new Attack("Scimitar", 4, new Damage(DamageType.SLASHING, "1d6+2"), 5);
            }
        }
        public static Attack GoblinShortbow {
            get {
                return new Attack("Shortbow", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5, 80, 320);
            }
        }
        public static Attack GoldDragonWyrmlingBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "1d10+4"), 5);
            }
        }
        public static Attack GorgonGore {
            get {
                return new Attack("Gore", 8, new Damage(DamageType.PIERCING, "2d12+5"), 5);
            }
        }
        public static Attack GorgonHooves {
            get {
                return new Attack("Hooves", 8, new Damage(DamageType.BLUDGEONING, "2d10+5"), 5);
            }
        }

        public static readonly AttackEffect GrayOozePseudopodEffect = delegate (Combattant attacker, Combattant target) {
            // exactly same effect as Black Pudding
            BlackPuddingPseudopodEffect(attacker, target);
        };
        public static Attack GrayOozePseudopod {
            get {
                return new Attack("Pseudopod", 3, new Damage(DamageType.BLUDGEONING, "1d6+1"), 5, new Damage(DamageType.ACID, "2d6"), GrayOozePseudopodEffect);
            }
        }
        public static Attack GreenDragonWyrmlingBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d10+2"), 5, new Damage(DamageType.POISON, "1d6"));
            }
        }
        public static Attack GreenHagClaws {
            get {
                return new Attack("Claws", 6, new Damage(DamageType.SLASHING, "2d8+4"), 5);
            }
        }
        public static Attack GrickTentacles {
            get {
                return new Attack("Tentacles", 4, new Damage(DamageType.SLASHING, "2d6+2"), 5);
            }
        }
        public static Attack GrickBeak {
            get {
                return new Attack("Beak", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack GriffonBeak {
            get {
                return new Attack("Beak", 6, new Damage(DamageType.PIERCING, "1d8+4"), 5);
            }
        }
        public static Attack GriffonClaws {
            get {
                return new Attack("Claws", 6, new Damage(DamageType.SLASHING, "2d6+4"), 5);
            }
        }
        public static Attack GrimlockSpikedBoneClub {
            get {
                return new Attack("Spiked Bone Club", 5, new Damage(DamageType.BLUDGEONING, "1d4+3"), 5, new Damage(DamageType.PIERCING, "1d4"));
            }
        }
        public static Attack GuardSpearMelee {
            get {
                return new Attack("Spear", 3, new Damage(DamageType.PIERCING, "1d8+1"), 5);
            }
        }
        public static Attack GuardSpearRanged {
            get {
                return new Attack("Spear", 3, new Damage(DamageType.PIERCING, "1d6+1"), 5, 20, 60);
            }
        }
        public static readonly AttackEffect GuardianNagaBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, GuardianNagaBite, "10d8", 15);
        };
        public static Attack GuardianNagaBite {
            get {
                return new Attack("Bite", 8, new Damage(DamageType.PIERCING, "1d8+4"), 5, null, GuardianNagaBiteEffect);
            }
        }
        public static readonly AttackEffect GuardianNagaSpitPoisonEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, GuardianNagaSpitPoison, "10d8", 15);
        };
        public static Attack GuardianNagaSpitPoison {
            get {
                return new Attack("Spit Poison", 8, new Damage(DamageType.POISON, 0), 5, null, GuardianNagaSpitPoisonEffect);
            }
        }
        public static Attack GynosphinxClaw {
            get {
                return new Attack("Claw", 9, new Damage(DamageType.SLASHING, "2d8+4"), 5);
            }
        }

    }
    public partial struct Monsters {
        /* TODO */
        public static Monster Gargoyle {
            get {
                Monster gargoyle = new Monster(
                    Monsters.Type.ELEMENTAL, Monsters.ID.GARGOYLE, Alignment.CHAOTIC_EVIL, 15, 11, 16, 6, 11, 7, 15, "7d8+21", 40, 2,
                    new Attack[] { Attacks.GargoyleBite, Attacks.GargoyleClaws }, new Attack[] { }, Size.MEDIUM
                );
                gargoyle.AddEffect(Effect.RESISTANCE_NONMAGIC);
                gargoyle.AddEffect(Effect.IMMUNITY_POISON);
                gargoyle.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                gargoyle.AddEffect(Effect.IMMUNITY_PETRIFIED);
                gargoyle.AddEffect(Effect.IMMUNITY_POISONED);
                gargoyle.AddFeat(Feat.FALSE_APPEARANCE);
                return gargoyle;
            }
        }

        /* TODO */
        public static Monster GelatinousCube {
            get {
                Monster gelatinousCube = new Monster(
                    Monsters.Type.OOZE, Monsters.ID.GELATINOUS_CUBE, Alignment.UNALIGNED, 14, 3, 20, 1, 6, 1, 6, "8d10+40", 40, 2,
                    new Attack[] { Attacks.GelatinousCubePseudopod }, new Attack[] { }, Size.LARGE
                );
                gelatinousCube.AddEffect(Effect.IMMUNITY_BLINDED);
                gelatinousCube.AddEffect(Effect.IMMUNITY_CHARMED);
                gelatinousCube.AddEffect(Effect.IMMUNITY_DEAFENED);
                gelatinousCube.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                gelatinousCube.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                gelatinousCube.AddEffect(Effect.IMMUNITY_PRONE);
                gelatinousCube.AddFeat(Feat.OOZE_CUBE);
                gelatinousCube.AddFeat(Feat.TRANSPARENT);
                return gelatinousCube;
            }
        }

        /* TODO */
        public static Monster Ghast {
            get {
                Monster ghast = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.GHAST, Alignment.CHAOTIC_EVIL, 16, 17, 10, 11, 10, 8, 13, "8d8", 40, 2,
                    new Attack[] { Attacks.GhastBite, Attacks.GhastClaws }, new Attack[] { }, Size.MEDIUM
                );
                ghast.AddEffect(Effect.RESISTANCE_NECROTIC);
                ghast.AddEffect(Effect.IMMUNITY_POISON);
                ghast.AddEffect(Effect.IMMUNITY_POISONED);
                ghast.AddEffect(Effect.IMMUNITY_CHARMED);
                ghast.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                ghast.AddFeat(Feat.STENCH_GHAST);
                ghast.AddFeat(Feat.TURN_DEFIANCE);
                return ghast;
            }
        }

        /* TODO */
        public static Monster Ghost {
            get {
                Monster ghost = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.GHOST, Alignment.UNALIGNED, 7, 13, 10, 10, 12, 17, 11, "10d8", 40, 4,
                    new Attack[] { Attacks.GhostWitheringTouch }, new Attack[] { }, Size.MEDIUM
                );
                ghost.AddEffect(Effect.RESISTANCE_ACID);
                ghost.AddEffect(Effect.RESISTANCE_FIRE);
                ghost.AddEffect(Effect.RESISTANCE_LIGHTNING);
                ghost.AddEffect(Effect.RESISTANCE_THUNDER);
                ghost.AddEffect(Effect.RESISTANCE_NONMAGIC);
                ghost.AddEffect(Effect.IMMUNITY_COLD);
                ghost.AddEffect(Effect.IMMUNITY_NECROTIC);
                ghost.AddEffect(Effect.IMMUNITY_POISON);
                ghost.AddEffect(Effect.IMMUNITY_CHARMED);
                ghost.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                ghost.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                ghost.AddEffect(Effect.IMMUNITY_GRAPPLED);
                ghost.AddEffect(Effect.IMMUNITY_PARALYZED);
                ghost.AddEffect(Effect.IMMUNITY_PETRIFIED);
                ghost.AddEffect(Effect.IMMUNITY_POISONED);
                ghost.AddEffect(Effect.IMMUNITY_PRONE);
                ghost.AddEffect(Effect.IMMUNITY_RESTRAINED);
                ghost.AddFeat(Feat.ETHEREAL_SIGHT);
                ghost.AddFeat(Feat.INCORPOREAL_MOVEMENT);
                return ghost;
            }
        }

        /* TODO */
        public static Monster Ghoul {
            get {
                Monster ghoul = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.GHOUL, Alignment.CHAOTIC_EVIL, 13, 15, 10, 7, 10, 6, 12, "5d8", 40, 1,
                    new Attack[] { Attacks.GhoulBite, Attacks.GhoulClaws }, new Attack[] { }, Size.MEDIUM
                );
                ghoul.AddEffect(Effect.IMMUNITY_POISON);
                ghoul.AddEffect(Effect.IMMUNITY_POISONED);
                ghoul.AddEffect(Effect.IMMUNITY_CHARMED);
                ghoul.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                return ghoul;
            }
        }

        /* TODO */
        public static Monster GiantApe {
            get {
                Monster giantApe = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_APE, Alignment.UNALIGNED, 23, 14, 18, 7, 12, 7, 12, "15d12+60", 40, 7,
                    new Attack[] { Attacks.GiantApeFist }, new Attack[] { Attacks.GiantApeRock }, Size.HUGE
                );
                giantApe.AddProficiency(Proficiency.ATHLETICS);
                giantApe.AddProficiency(Proficiency.PERCEPTION);
                return giantApe;
            }
        }

        /* TODO */
        public static Monster GiantBadger {
            get {
                Monster giantBadger = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_BADGER, Alignment.UNALIGNED, 13, 10, 15, 2, 12, 5, 10, "2d8+4", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GiantBadgerBite, Attacks.GiantBadgerClaws }, new Attack[] { }, Size.MEDIUM
                );
                giantBadger.AddFeat(Feat.KEEN_SMELL);
                return giantBadger;
            }
        }

        /* TODO */
        public static Monster GiantBat {
            get {
                Monster giantBat = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_BAT, Alignment.UNALIGNED, 15, 16, 11, 2, 12, 6, 13, "4d10", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GiantBatBite }, new Attack[] { }, Size.LARGE
                );
                giantBat.AddFeat(Feat.ECHOLOCATION);
                giantBat.AddFeat(Feat.KEEN_HEARING);
                return giantBat;
            }
        }

        /* TODO */
        public static Monster GiantBoar {
            get {
                Monster giantBoar = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_BOAR, Alignment.UNALIGNED, 17, 10, 16, 2, 7, 5, 12, "5d10+15", 40, 2,
                    new Attack[] { Attacks.GiantBoarTusk }, new Attack[] { }, Size.LARGE
                );
                giantBoar.AddFeat(Feat.CHARGE_GIANT_BOAR);
                giantBoar.AddFeat(Feat.RELENTLESS_10);
                return giantBoar;
            }
        }

        /* TODO */
        public static Monster GiantCentipede {
            get {
                Monster giantCentipede = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_CENTIPEDE, Alignment.UNALIGNED, 5, 14, 12, 1, 7, 3, 13, "1d6+1", 40, ChallengeRating.QUARTER,
                    new Attack[] { }, new Attack[] { }, Size.SMALL
                );
                return giantCentipede;
            }
        }

        /* TODO */
        public static Monster GiantConstrictorSnake {
            get {
                Monster giantConstrictorSnake = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_CONSTRICTOR_SNAKE, Alignment.UNALIGNED, 19, 14, 12, 1, 10, 3, 12, "8d12+8", 40, 2,
                    new Attack[] { Attacks.GiantConstrictorSnakeBite, Attacks.GiantConstrictorSnakeConstrict }, new Attack[] { }, Size.HUGE
                );
                giantConstrictorSnake.AddProficiency(Proficiency.PERCEPTION);
                return giantConstrictorSnake;
            }
        }

        /* TODO */
        public static Monster GiantCrab {
            get {
                Monster giantCrab = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_CRAB, Alignment.UNALIGNED, 13, 15, 11, 1, 9, 3, 15, "3d8", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.GiantCrabClaw }, new Attack[] { }, Size.MEDIUM
                );
                giantCrab.AddProficiency(Proficiency.STEALTH);
                giantCrab.AddFeat(Feat.AMPHIBIOUS);
                return giantCrab;
            }
        }

        /* TODO */
        public static Monster GiantCrocodile {
            get {
                Monster giantCrocodile = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_CROCODILE, Alignment.UNALIGNED, 21, 9, 17, 2, 10, 7, 14, "9d12+27", 40, 5,
                    new Attack[] { Attacks.GiantCrocodileBite, Attacks.GiantCrocodileTail }, new Attack[] { }, Size.HUGE
                );
                giantCrocodile.AddProficiency(Proficiency.STEALTH);
                giantCrocodile.AddFeat(Feat.HOLD_BREATH_30MIN);
                return giantCrocodile;
            }
        }

        /* TODO */
        public static Monster GiantEagle {
            get {
                Monster giantEagle = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_EAGLE, Alignment.NEUTRAL_GOOD, 16, 17, 13, 8, 14, 10, 13, "4d10+4", 40, 1,
                    new Attack[] { Attacks.GiantEagleBeak, Attacks.GiantEagleTalons }, new Attack[] { }, Size.LARGE
                );
                giantEagle.AddProficiency(Proficiency.PERCEPTION);
                giantEagle.AddFeat(Feat.KEEN_SIGHT);
                return giantEagle;
            }
        }

        /* TODO */
        public static Monster GiantElk {
            get {
                Monster giantElk = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_ELK, Alignment.UNALIGNED, 19, 16, 14, 7, 14, 10, 14, "5d12+10", 40, 2,
                    new Attack[] { Attacks.GiantElkRam, Attacks.GiantElkHooves }, new Attack[] { }, Size.HUGE
                );
                giantElk.AddProficiency(Proficiency.PERCEPTION);
                giantElk.AddFeat(Feat.CHARGE_GIANT_ELK);
                return giantElk;
            }
        }

        /* TODO */
        public static Monster GiantFireBeetle {
            get {
                Monster giantFireBeetle = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_FIRE_BEETLE, Alignment.UNALIGNED, 8, 10, 12, 1, 7, 3, 13, "1d6+1", 40, 0,
                    new Attack[] { Attacks.GiantFireBeetleBite }, new Attack[] { }, Size.SMALL
                );
                giantFireBeetle.AddFeat(Feat.ILLUMINATION_10FT);
                return giantFireBeetle;
            }
        }

        /* TODO */
        public static Monster GiantFrog {
            get {
                Monster giantFrog = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_FROG, Alignment.UNALIGNED, 12, 13, 11, 2, 10, 3, 11, "4d8", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GiantFrogBite }, new Attack[] { }, Size.MEDIUM
                );
                giantFrog.AddProficiency(Proficiency.PERCEPTION);
                giantFrog.AddProficiency(Proficiency.STEALTH);
                giantFrog.AddFeat(Feat.AMPHIBIOUS);
                giantFrog.AddFeat(Feat.STANDING_LEAP_FROG_20FT);
                return giantFrog;
            }
        }

        /* TODO */
        public static Monster GiantGoat {
            get {
                Monster giantGoat = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_GOAT, Alignment.UNALIGNED, 17, 11, 12, 3, 12, 6, 11, "3d10+3", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.GiantGoatRam }, new Attack[] { }, Size.LARGE
                );
                giantGoat.AddFeat(Feat.CHARGE_GOAT);
                giantGoat.AddFeat(Feat.SURE_FOOTED);
                return giantGoat;
            }
        }

        /* TODO */
        public static Monster GiantHyena {
            get {
                Monster giantHyena = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_HYENA, Alignment.UNALIGNED, 16, 14, 14, 2, 12, 7, 12, "6d10+12", 40, 1,
                    new Attack[] { Attacks.GiantHyenaBite }, new Attack[] { }, Size.LARGE
                );
                giantHyena.AddProficiency(Proficiency.PERCEPTION);
                giantHyena.AddFeat(Feat.RAMPAGE);
                return giantHyena;
            }
        }

        /* TODO */
        public static Monster GiantLizard {
            get {
                Monster giantLizard = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_LIZARD, Alignment.UNALIGNED, 15, 12, 13, 2, 10, 5, 12, "3d10+3", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GiantLizardBite }, new Attack[] { }, Size.LARGE
                );
                return giantLizard;
            }
        }

        /* TODO */
        public static Monster GiantOctopus {
            get {
                Monster giantOctopus = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_OCTOPUS, Alignment.UNALIGNED, 17, 13, 13, 4, 10, 4, 11, "8d10+8", 40, 1,
                    new Attack[] { Attacks.GiantOctopusTentacles }, new Attack[] { }, Size.LARGE
                );
                giantOctopus.AddProficiency(Proficiency.PERCEPTION);
                giantOctopus.AddProficiency(Proficiency.STEALTH);
                giantOctopus.AddFeat(Feat.HOLD_BREATH_1HOUR);
                giantOctopus.AddFeat(Feat.UNDERWATER_CAMOUFLAGE);
                giantOctopus.AddFeat(Feat.WATER_BREATHING);
                return giantOctopus;
            }
        }

        /* TODO */
        public static Monster GiantOwl {
            get {
                Monster giantOwl = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_OWL, Alignment.NEUTRAL, 13, 15, 12, 8, 13, 10, 12, "3d10+3", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GiantOwlTalons }, new Attack[] { }, Size.LARGE
                );
                giantOwl.AddProficiency(Proficiency.PERCEPTION);
                giantOwl.AddProficiency(Proficiency.STEALTH);
                giantOwl.AddFeat(Feat.FLYBY);
                giantOwl.AddFeat(Feat.KEEN_HEARING_AND_SIGHT);
                return giantOwl;
            }
        }

        /* TODO */
        public static Monster GiantPoisonousSnake {
            get {
                Monster giantPoisonousSnake = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_POISONOUS_SNAKE, Alignment.UNALIGNED, 10, 18, 13, 2, 10, 3, 14, "2d8+2", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GiantPoisonousSnakeBite }, new Attack[] { }, Size.MEDIUM
                );
                giantPoisonousSnake.AddProficiency(Proficiency.PERCEPTION);
                return giantPoisonousSnake;
            }
        }

        /* TODO */
        public static Monster GiantRat {
            get {
                Monster giantRat = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_RAT, Alignment.UNALIGNED, 7, 15, 11, 2, 10, 4, 12, "2d6", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.GiantRatBite }, new Attack[] { }, Size.SMALL
                );
                giantRat.AddFeat(Feat.KEEN_SMELL);
                giantRat.AddFeat(Feat.PACK_TACTICS);
                return giantRat;
            }
        }

        /* TODO */
        public static Monster GiantRatDiseased {
            get {
                Monster giantRatDiseased = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_RAT__DISEASED, Alignment.UNALIGNED, 7, 15, 11, 2, 10, 4, 12, "2d6", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.GiantRatDiseasedBite }, new Attack[] { }, Size.SMALL
                );
                giantRatDiseased.AddFeat(Feat.KEEN_SMELL);
                giantRatDiseased.AddFeat(Feat.PACK_TACTICS);
                return giantRatDiseased;
            }
        }

        /* TODO */
        public static Monster GiantScorpion {
            get {
                Monster giantScorpion = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_SCORPION, Alignment.UNALIGNED, 15, 13, 15, 1, 9, 3, 15, "7d10+14", 40, 3,
                    new Attack[] { Attacks.GiantScorpionClaw, Attacks.GiantScorpionSting }, new Attack[] { }, Size.LARGE
                );
                return giantScorpion;
            }
        }

        /* TODO */
        public static Monster GiantSeaHorse {
            get {
                Monster giantSeaHorse = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_SEA_HORSE, Alignment.UNALIGNED, 12, 15, 11, 2, 12, 5, 13, "3d10", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.GiantSeaHorseRam }, new Attack[] { }, Size.LARGE
                );
                giantSeaHorse.AddFeat(Feat.CHARGE_GIANT_SEA_HORSE);
                giantSeaHorse.AddFeat(Feat.WATER_BREATHING);
                return giantSeaHorse;
            }
        }

        /* TODO */
        public static Monster GiantShark {
            get {
                Monster giantShark = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_SHARK, Alignment.UNALIGNED, 23, 11, 21, 1, 10, 5, 13, "11d12+55", 40, 5,
                    new Attack[] { Attacks.GiantSharkBite }, new Attack[] { }, Size.HUGE
                );
                giantShark.AddProficiency(Proficiency.PERCEPTION);
                giantShark.AddFeat(Feat.BLOOD_FRENZY);
                giantShark.AddFeat(Feat.WATER_BREATHING);
                return giantShark;
            }
        }

        /* TODO */
        public static Monster GiantSpider {
            get {
                Monster giantSpider = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_SPIDER, Alignment.UNALIGNED, 14, 16, 12, 2, 11, 4, 14, "4d10+4", 40, 1,
                    new Attack[] { Attacks.GiantSpiderBite }, new Attack[] { Attacks.GiantSpiderWeb }, Size.LARGE
                );
                giantSpider.AddProficiency(Proficiency.STEALTH);
                giantSpider.AddFeat(Feat.SPIDER_CLIMB);
                giantSpider.AddFeat(Feat.WEB_SENSE);
                giantSpider.AddFeat(Feat.WEB_WALKER);
                return giantSpider;
            }
        }

        /* TODO */
        public static Monster GiantToad {
            get {
                Monster giantToad = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_TOAD, Alignment.UNALIGNED, 15, 13, 13, 2, 10, 3, 11, "6d10+6", 40, 1,
                    new Attack[] { Attacks.GiantToadBite }, new Attack[] { }, Size.LARGE
                );
                giantToad.AddFeat(Feat.AMPHIBIOUS);
                giantToad.AddFeat(Feat.STANDING_LEAP_TOAD);
                return giantToad;
            }
        }

        /* TODO */
        public static Monster GiantVulture {
            get {
                Monster giantVulture = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_VULTURE, Alignment.NEUTRAL_EVIL, 15, 10, 15, 6, 12, 7, 10, "3d10+6", 40, 1,
                    new Attack[] { Attacks.GiantVultureBeak, Attacks.GiantVultureTalons }, new Attack[] { }, Size.LARGE
                );
                giantVulture.AddProficiency(Proficiency.PERCEPTION);
                giantVulture.AddFeat(Feat.KEEN_SIGHT_AND_SMELL);
                giantVulture.AddFeat(Feat.PACK_TACTICS);
                return giantVulture;
            }
        }

        /* TODO */
        public static Monster GiantWasp {
            get {
                Monster giantWasp = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_WASP, Alignment.UNALIGNED, 10, 14, 10, 1, 10, 3, 12, "3d8", 40, ChallengeRating.HALF,
                    new Attack[] { }, new Attack[] { }, Size.MEDIUM
                );
                return giantWasp;
            }
        }

        /* TODO */
        public static Monster GiantWeasel {
            get {
                Monster giantWeasel = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_WEASEL, Alignment.UNALIGNED, 11, 16, 10, 4, 12, 5, 13, "2d8", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.GiantWeaselBite }, new Attack[] { }, Size.MEDIUM
                );
                giantWeasel.AddProficiency(Proficiency.PERCEPTION);
                giantWeasel.AddProficiency(Proficiency.STEALTH);
                giantWeasel.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                return giantWeasel;
            }
        }

        /* TODO */
        public static Monster GiantWolfSpider {
            get {
                Monster giantWolfSpider = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GIANT_WOLF_SPIDER, Alignment.UNALIGNED, 12, 16, 13, 3, 12, 4, 13, "2d8+2", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GiantWolfSpiderBite }, new Attack[] { }, Size.MEDIUM
                );
                giantWolfSpider.AddProficiency(Proficiency.PERCEPTION);
                giantWolfSpider.AddProficiency(Proficiency.STEALTH);
                giantWolfSpider.AddFeat(Feat.SPIDER_CLIMB);
                giantWolfSpider.AddFeat(Feat.WEB_SENSE);
                giantWolfSpider.AddFeat(Feat.WEB_WALKER);
                return giantWolfSpider;
            }
        }

        /* TODO */
        public static Monster GibberingMouther {
            get {
                Monster gibberingMouther = new Monster(
                    Monsters.Type.ABERRATION, Monsters.ID.GIBBERING_MOUTHER, Alignment.NEUTRAL, 10, 8, 16, 3, 10, 6, 9, "9d8+27", 40, 2,
                    new Attack[] { Attacks.GibberingMoutherBites }, new Attack[] { }, Size.MEDIUM
                );
                gibberingMouther.AddEffect(Effect.IMMUNITY_PRONE);
                gibberingMouther.AddFeat(Feat.ABERRANT_GROUND);
                gibberingMouther.AddFeat(Feat.GIBBERING);
                return gibberingMouther;
            }
        }

        /* TODO */
        public static Monster Glabrezu {
            get {
                Monster glabrezu = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.GLABREZU, Alignment.CHAOTIC_EVIL, 20, 15, 21, 19, 17, 16, 17, "15d10+75", 40, 9,
                    new Attack[] { Attacks.GlabrezuPincer, Attacks.GlabrezuFist }, new Attack[] { }, Size.LARGE
                );
                glabrezu.AddProficiency(Proficiency.STRENGTH);
                glabrezu.AddProficiency(Proficiency.CONSTITUTION);
                glabrezu.AddProficiency(Proficiency.WISDOM);
                glabrezu.AddProficiency(Proficiency.CHARISMA);
                glabrezu.AddEffect(Effect.RESISTANCE_COLD);
                glabrezu.AddEffect(Effect.RESISTANCE_FIRE);
                glabrezu.AddEffect(Effect.RESISTANCE_LIGHTNING);
                glabrezu.AddEffect(Effect.RESISTANCE_NONMAGIC);
                glabrezu.AddEffect(Effect.IMMUNITY_POISON);
                glabrezu.AddEffect(Effect.IMMUNITY_POISONED);
                glabrezu.AddFeat(Feat.INNATE_SPELLCASTING_GLABREZU);
                glabrezu.AddFeat(Feat.MAGIC_RESISTANCE);
                return glabrezu;
            }
        }

        /* TODO */
        public static Monster Gladiator {
            get {
                Monster gladiator = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.GLADIATOR, Alignment.UNALIGNED, 18, 15, 16, 10, 12, 15, 16, "15d8+45", 40, 5,
                    new Attack[] { Attacks.GladiatorSpear, Attacks.GladiatorShieldBash }, new Attack[] { }, Size.MEDIUM
                );
                gladiator.AddProficiency(Proficiency.STRENGTH);
                gladiator.AddProficiency(Proficiency.DEXTERITY);
                gladiator.AddProficiency(Proficiency.CONSTITUTION);
                gladiator.AddProficiency(Proficiency.ATHLETICS);
                gladiator.AddProficiency(Proficiency.INTIMIDATION);
                gladiator.AddFeat(Feat.BRAVE);
                gladiator.AddFeat(Feat.BRUTE);
                return gladiator;
            }
        }

        /* TODO */
        public static Monster Gnoll {
            get {
                Monster gnoll = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.GNOLL, Alignment.CHAOTIC_EVIL, 14, 12, 11, 6, 10, 7, 15, "5d8", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.GnollBite, Attacks.GnollSpearMelee }, new Attack[] { Attacks.GnollSpearRanged, Attacks.GnollLongbow }, Size.MEDIUM
                );
                gnoll.AddFeat(Feat.RAMPAGE);
                return gnoll;
            }
        }

        /* TODO */
        public static Monster Goat {
            get {
                Monster goat = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.GOAT, Alignment.UNALIGNED, 12, 10, 11, 2, 10, 5, 10, "1d8", 40, 0,
                    new Attack[] { Attacks.GoatRam }, new Attack[] { }, Size.MEDIUM
                );
                goat.AddFeat(Feat.CHARGE_GOAT);
                goat.AddFeat(Feat.SURE_FOOTED);
                return goat;
            }
        }

        /* TODO */
        public static Monster Goblin {
            get {
                Monster goblin = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.GOBLIN, Alignment.NEUTRAL_EVIL, 8, 14, 10, 10, 8, 8, 15, "2d6", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GoblinScimitar }, new Attack[] { }, Size.SMALL
                );
                goblin.AddProficiency(Proficiency.STEALTH);
                goblin.AddFeat(Feat.NIMBLE_ESCAPE);
                return goblin;
            }
        }

        /* TODO */
        public static Monster GoldDragonWyrmling {
            get {
                Monster goldDragonWyrmling = new Monster(
                    Monsters.Type.DRAGON, Monsters.ID.GOLD_DRAGON_WYRMLING, Alignment.LAWFUL_GOOD, 19, 14, 17, 14, 11, 16, 17, "8d8+24", 40, 3,
                    new Attack[] { Attacks.GoldDragonWyrmlingBite }, new Attack[] { }, Size.MEDIUM
                );
                goldDragonWyrmling.AddProficiency(Proficiency.DEXTERITY);
                goldDragonWyrmling.AddProficiency(Proficiency.CONSTITUTION);
                goldDragonWyrmling.AddProficiency(Proficiency.WISDOM);
                goldDragonWyrmling.AddProficiency(Proficiency.CHARISMA);
                goldDragonWyrmling.AddProficiency(Proficiency.PERCEPTION);
                goldDragonWyrmling.AddProficiency(Proficiency.STEALTH);
                goldDragonWyrmling.AddEffect(Effect.IMMUNITY_FIRE);
                goldDragonWyrmling.AddFeat(Feat.AMPHIBIOUS);
                return goldDragonWyrmling;
            }
        }

        /* TODO */
        public static Monster Gorgon {
            get {
                Monster gorgon = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.GORGON, Alignment.UNALIGNED, 20, 11, 18, 2, 12, 7, 19, "12d10+48", 40, 5,
                    new Attack[] { Attacks.GorgonGore, Attacks.GorgonHooves }, new Attack[] { }, Size.LARGE
                );
                gorgon.AddProficiency(Proficiency.PERCEPTION);
                gorgon.AddEffect(Effect.IMMUNITY_PETRIFIED);
                gorgon.AddFeat(Feat.TRAMPLING_CHARGE_GORGON);
                return gorgon;
            }
        }

        /* TODO */
        public static Monster GrayOoze {
            get {
                Monster grayOoze = new Monster(
                    Monsters.Type.OOZE, Monsters.ID.GRAY_OOZE, Alignment.UNALIGNED, 12, 6, 16, 1, 6, 2, 8, "3d8+9", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.GrayOozePseudopod }, new Attack[] { }, Size.MEDIUM
                );
                grayOoze.AddProficiency(Proficiency.STEALTH);
                grayOoze.AddEffect(Effect.RESISTANCE_ACID);
                grayOoze.AddEffect(Effect.RESISTANCE_COLD);
                grayOoze.AddEffect(Effect.RESISTANCE_FIRE);
                grayOoze.AddEffect(Effect.IMMUNITY_BLINDED);
                grayOoze.AddEffect(Effect.IMMUNITY_CHARMED);
                grayOoze.AddEffect(Effect.IMMUNITY_DEAFENED);
                grayOoze.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                grayOoze.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                grayOoze.AddEffect(Effect.IMMUNITY_PRONE);
                grayOoze.AddFeat(Feat.AMORPHOUS);
                grayOoze.AddFeat(Feat.CORRODE_METAL);
                grayOoze.AddFeat(Feat.FALSE_APPEARANCE);
                return grayOoze;
            }
        }

        /* TODO */
        public static Monster GreenDragonWyrmling {
            get {
                Monster greenDragonWyrmling = new Monster(
                    Monsters.Type.DRAGON, Monsters.ID.GREEN_DRAGON_WYRMLING, Alignment.LAWFUL_EVIL, 15, 12, 13, 14, 11, 13, 17, "7d8+7", 40, 2,
                    new Attack[] { Attacks.GreenDragonWyrmlingBite }, new Attack[] { }, Size.MEDIUM
                );
                greenDragonWyrmling.AddProficiency(Proficiency.DEXTERITY);
                greenDragonWyrmling.AddProficiency(Proficiency.CONSTITUTION);
                greenDragonWyrmling.AddProficiency(Proficiency.WISDOM);
                greenDragonWyrmling.AddProficiency(Proficiency.CHARISMA);
                greenDragonWyrmling.AddProficiency(Proficiency.PERCEPTION);
                greenDragonWyrmling.AddProficiency(Proficiency.STEALTH);
                greenDragonWyrmling.AddEffect(Effect.IMMUNITY_POISON);
                greenDragonWyrmling.AddEffect(Effect.IMMUNITY_POISONED);
                greenDragonWyrmling.AddFeat(Feat.AMPHIBIOUS);
                return greenDragonWyrmling;
            }
        }

        /* TODO */
        public static Monster GreenHag {
            get {
                Monster greenHag = new Monster(
                    Monsters.Type.FEY, Monsters.ID.GREEN_HAG, Alignment.NEUTRAL_EVIL, 18, 12, 16, 13, 14, 14, 17, "11d8+33", 40, 3,
                    new Attack[] { Attacks.GreenHagClaws }, new Attack[] { }, Size.MEDIUM
                );
                greenHag.AddProficiency(Proficiency.ARCANA);
                greenHag.AddProficiency(Proficiency.DECEPTION);
                greenHag.AddProficiency(Proficiency.PERCEPTION);
                greenHag.AddProficiency(Proficiency.STEALTH);
                greenHag.AddFeat(Feat.AMPHIBIOUS);
                greenHag.AddFeat(Feat.INNATE_SPELLCASTING_GREEN_HAG);
                greenHag.AddFeat(Feat.MIMICRY_HAG);
                return greenHag;
            }
        }

        /* TODO */
        public static Monster Grick {
            get {
                Monster grick = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.GRICK, Alignment.NEUTRAL, 14, 14, 11, 3, 14, 5, 14, "6d8", 40, 2,
                    new Attack[] { Attacks.GrickTentacles, Attacks.GrickBeak }, new Attack[] { }, Size.MEDIUM
                );
                grick.AddEffect(Effect.RESISTANCE_NONMAGIC);
                grick.AddFeat(Feat.STONE_CAMOUFLAGE);
                return grick;
            }
        }

        /* TODO */
        public static Monster Griffon {
            get {
                Monster griffon = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.GRIFFON, Alignment.UNALIGNED, 18, 15, 16, 2, 13, 8, 12, "7d10+21", 40, 2,
                    new Attack[] { Attacks.GriffonBeak, Attacks.GriffonClaws }, new Attack[] { }, Size.LARGE
                );
                griffon.AddProficiency(Proficiency.PERCEPTION);
                griffon.AddFeat(Feat.KEEN_SIGHT);
                return griffon;
            }
        }

        /* TODO */
        public static Monster Grimlock {
            get {
                Monster grimlock = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.GRIMLOCK, Alignment.NEUTRAL_EVIL, 16, 12, 12, 9, 8, 6, 11, "2d8+2", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.GrimlockSpikedBoneClub }, new Attack[] { }, Size.MEDIUM
                );
                grimlock.AddProficiency(Proficiency.ATHLETICS);
                grimlock.AddProficiency(Proficiency.PERCEPTION);
                grimlock.AddProficiency(Proficiency.STEALTH);
                grimlock.AddEffect(Effect.IMMUNITY_BLINDED);
                grimlock.AddFeat(Feat.BLIND_SENSES);
                grimlock.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                grimlock.AddFeat(Feat.STONE_CAMOUFLAGE);
                return grimlock;
            }
        }

        /* TODO */
        public static Monster Guard {
            get {
                Monster guard = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.GUARD, Alignment.UNALIGNED, 13, 12, 12, 10, 11, 10, 16, "2d8+2", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.GuardSpearMelee }, new Attack[] { Attacks.GuardSpearRanged }, Size.MEDIUM
                );
                guard.AddProficiency(Proficiency.PERCEPTION);
                return guard;
            }
        }

        /* TODO */
        public static Monster GuardianNaga {
            get {
                Monster guardianNaga = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.GUARDIAN_NAGA, Alignment.LAWFUL_GOOD, 19, 18, 16, 16, 19, 18, 18, "15d10+45", 40, 10,
                    new Attack[] { Attacks.GuardianNagaBite }, new Attack[] { }, Size.LARGE
                );
                guardianNaga.AddProficiency(Proficiency.DEXTERITY);
                guardianNaga.AddProficiency(Proficiency.CONSTITUTION);
                guardianNaga.AddProficiency(Proficiency.INTELLIGENCE);
                guardianNaga.AddProficiency(Proficiency.WISDOM);
                guardianNaga.AddProficiency(Proficiency.CHARISMA);
                guardianNaga.AddEffect(Effect.IMMUNITY_POISON);
                guardianNaga.AddEffect(Effect.IMMUNITY_CHARMED);
                guardianNaga.AddEffect(Effect.IMMUNITY_POISONED);
                guardianNaga.AddFeat(Feat.REJUVENATION_NAGA);
                guardianNaga.AddFeat(Feat.SPELLCASTING_NAGA_10);
                return guardianNaga;
            }
        }

        /* TODO */
        public static Monster Gynosphinx {
            get {
                Monster gynosphinx = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.GYNOSPHINX, Alignment.LAWFUL_NEUTRAL, 18, 15, 16, 18, 18, 18, 17, "16d10+48", 40, 11,
                    new Attack[] { Attacks.GynosphinxClaw }, new Attack[] { }, Size.LARGE
                );
                gynosphinx.AddProficiency(Proficiency.ARCANA);
                gynosphinx.AddProficiency(Proficiency.HISTORY);
                gynosphinx.AddProficiency(Proficiency.PERCEPTION);
                gynosphinx.AddProficiency(Proficiency.RELIGION);
                gynosphinx.AddEffect(Effect.RESISTANCE_NONMAGIC);
                gynosphinx.AddEffect(Effect.IMMUNITY_PSYCHIC);
                gynosphinx.AddEffect(Effect.IMMUNITY_CHARMED);
                gynosphinx.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                gynosphinx.AddFeat(Feat.INSCRUTABLE);
                gynosphinx.AddFeat(Feat.MAGIC_WEAPONS);
                gynosphinx.AddFeat(Feat.SPELLCASTING_GYNOSPHINX);
                return gynosphinx;
            }
        }

    }
}