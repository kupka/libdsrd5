using Xunit;

namespace srd5 {
    public partial class SpellTest {
        [Fact]
        public void ViciousMockeryTest() {
            CharacterSheet bard = new CharacterSheet(Race.GNOME, true);
            bard.AddLevels(CharacterClasses.Bard, CharacterClasses.Bard);
            Monster goblin = Monsters.Goblin;
            Monster goblin2 = Monsters.Goblin;
            BattleGroundClassic ground = new BattleGroundClassic();
            ground.AddCombattant(bard, ClassicLocation.Row.FRONT_LEFT);
            ground.AddCombattant(goblin, ClassicLocation.Row.FRONT_RIGHT);
            ground.AddCombattant(goblin2, ClassicLocation.Row.FRONT_RIGHT);
            Spells.ViciousMockery.Cast(ground, bard, 30, SpellLevel.CANTRIP, 2, goblin);
            Assert.True(goblin.HitPointsMax > goblin.HitPoints);
            Assert.True(goblin.HasEffect(Effect.DISADVANTAGE_ON_ATTACK));
            goblin.OnEndOfTurn();
            Assert.False(goblin.HasEffect(Effect.DISADVANTAGE_ON_ATTACK));
            Random.State = 1;
            Spells.ViciousMockery.Cast(ground, bard, 1, SpellLevel.CANTRIP, 2, goblin2);
            Assert.True(goblin2.HitPointsMax == goblin2.HitPoints);
            Assert.False(goblin2.HasEffect(Effect.DISADVANTAGE_ON_ATTACK));
        }
    }
}