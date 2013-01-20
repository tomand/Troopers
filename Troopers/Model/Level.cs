using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class Level
    {
        private int _width;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        private int _height;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Cursor Cursor
        {
            get { return _cursor; }
        }

        public bool IsFinished
        {
            get { return _troopers.Count(t => t.IsControlledByComputer && t.IsAlive) == 0 || _troopers.Count(t => !t.IsControlledByComputer && t.IsAlive) == 0; }
          
        }

        public bool PlayerWon
        {
            get { return IsFinished && _troopers.Count(t => !t.IsControlledByComputer && t.IsAlive) > 0; }
        }

        public bool Current { get; set; }
        List<Trooper> _troopers;
        private  Cursor _cursor;
        private readonly Random _random;
        private int _nextActiveTrooper = 1;
        private string _levelId;
        private List<MediKit> _mediKits;
        private List<Building> _buildings;

        public Level(int width, int height, Vector2 position, string levelId)
        {
            _random = new Random();
            _position = position;
            _width = width;
            _height = height;
            _cursor = new Cursor(new Vector2(0, 0), 1f);
            _levelId = levelId;
         
        }

        internal IEnumerable<Trooper> GetTroopers()
        {
            return _troopers.Where(t => t.IsAlive);
        }


        public void Update(GameTime gameTime, Vector2 mousePosition, bool mouseClicked, bool tabIsClicked)
        {
           Trooper trooper = GetCurrentTrooper();
            if (tabIsClicked)
            {
                trooper.EndTurn();
            }
            
            _cursor.UpdatePosition(mousePosition, Width, Height);
            bool buildingIsBetweenCursorAndCurrentTrooper = IsBuildingBetweenCursorAndCurrentTrooper();
            _cursor.MarksEnemyTrooper = IsComputerControlledTrooperOnPosition(_cursor.Position);
            _cursor.BlockedByBuilding = IsHouseOnPosition(_cursor.Position) || buildingIsBetweenCursorAndCurrentTrooper;
          //  _cursor.BlockedByBuilding =  buildingIsBetweenCursorAndCurrentTrooper;
            if (trooper.IsControlledByComputer)
            {
                trooper.Update(gameTime, GetPlayerControlledTroopers(), GetBuildings());
            }
            else
            {
                trooper.Update(gameTime, _cursor.CenterPosition, _cursor.Position, !_cursor.BlockedByBuilding && !buildingIsBetweenCursorAndCurrentTrooper && mouseClicked && GetPlayerControlledTroopers().Count(t => t.Position.Equals(_cursor.Position)) == 0, _cursor.MarksEnemyTrooper && !buildingIsBetweenCursorAndCurrentTrooper);
                if (_cursor.MarksEnemyTrooper)
                {
                    trooper.ShootingTarget = GetTrooperOnPosition(_cursor.Position);
                }
                if (_mediKits.Count(m => m.Position.Equals(trooper.Position) && !m.IsTaken) > 0)
                {
                    _mediKits.Find(m => m.Position.Equals(trooper.Position)).IsTaken = true;
                    trooper.Heal();
                }
            }
            

            _cursor.DistanceGrade = trooper.GetDistanceGrade(_cursor.Position);

            if (trooper.HasNoTimeLeft) 
                UpdateWhoIsCurrent(trooper);
        }

        private bool IsBuildingBetweenCursorAndCurrentTrooper()
        {
            foreach (Building building in _buildings)
            {
                if (building.IsBetweenPosition(_cursor.CenterPosition, GetCurrentTrooper().CenterPosition))
                {
                    return true;
                }
                
            }
            return false;
        }

        private bool IsHouseOnPosition(Vector2 position)
        {
            foreach (Building building in _buildings)
            {
                if (building.CoversPosition(position))
                {
                    return true;
                }
            }
            return false;
        }

        private Trooper GetTrooperOnPosition(Vector2 position)
        {
            return _troopers.Find(t => t.Position.Equals(position) && t.IsAlive);
        }

        private IEnumerable<Trooper> GetPlayerControlledTroopers()
        {
            return _troopers.Where(t => !t.IsControlledByComputer && t.IsAlive);
        }

        private bool IsComputerControlledTrooperOnPosition(Vector2 position)
        {
            return _troopers.Exists(t => t.Position.Equals(position) && t.IsAlive && t.IsControlledByComputer);
        }

        private void UpdateWhoIsCurrent(Trooper trooper)
        {
            for (int i = 0; i < _troopers.Count(t => t.IsAlive); i++)
            {
                if (_troopers.Where(t => t.IsAlive).OrderByDescending(t => t.Speed).ElementAt(i).Current)
                {
                    _troopers.Where(t => t.IsAlive).OrderByDescending(t => t.Speed).ElementAt(i).Current = false;
                    _troopers.Where(t => t.IsAlive).OrderByDescending(t => t.Speed).ElementAt(i + 1 == _troopers.Count(t => t.IsAlive)? 0: i+1).Current = true;
                    return;
                }
            }
            
            //trooper.Current = false;
            //GetNextTrooper().Current = true;
            //SetNextActiveTrooper();
        }

        internal Trooper GetCurrentTrooper()
        {
            return _troopers.Find(t => t.Current);
        }

       

        public void Start()
        {
           _troopers = new List<Trooper>();
            _mediKits = new List<MediKit>();
            _buildings = new List<Building>();
            LoadLevelData();

            _troopers.Where(t => t.IsAlive).OrderByDescending(t => t.Speed).ElementAt(0).Current = true;
        }

        private void LoadLevelData()
        {
            switch (_levelId)
            {
                case "level1":
                    {
                        _troopers.Add(new Trooper(new Vector2(1f, 28f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(GetComputerControlledTrooper(new Vector2(28f, 1f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _buildings.Add(new Building(new Vector2(15, 15), 3f, 3f));
                        break;
                    }
                case "level2":
                    {

                        _troopers.Add(new Trooper(new Vector2(1f, 28f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(new Trooper(new Vector2(9f, 20f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(new Trooper(new Vector2(1f, 20f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(GetComputerControlledTrooper(new Vector2(28f, 1f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(GetComputerControlledTrooper(new Vector2(20f, 1f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(GetComputerControlledTrooper(new Vector2(28f, 9f), 90f, 1f, 1f, _random.Next(100, 200)));
                        
                        break;
                    }
                case "level3":
                    {
                        _troopers.Add(new Trooper(new Vector2(1f, 28f), 90f, 1f, 1f, _random.Next(100, 200), 80));
                        _troopers.Add(GetComputerControlledTrooper(new Vector2(28f, 1f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(GetComputerControlledTrooper(new Vector2(20f, 1f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(GetComputerControlledTrooper(new Vector2(28f, 9f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _mediKits.Add(new MediKit(new Vector2(5f, 20f)));
                        _mediKits.Add(new MediKit(new Vector2(7f, 23f)));
                        _mediKits.Add(new MediKit(new Vector2(19f, 12f)));
                        _mediKits.Add(new MediKit(new Vector2(10f, 10f)));
                        _mediKits.Add(new MediKit(new Vector2(20f, 5f)));
                        _mediKits.Add(new MediKit(new Vector2(15f, 25f)));
                        _mediKits.Add(new MediKit(new Vector2(18f, 27f)));
                        _mediKits.Add(new MediKit(new Vector2(15f, 15f)));
                        
                        break;
                    }
            }
            
        }

        private ComputerControlledTrooper GetComputerControlledTrooper(Vector2 startPosition, float faceDirection, float width, float height, int speed)
        {
            return new ComputerControlledTrooper(startPosition, faceDirection, width, height, speed, GetAllPositions());
        }

        private List<Vector2> GetAllPositions()
        {
            List<Vector2> positions = new List<Vector2>();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    positions.Add(new Vector2(i,j));
                }
            }
            return positions;
        }

        public IEnumerable<MediKit> GetMediKits()
        {
            return _mediKits.Where(m => !m.IsTaken);
        }

        public IEnumerable<Trooper> GetDeadTroopers()
        {
            List<Trooper> deadTroopers = _troopers.Where(t => !t.IsAlive).ToList();
            _troopers.RemoveAll(t => !t.IsAlive);
            return deadTroopers;
        }

        public IEnumerable<Building> GetBuildings()
        {
            return _buildings;
        }
    }
}
