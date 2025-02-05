using System;
using Xunit;

namespace srd5 {
    public class EffectsTest {
        [Fact]
        public void ResistanceTest() {
            Assert.Equal(Effect.RESISTANCE_FORCE, Effects.Resistance(DamageType.FORCE));
        }
        [Fact]
        public void ImmunityTest() {
            Assert.Equal(Effect.IMMUNITY_PSYCHIC, Effects.Immunity(DamageType.PSYCHIC));
        }
        [Fact]
        public void VulnerabilityTest() {
            Assert.Equal(Effect.VULNERABILITY_SLASHING, Effects.Vulnerability(DamageType.SLASHING));
        }

        [Fact]
        public void TrueDamageTest() {
            Assert.Throws<Srd5ArgumentException>(delegate () {
                Monsters.Aboleth.AddEffect(Effect.IMMUNITY_TRUE_DAMAGE);
            });
            Assert.Throws<Srd5ArgumentException>(delegate () {
                Monsters.Aboleth.AddEffect(Effect.RESISTANCE_TRUE_DAMAGE);
            });
            Assert.Throws<Srd5ArgumentException>(delegate () {
                Monsters.Aboleth.AddEffect(Effect.VULNERABILITY_TRUE_DAMAGE);
            });
        }

        [Fact]
        public void CurseTest() {
            Assert.True(Effect.CURSE_MUMMY_ROT.IsCurse());
            Assert.False(Effect.BEARDED_DEVIL_POISON.IsCurse());
        }

        [Fact]
        public void EnumerateEffectsTest() {
            foreach (Effect effect in Enum.GetValues(typeof(Effect))) {
                if (effect == Effect.IMMUNITY_TRUE_DAMAGE || effect == Effect.RESISTANCE_TRUE_DAMAGE || effect == Effect.VULNERABILITY_TRUE_DAMAGE) continue;
                Combattant bandit = Monsters.Bandit;
                bandit.AddEffect(effect);
                Assert.True(bandit.HasEffect(effect));
                bandit.OnStartOfTurn();
                bandit.OnEndOfTurn();
                bandit.OnDamageTaken(effect, new Damage(DamageType.TRUE_DAMAGE, 1));
                bandit.RemoveEffect(effect);
                Assert.False(bandit.HasEffect(effect));
                bandit.OnStartOfTurn();
                bandit.OnEndOfTurn();
                bandit.OnDamageTaken(effect, new Damage(DamageType.TRUE_DAMAGE, 1));
            }
        }

        [Fact]
        public void QuasitPoisonEffectTest() {
            Combattant bandit = Monsters.Bandit;
            while (!bandit.HasEffect(Effect.QUASIT_POISON)) {
                bandit.AddEffect(Effect.QUASIT_POISON);
                bandit.OnEndOfTurn();
            }
            Assert.True(bandit.HasCondition(ConditionType.POISONED));
            bandit.RemoveEffect(Effect.QUASIT_POISON);
            bandit.OnEndOfTurn();
            Assert.False(bandit.HasCondition(ConditionType.POISONED));
        }

        [Fact]
        public void LichParalyzingTouchEffectTest() {
            Combattant bandit = Monsters.Bandit;
            while (!bandit.HasEffect(Effect.LICH_PARALYZATION)) {
                bandit.AddEffect(Effect.LICH_PARALYZATION);
                bandit.OnEndOfTurn();
            }
            Assert.True(bandit.HasCondition(ConditionType.PARALYZED));
            bandit.RemoveEffect(Effect.LICH_PARALYZATION);
            bandit.OnEndOfTurn();
            Assert.False(bandit.HasCondition(ConditionType.PARALYZED));
        }
    }
}