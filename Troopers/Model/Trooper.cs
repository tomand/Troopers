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
        protected int _time;
        private bool _current;
        protected bool _isControlledByComputer = false;
        private Weapon _weapon;
        private int _health;
        private int _initialHealth;

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
        public bool IsAlive { get { return _health > 0; } }

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
           _time = 12;
        }


        public Trooper(Vector2 startPosition,float faceDirection ,float width, float height, int speed, int health = 40)
        {
            Position = startPosition;
            TargetPosition = startPosition;
            Width = width;
            Height = height;
            FaceDirection =  faceDirection;
            Speed = speed;
            _health = health;
            _initialHealth = _health;
            Weapon = new Weapon();
            InitiateNewTurn();
        }


        public virtual void Update(GameTime gameTime, IEnumerable<Trooper> troopers )
        {
        }

        public void Update(GameTime gameTime, Vector2 cursorCenterPosition, Vector2 cursorPosition, bool mouseClicked, bool enemyIsMarked)
        {
            if (!enemyIsMarked && mouseClicked && GetDistanceSquared(cursorPosition) <= _time * _time)
            {
                FacePosition(cursorCenterPosition);
                TargetPosition = cursorPosition;
                _direction = TargetPosition - Position;
                _time = _time - (int)Math.Ceiling( Math.Sqrt(GetDistanceSquared(cursorPosition)));
            }

            if (enemyIsMarked && mouseClicked && _time >= _weapon.TimeToShoot)
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
         
        }

        
        private void Shoot()
        {
            _time = _time - Weapon.TimeToShoot;
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
            get { return _time == 0 && (Position == TargetPosition) && !_weapon.IsShooting; }
            
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
            get { return (float)_health / (float)_initialHealth; }
            
        }

        public void EndTurn()
        {
            _time = 0;
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

            if (squaredDistance > _time * _time)
                return Distance.Far;
            else if ((squaredDistance > (_time - Weapon.TimeToShoot) * (_time - Weapon.TimeToShoot)) || (_time < Weapon.TimeToShoot))
                return Distance.Medium;

            return Distance.Close;
        }


        public void Hit(int damage)
        {
            _health = _health - damage;
        }

        public void Heal()
        {
            _health = Math.Min(_health + (_initialHealth / 2), _initialHealth);
        }
    }
}
