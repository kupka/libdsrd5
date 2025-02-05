using System;
using Xunit;

namespace srd5 {
    [CollectionDefinition("SingleThreaded", DisableParallelization = true)]
    [Collection("SingleThreaded")]
    public partial class SpellTest {
        private BattleGroundClassic createBattleground(Combattant caster, params Combattant[] targets) {
            BattleGroundClassic ground = new BattleGroundClassic();
            ground.AddCombattant(caster, ClassicLocation.Row.FRONT_LEFT);
            foreach (Combattant target in targets) {
                ground.AddCombattant(target, ClassicLocation.Row.FRONT_RIGHT);
            }
            return ground;
        }


        [Fact]
        public void AcidSplashTest() {
            CharacterSheet hero = new CharacterSheet(Race.HUMAN, true);
            Monster ogre = Monsters.Ogre;
            for (int i = 0; i < 17; i++) {
                hero.AddLevel(CharacterClasses.Druid);
                if (i == 1 || i == 4 || i == 10 || i == 16) {
                    int hpBefore = ogre.HitPoints;
                    Spells.AcidSplash.Cast(createBattleground(hero, ogre), hero, 8 + hero.ProficiencyBonus + hero.Intelligence.Modifier, SpellLevel.CANTRIP, hero.Intelligence.Modifier, ogre);
                    Assert.True(ogre.HitPoints <= hpBefore);
                }
            }
        }

        [Fact]
        public void EqualTest() {
            Spell spell = Spells.AcidSplash;
            Assert.True(Spells.AcidSplash.Equals(spell));
            Assert.False(spell.Equals("foo"));
            Assert.Equal(Spells.ID.ACID_SPLASH.Name(), spell.Name);
        }

        [Fact]
        public void MagicMissileTest() {
            CharacterSheet hero = new CharacterSheet(Race.HUMAN, true);
            Monster ogre1 = Monsters.Ogre;
            Monster ogre2 = Monsters.Ogre;
            Monster ogre3 = Monsters.Ogre;
            Monster ogre4 = Monsters.Ogre;
            Monster ogre5 = Monsters.Ogre;
            Spells.MagicMissile.Cast(createBattleground(hero, ogre1, ogre2, ogre3, ogre4, ogre5), hero, 8 + hero.ProficiencyBonus + hero.Intelligence.Modifier, SpellLevel.SECOND, hero.Intelligence.Modifier, ogre1, ogre2, ogre3, ogre4, ogre5);
            Assert.True(ogre1.HitPoints < ogre1.HitPointsMax);
            Assert.True(ogre2.HitPoints < ogre2.HitPointsMax);
            Assert.True(ogre3.HitPoints < ogre3.HitPointsMax);
            Assert.True(ogre4.HitPoints < ogre4.HitPointsMax);
            Assert.Equal(ogre5.HitPointsMax, ogre5.HitPoints);
        }

        [Fact]
        public void CureWoundsTest() {
            CharacterSheet hero = new CharacterSheet(Race.GNOME);
            Battleground ground = createBattleground(hero);
            hero.AddLevel(CharacterClasses.Druid);
            hero.HitPoints = 1;
            Spells.CureWounds.Cast(ground, hero, 0, SpellLevel.NINETH, hero.Wisdom.Modifier, hero);
            Assert.Equal(hero.HitPointsMax, hero.HitPoints);
            // doesn't affect undead
            Monster shadow = Monsters.Shadow;
            shadow.HitPoints = shadow.HitPointsMax - 1;
            Spells.CureWounds.Cast(ground, hero, 0, SpellLevel.NINETH, hero.Wisdom.Modifier, shadow);
            Assert.Equal(shadow.HitPointsMax - 1, shadow.HitPoints);
            // doesn't affect constructs
            Monster golem = Monsters.ClayGolem;
            golem.HitPoints = golem.HitPointsMax - 1;
            Spells.CureWounds.Cast(ground, hero, 0, SpellLevel.NINETH, hero.Wisdom.Modifier, golem);
            Assert.Equal(golem.HitPointsMax - 1, golem.HitPoints);
            // affects giants
            Monster ogre = Monsters.Ogre;
            ogre.HitPoints = ogre.HitPointsMax - 10;
            Spells.CureWounds.Cast(ground, hero, 0, SpellLevel.NINETH, hero.Wisdom.Modifier, ogre);
            Assert.Equal(ogre.HitPointsMax, ogre.HitPoints);
        }

        [Fact]
        public void HealingWordTest() {
            CharacterSheet hero = new CharacterSheet(Race.GNOME);
            hero.AddLevel(CharacterClasses.Druid);
            hero.HitPoints = 1;
            Spells.HealingWord.Cast(createBattleground(hero), hero, 0, SpellLevel.SEVENTH, hero.Wisdom.Modifier, hero);
            Assert.Equal(hero.HitPointsMax, hero.HitPoints);
            // doesn't affect undead
            Monster shadow = Monsters.Shadow;
            shadow.HitPoints = shadow.HitPointsMax - 1;
            Spells.HealingWord.Cast(createBattleground(hero, shadow), hero, 0, SpellLevel.NINETH, hero.Wisdom.Modifier, shadow);
            Assert.Equal(shadow.HitPointsMax - 1, shadow.HitPoints);
            // doesn't affect constructs
            Monster golem = Monsters.ClayGolem;
            golem.HitPoints = golem.HitPointsMax - 1;
            Spells.HealingWord.Cast(createBattleground(hero, golem), hero, 0, SpellLevel.NINETH, hero.Wisdom.Modifier, golem);
            Assert.Equal(golem.HitPointsMax - 1, golem.HitPoints);
            // affects giants
            Monster ogre = Monsters.Ogre;
            ogre.HitPoints = ogre.HitPointsMax - 10;
            Spells.HealingWord.Cast(createBattleground(hero, ogre), hero, 0, SpellLevel.NINETH, hero.Wisdom.Modifier, ogre);
            Assert.Equal(ogre.HitPointsMax, ogre.HitPoints);
        }

        [Fact]
        public void ShillelaghTest() {
            CharacterSheet hero;
            // Execute this multiple times because of random character generation
            for (int i = 0; i < 10; i++) {
                hero = new CharacterSheet(Race.HALF_ELF, true);
                hero.AddLevel(CharacterClasses.Druid);
                if (i % 2 == 0)
                    hero.Equip(Weapons.Club);
                else {
                    hero.Equip(Weapons.Quarterstaff);
                    hero.BonusAttack = Attack.FromWeapon(hero.AttackProficiency, "1d6", Weapons.Quarterstaff);
                }
                Assert.Equal(hero.ProficiencyBonus + hero.Strength.Modifier, hero.MeleeAttacks[0].AttackBonus);
                Spells.Shillelagh.Cast(hero, 0, SpellLevel.CANTRIP, hero.Wisdom.Modifier);
                Assert.Equal(hero.ProficiencyBonus + hero.Wisdom.Modifier, hero.MeleeAttacks[0].AttackBonus);
                if (hero.Inventory.MainHand.IsThisA(Weapons.Quarterstaff)) {
                    Assert.Equal(Spells.ID.SHILLELAGH.Name(), hero.BonusAttack.Name);
                }
            }
            // Also check with wrong weapon
            hero = new CharacterSheet(Race.HALF_ELF, true);
            hero.AddLevel(CharacterClasses.Druid);
            hero.Equip(Weapons.Greataxe);
            Assert.Equal(hero.Strength.Modifier, hero.MeleeAttacks[0].AttackBonus);
            Spells.Shillelagh.Cast(hero, 0, SpellLevel.CANTRIP, hero.Wisdom.Modifier);
            Assert.Equal(hero.Strength.Modifier, hero.MeleeAttacks[0].AttackBonus);
            // Monster test (cannot cast Shillelagh yet)
            Spells.Shillelagh.Cast(Monsters.Goblin, 0, SpellLevel.CANTRIP, 3);
        }

        private void DefaultSpellTest(Spell spell, int dc, SpellLevel slot, ConditionType? checkForCondition, Effect? checkForEffect, int? simulateTurns, Monsters.Type monsterType = Monsters.Type.BEAST) {
            if (checkForCondition == null && checkForEffect == null) throw new ArgumentException("Don't use is when not checking for Condition and/or Effect");
            CharacterSheet hero = new CharacterSheet(Race.DRAGONBORN, true);
            Monster orc1 = Monsters.Orc;
            Monster orc2 = Monsters.Orc;
            Monster orc3 = Monsters.Orc;
            Monster badger = Monsters.Badger;
            Monster zombie = Monsters.Zombie;
            Monster dragon = Monsters.AncientBlackDragon;
            Monster typed = new Monster(monsterType, Monsters.ID.MANTICORE, Alignment.UNALIGNED, 10, 10, 10, 10, 10, 10, 10, "5d8", 30, 2, new Attack[0], new Attack[0], Size.MEDIUM, 5);
            Monster typedImmune = new Monster(monsterType, Monsters.ID.MANTICORE, Alignment.UNALIGNED, 10, 10, 10, 10, 10, 10, 10, "5d8", 30, 2, new Attack[0], new Attack[0], Size.MEDIUM, 5);
            typedImmune.AddEffects(Effect.IMMUNITY_ACID, Effect.IMMUNITY_BLINDED, Effect.IMMUNITY_BLUDGEONING, Effect.IMMUNITY_CHARMED, Effect.IMMUNITY_COLD, Effect.IMMUNITY_DAMAGE_FROM_SPELLS,
                Effect.IMMUNITY_DEAFENED, Effect.IMMUNITY_EXHAUSTION, Effect.IMMUNITY_FIRE, Effect.IMMUNITY_FORCE, Effect.IMMUNITY_FRIGHTENED, Effect.IMMUNITY_GRAPPLED, Effect.IMMUNITY_INCAPACITATED,
                Effect.IMMUNITY_INVISIBLE, Effect.IMMUNITY_LIGHTNING, Effect.IMMUNITY_NECROTIC, Effect.IMMUNITY_NONMAGIC, Effect.IMMUNITY_PARALYZED, Effect.IMMUNITY_PETRIFIED, Effect.IMMUNITY_PIERCING,
                Effect.IMMUNITY_POISON, Effect.IMMUNITY_POISONED, Effect.IMMUNITY_PRONE, Effect.IMMUNITY_PSYCHIC, Effect.IMMUNITY_RADIANT, Effect.IMMUNITY_RESTRAINED, Effect.IMMUNITY_SLEEP,
                Effect.IMMUNITY_SLEEP, Effect.IMMUNITY_STUNNED, Effect.IMMUNITY_THUNDER, Effect.IMMUNITY_UNCONSCIOUS);

            Battleground ground = createBattleground(hero, orc1, orc2, orc3, badger, zombie, dragon);
            Random.State = 1;
            if (spell.MaximumTargets > 1) {
                spell.Cast(ground, hero, dc, slot, 0, orc1, orc2);
                spell.Cast(ground, hero, dc, slot, 0, orc3, badger);
                spell.Cast(ground, hero, dc, slot, 0, zombie, dragon);
                spell.Cast(ground, hero, dc, slot, 0, typed, typedImmune);
            } else {
                spell.Cast(ground, hero, dc, slot, 0, orc1);
                spell.Cast(ground, hero, dc, slot, 0, orc2);
                spell.Cast(ground, hero, dc, slot, 0, orc3);
                spell.Cast(ground, hero, dc, slot, 0, badger);
                spell.Cast(ground, hero, dc, slot, 0, zombie);
                spell.Cast(ground, hero, dc, slot, 0, dragon);
                spell.Cast(ground, hero, dc, slot, 0, typed);
                spell.Cast(ground, hero, dc, slot, 0, typedImmune);
            }
            if (checkForCondition != null) {
                ConditionType cond = (ConditionType)checkForCondition;
                Assert.True(
                    orc1.HasCondition(cond) || orc2.HasCondition(cond) || orc3.HasCondition(cond) || badger.HasCondition(cond) || zombie.HasCondition(cond) || dragon.HasCondition(cond) || typed.HasCondition(cond)
                );
                Assert.False(typedImmune.HasCondition(cond));
            }
            if (checkForEffect != null) {
                Effect eff = (Effect)checkForEffect;
                Assert.True(
                    orc1.HasEffect(eff) || orc2.HasEffect(eff) || orc3.HasEffect(eff) || badger.HasEffect(eff) || zombie.HasEffect(eff) || dragon.HasEffect(eff) || typed.HasEffect(eff)
                );
            }
            if (simulateTurns != null) {
                int turns = (int)simulateTurns;
                for (int i = 0; i < turns; i++) {
                    hero.OnStartOfTurn();
                    orc1.OnStartOfTurn();
                    orc2.OnStartOfTurn();
                    orc3.OnStartOfTurn();
                    badger.OnStartOfTurn();
                    zombie.OnStartOfTurn();
                    dragon.OnStartOfTurn();
                    typed.OnStartOfTurn();
                    typedImmune.OnStartOfTurn();

                    hero.OnEndOfTurn();
                    orc1.OnEndOfTurn();
                    orc2.OnEndOfTurn();
                    orc3.OnEndOfTurn();
                    badger.OnEndOfTurn();
                    zombie.OnEndOfTurn();
                    dragon.OnEndOfTurn();
                    typed.OnEndOfTurn();
                    typedImmune.OnEndOfTurn();
                }
                if (checkForCondition != null) {
                    ConditionType cond = (ConditionType)checkForCondition;
                    Assert.False(
                        orc1.HasCondition(cond) || orc2.HasCondition(cond) || orc3.HasCondition(cond) || badger.HasCondition(cond) || zombie.HasCondition(cond) || dragon.HasCondition(cond) || typed.HasCondition(cond)
                    );
                }
                if (checkForEffect != null) {
                    Effect eff = (Effect)checkForEffect;
                    Assert.False(
                        orc1.HasEffect(eff) || orc2.HasEffect(eff) || orc3.HasEffect(eff) || badger.HasEffect(eff) || zombie.HasEffect(eff) || dragon.HasEffect(eff) || typed.HasEffect(eff)
                    );
                }
            }
        }

        private void DamagingSpellTesting(Spell spell, int dc, DamageType damageType) {
            Monster hag = Monsters.NightHag;
            Monster monster1 = Monsters.Bandit;
            Monster monster2 = Monsters.Baboon;
            monster2.AddEffect(Enum.Parse<Effect>("IMMUNITY_" + Enum.GetName<DamageType>(damageType)));
            Battleground ground = createBattleground(hag, monster1, monster2);
            for (int i = 0; i < 10; i++) {
                spell.Cast(ground, hag, dc, spell.Level, 0, monster1, monster2);
                spell.Cast(ground, hag, dc, spell.Level, 0, monster2, monster1);
            }
            Assert.True(monster1.HitPoints < monster1.HitPointsMax);
            Assert.True(monster2.HitPoints == monster2.HitPointsMax);
        }

        [Fact]
        public void CharmPersonTest() {
            Combattant hag = Monsters.NightHag;
            Combattant badger = Monsters.GiantBadger;
            Spells.CharmPerson.Cast(Monsters.NightHag, 10, SpellLevel.CANTRIP, 10);
            Spells.CharmPerson.Cast(createBattleground(hag, badger), hag, 10, SpellLevel.SECOND, 10, badger); // not affected
            DefaultSpellTest(Spells.CharmPerson, 14, SpellLevel.SIXTH, ConditionType.CHARMED, null, null);
        }

        [Fact]
        public void HoldPersonTest() {
            Combattant hag = Monsters.NightHag;
            Combattant badger = Monsters.GiantBadger;
            CharacterSheet hero = new CharacterSheet(Race.HUMAN);
            Spells.HoldPerson.Cast(createBattleground(hag, badger), hag, 10, SpellLevel.SECOND, 10, badger); // not affected

            hero.AddEffect(Effect.IMMUNITY_PARALYZED);
            Spells.HoldPerson.Cast(createBattleground(hag, hero), hag, 10, SpellLevel.SECOND, 10, hero); // immune to paralyze
            DefaultSpellTest(Spells.HoldPerson, 14, SpellLevel.SEVENTH, ConditionType.PARALYZED, null, 100);
        }

        [Fact]
        public void EntangleTest() {
            DefaultSpellTest(Spells.Entangle, 14, SpellLevel.FIRST, null, Effect.SPELL_ENTANGLE, 100);
        }

        [Fact]
        public void FairieFireTest() {
            DefaultSpellTest(Spells.FaerieFire, 14, SpellLevel.SECOND, null, Effect.SPELL_FAIRIE_FIRE, 100);
        }

        [Fact]
        public void JumpTest() {
            DefaultSpellTest(Spells.Jump, 14, SpellLevel.THIRD, null, Effect.SPELL_JUMP, null);
        }

        [Fact]
        public void LongStriderTest() {
            CharacterSheet hero = new CharacterSheet(Race.GNOME);
            int speed = hero.Speed;
            Spells.Longstrider.Cast(hero, 10, SpellLevel.FIRST, 0);
            Assert.True(hero.Speed > speed);
            hero.RemoveEffect(Effect.SPELL_LONGSTRIDER);
            Assert.Equal(speed, hero.Speed);
        }

        [Fact]
        public void ProduceFlameTest() {
            CharacterSheet hero = new CharacterSheet(Race.GNOME);
            hero.AddLevel(CharacterClasses.Druid);
            Monster badger = Monsters.GiantBadger;
            int hps = badger.HitPoints;
            Battleground2D ground = new Battleground2D(10, 10);
            ground.AddCombattant(hero, 3, 3);
            ground.AddCombattant(badger, 5, 5);
            Spells.ProduceFlame.Cast(ground, hero, 10, SpellLevel.FIRST, 0, badger);
            hero.AddLevels(CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid);
            Spells.ProduceFlame.Cast(ground, hero, 10, SpellLevel.FIRST, 0, badger);
            hero.AddLevels(CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid);
            Spells.ProduceFlame.Cast(ground, hero, 10, SpellLevel.FIRST, 0, badger);
            hero.AddLevels(CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid, CharacterClasses.Druid);
            Spells.ProduceFlame.Cast(ground, hero, 10, SpellLevel.FIRST, 0, badger);
            Assert.True(badger.HitPoints < hps);
        }

        [Fact]
        public void ThunderwaveTest() {
            CharacterSheet hero = new CharacterSheet(Race.GNOME);
            hero.AddLevel(CharacterClasses.Druid);
            Monster badger = Monsters.GiantBadger;
            int hps = badger.HitPoints;
            Random.State = 1;
            Battleground2D ground = new Battleground2D(10, 10);
            ground.AddCombattant(hero, 3, 3);
            ground.AddCombattant(badger, 5, 5);
            Assert.Equal(5, ground.LocateCombattant2D(badger).X);
            Spells.Thunderwave.Cast(ground, hero, 10, SpellLevel.FIRST, 0, badger);
            Spells.Thunderwave.Cast(ground, hero, 10, SpellLevel.FIRST, 0, badger);
            Spells.Thunderwave.Cast(ground, hero, 10, SpellLevel.FIRST, 0, badger);
            Assert.True(badger.HitPoints < hps);
            Assert.True(ground.LocateCombattant2D(badger).X > 5);

            BattleGroundClassic classic = new BattleGroundClassic();
            badger.HealDamage(1000);
            classic.AddCombattant(hero, ClassicLocation.Row.FRONT_LEFT);
            classic.AddCombattant(badger, ClassicLocation.Row.FRONT_RIGHT);
            Spells.Thunderwave.Cast(classic, hero, 10, SpellLevel.FIRST, 0, badger);
            Spells.Thunderwave.Cast(classic, hero, 10, SpellLevel.FIRST, 0, badger);
            Spells.Thunderwave.Cast(classic, hero, 10, SpellLevel.FIRST, 0, badger);
            Assert.True(badger.HitPoints < hps);
            Assert.Equal(ClassicLocation.Row.BACK_RIGHT, classic.LocateClassicCombattant(badger).Location);
        }

        [Fact]
        public void ResistanceTest() {
            CharacterSheet hero = new CharacterSheet(Race.GNOME);
            Spells.Resistance.Cast(hero, 10, SpellLevel.FIRST, 0);
            Assert.True(hero.HasEffect(Effect.SPELL_RESISTANCE));
            hero.DC(null, 10, AbilityType.STRENGTH);
            Assert.False(hero.HasEffect(Effect.SPELL_RESISTANCE));
        }

        [Fact]
        public void AnimalFriendshipTest() {
            DefaultSpellTest(Spells.AnimalFriendship, 14, SpellLevel.FIRST, ConditionType.CHARMED, null, null, Monsters.Type.BEAST);
        }
    }
}