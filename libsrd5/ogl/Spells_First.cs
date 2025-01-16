using System;

namespace srd5 {
    public partial struct Spells {
        /* TODO */
        public static readonly Spell Alarm = new Spell(Spells.ID.ALARM, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.ONE_MINUTE, 30, VSM, SpellDuration.EIGHT_HOURS, 20, 0, doNothing);
        /* TODO */
        public static readonly Spell AnimalFriendship = new Spell(Spells.ID.ANIMAL_FRIENDSHIP, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_DAY, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Bane = new Spell(Spells.ID.BANE, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Bless = new Spell(Spells.ID.BLESS, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell BurningHands = new Spell(Spells.ID.BURNING_HANDS, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS, SpellDuration.INSTANTANEOUS, 15, 0, doNothing);

        public static readonly Spell CharmPerson = new Spell(
            ID.CHARM_PERSON, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VS,
            SpellDuration.ONE_HOUR, 0, 20, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                // one target per slot
                for (int i = 0; i < (int)slot && i < targets.Length; i++) {
                    Combattant target = targets[i];
                    // only affect humanoid monsters
                    if (target is Monster monster) {
                        if (monster.Type != Monsters.Type.HUMANOID) {
                            GlobalEvents.AffectBySpell(caster, ID.CHARM_PERSON, monster, false);
                            continue;
                        }
                    }
                    // Wisdom save with advantage since we assume a fight
                    if (!target.DC(ID.CHARM_PERSON, dc, AbilityType.WISDOM, true) && target.AddCondition(ConditionType.CHARMED)) {
                        GlobalEvents.AffectBySpell(caster, ID.CHARM_PERSON, target, true);
                    } else {
                        GlobalEvents.AffectBySpell(caster, ID.CHARM_PERSON, target, false);
                    }
                }
            }
        );

        /* TODO */
        public static readonly Spell ColorSpray = new Spell(Spells.ID.COLOR_SPRAY, SpellSchool.ILLUSION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_ROUND, 15, 0, doNothing);
        /* TODO */
        public static readonly Spell Command = new Spell(Spells.ID.COMMAND, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 60, V, SpellDuration.ONE_ROUND, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ComprehendLanguages = new Spell(Spells.ID.COMPREHEND_LANGUAGES, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);

        public static readonly Spell CreateorDestroyWater = new Spell(
            ID.CREATE_OR_DESTROY_WATER, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VS,
            SpellDuration.INSTANTANEOUS, 0, 0, doNothing
        );

        public static readonly Spell CureWounds = new Spell(
            ID.CURE_WOUNDS, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 5, VS,
            SpellDuration.INSTANTANEOUS, 0, 1, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                if (targets[0] is Monster monster) {
                    if (monster.Type == Monsters.Type.CONSTRUCT || monster.Type == Monsters.Type.UNDEAD) {
                        GlobalEvents.AffectBySpell(caster, ID.CURE_WOUNDS, monster, false);
                        return;
                    }
                }
                int dices = (int)slot;
                GlobalEvents.AffectBySpell(caster, ID.CURE_WOUNDS, targets[0], true);
                Dices healed = new Dices(dices, 8, modifier);
                targets[0].HealDamage(healed.Roll());
            }
        );

        /* TODO */
        public static readonly Spell DetectEvilandGood = new Spell(Spells.ID.DETECT_EVIL_AND_GOOD, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS, SpellDuration.TEN_MINUTES, 30, 0, doNothing);

        public static readonly Spell DetectMagic = new Spell(
            ID.DETECT_MAGIC, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS,
            SpellDuration.TEN_MINUTES, 30, 0, doNothing
        );

        public static readonly Spell DetectPoisonAndDisease = new Spell(
            ID.DETECT_POISON_AND_DISEASE, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM,
            SpellDuration.TEN_MINUTES, 30, 0, doNothing
        );

        /* TODO */
        public static readonly Spell DisguiseSelf = new Spell(Spells.ID.DISGUISE_SELF, SpellSchool.ILLUSION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell DivineFavor = new Spell(Spells.ID.DIVINE_FAVOR, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 0, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);

        public static readonly Spell Entangle = new Spell(
            ID.ENTANGLE, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 90, VS,
            SpellDuration.ONE_MINUTE, 20, 20, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                foreach (Combattant target in targets) {
                    if (target.DC(ID.ENTANGLE, dc, AbilityType.STRENGTH)) {
                        GlobalEvents.AffectBySpell(caster, ID.ENTANGLE, target, false);
                    } else {
                        GlobalEvents.AffectBySpell(caster, ID.ENTANGLE, target, true);
                        target.AddEffect(Effect.ENTANGLE);
                        int rounds = 10;
                        target.AddEndOfTurnEvent(delegate () {
                            rounds--;
                            bool done = rounds <= 0;
                            if (done)
                                target.RemoveEffect(Effect.ENTANGLE);
                            return done;
                        });
                    }
                }
            }
        );

        /* TODO */
        public static readonly Spell ExpeditiousRetreat = new Spell(Spells.ID.EXPEDITIOUS_RETREAT, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 0, VS, SpellDuration.TEN_MINUTES, 0, 0, doNothing);

        public static readonly Spell FaerieFire = new Spell(
            ID.FAERIE_FIRE, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 60, V,
            SpellDuration.ONE_MINUTE, 20, 20, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                foreach (Combattant target in targets) {
                    if (target.DC(ID.FAERIE_FIRE, dc, AbilityType.DEXTERITY)) {
                        GlobalEvents.AffectBySpell(caster, ID.FAERIE_FIRE, target, false);
                    } else {
                        GlobalEvents.AffectBySpell(caster, ID.FAERIE_FIRE, target, true);
                        target.AddEffect(Effect.FAIRIE_FIRE);
                        int rounds = 10;
                        target.AddEndOfTurnEvent(delegate () {
                            rounds--;
                            bool done = rounds <= 0;
                            if (done)
                                target.RemoveEffect(Effect.FAIRIE_FIRE);
                            return done;
                        });
                    }
                }
            }
        );

        /* TODO */
        public static readonly Spell FalseLife = new Spell(Spells.ID.FALSE_LIFE, SpellSchool.NECROMANCY, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell FeatherFall = new Spell(Spells.ID.FEATHER_FALL, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.REACTION, 60, VM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell FindFamiliar = new Spell(Spells.ID.FIND_FAMILIAR, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_HOUR, 10, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell FloatingDisk = new Spell(Spells.ID.FLOATING_DISK, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);

        public static readonly Spell FogCloud = new Spell(
            ID.FOG_CLOUD, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 120, VS,
            SpellDuration.ONE_HOUR, 20, 0, doNothing
        );

        /* TODO */
        public static readonly Spell Goodberry = new Spell(Spells.ID.GOODBERRY, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Grease = new Spell(Spells.ID.GREASE, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_MINUTE, 10, 0, doNothing);
        /* TODO */
        public static readonly Spell GuidingBolt = new Spell(Spells.ID.GUIDING_BOLT, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 120, VS, SpellDuration.ONE_ROUND, 0, 0, doNothing);

        public static readonly Spell HealingWord = new Spell(
            ID.HEALING_WORD, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 60, V,
            SpellDuration.INSTANTANEOUS, 0, 1, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                if (targets[0] is Monster monster) {
                    if (monster.Type == Monsters.Type.CONSTRUCT || monster.Type == Monsters.Type.UNDEAD) {
                        GlobalEvents.AffectBySpell(caster, ID.HEALING_WORD, monster, false);
                        return;
                    };
                }
                int dices = (int)slot;
                Dices healed = new Dices(dices, 4, modifier);
                GlobalEvents.AffectBySpell(caster, ID.HEALING_WORD, targets[0], true);
                targets[0].HealDamage(healed.Roll());
            }
        );

        /* TODO */
        public static readonly Spell HellishRebuke = new Spell(Spells.ID.HELLISH_REBUKE, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.REACTION, 60, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Heroism = new Spell(Spells.ID.HEROISM, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell HideousLaughter = new Spell(Spells.ID.HIDEOUS_LAUGHTER, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell HuntersMark = new Spell(Spells.ID.HUNTERS_MARK, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 90, V, SpellDuration.ONE_HOUR, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Identify = new Spell(Spells.ID.IDENTIFY, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_MINUTE, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell IllusoryScript = new Spell(Spells.ID.ILLUSORY_SCRIPT, SpellSchool.ILLUSION, SpellLevel.FIRST, CastingTime.ONE_MINUTE, 0, SM, SpellDuration.TEN_DAYS, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell InflictWounds = new Spell(Spells.ID.INFLICT_WOUNDS, SpellSchool.NECROMANCY, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS, SpellDuration.INSTANTANEOUS, 0, 0, doNothing);

        public static readonly Spell Jump = new Spell(
            ID.JUMP, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM,
            SpellDuration.ONE_MINUTE, 0, 1, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                Combattant target = targets[0];
                GlobalEvents.AffectBySpell(caster, ID.JUMP, target, true);
                target.AddEffect(Effect.JUMP);
            }
        );

        public static readonly Spell Longstrider = new Spell(
            ID.LONGSTRIDER, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM,
            SpellDuration.ONE_HOUR, 0, 1, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                Combattant target = targets[0];
                GlobalEvents.AffectBySpell(caster, ID.LONGSTRIDER, target, true);
                target.AddEffect(Effect.LONGSTRIDER);
            }
        );

        /* TODO */
        public static readonly Spell MageArmor = new Spell(Spells.ID.MAGE_ARMOR, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.EIGHT_HOURS, 0, 0, doNothing);

        public static readonly Spell MagicMissile = new Spell(
            ID.MAGIC_MISSILE, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 120, VS,
            SpellDuration.INSTANTANEOUS, 0, 20, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                Damage damage = new Damage(DamageType.FORCE, "1d4+1");
                int missilesTotal = (int)slot + 2;
                foreach (Combattant possibleTarget in targets) {
                    Combattant target = possibleTarget;
                    if (target.HasFeat(Feat.REFLECTIVE_CARAPACE)) {
                        GlobalEvents.ActivateFeat(target, Feat.REFLECTIVE_CARAPACE);
                        GlobalEvents.AffectBySpell(caster, ID.MAGIC_MISSILE, target, false);
                        if (Dice.D6.Value == 6) {
                            target = caster;
                        }
                    }
                    GlobalEvents.AffectBySpell(caster, ID.MAGIC_MISSILE, target, true);
                    int bonusMissiles = missilesTotal % targets.Length;
                    bonusMissiles = Math.Min(1, bonusMissiles);
                    missilesTotal -= bonusMissiles;
                    for (int m = 0; m < missilesTotal / targets.Length + bonusMissiles; m++) {
                        target.TakeDamage(damage.Type, damage.Dices.Roll());
                    }
                }
            }
        );

        /* TODO */
        public static readonly Spell ProtectionfromEvilandGood = new Spell(Spells.ID.PROTECTION_FROM_EVIL_AND_GOOD, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */

        public static readonly Spell PurifyFoodAndDrink = new Spell(
            ID.PURIFY_FOOD_AND_DRINK, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 10, VS,
            SpellDuration.INSTANTANEOUS, 5, 0, doNothing
        );

        public static readonly Spell Sanctuary = new Spell(Spells.ID.SANCTUARY, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell Shield = new Spell(Spells.ID.SHIELD, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.REACTION, 0, VS, SpellDuration.ONE_ROUND, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell ShieldofFaith = new Spell(Spells.ID.SHIELD_OF_FAITH, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 60, VSM, SpellDuration.TEN_MINUTES, 0, 0, doNothing);
        /* TODO */
        public static readonly Spell SilentImage = new Spell(Spells.ID.SILENT_IMAGE, SpellSchool.ILLUSION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.TEN_MINUTES, 15, 0, doNothing);
        /* TODO */
        public static readonly Spell Sleep = new Spell(Spells.ID.SLEEP, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 90, VSM, SpellDuration.ONE_MINUTE, 20, 99,
            delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                Dices dices = new Dices(5 + ((int)slot - 1) * 2, Dice.D8.MaxValue, 0);
                int remainingHitpoints = dices.Roll();
                Array.Sort(targets, (t1, t2) => {
                    return t1.HitPoints.CompareTo(t2.HitPoints);
                });
                for (int i = 0; i < targets.Length && targets[i].HitPoints <= remainingHitpoints; i++) {
                    Combattant target = targets[i];
                    // ignoring unconscious creatures
                    if (target.HasCondition(ConditionType.UNCONSCIOUS)) continue;
                    // Undead and creatures immune to being charmed aren't affected by this spell
                    if (target.HasEffect(Effect.IMMUNITY_CHARMED)) continue;
                    if (target is Monster monster && monster.Type == Monsters.Type.UNDEAD) continue;
                    // substract hitpoints and make target unconcious for one minute (10 rounds)
                    int remainingRounds = 10;
                    remainingHitpoints -= target.HitPoints;
                    target.AddCondition(ConditionType.UNCONSCIOUS);
                    bool targetTookDamage = false;
                    target.AddDamageTakenEvent(delegate () {
                        targetTookDamage = true;
                        return true;
                    });

                    target.AddEndOfTurnEvent(delegate () {
                        remainingRounds--;
                        if (targetTookDamage || remainingRounds < 1) {
                            target.RemoveCondition(ConditionType.UNCONSCIOUS);
                            return true;
                        }
                        return false;
                    });
                }
            });

        public static readonly Spell SpeakWithAnimals = new Spell(
            ID.SPEAK_WITH_ANIMALS, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS,
            SpellDuration.TEN_MINUTES, 0, 0, doNothing
        );

        public static readonly Spell Thunderwave = new Spell(
            ID.THUNDERWAVE, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 15, VS,
            SpellDuration.INSTANTANEOUS, 15, 20, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                int dices = (int)slot + 1;
                bool pushed = true;
                foreach (Combattant target in targets) {
                    GlobalEvents.AffectBySpell(caster, ID.THUNDERWAVE, target, true);
                    if (target.DC(ID.THUNDERWAVE, dc, AbilityType.CONSTITUTION)) {
                        dices = (int)(dices / 2);
                        pushed = false;
                    }
                    Damage damage = new Damage(DamageType.THUNDER, dices + "d8");
                    target.TakeDamage(damage.Type, damage.Dices.Roll());
                    if (pushed) {
                        ground.Push(ground.LocateCombattant(caster), target, 10);
                    }
                }
            }
        );

        /* TODO */
        public static readonly Spell UnseenServant = new Spell(Spells.ID.UNSEEN_SERVANT, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_HOUR, 0, 0, doNothing);

    }
}