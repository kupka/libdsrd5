using System;

namespace srd5 {
    public class AvailableSpells {
        public CharacterClass CharacterClass { get; internal set; }
        public Spell[] KnownSpells {
            get {
                return knownSpells;
            }
        }
        private Spell[] knownSpells = new Spell[0];
        public Spell[] PreparedSpells {
            get {
                return preparedSpells;
            }
        }
        private Spell[] preparedSpells = new Spell[0];
        public Spell[] BonusPreparedSpells { get; internal set; } = new Spell[0];
        public int[] SlotsMax { get; internal set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] SlotsCurrent { get; internal set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public AvailableSpells(AbilityType abilityType) {
            CharacterClass clazz = new CharacterClass();
            clazz.SpellCastingAbility = abilityType;
            clazz.MustPrepareSpells = false;
            CharacterClass = clazz;
        }
        public AvailableSpells(CharacterClass clazz) {
            CharacterClass = clazz;
        }

        public void AddKnownSpell(params Spell[] spells) {
            Utils.Push<Spell>(ref knownSpells, spells);
        }

        public void AddPreparedSpell(params Spell[] spells) {
            foreach (Spell spell in spells)
                if (Array.IndexOf(knownSpells, spell) == -1) return;
            Utils.Push<Spell>(ref preparedSpells, spells);
        }

        /// <summary>
        /// Calculates the spell cast DC for the sheet. Assumes that this object belongs to this sheet.
        /// </summary>
        public int GetSpellCastDC(CharacterSheet sheet) {
            int dc = 8;
            dc += sheet.GetAbility(CharacterClass.SpellCastingAbility).Modifier;
            dc += sheet.ProficiencyBonus;
            return dc;
        }

        /// <summary>
        /// Calculates the spell cast DC for the Monster. Assumes that this object belongs to this Monster.
        /// </summary>
        public int GetSpellCastDC(Combattant combattant) {
            if (combattant is CharacterSheet characterSheet) return GetSpellCastDC(characterSheet);
            Monster monster = (Monster)combattant;
            return monster.SpellCastDC;
        }

        /// <summary>
        /// Get the spellcasting modifier for the Combattant. Assumes that this object belongs to this sheet.
        /// <summary>
        public int GetSpellcastingModifier(Combattant combattant) {
            AbilityType spellAbility = CharacterClass.SpellCastingAbility;
            return combattant.GetAbility(spellAbility).Modifier;
        }

    }

}