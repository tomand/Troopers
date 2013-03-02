using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    public class Trooper
    {
        private Vector2 _position;
        Vector2 _direction;
        public int Time { get; private set; }
        private bool _current;
        protected bool _isControlledByComputer = false;
        private Weapon _weapon;
        private int _initialHealth;
        private int _lastHealth;
        protected float _timeSinceLastAction = 1;
        public int NumberOfBullets { get; protected set; }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        } 
        //public Vector2 Position { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }
        public Vector2 Velocity { get; set; }
        public float FaceDirection { get; set; }
        public Vector2 TargetPosition { get; set; }
        public int Speed { get; set; }
        public Trooper ShootingTarget { get; set; }
        public bool IsAlive { get { return Health > 0; } }
        public int LifeChange { get { return Health - _lastHealth; } }

        public bool Current
        {
            get { return _current; } 
            set 
            { 
                if (value) 
                    InitiateNewTurn();
                _current = value;
        } }

        private void InitiateNewTurn()
        {
           Time = 12;
        }


        public Trooper(Vector2 startPosition,float faceDirection ,float width, float height, int speed, int health = 30)
        {
            Position = startPosition;
            TargetPosition = startPosition;
            Width = width;
            Height = height;
            FaceDirection =  faceDirection;
            Speed = speed;
            Health = health;
            _initialHealth = Health;
            Weapon = new Weapon();
            NumberOfBullets = 20;
            InitiateNewTurn();
        }


        public virtual void Update(GameTime gameTime, IEnumerable<Trooper> troopers, IEnumerable<Building> buildings )
        {
        }

        public void Update(GameTime gameTime, Vector2 cursorCenterPosition, Vector2 cursorPosition, bool mouseClicked, bool enemyIsMarked)
        {
            if (!enemyIsMarked && mouseClicked && GetDistanceSquared(cursorPosition) <= Time * Time)
            {
                FacePosition(cursorCenterPosition);
                TargetPosition = cursorPosition;
                _direction = TargetPosition - Position;
                Time = Time - (int)Math.Ceiling( Math.Sqrt(GetDistanceSquared(cursorPosition)));
            }

            if (enemyIsMarked && mouseClicked && Time >= _weapon.TimeToShoot && _timeSinceLastAction > 0.5f && NumberOfBullets > 0)
            {
                FacePosition(cursorCenterPosition);
                Shoot();
                
            }

            if (!TargetIsReached())
            {
                UpdatePosition(gameTime);
            }
            else
            {
                Position = TargetPosition;
            }

            Weapon.Update(gameTime);
            _lastHealth = Health;

        }

        
        private void Shoot()
        {
            Time = Time - Weapon.TimeToShoot;
            NumberOfBullets--;
            Weapon.Fire(FaceDirection, CenterPosition, ShootingTarget);

        }

        protected float GetDistanceSquared(Vector2 distantPosition)
        {
            if (_position.Equals(distantPosition))
            {
                return 0;
            }

           return Math.Max(1, Vector2.DistanceSquared(Position, distantPosition));
        }

        private bool TargetIsReached()
        {
            return (Math.Abs(Position.X - TargetPosition.X) < 0.2 && Math.Abs(Position.Y - TargetPosition.Y) < 0.2);
          //  return Position.Equals(TargetPosition);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            _direction.Normalize();
            _position += _direction * 3.2f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public Vector2 CenterPosition
        {
            get { return new Vector2(Position.X + Width / 2, Position.Y + Height / 2); }
        }

        public bool HasNoTimeLeft
        {
            get { return Time == 0 && (Position == TargetPosition) && !_weapon.IsShooting; }
            
        }

        public  bool IsControlledByComputer
        {
            get { return _isControlledByComputer; }          
        }

        public Weapon Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }

        public float HealthPercent
        {
            get { return (float)Health / (float)_initialHealth; }
            
        }

        public int Health { get; private set; }

        public void EndTurn()
        {
            Time = 0;
        }



        public void FacePosition(Vector2 positionToFace)
        {
          //  positionToFace = new Vector2(1f, 26.5f);
            Vector2 direction = positionToFace - CenterPosition;
            FaceDirection = (float)(Math.Atan2(direction.Y, direction.X) + (Math.PI/2) );
        }

        public Distance GetDistanceGrade(Vector2 distantPosition)
        {
            int squaredDistance = (int) Math.Ceiling( GetDistanceSquared(distantPosition));

            if (squaredDistance > Time * Time)
                return Distance.Far;
            else if ((squaredDistance > (Time - Weapon.TimeToShoot) * (Time - Weapon.TimeToShoot)) || (Time < Weapon.TimeToShoot))
                return Distance.Medium;

            return Distance.Close;
        }


        public void Hit(int damage)
        {
            _lastHealth = Health;
            Health = Health - damage;
        }

        public void Heal()
        {
            _lastHealth = Health;
            Health = Math.Min(Health + (_initialHealth / 2), _initialHealth);
        }

        public void AddAmmo(int numberOfBullets)
        {
            NumberOfBullets += numberOfBullets;
        }

        public void ResetLifeChange()
        {
            _lastHealth = Health;
        }

        public bool IsMoving { get { return !TargetIsReached(); } }
    }
}
