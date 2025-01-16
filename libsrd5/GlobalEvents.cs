using System;

namespace srd5 {
    public static class GlobalEvents {
        public enum EventTypes {
            ACTION_FAILED,
            INITIATIVE,
            ATTACKED,
            DAMAGED,
            HEALED,
            CONDITION,
            DC,
            SPELL,
            SPELL_WITHOUT_EFFECT,
            EQUIPMENT,
            EFFECT_ACTIVATED
        }
        public static event EventHandler<EventArgs> Handlers;

        public class InitiativeRolled : EventArgs {
            public Combattant Roller { get; private set; }
            public int Result { get; private set; }

            public InitiativeRolled(Combattant roller, int result) {
                Roller = roller;
                Result = result;
            }
        }

        internal static void RolledInitiative(Combattant combattant, int result) {
            if (Handlers == null) return;
            Handlers(EventTypes.INITIATIVE, new InitiativeRolled(combattant, result));
        }

        public class AttackRolled : EventArgs {
            public Combattant Attacker { get; private set; }
            public Attack Attack { get; private set; }
            public Combattant Target { get; private set; }
            public int Roll { get; private set; }
            public bool Hit { get; private set; }
            public bool CriticalHit { get; private set; }

            public AttackRolled(Combattant attacker, Attack attack, Combattant target, int roll, bool hit, bool criticalHit) {
                Attacker = attacker;
                Attack = attack;
                Target = target;
                Roll = roll;
                Hit = hit;
                CriticalHit = criticalHit;
            }
        }

        internal static void RolledAttack(Combattant attacker, Attack attack, Combattant target, int roll, bool hit, bool criticalHit = false) {
            if (Handlers == null) return;
            Handlers(EventTypes.ATTACKED, new AttackRolled(attacker, attack, target, roll, hit, criticalHit));
        }

        public class DamageReceived : EventArgs {
            public Combattant Victim { get; private set; }
            public int Amount { get; private set; }
            public DamageType DamageType { get; private set; }

            public DamageReceived(Combattant victim, int amount, DamageType damageType) {
                Victim = victim;
                Amount = amount;
                DamageType = damageType;
            }
        }

        internal static void ReceivedDamage(Combattant victim, int amount, DamageType damageType) {
            if (Handlers == null) return;
            Handlers(EventTypes.DAMAGED, new DamageReceived(victim, amount, damageType));
        }

        public class HealingReceived : EventArgs {
            public Combattant Beneficiary { get; private set; }
            public int Amount { get; private set; }
            public HealingReceived(Combattant beneficiary, int amount) {
                Beneficiary = beneficiary;
                Amount = amount;
            }
        }

        internal static void ReceivedHealing(Combattant beneficiary, int amount) {
            if (Handlers == null) return;
            Handlers(EventTypes.HEALED, new HealingReceived(beneficiary, amount));
        }

        public class DCRolled : EventArgs {
            public Combattant Roller { get; private set; }
            public object Source { get; private set; }
            public Ability Ability { get; private set; }
            public int DC { get; private set; }
            public int Roll { get; private set; }
            public bool Success { get; private set; }

            public DCRolled(Combattant roller, object source, Ability ability, int dc, int roll, bool success) {
                Roller = roller;
                Source = source;
                Ability = ability;
                DC = dc;
                Roll = roll;
                Success = success;
            }
        }

        internal static void RolledDC(Combattant roller, object source, Ability ability, int dc, int roll, bool success) {
            if (Handlers == null) return;
            Handlers(EventTypes.DC, new DCRolled(roller, source, ability, dc, roll, success));
        }

        public class ConditionChanged : EventArgs {
            public Combattant Bearer { get; private set; }
            public ConditionType Condition { get; private set; }
            public bool Removed { get; private set; }

            public ConditionChanged(Combattant bearer, ConditionType condition, bool removed = false) {
                Bearer = bearer;
                Condition = condition;
                Removed = removed;
            }
        }

        internal static void ChangedCondition(Combattant bearer, ConditionType condition, bool removed = false) {
            if (Handlers == null) return;
            Handlers(EventTypes.CONDITION, new ConditionChanged(bearer, condition, removed));
        }

        public class SpellAffection : EventArgs {
            public Combattant Caster { get; private set; }
            public Spells.ID Spell { get; private set; }
            public Combattant Target { get; private set; }
            public bool Affected { get; private set; }

            public SpellAffection(Combattant caster, Spells.ID spell, Combattant target, bool affected) {
                Caster = caster;
                Spell = spell;
                Target = target;
                Affected = affected;
            }
        }

        internal static void AffectBySpell(Combattant caster, Spells.ID spell, Combattant target, bool affected) {
            if (Handlers == null) return;
            Handlers(EventTypes.SPELL, new SpellAffection(caster, spell, target, affected));
        }

        public class ActionFailed : EventArgs {
            public enum Reasons {
                CANNOT_TAKE_ACTIONS,
                WRONG_PHASE,
                TARGET_OUT_OF_RANGE,
                WRONG_NUMBER_OF_TARGETS,
                SPELL_NOT_KNOWN,
                SPELL_NOT_PREPARED,
                SPELLSLOT_EMPTY,
                SPELLSLOT_INVALID,
                INSUFFICIENT_USES

            }
            public Combattant Initiator { get; private set; }
            public Reasons Reason { get; private set; }

            public ActionFailed(Combattant initiator, Reasons reason) {
                Initiator = initiator;
                Reason = reason;
            }
        }

        internal static void FailAction(Combattant initiator, ActionFailed.Reasons reason) {
            if (Handlers == null) return;
            Handlers(EventTypes.ACTION_FAILED, new ActionFailed(initiator, reason));
        }

        public class EquipmentChanged : EventArgs {
            public enum Events {
                EQUIPPED,
                UNEQUIPPED,
                DESTROYED,
                USED,
                PUT_IN_BAG,
                REMOVED_FROM_BAG
            }

            public CharacterSheet Hero { get; private set; }
            public Item Item { get; private set; }
            public Events Event { get; private set; }

            public EquipmentChanged(CharacterSheet hero, Item item, Events evnt) {
                Hero = hero;
                Item = item;
                Event = evnt;
            }
        }

        public static void ChangeEquipment(CharacterSheet hero, Item item, EquipmentChanged.Events evnt) {
            if (Handlers == null) return;
            Handlers(EventTypes.EQUIPMENT, new EquipmentChanged(hero, item, evnt));
        }

        public class EffectActivated : EventArgs {
            public Combattant Source { get; private set; }
            public Effect Effect { get; private set; }

            public EffectActivated(Combattant source, Effect effect) {
                Source = source;
                Effect = effect;
            }
        }

        public static void ActivateEffect(Combattant source, Effect effect) {
            if (Handlers == null) return;
            Handlers(EventTypes.EFFECT_ACTIVATED, new EffectActivated(source, effect));
        }

        public class FeatActivated : EventArgs {
            public Combattant Source { get; private set; }
            public Feat Feat { get; private set; }

            public FeatActivated(Combattant source, Feat feat) {
                Source = source;
                Feat = feat;
            }
        }

        public static void ActivateFeat(Combattant source, Feat feat) {
            if (Handlers == null) return;
            Handlers(EventTypes.EFFECT_ACTIVATED, new FeatActivated(source, feat));
        }

        public class SpellWithoutEffect : EventArgs {
            public Combattant Caster { get; private set; }
            public Spells.ID Spell { get; private set; }

            public SpellWithoutEffect(Combattant caster, Spells.ID spell) {
                Caster = caster;
                Spell = spell;
            }
        }

        public static void EffectlessSpell(Combattant caster, Spells.ID spell) {
            if (Handlers == null) return;
            Handlers(EventTypes.SPELL_WITHOUT_EFFECT, new SpellWithoutEffect(caster, spell));
        }
    }
}