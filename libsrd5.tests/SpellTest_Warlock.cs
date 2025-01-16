using System;
using Xunit;

namespace srd5 {
    public partial class SpellTest {
        [Fact]
        public void EldritchBlastTest() {
            Monster hag = Monsters.NightHag;
            Monster badger = Monsters.Badger;
            Battleground ground = createBattleground(hag, badger);
            int hp = badger.HitPoints;
            for (int i = 0; i < 5; i++)
                Spells.EldritchBlast.Cast(ground, hag, 12, SpellLevel.CANTRIP, hag.ProficiencyBonus, badger, badger, badger);
            Assert.True(badger.HitPoints < hp);
        }

        [Fact]
        public void PoisonSprayTest() {
            Monster hag = Monsters.NightHag;
            Monster badger = Monsters.Badger;
            Battleground ground = createBattleground(hag, badger);
            int hp = badger.HitPoints;
            for (int i = 0; i < 5; i++)
                Spells.PoisonSpray.Cast(ground, hag, 12, SpellLevel.CANTRIP, hag.ProficiencyBonus, badger, badger, badger);
            Assert.True(badger.HitPoints < hp);
        }
    }
}