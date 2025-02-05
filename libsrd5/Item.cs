using System;

namespace srd5 {
    public delegate void ConsumableEffect(Combattant consumer, Consumable item);
    public class Consumable : Item {
        internal ConsumableEffect ConsumableEffect;
        public int Charges { get; internal set; }

        public Consumable(string name, ItemType type, ConsumableEffect effect, ItemRarity rarity, int charges = 1) {
            Name = name;
            Type = type;
            ConsumableEffect = effect;
            Rarity = rarity;
            Charges = charges;
        }
    }

    public delegate void UsableEffect(Battleground ground, Combattant user, Usable item, int expendedCharges, params Combattant[] targets);

    public class Usable : Item {
        internal UsableEffect UsableEffect;
        public int Charges { get; internal set; }
        public int MaximumChargePerUse { get; internal set; }
        public int MaximumTargets { get; internal set; }

        public Usable(string name, ItemType type, UsableEffect effect, ItemRarity rarity, int maxTargets, int maxChargePerUse, int charges) {
            Name = name;
            Type = type;
            UsableEffect = effect;
            Rarity = rarity;
            MaximumTargets = maxTargets;
            Charges = charges;
            MaximumChargePerUse = maxChargePerUse;
        }
    }

    public class Shield : Item {
        public int AC { get; private set; }

        public Shield(string name, int ac, int value, int weight) {
            Name = name;
            AC = ac;
            Proficiencies = new Proficiency[] { Proficiency.SHIELDS };
            Value = value;
            Weight = weight;
            Type = ItemType.SHIELD;
        }
    }

    public class Armor : Item {
        public int AC { get; internal set; }
        public int MaxDexBonus { get; private set; }
        public int Strength { get; private set; }
        public ArmorProperty[] Properties { get; private set; }

        public Armor(string name, int ac, int maxDexBonus, int strength, Proficiency proficiency, ArmorProperty[] properties, int value, float weight) {
            Name = name;
            AC = ac;
            MaxDexBonus = maxDexBonus;
            Strength = strength;
            Proficiencies = new Proficiency[] { proficiency };
            Properties = properties;
            Value = value;
            Weight = weight;
            Type = ItemType.ARMOR;
        }

        public bool HasProperty(ArmorProperty property) {
            return Array.IndexOf(Properties, property) > -1;
        }
    }

    public class Helmet : MagicItem {
        public Helmet(string name, Effect[] effects) : base(name, effects) {
            Type = ItemType.HELMET;
        }
    }

    public class Amulet : MagicItem {
        public Amulet(string name, Effect[] effects) : base(name, effects) {
            Type = ItemType.AMULET;
        }
    }

    public class Ring : MagicItem {
        public Ring(string name, Effect[] effects) : base(name, effects) {
            Type = ItemType.RING;
        }
    }

    public class Boots : MagicItem {
        public Boots(string name, Effect[] effects) : base(name, effects) {
            Type = ItemType.BOOTS;
        }
    }

    public class MagicItem : Item {
        public Effect[] Effects { get; internal set; }

        public MagicItem(string name, Effect[] effects) {
            Name = name;
            Effects = effects;
        }
    }

    public class Damage {
        public DamageType Type { get; internal set; }
        public Dice Dice { get; internal set; }

        public Damage(DamageType type, string diceString) {
            Type = type;
            Dice = new Dice(diceString);
        }

        public Damage(DamageType type, int value) {
            Type = type;
            Dice = new Dice(value, 1, 0);
        }
    }


    public class Weapon : Item {
        internal Weapon(string name, string damage, DamageType damageType, WeaponProperty[] properties, Proficiency[] proficiencies,
                        int value, float weight, int reach = 5, int rangeNormal = 0, int rangeLong = 0) {
            Name = name;
            Type = ItemType.WEAPON;
            Damage = new Damage(damageType, damage);
            Proficiencies = proficiencies;
            Properties = properties;
            Value = value;
            Weight = weight;
            RangeNormal = rangeNormal;
            RangeLong = rangeLong;
            Reach = reach;
        }

        public Damage Damage { get; private set; }

        public WeaponProperty[] Properties { get; private set; }

        public int RangeNormal { get; private set; }

        public int RangeLong { get; private set; }

        public int Reach { get; private set; }

        public bool HasProperty(WeaponProperty property) {
            return Array.IndexOf(Properties, property) > -1;
        }
    }

    public abstract class Item {
        private readonly int id = Random.Get(1, int.MaxValue);
        public string Name { get; protected set; }
        public ItemType Type { get; protected set; }

        public Proficiency[] Proficiencies { get; protected set; }

        public float Weight { get; protected set; }

        public int Value { get; internal set; }

        public bool Destroyed { get; internal set; } = false;

        public ItemRarity Rarity { get; internal set; } = ItemRarity.COMMON;

        public override bool Equals(object other) {
            if (other == null) return false;
            if (!(other is Item)) return false;
            return id == ((Item)other).id;
        }

        public bool IsThisA(Item other) {
            if (other == null) return false;
            return Name == other.Name;
        }

        public override int GetHashCode() {
            return id;
        }

        public override string ToString() {
            return Name + "#" + id;
        }

        public void Destroy() {
            Destroyed = true;
        }
    }
}