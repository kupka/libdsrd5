using System;

namespace srd5 {
    public enum ConditionType {
        BLINDED, // Disadvantage on attacks, Advantage on being attacked
        CHARMED, // Cannot attack source of charm
        DEAFENED, // Fail hearing checks
        EXHAUSTED_1, // Disadvantage on all ability checks
        EXHAUSTED_2, // Speed halved
        EXHAUSTED_3, // Disadvantage on attack and save
        EXHAUSTED_4, // Max HPs halved
        EXHAUSTED_5, // Speed = 0
        EXHAUSTED_6, // Death
        FRIGHTENED, // Disadvantage on ability and attack when source is close, cannot approach source
        GRAPPLED_DC10, // Speed = 0  
        GRAPPLED_DC11, // Speed = 0        
        GRAPPLED_DC12, // Speed = 0
        GRAPPLED_DC13, // Speed = 0
        GRAPPLED_DC14, // Speed = 0
        GRAPPLED_DC15, // Speed = 0
        GRAPPLED_DC16, // Speed = 0
        GRAPPLED_DC17, // Speed = 0
        GRAPPLED_DC18, // Speed = 0
        GRAPPLED_DC19, // Speed = 0
        GRAPPLED_DC20, // Speed = 0
        INCAPACITATED, // Cannot take actions and reactions
        INVISIBLE, // Advantage on attacks, Disadvantage on being attacked
        PARALYZED, // Stunned & takes auto crits
        PETRIFIED, // Stunned & resistance against all damage, immune against poison and disease
        POISONED,  // Disadvantage on ability and attack
        PRONE, // Disadvantage on attack, Advantage on being melee attacked, Disadvantage on being ranged attacked
        RESTRAINED, // Speed = 0, Disadvantage on attacks, Advantage on being attacked, Disadvantage on DEX saves
        STUNNED, // Incapacitated & fail STR/DEX checks, Advantage on being attacked
        UNCONSCIOUS, // Paralyzed & Fully Disarmed
    }

    public static class ConditionsExtension {
        public static void Apply(this ConditionType type, Combattant combattant) {
            switch (type) {
                case ConditionType.BLINDED:
                    applyBlinded(combattant);
                    break;
                case ConditionType.INCAPACITATED:
                    applyIncapacitated(combattant);
                    break;
                case ConditionType.PARALYZED:
                    applyParalyzed(combattant);
                    break;
                case ConditionType.STUNNED:
                    applyStunned(combattant);
                    break;
                case ConditionType.UNCONSCIOUS:
                    applyUnconcious(combattant);
                    break;
            }
        }

        public static void Unapply(this ConditionType type, Combattant combattant) {
            switch (type) {
                case ConditionType.BLINDED:
                    unapplyBlinded(combattant);
                    break;
                case ConditionType.INCAPACITATED:
                    unapplyIncapacitated(combattant);
                    break;
                case ConditionType.PARALYZED:
                    unapplyParalyzed(combattant);
                    break;
                case ConditionType.STUNNED:
                    unapplyStunned(combattant);
                    break;
                case ConditionType.UNCONSCIOUS:
                    unapplyUnconcious(combattant);
                    break;
            }
        }

        private static void applyBlinded(Combattant combattant) {
            combattant.AddEffect(Effect.ADVANTAGE_ON_BEING_ATTACKED);
            combattant.AddEffect(Effect.DISADVANTAGE_ON_ATTACK);
        }

        private static void unapplyBlinded(Combattant combattant) {
            combattant.RemoveEffect(Effect.ADVANTAGE_ON_BEING_ATTACKED);
            combattant.RemoveEffect(Effect.DISADVANTAGE_ON_ATTACK);
        }

        private static void applyIncapacitated(Combattant combattant) {
            combattant.AddEffect(Effect.CANNOT_TAKE_ACTIONS);
        }

        private static void unapplyIncapacitated(Combattant combattant) {
            combattant.RemoveEffect(Effect.CANNOT_TAKE_ACTIONS);
        }


        private static void applyParalyzed(Combattant combattant) {
            applyStunned(combattant);
            combattant.AddEffect(Effect.AUTOMATIC_CRIT_ON_BEING_HIT_WITHIN_5_FT);
        }

        private static void unapplyParalyzed(Combattant combattant) {
            unapplyStunned(combattant);
            combattant.RemoveEffect(Effect.AUTOMATIC_CRIT_ON_BEING_HIT_WITHIN_5_FT);
        }

        private static void applyStunned(Combattant combattant) {
            applyIncapacitated(combattant);
            combattant.AddEffect(Effect.FAIL_STRENGTH_CHECK);
            combattant.AddEffect(Effect.FAIL_DEXERITY_CHECK);
            combattant.AddEffect(Effect.ADVANTAGE_ON_BEING_ATTACKED);
        }

        private static void unapplyStunned(Combattant combattant) {
            unapplyIncapacitated(combattant);
            combattant.RemoveEffect(Effect.FAIL_STRENGTH_CHECK);
            combattant.RemoveEffect(Effect.FAIL_DEXERITY_CHECK);
            combattant.RemoveEffect(Effect.ADVANTAGE_ON_BEING_ATTACKED);

        }

        private static void applyUnconcious(Combattant combattant) {
            applyParalyzed(combattant);
            if (combattant is CharacterSheet sheet) {
                sheet.Unequip(sheet.Inventory.MainHand);
                sheet.Unequip(sheet.Inventory.OffHand);
            }
        }

        private static void unapplyUnconcious(Combattant combattant) {
            unapplyParalyzed(combattant);
        }

    }
}