using System;

namespace srd5 {
    public static class RaceTextResource {
        public static string Name(this Race race) {
            switch (race) {
                case Race.HILL_DWARF:
                    return "Hill Dwarf";
                case Race.HIGH_ELF:
                    return "High Elf";
                default:
                    return Enum.GetName(typeof(Race), race) + ": (Name missing)";
            }
        }

        public static string Description(this Race race) {
            switch (race) {
                case Race.HILL_DWARF:
                    return "Your dwarf character has an assortment of inborn abilities, part and parcel of dwarven nature.\nAs a hill dwarf, you have keen senses, deep intuition, and remarkable resilience.";
                case Race.HIGH_ELF:
                    return "Your elf character has a variety of natural abilities, the result of thousands of years of elven refinement.\nAs a high elf, you have a keen mind and a mastery of at least the basics of magic. In many fantasy gaming worlds, there are two kinds of high elves. One type is haughty and reclusive, believing themselves to be superior to non-elves and even other elves. The other type is more common and more friendly, and often encountered among humans and other races.";
                default:
                    return Enum.GetName(typeof(Race), race) + ": (Description missing)";
            }
        }
    }

    public static class EffectTextResource {
        public static string Name(this Effect effect) {
            string name = Enum.GetName(typeof(Effect), effect);
            switch (effect) {
                case Effect.RESISTANCE_POISON:
                case Effect.RESISTANCE_COLD:
                    return "Resistance to " + name.Substring(11).ToLower() + " damage";
                case Effect.IMMUNITY_SLEEP:
                    return "Immunity to " + name.Substring(9).ToLower();
                default:
                    return name + ": (Name missing)";
            }
        }
    }

    public static class FeatTextResource {
        public static string Name(this Feat feat) {
            switch (feat) {
                case Feat.DWARVEN_ABILITY_INCREASE:
                case Feat.HILL_DWARVEN_ABILITY_INCREASE:
                case Feat.ELVEN_ABILITY_INCREASE:
                case Feat.HIGH_ELVEN_ABILITY_INCREASE:
                    return "Ability Score Increase";
                case Feat.DWARVEN_DARKVISION:
                case Feat.ELVEN_DARKVISION:
                    return "Darkvision";
                case Feat.DWARVEN_RESILIENCE:
                    return "Dwarven Resilience";
                case Feat.DWARVEN_SMITH:
                case Feat.DWARVEN_BREWER:
                case Feat.DWARVEN_MASON:
                    return "Tool Proficiency";
                case Feat.DWARVEN_COMBAT_TRAINING:
                    return "Dwarven Combat Training";
                case Feat.STONECUNNING:
                    return "Stonecunning";
                case Feat.DWARVEN_TOUGHNESS:
                    return "Dwarven Toughness";
                case Feat.KEEN_SENSES:
                    return "Keen Senses";
                case Feat.FEY_ANCESTRY:
                    return "Fey Ancestry";
                case Feat.HIGH_ELVEN_WEAPON_TRAINING:
                    return "Elf Weapon Training";
                case Feat.HIGH_ELVEN_CANTRIP:
                    return "Cantrip";
                default:
                    return Enum.GetName(typeof(Feat), feat) + ": (Name missing)";
            }
        }

        public static string Description(this Feat feat) {
            switch (feat) {
                case Feat.DWARVEN_ABILITY_INCREASE:
                    return "Your Constitution score increases by 2.";
                case Feat.HILL_DWARVEN_ABILITY_INCREASE:
                    return "Your Wisdom score increases by 1.";
                case Feat.ELVEN_ABILITY_INCREASE:
                    return "Your Dexterity score increases by 2.";
                case Feat.HIGH_ELVEN_ABILITY_INCREASE:
                    return "Your Intelligence score increases by 1.";
                case Feat.DWARVEN_DARKVISION:
                    return "Accustomed to life underground, you have superior vision in dark and dim conditions. You can see in dim light within 60 feet of you as if it were bright light, and in darkness as if it were dim light. You can’t discern color in darkness, only shades of gray.";
                case Feat.DWARVEN_RESILIENCE:
                    return "You have advantage on saving throws against poison, and you have resistance against poison damage.";
                case Feat.DWARVEN_SMITH:
                case Feat.DWARVEN_BREWER:
                case Feat.DWARVEN_MASON:
                    return "You gain proficiency with the artisan’s tools of your choice: smith’s tools, brewer’s supplies, or mason’s tools.";
                case Feat.DWARVEN_COMBAT_TRAINING:
                    return "You have proficiency with the battleaxe, handaxe, light hammer, and warhammer.";
                case Feat.STONECUNNING:
                    return "Whenever you make an Intelligence (History) check related to the origin of stonework, you are considered proficient in the History skill and add double your proficiency bonus to the check, instead of your normal proficiency bonus.";
                case Feat.DWARVEN_TOUGHNESS:
                    return "Your hit point maximum increases by 1, and it increases by 1 every time you gain a level.";
                case Feat.KEEN_SENSES:
                    return "You have proficiency in the Perception skill.";
                case Feat.FEY_ANCESTRY:
                    return "You have advantage on saving throws against being charmed, and magic can’t put you to sleep.";
                case Feat.HIGH_ELVEN_WEAPON_TRAINING:
                    return "You have proficiency with the longsword, shortsword, shortbow, and longbow.";
                case Feat.HIGH_ELVEN_CANTRIP:
                    return "You know one cantrip of your choice from the wizard spell list. Intelligence is your spellcasting ability for it.";
                default:
                    return Enum.GetName(typeof(Feat), feat) + ": (Description missing)";
            }
        }
    }
}