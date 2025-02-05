using System;
using Xunit;

namespace srd5 {
    [CollectionDefinition("SingleThreaded", DisableParallelization = true)]
    [Collection("SingleThreaded")]
    public class CombattantTest {
        [Fact]
        public void TakeDamageTest() {
            Combattant ogre = Monsters.Ogre;
            ogre.AddEffect(Effect.VULNERABILITY_COLD);
            ogre.AddEffect(Effect.IMMUNITY_ACID);
            ogre.AddEffect(Effect.RESISTANCE_LIGHTNING);
            int hp = ogre.HitPoints;
            Damage cold = new Damage(DamageType.COLD, "1d6+5");
            ogre.TakeDamage(ogre, cold.Type, cold.Dice.RollCritical());
            Assert.InRange<int>(ogre.HitPoints, hp - 34, hp - 14);
            hp = ogre.HitPoints;
            Damage acid = new Damage(DamageType.ACID, "1d4+3");
            ogre.TakeDamage(ogre, acid.Type, acid.Dice.Roll());
            Assert.Equal(hp, ogre.HitPoints);
            Damage lightning = new Damage(DamageType.LIGHTNING, "1d12");
            ogre.TakeDamage(ogre, lightning.Type, lightning.Dice.Roll());
            Assert.InRange<int>(ogre.HitPoints, hp - 6, hp);
            Assert.Throws<Srd5ArgumentException>(delegate {
                ogre.TakeDamage(this, DamageType.ACID, -100);
            });
            Assert.Throws<Srd5ArgumentException>(delegate {
                ogre.HealDamage(-100);
            });
            ogre.TakeDamage(this, DamageType.PIERCING, ogre.HitPoints);
            Assert.True(ogre.Dead);
        }

        [Fact]
        public void TakeDamageTestWithDCHalves() {
            Combattant ogre = Monsters.Ogre;
            int tookFullDamage = 0, tookHalfDamage = 0;
            for (int i = 0; i < 10; i++) {
                int damageTaken = ogre.TakeDamage(this, DamageType.TRUE_DAMAGE, 2, Spells.DCEffect.HALVES_DAMAGE, 10, AbilityType.DEXTERITY, out _);
                if (damageTaken == 2) {
                    tookFullDamage++;
                } else if (damageTaken == 1) {
                    tookHalfDamage++;
                }
            }
            Assert.Equal(10, tookFullDamage + tookHalfDamage);
        }

        [Fact]
        public void TakeDamageTestWithDCNullify() {
            Combattant ogre = Monsters.Ogre;
            int tookFullDamage = 0, tookNoDamage = 0;
            for (int i = 0; i < 10; i++) {
                int damageTaken = ogre.TakeDamage(this, DamageType.TRUE_DAMAGE, 2, Spells.DCEffect.NULLIFIES_DAMAGE, 10, AbilityType.DEXTERITY, out _);
                if (damageTaken == 2) {
                    tookFullDamage++;
                } else if (damageTaken == 0) {
                    tookNoDamage++;
                }
            }
            Assert.Equal(10, tookFullDamage + tookNoDamage);
        }

        [Fact]
        public void GetAbilityTest() {
            Combattant ogre = Monsters.Ogre;
            Assert.Equal(ogre.Strength.Value, ogre.GetAbility(AbilityType.STRENGTH).Value);
            Assert.Equal(ogre.Constitution.Value, ogre.GetAbility(AbilityType.CONSTITUTION).Value);
            Assert.Equal(ogre.Dexterity.Value, ogre.GetAbility(AbilityType.DEXTERITY).Value);
            Assert.Equal(ogre.Intelligence.Value, ogre.GetAbility(AbilityType.INTELLIGENCE).Value);
            Assert.Equal(ogre.Wisdom.Value, ogre.GetAbility(AbilityType.WISDOM).Value);
            Assert.Equal(ogre.Charisma.Value, ogre.GetAbility(AbilityType.CHARISMA).Value);
            Assert.Throws<Srd5ArgumentException>(delegate {
                ogre.GetAbility(AbilityType.NONE);
            });
        }

        [Fact]
        public void ConditionTest() {
            CharacterSheet hero = new CharacterSheet(Race.HUMAN);
            Assert.Empty(hero.Conditions);
            Assert.Empty(hero.Effects);
            foreach (ConditionType condition in Enum.GetValues(typeof(ConditionType))) {
                hero.AddCondition(condition);
                hero.RemoveCondition(condition);
                Assert.Empty(hero.Conditions);
                Assert.Empty(hero.Effects);
            }
        }

        [Fact]
        public void CriticalDCTest() {
            Combattant ogre = Monsters.Ogre;
            bool criticalSuccess = false;
            bool criticalFail = false;
            for (int i = 0; i < 1000; i++) {
                if (ogre.DC(null, 35, AbilityType.STRENGTH))
                    criticalSuccess = true;
            }
            for (int i = 0; i < 1000; i++) {
                if (!ogre.DC(null, 0, AbilityType.STRENGTH))
                    criticalFail = true;
            }
            Assert.True(criticalFail && criticalSuccess);
        }

        [Fact]
        public void DCTest() {
            Combattant orc = Monsters.Orc;
            Assert.True(orc.DC(null, 2, AbilityType.WISDOM, true, false));
            Assert.False(orc.DC(null, 19, AbilityType.STRENGTH, false, true));
            Combattant assassin = Monsters.Assassin;
            Assert.True(assassin.DC(null, 2, Skill.STEALTH, true, false));
            Assert.False(assassin.DC(null, 30, Skill.NATURE, false, true));
        }

        [Fact]
        public void AbilityProficienciesTerst() {
            CharacterSheet hero = new CharacterSheet(Race.HILL_DWARF, true);
            hero.AddLevel(CharacterClasses.Barbarian);
            Assert.True(hero.IsProficient(AbilityType.CONSTITUTION));
            Assert.True(hero.IsProficient(Skill.HISTORY));
            hero.DC(Spells.ID.CHARM_PERSON, 10, AbilityType.CONSTITUTION);
            Assert.False(hero.IsProficient(AbilityType.INTELLIGENCE));
            hero.DC(null, 10, AbilityType.INTELLIGENCE);
            Assert.False(hero.IsProficient(AbilityType.NONE));
        }

        [Fact]
        public void HitPointMaxiumModifierTest() {
            Combattant combattant = Monsters.Orc;
            int hpMax = combattant.HitPointsMax;
            HitPointMaxiumModifier modifier1 = new HitPointMaxiumModifier(-10, HitPointMaxiumModifier.RemovedByEffect.GREATER_RESTORATION);
            Assert.False(modifier1.Equals(combattant));
            Assert.NotEqual(0, modifier1.GetHashCode());
            HitPointMaxiumModifier modifier2 = new HitPointMaxiumModifier(5, HitPointMaxiumModifier.RemovedByEffect.AFTER_24_HOURS);
            combattant.AddHitPointMaximumModifiers(modifier1, modifier2);
            Assert.Equal(2, combattant.HitPointMaxiumModifiers.Length);
            Assert.Equal(hpMax - 5, combattant.HitPointsMax);
            combattant.RemoveHitPointsMaximumModifiers(modifier1);
            Assert.Equal(hpMax + 5, combattant.HitPointsMax);
        }

        [Fact]
        public void EscapeFromGrappleTest() {
            Combattant combattant = Monsters.BanditCaptain; // Athelics Proficiency
            Assert.False(combattant.EscapeFromGrapple());
            bool success = false;
            combattant.AddCondition(ConditionType.DEAFENED);
            combattant.AddCondition(ConditionType.GRAPPLED_DC14);
            while (!success) {
                success = combattant.EscapeFromGrapple();
            }
            Assert.False(combattant.HasCondition(ConditionType.GRAPPLED_DC14));
            Assert.True(combattant.HasCondition(ConditionType.DEAFENED));
            combattant = Monsters.Assassin; // Acrobatics Proficiency
            combattant.AddCondition(ConditionType.GRAPPLED_DC16);
            success = false;
            while (!success) {
                success = combattant.EscapeFromGrapple();
            }
            Assert.False(combattant.HasCondition(ConditionType.GRAPPLED_DC14));
            combattant = Monsters.Ankheg; // Str > Dex
            combattant.AddCondition(ConditionType.GRAPPLED_DC16);
            success = false;
            while (!success) {
                success = combattant.EscapeFromGrapple();
            }
            Assert.False(combattant.HasCondition(ConditionType.GRAPPLED_DC14));
            combattant = Monsters.Bat; // Dex > Str
            combattant.AddCondition(ConditionType.GRAPPLED_DC16);
            success = false;
            while (!success) {
                success = combattant.EscapeFromGrapple();
            }
            Assert.False(combattant.HasCondition(ConditionType.GRAPPLED_DC14));
        }

        [Fact]
        public void LockedTargetTest() {
            Combattant combattant = Monsters.Ankheg;
            Combattant bat = Monsters.Bat;
            combattant.MeleeAttacks[0].LockedTarget = bat;
            Assert.False(combattant.Attack(combattant.MeleeAttacks[0], Monsters.Badger, 5));
            bool success = false;
            while (!success) {
                success = combattant.Attack(combattant.MeleeAttacks[0], bat, 5);
            }
            Assert.True(success);
        }

        [Fact]
        public void OutOfReachTest() {
            Combattant ogre = Monsters.Ogre;
            Combattant bat = Monsters.Bat;
            Assert.False(ogre.Attack(Attacks.OgreGreatclub, bat, 8));

        }
    }
}