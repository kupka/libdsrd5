using Xunit;
using System;
using System.Reflection;

namespace srd5 {
    [CollectionDefinition("SingleThreaded", DisableParallelization = true)]
    [Collection("SingleThreaded")]
    public class GlobalEventsTest {
        private int initiative = 0, attacked = 0, healed = 0, damaged = 0, unkown = 0;
        private void eventListener(object sender, EventArgs args) {
            if (GlobalEvents.EventTypes.INITIATIVE.Equals(sender)) {
                initiative++;
            } else if (GlobalEvents.EventTypes.ATTACKED.Equals(sender)) {
                attacked++;
            } else if (GlobalEvents.EventTypes.HEALED.Equals(sender)) {
                healed++;
            } else if (GlobalEvents.EventTypes.DAMAGED.Equals(sender)) {
                damaged++;
            } else {
                unkown++;
            }
        }

        [Fact]
        public void AllEventsTest() {
            GlobalEvents.Handlers += eventListener;
            BattlegroundTest groundTest = new BattlegroundTest();
            foreach (MethodInfo method in typeof(BattlegroundTest).GetMethods()) {
                if (method.Name.IndexOf("Test") >= 0) method.Invoke(groundTest, null);
            }
            SpellTest spellTest = new SpellTest();
            foreach (MethodInfo method in typeof(SpellTest).GetMethods()) {
                if (method.Name.IndexOf("Test") >= 0) method.Invoke(spellTest, null);
            }
            Assert.False(initiative == 0);
            Assert.False(attacked == 0);
            Assert.False(healed == 0);
            Assert.False(damaged == 0);
            Assert.True(unkown == 0);
        }
    }
}