using Xunit;
using System;

namespace srd5 {
    [CollectionDefinition("SingleThreaded", DisableParallelization = true)]
    [Collection("SingleThreaded")]
    public class BattlegroundTest {
        [Fact]
        public void Setup2DTest() {
            Battleground2D ground = new Battleground2D(100, 100);
            CharacterSheet hero = new CharacterSheet(Race.HUMAN);
            hero.Name = "Bob";
            Monster badguy = Monsters.Ogre;
            Random.State = 7; // Fix deterministic random so that ogre wins initiative
            ground.AddCombattant(hero, 10, 10);
            ground.AddCombattant(badguy, 30, 30);
            Assert.Equal(hero.Name, ground.CurrentCombattant.Name);
            Assert.Equal(ground.LocateCombattant(hero), ground.LocateCombattant2D(hero));
            Assert.Equal(10, ground.LocateCombattant2D(hero).X);
            Assert.Equal(30, ground.LocateCombattant2D(badguy).Y);
            Assert.Equal(140, ground.LocateCombattant(hero).Distance(ground.LocateCombattant(badguy)));
            Assert.Throws<Srd5ArgumentException>(delegate {
                ground.LocateCombattant(hero).Distance(new ClassicLocation(ClassicLocation.Row.FRONT_LEFT));
            });
            ground.Initialize();
            Assert.Equal(badguy.Name, ground.CurrentCombattant.Name);
            Assert.Equal(30, ground.LocateCombattant2D(badguy).X);
            Assert.Equal(30, ((Coord)ground.GetCurrentLocation()).X);
            Assert.Equal(140, ground.Distance(hero));
            ground.Initialize(); // nothing should happen
            Assert.Equal(badguy.Name, ground.CurrentCombattant.Name);
            Assert.Equal(10, ground.LocateCombattant2D(hero).X);
        }

        [Fact]
        public void SetupClassicTest() {
            BattleGroundClassic classic = new BattleGroundClassic();
            Monster boar = Monsters.Boar;
            classic.AddCombattant(boar, ClassicLocation.Row.BACK_RIGHT);
            classic.Initialize();
            while (TurnPhase.MOVE != classic.NextPhase()) ;
            classic.MoveAction(ClassicLocation.FrontRight);
            Assert.Equal(ClassicLocation.FrontRight, classic.LocateCombattant(boar));
        }

        [Fact]
        public void ClassicDistanceTest() {
            ClassicLocation backLeft = new ClassicLocation(ClassicLocation.Row.BACK_LEFT);
            ClassicLocation frontLeft = new ClassicLocation(ClassicLocation.Row.FRONT_LEFT);
            ClassicLocation frontRight = new ClassicLocation(ClassicLocation.Row.FRONT_RIGHT);
            ClassicLocation backRight = new ClassicLocation(ClassicLocation.Row.BACK_RIGHT);
            Assert.Equal(5, backRight.Distance(backRight));
            Assert.True(frontLeft.Distance(backLeft) == backLeft.Distance(frontLeft));
            Assert.True(backLeft.Distance(frontLeft) < backLeft.Distance(frontRight));
            Assert.True(backLeft.Distance(frontRight) < backLeft.Distance(backRight));
            Assert.Throws<Srd5ArgumentException>(delegate {
                frontLeft.Distance(new Coord(2, 3));
            });
        }

        [Fact]
        public void MoveTest() {
            CharacterSheet sheet = new CharacterSheet(Race.HILL_DWARF);
            Battleground2D ground = new Battleground2D(50, 50);
            ground.AddCombattant(sheet, 10, 10);
            ground.Initialize();
            Assert.Throws<Srd5ArgumentException>(delegate {
                ground.MoveAction(null);
            });
            sheet.AddEffect(Effect.CANNOT_TAKE_ACTIONS);
            Assert.False(ground.MoveAction(new Coord(13, 13))); // incapacitated
            sheet.RemoveEffect(Effect.CANNOT_TAKE_ACTIONS);
            Assert.True(ground.MoveAction(new Coord(13, 13))); // distance 20 => remaining speed = 5
            Assert.Equal(5, ground.RemainingSpeed);
            Assert.Equal(13, ground.LocateCombattant2D(sheet).X);
            Assert.False(ground.MoveAction(new Coord(15, 13))); // distance 5 => can't move
            Assert.Equal(13, ground.LocateCombattant2D(sheet).X);
            ground.NextPhase();
            Assert.Equal(1, ground.Turn);
            ground.NextPhase();
            ground.NextPhase();
            Assert.Equal(2, ground.Turn);
        }

        [Fact]
        public void AttackTest2D() {
            Battleground2D ground = new Battleground2D(5, 5);
            CharacterSheet hero = new CharacterSheet(Race.HILL_DWARF);
            hero.Strength.BaseValue = 18;
            hero.Dexterity.BaseValue = 10;
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.HitPoints = hero.HitPointsMax;
            hero.Equip(Weapons.Greataxe);
            ground.AddCombattant(hero, 1, 1);
            Monster badger = Monsters.GiantBadger;
            ground.AddCombattant(badger, 1, 2);
            ground.Initialize();
            while (ground.CurrentCombattant == hero) ground.NextPhase();
            ground.NextPhase(); // skip move
            Assert.True(ground.MeleeAttackAction(hero));
            ground.NextPhase(); // skip bonus action
            Assert.Equal(hero, ground.CurrentCombattant);
            ground.NextPhase(); // skip move
            hero.BonusAttack = new Attack("Test Attack", 0, new Damage(DamageType.BLUDGEONING, "1d6+4"), 5);
            Random.State = 11; // Fix deterministic random to guarantee critical hit
            Assert.True(ground.MeleeAttackAction(badger));
            Random.State = 10; // Fix deterministic random to guarantee normal hit     
            Assert.Throws<Srd5ArgumentException>(delegate {
                ground.MeleeAttackAction(null);
            });
            Assert.Throws<Srd5ArgumentException>(delegate {
                ground.MeleeAttackAction(hero);
            });
            Assert.True(ground.MeleeAttackAction(badger));
            Assert.False(ground.MeleeAttackAction(hero));
        }

        [Fact]
        public void TooFarTest() {
            Battleground2D ground = new Battleground2D(5, 5);
            CharacterSheet hero = new CharacterSheet(Race.HILL_DWARF);
            hero.Strength.BaseValue = 18;
            hero.Dexterity.BaseValue = 10;
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.HitPoints = hero.HitPointsMax;
            hero.Equip(Weapons.Greataxe);
            hero.BonusAttack = new Attack("Test Attack", 0, new Damage(DamageType.BLUDGEONING, "1d6+4"), 5);
            Monster badger = Monsters.GiantBadger;
            ground.AddCombattant(hero, 1, 1);
            ground.AddCombattant(badger, 5, 5);
            while (ground.CurrentCombattant != hero) {
                ground.NextPhase();
            }
            Assert.Equal(TurnPhase.ACTION, ground.NextPhase()); // skip move            
            Assert.False(ground.MeleeAttackAction(badger));
            Assert.Equal(TurnPhase.BONUS_ACTION, ground.NextPhase()); // skip action            
            Assert.False(ground.MeleeAttackAction(badger));
        }

        [Fact]
        public void AttackTest2DWithBonusDamageCritical() {
            Battleground2D ground = new Battleground2D(5, 5);
            CharacterSheet hero = new CharacterSheet(Race.HILL_DWARF);
            hero.Strength.BaseValue = 18;
            hero.Dexterity.BaseValue = 10;
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.HitPoints = hero.HitPointsMax;
            hero.Equip(Weapons.Greataxe);
            Random.State = 3; // Fix deterministic random so that ogre goes first
            ground.AddCombattant(hero, 1, 1);
            Monster ogre = Monsters.Ogre;
            ground.AddCombattant(ogre, 1, 2);
            ground.Initialize();
            ground.NextPhase(); // skip move
            Assert.True(ground.MeleeAttackAction(hero));
            ground.NextPhase();
            Assert.Equal(hero, ground.CurrentCombattant);
            ground.NextPhase(); // skip move
            hero.BonusAttack = new Attack("Test Attack", 0, new Damage(DamageType.BLUDGEONING, "1d6+4"), 5, 0, 0, new Damage(DamageType.COLD, "1d4+1"));
            Random.State = 10; // Fix deterministic random to guarantee normal hit
            Assert.True(ground.MeleeAttackAction(ogre));
            Random.State = 11; // Fix deterministic random to guarantee critical hit
            Assert.True(ground.MeleeAttackAction(ogre));
            Assert.False(ground.MeleeAttackAction(hero));
            ground.NextPhase(); // End hero turn
            ground.NextPhase(); // Skip ogre move
            ground.NextPhase(); // Skip ogre attack
            ground.NextPhase(); // Skip ogre bonus attack
            Assert.True(ground.MeleeAttackAction(ogre));
            Random.State = 10; // Fix deterministic random to guarantee normal hit
            Assert.True(ground.MeleeAttackAction(ogre));
        }


        [Fact]
        public void OutOfRangeTest() {
            Battleground2D ground = new Battleground2D(5, 5);
            CharacterSheet hero = new CharacterSheet(Race.HILL_DWARF);
            hero.Strength.BaseValue = 18;
            hero.Dexterity.BaseValue = 10;
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.Equip(Weapons.Battleaxe);
            Random.State = 1; // Fix deterministic random so that hero goes first
            ground.AddCombattant(hero, 1, 1);
            Monster ogre = Monsters.Ogre;
            ground.AddCombattant(ogre, 4, 4);
            ground.Initialize();
            ground.NextPhase(); // skip move  
            Assert.Equal(hero, ground.CurrentCombattant);
            Assert.False(ground.MeleeAttackAction(ogre));
        }

        private void setupBattleField2D(ref Battleground2D ground, ref CharacterSheet hero, ref Monster ogre) {
            ground = new Battleground2D(5, 5);
            hero = new CharacterSheet(Race.HILL_DWARF);
            hero.Strength.BaseValue = 18;
            hero.Dexterity.BaseValue = 10;
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.Equip(Weapons.Battleaxe);
            Random.State = 1; // Fix deterministic random so that hero goes first
            ground.AddCombattant(hero, 1, 1);
            ogre = Monsters.Ogre;
            ground.AddCombattant(ogre, 1, 2);
            ground.Initialize();

        }

        [Fact]
        public void SpecialConditionTest() {
            Battleground2D ground = null;
            CharacterSheet hero = null;
            Monster ogre = null;
            // Advantage on attack
            setupBattleField2D(ref ground, ref hero, ref ogre);
            ground.NextPhase(); // skip move
            ogre.AddEffect(Effect.ADVANTAGE_ON_BEING_ATTACKED);
            Assert.True(ground.MeleeAttackAction(ogre));
            setupBattleField2D(ref ground, ref hero, ref ogre);
            ground.NextPhase(); // skip move
            hero.AddEffect(Effect.ADVANTAGE_ON_ATTACK);
            Assert.True(ground.MeleeAttackAction(ogre));
            // Disadvantage on attack
            setupBattleField2D(ref ground, ref hero, ref ogre);
            ground.NextPhase(); // skip move
            ogre.AddEffect(Effect.DISADVANTAGE_ON_BEING_ATTACKED);
            Assert.True(ground.MeleeAttackAction(ogre));
            setupBattleField2D(ref ground, ref hero, ref ogre);
            ground.NextPhase(); // skip move
            hero.AddEffect(Effect.DISADVANTAGE_ON_ATTACK);
            Assert.True(ground.MeleeAttackAction(ogre));
            // Nullify advtange and disadvantage
            setupBattleField2D(ref ground, ref hero, ref ogre);
            ground.NextPhase(); // skip move
            ogre.AddEffect(Effect.DISADVANTAGE_ON_BEING_ATTACKED);
            hero.AddEffect(Effect.ADVANTAGE_ON_ATTACK);
            Assert.True(ground.MeleeAttackAction(ogre));
            // Auto Crit
            setupBattleField2D(ref ground, ref hero, ref ogre);
            ground.NextPhase(); // skip move
            ogre.AddEffect(Effect.AUTOMATIC_CRIT_ON_BEING_HIT_WITHIN_5_FT);
            hero.AddEffect(Effect.AUTOMATIC_CRIT_ON_HIT);
            Assert.True(ground.MeleeAttackAction(ogre));
            // Incapacitated
            setupBattleField2D(ref ground, ref hero, ref ogre);
            ground.NextPhase(); // skip move
            hero.AddEffect(Effect.CANNOT_TAKE_ACTIONS);
            Assert.False(ground.MeleeAttackAction(ogre));
            Assert.False(ground.RangedAttackAction(ogre));
        }

        [Fact]
        public void CastMagicMissileTest() {
            Battleground2D ground = new Battleground2D(30, 30);
            CharacterSheet hero = new CharacterSheet(Race.TIEFLING, true);
            ground.AddCombattant(hero, 0, 0);
            hero.AddLevel(CharacterClasses.Druid);
            Monster ogre = Monsters.Ogre;
            Monster ogre2 = Monsters.Ogre;
            ground.AddCombattant(ogre, 10, 10);
            ground.AddCombattant(ogre2, 30, 30);
            while (ground.CurrentCombattant != hero) {
                ground.NextPhase();
            }
            // incapacitated
            hero.AddEffect(Effect.CANNOT_TAKE_ACTIONS);
            Assert.False(ground.SpellCastAction(Spells.MagicMissile, SpellLevel.FIRST, hero.AvailableSpells[0], ogre));
            hero.RemoveEffect(Effect.CANNOT_TAKE_ACTIONS);
            // wrong phase
            Assert.False(ground.SpellCastAction(Spells.MagicMissile, SpellLevel.FIRST, hero.AvailableSpells[0], ogre));
            ground.NextPhase();
            // not known
            Assert.False(ground.SpellCastAction(Spells.MagicMissile, SpellLevel.FIRST, hero.AvailableSpells[0], ogre));
            hero.AvailableSpells[0].AddKnownSpell(Spells.MagicMissile);
            // not prepared
            Assert.False(ground.SpellCastAction(Spells.MagicMissile, SpellLevel.FIRST, hero.AvailableSpells[0], ogre));
            hero.AvailableSpells[0].AddPreparedSpell(Spells.MagicMissile);
            // out of range
            Assert.False(ground.SpellCastAction(Spells.MagicMissile, SpellLevel.FIRST, hero.AvailableSpells[0], ogre2));
            // no slot available
            Assert.False(ground.SpellCastAction(Spells.MagicMissile, SpellLevel.FIRST, hero.AvailableSpells[0], ogre));
            hero.LongRest();
            int slots = ground.CurrentCombattant.AvailableSpells[0].SlotsCurrent[(int)SpellLevel.FIRST];
            // slot invalid
            Assert.False(ground.SpellCastAction(Spells.MagicMissile, SpellLevel.CANTRIP, hero.AvailableSpells[0], ogre));
            // All good
            Assert.True(ground.SpellCastAction(Spells.MagicMissile, SpellLevel.FIRST, hero.AvailableSpells[0], ogre));
            Assert.Equal(slots - 1, ground.CurrentCombattant.AvailableSpells[0].SlotsCurrent[(int)SpellLevel.FIRST]);
            Assert.True(ogre.HitPointsMax > ogre.HitPoints);
            // wrong phase (Bonus)
            Assert.False(ground.SpellCastAction(Spells.MagicMissile, SpellLevel.FIRST, hero.AvailableSpells[0], ogre));
            // Add Shillelagh and cast as bonus action
            hero.AvailableSpells[0].AddKnownSpell(Spells.Shillelagh);
            hero.AvailableSpells[0].AddPreparedSpell(Spells.Shillelagh);
            Assert.True(ground.SpellCastAction(Spells.Shillelagh, SpellLevel.CANTRIP, hero.AvailableSpells[0]));
        }

        [Fact]
        public void MonsterMagicTestInnateAtWill() {
            Battleground2D ground = new Battleground2D(10, 10);
            Monster hag = Monsters.NightHag;
            Monster ogre = Monsters.Ogre;
            ground.AddCombattant(hag, 5, 5);
            ground.AddCombattant(ogre, 8, 8);
            while (ground.CurrentCombattant != hag) {
                ground.NextPhase();
            }
            ground.NextPhase();
            Assert.True(ground.SpellCastAction(Spells.MagicMissile, SpellLevel.FIRST, hag.AvailableSpells[0], ogre));
            Assert.True(ogre.HitPointsMax > ogre.HitPoints);
        }

        [Fact]
        public void MonsterMagicTestInnateLimited() {
            Battleground2D ground = new Battleground2D(10, 10);
            Monster hag = Monsters.NightHag;
            Monster ogre = Monsters.Ogre;
            ground.AddCombattant(hag, 5, 5);
            ground.AddCombattant(ogre, 8, 8);
            while (ground.CurrentCombattant != hag) {
                ground.NextPhase();
            }
            ground.NextPhase();
            Assert.True(ground.SpellCastAction(Spells.Sleep, SpellLevel.FIRST, hag.AvailableSpells[0], ogre));
            ground.NextPhase();
            while (ground.CurrentCombattant != hag) {
                ground.NextPhase();
            }
            ground.NextPhase();
            Assert.True(ground.SpellCastAction(Spells.Sleep, SpellLevel.FIRST, hag.AvailableSpells[0], ogre));
            ground.NextPhase();
            while (ground.CurrentCombattant != hag) {
                ground.NextPhase();
            }
            ground.NextPhase();
            Assert.False(ground.SpellCastAction(Spells.Sleep, SpellLevel.FIRST, hag.AvailableSpells[0], ogre));
        }


        [Fact]
        public void CastAreaEffectTest() {
            Battleground2D ground = new Battleground2D(20, 20);
            CharacterSheet hero = new CharacterSheet(Race.TIEFLING, true);
            Combattant currentCombattant = null;
            ground.EventSubscription += delegate (object sender, BattlegroundEvent bgEvent) {
                if (bgEvent is CombattantChangedEvent @event) {
                    currentCombattant = @event.CurrentCombattant;
                }
            };
            ground.AddCombattant(hero, 0, 0);
            hero.AddLevel(CharacterClasses.Druid);
            hero.LongRest();
            hero.AvailableSpells[0].AddKnownSpell(Spells.AcidSplash);
            hero.AvailableSpells[0].AddPreparedSpell(Spells.AcidSplash);
            Monster ogre = Monsters.Ogre;
            Monster ogre2 = Monsters.Ogre;
            Monster ogre3 = Monsters.Ogre;
            Monster ogre4 = Monsters.Ogre;
            ground.AddCombattant(ogre, 6, 6);
            ground.AddCombattant(ogre2, 6, 7);
            ground.AddCombattant(ogre3, 7, 6);
            ground.AddCombattant(ogre4, 8, 8);
            while (currentCombattant != hero) {
                ground.NextPhase();
            }
            ground.NextPhase();
            // Too many targets
            Assert.False(ground.SpellCastAction(Spells.AcidSplash, SpellLevel.CANTRIP, hero.AvailableSpells[0], ogre, ogre2, ogre3));
            // Target outside area of effect
            Assert.False(ground.SpellCastAction(Spells.AcidSplash, SpellLevel.CANTRIP, hero.AvailableSpells[0], ogre, ogre4));
            Random.State = 11; // Fix random so one ogre fails DC
            Assert.True(ground.SpellCastAction(Spells.AcidSplash, SpellLevel.CANTRIP, hero.AvailableSpells[0], ogre, ogre2));
            Assert.True(ogre.HitPointsMax > ogre.HitPoints);
            Assert.True(ogre2.HitPointsMax == ogre2.HitPoints);
        }

        [Fact]
        public void ClassicBattlegroundTest() {
            CharacterSheet hero = new CharacterSheet(Race.HILL_DWARF, true);
            hero.AddLevel(CharacterClasses.Barbarian);
            hero.Name = "Boldo";
            hero.Equip(Shields.Shield);
            hero.Equip(Weapons.Longsword);
            hero.Equip(Armors.StuddedLeatherArmor);
            Monster boar = Monsters.Orc;
            BattleGroundClassic battle = new BattleGroundClassic();
            battle.AddCombattant(hero, ClassicLocation.Row.FRONT_LEFT);
            battle.AddCombattant(boar, ClassicLocation.Row.FRONT_LEFT);
            battle.Initialize();
            while (battle.CurrentCombattant.HitPoints > 0) {
                if (battle.NextPhase() == TurnPhase.ACTION) {
                    if (battle.CurrentCombattant == hero) {
                        Assert.True(battle.MeleeAttackAction(boar));
                        Assert.False(battle.MeleeAttackAction(boar)); // no bonus attack
                    } else {
                        Assert.True(battle.MeleeAttackAction(hero));
                        Assert.False(battle.MeleeAttackAction(hero)); // no bonus attack
                    }
                }
            }
        }

        [Fact]
        public void RangedTest() {
            Monster ogre = Monsters.Ogre;
            Monster bandit = Monsters.Bandit;
            BattleGroundClassic battle = new BattleGroundClassic();
            battle.AddCombattant(ogre, ClassicLocation.Row.BACK_LEFT);
            battle.AddCombattant(bandit, ClassicLocation.Row.BACK_RIGHT);
            bandit.BonusAttack = Attacks.GoblinShortbow;
            battle.Initialize();
            Random.State = 1;
            if (battle.CurrentCombattant == ogre)
                Assert.False(battle.RangedAttackAction(bandit));
            else
                Assert.False(battle.RangedAttackAction(ogre));
            while (bandit.HitPoints > 0) {
                while (battle.NextPhase() != TurnPhase.ACTION) ;
                Assert.Throws<Srd5ArgumentException>(delegate { battle.RangedAttackAction(null); });
                if (battle.CurrentCombattant == ogre)
                    Assert.True(battle.RangedAttackAction(bandit));
                else {
                    Assert.Throws<Srd5ArgumentException>(delegate { battle.RangedAttackAction(bandit); });
                    Assert.True(battle.RangedAttackAction(ogre));
                    // Bonus Attack
                    Assert.True(battle.RangedAttackAction(ogre));
                }
            }
        }

        [Fact]
        public void RangedDifferentRangesTest() {
            Monster ogre = Monsters.Ogre;
            Monster[] orcs = new Monster[4];
            orcs[0] = Monsters.Orc;
            orcs[1] = Monsters.Orc;
            orcs[2] = Monsters.Orc;
            orcs[3] = Monsters.Orc;
            Battleground2D battle = new Battleground2D(30, 1);
            battle.AddCombattant(ogre, 0, 0);
            battle.AddCombattant(orcs[0], 1, 0); // next to ogre => Disadvantage
            battle.AddCombattant(orcs[1], 3, 0); // normal range
            battle.AddCombattant(orcs[2], 15, 0); // long range
            battle.AddCombattant(orcs[3], 28, 0); // out of range
            battle.Initialize();
            for (int i = 0; i < orcs.Length; i++) {
                while (battle.CurrentCombattant != ogre) {
                    battle.NextPhase();
                }
                battle.NextPhase(); // skip move
                if (i < orcs.Length - 1)
                    Assert.True(battle.RangedAttackAction(orcs[i])); // in range
                else {
                    Assert.False(battle.RangedAttackAction(orcs[i])); // out of range
                    battle.NextPhase();
                }
                Assert.False(battle.RangedAttackAction(orcs[i])); // no bonus attack
                battle.NextPhase(); // skip bonus attack
            }
        }

        [Fact]
        public void PushTest2D() {
            Battleground2D ground = new Battleground2D(10, 10);
            Combattant ogre = Monsters.Ogre;
            ground.AddCombattant(ogre, 5, 5);
            ground.Initialize();
            ground.Push(new Coord(5, 4), ogre, 10); // push south
            Assert.Equal(7, ground.LocateCombattant2D(ogre).Y);
            ground.Push(new Coord(5, 8), ogre, 5); // push north
            Assert.Equal(6, ground.LocateCombattant2D(ogre).Y);
            ground.Push(new Coord(4, 6), ogre, 15); // push east
            Assert.Equal(8, ground.LocateCombattant2D(ogre).X);
            ground.Push(new Coord(9, 6), ogre, 15); // push west
            Assert.Equal(5, ground.LocateCombattant2D(ogre).X);
            ground.Push(new Coord(4, 3), ogre, 10); // push northeast
            Assert.Equal(6, ground.LocateCombattant2D(ogre).X);
            Assert.Equal(7, ground.LocateCombattant2D(ogre).Y);
            ground.Push(new Coord(4, 9), ogre, 5); // push southeast
            Assert.Equal(7, ground.LocateCombattant2D(ogre).X);
            Assert.Equal(6, ground.LocateCombattant2D(ogre).Y);
            ground.Push(new Coord(9, 2), ogre, 5); // push northwest
            Assert.Equal(6, ground.LocateCombattant2D(ogre).X);
            Assert.Equal(7, ground.LocateCombattant2D(ogre).Y);
            ground.Push(new Coord(9, 9), ogre, 5); // push southwest
            Assert.Equal(5, ground.LocateCombattant2D(ogre).X);
            Assert.Equal(6, ground.LocateCombattant2D(ogre).Y);
        }

        [Fact]
        public void PushTestClassic() {
            BattleGroundClassic classic = new BattleGroundClassic();
            Combattant ogre = Monsters.Ogre;
            Combattant goblin = Monsters.Goblin;
            classic.AddCombattant(ogre, ClassicLocation.Row.FRONT_LEFT);
            classic.AddCombattant(goblin, ClassicLocation.Row.FRONT_RIGHT);
            classic.Push(classic.LocateClassicCombattant(ogre), goblin, 10);
            classic.Push(classic.LocateClassicCombattant(goblin), ogre, 10);
            Assert.Equal(ClassicLocation.Row.BACK_LEFT, classic.LocateClassicCombattant(ogre).Location);
            Assert.Equal(ClassicLocation.Row.BACK_RIGHT, classic.LocateClassicCombattant(goblin).Location);
        }
    }
}