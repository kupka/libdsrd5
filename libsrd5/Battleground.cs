using System;
using System.Collections;

namespace srd5 {
    public class Tile {

    }

    public class Coord : Location {
        public int X;

        public int Y;

        public Coord(int x, int y) {
            X = x;
            Y = y;
        }

        public override int Distance(Location another) {
            if (!(another is Coord)) throw new Srd5ArgumentException("another must be a Coord.");
            Coord anotherCoord = (Coord)another;
            double dX = this.X - anotherCoord.X;
            double dY = this.Y - anotherCoord.Y;
            double distance = Math.Sqrt(dX * dX + dY * dY);
            // one tile is 5ft x 5ft - round before multiply to have distance % 5 == 0
            return (int)Math.Round(distance) * 5;
        }
    }

    public abstract class Location {
        public abstract int Distance(Location another);
    }

    /// <summary>
    /// Has front and back lines for left (usually heroes) and right (enemies) side of the screen. Classic JRPG-style.
    /// </summary>
    public class ClassicLocation : Location {
        public enum Row {
            BACK_LEFT = 0,
            FRONT_LEFT = 25,
            FRONT_RIGHT = 30,
            BACK_RIGHT = 55
        }

        public static readonly ClassicLocation BackLeft = new ClassicLocation(Row.BACK_LEFT);
        public static readonly ClassicLocation FrontLeft = new ClassicLocation(Row.FRONT_LEFT);
        public static readonly ClassicLocation FrontRight = new ClassicLocation(Row.FRONT_RIGHT);
        public static readonly ClassicLocation BackRight = new ClassicLocation(Row.BACK_RIGHT);

        public Row Location { get; internal set; }

        public ClassicLocation(Row row) {
            Location = row;
        }

        public override int Distance(Location another) {
            if (!(another is ClassicLocation)) throw new Srd5ArgumentException("another must be a ClassicLocation.");
            ClassicLocation classicAnother = (ClassicLocation)another;
            return Math.Max(Math.Abs((int)classicAnother.Location - (int)this.Location), 5);
        }
    }

    internal class ReverseComparer : System.Collections.IComparer {
        int IComparer.Compare(object x, object y) {
            return new CaseInsensitiveComparer().Compare(y, x);
        }
    }

    public class BattleGroundClassic : Battleground {
        private ClassicLocation[] locations = new ClassicLocation[0];

        public override void Initialize() {
            base.Initialize();
            Array.Sort(initiativeRolls, locations, new ReverseComparer());
            Turn = 1;
        }

        public override Location LocateCombattant(Combattant combattant) {
            return locations[Array.IndexOf(combattants, combattant)];
        }

        public ClassicLocation LocateClassicCombattant(Combattant combattant) {
            return locations[Array.IndexOf(combattants, combattant)];
        }


        protected override void SetCurrentLocation(Location location) {
            SetLocation(CurrentCombattant, location);
        }

        protected override void SetLocation(Combattant combattant, Location location) {
            locations[Array.IndexOf(combattants, combattant)] = (ClassicLocation)location;
        }

        public void AddCombattant(Combattant combattant, ClassicLocation.Row row) {
            AddCombattant(combattant);
            Utils.Push<ClassicLocation>(ref locations, new ClassicLocation(row));
        }

        public override void Push(Location source, Combattant target, int distance) {
            if (LocateClassicCombattant(target).Location == ClassicLocation.Row.FRONT_LEFT) {
                SetLocation(target, ClassicLocation.BackLeft);
            } else if (LocateClassicCombattant(target).Location == ClassicLocation.Row.FRONT_RIGHT) {
                SetLocation(target, ClassicLocation.BackRight);
            }
        }
    }

    public class Battleground2D : Battleground {
        public Tile[,] Tiles { get; internal set; }
        private Coord[] coords = new Coord[0];

        public Battleground2D(int x, int y) {
            Tiles = new Tile[x, y];
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    Tiles[i, j] = new Tile();
                }
            }
        }

        public override void Initialize() {
            base.Initialize();
            Array.Sort(initiativeRolls, coords, new ReverseComparer());
            Turn = 1;
        }

        public override Location LocateCombattant(Combattant combattant) {
            return coords[Array.IndexOf(combattants, combattant)];
        }

        public Coord LocateCombattant2D(Combattant combattant) {
            return coords[Array.IndexOf(combattants, combattant)];
        }

        public void AddCombattant(Combattant combattant, int x, int y) {
            AddCombattant(combattant);
            Utils.Push<Coord>(ref coords, new Coord(x, y));
        }

        protected override void SetCurrentLocation(Location location) {
            SetLocation(CurrentCombattant, location);
        }

        protected override void SetLocation(Combattant combattant, Location location) {
            coords[Array.IndexOf(combattants, combattant)] = (Coord)location;
        }

        public override void Push(Location source, Combattant target, int distance) {
            // For simplicity, this assumes either a straight line or 45° angle. 
            Coord sourceCoord = (Coord)source;
            Coord targetCoord = LocateCombattant2D(target);
            Coord destination = targetCoord;
            distance /= 5;
            if (targetCoord.X == sourceCoord.X) {
                if (targetCoord.Y <= sourceCoord.Y) { // push down
                    destination = new Coord(targetCoord.X, targetCoord.Y - distance);
                } else { // push up
                    destination = new Coord(targetCoord.X, targetCoord.Y + distance);
                }
            } else if (targetCoord.Y == sourceCoord.Y) {
                if (targetCoord.X <= sourceCoord.X) { // push left
                    destination = new Coord(targetCoord.X - distance, targetCoord.Y);
                } else { // push right
                    destination = new Coord(targetCoord.X + distance, targetCoord.Y);
                }
            } else if (targetCoord.X < sourceCoord.X) { // push left
                distance /= 2;
                if (distance == 0) distance = 1;
                if (targetCoord.Y < sourceCoord.Y) { // push down
                    destination = new Coord(targetCoord.X - distance, targetCoord.Y - distance);
                } else if (targetCoord.Y > sourceCoord.Y) { // push up
                    destination = new Coord(targetCoord.X - distance, targetCoord.Y + distance);
                }
            } else { // push right
                distance /= 2;
                if (distance == 0) distance = 1;
                if (targetCoord.Y < sourceCoord.Y) { // push down
                    destination = new Coord(targetCoord.X + distance, targetCoord.Y - distance);
                } else if (targetCoord.Y > sourceCoord.Y) { // push up
                    destination = new Coord(targetCoord.X + distance, targetCoord.Y + distance);
                }
            }
            SetLocation(target, destination);
        }
    }

    public enum TurnPhase {
        MOVE,
        ACTION,
        BONUS_ACTION
    }

    public class CombattantChangedEvent : BattlegroundEvent {
        public Combattant CurrentCombattant { get; internal set; }
    }

    public class BattlegroundEvent : EventArgs {
    }

    public abstract class Battleground {

        protected Combattant[] combattants = new Combattant[0];

        public int Turn { get; internal set; } = 0;
        protected int[] initiativeRolls = new int[0];
        public Combattant CurrentCombattant {
            get {
                return combattants[currentCombattant];
            }
        }
        protected int currentCombattant = 0;
        public int RemainingSpeed {
            get {
                return remainingSpeed;
            }
        }
        protected int remainingSpeed = 0;
        protected TurnPhase currentPhase = TurnPhase.MOVE;

        public virtual void Initialize() {
            if (Turn > 0) return;
            // Sort Combattants and Coords according to Initiative Rolls
            int[] backup = new int[initiativeRolls.Length];
            Array.Copy(initiativeRolls, backup, initiativeRolls.Length);
            Array.Sort(backup, combattants, new ReverseComparer());
            currentCombattant = 0;
            currentPhase = TurnPhase.MOVE;
            remainingSpeed = CurrentCombattant.Speed;
        }

        private void nextCombattant() {
            currentCombattant++;
            currentCombattant %= combattants.Length;
            if (currentCombattant == 0) Turn++;
            currentPhase = TurnPhase.MOVE;
            remainingSpeed = CurrentCombattant.Speed;
            onCurrentCombattantChanged();
        }

        /// <summary>
        /// Add a combattant to the battlefield and roll initiative
        /// </summary>
        public void AddCombattant(Combattant combattant) {
            if (Array.IndexOf(combattants, combattant) > -1) return;
            Utils.Push<Combattant>(ref combattants, combattant);
            int roll = Die.D20.Value + combattant.Dexterity.Modifier;
            Utils.Push<int>(ref initiativeRolls, roll);
            GlobalEvents.RolledInitiative(combattant, roll);
        }

        /// <summary>
        /// Move on to the next phase (MOVE -> ACTION -> BONUS ACTION -> Next Combattant)
        /// </summary>
        public TurnPhase NextPhase() {
            switch (currentPhase) {
                case TurnPhase.MOVE:
                    remainingSpeed = 0;
                    currentPhase = TurnPhase.ACTION;
                    break;
                case TurnPhase.ACTION:
                    currentPhase = TurnPhase.BONUS_ACTION;
                    break;
                case TurnPhase.BONUS_ACTION:
                    nextCombattant();
                    break;
            }
            return currentPhase;
        }

        /// <summary>
        /// Move the current combattant to the target destination if able
        /// </summary>
        public bool MoveAction(Location destination) {
            if (CurrentCombattant.HasEffect(Effect.CANNOT_TAKE_ACTIONS)) return false;
            if (destination == null) throw new Srd5ArgumentException("destination cannot be null");
            int distance = destination.Distance(LocateCombattant(CurrentCombattant));
            if (distance > remainingSpeed) return false;
            remainingSpeed -= distance;
            SetCurrentLocation(destination);
            return true;
        }

        /// <summary>
        /// Current combattant melee attacks a target if able
        /// </summary>
        public bool MeleeAttackAction(Combattant target) {
            if (CurrentCombattant.HasEffect(Effect.CANNOT_TAKE_ACTIONS)) return false;
            if (currentPhase == TurnPhase.MOVE) return false;
            if (target == null) throw new Srd5ArgumentException("target cannot be null");
            if (target == CurrentCombattant) throw new Srd5ArgumentException("cannot attack self");
            bool success = false;
            if (currentPhase == TurnPhase.ACTION)
                success = doFullMeleeAttack(target);
            else
                success = doBonusMeleeAttack(target);
            if (success) NextPhase();
            return success;
        }

        /// <summary>
        /// Current combattant uses their ranged attack against the target if able
        /// </summary>
        public bool RangedAttackAction(Combattant target) {
            if (target == null) throw new Srd5ArgumentException("target cannot be null");
            if (CurrentCombattant.HasEffect(Effect.CANNOT_TAKE_ACTIONS)) {
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.CANNOT_TAKE_ACTIONS);
                return false;
            }
            if (currentPhase == TurnPhase.MOVE) {
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.WRONG_PHASE);
                return false;
            }
            if (target == CurrentCombattant) throw new Srd5ArgumentException("cannot attack self");
            bool success = false;
            if (currentPhase == TurnPhase.ACTION)
                success = doFullRangedAttack(target);
            else
                success = doBonusRangedAttack(target);
            if (!success) {
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.TARGET_OUT_OF_RANGE);
            }
            if (success) NextPhase();
            return success;
        }

        /// <summary>
        /// Current combattant casts a spell if able. Checks all relevant constraints, such as range and if the spell is prepared
        /// <summary>
        public bool SpellCastAction(Spell spell, SpellLevel slot, AvailableSpells availableSpells, params Combattant[] targets) {
            if (CurrentCombattant.HasEffect(Effect.CANNOT_TAKE_ACTIONS)) {
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.CANNOT_TAKE_ACTIONS);
                return false;
            }
            // check if phase is valid for spell
            if (currentPhase == TurnPhase.BONUS_ACTION && spell.CastingTime != CastingTime.BONUS_ACTION) {
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.WRONG_PHASE);
                return false;
            } else if (currentPhase == TurnPhase.MOVE) {
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.WRONG_PHASE);
                return false;
            }
            // check if spell is known
            if (Array.IndexOf(availableSpells.KnownSpells, spell) == -1) {
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.SPELL_NOT_KNOWN);
                return false;
            }
            // check if spell is prepared
            if (availableSpells.CharacterClass.MustPrepareSpells == true
                    && Array.IndexOf(availableSpells.PreparedSpells, spell) == -1
                    && Array.IndexOf(availableSpells.BonusPreparedSpells, spell) == -1) {
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.SPELL_NOT_PREPARED);
                return false;
            }
            // check if spell allows amount of targets
            if (spell.MaximumTargets < targets.Length) {
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.WRONG_NUMBER_OF_TARGETS);
                return false;
            }
            // check if targets are in range
            foreach (Combattant target in targets) {
                int distance = Distance(target);
                if (distance > spell.Range) {
                    GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.TARGET_OUT_OF_RANGE);
                    return false;
                }
            }
            // if the spell has an area of effect, check that all targets are within this area of the first target
            if (spell.AreaOfEffect > 0) {
                for (int i = 1; i < targets.Length; i++) {
                    int distance = Distance(targets[0], targets[i]);
                    if (distance > spell.AreaOfEffect) {
                        GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.TARGET_OUT_OF_RANGE);
                        return false;
                    }
                }
            }
            // Check if slot is sufficient for spell
            if (slot < spell.Level) {
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.SPELLSLOT_INVALID);
                return false;
            }
            InnateSpellcasting innate = null;
            if (CurrentCombattant is Monster monster) {
                innate = monster.InnateSpellcastingBySpell(spell);
            }
            // Check if innate Spell casting is available
            if (innate != null && innate.Frequency == InnateSpellcasting.Frequencies.AT_WILL) {
                // can cast at will, nothing to do
            } else if (innate != null) {
                // requires available use
                int maxUses = (int)(((Monster)CurrentCombattant).InnateSpellcastingBySpell(spell).Frequency);
                if (maxUses == innate.Uses) {
                    GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.INSUFFICIENT_USES);
                    return false;
                }
                innate.Uses++;
            } else if (availableSpells.SlotsCurrent[(int)slot] <= 0) { // otherwise, check if slot is available
                GlobalEvents.FailAction(CurrentCombattant, GlobalEvents.ActionFailed.Reasons.SPELLSLOT_EMPTY);
                return false;
            } else {
                // Expend slot if not Cantrip
                if (slot != SpellLevel.CANTRIP) availableSpells.SlotsCurrent[(int)slot]--;
            }
            // Cast Spell
            int modifier = availableSpells.GetSpellcastingModifier(CurrentCombattant);
            spell.Cast(this, CurrentCombattant, availableSpells.GetSpellCastDC(CurrentCombattant), slot, modifier, targets);
            NextPhase();
            return true;
        }

        private bool doBonusMeleeAttack(Combattant target) {
            bool success = false;
            int distance = Distance(target);
            Attack attack = CurrentCombattant.BonusAttack;
            if (attack == null || distance > attack.Reach) return false;
            success = true;
            CurrentCombattant.Attack(attack, target, distance);
            return success;
        }

        private bool doBonusRangedAttack(Combattant target) {
            bool success = false;
            int distance = Distance(target);
            Attack attack = CurrentCombattant.BonusAttack;
            if (attack == null || distance > attack.RangeLong) return false;
            success = true;
            CurrentCombattant.Attack(attack, target, distance, true);
            return success;
        }

        private bool doFullMeleeAttack(Combattant target) {
            bool success = false;
            int distance = Distance(target);
            foreach (Attack attack in CurrentCombattant.MeleeAttacks) {
                if (distance > attack.Reach) continue; // skip attack when out of reach
                success = true;
                CurrentCombattant.Attack(attack, target, distance);
            }
            return success;
        }

        private bool doFullRangedAttack(Combattant target) {
            bool success = false;
            int distance = Distance(target);
            foreach (Attack attack in CurrentCombattant.RangedAttacks) {
                if (distance > attack.RangeLong) continue; // skip attack when out of range
                success = true;
                CurrentCombattant.Attack(attack, target, distance, true);
            }
            return success;
        }

        /// <summary>
        /// Returns the Location of the given combattant
        /// </summary>
        public abstract Location LocateCombattant(Combattant combattant);

        /// <summary>
        /// Set the location of the current active combattant
        /// </summary>
        protected abstract void SetCurrentLocation(Location location);

        /// <summary>
        /// Set the location of the combattant
        /// </summary>
        protected abstract void SetLocation(Combattant combattant, Location location);


        /// <summary>
        /// Get the location of the current active combattant
        /// </summary>
        public virtual Location GetCurrentLocation() {
            return LocateCombattant(CurrentCombattant);
        }

        /// <summary>
        /// Returns the distance between the current combattant and the target.
        /// </summary>
        public int Distance(Combattant target) {
            return GetCurrentLocation().Distance(LocateCombattant(target));
        }

        /// <summary>
        /// Returns the distance between two combattants.
        /// </summary>
        public int Distance(Combattant combattant1, Combattant combattant2) {
            return LocateCombattant(combattant1).Distance(LocateCombattant(combattant2));
        }

        // Events
        public event EventHandler<BattlegroundEvent> EventSubscription;
        private void onCurrentCombattantChanged() {
            if (EventSubscription == null) return;
            CombattantChangedEvent bgEvent = new CombattantChangedEvent();
            bgEvent.CurrentCombattant = this.CurrentCombattant;
            EventSubscription(this, bgEvent);
        }

        /// <summary>
        /// Pushes the target away from the source by the distance.
        /// </summar>)
        public abstract void Push(Location source, Combattant target, int distance);
    }
}