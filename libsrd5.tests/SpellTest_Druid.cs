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

        private void DefaultSpellTest(Spell spell, int dc, SpellLevel slot, ConditionType? checkForCondition, Effect? checkForEffect, int? simulateTurns) {
            CharacterSheet hero = new CharacterSheet(Race.DRAGONBORN, true);
            Monster orc1 = Monsters.Orc;
            Monster orc2 = Monsters.Orc;
            Monster orc3 = Monsters.Orc;
            Monster orc4 = Monsters.Orc;
            Monster orc5 = Monsters.Orc;
            Monster orc6 = Monsters.Orc;
            Battleground ground = createBattleground(hero, orc1, orc2, orc3, orc4, orc5, orc6);
            Random.State = 1;
            if (spell.MaximumTargets > 1) {
                spell.Cast(ground, hero, dc, slot, 0, orc1, orc2);
                spell.Cast(ground, hero, dc, slot, 0, orc3, orc4);
                spell.Cast(ground, hero, dc, slot, 0, orc5, orc6);
            } else {
                spell.Cast(ground, hero, dc, slot, 0, orc1);
                spell.Cast(ground, hero, dc, slot, 0, orc2);
                spell.Cast(ground, hero, dc, slot, 0, orc3);
                spell.Cast(ground, hero, dc, slot, 0, orc4);
                spell.Cast(ground, hero, dc, slot, 0, orc5);
                spell.Cast(ground, hero, dc, slot, 0, orc6);
            }
            if (checkForCondition != null) {
                ConditionType cond = (ConditionType)checkForCondition;
                Assert.True(
                    orc1.HasCondition(cond) || orc2.HasCondition(cond) || orc3.HasCondition(cond) || orc4.HasCondition(cond) || orc5.HasCondition(cond) || orc6.HasCondition(cond)
                );
            }
            if (checkForEffect != null) {
                Effect eff = (Effect)checkForEffect;
                Assert.True(
                    orc1.HasEffect(eff) || orc2.HasEffect(eff) || orc3.HasEffect(eff) || orc4.HasEffect(eff) || orc5.HasEffect(eff) || orc6.HasEffect(eff)
                );
            }
            if (simulateTurns != null) {
                int turns = (int)simulateTurns;
                for (int i = 0; i < turns; i++) {
                    orc1.OnEndOfTurn();
                    orc2.OnEndOfTurn();
                    orc3.OnEndOfTurn();
                    orc4.OnEndOfTurn();
                    orc5.OnEndOfTurn();
                    orc6.OnEndOfTurn();
                }
                if (checkForCondition != null) {
                    ConditionType cond = (ConditionType)checkForCondition;
                    Assert.False(
                        orc1.HasCondition(cond) || orc2.HasCondition(cond) || orc3.HasCondition(cond) || orc4.HasCondition(cond) || orc5.HasCondition(cond) || orc6.HasCondition(cond)
                    );
                }
                if (checkForEffect != null) {
                    Effect eff = (Effect)checkForEffect;
                    Assert.False(
                        orc1.HasEffect(eff) || orc2.HasEffect(eff) || orc3.HasEffect(eff) || orc4.HasEffect(eff) || orc5.HasEffect(eff) || orc6.HasEffect(eff)
                    );
                }
            }
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
            DefaultSpellTest(Spells.Entangle, 14, SpellLevel.FIRST, null, Effect.ENTANGLE, 100);
        }

        [Fact]
        public void FairieFireTest() {
            DefaultSpellTest(Spells.FaerieFire, 14, SpellLevel.SECOND, null, Effect.FAIRIE_FIRE, 100);
        }

        [Fact]
        public void JumpTest() {
            DefaultSpellTest(Spells.Jump, 14, SpellLevel.THIRD, null, Effect.JUMP, null);
        }

        [Fact]
        public void LongStriderTest() {
            CharacterSheet hero = new CharacterSheet(Race.GNOME);
            int speed = hero.Speed;
            Spells.Longstrider.Cast(hero, 10, SpellLevel.FIRST, 0);
            Assert.True(hero.Speed > speed);
            hero.RemoveEffect(Effect.LONGSTRIDER);
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
            Assert.True(hero.HasEffect(Effect.RESISTANCE));
            hero.DC(null, 10, AbilityType.STRENGTH);
            Assert.False(hero.HasEffect(Effect.RESISTANCE));
        }
    }
}