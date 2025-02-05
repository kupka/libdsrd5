namespace srd5 {
    public partial struct Attacks {
        public static Attack CamelBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.BLUDGEONING, "1d4"), 5);
            }
        }
        public static Attack CatClaws {
            get {
                return new Attack("Claws", 0, new Damage(DamageType.SLASHING, 1), 5);
            }
        }
        public static Attack CentaurPike {
            get {
                return new Attack("Pike", 6, new Damage(DamageType.PIERCING, "1d10+4"), 10);
            }
        }
        public static Attack CentaurHooves {
            get {
                return new Attack("Hooves", 6, new Damage(DamageType.BLUDGEONING, "2d6+4"), 5);
            }
        }
        public static Attack CentaurLongbow {
            get {
                return new Attack("Longbow", 4, new Damage(DamageType.PIERCING, "1d8+2"), 5, 150, 600);
            }
        }
        public static readonly AttackEffect ChainDevilChainEffect = delegate (Combattant attacker, Combattant target) {
            if (target.HasEffect(Effect.IMMUNITY_GRAPPLED)) return;
            if (attacker.HasEffect(Effect.GRAPPLING)) return;
            attacker.AddEffect(Effect.GRAPPLING);
            target.AddCondition(ConditionType.GRAPPLED_DC14);
            target.AddCondition(ConditionType.RESTRAINED);
            target.AddStartOfTurnEvent(delegate () {
                if (!target.HasCondition(ConditionType.GRAPPLED_DC14)) {
                    attacker.RemoveEffect(Effect.GRAPPLING);
                    target.RemoveCondition(ConditionType.RESTRAINED);
                    return true;
                }
                target.TakeDamage(attacker, DamageType.PIERCING, "2d6");
                return false;
            });
        };
        public static Attack ChainDevilChain {
            get {
                return new Attack("Chain", 8, new Damage(DamageType.SLASHING, "2d6+4"), 5, null, ChainDevilChainEffect);
            }
        }
        public static Attack ChimeraBite {
            get {
                return new Attack("Bite", 7, new Damage(DamageType.PIERCING, "2d6+4"), 5);
            }
        }
        public static Attack ChimeraHorns {
            get {
                return new Attack("Horns", 7, new Damage(DamageType.BLUDGEONING, "1d12+4"), 5);
            }
        }
        public static Attack ChimeraClaws {
            get {
                return new Attack("Claws", 7, new Damage(DamageType.SLASHING, "2d6+4"), 5);
            }
        }
        public static readonly AttackEffect ChuulPincerEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 14, Size.LARGE, false, null, 2);
        };
        public static Attack ChuulPincer {
            get {
                return new Attack("Pincer", 6, new Damage(DamageType.BLUDGEONING, "2d6+4"), 5, null, ChuulPincerEffect);
            }
        }
        public static readonly AttackEffect ClayGolemSlamEffect = delegate (Combattant attacker, Combattant target) {
            if (target.DC(ClayGolemSlam, 15, AbilityType.CONSTITUTION)) return;
            target.AddHitPointMaximumModifiers(new HitPointMaxiumModifier(-16, HitPointMaxiumModifier.RemovedByEffect.GREATER_RESTORATION)); // FIXME: Amount should be equal to actual damage, not average
            if (target.HitPointsMax <= 0) {
                // TODO: target dies
            }
        };
        public static Attack ClayGolemSlam {
            get {
                return new Attack("Slam", 8, new Damage(DamageType.BLUDGEONING, "2d10+5"), 5, null, ClayGolemSlamEffect);
            }
        }
        public static readonly AttackEffect CloakerBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target.Size > Size.LARGE) return;
            if (attacker.HasEffect(Effect.ATTACHED_TO_TARGET)) return;
            attacker.AddEffect(Effect.ATTACHED_TO_TARGET);
            attacker.AddEffect(Effect.ADVANTAGE_ON_ATTACK);
            foreach (Attack attack in attacker.MeleeAttacks) {
                if (attack.Name == CloakerBite.Name)
                    attack.LockedTarget = target;
            }
            target.AddCondition(ConditionType.BLINDED);
            target.AddEffect(Effect.UNABLE_TO_BREATHE);
        };
        public static Attack CloakerBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "2d6+3"), 5, null, CloakerBiteEffect);
            }
        }
        public static Attack CloakerTail {
            get {
                return new Attack("Tail", 6, new Damage(DamageType.SLASHING, "1d8+3"), 10);
            }
        }
        public static Attack CloudGiantMorningstar {
            get {
                return new Attack("Morningstar", 12, new Damage(DamageType.PIERCING, "3d8+8"), 10);
            }
        }
        public static Attack CloudGiantRock {
            get {
                return new Attack("Rock", 12, new Damage(DamageType.BLUDGEONING, "4d10+8"), 5, 60, 240);
            }
        }
        public static readonly AttackEffect CockatriceBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target.DC(CockatriceBite, 12, AbilityType.CONSTITUTION)) return;
            target.AddCondition(ConditionType.RESTRAINED);
            target.AddEndOfTurnEvent(delegate () {
                if (!target.DC(CockatriceBite, 11, AbilityType.CONSTITUTION)) target.AddCondition(ConditionType.PETRIFIED);
                target.RemoveCondition(ConditionType.RESTRAINED);
                return true;
            });
        };
        public static Attack CockatriceBite {
            get {
                return new Attack("Bite", 3, new Damage(DamageType.PIERCING, "1d4+1"), 5, null, CockatriceBiteEffect);
            }
        }
        public static Attack CommonerClub {
            get {
                return new Attack("Club", 2, new Damage(DamageType.BLUDGEONING, "1d4"), 5);
            }
        }
        public static readonly AttackEffect ConstrictorSnakeConstrictEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 14, Monsters.ConstrictorSnake.Size + 1, true);
        };
        public static Attack ConstrictorSnakeConstrict {
            get {
                return new Attack("Constrict", 4, new Damage(DamageType.BLUDGEONING, "1d8+2"), 5, null, ConstrictorSnakeConstrictEffect);
            }
        }
        public static Attack ConstrictorSnakeBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack CopperDragonWyrmlingBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d10+2"), 5);
            }
        }
        public static readonly AttackEffect CouatlBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target.IsImmune(DamageType.POISON)) return;
            if (target.DC(CouatlBite, 13, AbilityType.CONSTITUTION)) return;
            target.AddEffect(Effect.COUATL_POISON);
            target.AddDamageTakenEvent(delegate (object source, Damage damage) {
                target.RemoveEffect(Effect.COUATL_POISON);
                return true;
            });
        };
        public static Attack CouatlBite {
            get {
                return new Attack("Bite", 8, new Damage(DamageType.PIERCING, "1d6+5"), 5, null, CouatlBiteEffect);
            }
        }
        public static readonly AttackEffect CouatlConstrictEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 15, Monsters.Couatl.Size + 1, true);
        };
        public static Attack CouatlConstrict {
            get {
                return new Attack("Constrict", 6, new Damage(DamageType.BLUDGEONING, "2d6+3"), 5, null, CouatlConstrictEffect);
            }
        }
        public static Attack CrabClaw {
            get {
                return new Attack("Claw", 0, new Damage(DamageType.BLUDGEONING, 1), 5);
            }
        }
        public static readonly AttackEffect CrocodileBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 12, Monsters.Crocodile.Size + 1, true, CrocodileBite);
        };
        public static Attack CrocodileBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d10+2"), 5, null, CrocodileBiteEffect);
            }
        }
        public static Attack CultFanaticDaggerMelee {
            get {
                return new Attack("Dagger", 4, new Damage(DamageType.PIERCING, "1d4+2"), 5);
            }
        }
        public static Attack CultFanaticDaggerRanged {
            get {
                return new Attack("Dagger", 4, new Damage(DamageType.PIERCING, "1d4+2"), 5, 20, 60);
            }
        }
        public static Attack CultistScimitar {
            get {
                return new Attack("Scimitar", 3, new Damage(DamageType.SLASHING, "1d6+1"), 5);
            }
        }
    }

    public partial struct Monsters {
        /* TODO */
        public static Monster Camel {
            get {
                Monster camel = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.CAMEL, Alignment.UNALIGNED, 16, 8, 14, 2, 8, 5, 9, "2d10+4", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.CamelBite }, new Attack[] { }, Size.LARGE
                );
                return camel;
            }
        }

        /* TODO */
        public static Monster Cat {
            get {
                Monster cat = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.CAT, Alignment.UNALIGNED, 3, 15, 10, 3, 12, 7, 12, "1d4", 40, 0,
                    new Attack[] { Attacks.CatClaws }, new Attack[] { }, Size.TINY
                );
                cat.AddProficiency(Proficiency.PERCEPTION);
                cat.AddProficiency(Proficiency.STEALTH);
                cat.AddFeat(Feat.KEEN_SMELL);
                return cat;
            }
        }

        /* TODO */
        public static Monster Centaur {
            get {
                Monster centaur = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.CENTAUR, Alignment.NEUTRAL_GOOD, 18, 14, 14, 9, 13, 11, 12, "6d10+12", 40, 2,
                    new Attack[] { Attacks.CentaurPike, Attacks.CentaurHooves }, new Attack[] { Attacks.CentaurLongbow }, Size.LARGE
                );
                centaur.AddProficiency(Proficiency.ATHLETICS);
                centaur.AddProficiency(Proficiency.PERCEPTION);
                centaur.AddProficiency(Proficiency.SURVIVAL);
                centaur.AddFeat(Feat.CHARGE_CENTAUR);
                return centaur;
            }
        }

        /* TODO */
        public static Monster ChainDevil {
            get {
                Monster chainDevil = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.CHAIN_DEVIL, Alignment.LAWFUL_EVIL, 18, 15, 18, 11, 12, 14, 16, "10d8+40", 40, 8,
                    new Attack[] { Attacks.ChainDevilChain }, new Attack[] { }, Size.MEDIUM
                );
                chainDevil.AddProficiency(Proficiency.CONSTITUTION);
                chainDevil.AddProficiency(Proficiency.WISDOM);
                chainDevil.AddProficiency(Proficiency.CHARISMA);
                chainDevil.AddEffect(Effect.RESISTANCE_COLD);
                chainDevil.AddEffect(Effect.RESISTANCE_NONMAGIC);
                chainDevil.AddEffect(Effect.IMMUNITY_FIRE);
                chainDevil.AddEffect(Effect.IMMUNITY_POISON);
                chainDevil.AddEffect(Effect.IMMUNITY_POISONED);
                chainDevil.AddFeat(Feat.DEVILS_SIGHT);
                chainDevil.AddFeat(Feat.MAGIC_RESISTANCE);
                return chainDevil;
            }
        }

        /* TODO */
        public static Monster Chimera {
            get {
                Monster chimera = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.CHIMERA, Alignment.CHAOTIC_EVIL, 19, 11, 19, 3, 14, 10, 14, "12d10+48", 40, 6,
                    new Attack[] { Attacks.ChimeraBite, Attacks.ChimeraHorns, Attacks.ChimeraClaws }, new Attack[] { }, Size.LARGE
                );
                chimera.AddProficiency(Proficiency.PERCEPTION);
                return chimera;
            }
        }

        /* TODO */
        public static Monster Chuul {
            get {
                Monster chuul = new Monster(
                    Monsters.Type.ABERRATION, Monsters.ID.CHUUL, Alignment.CHAOTIC_EVIL, 19, 10, 16, 5, 11, 5, 16, "11d10+33", 40, 4,
                    new Attack[] { Attacks.ChuulPincer }, new Attack[] { }, Size.LARGE
                );
                chuul.AddProficiency(Proficiency.PERCEPTION);
                chuul.AddEffect(Effect.IMMUNITY_POISON);
                chuul.AddEffect(Effect.IMMUNITY_POISONED);
                chuul.AddFeat(Feat.AMPHIBIOUS);
                chuul.AddFeat(Feat.SENSE_MAGIC);
                return chuul;
            }
        }

        /* TODO */
        public static Monster ClayGolem {
            get {
                Monster clayGolem = new Monster(
                    Monsters.Type.CONSTRUCT, Monsters.ID.CLAY_GOLEM, Alignment.UNALIGNED, 20, 9, 18, 3, 8, 1, 14, "14d10+56", 40, 9,
                    new Attack[] { Attacks.ClayGolemSlam }, new Attack[] { }, Size.LARGE
                );
                clayGolem.AddEffect(Effect.IMMUNITY_ACID);
                clayGolem.AddEffect(Effect.IMMUNITY_POISON);
                clayGolem.AddEffect(Effect.IMMUNITY_PSYCHIC);
                clayGolem.AddEffect(Effect.IMMUNITY_NONMAGIC);
                clayGolem.AddEffect(Effect.IMMUNITY_CHARMED);
                clayGolem.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                clayGolem.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                clayGolem.AddEffect(Effect.IMMUNITY_PARALYZED);
                clayGolem.AddEffect(Effect.IMMUNITY_PETRIFIED);
                clayGolem.AddEffect(Effect.IMMUNITY_POISONED);
                clayGolem.AddFeat(Feat.ACID_ABSORPTION);
                clayGolem.AddFeat(Feat.BERSERK_CLAY_GOLEM);
                clayGolem.AddFeat(Feat.IMMUTABLE_FORM);
                clayGolem.AddFeat(Feat.MAGIC_RESISTANCE);
                clayGolem.AddFeat(Feat.MAGIC_WEAPONS);
                return clayGolem;
            }
        }

        /* TODO */
        public static Monster Cloaker {
            get {
                Monster cloaker = new Monster(
                    Monsters.Type.ABERRATION, Monsters.ID.CLOAKER, Alignment.CHAOTIC_NEUTRAL, 17, 15, 12, 13, 12, 14, 14, "12d10+12", 40, 8,
                    new Attack[] { Attacks.CloakerBite, Attacks.CloakerTail }, new Attack[] { }, Size.LARGE
                );
                cloaker.AddProficiency(Proficiency.STEALTH);
                cloaker.AddFeat(Feat.DAMAGE_TRANSFER_ATTACHED);
                cloaker.AddFeat(Feat.FALSE_APPEARANCE);
                cloaker.AddFeat(Feat.LIGHT_SENSITIVITY);
                return cloaker;
            }
        }

        /* TODO */
        public static Monster CloudGiant {
            get {
                Monster cloudGiant = new Monster(
                    Monsters.Type.GIANT, Monsters.ID.CLOUD_GIANT, Alignment.NEUTRAL_GOOD, 27, 10, 22, 12, 16, 16, 14, "16d12+96", 40, 9,
                    new Attack[] { Attacks.CloudGiantMorningstar }, new Attack[] { Attacks.CloudGiantRock }, Size.HUGE
                );
                cloudGiant.AddProficiency(Proficiency.CONSTITUTION);
                cloudGiant.AddProficiency(Proficiency.WISDOM);
                cloudGiant.AddProficiency(Proficiency.CHARISMA);
                cloudGiant.AddProficiency(Proficiency.INSIGHT);
                cloudGiant.AddProficiency(Proficiency.PERCEPTION);
                cloudGiant.AddFeat(Feat.KEEN_SMELL);
                cloudGiant.AddFeat(Feat.INNATE_SPELLCASTING_CLOUD_GIANT);
                return cloudGiant;
            }
        }

        /* TODO */
        public static Monster Cockatrice {
            get {
                Monster cockatrice = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.COCKATRICE, Alignment.UNALIGNED, 6, 12, 12, 2, 13, 5, 11, "6d6+6", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.CockatriceBite }, new Attack[] { }, Size.SMALL
                );
                return cockatrice;
            }
        }

        /* TODO */
        public static Monster Commoner {
            get {
                Monster commoner = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.COMMONER, Alignment.UNALIGNED, 10, 10, 10, 10, 10, 10, 10, "1d8", 40, 0,
                    new Attack[] { Attacks.CommonerClub }, new Attack[] { }, Size.MEDIUM
                );
                return commoner;
            }
        }

        /* TODO */
        public static Monster ConstrictorSnake {
            get {
                Monster constrictorSnake = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.CONSTRICTOR_SNAKE, Alignment.UNALIGNED, 15, 14, 12, 1, 10, 3, 12, "2d10+2", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.ConstrictorSnakeBite, Attacks.ConstrictorSnakeConstrict }, new Attack[] { }, Size.LARGE
                );
                return constrictorSnake;
            }
        }

        /* TODO */
        public static Monster CopperDragonWyrmling {
            get {
                Monster copperDragonWyrmling = new Monster(
                    Monsters.Type.DRAGON, Monsters.ID.COPPER_DRAGON_WYRMLING, Alignment.CHAOTIC_GOOD, 15, 12, 13, 14, 11, 13, 16, "4d8+4", 40, 1,
                    new Attack[] { Attacks.CopperDragonWyrmlingBite }, new Attack[] { }, Size.MEDIUM
                );
                copperDragonWyrmling.AddProficiency(Proficiency.DEXTERITY);
                copperDragonWyrmling.AddProficiency(Proficiency.CONSTITUTION);
                copperDragonWyrmling.AddProficiency(Proficiency.WISDOM);
                copperDragonWyrmling.AddProficiency(Proficiency.CHARISMA);
                copperDragonWyrmling.AddProficiency(Proficiency.PERCEPTION);
                copperDragonWyrmling.AddProficiency(Proficiency.STEALTH);
                copperDragonWyrmling.AddEffect(Effect.IMMUNITY_ACID);
                return copperDragonWyrmling;
            }
        }

        /* TODO */
        public static Monster Couatl {
            get {
                Monster couatl = new Monster(
                    Monsters.Type.CELESTIAL, Monsters.ID.COUATL, Alignment.LAWFUL_GOOD, 16, 20, 17, 18, 20, 18, 19, "13d8+39", 40, 4,
                    new Attack[] { Attacks.CouatlBite, Attacks.CouatlConstrict }, new Attack[] { }, Size.MEDIUM
                );
                couatl.AddProficiency(Proficiency.CONSTITUTION);
                couatl.AddProficiency(Proficiency.WISDOM);
                couatl.AddProficiency(Proficiency.CHARISMA);
                couatl.AddEffect(Effect.RESISTANCE_RADIANT);
                couatl.AddEffect(Effect.IMMUNITY_PSYCHIC);
                couatl.AddEffect(Effect.IMMUNITY_NONMAGIC);
                couatl.AddFeat(Feat.INNATE_SPELLCASTING_COUATL);
                couatl.AddFeat(Feat.MAGIC_WEAPONS);
                couatl.AddFeat(Feat.SHIELDED_MIND);
                return couatl;
            }
        }

        /* TODO */
        public static Monster Crab {
            get {
                Monster crab = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.CRAB, Alignment.UNALIGNED, 2, 11, 10, 1, 8, 2, 11, "1d4", 40, 0,
                    new Attack[] { Attacks.CrabClaw }, new Attack[] { }, Size.TINY
                );
                crab.AddProficiency(Proficiency.STEALTH);
                crab.AddFeat(Feat.AMPHIBIOUS);
                return crab;
            }
        }

        /* TODO */
        public static Monster Crocodile {
            get {
                Monster crocodile = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.CROCODILE, Alignment.UNALIGNED, 15, 10, 13, 2, 10, 5, 12, "3d10+3", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.CrocodileBite }, new Attack[] { }, Size.LARGE
                );
                crocodile.AddProficiency(Proficiency.STEALTH);
                crocodile.AddFeat(Feat.HOLD_BREATH_15MIN);
                return crocodile;
            }
        }

        /* TODO */
        public static Monster CultFanatic {
            get {
                Monster cultFanatic = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.CULT_FANATIC, Alignment.UNALIGNED, 11, 14, 12, 10, 13, 14, 13, "6d8-5", 40, 2,
                    new Attack[] { Attacks.CultFanaticDaggerMelee }, new Attack[] { Attacks.CultFanaticDaggerRanged }, Size.MEDIUM
                );
                cultFanatic.AddProficiency(Proficiency.DECEPTION);
                cultFanatic.AddProficiency(Proficiency.PERSUASION);
                cultFanatic.AddProficiency(Proficiency.RELIGION);
                cultFanatic.AddFeat(Feat.DARK_DEVOTION);
                cultFanatic.AddFeat(Feat.INNATE_SPELLCASTING_CULT_FANATIC);
                return cultFanatic;
            }
        }

        /* TODO */
        public static Monster Cultist {
            get {
                Monster cultist = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.CULTIST, Alignment.UNALIGNED, 11, 12, 10, 10, 11, 10, 12, "2d8", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.CultistScimitar }, new Attack[] { }, Size.MEDIUM
                );
                cultist.AddProficiency(Proficiency.DECEPTION);
                cultist.AddProficiency(Proficiency.RELIGION);
                cultist.AddFeat(Feat.DARK_DEVOTION);
                return cultist;
            }
        }
    }
}