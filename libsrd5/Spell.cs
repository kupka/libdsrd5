namespace srd5 {
    public class Spell {
        public Spells.ID ID { get; private set; }
        public SpellSchool School { get; private set; }
        public SpellLevel Level { get; private set; }
        public CastingTime CastingTime { get; private set; }
        public int Range { get; private set; }
        public SpellComponent[] Components { get; private set; }
        public SpellDuration Duration { get; private set; }
        public int AreaOfEffect { get; private set; }
        public int MaximumTargets { get; private set; }
        public string Name {
            get {
                return ID.Name();
            }
        }
        private readonly SpellCastEffect cast;

        public Spell(Spells.ID id, SpellSchool school, SpellLevel level, CastingTime castingTime,
                     int range, SpellComponent[] components, SpellDuration duration,
                     int areaOfEffect, int maximumTargets, SpellCastEffect effect) {
            ID = id;
            School = school;
            Level = level;
            CastingTime = castingTime;
            Range = range;
            Components = components;
            Duration = duration;
            AreaOfEffect = areaOfEffect;
            MaximumTargets = maximumTargets;
            cast = effect;
        }

        /* Cast a Spell that doesn't require a battleground, because it does not target
           anyone other or only the caster */
        public void Cast(Combattant caster, int dc, SpellLevel slot, int modifier) {
            BattleGroundClassic ground = new BattleGroundClassic();
            ground.AddCombattant(caster, ClassicLocation.Row.FRONT_LEFT);
            cast(ground, caster, dc, slot, modifier, caster);
        }

        /* Cast a spell on one or more targets */
        public void Cast(Battleground ground, Combattant caster, int dc, SpellLevel slot, int modifier, params Combattant[] targets) {
            cast(ground, caster, dc, slot, modifier, targets);
        }

        public override bool Equals(object obj) {
            if (!(obj is Spell)) return false;
            return obj.GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode() {
            return (int)ID;
        }
    }
}