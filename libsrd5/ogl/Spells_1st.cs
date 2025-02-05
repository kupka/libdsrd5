using System;

namespace srd5 {
    public partial struct Spells {
        public static readonly Spell Alarm = new Spell(ID.ALARM, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.ONE_MINUTE, 30, VSM, SpellDuration.EIGHT_HOURS, 20, 0, SpellWithoutEffect(ID.ALARM));

        public static readonly Spell AnimalFriendship = new Spell(ID.ANIMAL_FRIENDSHIP, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_DAY, 0, 1, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Combattant target = targets[0];
            if (target is Monster monster
                    && monster.Type == Monsters.Type.BEAST
                    && monster.Intelligence.Value < 4
                    && !monster.HasEffect(Effect.IMMUNITY_CHARMED)
                    // Wisdom save with advantage since we assume a fight
                    && !monster.DC(ID.ANIMAL_FRIENDSHIP, dc, AbilityType.WISDOM, false, true)) {
                monster.AddCondition(ConditionType.CHARMED);
                GlobalEvents.AffectBySpell(caster, ID.ANIMAL_FRIENDSHIP, target, true);
            } else {
                GlobalEvents.AffectBySpell(caster, ID.ANIMAL_FRIENDSHIP, target, false);
            }
        });
        public static readonly Spell Bane = new Spell(ID.BANE, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 3, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            foreach (Combattant target in targets) {
                if (target.DC(ID.BANE, dc, AbilityType.CHARISMA)) {
                    GlobalEvents.AffectBySpell(caster, ID.BANE, target, false);
                } else {
                    target.AddEffect(Effect.SPELL_BANE);
                    GlobalEvents.AffectBySpell(caster, ID.BANE, target, true);
                    int maxRounds = 10;
                    target.AddEndOfTurnEvent(delegate () {
                        if (maxRounds++ > 10) {
                            target.RemoveEffect(Effect.SPELL_BANE);
                            return true;
                        } else {
                            return false;
                        }
                    });
                }
            }
        });

        public static readonly Spell Bless = new Spell(ID.BLESS, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            foreach (Combattant target in targets) {
                target.AddEffect(Effect.SPELL_BLESS);
                GlobalEvents.AffectBySpell(caster, ID.BLESS, target, true);
                int maxRounds = 10;
                target.AddEndOfTurnEvent(delegate () {
                    if (maxRounds++ > 10) {
                        target.RemoveEffect(Effect.SPELL_BLESS);
                        return true;
                    } else {
                        return false;
                    }
                });
            }
        });

        public static readonly Spell BurningHands = new Spell(ID.BURNING_HANDS, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS, SpellDuration.INSTANTANEOUS, 15, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Dice dice = DiceSlotScaling(SpellLevel.FIRST, slot, 6, 3);
            foreach (Combattant target in targets) {
                target.TakeDamage(ID.BURNING_HANDS, DamageType.FIRE, dice.Roll(), DCEffect.HALVES_DAMAGE, dc, AbilityType.DEXTERITY, out _);
                GlobalEvents.AffectBySpell(caster, ID.BURNING_HANDS, target, true);
            }
        });

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

        public static readonly Spell ColorSpray = new Spell(ID.COLOR_SPRAY, SpellSchool.ILLUSION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_ROUND, 15, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Dice dice = DiceSlotScaling(SpellLevel.FIRST, slot, 10, 5, 0, 2);
            int remainingHitpoints = dice.Roll();
            Array.Sort(targets, (t1, t2) => {
                return t1.HitPoints.CompareTo(t2.HitPoints);
            });
            for (int i = 0; i < targets.Length && targets[i].HitPoints <= remainingHitpoints; i++) {
                Combattant target = targets[i];
                // ignoring unconscious creatures
                if (target.HasCondition(ConditionType.UNCONSCIOUS)) continue;
                // Creatures immune to being blinded aren't affected by this spell
                if (target.HasEffect(Effect.IMMUNITY_BLINDED)) continue;
                // substract hitpoints and make target blind for one round
                remainingHitpoints -= target.HitPoints;
                target.AddCondition(ConditionType.BLINDED);
                caster.AddStartOfTurnEvent(delegate () {
                    target.RemoveCondition(ConditionType.BLINDED);
                    return true;
                });
            }
        });

        public static readonly Spell Command = new Spell(ID.COMMAND, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 60, V, SpellDuration.ONE_ROUND, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            // Command knows multiple effects, but here only the "Grovel" command seems to be feasible for implementation
            Combattant target = targets[0];
            bool affected = true;
            if (target is Monster monster && monster.Type == Monsters.Type.UNDEAD) {
                affected = false;
            } else if (target.DC(ID.COMMAND, dc, AbilityType.WISDOM)) {
                affected = false;
            } else {
                target.AddCondition(ConditionType.PRONE);
                target.AddEffect(Effect.SPELL_COMMAND_GROVEL);
                target.AddEndOfTurnEvent(delegate () {
                    target.RemoveEffect(Effect.SPELL_COMMAND_GROVEL);
                    return true;
                });
            }
            GlobalEvents.AffectBySpell(caster, ID.COMMAND, target, affected);
        });

        public static readonly Spell ComprehendLanguages = new Spell(ID.COMPREHEND_LANGUAGES, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, SpellWithoutEffect(ID.COMPREHEND_LANGUAGES));

        public static readonly Spell CreateorDestroyWater = new Spell(
            ID.CREATE_OR_DESTROY_WATER, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VS,
            SpellDuration.INSTANTANEOUS, 0, 0, SpellWithoutEffect(ID.CREATE_OR_DESTROY_WATER)
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
                Dice dice = DiceSlotScaling(SpellLevel.FIRST, slot, 8, 1, modifier);
                GlobalEvents.AffectBySpell(caster, ID.CURE_WOUNDS, targets[0], true);
                targets[0].HealDamage(dice.Roll());
            }
        );

        public static readonly Spell DetectEvilandGood = new Spell(ID.DETECT_EVIL_AND_GOOD, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS, SpellDuration.TEN_MINUTES, 30, 0, SpellWithoutEffect(ID.DETECT_EVIL_AND_GOOD));

        public static readonly Spell DetectMagic = new Spell(
            ID.DETECT_MAGIC, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS,
            SpellDuration.TEN_MINUTES, 30, 0, SpellWithoutEffect(ID.DETECT_MAGIC)
        );

        public static readonly Spell DetectPoisonAndDisease = new Spell(
            ID.DETECT_POISON_AND_DISEASE, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM,
            SpellDuration.TEN_MINUTES, 30, 0, SpellWithoutEffect(ID.DETECT_POISON_AND_DISEASE)
        );

        public static readonly Spell DisguiseSelf = new Spell(ID.DISGUISE_SELF, SpellSchool.ILLUSION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_HOUR, 0, 0, SpellWithoutEffect(ID.DISGUISE_SELF));

        public static readonly Spell DivineFavor = new Spell(ID.DIVINE_FAVOR, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 0, VS, SpellDuration.ONE_MINUTE, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            GlobalEvents.AffectBySpell(caster, ID.DIVINE_FAVOR, caster, true);
            caster.AddEffect(Effect.SPELL_DIVINE_FAVOR);
            int maxRounds = 10;
            caster.AddEndOfTurnEvent(delegate () {
                if (maxRounds++ > 10) {
                    caster.RemoveEffect(Effect.SPELL_DIVINE_FAVOR);
                    return true;
                } else {
                    return false;
                }
            });
        });

        public static readonly Spell Entangle = new Spell(
            ID.ENTANGLE, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 90, VS,
            SpellDuration.ONE_MINUTE, 20, 20, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                foreach (Combattant target in targets) {
                    if (target.DC(ID.ENTANGLE, dc, AbilityType.STRENGTH)) {
                        GlobalEvents.AffectBySpell(caster, ID.ENTANGLE, target, false);
                    } else {
                        GlobalEvents.AffectBySpell(caster, ID.ENTANGLE, target, true);
                        target.AddEffect(Effect.SPELL_ENTANGLE);
                        target.AddCondition(ConditionType.RESTRAINED);
                        int rounds = 10;
                        target.AddEndOfTurnEvent(delegate () {
                            rounds--;
                            bool done = rounds <= 0;
                            if (done) {
                                target.RemoveEffect(Effect.SPELL_ENTANGLE);
                                target.RemoveCondition(ConditionType.RESTRAINED);
                            }
                            return done;
                        });
                    }
                }
            }
        );

        /* TODO: Needs implementation of "Dash" Bonus Action */
        public static readonly Spell ExpeditiousRetreat = new Spell(ID.EXPEDITIOUS_RETREAT, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 0, VS, SpellDuration.TEN_MINUTES, 0, 0, doNothing);

        public static readonly Spell FaerieFire = new Spell(
            ID.FAERIE_FIRE, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 60, V,
            SpellDuration.ONE_MINUTE, 20, 20, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                foreach (Combattant target in targets) {
                    if (target.DC(ID.FAERIE_FIRE, dc, AbilityType.DEXTERITY)) {
                        GlobalEvents.AffectBySpell(caster, ID.FAERIE_FIRE, target, false);
                    } else {
                        GlobalEvents.AffectBySpell(caster, ID.FAERIE_FIRE, target, true);
                        target.AddEffect(Effect.SPELL_FAIRIE_FIRE);
                        int rounds = 10;
                        target.AddEndOfTurnEvent(delegate () {
                            rounds--;
                            bool done = rounds <= 0;
                            if (done)
                                target.RemoveEffect(Effect.SPELL_FAIRIE_FIRE);
                            return done;
                        });
                    }
                }
            }
        );

        public static readonly Spell FalseLife = new Spell(ID.FALSE_LIFE, SpellSchool.NECROMANCY, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.ONE_HOUR, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            int temporaryHitpoints = Die.D4.Value + 4 + ((int)slot - 1) * 5;
            GlobalEvents.AffectBySpell(caster, ID.FALSE_LIFE, caster, true);
            caster.AddTemporaryHitpoints(temporaryHitpoints, SpellDuration.ONE_HOUR, ID.FALSE_LIFE);
        });

        public static readonly Spell FeatherFall = new Spell(ID.FEATHER_FALL, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.REACTION, 60, VM, SpellDuration.ONE_MINUTE, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Combattant target = targets[0];
            target.AddEffect(Effect.SPELL_FEATHER_FALL);
        });

        public static readonly Spell FindFamiliar = new Spell(ID.FIND_FAMILIAR, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_HOUR, 10, VSM, SpellDuration.INSTANTANEOUS, 0, 0, SpellWithoutEffect(ID.FIND_FAMILIAR));

        public static readonly Spell FloatingDisk = new Spell(ID.FLOATING_DISK, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_HOUR, 0, 0, SpellWithoutEffect(ID.FLOATING_DISK));

        public static readonly Spell FogCloud = new Spell(
            ID.FOG_CLOUD, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 120, VS,
            SpellDuration.ONE_HOUR, 20, 0, SpellWithoutEffect(ID.FOG_CLOUD)
        );

        public static readonly Spell Goodberry = new Spell(ID.GOODBERRY, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            if (caster is CharacterSheet hero) {
                for (int i = 0; i < 10; i++) {
                    hero.Inventory.AddToBag(Potions.Goodberry);
                }
            }
        });

        public static readonly Spell Grease = new Spell(ID.GREASE, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_MINUTE, 10, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            foreach (Combattant target in targets) {
                if (!target.HasEffect(Effect.IMMUNITY_PRONE) && !target.DC(ID.GREASE, dc, AbilityType.DEXTERITY)) {
                    target.AddCondition(ConditionType.PRONE);
                    GlobalEvents.AffectBySpell(caster, ID.GREASE, target, true);
                } else {
                    GlobalEvents.AffectBySpell(caster, ID.GREASE, target, false);
                }
            }
        });

        public static readonly Spell GuidingBolt = new Spell(ID.GUIDING_BOLT, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 120, VS, SpellDuration.ONE_ROUND, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Combattant target = targets[0];
            Damage damage = new Damage(DamageType.RADIANT, DiceSlotScaling(SpellLevel.FIRST, slot, 6, 3).Roll());
            bool hit = SpellAttack(ID.GUIDING_BOLT, ground, caster, damage, modifier, target, 120);
            if (hit) {
                target.AddEffect(Effect.ADVANTAGE_ON_BEING_ATTACKED);
                bool removeNextRound = false;
                caster.AddEndOfTurnEvent(delegate () {
                    if (removeNextRound) {
                        target.RemoveEffect(Effect.ADVANTAGE_ON_BEING_ATTACKED);
                        return true;
                    } else {
                        removeNextRound = true;
                        return false;
                    }
                });
            }
        });

        public static readonly Spell HealingWord = new Spell(
            ID.HEALING_WORD, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 60, V,
            SpellDuration.INSTANTANEOUS, 0, 1, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                if (targets[0] is Monster monster) {
                    if (monster.Type == Monsters.Type.CONSTRUCT || monster.Type == Monsters.Type.UNDEAD) {
                        GlobalEvents.AffectBySpell(caster, ID.HEALING_WORD, monster, false);
                        return;
                    };
                }
                Dice healed = DiceSlotScaling(SpellLevel.FIRST, slot, 4, 1, modifier);
                GlobalEvents.AffectBySpell(caster, ID.HEALING_WORD, targets[0], true);
                targets[0].HealDamage(healed.Roll());
            }
        );

        public static readonly Spell HellishRebuke = new Spell(ID.HELLISH_REBUKE, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.REACTION, 60, VS, SpellDuration.INSTANTANEOUS, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            // Should only affect targets that damaged the caster but we can't determine this here
            Dice dice = DiceSlotScaling(SpellLevel.FIRST, slot, 10, 2);
            Combattant target = targets[0];
            target.TakeDamage(ID.HELLISH_REBUKE, DamageType.FIRE, dice.Roll(), DCEffect.HALVES_DAMAGE, dc, AbilityType.DEXTERITY, out _);
            GlobalEvents.AffectBySpell(caster, ID.HELLISH_REBUKE, target, true);
        });

        public static readonly Spell Heroism = new Spell(ID.HEROISM, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS, SpellDuration.ONE_MINUTE, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Combattant target = targets[0];
            target.AddEffect(Effect.IMMUNITY_FRIGHTENED);
            int remainingTurns = 10;
            target.AddStartOfTurnEvent(delegate () {
                target.AddTemporaryHitpoints(modifier, SpellDuration.ONE_MINUTE, ID.HEROISM);
                remainingTurns--;
                return remainingTurns == 0;
            });
            target.AddEndOfTurnEvent(delegate () {
                if (remainingTurns == 0) {
                    target.RemoveTemporaryHitpoints(ID.HEROISM);
                    return true;
                } else {
                    return false;
                }
            });
        });

        public static readonly Spell HideousLaughter = new Spell(ID.HIDEOUS_LAUGHTER, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Combattant target = targets[0];
            if (target.Intelligence.Value < 5 || target.DC(ID.HIDEOUS_LAUGHTER, dc, AbilityType.WISDOM)) {
                GlobalEvents.AffectBySpell(caster, ID.HIDEOUS_LAUGHTER, target, false);
            } else {
                GlobalEvents.AffectBySpell(caster, ID.HIDEOUS_LAUGHTER, target, true);
                target.AddCondition(ConditionType.PRONE);
                target.AddEffects(Effect.SPELL_HIDEOUS_LAUGHTER);
                int remainingRounds = 10;
                target.AddDamageTakenEvent(delegate (object source, Damage damage) {
                    if (target.DC(ID.HIDEOUS_LAUGHTER, dc, AbilityType.WISDOM, out _, true)) {
                        remainingRounds = 0;
                        target.RemoveEffects(Effect.SPELL_HIDEOUS_LAUGHTER);
                        return true;
                    } else {
                        return false;
                    }
                });
                target.AddEndOfTurnEvent(delegate () {
                    if (--remainingRounds < 1 || target.DC(ID.HIDEOUS_LAUGHTER, dc, AbilityType.WISDOM)) {
                        target.RemoveEffects(Effect.SPELL_HIDEOUS_LAUGHTER);
                        return true;
                    } else {
                        return false;
                    }
                });
            }
        });

        public static readonly Spell HuntersMark = new Spell(ID.HUNTERS_MARK, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 90, V, SpellDuration.ONE_HOUR, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Combattant target = targets[0];
            target.AddDamageTakenEvent(delegate (object source, Damage damage) {
                if (caster == source) {
                    target.TakeDamage(ID.HUNTERS_MARK, DamageType.TRUE_DAMAGE, "1d6");
                }
                return false;
            });
        });
        /* TODO: At some point we will want to have unidentified items*/
        public static readonly Spell Identify = new Spell(ID.IDENTIFY, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_MINUTE, 0, VSM, SpellDuration.INSTANTANEOUS, 0, 0, SpellWithoutEffect(ID.IDENTIFY));

        public static readonly Spell IllusoryScript = new Spell(ID.ILLUSORY_SCRIPT, SpellSchool.ILLUSION, SpellLevel.FIRST, CastingTime.ONE_MINUTE, 0, SM, SpellDuration.TEN_DAYS, 0, 0, SpellWithoutEffect(ID.ILLUSORY_SCRIPT));

        public static readonly Spell InflictWounds = new Spell(ID.INFLICT_WOUNDS, SpellSchool.NECROMANCY, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS, SpellDuration.INSTANTANEOUS, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Combattant target = targets[0];
            Dice dice = DiceSlotScaling(SpellLevel.FIRST, slot, 10, 2);
            SpellAttack(ID.INFLICT_WOUNDS, ground, caster, new Damage(DamageType.NECROTIC, dice.Roll()), modifier, target, 5);
        });

        public static readonly Spell Jump = new Spell(
            ID.JUMP, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM,
            SpellDuration.ONE_MINUTE, 0, 1, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                Combattant target = targets[0];
                GlobalEvents.AffectBySpell(caster, ID.JUMP, target, true);
                target.AddEffect(Effect.SPELL_JUMP);
            }
        );

        public static readonly Spell Longstrider = new Spell(
            ID.LONGSTRIDER, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM,
            SpellDuration.ONE_HOUR, 0, 1, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                Combattant target = targets[0];
                GlobalEvents.AffectBySpell(caster, ID.LONGSTRIDER, target, true);
                target.AddEffect(Effect.SPELL_LONGSTRIDER);
            }
        );

        public static readonly Spell MageArmor = new Spell(ID.MAGE_ARMOR, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.EIGHT_HOURS, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Combattant target = targets[0];
            if (target is CharacterSheet hero && hero.Inventory.Armor == null) {
                GlobalEvents.AffectBySpell(caster, ID.MAGE_ARMOR, hero, true);
                hero.Equip(Armors.MageArmor);
            } else {
                GlobalEvents.AffectBySpell(caster, ID.MAGE_ARMOR, target, true);
            }
        });

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
                        if (Die.D6.Value == 6) {
                            target = caster;
                        }
                    }
                    GlobalEvents.AffectBySpell(caster, ID.MAGIC_MISSILE, target, true);
                    int bonusMissiles = missilesTotal % targets.Length;
                    bonusMissiles = Math.Min(1, bonusMissiles);
                    missilesTotal -= bonusMissiles;
                    for (int m = 0; m < missilesTotal / targets.Length + bonusMissiles; m++) {
                        target.TakeDamage(ID.MAGIC_MISSILE, damage.Type, damage.Dice.Roll());
                    }
                }
            }
        );

        public static readonly Spell ProtectionfromEvilandGood = new Spell(ID.PROTECTION_FROM_EVIL_AND_GOOD, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VSM, SpellDuration.TEN_MINUTES, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            GlobalEvents.AffectBySpell(caster, ID.PROTECTION_FROM_EVIL_AND_GOOD, targets[0], true);
            targets[0].AddEffect(Effect.SPELL_PROTECTION_FROM_EVIL_AND_GOOD);
        });

        public static readonly Spell PurifyFoodAndDrink = new Spell(
            ID.PURIFY_FOOD_AND_DRINK, SpellSchool.TRANSMUTATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 10, VS,
            SpellDuration.INSTANTANEOUS, 5, 0, SpellWithoutEffect(ID.PURIFY_FOOD_AND_DRINK)
        );

        // TODO: Right now, we don't have a means to redirect a target
        public static readonly Spell Sanctuary = new Spell(ID.SANCTUARY, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 30, VSM, SpellDuration.ONE_MINUTE, 0, 0, SpellWithoutEffect(ID.SANCTUARY));

        public static readonly Spell Shield = new Spell(ID.SHIELD, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.REACTION, 0, VS, SpellDuration.ONE_ROUND, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            // TODO: Implement Reaction and apply bonus to reaction attack or nullify Magic Missile
            GlobalEvents.AffectBySpell(caster, ID.SHIELD, caster, true);
            caster.AddEffect(Effect.SPELL_SHIELD);
            caster.ArmorClassModifier += 5;
            caster.AddStartOfTurnEvent(delegate () {
                caster.ArmorClassModifier -= 5;
                caster.RemoveEffect(Effect.SPELL_SHIELD);
                return true;
            });
        });

        public static readonly Spell ShieldofFaith = new Spell(ID.SHIELD_OF_FAITH, SpellSchool.ABJURATION, SpellLevel.FIRST, CastingTime.BONUS_ACTION, 60, VSM, SpellDuration.TEN_MINUTES, 0, 0, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
            Combattant target = targets[0];
            if (target.HasEffect(Effect.SPELL_SHIELD_OF_FAITH)) {
                GlobalEvents.AffectBySpell(caster, ID.SHIELD_OF_FAITH, target, false);
            } else {
                GlobalEvents.AffectBySpell(caster, ID.SHIELD_OF_FAITH, target, false);
                target.AddEffect(Effect.SPELL_SHIELD_OF_FAITH);
                target.ArmorClassModifier += 2;
                int remainingRounds = 100;
                target.AddEndOfTurnEvent(delegate () {
                    if (--remainingRounds == 0) {
                        target.ArmorClassModifier -= 2;
                        target.RemoveEffect(Effect.SPELL_SHIELD_OF_FAITH);
                        return true;
                    } else {
                        return false;
                    }
                });
            }
        });

        public static readonly Spell SilentImage = new Spell(ID.SILENT_IMAGE, SpellSchool.ILLUSION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.TEN_MINUTES, 15, 0, SpellWithoutEffect(ID.SILENT_IMAGE));

        public static readonly Spell Sleep = new Spell(ID.SLEEP, SpellSchool.ENCHANTMENT, SpellLevel.FIRST, CastingTime.ONE_ACTION, 90, VSM, SpellDuration.ONE_MINUTE, 20, 99,
            delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                Dice dice = DiceSlotScaling(SpellLevel.FIRST, slot, 8, 5, 0, 2);
                int remainingHitpoints = dice.Roll();
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
                    target.AddDamageTakenEvent(delegate (object source, Damage damage) {
                        targetTookDamage = true;
                        return true;
                    });

                    target.AddEndOfTurnEvent(delegate () {
                        if (targetTookDamage || --remainingRounds < 1) {
                            target.RemoveCondition(ConditionType.UNCONSCIOUS);
                            return true;
                        }
                        return false;
                    });
                }
            });

        public static readonly Spell SpeakWithAnimals = new Spell(
            ID.SPEAK_WITH_ANIMALS, SpellSchool.DIVINATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 0, VS,
            SpellDuration.TEN_MINUTES, 0, 0, SpellWithoutEffect(ID.SPEAK_WITH_ANIMALS)
        );

        public static readonly Spell Thunderwave = new Spell(
            ID.THUNDERWAVE, SpellSchool.EVOCATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 15, VS,
            SpellDuration.INSTANTANEOUS, 15, 20, delegate (Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, Combattant[] targets) {
                Dice dice = DiceSlotScaling(SpellLevel.FIRST, slot, 8);
                foreach (Combattant target in targets) {
                    GlobalEvents.AffectBySpell(caster, ID.THUNDERWAVE, target, true);
                    target.TakeDamage(ID.THUNDERWAVE, DamageType.THUNDER, dice.Roll(), DCEffect.HALVES_DAMAGE, dc, AbilityType.CONSTITUTION, out bool dcResult);
                    if (!dcResult) {
                        ground.Push(ground.LocateCombattant(caster), target, 10);
                    }
                }
            }
        );

        public static readonly Spell UnseenServant = new Spell(ID.UNSEEN_SERVANT, SpellSchool.CONJURATION, SpellLevel.FIRST, CastingTime.ONE_ACTION, 60, VSM, SpellDuration.ONE_HOUR, 0, 0, SpellWithoutEffect(ID.UNSEEN_SERVANT));

    }
}