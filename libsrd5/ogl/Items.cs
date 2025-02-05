namespace srd5 {
    public enum WeaponProperty {
        AMMUNITION,
        FINESSE,
        HEAVY,
        LIGHT,
        LOADING,
        RANGED,
        REACH,
        SPECIAL_LANCE,
        SPECIAL_NET,
        THROWN,
        TWO_HANDED,
        VERSATILE,
        MAGIC,
        PLUS_1,
        PLUS_2,
        PLUS_3
    }

    public enum ArmorProperty {
        STEALTH_DISADVANTAGE,
        LIGHT,
        MEDIUM,
        MAGIC,
        HEAVY,
        METAL,
        PLUS_1,
        PLUS_2,
        PLUS_3
    }

    public enum ItemType {
        ARMOR,
        SHIELD,
        WEAPON,
        TOOL,
        HELMET,
        RING,
        AMULET,
        BOOTS,
        POTION,
        WAND
    }

    public enum DamageType {
        TRUE_DAMAGE, // unresistable damage
        ACID,
        BLUDGEONING,
        COLD,
        FIRE,
        FORCE,
        LIGHTNING,
        NECROTIC,
        PIERCING,
        POISON,
        PSYCHIC,
        RADIANT,
        SLASHING,
        THUNDER
    }

    public enum ItemRarity {
        COMMON,
        UNCOMMON,
        RARE,
        VERY_RARE
    }

    public struct Weapons {
        private static Weapon createMagicWeapon(Weapon input, string name, WeaponProperty[] props) {
            return new Weapon(
                name,
                input.Damage.Dice.ToString(),
                input.Damage.Type,
                props,
                input.Proficiencies,
                input.Value,
                input.Weight
            );
        }

        public static Weapon CreatePlus1Weapon(Weapon input) {
            WeaponProperty[] props = input.Properties;
            Utils.Push<WeaponProperty>(ref props, WeaponProperty.MAGIC);
            Utils.Push<WeaponProperty>(ref props, WeaponProperty.PLUS_1);
            return createMagicWeapon(input, input.Name + " +1", props);
        }

        public static Weapon CreatePlus2Weapon(Weapon input) {
            WeaponProperty[] props = input.Properties;
            Utils.Push<WeaponProperty>(ref props, WeaponProperty.MAGIC);
            Utils.Push<WeaponProperty>(ref props, WeaponProperty.PLUS_2);
            return createMagicWeapon(input, input.Name + " +2", props);
        }

        public static Weapon CreatePlus3Weapon(Weapon input) {
            WeaponProperty[] props = input.Properties;
            Utils.Push<WeaponProperty>(ref props, WeaponProperty.MAGIC);
            Utils.Push<WeaponProperty>(ref props, WeaponProperty.PLUS_3);
            return createMagicWeapon(input, input.Name + " +3", props);
        }

        // Simple Meelee Weapons
        public static Weapon Club {
            get {
                return new Weapon(
                           "Club",
                           "1d4",
                           DamageType.BLUDGEONING,
                           new WeaponProperty[] { WeaponProperty.LIGHT },
                           new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.CLUB },
                           10,
                           2
               );
            }
        }

        public static Weapon Dagger {
            get {
                return new Weapon(
                           "Dagger", "1d4", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.LIGHT, WeaponProperty.FINESSE, WeaponProperty.THROWN },
                           new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.DAGGER },
                           200, 1, 20, 60
               );
            }
        }

        public static Weapon Greatclub {
            get {
                return new Weapon(
                           "Greatclub", "1d8", DamageType.BLUDGEONING,
                           new WeaponProperty[] { WeaponProperty.TWO_HANDED },
                           new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.GREATCLUB },
                           20, 10
               );
            }
        }

        public static Weapon Handaxe {
            get {
                return new Weapon(
                           "Handaxe", "1d6", DamageType.SLASHING,
                           new WeaponProperty[] { WeaponProperty.LIGHT, WeaponProperty.THROWN },
                           new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.HANDAXE },
                           500, 2, 5, 20, 60
               );
            }
        }

        public static Weapon Javelin {
            get {
                return new Weapon(
                           "Javelin", "1d6", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.THROWN },
                           new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.JAVELIN },
                           200, 2, 30, 120
               );
            }
        }

        public static Weapon Mace {
            get {
                return new Weapon(
                           "Mace", "1d6", DamageType.BLUDGEONING,
                           new WeaponProperty[] { },
                           new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.MACE },
                           500, 4
               );
            }
        }

        public static Weapon Quarterstaff {
            get {
                return new Weapon(
                           "Quarterstaff", "1d6", DamageType.BLUDGEONING,
                           new WeaponProperty[] { WeaponProperty.VERSATILE },
                           new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.QUARTERSTAFF },
                           20, 4
               );
            }
        }

        public static Weapon Sickle {
            get {
                return new Weapon(
                           "Sickle", "1d4", DamageType.SLASHING,
                           new WeaponProperty[] { WeaponProperty.LIGHT },
                           new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.SICKLE },
                           10, 2
               );
            }
        }

        public static Weapon Spear {
            get {
                return new Weapon(
                           "Spear", "1d6", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.THROWN, WeaponProperty.VERSATILE },
                           new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.SPEAR },
                           10, 3, 5, 20, 60
               );
            }
        }

        // Simple Ranged Weapons
        public static Weapon LightCrossbow {
            get {
                return new Weapon(
                           "Light Crossbow", "1d8", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.AMMUNITION, WeaponProperty.LOADING, WeaponProperty.TWO_HANDED },
                           new Proficiency[] { Proficiency.SIMPLE_RANGED_WEAPONS, Proficiency.CROSSBOW_LIGHT },
                           2500, 5, 0, 80, 320
               );
            }
        }

        public static Weapon Dart {
            get {
                return new Weapon(
                           "Dart", "1d4", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.THROWN, WeaponProperty.FINESSE },
                           new Proficiency[] { Proficiency.SIMPLE_RANGED_WEAPONS, Proficiency.DARTS },
                           5, 0.25f, 0, 20, 60
               );
            }
        }

        public static Weapon Shortbow {
            get {
                return new Weapon(
                           "Shortbow", "1d6", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.AMMUNITION, WeaponProperty.TWO_HANDED },
                           new Proficiency[] { Proficiency.SIMPLE_RANGED_WEAPONS, Proficiency.SHORTBOW },
                           2500, 2, 0, 80, 320
               );
            }
        }

        public static Weapon Sling {
            get {
                return new Weapon(
                           "Sling", "1d4", DamageType.BLUDGEONING,
                           new WeaponProperty[] { WeaponProperty.AMMUNITION },
                           new Proficiency[] { Proficiency.SIMPLE_RANGED_WEAPONS, Proficiency.SLING },
                           10, 0, 0, 30, 120
               );
            }
        }

        // Martial Melee Weapons
        public static Weapon Battleaxe {
            get {
                return new Weapon(
                           "Battleaxe", "1d8", DamageType.SLASHING,
                           new WeaponProperty[] { WeaponProperty.VERSATILE },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.BATTLEAXE },
                           1000, 4
               );
            }
        }

        public static Weapon Flail {
            get {
                return new Weapon(
                           "Greataxe", "1d8", DamageType.BLUDGEONING,
                           new WeaponProperty[] { },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.FLAIL },
                           1000, 2
               );
            }
        }

        public static Weapon Glaive {
            get {
                return new Weapon(
                           "Glaive", "1d10", DamageType.SLASHING,
                           new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY, WeaponProperty.REACH },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.GLAIVE },
                           2000, 6, 10
               );
            }
        }

        public static Weapon Greataxe {
            get {
                return new Weapon(
                           "Greataxe", "1d12", DamageType.SLASHING,
                           new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.GREATAXE },
                           3000, 7
               );
            }
        }

        public static Weapon Greatsword {
            get {
                return new Weapon(
                           "Greataxe", "2d6", DamageType.SLASHING,
                           new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.GREATSWORD },
                           5000, 6
               );
            }
        }

        public static Weapon Halberd {
            get {
                return new Weapon(
                           "Halberd", "1d10", DamageType.SLASHING,
                           new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY, WeaponProperty.REACH },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.HALBERD },
                           2000, 6, 10
               );
            }
        }

        public static Weapon Lance {
            get {
                return new Weapon(
                           "Lance", "1d10", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.REACH, WeaponProperty.SPECIAL_LANCE },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.LANCE },
                           1000, 6, 10
               );
            }
        }

        public static Weapon Longsword {
            get {
                return new Weapon(
                           "Longsword", "1d8", DamageType.SLASHING,
                           new WeaponProperty[] { WeaponProperty.VERSATILE },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.LONGSWORD },
                           1500, 3
               );
            }
        }

        public static Weapon Maul {
            get {
                return new Weapon(
                           "Maul", "2d6", DamageType.BLUDGEONING,
                           new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.MAUL },
                           1000, 10
               );
            }
        }

        public static Weapon Morningstar {
            get {
                return new Weapon(
                           "Morningstar", "1d8", DamageType.PIERCING,
                           new WeaponProperty[] { },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.MORNINGSTAR },
                           1500, 4
               );
            }
        }

        public static Weapon Pike {
            get {
                return new Weapon(
                           "Greataxe", "1d10", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY, WeaponProperty.REACH },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.PIKE },
                           500, 18, 10
               );
            }
        }

        public static Weapon Rapier {
            get {
                return new Weapon(
                           "Rapier", "1d8", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.FINESSE },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.RAPIER },
                           2500, 2
               );
            }
        }

        public static Weapon Scimitar {
            get {
                return new Weapon(
                           "Scimitar", "1d6", DamageType.SLASHING,
                           new WeaponProperty[] { WeaponProperty.FINESSE, WeaponProperty.LIGHT },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.SCIMITAR },
                           2500, 3
               );
            }
        }

        public static Weapon Shortsword {
            get {
                return new Weapon(
                           "Shortsword", "1d6", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.FINESSE, WeaponProperty.LIGHT },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.SHORTSWORD },
                           1000, 2
               );
            }
        }

        public static Weapon Trident {
            get {
                return new Weapon(
                           "Trident", "1d6", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.VERSATILE, WeaponProperty.THROWN },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.TRIDENT },
                           500, 4, 5, 20, 60
               );
            }
        }

        public static Weapon WarPick {
            get {
                return new Weapon(
                           "War pick", "1d8", DamageType.PIERCING,
                           new WeaponProperty[] { },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.WAR_PICK },
                           500, 2
               );
            }
        }

        public static Weapon Warhammer {
            get {
                return new Weapon(
                           "Warhammer", "1d8", DamageType.BLUDGEONING,
                           new WeaponProperty[] { WeaponProperty.VERSATILE },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.WARHAMMER },
                           1500, 2
               );
            }
        }

        public static Weapon Whip {
            get {
                return new Weapon(
                           "Whip", "1d4", DamageType.SLASHING,
                           new WeaponProperty[] { WeaponProperty.FINESSE, WeaponProperty.REACH },
                           new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.WHIP },
                           200, 3, 10
               );
            }
        }

        // Martial Ranged Weapons

        public static Weapon Blowgun {
            get {
                return new Weapon(
                           "Blowgun", "1d1", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.LOADING, WeaponProperty.AMMUNITION },
                           new Proficiency[] { Proficiency.MARTIAL_RANGED_WEAPONS, Proficiency.BLOWGUN },
                           1000, 1, 0, 25, 100
               );
            }
        }

        public static Weapon HandCrossbow {
            get {
                return new Weapon(
                           "Hand Crossbow", "1d6", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.LOADING, WeaponProperty.LIGHT, WeaponProperty.AMMUNITION },
                           new Proficiency[] { Proficiency.MARTIAL_RANGED_WEAPONS, Proficiency.CROSSBOW_HAND },
                           7500, 3, 0, 30, 120
               );
            }
        }

        public static Weapon HeavyCrossbow {
            get {
                return new Weapon(
                           "Heavy Crossbow", "1d10", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY, WeaponProperty.AMMUNITION, WeaponProperty.LOADING },
                           new Proficiency[] { Proficiency.MARTIAL_RANGED_WEAPONS, Proficiency.CROSSBOW_HEAVY },
                           5000, 18, 0, 100, 400
               );
            }
        }

        public static Weapon Longbow {
            get {
                return new Weapon(
                           "Longbow", "1d8", DamageType.PIERCING,
                           new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY, WeaponProperty.AMMUNITION },
                           new Proficiency[] { Proficiency.MARTIAL_RANGED_WEAPONS, Proficiency.LONGBOW },
                           5000, 2, 0, 150, 600
               );
            }
        }

        public static Weapon Net {
            get {
                return new Weapon(
                           "Net", "1d1-1", DamageType.BLUDGEONING,
                           new WeaponProperty[] { WeaponProperty.THROWN, WeaponProperty.SPECIAL_NET },
                           new Proficiency[] { Proficiency.MARTIAL_RANGED_WEAPONS, Proficiency.NET },
                           100, 3, 0, 5, 15
               );
            }
        }
    }

    public struct Shields {
        public static readonly Shield Shield = new Shield("Shield", 2, 1000, 6);
    }

    public struct Armors {
        private static Armor createMagicArmor(Armor armor, string name, int ac, ArmorProperty[] properties) {
            return new Armor(name, ac, armor.MaxDexBonus, armor.Strength, armor.Proficiencies[0], properties, armor.Value, armor.Weight);
        }

        public static Armor CreatePlus1Armor(Armor armor) {
            ArmorProperty[] properties = armor.Properties;
            Utils.Push<ArmorProperty>(ref properties, ArmorProperty.PLUS_1);
            Utils.Push<ArmorProperty>(ref properties, ArmorProperty.MAGIC);
            return createMagicArmor(armor, armor.Name + " +1", armor.AC + 1, properties);
        }

        public static Armor CreatePlus2Armor(Armor armor) {
            ArmorProperty[] properties = armor.Properties;
            Utils.Push<ArmorProperty>(ref properties, ArmorProperty.PLUS_2);
            Utils.Push<ArmorProperty>(ref properties, ArmorProperty.MAGIC);
            return createMagicArmor(armor, armor.Name + " +2", armor.AC + 2, properties);
        }

        public static Armor CreatePlus3Armor(Armor armor) {
            ArmorProperty[] properties = armor.Properties;
            Utils.Push<ArmorProperty>(ref properties, ArmorProperty.PLUS_3);
            Utils.Push<ArmorProperty>(ref properties, ArmorProperty.MAGIC);
            return createMagicArmor(armor, armor.Name + " +3", armor.AC + 3, properties);
        }

        // Light Armor
        public static Armor PaddedArmor {
            get {
                return new Armor("Padded armor", 11, 20, 0, Proficiency.LIGHT_ARMOR,
                           new ArmorProperty[] { ArmorProperty.LIGHT, ArmorProperty.STEALTH_DISADVANTAGE }, 500, 8);
            }
        }
        public static Armor LeatherArmor {
            get {
                return new Armor("Leather armor", 11, 20, 0, Proficiency.LIGHT_ARMOR,
                           new ArmorProperty[] { ArmorProperty.LIGHT }, 1000, 10);
            }
        }
        public static Armor StuddedLeatherArmor {
            get {
                return new Armor("Studded leather armor", 12, 20, 0, Proficiency.LIGHT_ARMOR,
                           new ArmorProperty[] { ArmorProperty.LIGHT }, 4500, 13);
            }
        }
        // Medium Armor
        public static Armor HideArmor {
            get {
                return new Armor("Hide armor", 12, 2, 0, Proficiency.MEDIUM_ARMOR,
                            new ArmorProperty[] { ArmorProperty.MEDIUM }, 1000, 12);
            }
        }
        public static Armor ChainShirt {
            get {
                return new Armor("Chain shirt", 13, 2, 0, Proficiency.MEDIUM_ARMOR,
                           new ArmorProperty[] { ArmorProperty.MEDIUM, ArmorProperty.METAL }, 5000, 20);
            }
        }
        public static Armor ScaleMailArmor {
            get {
                return new Armor("Scale mail armor", 14, 2, 0, Proficiency.MEDIUM_ARMOR,
                           new ArmorProperty[] { ArmorProperty.MEDIUM, ArmorProperty.STEALTH_DISADVANTAGE, ArmorProperty.METAL }, 5000, 45);
            }
        }
        public static Armor Breastplate {
            get {
                return new Armor("Breastplate", 14, 2, 0, Proficiency.MEDIUM_ARMOR,
                           new ArmorProperty[] { ArmorProperty.MEDIUM, ArmorProperty.METAL }, 40000, 20);
            }
        }

        public static Armor HalfPlateArmor {
            get {
                return new Armor("Half plate armor", 15, 2, 0, Proficiency.MEDIUM_ARMOR,
                           new ArmorProperty[] { ArmorProperty.MEDIUM, ArmorProperty.STEALTH_DISADVANTAGE, ArmorProperty.METAL }, 75000, 40);
            }
        }
        // Heavy Armor
        public static Armor RingMailArmor {
            get {
                return new Armor("Ring mail armor", 14, 0, 0, Proficiency.HEAVY_ARMOR,
                           new ArmorProperty[] { ArmorProperty.HEAVY, ArmorProperty.STEALTH_DISADVANTAGE, ArmorProperty.METAL }, 3000, 40);
            }
        }
        public static Armor ChainMailArmor {
            get {
                return new Armor("Chain mail armor", 16, 0, 13, Proficiency.HEAVY_ARMOR,
                           new ArmorProperty[] { ArmorProperty.HEAVY, ArmorProperty.STEALTH_DISADVANTAGE, ArmorProperty.METAL }, 7500, 65);
            }
        }
        public static Armor SplintArmor {
            get {
                return new Armor("Splint armor", 17, 0, 15, Proficiency.HEAVY_ARMOR,
                           new ArmorProperty[] { ArmorProperty.HEAVY, ArmorProperty.STEALTH_DISADVANTAGE, ArmorProperty.METAL }, 20000, 60);
            }
        }
        public static Armor PlateArmor {
            get {
                return new Armor("Plate armor", 18, 0, 15, Proficiency.HEAVY_ARMOR,
                           new ArmorProperty[] { ArmorProperty.HEAVY, ArmorProperty.STEALTH_DISADVANTAGE, ArmorProperty.METAL }, 150000, 65);
            }
        }

        public static Armor MageArmor {
            get {
                return new Armor("Mage armor", 13, 10, 0, Proficiency.LIGHT_ARMOR, new ArmorProperty[] { }, 0, 0);
            }
        }
    }

    public struct Rings {
        public static Ring RingOfSwimming { get { return new Ring("Ring of Swimming", new Effect[] { Effect.SWIMMING_SPEED_40 }); } }
        public static Ring RingOfProtection { get { return new Ring("Ring of Protection", new Effect[] { Effect.PROTECTION }); } }
    }

    public struct Helmets {
        public static Helmet HeadbandOfIntellect { get { return new Helmet("Headband of Intellect", new Effect[] { Effect.INTELLIGENCE_19 }); } }
    }

    public struct Amulets {
        public static Amulet AmuletOfHealth { get { return new Amulet("Amulet of Health", new Effect[] { Effect.CONSTITUTION_19 }); } }
    }

    public struct Bootss { // additional s because double plural ^_^
        public static Boots BootsOfTheWinterland { get { return new Boots("Boots of the Winterland", new Effect[] { Effect.RESISTANCE_COLD, Effect.IGNORE_SNOW_PENALITY }); } }
    }

    public struct Potions {
        public static Consumable PotionOfHealing {
            get {
                return new Consumable("Potion of Healing", ItemType.POTION, delegate (Combattant consumer, Consumable potion) {
                    Dice healing = new Dice("2d4+2");
                    consumer.HealDamage(healing.Roll());
                    potion.Charges = 0;
                    potion.Destroyed = true;
                }, ItemRarity.COMMON);
            }
        }

        public static Consumable PotionOfGreaterHealing {
            get {
                return new Consumable("Potion of Greater Healing", ItemType.POTION, delegate (Combattant consumer, Consumable potion) {
                    Dice healing = new Dice("4d4+4");
                    consumer.HealDamage(healing.Roll());
                    potion.Charges = 0;
                    potion.Destroyed = true;
                }, ItemRarity.UNCOMMON);
            }
        }

        public static Consumable PotionOfSuperiorHealing {
            get {
                return new Consumable("Potion of Superior Healing", ItemType.POTION, delegate (Combattant consumer, Consumable potion) {
                    Dice healing = new Dice("8d4+8");
                    consumer.HealDamage(healing.Roll());
                    potion.Charges = 0;
                    potion.Destroyed = true;
                }, ItemRarity.RARE);
            }
        }

        public static Consumable PotionOfSupremeHealing {
            get {
                return new Consumable("Potion of Supreme Healing", ItemType.POTION, delegate (Combattant consumer, Consumable potion) {
                    Dice healing = new Dice("10d4+20");
                    consumer.HealDamage(healing.Roll());
                    potion.Charges = 0;
                    potion.Destroyed = true;
                }, ItemRarity.VERY_RARE);
            }
        }

        public static Consumable Goodberry {
            get {
                return new Consumable("Goodberry", ItemType.POTION, delegate (Combattant consumer, Consumable berry) {
                    consumer.HealDamage(1);
                    berry.Charges = 0;
                    berry.Destroyed = true;
                }, ItemRarity.COMMON);
            }
        }
    }

    public struct Wands {
        public static Usable WandOfMagicMissiles {
            get {
                return new Usable("Wand of Magic Missiles", ItemType.WAND, delegate (Battleground ground, Combattant user, Usable item, int expendedCharges, Combattant[] targets) {
                    Spells.MagicMissile.Cast(ground, user, 0, (SpellLevel)expendedCharges, 0, targets);
                    item.Charges -= expendedCharges;
                    if (item.Charges == 0) {
                        int roll = Die.D20.Value;
                        if (roll == 1) {
                            item.Destroyed = true;
                        }
                    }
                }, ItemRarity.UNCOMMON, 20, 7, 7);
            }
        }
    }
}