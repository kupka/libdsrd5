using System;

namespace srd5 {
    public struct Die {
        public static Die Get(int max) {
            switch (max) {
                case 0: // constant 0, not an actual die
                case 1: // constant 1, not an actual die
                case 2:
                case 3:
                case 4:
                case 6:
                case 8:
                case 10:
                case 12:
                case 20:
                case 100:
                    Die die = new Die();
                    die.MaxValue = max;
                    die.Value = Random.Get(Math.Min(max, 1), max);
                    Dice.onDiceRolled(die);
                    return die;
                default:
                    throw new Srd5ArgumentException("No such die d" + max);
            }
        }

        public static Die D2 {
            get {
                return Get(2);
            }
        }

        public static Die D3 {
            get {
                return Get(3);
            }
        }

        public static Die D4 {
            get {
                return Get(4);
            }
        }

        public static Die D6 {
            get {
                return Get(6);
            }
        }

        public static Die D8 {
            get {
                return Get(8);
            }
        }

        public static Die D10 {
            get {
                return Get(10);
            }
        }

        public static Die D12 {
            get {
                return Get(12);
            }
        }

        public static Die D20 {
            get {
                return Get(20);
            }
        }

        public static Die D20Advantage {
            get {
                Die first = Get(20);
                Die second = Get(20);
                if (first.Value >= second.Value)
                    return first;
                else
                    return second;
            }
        }

        public static Die D20Disadvantage {
            get {
                Die first = Get(20);
                Die second = Get(20);
                if (first.Value <= second.Value)
                    return first;
                else
                    return second;
            }
        }

        public static Die D100 {
            get {
                return Get(100);
            }
        }

        public int Value {
            get;
            internal set;
        }

        public int MaxValue {
            get;
            private set;
        }

        public static int Roll(string diceString) {
            Dice parsed = new Dice(diceString);
            return parsed.Roll();
        }

        public override string ToString() {
            return "d" + MaxValue;
        }
    }

    public abstract class DiceRolledEventSource {

    }

    public class DiceRolledEvent : EventArgs {
        internal static DiceRolledEventSource source { get; set; }
        public Dice Dice { get; private set; }
        public int Value { get; private set; }
        public DiceRolledEventSource Source { get; private set; }

        public DiceRolledEvent(Dice dice, int value) {
            Dice = dice;
            Value = value;
            Source = source;
            source = null;
        }
    }

    public struct Dice {
        internal int Modifier {
            get;
            private set;
        }

        internal int Amount {
            get;
            private set;
        }

        internal int Die {
            get;
            private set;
        }

        public int Min {
            get {
                return Math.Max(0, Amount + Modifier);
            }
        }

        public int Max {
            get {
                return Math.Max(0, Amount * Die + Modifier);
            }
        }

        private string diceString;
        public override string ToString() {
            return diceString;
        }

        internal Dice(int amount, int die, int modifier) {
            Amount = amount;
            Die = die;
            Modifier = modifier;
            string modifierString = "";
            if (modifier > 0)
                modifierString = "+" + modifier;
            else if (modifier < 0)
                modifierString += modifier;
            diceString = amount + "d" + die + modifierString;
        }

        internal Dice(string diceString) {
            this.diceString = diceString;
            Amount = 1;
            Die = 0;
            Modifier = 0;
            bool negativeModifier = false;
            int state = 0; // 0 = Start, 1 = Dice, 2 = Modifier
            for (int i = 0; i < diceString.Length; i++) {
                char c = diceString[i];
                switch (state) {
                    case 0:
                        if (char.IsDigit(c)) {
                            if (i == 0) {
                                Amount = 0;
                            } else {
                                Amount *= 10;
                            }
                            Amount += int.Parse(c.ToString());
                            break;
                        } else if (c == 'd') {
                            state = 1;
                            continue;
                        } else {
                            throw new Srd5FormatException("Invalid input: " + diceString);
                        }
                    case 1:
                        if (char.IsDigit(c)) {
                            if (Die > 0) {
                                Die *= 10;
                            }
                            Die += int.Parse(c.ToString());
                            break;
                        } else if (c == '+') {
                            state = 2;
                            continue;
                        } else if (c == '-') {
                            negativeModifier = true;
                            state = 2;
                            continue;
                        } else {
                            throw new Srd5FormatException("Invalid input: " + diceString);
                        }
                    case 2:
                        if (char.IsDigit(c)) {
                            if (Modifier != 0) {
                                Modifier *= 10;
                            }
                            Modifier += int.Parse(c.ToString());
                        } else {
                            throw new Srd5FormatException("Invalid input: " + diceString);
                        }
                        break;
                }
            }
            if (negativeModifier) {
                Modifier *= -1;
            }
            switch (Die) {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 6:
                case 8:
                case 10:
                case 12:
                case 20:
                case 100:
                    break;
                default:
                    throw new Srd5FormatException("Invalid input: " + diceString);
            }
        }

        public int Roll() {
            int result = Modifier;
            for (int i = 0; i < Amount; i++) {
                result += Random.Get(Math.Min(Die, 1), Die);
            }
            int value = Math.Max(0, result);
            onDiceRolled(this, value);
            return value;
        }

        // Crticial hits roll each dice twice or more
        public int RollCritical(int times = 2) {
            int result = Modifier;
            for (int i = 0; i < Amount; i++) {
                for (int j = 0; j < times; j++) {
                    result += Random.Get(1, Die);
                }
            }
            int value = Math.Max(0, result);
            onDiceRolled(this, value);
            return value;
        }

        // Event Handling
        public static event EventHandler<DiceRolledEvent> DiceRolled;

        internal static void onDiceRolled(Dice dice, int value) {
            if (DiceRolled == null) return;
            DiceRolledEvent diceRolledEvent = new DiceRolledEvent(dice, value);
            DiceRolled(dice, diceRolledEvent);
        }

        internal static void onDiceRolled(Die die) {
            if (DiceRolled == null) return;
            Dice dice = new Dice(die.ToString());
            DiceRolledEvent diceRolledEvent = new DiceRolledEvent(dice, die.Value);
            DiceRolled(dice, diceRolledEvent);
        }
    }
}