using System;
using Xunit;

namespace srd5 {
    public class CharacterSheetTest {
        [Fact]
        public void EquipOneHandTest() {
            Weapon club = Weapons.Club;
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            Assert.Equal(Race.HUMAN.Name(), sheet.Race.Name);
            sheet.Name = "Foo Bar";
            Assert.Equal("Foo Bar", sheet.Name);
            sheet.Equip(club);
            Assert.Equal(club, sheet.Inventory.MainHand);
        }

        [Fact]
        public void EquipTwoHandTest() {
            Weapon greatAxe = Weapons.Greataxe;
            Weapon club = Weapons.Club;
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            sheet.Equip(club);
            sheet.Equip(greatAxe);
            Assert.Equal(greatAxe, sheet.Inventory.MainHand);
            Assert.Null(sheet.Inventory.OffHand);
        }

        [Fact]
        public void EquipDualWieldTest() {
            Weapon greatAxe = Weapons.Greataxe;
            Weapon club1 = Weapons.Club;
            Weapon club2 = Weapons.Club;
            Weapon club3 = Weapons.Club;
            CharacterSheet sheet = new CharacterSheet(Race.DRAGONBORN);
            sheet.Equip(greatAxe);
            sheet.Equip(club1);
            sheet.Equip(club2);
            sheet.Equip(club3);
            Assert.True(sheet.Inventory.MainHand.Equals(club3));
            Assert.True(sheet.Inventory.OffHand.Equals(club2));
            sheet.Unequip(club3);
            sheet.Equip(club2);
            Assert.Null(sheet.Inventory.MainHand);
            Assert.True(sheet.Inventory.OffHand.Equals(club2));
        }

        [Fact]
        public void EquipArmorTest() {
            Armor chainShirt = Armors.ChainShirt;
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            sheet.Dexterity.BaseValue = 18;
            Assert.Equal(14, sheet.ArmorClass);
            sheet.Equip(chainShirt);
            Assert.Equal(15, sheet.ArmorClass);
        }

        [Fact]
        public void EquipMagicArmorTest() {
            Armor chainShirt = Armors.ChainShirt;
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            sheet.Dexterity.BaseValue = 18;
            Assert.Equal(14, sheet.ArmorClass);
            sheet.Equip(chainShirt);
            Assert.Equal(15, sheet.ArmorClass);
            Armor chainShirtPlus1 = Armors.CreatePlus1Armor(Armors.ChainShirt);
            sheet.Equip(chainShirtPlus1);
            Assert.Equal(16, sheet.ArmorClass);
            Armor chainShirtPlus2 = Armors.CreatePlus2Armor(Armors.ChainShirt);
            sheet.Equip(chainShirtPlus2);
            Assert.Equal(17, sheet.ArmorClass);
            Armor chainShirtPlus3 = Armors.CreatePlus3Armor(Armors.ChainShirt);
            sheet.Equip(chainShirtPlus3);
            Assert.Equal(18, sheet.ArmorClass);
        }

        [Fact]
        public void EquipFullTest() {
            Weapon twoHander = Weapons.Greatsword;
            Weapon club = Weapons.Club;
            Weapon club2 = Weapons.Club;
            Shield shield = Shields.Shield;
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            sheet.Equip(twoHander);
            sheet.Equip(shield);
            sheet.Equip(club);
            Assert.Equal(club, sheet.Inventory.MainHand);
            Assert.True(shield.Equals(sheet.Inventory.OffHand));
            sheet.Unequip(shield);
            Assert.Null(sheet.Inventory.OffHand);
            sheet.Equip(twoHander);
            Assert.Equal(twoHander, sheet.Inventory.MainHand);
            sheet.Equip(shield);
            Assert.Null(sheet.Inventory.MainHand);
            Assert.True(shield.Equals(sheet.Inventory.OffHand));
            sheet.Unequip(shield);
            sheet.Equip(club);
            sheet.Equip(club);
            sheet.Equip(club2);
            sheet.Equip(club2);
            sheet.Unequip(club);
            sheet.Equip(club2);
            Assert.Null(sheet.Inventory.MainHand);
            Assert.True(club2.Equals(sheet.Inventory.OffHand));
            Assert.False(club.Equals(sheet.Inventory.OffHand));
            sheet.Equip(shield);
            sheet.Equip(club);
            Assert.True(club.Equals(sheet.Inventory.MainHand));
            Assert.True(shield.Equals(sheet.Inventory.OffHand));
        }

        [Fact]
        public void IllegalRaceTest() {
            Assert.Throws<Srd5ArgumentException>(
                delegate {
                    CharacterRace race = RaceExtension.CharacterRace((Race)(-1));
                }
            );
        }

        [Fact]
        public void BarbarianTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Contains(Feat.RAGE, sheet.Feats);
            Assert.DoesNotContain(Feat.RECKLESS_ATTACK, sheet.Feats);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Contains(Feat.RECKLESS_ATTACK, sheet.Feats);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Equal(2, sheet.AttackProficiency);
            sheet.AddLevel(CharacterClasses.Barbarian);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Equal(3, sheet.AttackProficiency);
            sheet.Strength.BaseValue = 18;
            sheet.Dexterity.BaseValue = 14;
            Weapon club = Weapons.Club;
            sheet.Equip(club);
            Assert.Equal(7, sheet.AttackProficiency);
            Weapon longbow = Weapons.Longbow;
            sheet.Equip(longbow);
            Assert.Equal(5, sheet.AttackProficiency);
        }

        [Fact]
        public void MagicWeaponTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Contains(Feat.RAGE, sheet.Feats);
            Assert.DoesNotContain(Feat.RECKLESS_ATTACK, sheet.Feats);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Contains(Feat.RECKLESS_ATTACK, sheet.Feats);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Equal(2, sheet.AttackProficiency);
            sheet.AddLevel(CharacterClasses.Barbarian);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Equal(3, sheet.AttackProficiency);
            sheet.Strength.BaseValue = 18;
            sheet.Dexterity.BaseValue = 14;
            Weapon club = Weapons.Club;
            sheet.Equip(club);
            Assert.Equal(7, sheet.AttackProficiency);
            Assert.Equal("1d4+4", sheet.MeleeAttacks[0].Damage.Dices.ToString());
            Weapon clubPlus1 = Weapons.CreatePlus1Weapon(Weapons.Club);
            sheet.Unequip(club);
            sheet.Equip(clubPlus1);
            Assert.Equal(8, sheet.AttackProficiency);
            Assert.Equal("1d4+5", sheet.MeleeAttacks[0].Damage.Dices.ToString());
            Weapon clubPlus2 = Weapons.CreatePlus2Weapon(Weapons.Club);
            sheet.Unequip(clubPlus1);
            sheet.Equip(clubPlus2);
            Assert.Equal(9, sheet.AttackProficiency);
            Assert.Equal("1d4+6", sheet.MeleeAttacks[0].Damage.Dices.ToString());
            Weapon clubPlus3 = Weapons.CreatePlus3Weapon(Weapons.Club);
            sheet.Unequip(clubPlus2);
            sheet.Equip(clubPlus3);
            Assert.Equal(10, sheet.AttackProficiency);
            Assert.Equal("1d4+7", sheet.MeleeAttacks[0].Damage.Dices.ToString());
        }

        [Fact]
        public void DruidTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            sheet.Strength.BaseValue = 8;
            sheet.Dexterity.BaseValue = 12;
            sheet.AddLevel(CharacterClasses.Druid);
            sheet.AddLevel(CharacterClasses.Druid);
            sheet.AddLevel(CharacterClasses.Druid);
            Assert.Equal(Class.DRUID.Name(), sheet.Levels[0].Class.Name);
            Assert.Equal(1, sheet.AttackProficiency);
            sheet.AddLevel(CharacterClasses.Druid);
            sheet.AddLevel(CharacterClasses.Druid);
            Assert.Equal(2, sheet.AttackProficiency);
            Weapon greatAxe = Weapons.Greataxe;
            sheet.Equip(greatAxe);
            Assert.Equal(-1, sheet.AttackProficiency);
            Weapon dagger = Weapons.Dagger;
            sheet.Equip(dagger);
            Assert.Equal(4, sheet.AttackProficiency); // Dex bonus because finesse
            sheet.Strength.BaseValue = 14;
            Assert.Equal(5, sheet.AttackProficiency); // Str bonus because greater
            sheet.AddLevel(CharacterClasses.Druid);
            sheet.AddLevel(CharacterClasses.Druid);
            sheet.AddLevel(CharacterClasses.Druid);
            Assert.Equal(5, sheet.AttackProficiency);
            sheet.AddLevel(CharacterClasses.Druid);
            Assert.Equal(6, sheet.AttackProficiency);
        }

        [Fact]
        public void HitPointsTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            sheet.AddLevel(CharacterClasses.Druid);
            Assert.Equal(CharacterClasses.Druid.HitDice, sheet.HitPoints);
            Assert.Equal(CharacterClasses.Druid.HitDice, sheet.HitPointsMax);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.InRange(sheet.HitPoints, CharacterClasses.Druid.HitDice + 1, CharacterClasses.Druid.HitDice + CharacterClasses.Barbarian.HitDice);
            Assert.InRange(sheet.HitPointsMax, CharacterClasses.Druid.HitDice + 1, CharacterClasses.Druid.HitDice + CharacterClasses.Barbarian.HitDice);
        }

        [Fact]
        public void DwarfTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HILL_DWARF);
            Assert.Contains(Feat.DWARVEN_TOUGHNESS, sheet.Feats);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Equal(Race.HILL_DWARF, sheet.Race.Race);
            Assert.Equal(12, sheet.Constitution.Value);
            Assert.True(sheet.IsProficient(null));
            Assert.True(sheet.IsProficient(Proficiency.WARHAMMER));
            Assert.Equal(14, sheet.HitPoints); // 12 barbarian + 1 constituion + 1 racial feat
            Assert.Equal(14, sheet.HitPointsMax); // 12 barbarian + 1 constituion + 1 racial feat
            sheet.Equip(Armors.PlateArmor);
            Assert.Equal(18, sheet.ArmorClass);
            Assert.Equal(25, sheet.Speed);
        }

        [Fact]
        public void HeavyArmorTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            Assert.Equal(30, sheet.Speed);
            Armor plate = Armors.PlateArmor;
            sheet.Equip(plate);
            Assert.Equal(20, sheet.Speed);
            sheet.Unequip(plate);
            Assert.Equal(30, sheet.Speed);
        }

        [Fact]
        public void BlindedTest() {
            CharacterSheet sheet = new CharacterSheet(Race.GNOME);
            Assert.Equal(Size.SMALL, sheet.Size);
            sheet.AddCondition(ConditionType.BLINDED);
            Assert.True(sheet.HasEffect(Effect.DISADVANTAGE_ON_ATTACK));
            sheet.RemoveCondition(ConditionType.BLINDED);
            Assert.False(sheet.HasEffect(Effect.DISADVANTAGE_ON_ATTACK));
        }

        [Fact]
        public void HeadbandOfIntellectTest() {
            CharacterSheet sheet = new CharacterSheet(Race.TIEFLING);
            Assert.Equal(10, sheet.Intelligence.Value);
            Helmet headband = Helmets.HeadbandOfIntellect;
            sheet.Equip(headband);
            Assert.Equal(19, sheet.Intelligence.Value);
            sheet.Unequip(headband);
            Assert.Equal(10, sheet.Intelligence.Value);
        }

        [Fact]
        public void RingTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HALF_ELF);
            Assert.Equal(10, sheet.ArmorClass);
            Ring ring1 = Rings.RingOfSwimming;
            Ring ring2 = Rings.RingOfProtection;
            Ring ring3 = Rings.RingOfProtection;
            sheet.Equip(ring1);
            sheet.Equip(ring2);
            sheet.Equip(ring1);
            sheet.Equip(ring2);
            Assert.Equal(ring2, sheet.Inventory.RingRight);
            Assert.Equal(11, sheet.ArmorClass);
            sheet.Unequip(ring2);
            Assert.Equal(10, sheet.ArmorClass);
            sheet.Equip(ring2);
            Assert.Equal(11, sheet.ArmorClass);
            sheet.Equip(ring3);
            Assert.Equal(11, sheet.ArmorClass);
            Assert.Equal(ring3, sheet.Inventory.RingLeft);
            sheet.Unequip(ring3);
            Assert.Equal(11, sheet.ArmorClass);
            sheet.Unequip(ring2);
            Assert.Equal(10, sheet.ArmorClass);
        }

        [Fact]
        public void AmuletTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HALF_ORC);
            sheet.AddLevel(CharacterClasses.Druid);
            Assert.Equal(8, sheet.HitPointsMax);
            Amulet amulet = Amulets.AmuletOfHealth;
            sheet.Equip(amulet);
            Assert.Equal(19, sheet.Constitution.Value);
            Assert.Equal(12, sheet.HitPointsMax);
            sheet.Unequip(amulet);
            Assert.Equal(8, sheet.HitPointsMax);
        }

        [Fact]
        public void BootsTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HIGH_ELF);
            Boots boots = Bootss.BootsOfTheWinterland;
            sheet.Equip(boots);
            Assert.Equal(boots, sheet.Inventory.Boots);
            Assert.True(sheet.HasEffect(Effect.RESISTANCE_COLD));
            sheet.Unequip(boots);
            Assert.Null(sheet.Inventory.Boots);
        }

        [Fact]
        public void CoverageTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HIGH_ELF);
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Single(sheet.Levels);
            Assert.NotEmpty(sheet.Proficiencies);
            Assert.NotEmpty(sheet.Effects);
            Assert.Single(sheet.HitDice);
            Assert.Empty(sheet.Conditions);
        }

        [Fact]
        public void UnarmedTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HALF_ELF);
            sheet.Strength.BaseValue = 14;
            sheet.AddLevel(CharacterClasses.Barbarian);
            Assert.Single(sheet.MeleeAttacks);
            Assert.Empty(sheet.RangedAttacks);
            Attack unarmed = sheet.MeleeAttacks[0];
            Assert.Equal(4, unarmed.AttackBonus);
            Assert.Equal("1d1+2", unarmed.Damage.Dices.ToString());
            Assert.Equal(3, unarmed.Damage.Dices.Roll());
        }

        [Fact]
        public void MultiAttacksMeleeTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HALF_ELF);
            sheet.Strength.BaseValue = 14;
            sheet.AddLevel(CharacterClasses.Barbarian);
            sheet.AddEffect(Effect.ONE_EXTRA_ATTACK);
            sheet.AddEffect(Effect.TWO_EXTRA_ATTACKS);
            sheet.AddEffect(Effect.THREE_EXTRA_ATTACKS);
            Weapon club = Weapons.Club;
            Weapon dagger = Weapons.Dagger;
            int ac = sheet.ArmorClass;
            sheet.Equip(club);
            sheet.Equip(dagger);
            Assert.Equal(ac, sheet.ArmorClass);
            Assert.Equal(4, sheet.MeleeAttacks.Length);
            Assert.Equal(dagger.Name, sheet.BonusAttack.Name);
            sheet.RemoveEffect(Effect.THREE_EXTRA_ATTACKS);
            Assert.Equal(3, sheet.MeleeAttacks.Length);
            sheet.RemoveEffect(Effect.TWO_EXTRA_ATTACKS);
            Assert.Equal(2, sheet.MeleeAttacks.Length);
            sheet.RemoveEffect(Effect.ONE_EXTRA_ATTACK);
            Assert.Single(sheet.MeleeAttacks);
        }

        [Fact]
        public void MultiAttacksRangedTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HALF_ELF);
            sheet.Dexterity.BaseValue = 14;
            sheet.AddLevel(CharacterClasses.Barbarian);
            sheet.AddEffect(Effect.ONE_EXTRA_ATTACK);
            sheet.AddEffect(Effect.TWO_EXTRA_ATTACKS);
            sheet.AddEffect(Effect.THREE_EXTRA_ATTACKS);
            Weapon longbow = Weapons.Longbow;
            sheet.Equip(longbow);
            Assert.Equal(4, sheet.RangedAttacks.Length);
            sheet.RemoveEffect(Effect.THREE_EXTRA_ATTACKS);
            Assert.Equal(3, sheet.RangedAttacks.Length);
            sheet.RemoveEffect(Effect.TWO_EXTRA_ATTACKS);
            Assert.Equal(2, sheet.RangedAttacks.Length);
            sheet.RemoveEffect(Effect.ONE_EXTRA_ATTACK);
            Assert.Single(sheet.RangedAttacks);
        }

        [Fact]
        public void VersatileTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HALFLING);
            Weapon quarterstaff = Weapons.Quarterstaff;
            sheet.Equip(quarterstaff);
            Assert.Equal(8, sheet.MeleeAttacks[0].Damage.Dices.Dice);
            int ac = sheet.ArmorClass;
            Shield shield = Shields.Shield;
            Assert.Equal(quarterstaff, sheet.Inventory.MainHand);
            sheet.Equip(shield);
            Assert.Equal(ac + shield.AC, sheet.ArmorClass);
            Assert.Equal(6, sheet.MeleeAttacks[0].Damage.Dices.Dice);
        }

        [Fact]
        public void SkillModifierTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            sheet.Strength.BaseValue = 8;
            sheet.Wisdom.BaseValue = 14;
            sheet.Charisma.BaseValue = 10;
            sheet.Dexterity.BaseValue = 16;
            sheet.Intelligence.BaseValue = 1;
            Assert.Equal(3, sheet.GetSkillModifier(Skill.STEALTH));
            Assert.Equal(-1, sheet.GetSkillModifier(Skill.ATHLETICS));
            Assert.Equal(0, sheet.GetSkillModifier(Skill.PERFORMANCE));
            Assert.Equal(-5, sheet.GetSkillModifier(Skill.HISTORY));
            Assert.Equal(2, sheet.GetSkillModifier(Skill.PERCEPTION));
            sheet.AddProficiency(Proficiency.STEALTH);
            sheet.AddProficiency(Proficiency.HISTORY);
            sheet.AddEffect(Effect.DOUBLE_PROFICIENCY_BONUS_HISTORY);
            Assert.Equal(5, sheet.GetSkillModifier(Skill.STEALTH));
            Assert.Equal(-1, sheet.GetSkillModifier(Skill.HISTORY));
        }


        [Fact]
        public void ClassCreationTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN, true);
            Assert.InRange<int>(sheet.Strength.Value, 4, 19);
            Assert.InRange<int>(sheet.Constitution.Value, 4, 19);
            Assert.InRange<int>(sheet.Dexterity.Value, 4, 19);
            Assert.InRange<int>(sheet.Wisdom.Value, 4, 19);
            Assert.InRange<int>(sheet.Intelligence.Value, 4, 19);
            Assert.InRange<int>(sheet.Charisma.Value, 4, 19);
        }

        [Fact]
        public void AbilitiyIncreaseTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HUMAN);
            int points = sheet.AbilityPoints;
            sheet.IncreaseAbility(AbilityType.STRENGTH);
            sheet.IncreaseAbility(AbilityType.DEXTERITY);
            sheet.IncreaseAbility(AbilityType.CONSTITUTION);
            sheet.IncreaseAbility(AbilityType.INTELLIGENCE);
            sheet.IncreaseAbility(AbilityType.WISDOM);
            sheet.IncreaseAbility(AbilityType.CHARISMA);
            Assert.Equal(points - 6, sheet.AbilityPoints);
        }

        [Fact]
        public void EndOfTurnEventTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HALFLING, true);
            int i = 0;
            TurnEvent endOfTurnEventTrue = delegate () {
                i++;
                return true;
            };
            TurnEvent endOfTurnEventFalse = delegate () {
                i++;
                return false;
            };
            sheet.AddEndOfTurnEvent(endOfTurnEventFalse);
            sheet.AddEndOfTurnEvent(endOfTurnEventTrue);
            sheet.OnEndOfTurn();
            sheet.OnEndOfTurn();
            Assert.Equal(3, i);
        }

        [Fact]
        public void BagTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HALFLING);
            Weapon club = Weapons.Club;
            sheet.Inventory.AddToBag(club);
            Assert.NotEmpty(sheet.Inventory.Bag);
            sheet.Inventory.RemoveFromBag(club);
            Assert.Empty(sheet.Inventory.Bag);
        }

        [Fact]
        public void LongRestTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HALFLING);
            sheet.AddLevel(CharacterClasses.Barbarian);
            sheet.TakeDamage(DamageType.SLASHING, 2);
            Assert.True(sheet.HitPointsMax > sheet.HitPoints);
            sheet.LongRest();
            Assert.Equal(sheet.HitPointsMax, sheet.HitPoints);
        }

        [Fact]
        public void UnequipWrongItemTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HALFLING);
            Armor hide = Armors.HideArmor;
            Armor chain = Armors.ChainShirt;
            Weapon dagger = Weapons.Dagger;
            sheet.Equip(hide);
            sheet.Equip((Armor)null);
            Assert.Equal(hide, sheet.Inventory.Armor);
            sheet.Unequip(null);
            sheet.Unequip(chain);
            sheet.Unequip(dagger);
            Assert.Equal(hide, sheet.Inventory.Armor);
        }

        [Fact]
        public void CannotEquipOrUseDestroyedItems() {
            CharacterSheet sheet = new CharacterSheet(Race.HILL_DWARF);
            sheet.AddLevel(CharacterClasses.Barbarian);
            sheet.HitPoints = 1;
            Armor chain = Armors.ChainMailArmor;
            chain.Destroy();
            Consumable potion = Potions.PotionOfSuperiorHealing;
            potion.Destroy();
            Usable wand = Wands.WandOfMagicMissiles;
            sheet.Equip(chain);
            sheet.Consume(null);
            sheet.Consume(potion);
            sheet.Use(null, 7, sheet);
            int charges = wand.Charges;
            sheet.Use(wand, charges + 1, sheet);
            Assert.Equal(charges, wand.Charges);
            wand.Destroy();
            sheet.Use(wand, 7, sheet);
            Assert.Null(sheet.Inventory.Armor);
            Assert.Equal(1, sheet.HitPoints);
        }

        [Fact]
        public void HitPointMaxiumModifierTest() {
            CharacterSheet hero = new CharacterSheet(Race.HUMAN);
            hero.AddLevel(CharacterClasses.Barbarian);
            int hpMax = hero.HitPointsMax;
            HitPointMaxiumModifier modifier1 = new HitPointMaxiumModifier(-10, HitPointMaxiumModifier.RemovedByEffect.GREATER_RESTORATION);
            Assert.False(modifier1.Equals(hero));
            Assert.NotEqual(0, modifier1.GetHashCode());
            HitPointMaxiumModifier modifier2 = new HitPointMaxiumModifier(5, HitPointMaxiumModifier.RemovedByEffect.AFTER_24_HOURS);
            hero.AddHitPointMaximumModifiers(modifier1, modifier2);
            Assert.Equal(hpMax - 5, hero.HitPointsMax);
            hero.RemoveHitPointsMaximumModifiers(modifier1);
            Assert.Equal(hpMax + 5, hero.HitPointsMax);
        }
    }
}