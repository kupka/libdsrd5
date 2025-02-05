namespace srd5 {
    public partial struct Attacks {
        public static readonly AttackEffect QuasitClawEffect = delegate (Combattant attacker, Combattant target) {
            if (target.IsImmune(DamageType.POISON)) return;
            if (target.DC(QuasitClaw, 10, AbilityType.CONSTITUTION)) return;
            target.TakeDamage(attacker, DamageType.POISON, "2d4");
            target.AddEffect(Effect.QUASIT_POISON);
        };
        public static Attack QuasitClaw {
            get {
                return new Attack("Claw", 4, new Damage(DamageType.PIERCING, "1d4+3"), 5, null, QuasitClawEffect);
            }
        }
        public static Attack QuipperBite {
            get {
                return new Attack("Bite", 5, new Damage(DamageType.PIERCING, 1), 5);
            }
        }
    }


    public partial struct Monsters {
        /* TODO */
        public static Monster Quasit {
            get {
                Monster quasit = new Monster(
                    Monsters.Type.FIEND, Monsters.ID.QUASIT, Alignment.CHAOTIC_EVIL, 5, 17, 10, 7, 10, 10, 13, "3d4", 40, 1,
                    new Attack[] { Attacks.QuasitClaw }, new Attack[] { }, Size.TINY
                );
                quasit.AddProficiency(Proficiency.STEALTH);
                quasit.AddEffect(Effect.RESISTANCE_COLD);
                quasit.AddEffect(Effect.RESISTANCE_FIRE);
                quasit.AddEffect(Effect.RESISTANCE_LIGHTNING);
                quasit.AddEffect(Effect.RESISTANCE_NONMAGIC);
                quasit.AddEffect(Effect.IMMUNITY_POISON);
                quasit.AddEffect(Effect.IMMUNITY_POISONED);
                quasit.AddFeat(Feat.SHAPECHANGER_QUASIT);
                quasit.AddFeat(Feat.MAGIC_RESISTANCE);
                return quasit;
            }
        }

        /* TODO */
        public static Monster Quipper {
            get {
                Monster quipper = new Monster(
                    Monsters.Type.BEAST, Monsters.ID.QUIPPER, Alignment.UNALIGNED, 2, 16, 9, 1, 7, 2, 13, "1d4-1", 40, 0,
                    new Attack[] { Attacks.QuipperBite }, new Attack[] { }, Size.TINY
                );
                quipper.AddFeat(Feat.BLOOD_FRENZY);
                quipper.AddFeat(Feat.WATER_BREATHING);
                return quipper;
            }
        }
    }
}