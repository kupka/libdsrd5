using System;
using Xunit;

namespace srd5 {
    [CollectionDefinition("SingleThreaded", DisableParallelization = true)]
    [Collection("SingleThreaded")]
    public class DiceTest {
        [Fact]
        public void D2Test() {
            Die die = Die.D2;
            Assert.InRange<int>(die.Value, 1, 2);
        }

        [Fact]
        public void D3Test() {
            Die die = Die.D3;
            Assert.InRange<int>(die.Value, 1, 3);
        }

        [Fact]
        public void D4Test() {
            Die die = Die.D4;
            Assert.InRange<int>(die.Value, 1, 4);
        }

        [Fact]
        public void D6Test() {
            Die die = Die.D6;
            Assert.InRange<int>(die.Value, 1, 6);
        }

        [Fact]
        public void D8Test() {
            Die die = Die.D8;
            Assert.InRange<int>(die.Value, 1, 8);
        }

        [Fact]
        public void D10Test() {
            Die die = Die.D10;
            Assert.InRange<int>(die.Value, 1, 10);
        }

        [Fact]
        public void D12Test() {
            Die die = Die.D12;
            Assert.InRange<int>(die.Value, 1, 12);
        }

        [Fact]
        public void D20Test() {
            Die die = Die.D20;
            Assert.InRange<int>(die.Value, 1, 20);
        }

        [Fact]
        public void D100Test() {
            Die die = Die.D100;
            Assert.InRange<int>(die.Value, 1, 100);
        }

        [Fact]
        public void NotADiceTest() {
            Assert.Throws<Srd5ArgumentException>(delegate {
                Die.Get(21);
            });
        }

        [Fact]
        public void AnotherNotADiceTest() {
            Assert.Throws<Srd5FormatException>(delegate {
                new Dice("3d7+3");
            });
        }

        [Fact]
        public void DisAdvantageTest() {
            int advantageRolls = 0, disadvantageRolls = 0;
            for (int i = 0; i < 10; i++) {
                advantageRolls += Die.D20Advantage.Value;
                disadvantageRolls += Die.D20Disadvantage.Value;
            }
            Assert.True(advantageRolls > disadvantageRolls);
        }

        [Theory]
        [InlineData("3d6+3", 6, 21)]
        [InlineData("d20", 1, 20)]
        [InlineData("d4-5", 0, 0)]
        [InlineData("d2-1", 0, 1)]
        [InlineData("5d3+10", 15, 25)]
        [InlineData("1111d12+889", 2000, 14221)]
        public void ParseTest(string diceString, int min, int max) {
            Dice parsed = new Dice(diceString);
            Assert.Equal(diceString, parsed.ToString());
            Assert.Equal(min, parsed.Min);
            Assert.Equal(max, parsed.Max);
            int result = Die.Roll(diceString);
            Assert.InRange<long>(result, min, max);
        }

        [Theory]
        [InlineData("x6+3")]
        [InlineData("3f6+3")]
        [InlineData("3d6*3")]
        [InlineData("3d6+x")]
        [InlineData("3d6-41n")]
        public void InvalidDiceTest(string diceString) {
            Assert.Throws<Srd5FormatException>(delegate { new Dice(diceString); });
        }

        [Fact]
        public void DiceRolledEventTest() {
            int receivedValue = 0;
            Dice.DiceRolled += delegate (object sender, DiceRolledEvent e) {
                receivedValue += e.Value;
            };
            int value = Die.Roll("2d6+5");
            Assert.Equal(value, receivedValue);
            value += Die.D20.Value;
            Assert.Equal(value, receivedValue);
        }

        [Fact]
        public void DiceConstructorTest() {
            Assert.Equal("2d6+3", new Dice(2, 6, 3).ToString());
            Assert.Equal("1d12", new Dice(1, 12, 0).ToString());
            Assert.Equal("3d8-2", new Dice(3, 8, -2).ToString());
        }
    }
}