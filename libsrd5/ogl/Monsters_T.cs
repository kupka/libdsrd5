namespace srd5 {
    public partial struct Attacks {
        public static readonly AttackEffect TarrasqueBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 20, Size.GARGANTUAN, true, TarrasqueBite, 1);
        };
        public static Attack TarrasqueBite {
            get {
                return new Attack("Bite", 19, new Damage(DamageType.PIERCING, "4d12+10"), 10, null, TarrasqueBiteEffect);
            }
        }
        public static readonly AttackEffect TarrasqueTailEffect = delegate (Combattant attacker, Combattant target) {
            if (target.HasEffect(Effect.IMMUNITY_PRONE)) return;
            if (target.DC(TarrasqueTail, 20, AbilityType.STRENGTH)) return;
            target.AddCondition(ConditionType.PRONE);
        };
        public static Attack TarrasqueTail {
            get {
                return new Attack("Tail", 19, new Damage(DamageType.BLUDGEONING, "4d6+10"), 20, null, TarrasqueTailEffect);
            }
        }
        public static Attack TarrasqueClaw {
            get {
                return new Attack("Claw", 19, new Damage(DamageType.SLASHING, "4d8+10"), 15);
            }
        }
        public static Attack TarrasqueHorns {
            get {
                return new Attack("Horns", 19, new Damage(DamageType.PIERCING, "4d10+10"), 10);
            }
        }
        public static Attack ThugMace {
            get {
                return new Attack("Mace", 4, new Damage(DamageType.BLUDGEONING, "1d6+2"), 5);
            }
        }
        public static Attack ThugHeavyCrossbow {
            get {
                return new Attack("Heavy Crossbow", 2, new Damage(DamageType.PIERCING, "1d10"), 5, 100, 400);
            }
        }
        public static Attack TigerBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, "1d10+3"), 5);
            }
        }
        public static Attack TigerClaw {
            get {
                return new Attack("Claw", 5, new Damage(DamageType.SLASHING, "1d8+3"), 5);
            }
        }
        public static Attack TreantSlam {
            get {
                return new Attack("Slam", 10, new Damage(DamageType.BLUDGEONING, "3d6+6"), 5);
            }
        }
        public static Attack TreantRock {
            get {
                return new Attack("Rock", 10, new Damage(DamageType.BLUDGEONING, "4d10+6"), 5, 60, 180);
            }
        }
        public static Attack TribalWarriorSpear {
            get {
                return new Attack("Spear", 3, new Damage(DamageType.PIERCING, "1d8+1"), 5);
            }
        }
        public static Attack TribalWarriorSpearRanged {
            get {
                return new Attack("Spear", 3, new Damage(DamageType.PIERCING, "1d6+1"), 20, 60);
            }
        }
        public static readonly AttackEffect TriceratopsStompEffect = delegate (Combattant attacker, Combattant target) {
            if (!target.HasCondition(ConditionType.PRONE)) return;
            target.TakeDamage(attacker, DamageType.BLUDGEONING, "3d10+6");
        };
        public static Attack TriceratopsStomp {
            get {
                return new Attack("Stomp", 9, new Damage(DamageType.BLUDGEONING, 0), 5, null, TriceratopsStompEffect);
            }
        }
        public static Attack TriceratopsGore {
            get {
                return new Attack("Gore", 9, new Damage(DamageType.PIERCING, "4d8+6"), 5);
            }
        }
        public static Attack TrollBite {
            get {
                return new Attack("Bite", 7, new Damage(DamageType.PIERCING, "1d6+4"), 5);
            }
        }
        public static Attack TrollClaw {
            get {
                return new Attack("Claw", 7, new Damage(DamageType.SLASHING, "2d6+4"), 5);
            }
        }
        public static readonly AttackEffect TyrannosaurusRexBiteEffect = delegate (Combattant attacker, Combattant target) {
            AttackEffects.GrapplingEffect(attacker, target, 17, Size.MEDIUM, true, TyrannosaurusRexBite);
        };
        public static Attack TyrannosaurusRexBite {
            get {
                return new Attack("Bite", 10, new Damage(DamageType.PIERCING, "4d12+7"), 5, null, TyrannosaurusRexBiteEffect);
            }
        }
        public static Attack TyrannosaurusRexTail {
            get {
                return new Attack("Tail", 10, new Damage(DamageType.BLUDGEONING, "3d8+7"), 10);
            }
        }
    }

    public partial struct Monsters {
        /* TODO */
        public static Monster Tarrasque {
            get {
                Monster tarrasque = new Monster(
                    Monsters.Type.MONSTROSITY, Monsters.ID.TARRASQUE, Alignment.UNALIGNED, 30, 11, 30, 3, 11, 11, 25, "33d20+330", 40, 30,
                    new Attack[] { Attacks.TarrasqueBite, Attacks.TarrasqueClaw, Attacks.TarrasqueHorns, Attacks.TarrasqueTail }, new Attack[] { }, Size.GARGANTUAN
                );
                tarrasque.AddProficiency(Proficiency.INTELLIGENCE);
                tarrasque.AddProficiency(Proficiency.WISDOM);
                tarrasque.AddProficiency(Proficiency.CHARISMA);
                tarrasque.AddEffect(Effect.IMMUNITY_FIRE);
                tarrasque.AddEffect(Effect.IMMUNITY_POISON);
                tarrasque.AddEffect(Effect.IMMUNITY_NONMAGIC);
                tarrasque.AddEffect(Effect.IMMUNITY_CHARMED);
                tarrasque.AddEffect(Effect.IMMUNITY_FRIGHTENED);
                tarrasque.AddEffect(Effect.IMMUNITY_PARALYZED);
                tarrasque.AddEffect(Effect.IMMUNITY_POISONED);
                tarrasque.AddFeat(Feat.LEGENDARY_RESISTANCE);
                tarrasque.AddFeat(Feat.MAGIC_RESISTANCE);
                tarrasque.AddFeat(Feat.REFLECTIVE_CARAPACE);
                tarrasque.AddFeat(Feat.SIEGE_MONSTER);
                return tarrasque;
            }
        }

        /* TODO */
        public static Monster Thug {
            get {
                Monster thug = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.THUG, Alignment.UNALIGNED, 15, 11, 14, 10, 10, 11, 11, "5d8+10", 40, ChallengeRating.HALF,
                    new Attack[] { Attacks.ThugMace }, new Attack[] { Attacks.ThugHeavyCrossbow }, Size.MEDIUM
                );
                thug.AddProficiency(Proficiency.INTIMIDATION);
                thug.AddFeat(Feat.PACK_TACTICS);
                return thug;
            }
        }

        /* TODO */
        public static Monster Tiger {
            get {
                Monster tiger = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.TIGER, Alignment.UNALIGNED, 17, 15, 14, 3, 12, 8, 12, "5d10+10", 40, 1,
                    new Attack[] { Attacks.TigerBite, Attacks.TigerClaw }, new Attack[] { }, Size.LARGE
                );
                tiger.AddProficiency(Proficiency.PERCEPTION);
                tiger.AddProficiency(Proficiency.STEALTH);
                tiger.AddFeat(Feat.KEEN_SMELL);
                tiger.AddFeat(Feat.POUNCE_TIGER_13);
                return tiger;
            }
        }

        /* TODO */
        public static Monster Treant {
            get {
                Monster treant = new Monster(
                    Monsters.Type.PLANT, Monsters.ID.TREANT, Alignment.CHAOTIC_GOOD, 23, 8, 21, 12, 16, 12, 16, "12d12+60", 40, 9,
                    new Attack[] { Attacks.TreantSlam }, new Attack[] { Attacks.TreantRock }, Size.HUGE
                );
                treant.AddEffect(Effect.VULNERABILITY_FIRE);
                treant.AddEffect(Effect.RESISTANCE_BLUDGEONING);
                treant.AddEffect(Effect.RESISTANCE_PIERCING);
                treant.AddFeat(Feat.FALSE_APPEARANCE);
                treant.AddFeat(Feat.SIEGE_MONSTER);
                return treant;
            }
        }

        /* TODO */
        public static Monster TribalWarrior {
            get {
                Monster tribalWarrior = new Monster(
                    Monsters.Type.HUMANOID, Monsters.ID.TRIBAL_WARRIOR, Alignment.UNALIGNED, 13, 11, 12, 8, 11, 8, 12, "2d8+2", 40, ChallengeRating.EIGHTH,
                    new Attack[] { Attacks.TribalWarriorSpear }, new Attack[] { Attacks.TribalWarriorSpearRanged }, Size.MEDIUM
                );
                tribalWarrior.AddFeat(Feat.PACK_TACTICS);
                return tribalWarrior;
            }
        }

        /* TODO */
        public static Monster Triceratops {
            get {
                Monster triceratops = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.TRICERATOPS, Alignment.UNALIGNED, 22, 9, 17, 2, 11, 5, 13, "10d12+30", 40, 5,
                    new Attack[] { Attacks.TriceratopsGore, Attacks.TriceratopsStomp }, new Attack[] { }, Size.HUGE
                );
                triceratops.AddFeat(Feat.TRAMPLING_CHARGE_TRICERATOPS);
                return triceratops;
            }
        }

        /* TODO */
        public static Monster Troll {
            get {
                Monster troll = new Monster(
                    Monsters.Type.GIANT, Monsters.ID.TROLL, Alignment.CHAOTIC_EVIL, 18, 13, 20, 7, 9, 7, 15, "8d10+40", 40, 5,
                    new Attack[] { Attacks.TrollBite, Attacks.TrollClaw }, new Attack[] { }, Size.LARGE
                );
                troll.AddProficiency(Proficiency.PERCEPTION);
                troll.AddFeat(Feat.KEEN_SMELL);
                troll.AddFeat(Feat.REGENERATION);
                return troll;
            }
        }

        /* TODO */
        public static Monster TyrannosaurusRex {
            get {
                Monster tyrannosaurusRex = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.TYRANNOSAURUS_REX, Alignment.UNALIGNED, 25, 10, 19, 2, 12, 9, 13, "13d12+52", 40, 8,
                    new Attack[] { Attacks.TyrannosaurusRexBite, Attacks.TyrannosaurusRexTail }, new Attack[] { }, Size.HUGE
                );
                tyrannosaurusRex.AddProficiency(Proficiency.PERCEPTION);
                return tyrannosaurusRex;
            }
        }

    }
}