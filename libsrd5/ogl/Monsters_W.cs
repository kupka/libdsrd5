namespace srd5 {
    public partial struct Attacks {
        public static Attack WarhorseHooves {
            get {
                return new Attack("Hooves", 6, new Damage(DamageType.BLUDGEONING, "2d6+4"), 5);
            }
        }
        public static Attack WarhorseSkeletonHooves {
            get {
                return new Attack("Hooves", 6, new Damage(DamageType.BLUDGEONING, "2d6+4"), 5);
            }
        }
        public static Attack WaterElementalSlam {
            get {
                return new Attack("Slam", 7, new Damage(DamageType.BLUDGEONING, "2d8+4"), 5);
            }
        }
        public static Attack WeaselBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d1"), 5);
            }
        }
        public static readonly AttackEffect WerebearBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target is Monster monster && monster.Type != Monsters.Type.HUMANOID) return;
            if (target.DC(WerebearBite, 14, AbilityType.CONSTITUTION)) return;
            target.AddEffect(Effect.CURSE_WEREBEAR);
        };
        public static Attack WerebearBite {
            get {
                return new Attack("Bite", 7, new Damage(DamageType.PIERCING, "2d10+4"), 5, null, WerebearBiteEffect);
            }
        }
        public static Attack WerebearClaw {
            get {
                return new Attack("Claw", 7, new Damage(DamageType.SLASHING, "2d8+4"), 5);
            }
        }
        public static Attack WerebearHumanFormGreataxe {
            get {
                return new Attack("Greataxe", 7, new Damage(DamageType.SLASHING, "1d12+4"), 5);
            }
        }
        public static Attack WerebearHybridFormBite {
            get {
                return new Attack("Bite", 7, new Damage(DamageType.PIERCING, "2d10+4"), 5, null, WerebearBiteEffect);
            }
        }
        public static Attack WerebearHybridFormClaw {
            get {
                return new Attack("Claw", 7, new Damage(DamageType.SLASHING, "2d8+4"), 5);
            }
        }
        public static Attack WerebearHybridFormGreataxe {
            get {
                return new Attack("Greataxe", 7, new Damage(DamageType.SLASHING, "1d12+4"), 5);
            }
        }
        public static readonly AttackEffect WereboarTusksEffect = delegate (Combattant attacker, Combattant target) {
            if (target is Monster monster && monster.Type != Monsters.Type.HUMANOID) return;
            if (target.DC(WereboarTusks, 12, AbilityType.CONSTITUTION)) return;
            target.AddEffect(Effect.CURSE_WEREBOAR);
        };
        public static Attack WereboarTusks {
            get {
                return new Attack("Tusks", 5, new Damage(DamageType.SLASHING, "2d6+3"), 5, null, WereboarTusksEffect);
            }
        }
        public static Attack WereboarHumanFormMaul {
            get {
                return new Attack("Maul", 5, new Damage(DamageType.BLUDGEONING, "2d6+3"), 5);
            }
        }
        public static Attack WereboarHybridFormTusks {
            get {
                return new Attack("Tusks", 5, new Damage(DamageType.SLASHING, "1d6+0"), 5, null, WereboarTusksEffect);
            }
        }
        public static Attack WereboarHybridFormMaul {
            get {
                return new Attack("Maul", 5, new Damage(DamageType.BLUDGEONING, "2d6+3"), 5);
            }
        }
        public static Attack WereratHumanFormShortsword {
            get {
                return new Attack("Shortsword", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack WereratHumanFormHandCrossbow {
            get {
                return new Attack("Hand Crossbow", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5, 30, 120);
            }
        }
        public static readonly AttackEffect WereratBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target is Monster monster && monster.Type != Monsters.Type.HUMANOID) return;
            if (target.DC(WereratBite, 11, AbilityType.CONSTITUTION)) return;
            target.AddEffect(Effect.CURSE_WERERAT);
        };
        public static Attack WereratHybridFormBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d4+2"), 5, null, WereratBiteEffect);
            }
        }
        public static Attack WereratHybridFormShortsword {
            get {
                return new Attack("Shortsword", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5);
            }
        }
        public static Attack WereratHybridFormHandCrossbow {
            get {
                return new Attack("Hand Crossbow", 4, new Damage(DamageType.PIERCING, "1d6+2"), 5, 30, 120);
            }
        }
        public static Attack WereratBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d4+2"), 5, null, WereratBiteEffect);
            }
        }
        public static Attack WeretigerHumanFormScimitar {
            get {
                return new Attack("Scimitar", 5, new Damage(DamageType.SLASHING, "1d6+3"), 5);
            }
        }
        public static Attack WeretigerHumanFormLongbow {
            get {
                return new Attack("Longbow", 4, new Damage(DamageType.PIERCING, "1d8+2"), 5, 150, 600);
            }
        }
        public static readonly AttackEffect WeretigerBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target is Monster monster && monster.Type != Monsters.Type.HUMANOID) return;
            if (target.DC(WeretigerBite, 13, AbilityType.CONSTITUTION)) return;
            target.AddEffect(Effect.CURSE_WERETIGER);
        };
        public static Attack WeretigerHybridFormBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d10+3"), 5, null, WeretigerBiteEffect);
            }
        }
        public static Attack WeretigerHybridFormClaw {
            get {
                return new Attack("Claw", 5, new Damage(DamageType.SLASHING, "1d8+3"), 5);
            }
        }
        public static Attack WeretigerHybridFormScimitar {
            get {
                return new Attack("Scimitar", 5, new Damage(DamageType.SLASHING, "1d6+3"), 5);
            }
        }
        public static Attack WeretigerHybridFormLongbow {
            get {
                return new Attack("Longbow", 4, new Damage(DamageType.PIERCING, "1d8+2"), 5, 150, 600);
            }
        }
        public static Attack WeretigerBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d10+3"), 5, null, WeretigerBiteEffect);
            }
        }
        public static Attack WeretigerClaw {
            get {
                return new Attack("Claw", 5, new Damage(DamageType.SLASHING, "1d8+3"), 5);
            }
        }
        public static Attack WerewolfHumanFormSpearMelee {
            get {
                return new Attack("Spear", 4, new Damage(DamageType.PIERCING, "1d8+2"), 5);
            }
        }
        public static Attack WerewolfHumanFormSpearRanged {
            get {
                return new Attack("Spear", 4, new Damage(DamageType.PIERCING, "1d8+2"), 20, 60);
            }
        }
        public static readonly AttackEffect WerewolfBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target is Monster monster && monster.Type != Monsters.Type.HUMANOID) return;
            if (target.DC(WerewolfBite, 12, AbilityType.CONSTITUTION)) return;
            target.AddEffect(Effect.CURSE_WEREWOLF);
        };
        public static Attack WerewolfHybridFormBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d6+0"), 5, null, WerewolfBiteEffect);
            }
        }
        public static Attack WerewolfHybridFormClaws {
            get {
                return new Attack("Claws", 4, new Damage(DamageType.SLASHING, "2d4+2"), 5);
            }
        }

        public static Attack WerewolfBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d8+2"), 5, null, WerewolfBiteEffect);
            }
        }
        public static Attack WhiteDragonWyrmlingBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d10+2"), 5, new Damage(DamageType.COLD, "1d4"));
            }
        }
        public static readonly AttackEffect WightLifeDrainEffect = delegate (Combattant attacker, Combattant target) {
            int damage = target.TakeDamage(DamageType.NECROTIC, "1d6+2");
            target.AddHitPointMaximumModifiers(new HitPointMaxiumModifier(-damage, HitPointMaxiumModifier.RemovedByEffect.LONG_REST));
            if (target.HitPointsMax == 0) {
                target.Die();
            }
            // TODO: A humanoid slain by this attack rises 24 hours later as a zombie under the wight's control, 
            // unless the humanoid is restored to life or its body is destroyed. 
            // The wight can have no more than twelve zombies under its control at one time.
        };
        public static Attack WightLifeDrain {
            get {
                return new Attack("Life Drain", 4, new Damage(DamageType.NECROTIC, 0), 5, null, WightLifeDrainEffect);
            }
        }
        public static Attack WightLongsword {
            get {
                return new Attack("Longsword", 4, new Damage(DamageType.SLASHING, "1d10+2"), 5);
            }
        }
        public static Attack WightLongbow {
            get {
                return new Attack("Longbow", 4, new Damage(DamageType.PIERCING, "1d8+2"), 5, 150, 600);
            }
        }
        public static Attack WilloWispShock {
            get {
                return new Attack("Shock", 4, new Damage(DamageType.LIGHTNING, "2d8"), 5);
            }
        }
        public static readonly AttackEffect WinterWolfBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target.HasEffect(Effect.IMMUNITY_PRONE)) return;
            if (target.DC(WinterWolfBite, 14, AbilityType.STRENGTH)) return;
            target.AddCondition(ConditionType.PRONE);
        };
        public static Attack WinterWolfBite {
            get {
                return new Attack("Bite", 6, new Damage(DamageType.PIERCING, "2d6+4"), 5, null, WinterWolfBiteEffect);
            }
        }
        public static readonly AttackEffect WolfBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target.HasEffect(Effect.IMMUNITY_PRONE)) return;
            if (target.DC(WolfBite, 11, AbilityType.STRENGTH)) return;
            target.AddCondition(ConditionType.PRONE);
        };
        public static Attack WolfBite {
            get {
                return new Attack("Bite", 4, new Damage(DamageType.PIERCING, "1d6+0"), 5, null, WolfBiteEffect);
            }
        }
        public static readonly AttackEffect WorgBiteEffect = delegate (Combattant attacker, Combattant target) {
            if (target.HasEffect(Effect.IMMUNITY_PRONE)) return;
            if (target.DC(WorgBite, 13, AbilityType.STRENGTH)) return;
            target.AddCondition(ConditionType.PRONE);
        };
        public static Attack WorgBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "2d6+3"), 5, null, WorgBiteEffect);
            }
        }
        public static readonly AttackEffect WraithLifeDrainEffect = delegate (Combattant attacker, Combattant target) {
            int damage = target.TakeDamage(DamageType.NECROTIC, "4d8+3");
            target.AddHitPointMaximumModifiers(new HitPointMaxiumModifier(-damage, HitPointMaxiumModifier.RemovedByEffect.LONG_REST));
            if (target.HitPointsMax == 0) {
                target.Die();
            }
        };
        public static Attack WraithLifeDrain {
            get {
                return new Attack("Life Drain", 6, new Damage(DamageType.NECROTIC, 0), 5, null, WraithLifeDrainEffect);
            }
        }
        public static readonly AttackEffect WyvernStingerEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.PoisonEffect(target, WyvernStinger, "7d6", 15);
        };
        public static Attack WyvernStinger {
            get {
                return new Attack("Stinger", 7, new Damage(DamageType.PIERCING, "2d6+4"), 5, null, WyvernStingerEffect);
            }
        }
        public static Attack WyvernBite {
            get {
                return new Attack("Bite", 7, new Damage(DamageType.PIERCING, "2d6+4"), 10);
            }
        }
        public static Attack WyvernClaws {
            get {
                return new Attack("Claws", 7, new Damage(DamageType.SLASHING, "2d8+4"), 5);
            }
        }
    }

    public partial struct Monsters {
        /* TODO */
        public static Monster Warhorse {
            get {
                Monster warhorse = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.WARHORSE, Alignment.UNALIGNED, 18, 12, 13, 2, 12, 7, 11, "3d10+3", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.WarhorseHooves }, new Attack[] { }, Size.LARGE
                );
                warhorse.AddFeat(Feat.TRAMPLING_CHARGE_HORSE);
                return warhorse;
            }
        }

        /* TODO */
        public static Monster WarhorseSkeleton {
            get {
                Monster warhorseSkeleton = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.WARHORSE_SKELETON, Alignment.LAWFUL_EVIL, 18, 12, 15, 2, 8, 5, 13, "3d10+6", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.WarhorseSkeletonHooves }, new Attack[] { }, Size.LARGE
                );
                warhorseSkeleton.AddEffect(Effect.VULNERABILITY_BLUDGEONING);
                warhorseSkeleton.AddEffect(Effect.IMMUNITY_POISON);
                warhorseSkeleton.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                warhorseSkeleton.AddEffect(Effect.IMMUNITY_POISONED);
                return warhorseSkeleton;
            }
        }

        /* TODO */
        public static Monster WaterElemental {
            get {
                Monster waterElemental = new Monster(
                    Monsters.Type.ELEMENTAL, Monsters.ID.WATER_ELEMENTAL, Alignment.NEUTRAL, 18, 14, 18, 5, 10, 8, 14, "12d10+48", 40, 5,
                    new Attack[] { Attacks.WaterElementalSlam }, new Attack[] { }, Size.LARGE
                );
                waterElemental.AddEffect(Effect.RESISTANCE_ACID);
                waterElemental.AddEffect(Effect.RESISTANCE_NONMAGIC);
                waterElemental.AddEffect(Effect.IMMUNITY_POISON);
                waterElemental.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                waterElemental.AddEffect(Effect.IMMUNITY_GRAPPLED);
                waterElemental.AddEffect(Effect.IMMUNITY_PARALYZED);
                waterElemental.AddEffect(Effect.IMMUNITY_PETRIFIED);
                waterElemental.AddEffect(Effect.IMMUNITY_POISONED);
                waterElemental.AddEffect(Effect.IMMUNITY_PRONE);
                waterElemental.AddEffect(Effect.IMMUNITY_RESTRAINED);
                waterElemental.AddEffect(Effect.IMMUNITY_UNCONSCIOUS);
                waterElemental.AddFeat(Feat.WATER_FORM);
                waterElemental.AddFeat(Feat.FREEZE);
                return waterElemental;
            }
        }

        /* TODO */
        public static Monster Weasel {
            get {
                Monster weasel = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.WEASEL, Alignment.UNALIGNED, 3, 16, 8, 2, 12, 3, 13, "1d4-1", 40, 0,
                    new Attack[] { Attacks.WeaselBite }, new Attack[] { }, Size.TINY
                );
                weasel.AddProficiency(Proficiency.PERCEPTION);
                weasel.AddProficiency(Proficiency.STEALTH);
                weasel.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                return weasel;
            }
        }

        /* TODO */
        public static Monster Werebear {
            get {
                Monster werebear = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WEREBEAR_BEAR_FORM, Alignment.NEUTRAL_GOOD, 19, 10, 17, 11, 12, 12, 11, "18d8+54", 40, 5,
                    new Attack[] { Attacks.WerebearBite, Attacks.WerebearClaw }, new Attack[] { }, Size.MEDIUM
                );
                werebear.AddProficiency(Proficiency.PERCEPTION);
                werebear.AddEffect(Effect.IMMUNITY_NONMAGIC);
                werebear.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                werebear.AddFeat(Feat.KEEN_SMELL);
                return werebear;
            }
        }

        /* TODO */
        public static Monster WerebearHumanForm {
            get {
                Monster werebearHumanForm = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WEREBEAR_HUMAN_FORM, Alignment.NEUTRAL_GOOD, 19, 10, 17, 11, 12, 12, 10, "18d8+54", 40, 5,
                    new Attack[] { Attacks.WerebearHumanFormGreataxe }, new Attack[] { }, Size.MEDIUM
                );
                werebearHumanForm.AddProficiency(Proficiency.PERCEPTION);
                werebearHumanForm.AddEffect(Effect.IMMUNITY_NONMAGIC);
                werebearHumanForm.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                werebearHumanForm.AddFeat(Feat.KEEN_SMELL);
                return werebearHumanForm;
            }
        }

        /* TODO */
        public static Monster WerebearHybridForm {
            get {
                Monster werebearHybridForm = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WEREBEAR_HYBRID_FORM, Alignment.NEUTRAL_GOOD, 19, 10, 17, 11, 12, 12, 11, "18d8+54", 40, 5,
                    new Attack[] { Attacks.WerebearHybridFormBite, Attacks.WerebearHybridFormClaw, Attacks.WerebearHybridFormGreataxe }, new Attack[] { }, Size.MEDIUM
                );
                werebearHybridForm.AddProficiency(Proficiency.PERCEPTION);
                werebearHybridForm.AddEffect(Effect.IMMUNITY_NONMAGIC);
                werebearHybridForm.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                werebearHybridForm.AddFeat(Feat.KEEN_SMELL);
                return werebearHybridForm;
            }
        }

        /* TODO */
        public static Monster Wereboar {
            get {
                Monster wereboar = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WEREBOAR_BOAR_FORM, Alignment.NEUTRAL_EVIL, 17, 10, 15, 10, 11, 8, 11, "12d8+24", 40, 4,
                    new Attack[] { Attacks.WereboarTusks }, new Attack[] { }, Size.MEDIUM
                );
                wereboar.AddProficiency(Proficiency.PERCEPTION);
                wereboar.AddEffect(Effect.IMMUNITY_NONMAGIC);
                wereboar.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                wereboar.AddFeat(Feat.CHARGE_WEREBOAR);
                wereboar.AddFeat(Feat.RELENTLESS_14);
                return wereboar;
            }
        }

        /* TODO */
        public static Monster WereboarHumanForm {
            get {
                Monster wereboarHumanForm = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WEREBOAR_HUMAN_FORM, Alignment.NEUTRAL_EVIL, 17, 10, 15, 10, 11, 8, 10, "12d8+24", 40, 4,
                    new Attack[] { Attacks.WereboarHumanFormMaul }, new Attack[] { }, Size.MEDIUM
                );
                wereboarHumanForm.AddProficiency(Proficiency.PERCEPTION);
                wereboarHumanForm.AddEffect(Effect.IMMUNITY_NONMAGIC);
                wereboarHumanForm.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                wereboarHumanForm.AddFeat(Feat.RELENTLESS_14);
                return wereboarHumanForm;
            }
        }

        /* TODO */
        public static Monster WereboarHybridForm {
            get {
                Monster wereboarHybridForm = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WEREBOAR_HYBRID_FORM, Alignment.NEUTRAL_EVIL, 17, 10, 15, 10, 11, 8, 11, "12d8+24", 40, 4,
                    new Attack[] { Attacks.WereboarHybridFormMaul, Attacks.WereboarHybridFormTusks }, new Attack[] { }, Size.MEDIUM
                );
                wereboarHybridForm.AddProficiency(Proficiency.PERCEPTION);
                wereboarHybridForm.AddEffect(Effect.IMMUNITY_NONMAGIC);
                wereboarHybridForm.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                wereboarHybridForm.AddFeat(Feat.CHARGE_WEREBOAR);
                wereboarHybridForm.AddFeat(Feat.RELENTLESS_14);
                return wereboarHybridForm;
            }
        }

        /* TODO */
        public static Monster WereratHumanForm {
            get {
                Monster wereratHumanForm = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WERERAT_HUMAN_FORM, Alignment.LAWFUL_EVIL, 10, 15, 12, 11, 10, 8, 12, "6d8+6", 40, 2,
                    new Attack[] { Attacks.WereratHumanFormShortsword }, new Attack[] { Attacks.WereratHumanFormHandCrossbow }, Size.MEDIUM
                );
                wereratHumanForm.AddProficiency(Proficiency.PERCEPTION);
                wereratHumanForm.AddProficiency(Proficiency.STEALTH);
                wereratHumanForm.AddEffect(Effect.IMMUNITY_NONMAGIC);
                wereratHumanForm.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                wereratHumanForm.AddFeat(Feat.KEEN_SMELL);
                return wereratHumanForm;
            }
        }

        /* TODO */
        public static Monster WereratHybridForm {
            get {
                Monster wereratHybridForm = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WERERAT_HYBRID_FORM, Alignment.LAWFUL_EVIL, 10, 15, 12, 11, 10, 8, 12, "6d8+6", 40, 2,
                    new Attack[] { Attacks.WereratHybridFormBite, Attacks.WereratHybridFormShortsword }, new Attack[] { Attacks.WereratHybridFormHandCrossbow }, Size.MEDIUM
                );
                wereratHybridForm.AddProficiency(Proficiency.PERCEPTION);
                wereratHybridForm.AddProficiency(Proficiency.STEALTH);
                wereratHybridForm.AddEffect(Effect.IMMUNITY_NONMAGIC);
                wereratHybridForm.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                wereratHybridForm.AddFeat(Feat.KEEN_SMELL);
                return wereratHybridForm;
            }
        }

        /* TODO */
        public static Monster Wererat {
            get {
                Monster wererat = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WERERAT_RAT_FORM, Alignment.LAWFUL_EVIL, 10, 15, 12, 11, 10, 8, 12, "6d8+6", 40, 2,
                    new Attack[] { Attacks.WereratBite }, new Attack[] { }, Size.MEDIUM
                );
                wererat.AddProficiency(Proficiency.PERCEPTION);
                wererat.AddProficiency(Proficiency.STEALTH);
                wererat.AddEffect(Effect.IMMUNITY_NONMAGIC);
                wererat.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                wererat.AddFeat(Feat.KEEN_SMELL);
                return wererat;
            }
        }

        /* TODO */
        public static Monster WeretigerHumanForm {
            get {
                Monster weretigerHumanForm = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WERETIGER_HUMAN_FORM, Alignment.NEUTRAL, 17, 15, 16, 10, 13, 11, 12, "16d8+48", 40, 4,
                    new Attack[] { Attacks.WeretigerHumanFormScimitar }, new Attack[] { Attacks.WeretigerHumanFormLongbow }, Size.MEDIUM
                );
                weretigerHumanForm.AddProficiency(Proficiency.PERCEPTION);
                weretigerHumanForm.AddProficiency(Proficiency.STEALTH);
                weretigerHumanForm.AddEffect(Effect.IMMUNITY_NONMAGIC);
                weretigerHumanForm.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                weretigerHumanForm.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                return weretigerHumanForm;
            }
        }

        /* TODO */
        public static Monster WeretigerHybridForm {
            get {
                Monster weretigerHybridForm = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WERETIGER_HYBRID_FORM, Alignment.NEUTRAL, 17, 15, 16, 10, 13, 11, 12, "16d8+48", 40, 4,
                    new Attack[] { Attacks.WeretigerHybridFormBite, Attacks.WeretigerHybridFormClaw, Attacks.WeretigerHybridFormScimitar }, new Attack[] { Attacks.WeretigerHybridFormLongbow }, Size.MEDIUM
                );
                weretigerHybridForm.AddProficiency(Proficiency.PERCEPTION);
                weretigerHybridForm.AddProficiency(Proficiency.STEALTH);
                weretigerHybridForm.AddEffect(Effect.IMMUNITY_NONMAGIC);
                weretigerHybridForm.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                weretigerHybridForm.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                weretigerHybridForm.AddFeat(Feat.POUNCE_WERETIGER);
                return weretigerHybridForm;
            }
        }

        /* TODO */
        public static Monster Weretiger {
            get {
                Monster weretiger = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WERETIGER_TIGER_FORM, Alignment.NEUTRAL, 17, 15, 16, 10, 13, 11, 12, "16d8+48", 40, 4,
                    new Attack[] { Attacks.WeretigerBite, Attacks.WeretigerClaw }, new Attack[] { }, Size.MEDIUM
                );
                weretiger.AddProficiency(Proficiency.PERCEPTION);
                weretiger.AddProficiency(Proficiency.STEALTH);
                weretiger.AddEffect(Effect.IMMUNITY_NONMAGIC);
                weretiger.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                weretiger.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                weretiger.AddFeat(Feat.POUNCE_WERETIGER);
                return weretiger;
            }
        }

        /* TODO */
        public static Monster WerewolfHumanForm {
            get {
                Monster werewolfHumanForm = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WEREWOLF_HUMAN_FORM, Alignment.CHAOTIC_EVIL, 15, 13, 14, 10, 11, 10, 11, "9d8+18", 40, 3,
                    new Attack[] { Attacks.WerewolfHumanFormSpearMelee }, new Attack[] { Attacks.WerewolfHumanFormSpearRanged }, Size.MEDIUM
                );
                werewolfHumanForm.AddProficiency(Proficiency.PERCEPTION);
                werewolfHumanForm.AddEffect(Effect.IMMUNITY_NONMAGIC);
                werewolfHumanForm.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                werewolfHumanForm.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                return werewolfHumanForm;
            }
        }

        /* TODO */
        public static Monster WerewolfHybridForm {
            get {
                Monster werewolfHybridForm = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WEREWOLF_HYBRID_FORM, Alignment.CHAOTIC_EVIL, 15, 13, 14, 10, 11, 10, 12, "9d8+18", 40, 3,
                    new Attack[] { Attacks.WerewolfHybridFormBite, Attacks.WerewolfHybridFormClaws }, new Attack[] { }, Size.MEDIUM
                );
                werewolfHybridForm.AddProficiency(Proficiency.PERCEPTION);
                werewolfHybridForm.AddEffect(Effect.IMMUNITY_NONMAGIC);
                werewolfHybridForm.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                werewolfHybridForm.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                return werewolfHybridForm;
            }
        }

        /* TODO */
        public static Monster Werewolf {
            get {
                Monster werewolf = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.WEREWOLF_WOLF_FORM, Alignment.CHAOTIC_EVIL, 15, 13, 14, 10, 11, 10, 12, "9d8+18", 40, 3,
                    new Attack[] { Attacks.WerewolfBite }, new Attack[] { }, Size.MEDIUM
                );
                werewolf.AddProficiency(Proficiency.PERCEPTION);
                werewolf.AddEffect(Effect.IMMUNITY_NONMAGIC);
                werewolf.AddFeat(Feat.SHAPECHANGER_WEREBEAST);
                werewolf.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                return werewolf;
            }
        }

        /* TODO */
        public static Monster WhiteDragonWyrmling {
            get {
                Monster whiteDragonWyrmling = new Monster(
                    Monsters.Type.DRAGON, Monsters.ID.WHITE_DRAGON_WYRMLING, Alignment.CHAOTIC_EVIL, 14, 10, 14, 5, 10, 11, 16, "5d8+10", 40, 2,
                    new Attack[] { Attacks.WhiteDragonWyrmlingBite }, new Attack[] { }, Size.MEDIUM
                );
                whiteDragonWyrmling.AddProficiency(Proficiency.DEXTERITY);
                whiteDragonWyrmling.AddProficiency(Proficiency.CONSTITUTION);
                whiteDragonWyrmling.AddProficiency(Proficiency.WISDOM);
                whiteDragonWyrmling.AddProficiency(Proficiency.CHARISMA);
                whiteDragonWyrmling.AddProficiency(Proficiency.PERCEPTION);
                whiteDragonWyrmling.AddProficiency(Proficiency.STEALTH);
                whiteDragonWyrmling.AddEffect(Effect.IMMUNITY_COLD);
                return whiteDragonWyrmling;
            }
        }

        /* TODO */
        public static Monster Wight {
            get {
                Monster wight = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.WIGHT, Alignment.NEUTRAL_EVIL, 15, 14, 16, 10, 13, 15, 14, "6d8+18", 40, 3,
                    new Attack[] { Attacks.WightLifeDrain, Attacks.WightLongsword }, new Attack[] { Attacks.WightLongbow }, Size.MEDIUM
                );
                wight.AddProficiency(Proficiency.PERCEPTION);
                wight.AddProficiency(Proficiency.STEALTH);
                wight.AddEffect(Effect.RESISTANCE_NECROTIC);
                wight.AddEffect(Effect.RESISTANCE_NONMAGIC);
                wight.AddEffect(Effect.IMMUNITY_POISON);
                wight.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                wight.AddEffect(Effect.IMMUNITY_POISONED);
                wight.AddFeat(Feat.SUNLIGHT_SENSITIVITY);
                return wight;
            }
        }

        /* TODO */
        public static Monster WilloWisp {
            get {
                Monster willoWisp = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.WILL_O_WISP, Alignment.CHAOTIC_EVIL, 1, 28, 10, 13, 14, 11, 19, "9d4", 40, 2,
                    new Attack[] { Attacks.WilloWispShock }, new Attack[] { }, Size.TINY
                );
                willoWisp.AddEffect(Effect.RESISTANCE_ACID);
                willoWisp.AddEffect(Effect.RESISTANCE_COLD);
                willoWisp.AddEffect(Effect.RESISTANCE_FIRE);
                willoWisp.AddEffect(Effect.RESISTANCE_NECROTIC);
                willoWisp.AddEffect(Effect.RESISTANCE_THUNDER);
                willoWisp.AddEffect(Effect.RESISTANCE_NONMAGIC);
                willoWisp.AddEffect(Effect.IMMUNITY_LIGHTNING);
                willoWisp.AddEffect(Effect.IMMUNITY_POISON);
                willoWisp.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                willoWisp.AddEffect(Effect.IMMUNITY_GRAPPLED);
                willoWisp.AddEffect(Effect.IMMUNITY_PARALYZED);
                willoWisp.AddEffect(Effect.IMMUNITY_POISONED);
                willoWisp.AddEffect(Effect.IMMUNITY_PRONE);
                willoWisp.AddEffect(Effect.IMMUNITY_RESTRAINED);
                willoWisp.AddEffect(Effect.IMMUNITY_UNCONSCIOUS);
                willoWisp.AddFeat(Feat.CONSUME_LIFE);
                willoWisp.AddFeat(Feat.EPHEMERAL);
                willoWisp.AddFeat(Feat.INCORPOREAL_MOVEMENT);
                willoWisp.AddFeat(Feat.VARIABLE_ILLUMINATION);
                return willoWisp;
            }
        }

        /* TODO */
        public static Monster WinterWolf {
            get {
                Monster winterWolf = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.WINTER_WOLF, Alignment.NEUTRAL_EVIL, 18, 13, 14, 7, 12, 8, 13, "10d10+20", 40, 3,
                    new Attack[] { Attacks.WinterWolfBite }, new Attack[] { }, Size.LARGE
                );
                winterWolf.AddProficiency(Proficiency.PERCEPTION);
                winterWolf.AddProficiency(Proficiency.STEALTH);
                winterWolf.AddEffect(Effect.IMMUNITY_COLD);
                winterWolf.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                winterWolf.AddFeat(Feat.PACK_TACTICS);
                winterWolf.AddFeat(Feat.SNOW_CAMOUFLAGE);
                return winterWolf;
            }
        }

        /* TODO */
        public static Monster Wolf {
            get {
                Monster wolf = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.WOLF, Alignment.UNALIGNED, 12, 15, 12, 3, 12, 6, 13, "2d8+2", 40, ChallengeRating.QUARTER,
                    new Attack[] { Attacks.WolfBite }, new Attack[] { }, Size.MEDIUM
                );
                wolf.AddProficiency(Proficiency.PERCEPTION);
                wolf.AddProficiency(Proficiency.STEALTH);
                wolf.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                wolf.AddFeat(Feat.PACK_TACTICS);
                return wolf;
            }
        }

        /* TODO */
        public static Monster Worg {
            get {
                Monster worg = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.WORG, Alignment.NEUTRAL_EVIL, 16, 13, 13, 7, 11, 8, 13, "4d10+4", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.WorgBite }, new Attack[] { }, Size.LARGE
                );
                worg.AddProficiency(Proficiency.PERCEPTION);
                worg.AddFeat(Feat.KEEN_HEARING_AND_SMELL);
                return worg;
            }
        }

        /* TODO */
        public static Monster Wraith {
            get {
                Monster wraith = new Monster(
                    Monsters.Type.UNDEAD, Monsters.ID.WRAITH, Alignment.NEUTRAL_EVIL, 6, 16, 16, 12, 14, 15, 13, "9d8+27", 40, 5,
                    new Attack[] { Attacks.WraithLifeDrain }, new Attack[] { }, Size.MEDIUM
                );
                wraith.AddEffect(Effect.RESISTANCE_ACID);
                wraith.AddEffect(Effect.RESISTANCE_COLD);
                wraith.AddEffect(Effect.RESISTANCE_FIRE);
                wraith.AddEffect(Effect.RESISTANCE_LIGHTNING);
                wraith.AddEffect(Effect.RESISTANCE_THUNDER);
                wraith.AddEffect(Effect.RESISTANCE_NONMAGIC);
                wraith.AddEffect(Effect.IMMUNITY_NECROTIC);
                wraith.AddEffect(Effect.IMMUNITY_POISON);
                wraith.AddEffect(Effect.IMMUNITY_CHARMED);
                wraith.AddEffect(Effect.IMMUNITY_EXHAUSTION);
                wraith.AddEffect(Effect.IMMUNITY_GRAPPLED);
                wraith.AddEffect(Effect.IMMUNITY_PARALYZED);
                wraith.AddEffect(Effect.IMMUNITY_PETRIFIED);
                wraith.AddEffect(Effect.IMMUNITY_POISONED);
                wraith.AddEffect(Effect.IMMUNITY_PRONE);
                wraith.AddEffect(Effect.IMMUNITY_RESTRAINED);
                wraith.AddFeat(Feat.INCORPOREAL_MOVEMENT);
                wraith.AddFeat(Feat.SUNLIGHT_SENSITIVITY);
                return wraith;
            }
        }

        /* TODO */
        public static Monster Wyvern {
            get {
                Monster wyvern = new Monster(
                    Monsters.Type.DRAGON, Monsters.ID.WYVERN, Alignment.UNALIGNED, 19, 10, 16, 5, 12, 6, 13, "13d10+39", 40, 6,
                    new Attack[] { Attacks.WyvernBite, Attacks.WyvernClaws, Attacks.WyvernStinger }, new Attack[] { }, Size.LARGE
                );
                wyvern.AddProficiency(Proficiency.PERCEPTION);
                return wyvern;
            }
        }
    }
}