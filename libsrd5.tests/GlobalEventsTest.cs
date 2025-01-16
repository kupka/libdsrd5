using Xunit;
using System;
using System.Reflection;

namespace srd5 {
    [CollectionDefinition("SingleThreaded", DisableParallelization = true)]
    [Collection("SingleThreaded")]
    public class GlobalEventsTest {
        private int initiative = 0, attacked = 0, healed = 0, damaged = 0, dc = 0, conditions = 0, spells = 0,
                    failedAction = 0, equipment = 0, effects = 0, spellsWithoutEffect = 0, unkown = 0;
        private void eventListener(object sender, EventArgs args) {
            if (GlobalEvents.EventTypes.INITIATIVE.Equals(sender)) {
                initiative++;
            } else if (GlobalEvents.EventTypes.ATTACKED.Equals(sender)) {
                attacked++;
            } else if (GlobalEvents.EventTypes.HEALED.Equals(sender)) {
                healed++;
            } else if (GlobalEvents.EventTypes.DAMAGED.Equals(sender)) {
                damaged++;
            } else if (GlobalEvents.EventTypes.DC.Equals(sender)) {
                dc++;
            } else if (GlobalEvents.EventTypes.CONDITION.Equals(sender)) {
                conditions++;
            } else if (GlobalEvents.EventTypes.SPELL.Equals(sender)) {
                spells++;
            } else if (GlobalEvents.EventTypes.ACTION_FAILED.Equals(sender)) {
                failedAction++;
            } else if (GlobalEvents.EventTypes.EQUIPMENT.Equals(sender)) {
                equipment++;
            } else if (GlobalEvents.EventTypes.EFFECT_ACTIVATED.Equals(sender)) {
                effects++;
            } else if (GlobalEvents.EventTypes.SPELL_WITHOUT_EFFECT.Equals(sender)) {
                spellsWithoutEffect++;
            } else {
                unkown++;
            }
        }

        [Fact]
        public void AllEventsTest() {
            GlobalEvents.Handlers += eventListener;
            BattlegroundTest groundTest = new BattlegroundTest();
            foreach (MethodInfo method in typeof(BattlegroundTest).GetMethods()) {
                if (method.Name.IndexOf("Test") > -1) method.Invoke(groundTest, null);
            }
            SpellTest spellTest = new SpellTest();
            foreach (MethodInfo method in typeof(SpellTest).GetMethods()) {
                if (method.Name.IndexOf("Test") > -1) method.Invoke(spellTest, null);
            }
            Assert.False(initiative == 0);
            Assert.False(attacked == 0);
            Assert.False(healed == 0);
            Assert.False(damaged == 0);
            Assert.False(dc == 0);
            Assert.False(conditions == 0);
            Assert.False(spells == 0);
            Assert.False(failedAction == 0);
            Assert.False(equipment == 0);
            Assert.False(effects == 0);
            Assert.False(spellsWithoutEffect == 0);
            Assert.True(unkown == 0);
        }
    }
}