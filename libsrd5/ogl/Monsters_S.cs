namespace srd5 {
    public partial struct Attacks {
        public static Attack SaberToothedTigerBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "1d10+5"), 5);
            }
        }
        public static Attack SaberToothedTigerClaw {
            get {
                return new Attack("Claw", 6, new Damage(DamageType.SLASHING, "2d6+5"), 5);
            }
        }
        public static Attack SahuaginSpearMelee {
            get {
                return new Attack("Spear", 3, new Damage(DamageType.PIERCING, "1d8+1"), 5);
            }
        }
        public static Attack SahuaginSpearRanged {
            get {
                return new Attack("Spear", 3, new Damage(DamageType.PIERCING, "1d8+1"), 20, 60);
            }
        }
        public static Attack SahuaginBite {
            get {
                return new Attack("Bite", 3, new Damage(DamageType.PIERCING, "1d4+1"), 5);
            }
        }
        public static Attack SahuaginClaws {
            get {
                return new Attack("Claws", 3, new Damage(DamageType.SLASHING, "1d4+1"), 5);
            }
        }
        public static Attack SalamanderSpear {
            get {
                return new Attack("Spear", 7, new Damage(DamageType.PIERCING, "2d8+4"), 5, new Damage(DamageType.FIRE, "1d6"));
            }
        }
        public static readonly AttackEffect SalamanderTailEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 14, Size.HUGE, true, SalamanderTail, 1);
        };
        public static Attack SalamanderTail {
            get {
                return new Attack("Tail", 7, new Damage(DamageType.BLUDGEONING, "2d6+4"), 5, new Damage(DamageType.FIRE, "2d6"), SalamanderTailEffect);
            }
        }
        public static Attack SatyrRam {
            get {
                return new Attack("Ram", 3, new Damage(DamageType.BLUDGEONING, "2d4+1"), 5);
            }
        }
        public static Attack SatyrShortsword {
            get {
                return new Attack("Shortsword", 5, new Damage(DamageType.PIERCING, "1d6+3"), 5);
            }
        }
        public static Attack SatyrShortbow {
            get {
                return new Attack("Shortbow", 5, new Damage(DamageType.PIERCING, "1d6+3"), 5, 80, 320);
            }
        }
        public static readonly AttackEffect ScorpionStingEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, ScorpionSting, "1d8", 9);
        };
        public static Attack ScorpionSting {
            get {
                return new Attack("Sting", 2, new Damage(DamageType.PIERCING, 1), 5, null, ScorpionStingEffect);
            }
        }
        public static readonly AttackEffect ScoutLongbowEffect = delegate (Combattant attacker, Combattant target) {
        };
        public static Attack ScoutLongbow {
            get {
                return new Attack("Longbow", 4, new Damage(DamageType.PIERCING, "1d8+2"), 150, 600);
            }
        }
        public static Attack ScoutShortsword {
            get {
                return new Attack("Shortsword", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack SeaHagClaws {
            get {
                return new Attack("Claws", 5, new Damage(DamageType.SLASHING, "2d6+3"), 5);
            }
        }
        public static readonly AttackEffect ShadowStrengthDrainEffect = delegate (Combattant attacker, Combattant target) {
            // TODO: the target's Strength score is reduced by 1d4. The target dies if this reduces its Strength to 0. 
            // Otherwise, the reduction lasts until the target finishes a short or long rest.
            // If a non-evil humanoid dies from this attack, a new shadow rises from the corpse 1d4 hours later.
        };
        public static Attack ShadowStrengthDrain {
            get {
                return new Attack("Strength Drain", 4, new Damage(DamageType.NECROTIC, "2d6+2"), 5, null, ShadowStrengthDrainEffect);
            }
        }
        public static Attack ShamblingMoundSlam {
            get {
                return new Attack("Slam", 7, new Damage(DamageType.BLUDGEONING, "2d8+4"), 5);
            }
        }
        public static Attack ShieldGuardianFist {
            get {
                return new Attack("Fist", 7, new Damage(DamageType.BLUDGEONING, "2d6+4"), 5);
            }
        }
        public static Attack SilverDragonWyrmlingBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "1d10+4"), 5);
            }
        }
        public static Attack SkeletonShortsword {
            get {
                return new Attack("Shortsword", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack SkeletonShortbow {
            get {
                return new Attack("Shortbow", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5, 80, 320);
            }
        }
        public static readonly AttackEffect SolarSlayingLongbowEffect = delegate (Combattant attacker, Combattant target) {
            if (target.HitPoints > 190) return;
            bool success = target.DC(SolarSlayingLongbow, 15, AbilityType.CONSTITUTION);
            if (success) return;
            target.Die();
        };
        public static Attack SolarSlayingLongbow {
            get {
                return new Attack("Slaying Longbow", 13, new Damage(DamageType.PIERCING, "2d8+6"), 150, 600, new Damage(DamageType.RADIANT, "6d8"), SolarSlayingLongbowEffect);
            }
        }
        public static Attack SolarGreatsword {
            get {
                return new Attack("Greatsword", 15, new Damage(DamageType.SLASHING, "4d6+8"), 5, new Damage(DamageType.RADIANT, "6d8"));
            }
        }
        public static readonly AttackEffect SpecterLifeDrainEffect = delegate (Combattant attacker, Combattant target) {
            int damage = target.TakeDamage(attacker, DamageType.NECROTIC, "3d6");
            target.AddHitPointMaximumModifiers(new HitPointMaxiumModifier(-damage, HitPointMaxiumModifier.RemovedByEffect.LONG_REST));
            if (target.HitPointsMax == 0) {
                target.Die();
            }
        };
        public static Attack SpecterLifeDrain {
            get {
                return new Attack("Life Drain", 4, new Damage(DamageType.NECROTIC, 0), 5, null, SpecterLifeDrainEffect);
            }
        }
        public static readonly AttackEffect SpiderBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, SpiderBite, "1d4", 9, AbilityType.CONSTITUTION, true);
        };
        public static Attack SpiderBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, 1), 5, null, SpiderBiteEffect);
            }
        }
        public static readonly AttackEffect SpiritNagaBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, SpiritNagaBite, "7d8", 13);
        };
        public static Attack SpiritNagaBite {
            get {
                return new Attack("Bite", 7, new Damage(DamageType.PIERCING, "1d6+4"), 5, null, SpiritNagaBiteEffect);
            }
        }
        public static readonly AttackEffect SpriteShortbowEffect = delegate (Combattant attacker, Combattant target) {
            if (target.IsImmune(DamageType.POISON)) return;
            if (target.DC(SpriteShortbow, 10, AbilityType.CONSTITUTION, out int roll)) return;
            target.AddEffect(Effect.SPRITE_POISON);
            if (roll <= 5) {
                target.AddEffect(Effect.SPRITE_POISON_UNCONCIOUS);
            }
        };
        public static Attack SpriteShortbow {
            get {
                return new Attack("Shortbow", 6, new Damage(DamageType.PIERCING, 1), 5, null, SpriteShortbowEffect);
            }
        }
        public static Attack SpriteLongsword {
            get {
                return new Attack("Longsword", 2, new Damage(DamageType.SLASHING, "1d1"), 5);
            }
        }
        public static Attack SpyShortsword {
            get {
                return new Attack("Shortsword", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack SpyHandCrossbow {
            get {
                return new Attack("Hand Crossbow", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5, 30, 120);
            }
        }
        public static Attack SteamMephitClaws {
            get {
                return new Attack("Claws", 2, new Damage(DamageType.SLASHING, "1d4"), 5, new Damage(DamageType.FIRE, "1d4"));
            }
        }
        public static readonly AttackEffect StirgeBloodDrainEffect = delegate (Combattant attacker, Combattant target) {
            if (target is Monster monster && (monster.Type == Monsters.Type.UNDEAD || monster.Type == Monsters.Type.CONSTRUCT)) return;
            // TODO: the stirge attaches to the target. While attached, the stirge doesn't attack.
            target.AddEffect(Effect.STIRGE_BLOOD_DRAIN_EFFECT);
            attacker.AddEffect(Effect.STIRGE_BLOOD_DRAINING);
            int damage = 0;
            target.AddStartOfTurnEvent(delegate () {
                if (!target.HasEffect(Effect.STIRGE_BLOOD_DRAIN_EFFECT)) return true;
                int delta = Die.Roll("1d4+3");
                damage += delta;
                target.TakeDamage(attacker, DamageType.TRUE_DAMAGE, "1d4+3");
                return damage >= 10;
            });

            // A creature, including the target, can use its action to detach the stirge.
        };
        public static Attack StirgeBloodDrain {
            get {
                return new Attack("Blood Drain", 5, new Damage(DamageType.PIERCING, "1d4+3"), 5, null, StirgeBloodDrainEffect);
            }
        }
        public static readonly AttackEffect StoneGiantRockEffect = delegate (Combattant attacker, Combattant target) {
            if (target.HasEffect(Effect.IMMUNITY_PRONE)) return;
            if (target.DC(StoneGiantRock, 17, AbilityType.STRENGTH)) return;
            target.AddCondition(ConditionType.PRONE);
        };
        public static Attack StoneGiantRock {
            get {
                return new Attack("Rock", 9, new Damage(DamageType.BLUDGEONING, "4d10+6"), 5, null, StoneGiantRockEffect);
            }
        }
        public static Attack StoneGiantGreatclub {
            get {
                return new Attack("Greatclub", 9, new Damage(DamageType.BLUDGEONING, "3d8+6"), 15);
            }
        }
        public static Attack StoneGolemSlam {
            get {
                return new Attack("Slam", 10, new Damage(DamageType.BLUDGEONING, "3d8+6"), 5);
            }
        }
        public static Attack StormGiantGreatsword {
            get {
                return new Attack("Greatsword", 14, new Damage(DamageType.SLASHING, "6d6+9"), 10);
            }
        }
        public static Attack StormGiantRock {
            get {
                return new Attack("Rock", 14, new Damage(DamageType.BLUDGEONING, "4d12+9"), 5, 60, 240);
            }
        }
        public static readonly AttackEffect SuccubusDrainingKissEffect = delegate (Combattant attacker, Combattant target) {
            bool success = target.DC(SuccubusDrainingKiss, 15, AbilityType.CONSTITUTION);
            int damage = Die.Roll("5d10+5");
            if (success) damage /= 2;
            target.TakeDamage(attacker, DamageType.PSYCHIC, damage);
            target.AddHitPointMaximumModifiers(new HitPointMaxiumModifier(-damage, HitPointMaxiumModifier.RemovedByEffect.LONG_REST));
            if (target.HitPointsMax <= 0) {
                target.Die();
            }
        };
        public static Attack SuccubusDrainingKiss {
            get {
                return new Attack("Draining Kiss", 0, new Damage(DamageType.TRUE_DAMAGE, 0), 5, null, SuccubusDrainingKissEffect);
            }
        }
        public static Attack SuccubusClaw {
            get {
                return new Attack("Claw)", 5, new Damage(DamageType.SLASHING, "1d6+3"), 5);
            }
        }
        public static readonly AttackEffect SwarmOfBatsBitesEffect = delegate (Combattant attacker, Combattant target) {
            if (attacker.HitPoints <= attacker.HitPointsMax / 2) return;
            target.TakeDamage(attacker, DamageType.PIERCING, "1d4");
        };
        public static Attack SwarmOfBatsBites {
            get {
                return new Attack("Bites", 4, new Damage(DamageType.PIERCING, "1d4"), 5, null, SwarmOfBatsBitesEffect);
            }
        }
        public static readonly AttackEffect SwarmOfBeetlesBitesEffect = delegate (Combattant attacker, Combattant target) {
            if (attacker.HitPoints <= attacker.HitPointsMax / 2) return;
            target.TakeDamage(attacker, DamageType.PIERCING, "2d4");
        };
        public static Attack SwarmOfBeetlesBites {
            get {
                return new Attack("Bites", 3, new Damage(DamageType.PIERCING, "2d4"), 5, null, SwarmOfBeetlesBitesEffect);
            }
        }
        public static readonly AttackEffect SwarmOfCentipedesBitesEffect = delegate (Combattant attacker, Combattant target) {
            if (attacker.HitPoints <= attacker.HitPointsMax / 2) return;
            target.TakeDamage(attacker, DamageType.PIERCING, "2d4");
            // TODO: A creature reduced to 0 hit points by a swarm of centipedes is stable but poisoned for 1 hour, 
            // even after regaining hit points, and paralyzed while poisoned in this way.
        };
        public static Attack SwarmOfCentipedesBites {
            get {
                return new Attack("Bites", 3, new Damage(DamageType.PIERCING, "2d4"), 5, null, SwarmOfCentipedesBitesEffect);
            }
        }
        public static readonly AttackEffect SwarmOfInsectsBitesEffect = delegate (Combattant attacker, Combattant target) {
            if (attacker.HitPoints <= attacker.HitPointsMax / 2) return;
            target.TakeDamage(attacker, DamageType.PIERCING, "2d4");
        };
        public static Attack SwarmOfInsectsBites {
            get {
                return new Attack("Bites", 3, new Damage(DamageType.PIERCING, "2d4"), 5, null, SwarmOfInsectsBitesEffect);
            }
        }
        public static readonly AttackEffect SwarmOfPoisonousSnakesBitesEffect = delegate (Combattant attacker, Combattant target) {
            if (attacker.HitPoints <= attacker.HitPointsMax / 2) return;
            target.TakeDamage(attacker, DamageType.PIERCING, "1d6");
            AttackEffects.PoisonEffect(target, SwarmOfPoisonousSnakesBites, "4d6", 10);
        };
        public static Attack SwarmOfPoisonousSnakesBites {
            get {
                return new Attack("Bites", 6, new Damage(DamageType.PIERCING, "1d6"), 5, null, SwarmOfPoisonousSnakesBitesEffect);
            }
        }
        public static readonly AttackEffect SwarmOfQuippersBitesEffect = delegate (Combattant attacker, Combattant target) {
            if (attacker.HitPoints <= attacker.HitPointsMax / 2) return;
            target.TakeDamage(attacker, DamageType.PIERCING, "2d6");
        };
        public static Attack SwarmOfQuippersBites {
            get {
                return new Attack("Bites", 5, new Damage(DamageType.PIERCING, "2d6"), 5, null, SwarmOfQuippersBitesEffect);
            }
        }
        public static readonly AttackEffect SwarmOfRatsBitesEffect = delegate (Combattant attacker, Combattant target) {
            if (attacker.HitPoints <= attacker.HitPointsMax / 2) return;
            target.TakeDamage(attacker, DamageType.PIERCING, "1d6");
        };
        public static Attack SwarmOfRatsBites {
            get {
                return new Attack("Bites", 2, new Damage(DamageType.PIERCING, "1d6"), 5, null, SwarmOfRatsBitesEffect);
            }
        }
        public static readonly AttackEffect SwarmOfRavensBeaksEffect = delegate (Combattant attacker, Combattant target) {
            if (attacker.HitPoints <= attacker.HitPointsMax / 2) return;
            target.TakeDamage(attacker, DamageType.PIERCING, "1d6");
        };
        public static Attack SwarmOfRavensBeaks {
            get {
                return new Attack("Beaks", 4, new Damage(DamageType.PIERCING, "1d6"), 5, null, SwarmOfRavensBeaksEffect);
            }
        }
        public static readonly AttackEffect SwarmOfSpidersBitesEffect = delegate (Combattant attacker, Combattant target) {
            if (attacker.HitPoints <= attacker.HitPointsMax / 2) return;
            target.TakeDamage(attacker, DamageType.PIERCING, "2d4");
        };
        public static Attack SwarmOfSpidersBites {
            get {
                return new Attack("Bites", 3, new Damage(DamageType.PIERCING, "2d4"), 5, null, SwarmOfSpidersBitesEffect);
            }
        }
        public static readonly AttackEffect SwarmOfWaspsBitesEffect = delegate (Combattant attacker, Combattant target) {
            if (attacker.HitPoints <= attacker.HitPointsMax / 2) return;
            target.TakeDamage(attacker, DamageType.PIERCING, "2d4");
        };
        public static Attack SwarmOfWaspsBites {
            get {
                return new Attack("Bites", 3, new Damage(DamageType.PIERCING, "2d4"), 5, null, SwarmOfWaspsBitesEffect);
            }
        }
    }

    public partial struct Monsters {
        /* TODO */
        public static Monster SaberToothedTiger {
            get {
                Monster saberToothedTiger = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.SABER_TOOTHED_TIGER, Alignment.UNALIGNED, 18, 14, 15, 3, 12, 8, 12, "7d10+14", 40, 2,
                    new Attack[] { Attacks.SaberToothedTigerBite, Attacks.SaberToothedTigerClaw
    }, new Attack[] { }, Size.LARGE
                );
                saberToothedTiger.AddProficiency(Proficiency.PERCEPTION);
                saberToothedTiger.AddProficiency(Proficiency.STEALTH);
                saberToothedTiger.AddFeat(Feat.KEEN_SMELL);
                saberToothedTiger.AddFeat(Feat.POUNCE_TIGER_14);
                return saberToothedTiger;
            }
        }

        /* TODO */
        public static Monster Sahuagin {
            get {
                Monster sahuagin = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.SAHUAGIN, Alignment.LAWFUL_EVIL, 13, 11, 12, 12, 13, 9, 12, "4d8+4", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.SahuaginBite, Attacks.SahuaginClaws, Attacks.SahuaginSpearMelee }, new Attack[] { Attacks.SahuaginSpearRanged }, Size.MEDIUM
                );
                sahuagin.AddProficiency(Proficiency.PERCEPTION);
                sahuagin.AddFeat(Feat.BLOOD_FRENZY);
                sahuagin.AddFeat(Feat.LIMITED_AMPHIBIOUSNESS);
                sahuagin.AddFeat(Feat.SHARK_TELEPATHY);
                return sahuagin;
            }
        }

        /* TODO */
        public static Monster Salamander {
            get {
                Monster salamander = new Monster(
                    Monsters.Type.ELEMENTAL, Monsters.ID.SALAMANDER, Alignment.NEUTRAL_EVIL, 18, 14, 15, 11, 10, 12, 15, "12d10+24", 40, 5,
                    new Attack[] { Attacks.SalamanderSpear, Attacks.SalamanderTail }, new Attack[] { }, Size.LARGE
                );
                salamander.AddEffect(Effect.VULNERABILITY_COLD);
                salamander.AddEffect(Effect.RESISTANCE_NONMAGIC);
                salamander.AddEffect(Effect.IMMUNITY_FIRE);
                salamander.AddFeat(Feat.HEATED_BODY_2D6);
                salamander.AddFeat(Feat.HEATED_WEAPONS);
                return salamander;
            }
        }

        /* TODO */
        public static Monster Satyr {
            get {
                Monster satyr = new Monster(
                    Monsters.Type.FEY, Monsters.ID.SATYR, Alignment.CHAOTIC_NEUTRAL, 12, 16, 11, 12, 10, 14, 14, "7d8", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.SatyrRam, Attacks.SatyrShortsword }, new Attack[] { Attacks.SatyrShortbow }, Size.MEDIUM
                );
                satyr.AddProficiency(Proficiency.PERCEPTION);
                satyr.AddProficiency(Proficiency.PERFORMANCE);
                satyr.AddProficiency(Proficiency.STEALTH);
                satyr.AddFeat(Feat.MAGIC_RESISTANCE);
                return satyr;
            }
        }

        /* TODO */
        public static Monster Scorpion {
            get {
                Monster scorpion = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.SCORPION, Alignment.UNALIGNED, 2, 11, 8, 1, 8, 2, 11, "1d4-1", 40, 0,
                    new Attack[] { }, new Attack[] { }, Size.TINY
                );
                return scorpion;
            }
        }

        /* TODO */
        public static Monster Scout {
            get {
                Monster scout = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.SCOUT, Alignment.UNALIGNED, 11, 14, 12, 11, 13, 11, 13, "3d8+3", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.ScoutShortsword }, new Attack[] { Attacks.ScoutLongbow }, Size.MEDIUM
                );
                scout.AddProficiency(Proficiency.NATURE);
                scout.AddProficiency(Proficiency.PERCEPTION);
                scout.AddProficiency(Proficiency.STEALTH);
                scout.AddProficiency(Proficiency.SURVIVAL);
                scout.AddFeat(Feat.KEEN_HEARING_AND_SIGHT);
                return scout;
            }
        }

        /* TODO */
        public static Monster SeaHag {
            get {
                Monster seaHag = new Monster(
                    Monsters.Type.FEY, Monsters.ID.SEA_HAG, Alignment.CHAOTIC_EVIL, 16, 13, 16, 12, 12, 13, 14, "7d8+21", 40, 2,
                    new Attack[] { Attacks.SeaHagClaws }, new Attack[] { }, Size.MEDIUM
                );
                seaHag.AddFeat(Feat.AMPHIBIOUS);
                seaHag.AddFeat(Feat.HORRIFIC_APPEARANCE);
                return seaHag;
            }
        }

        /* TODO */
        public static Monster SeaHorse {
            get {
                Monster seaHorse = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.SEA_HORSE, Alignment.UNALIGNED, 1, 12, 8, 1, 10, 2, 11, "1d4-1", 40, 0,
                    new Attack[] { }, new Attack[] { }, Size.TINY
                );
                seaHorse.AddFeat(Feat.WATER_BREATHING);
                return seaHorse;
            }
        }

        /* TODO */
        public static Monster Shadow {
            get {
                Monster shadow = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.SHADOW, Alignment.CHAOTIC_EVIL, 6, 14, 13, 6, 10, 8, 12, "3d8+3", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.ShadowStrengthDrain }, new Attack[] { }, Size.MEDIUM
                );
                shadow.AddProficiency(Proficiency.STEALTH);
                shadow.AddEffect(Effect.VULNERABILITY_RADIANT);
                shadow.AddEffect(Effect.RESISTANCE_ACID);
                shadow.AddEffect(Effect.RESISTANCE_COLD);
                shadow.AddEffect(Effect.RESISTANCE_FIRE);
                shadow.AddEffect(Effect.RESISTANCE_LIGHTNING);
                shadow.AddEffect(Effect.RESISTANCE_THUNDER);
                shadow.AddEffect(Effect.RESISTANCE_NONMAGIC);
                shadow.AddEffect(Effect.IMMUNITY_NECROTIC);
                shadow.AddEffect(Effect.IMMUNITY_POISON);
                shadow.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                shadow.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                shadow.AddEffect(Effect.IMMUNITY_GRAPPLED);
                shadow.AddEffect(Effect.IMMUNITY_PARALYZED);
                shadow.AddEffect(Effect.IMMUNITY_PETRIFIED);
                shadow.AddEffect(Effect.IMMUNITY_POISONED);
                shadow.AddEffect(Effect.IMMUNITY_PRONE);
                shadow.AddEffect(Effect.IMMUNITY_RESTRAINED);
                shadow.AddFeat(Feat.AMORPHOUS);
                shadow.AddFeat(Feat.SHADOW_STEALTH);
                shadow.AddFeat(Feat.SUNLIGHT_WEAKNESS);
                return shadow;
            }
        }

        /* TODO */
        public static Monster ShamblingMound {
            get {
                Monster shamblingMound = new Monster(
                    Monsters.Type.PLANT, Monsters.ID.SHAMBLING_MOUND, Alignment.UNALIGNED, 18, 8, 16, 5, 10, 5, 15, "16d10+48", 40, 5,
                    new Attack[] { Attacks.ShamblingMoundSlam }, new Attack[] { }, Size.LARGE
                );
                shamblingMound.AddProficiency(Proficiency.STEALTH);
                shamblingMound.AddEffect(Effect.RESISTANCE_COLD);
                shamblingMound.AddEffect(Effect.RESISTANCE_FIRE);
                shamblingMound.AddEffect(Effect.IMMUNITY_LIGHTNING);
                shamblingMound.AddEffect(Effect.IMMUNITY_BLINDED);
                shamblingMound.AddEffect(Effect.IMMUNITY_BLINDED);
                shamblingMound.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                shamblingMound.AddFeat(Feat.LIGHTNING_ABSORPTION);
                return shamblingMound;
            }
        }

        /* TODO */
        public static Monster ShieldGuardian {
            get {
                Monster shieldGuardian = new Monster(
                    Monsters.Type.CONSTRUCT, Monsters.ID.SHIELD_GUARDIAN, Alignment.UNALIGNED, 18, 8, 18, 7, 10, 3, 17, "15d10+60", 40, 7,
                    new Attack[] { Attacks.ShieldGuardianFist }, new Attack[] { }, Size.LARGE
                );
                shieldGuardian.AddEffect(Effect.IMMUNITY_POISON);
                shieldGuardian.AddEffect(Effect.IMMUNITY_CHARMED);
                shieldGuardian.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                shieldGuardian.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                shieldGuardian.AddEffect(Effect.IMMUNITY_PARALYZED);
                shieldGuardian.AddEffect(Effect.IMMUNITY_POISONED);
                shieldGuardian.AddFeat(Feat.BOUND);
                shieldGuardian.AddFeat(Feat.REGENERATION);
                shieldGuardian.AddFeat(Feat.SPELL_STORING);
                return shieldGuardian;
            }
        }

        /* TODO */
        public static Monster Shrieker {
            get {
                Monster shrieker = new Monster(
                    Monsters.Type.PLANT, Monsters.ID.SHRIEKER, Alignment.UNALIGNED, 1, 1, 10, 1, 3, 1, 5, "3d8", 40, 0,
                    new Attack[] { }, new Attack[] { }, Size.MEDIUM
                );
                shrieker.AddEffect(Effect.IMMUNITY_BLINDED);
                shrieker.AddEffect(Effect.IMMUNITY_BLINDED);
                shrieker.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                shrieker.AddFeat(Feat.FALSE_APPEARANCE);
                return shrieker;
            }
        }

        /* TODO */
        public static Monster SilverDragonWyrmling {
            get {
                Monster silverDragonWyrmling = new Monster(
                    Monsters.Type.DRAGON, Monsters.ID.SILVER_DRAGON_WYRMLING, Alignment.LAWFUL_GOOD, 19, 10, 17, 12, 11, 15, 17, "6d8+18", 40, 2,
                    new Attack[] { Attacks.SilverDragonWyrmlingBite }, new Attack[] { }, Size.MEDIUM
                );
                silverDragonWyrmling.AddProficiency(Proficiency.DEXTERITY);
                silverDragonWyrmling.AddProficiency(Proficiency.CONSTITUTION);
                silverDragonWyrmling.AddProficiency(Proficiency.WISDOM);
                silverDragonWyrmling.AddProficiency(Proficiency.CHARISMA);
                silverDragonWyrmling.AddProficiency(Proficiency.PERCEPTION);
                silverDragonWyrmling.AddProficiency(Proficiency.STEALTH);
                silverDragonWyrmling.AddEffect(Effect.IMMUNITY_COLD);
                return silverDragonWyrmling;
            }
        }

        /* TODO */
        public static Monster Skeleton {
            get {
                Monster skeleton = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.SKELETON, Alignment.LAWFUL_EVIL, 10, 14, 15, 6, 8, 5, 13, "2d8+4", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.SkeletonShortsword }, new Attack[] { Attacks.SkeletonShortbow }, Size.MEDIUM
                );
                skeleton.AddEffect(Effect.VULNERABILITY_BLUDGEONING);
                skeleton.AddEffect(Effect.IMMUNITY_POISON);
                skeleton.AddEffect(Effect.IMMUNITY_POISONED);
                skeleton.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                return skeleton;
            }
        }

        /* TODO */
        public static Monster Solar {
            get {
                Monster solar = new Monster(
                    Monsters.Type.CELESTIAL, Monsters.ID.SOLAR, Alignment.LAWFUL_GOOD, 26, 22, 26, 25, 25, 30, 21, "18d10+144", 40, 21,
                    new Attack[] { Attacks.SolarGreatsword }, new Attack[] { Attacks.SolarSlayingLongbow }, Size.LARGE
                );
                solar.AddProficiency(Proficiency.INTELLIGENCE);
                solar.AddProficiency(Proficiency.WISDOM);
                solar.AddProficiency(Proficiency.CHARISMA);
                solar.AddProficiency(Proficiency.PERCEPTION);
                solar.AddEffect(Effect.RESISTANCE_RADIANT);
                solar.AddEffect(Effect.RESISTANCE_NONMAGIC);
                solar.AddEffect(Effect.IMMUNITY_NECROTIC);
                solar.AddEffect(Effect.IMMUNITY_POISON);
                solar.AddEffect(Effect.IMMUNITY_CHARMED);
                solar.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                solar.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                solar.AddEffect(Effect.IMMUNITY_POISONED);
                solar.AddFeat(Feat.ANGELIC_WEAPONS_6D8);
                solar.AddFeat(Feat.DIVINE_AWARENESS);
                solar.AddFeat(Feat.INNATE_SPELLCASTING_SOLAR);
                solar.AddFeat(Feat.MAGIC_RESISTANCE);
                return solar;
            }
        }

        /* TODO */
        public static Monster Specter {
            get {
                Monster specter = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.SPECTER, Alignment.CHAOTIC_EVIL, 1, 14, 11, 10, 10, 11, 12, "5d8", 40, 1,
                    new Attack[] { Attacks.SpecterLifeDrain }, new Attack[] { }, Size.MEDIUM
                );
                specter.AddEffect(Effect.RESISTANCE_ACID);
                specter.AddEffect(Effect.RESISTANCE_COLD);
                specter.AddEffect(Effect.RESISTANCE_FIRE);
                specter.AddEffect(Effect.RESISTANCE_LIGHTNING);
                specter.AddEffect(Effect.RESISTANCE_THUNDER);
                specter.AddEffect(Effect.RESISTANCE_NONMAGIC);
                specter.AddEffect(Effect.IMMUNITY_NECROTIC);
                specter.AddEffect(Effect.IMMUNITY_POISON);
                specter.AddEffect(Effect.IMMUNITY_CHARMED);
                specter.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                specter.AddEffect(Effect.IMMUNITY_GRAPPLED);
                specter.AddEffect(Effect.IMMUNITY_PARALYZED);
                specter.AddEffect(Effect.IMMUNITY_PETRIFIED);
                specter.AddEffect(Effect.IMMUNITY_POISONED);
                specter.AddEffect(Effect.IMMUNITY_PRONE);
                specter.AddEffect(Effect.IMMUNITY_RESTRAINED);
                specter.AddEffect(Effect.IMMUNITY_UNCONSCIOUS);
                specter.AddFeat(Feat.INCORPOREAL_MOVEMENT);
                specter.AddFeat(Feat.SUNLIGHT_SENSITIVITY);
                return specter;
            }
        }

        /* TODO */
        public static Monster Spider {
            get {
                Monster spider = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.SPIDER, Alignment.UNALIGNED, 2, 14, 8, 1, 10, 2, 12, "1d4-1", 40, 0,
                    new Attack[] { Attacks.SpiderBite }, new Attack[] { }, Size.TINY
                );
                spider.AddProficiency(Proficiency.STEALTH);
                spider.AddFeat(Feat.SPIDER_CLIMB);
                spider.AddFeat(Feat.WEB_SENSE);
                spider.AddFeat(Feat.WEB_WALKER);
                return spider;
            }
        }

        /* TODO */
        public static Monster SpiritNaga {
            get {
                Monster spiritNaga = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.SPIRIT_NAGA, Alignment.CHAOTIC_EVIL, 18, 17, 14, 16, 15, 16, 15, "10d10+20", 40, 8,
                    new Attack[] { Attacks.SpiritNagaBite }, new Attack[] { }, Size.LARGE
                );
                spiritNaga.AddProficiency(Proficiency.DEXTERITY);
                spiritNaga.AddProficiency(Proficiency.CONSTITUTION);
                spiritNaga.AddProficiency(Proficiency.WISDOM);
                spiritNaga.AddProficiency(Proficiency.CHARISMA);
                spiritNaga.AddEffect(Effect.IMMUNITY_POISON);
                spiritNaga.AddEffect(Effect.IMMUNITY_CHARMED);
                spiritNaga.AddEffect(Effect.IMMUNITY_POISONED);
                spiritNaga.AddFeat(Feat.REJUVENATION_NAGA);
                spiritNaga.AddFeat(Feat.SPELLCASTING_NAGA_11);
                return spiritNaga;
            }
        }

        /* TODO */
        public static Monster Sprite {
            get {
                Monster sprite = new Monster(
                    Monsters.Type.FEY, Monsters.ID.SPRITE, Alignment.NEUTRAL_GOOD, 3, 18, 10, 14, 13, 11, 15, "1d4", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.SpriteLongsword }, new Attack[] { }, Size.TINY
                );
                sprite.AddProficiency(Proficiency.PERCEPTION);
                sprite.AddProficiency(Proficiency.STEALTH);
                return sprite;
            }
        }

        /* TODO */
        public static Monster Spy {
            get {
                Monster spy = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.SPY, Alignment.UNALIGNED, 10, 15, 10, 12, 14, 16, 12, "6d8", 40, 1,
                    new Attack[] { Attacks.SpyShortsword }, new Attack[] { Attacks.SpyHandCrossbow }, Size.MEDIUM
                );
                spy.AddProficiency(Proficiency.DECEPTION);
                spy.AddProficiency(Proficiency.INSIGHT);
                spy.AddProficiency(Proficiency.INVESTIGATION);
                spy.AddProficiency(Proficiency.PERCEPTION);
                spy.AddProficiency(Proficiency.PERSUASION);
                spy.AddProficiency(Proficiency.STEALTH);
                spy.AddFeat(Feat.CUNNING_ACTION);
                spy.AddFeat(Feat.SNEAK_ATTACK_2D6);
                return spy;
            }
        }

        /* TODO */
        public static Monster SteamMephit {
            get {
                Monster steamMephit = new Monster(
                    Monsters.Type.ELEMENTAL, Monsters.ID.STEAM_MEPHIT, Alignment.NEUTRAL_EVIL, 5, 11, 10, 11, 10, 12, 10, "6d6", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.SteamMephitClaws }, new Attack[] { }, Size.SMALL
                );
                steamMephit.AddEffect(Effect.IMMUNITY_FIRE);
                steamMephit.AddEffect(Effect.IMMUNITY_POISON);
                steamMephit.AddEffect(Effect.IMMUNITY_POISONED);
                steamMephit.AddFeat(Feat.DEATH_BURST_STEAM_MEPHIT);
                steamMephit.AddFeat(Feat.INNATE_SPELLCASTING_STEAM_MEPHIT);
                return steamMephit;
            }
        }

        /* TODO */
        public static Monster Stirge {
            get {
                Monster stirge = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.STIRGE, Alignment.UNALIGNED, 4, 16, 11, 2, 8, 6, 14, "1d4", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.StirgeBloodDrain }, new Attack[] { }, Size.TINY
                );
                return stirge;
            }
        }

        /* TODO */
        public static Monster StoneGiant {
            get {
                Monster stoneGiant = new Monster(
                    Monsters.Type.GIANT, Monsters.ID.STONE_GIANT, Alignment.NEUTRAL, 23, 15, 20, 10, 12, 9, 17, "11d12+55", 40, 7,
                    new Attack[] { Attacks.StoneGiantGreatclub }, new Attack[] { Attacks.StoneGiantRock }, Size.HUGE
                );
                stoneGiant.AddProficiency(Proficiency.DEXTERITY);
                stoneGiant.AddProficiency(Proficiency.CONSTITUTION);
                stoneGiant.AddProficiency(Proficiency.WISDOM);
                stoneGiant.AddProficiency(Proficiency.ATHLETICS);
                stoneGiant.AddProficiency(Proficiency.PERCEPTION);
                stoneGiant.AddFeat(Feat.STONE_CAMOUFLAGE);
                return stoneGiant;
            }
        }

        /* TODO */
        public static Monster StoneGolem {
            get {
                Monster stoneGolem = new Monster(
                    Monsters.Type.CONSTRUCT, Monsters.ID.STONE_GOLEM, Alignment.UNALIGNED, 22, 9, 20, 3, 11, 1, 17, "17d10+85", 40, 10,
                    new Attack[] { Attacks.StoneGolemSlam }, new Attack[] { }, Size.LARGE
                );
                stoneGolem.AddEffect(Effect.IMMUNITY_POISON);
                stoneGolem.AddEffect(Effect.IMMUNITY_PSYCHIC);
                stoneGolem.AddEffect(Effect.IMMUNITY_NONMAGIC);
                stoneGolem.AddEffect(Effect.IMMUNITY_CHARMED);
                stoneGolem.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                stoneGolem.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                stoneGolem.AddEffect(Effect.IMMUNITY_PARALYZED);
                stoneGolem.AddEffect(Effect.IMMUNITY_PETRIFIED);
                stoneGolem.AddEffect(Effect.IMMUNITY_POISONED);
                stoneGolem.AddFeat(Feat.IMMUTABLE_FORM);
                stoneGolem.AddFeat(Feat.MAGIC_RESISTANCE);
                stoneGolem.AddFeat(Feat.MAGIC_WEAPONS);
                return stoneGolem;
            }
        }

        /* TODO */
        public static Monster StormGiant {
            get {
                Monster stormGiant = new Monster(
                    Monsters.Type.GIANT, Monsters.ID.STORM_GIANT, Alignment.CHAOTIC_GOOD, 29, 14, 20, 16, 18, 18, 16, "20d12+100", 40, 13,
                    new Attack[] { Attacks.StormGiantGreatsword }, new Attack[] { Attacks.StormGiantRock }, Size.HUGE
                );
                stormGiant.AddProficiency(Proficiency.STRENGTH);
                stormGiant.AddProficiency(Proficiency.CONSTITUTION);
                stormGiant.AddProficiency(Proficiency.WISDOM);
                stormGiant.AddProficiency(Proficiency.CHARISMA);
                stormGiant.AddProficiency(Proficiency.ARCANA);
                stormGiant.AddProficiency(Proficiency.ATHLETICS);
                stormGiant.AddProficiency(Proficiency.HISTORY);
                stormGiant.AddProficiency(Proficiency.PERCEPTION);
                stormGiant.AddEffect(Effect.RESISTANCE_COLD);
                stormGiant.AddEffect(Effect.IMMUNITY_LIGHTNING);
                stormGiant.AddEffect(Effect.IMMUNITY_THUNDER);
                stormGiant.AddFeat(Feat.AMPHIBIOUS);
                stormGiant.AddFeat(Feat.INNATE_SPELLCASTING_STORM_GIANT);
                return stormGiant;
            }
        }

        /* TODO */
        public static Monster Succubus {
            get {
                Monster succubus = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.SUCCUBUS_INCUBUS, Alignment.NEUTRAL_EVIL, 8, 17, 13, 15, 12, 20, 15, "12d8+12", 40, 4,
                    new Attack[] { Attacks.SuccubusClaw }, new Attack[] { }, Size.MEDIUM
                );
                succubus.AddProficiency(Proficiency.DECEPTION);
                succubus.AddProficiency(Proficiency.INSIGHT);
                succubus.AddProficiency(Proficiency.PERCEPTION);
                succubus.AddProficiency(Proficiency.PERSUASION);
                succubus.AddProficiency(Proficiency.STEALTH);
                succubus.AddEffect(Effect.RESISTANCE_COLD);
                succubus.AddEffect(Effect.RESISTANCE_FIRE);
                succubus.AddEffect(Effect.RESISTANCE_LIGHTNING);
                succubus.AddEffect(Effect.RESISTANCE_POISON);
                succubus.AddEffect(Effect.RESISTANCE_NONMAGIC);
                succubus.AddFeat(Feat.TELEPATHIC_BOND_FIEND);
                succubus.AddFeat(Feat.SHAPECHANGER_FIEND);
                return succubus;
            }
        }

        /* TODO */
        public static Monster SwarmOfBats {
            get {
                Monster swarmOfBats = new Monster(
                    Monsters.Type.SWARM, Monsters.ID.SWARM_OF_BATS, Alignment.UNALIGNED, 5, 15, 10, 2, 12, 4, 12, "5d8", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.SwarmOfBatsBites }, new Attack[] { }, Size.MEDIUM
                );
                swarmOfBats.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                swarmOfBats.AddEffect(Effect.RESISTANCE_PIERCING);
                swarmOfBats.AddEffect(Effect.RESISTANCE_SLASHING);
                swarmOfBats.AddEffect(Effect.IMMUNITY_CHARMED);
                swarmOfBats.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                swarmOfBats.AddEffect(Effect.IMMUNITY_GRAPPLED);
                swarmOfBats.AddEffect(Effect.IMMUNITY_PARALYZED);
                swarmOfBats.AddEffect(Effect.IMMUNITY_PETRIFIED);
                swarmOfBats.AddEffect(Effect.IMMUNITY_PRONE);
                swarmOfBats.AddEffect(Effect.IMMUNITY_RESTRAINED);
                swarmOfBats.AddEffect(Effect.IMMUNITY_STUNNED);
                swarmOfBats.AddFeat(Feat.ECHOLOCATION);
                swarmOfBats.AddFeat(Feat.KEEN_HEARING);
                swarmOfBats.AddFeat(Feat.SWARM);
                return swarmOfBats;
            }
        }

        /* TODO */
        public static Monster SwarmOfBeetles {
            get {
                Monster swarmOfBeetles = new Monster(
                    Monsters.Type.SWARM, Monsters.ID.SWARM_OF_BEETLES, Alignment.UNALIGNED, 3, 13, 10, 1, 7, 1, 12, "5d8", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.SwarmOfBeetlesBites }, new Attack[] { }, Size.MEDIUM
                );
                swarmOfBeetles.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                swarmOfBeetles.AddEffect(Effect.RESISTANCE_PIERCING);
                swarmOfBeetles.AddEffect(Effect.RESISTANCE_SLASHING);
                swarmOfBeetles.AddEffect(Effect.IMMUNITY_CHARMED);
                swarmOfBeetles.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                swarmOfBeetles.AddEffect(Effect.IMMUNITY_GRAPPLED);
                swarmOfBeetles.AddEffect(Effect.IMMUNITY_PARALYZED);
                swarmOfBeetles.AddEffect(Effect.IMMUNITY_PETRIFIED);
                swarmOfBeetles.AddEffect(Effect.IMMUNITY_PRONE);
                swarmOfBeetles.AddEffect(Effect.IMMUNITY_RESTRAINED);
                swarmOfBeetles.AddEffect(Effect.IMMUNITY_STUNNED);
                swarmOfBeetles.AddFeat(Feat.SWARM);
                return swarmOfBeetles;
            }
        }

        /* TODO */
        public static Monster SwarmOfCentipedes {
            get {
                Monster swarmOfCentipedes = new Monster(
                    Monsters.Type.SWARM, Monsters.ID.SWARM_OF_CENTIPEDES, Alignment.UNALIGNED, 3, 13, 10, 1, 7, 1, 12, "5d8", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.SwarmOfCentipedesBites }, new Attack[] { }, Size.MEDIUM
                );
                swarmOfCentipedes.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                swarmOfCentipedes.AddEffect(Effect.RESISTANCE_PIERCING);
                swarmOfCentipedes.AddEffect(Effect.RESISTANCE_SLASHING);
                swarmOfCentipedes.AddEffect(Effect.IMMUNITY_CHARMED);
                swarmOfCentipedes.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                swarmOfCentipedes.AddEffect(Effect.IMMUNITY_GRAPPLED);
                swarmOfCentipedes.AddEffect(Effect.IMMUNITY_PARALYZED);
                swarmOfCentipedes.AddEffect(Effect.IMMUNITY_PETRIFIED);
                swarmOfCentipedes.AddEffect(Effect.IMMUNITY_PRONE);
                swarmOfCentipedes.AddEffect(Effect.IMMUNITY_RESTRAINED);
                swarmOfCentipedes.AddEffect(Effect.IMMUNITY_STUNNED);
                swarmOfCentipedes.AddFeat(Feat.SWARM);
                return swarmOfCentipedes;
            }
        }

        /* TODO */
        public static Monster SwarmOfInsects {
            get {
                Monster swarmOfInsects = new Monster(
                    Monsters.Type.SWARM, Monsters.ID.SWARM_OF_INSECTS, Alignment.UNALIGNED, 3, 13, 10, 1, 7, 1, 12, "5d8", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.SwarmOfInsectsBites }, new Attack[] { }, Size.MEDIUM
                );
                swarmOfInsects.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                swarmOfInsects.AddEffect(Effect.RESISTANCE_PIERCING);
                swarmOfInsects.AddEffect(Effect.RESISTANCE_SLASHING);
                swarmOfInsects.AddEffect(Effect.IMMUNITY_CHARMED);
                swarmOfInsects.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                swarmOfInsects.AddEffect(Effect.IMMUNITY_GRAPPLED);
                swarmOfInsects.AddEffect(Effect.IMMUNITY_PARALYZED);
                swarmOfInsects.AddEffect(Effect.IMMUNITY_PETRIFIED);
                swarmOfInsects.AddEffect(Effect.IMMUNITY_PRONE);
                swarmOfInsects.AddEffect(Effect.IMMUNITY_RESTRAINED);
                swarmOfInsects.AddEffect(Effect.IMMUNITY_STUNNED);
                swarmOfInsects.AddFeat(Feat.SWARM);
                return swarmOfInsects;
            }
        }

        /* TODO */
        public static Monster SwarmOfPoisonousSnakes {
            get {
                Monster swarmOfPoisonousSnakes = new Monster(
                    Monsters.Type.SWARM, Monsters.ID.SWARM_OF_POISONOUS_SNAKES, Alignment.UNALIGNED, 8, 18, 11, 1, 10, 3, 14, "8d8", 40, 2,
                    new Attack[] { Attacks.SwarmOfPoisonousSnakesBites }, new Attack[] { }, Size.MEDIUM
                );
                swarmOfPoisonousSnakes.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                swarmOfPoisonousSnakes.AddEffect(Effect.RESISTANCE_PIERCING);
                swarmOfPoisonousSnakes.AddEffect(Effect.RESISTANCE_SLASHING);
                swarmOfPoisonousSnakes.AddEffect(Effect.IMMUNITY_CHARMED);
                swarmOfPoisonousSnakes.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                swarmOfPoisonousSnakes.AddEffect(Effect.IMMUNITY_GRAPPLED);
                swarmOfPoisonousSnakes.AddEffect(Effect.IMMUNITY_PARALYZED);
                swarmOfPoisonousSnakes.AddEffect(Effect.IMMUNITY_PETRIFIED);
                swarmOfPoisonousSnakes.AddEffect(Effect.IMMUNITY_PRONE);
                swarmOfPoisonousSnakes.AddEffect(Effect.IMMUNITY_RESTRAINED);
                swarmOfPoisonousSnakes.AddEffect(Effect.IMMUNITY_STUNNED);
                swarmOfPoisonousSnakes.AddFeat(Feat.SWARM);
                return swarmOfPoisonousSnakes;
            }
        }

        /* TODO */
        public static Monster SwarmOfQuippers {
            get {
                Monster swarmOfQuippers = new Monster(
                    Monsters.Type.SWARM, Monsters.ID.SWARM_OF_QUIPPERS, Alignment.UNALIGNED, 13, 16, 9, 1, 7, 2, 13, "8d8-8", 40, 1,
                    new Attack[] { Attacks.SwarmOfQuippersBites }, new Attack[] { }, Size.MEDIUM
                );
                swarmOfQuippers.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                swarmOfQuippers.AddEffect(Effect.RESISTANCE_PIERCING);
                swarmOfQuippers.AddEffect(Effect.RESISTANCE_SLASHING);
                swarmOfQuippers.AddEffect(Effect.IMMUNITY_CHARMED);
                swarmOfQuippers.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                swarmOfQuippers.AddEffect(Effect.IMMUNITY_GRAPPLED);
                swarmOfQuippers.AddEffect(Effect.IMMUNITY_PARALYZED);
                swarmOfQuippers.AddEffect(Effect.IMMUNITY_PETRIFIED);
                swarmOfQuippers.AddEffect(Effect.IMMUNITY_PRONE);
                swarmOfQuippers.AddEffect(Effect.IMMUNITY_RESTRAINED);
                swarmOfQuippers.AddEffect(Effect.IMMUNITY_STUNNED);
                swarmOfQuippers.AddFeat(Feat.BLOOD_FRENZY);
                swarmOfQuippers.AddFeat(Feat.SWARM);
                swarmOfQuippers.AddFeat(Feat.WATER_BREATHING);
                return swarmOfQuippers;
            }
        }

        /* TODO */
        public static Monster SwarmOfRats {
            get {
                Monster swarmOfRats = new Monster(
                    Monsters.Type.SWARM, Monsters.ID.SWARM_OF_RATS, Alignment.UNALIGNED, 9, 11, 9, 2, 10, 3, 10, "7d8-7", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.SwarmOfRatsBites }, new Attack[] { }, Size.MEDIUM
                );
                swarmOfRats.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                swarmOfRats.AddEffect(Effect.RESISTANCE_PIERCING);
                swarmOfRats.AddEffect(Effect.RESISTANCE_SLASHING);
                swarmOfRats.AddEffect(Effect.IMMUNITY_CHARMED);
                swarmOfRats.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                swarmOfRats.AddEffect(Effect.IMMUNITY_GRAPPLED);
                swarmOfRats.AddEffect(Effect.IMMUNITY_PARALYZED);
                swarmOfRats.AddEffect(Effect.IMMUNITY_PETRIFIED);
                swarmOfRats.AddEffect(Effect.IMMUNITY_PRONE);
                swarmOfRats.AddEffect(Effect.IMMUNITY_RESTRAINED);
                swarmOfRats.AddEffect(Effect.IMMUNITY_STUNNED);
                swarmOfRats.AddFeat(Feat.KEEN_SMELL);
                swarmOfRats.AddFeat(Feat.SWARM);
                return swarmOfRats;
            }
        }

        /* TODO */
        public static Monster SwarmOfRavens {
            get {
                Monster swarmOfRavens = new Monster(
                    Monsters.Type.SWARM, Monsters.ID.SWARM_OF_RAVENS, Alignment.UNALIGNED, 6, 14, 8, 3, 12, 6, 12, "7d8-7", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.SwarmOfRavensBeaks }, new Attack[] { }, Size.MEDIUM
                );
                swarmOfRavens.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                swarmOfRavens.AddEffect(Effect.RESISTANCE_PIERCING);
                swarmOfRavens.AddEffect(Effect.RESISTANCE_SLASHING);
                swarmOfRavens.AddEffect(Effect.IMMUNITY_CHARMED);
                swarmOfRavens.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                swarmOfRavens.AddEffect(Effect.IMMUNITY_GRAPPLED);
                swarmOfRavens.AddEffect(Effect.IMMUNITY_PARALYZED);
                swarmOfRavens.AddEffect(Effect.IMMUNITY_PETRIFIED);
                swarmOfRavens.AddEffect(Effect.IMMUNITY_PRONE);
                swarmOfRavens.AddEffect(Effect.IMMUNITY_RESTRAINED);
                swarmOfRavens.AddEffect(Effect.IMMUNITY_STUNNED);
                swarmOfRavens.AddFeat(Feat.SWARM);
                return swarmOfRavens;
            }
        }

        /* TODO */
        public static Monster SwarmOfSpiders {
            get {
                Monster swarmOfSpiders = new Monster(
                    Monsters.Type.SWARM, Monsters.ID.SWARM_OF_SPIDERS, Alignment.UNALIGNED, 3, 13, 10, 1, 7, 1, 12, "5d8", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.SwarmOfSpidersBites }, new Attack[] { }, Size.MEDIUM
                );
                swarmOfSpiders.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                swarmOfSpiders.AddEffect(Effect.RESISTANCE_PIERCING);
                swarmOfSpiders.AddEffect(Effect.RESISTANCE_SLASHING);
                swarmOfSpiders.AddEffect(Effect.IMMUNITY_CHARMED);
                swarmOfSpiders.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                swarmOfSpiders.AddEffect(Effect.IMMUNITY_PARALYZED);
                swarmOfSpiders.AddEffect(Effect.IMMUNITY_PETRIFIED);
                swarmOfSpiders.AddEffect(Effect.IMMUNITY_PRONE);
                swarmOfSpiders.AddEffect(Effect.IMMUNITY_RESTRAINED);
                swarmOfSpiders.AddEffect(Effect.IMMUNITY_STUNNED);
                swarmOfSpiders.AddFeat(Feat.SWARM);
                swarmOfSpiders.AddFeat(Feat.SPIDER_CLIMB);
                swarmOfSpiders.AddFeat(Feat.WEB_SENSE);
                swarmOfSpiders.AddFeat(Feat.WEB_WALKER);
                return swarmOfSpiders;
            }
        }

        /* TODO */
        public static Monster SwarmOfWasps {
            get {
                Monster swarmOfWasps = new Monster(
                    Monsters.Type.SWARM, Monsters.ID.SWARM_OF_WASPS, Alignment.UNALIGNED, 3, 13, 10, 1, 7, 1, 12, "5d8", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.SwarmOfWaspsBites }, new Attack[] { }, Size.MEDIUM
                );
                swarmOfWasps.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                swarmOfWasps.AddEffect(Effect.RESISTANCE_PIERCING);
                swarmOfWasps.AddEffect(Effect.RESISTANCE_SLASHING);
                swarmOfWasps.AddEffect(Effect.IMMUNITY_CHARMED);
                swarmOfWasps.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                swarmOfWasps.AddEffect(Effect.IMMUNITY_GRAPPLED);
                swarmOfWasps.AddEffect(Effect.IMMUNITY_PARALYZED);
                swarmOfWasps.AddEffect(Effect.IMMUNITY_PETRIFIED);
                swarmOfWasps.AddEffect(Effect.IMMUNITY_PRONE);
                swarmOfWasps.AddEffect(Effect.IMMUNITY_RESTRAINED);
                swarmOfWasps.AddEffect(Effect.IMMUNITY_STUNNED);
                swarmOfWasps.AddFeat(Feat.SWARM);
                return swarmOfWasps;
            }
        }
    }
}