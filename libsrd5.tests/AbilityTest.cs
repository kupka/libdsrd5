using System;
using Xunit;

namespace srd5 {
    public class AbilityTest {
        [Theory]
        [InlineData(-5, -5)]
        [InlineData(3, -4)]
        [InlineData(8, -1)]
        [InlineData(15, 2)]
        [InlineData(26, 8)]
        [InlineData(39, 10)]
        public void TestModifier(int value, int expected) {
            Ability ability = new(AbilityType.STRENGTH, value);
            Assert.Equal(AbilityType.STRENGTH.Name(), ability.Name);
            ability.BaseValue = value;
            Assert.Equal(expected, ability.Modifier);
        }
    }
}