using System;
using System.Reflection;
using Xunit;

namespace srd5 {
    public class ItemTest {
        [Fact]
        public void NameEqualTest() {
            Weapon club1 = Weapons.Club;
            Weapon club2 = Weapons.Club;
            Assert.Equal(club1.Name, club2.Name);
            Assert.NotEqual(club1.ToString(), club2.ToString());
            Assert.NotEqual(club1.GetHashCode(), club2.GetHashCode());
        }

        [Fact]
        public void UnEqualTest() {
            Weapon club = Weapons.Club;
            Weapon dagger = Weapons.Dagger;
            Assert.False(club.Equals(dagger));
            Assert.False(club.Equals(null));
            Assert.False(club.Equals("not an item"));
            Assert.False(club.IsThisA(null));
            Assert.False(club.Equals(Armors.HideArmor));
        }

        [Fact]
        public void ThingsUnEqualTest() {
            Weapon club1 = Weapons.Club;
            Weapon club2 = Weapons.Club;
            Assert.False(club1.Equals(club2));
            Assert.True(club1.IsThisA(club2));
        }

        [Fact]
        public void GetterTest() {
            foreach (PropertyInfo property in typeof(Weapons).GetProperties()) {
                object o = property.GetMethod.Invoke(null, null);
                Assert.True(o is Weapon);
            }
            foreach (PropertyInfo property in typeof(Armors).GetProperties()) {
                object o = property.GetMethod.Invoke(null, null);
                Assert.True(o is Armor);
            }
            foreach (PropertyInfo property in typeof(Amulets).GetProperties()) {
                object o = property.GetMethod.Invoke(null, null);
                Assert.True(o is Amulet);
            }
            foreach (PropertyInfo property in typeof(Rings).GetProperties()) {
                object o = property.GetMethod.Invoke(null, null);
                Assert.True(o is Ring);
            }
            foreach (PropertyInfo property in typeof(Bootss).GetProperties()) {
                object o = property.GetMethod.Invoke(null, null);
                Assert.True(o is Boots);
            }
            foreach (PropertyInfo property in typeof(Helmets).GetProperties()) {
                object o = property.GetMethod.Invoke(null, null);
                Assert.True(o is Helmet);
            }
        }

        [Fact]
        public void HealingPotionTest() {
            CharacterSheet hero = new CharacterSheet(Race.HALFLING);
            hero.AddLevel(CharacterClasses.Druid);
            Consumable[] potions = new Consumable[] { Potions.PotionOfHealing, Potions.PotionOfGreaterHealing,
                                    Potions.PotionOfSuperiorHealing, Potions.PotionOfSupremeHealing };
            foreach (Consumable potion in potions) {
                hero.TakeDamage(DamageType.SLASHING, 15);
                Assert.True(hero.HasCondition(ConditionType.UNCONSCIOUS));
                hero.Consume(potion);
                Assert.False(hero.HasCondition(ConditionType.UNCONSCIOUS));
            }
        }

        [Fact]
        public void WandOfMagicMissilesTest() {
            CharacterSheet hero = new CharacterSheet(Race.HUMAN);
            Monster shadow = Monsters.Shadow;
            Usable wand = Wands.WandOfMagicMissiles;
            hero.Inventory.AddToBag(wand);
            while (!wand.Destroyed) {
                hero.Use(wand, 7, shadow);
                wand.Charges = 7;
            }
            Assert.True(wand.Destroyed);
            Assert.Empty(hero.Inventory.Bag);
        }
    }
}