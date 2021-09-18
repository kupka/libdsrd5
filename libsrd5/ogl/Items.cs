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
        VERSATILE
    }
    public enum ItemType {
        ARMOR,
        SHIELD,
        WEAPON,
        TOOL,
        HELMET,
        RING,
        AMULET,
        BOOTS
    }

    public enum DamageType {
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

    public struct Weapons {
        // Simple Meelee Weapons
        public static readonly Weapon Club = new Weapon(
            "Club",
            "1d4",
            DamageType.BLUDGEONING,
            new WeaponProperty[] { WeaponProperty.LIGHT },
            new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.CLUB },
            10,
            2
        );

        public static readonly Weapon Dagger = new Weapon(
            "Dagger", "1d4", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.LIGHT, WeaponProperty.FINESSE, WeaponProperty.THROWN },
            new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.DAGGER },
            200, 1, 20, 60
        );

        public static readonly Weapon Greatclub = new Weapon(
            "Greatclub", "1d8", DamageType.BLUDGEONING,
            new WeaponProperty[] { WeaponProperty.TWO_HANDED },
            new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.GREATCLUB },
            20, 10
        );

        public static readonly Weapon Handaxe = new Weapon(
            "Handaxe", "1d6", DamageType.SLASHING,
            new WeaponProperty[] { WeaponProperty.LIGHT, WeaponProperty.THROWN },
            new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.HANDAXE },
            500, 2, 5, 20, 60
        );

        public static readonly Weapon Javelin = new Weapon(
            "Javelin", "1d6", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.THROWN },
            new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.JAVELIN },
            200, 2, 30, 120
        );

        public static readonly Weapon Mace = new Weapon(
            "Mace", "1d6", DamageType.BLUDGEONING,
            new WeaponProperty[] { },
            new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.MACE },
            500, 4
        );

        public static readonly Weapon Quarterstaff = new Weapon(
            "Quarterstaff", "1d6", DamageType.BLUDGEONING,
            new WeaponProperty[] { WeaponProperty.VERSATILE },
            new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.QUARTERSTAFF },
            20, 4
        );

        public static readonly Weapon Sickle = new Weapon(
            "Sickle", "1d4", DamageType.SLASHING,
            new WeaponProperty[] { WeaponProperty.LIGHT },
            new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.SICKLE },
            10, 2
        );

        public static readonly Weapon Spear = new Weapon(
            "Spear", "1d6", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.THROWN, WeaponProperty.VERSATILE },
            new Proficiency[] { Proficiency.SIMPLE_MELEE_WEAPONS, Proficiency.SPEAR },
            10, 3, 5, 20, 60
        );

        // Simple Ranged Weapons
        public static readonly Weapon LightCrossbow = new Weapon(
            "Light Crossbow", "1d8", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.AMMUNITION, WeaponProperty.LOADING, WeaponProperty.TWO_HANDED },
            new Proficiency[] { Proficiency.SIMPLE_RANGED_WEAPONS, Proficiency.CROSSBOW_LIGHT },
            2500, 5, 0, 80, 320
        );

        public static readonly Weapon Dart = new Weapon(
            "Dart", "1d4", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.THROWN, WeaponProperty.FINESSE },
            new Proficiency[] { Proficiency.SIMPLE_RANGED_WEAPONS, Proficiency.DARTS },
            5, 0.25f, 0, 20, 60
        );

        public static readonly Weapon Shortbow = new Weapon(
            "Shortbow", "1d6", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.AMMUNITION, WeaponProperty.TWO_HANDED },
            new Proficiency[] { Proficiency.SIMPLE_RANGED_WEAPONS, Proficiency.SHORTBOW },
            2500, 2, 0, 80, 320
        );

        public static readonly Weapon Sling = new Weapon(
            "Sling", "1d4", DamageType.BLUDGEONING,
            new WeaponProperty[] { WeaponProperty.AMMUNITION },
            new Proficiency[] { Proficiency.SIMPLE_RANGED_WEAPONS, Proficiency.SLING },
            10, 0, 0, 30, 120
        );

        // Martial Melee Weapons
        public static readonly Weapon Battleaxe = new Weapon(
            "Battleaxe", "1d8", DamageType.SLASHING,
            new WeaponProperty[] { WeaponProperty.VERSATILE },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.BATTLEAXE },
            1000, 4
        );

        public static readonly Weapon Flail = new Weapon(
            "Greataxe", "1d8", DamageType.BLUDGEONING,
            new WeaponProperty[] { },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.FLAIL },
            1000, 2
        );

        public static readonly Weapon Glaive = new Weapon(
            "Glaive", "1d10", DamageType.SLASHING,
            new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY, WeaponProperty.REACH },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.GLAIVE },
            2000, 6, 10
        );

        public static readonly Weapon Greataxe = new Weapon(
            "Greataxe", "1d12", DamageType.SLASHING,
            new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.GREATAXE },
            3000, 7
        );

        public static readonly Weapon Greatsword = new Weapon(
            "Greataxe", "2d6", DamageType.SLASHING,
            new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.GREATSWORD },
            5000, 6
        );

        public static readonly Weapon Halberd = new Weapon(
            "Halberd", "1d10", DamageType.SLASHING,
            new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY, WeaponProperty.REACH },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.HALBERD },
            2000, 6, 10
        );

        public static readonly Weapon Lance = new Weapon(
            "Lance", "1d10", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.REACH, WeaponProperty.SPECIAL_LANCE },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.LANCE },
            1000, 6, 10
        );

        public static readonly Weapon Longsword = new Weapon(
            "Longsword", "1d8", DamageType.SLASHING,
            new WeaponProperty[] { WeaponProperty.VERSATILE },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.LONGSWORD },
            1500, 3
        );

        public static readonly Weapon Maul = new Weapon(
            "Maul", "2d6", DamageType.BLUDGEONING,
            new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.MAUL },
            1000, 10
        );

        public static readonly Weapon Morningstar = new Weapon(
            "Morningstar", "1d8", DamageType.PIERCING,
            new WeaponProperty[] { },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.MORNINGSTAR },
            1500, 4
        );

        public static readonly Weapon Pike = new Weapon(
            "Greataxe", "1d10", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY, WeaponProperty.REACH },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.PIKE },
            500, 18, 10
        );

        public static readonly Weapon Rapier = new Weapon(
            "Rapier", "1d8", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.FINESSE },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.RAPIER },
            2500, 2
        );

        public static readonly Weapon Scimitar = new Weapon(
            "Scimitar", "1d6", DamageType.SLASHING,
            new WeaponProperty[] { WeaponProperty.FINESSE, WeaponProperty.LIGHT },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.SCIMITAR },
            2500, 3
        );

        public static readonly Weapon Shortsword = new Weapon(
            "Shortsword", "1d6", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.FINESSE, WeaponProperty.LIGHT },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.SHORTSWORD },
            1000, 2
        );

        public static readonly Weapon Trident = new Weapon(
            "Trident", "1d6", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.VERSATILE, WeaponProperty.THROWN },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.TRIDENT },
            500, 4, 5, 20, 60
        );

        public static readonly Weapon WarPick = new Weapon(
            "War pick", "1d8", DamageType.PIERCING,
            new WeaponProperty[] { },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.WAR_PICK },
            500, 2
        );

        public static readonly Weapon Warhammer = new Weapon(
            "Warhammer", "1d8", DamageType.BLUDGEONING,
            new WeaponProperty[] { WeaponProperty.VERSATILE },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.WARHAMMER },
            1500, 2
        );

        public static readonly Weapon Whip = new Weapon(
            "Whip", "1d4", DamageType.SLASHING,
            new WeaponProperty[] { WeaponProperty.FINESSE, WeaponProperty.REACH },
            new Proficiency[] { Proficiency.MARTIAL_MELEE_WEAPONS, Proficiency.WHIP },
            200, 3, 10
        );

        // Martial Ranged Weapons

        public static readonly Weapon Blowgun = new Weapon(
            "Blowgun", "1d1", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.LOADING, WeaponProperty.AMMUNITION },
            new Proficiency[] { Proficiency.MARTIAL_RANGED_WEAPONS, Proficiency.BLOWGUN },
            1000, 1, 0, 25, 100
        );

        public static readonly Weapon HandCrossbow = new Weapon(
            "Hand Crossbow", "1d6", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.LOADING, WeaponProperty.LIGHT, WeaponProperty.AMMUNITION },
            new Proficiency[] { Proficiency.MARTIAL_RANGED_WEAPONS, Proficiency.CROSSBOW_HAND },
            7500, 3, 0, 30, 120
        );

        public static readonly Weapon HeavyCrossbow = new Weapon(
            "Heavy Crossbow", "1d10", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY, WeaponProperty.AMMUNITION, WeaponProperty.LOADING },
            new Proficiency[] { Proficiency.MARTIAL_RANGED_WEAPONS, Proficiency.CROSSBOW_HEAVY },
            5000, 18, 0, 100, 400
        );

        public static readonly Weapon Longbow = new Weapon(
            "Longbow", "1d8", DamageType.PIERCING,
            new WeaponProperty[] { WeaponProperty.TWO_HANDED, WeaponProperty.HEAVY, WeaponProperty.AMMUNITION },
            new Proficiency[] { Proficiency.MARTIAL_RANGED_WEAPONS, Proficiency.LONGBOW },
            5000, 2, 0, 150, 600
        );

        public static readonly Weapon Net = new Weapon(
            "Net", "1d1-1", DamageType.BLUDGEONING,
            new WeaponProperty[] { WeaponProperty.THROWN, WeaponProperty.SPECIAL_NET },
            new Proficiency[] { Proficiency.MARTIAL_RANGED_WEAPONS, Proficiency.NET },
            100, 3, 0, 5, 15
        );
    }

    public struct Shields {
        public static readonly Shield Buckler = new Shield("Buckler", 1, 100, 1);
    }

    public struct Armors {
        public static readonly Armor ChainShirt = new Armor("Chain shirt", 13, 2, 0, Proficiency.MEDIUM_ARMOR, 50000, 20);
        public static readonly Armor Plate = new Armor("Plate armor", 18, 0, 15, Proficiency.HEAVY_ARMOR, 1500000, 65);
    }

    public struct Rings {
        public static readonly Ring RingOfSwimming = new Ring("Ring of Swimming", new Effect[] { Effect.SWIMMING_SPEED_40 });
        public static readonly Ring RingOfProtection = new Ring("Ring of Protection", new Effect[] { Effect.PROTECTION });
    }

    public struct Helmets {
        public static readonly Helmet HeadbandOfIntellect = new Helmet("Headband of Intellect", new Effect[] { Effect.INTELLIGENCE_19 });
    }

    public struct Amulets {
        public static readonly Amulet AmuletOfHealth = new Amulet("Amulet of Health", new Effect[] { Effect.CONSTITUION_19 });
    }

    public struct Bootss { // additional s because double plural ^_^
        public static readonly Boots BootsOfTheWinterland = new Boots("Boots of the Winterland", new Effect[] { Effect.RESISTANCE_COLD, Effect.IGNORE_SNOW_PENALITY });
    }
}