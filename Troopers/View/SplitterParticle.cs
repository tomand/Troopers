using System;
using Microsoft.Xna.Framework;

namespace Troopers.View
{
    class SplitterParticle
    {
        private float _timeLived = 0;
        private float _maxLifeTime = 3f;
       
        
        public static float DELAY_MAX = 1.0f;

        public float LifePercent { get; set; }

        private Vector2 _acceleration;
        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }

        public float Radius { get; set; }

        public float Height { get { return Radius * 2; } }

        public float Width { get { return Radius * 2; } }

        public bool IsAlive { get { return _timeLived <= _maxLifeTime; } }
        public float AlphaValue { get { return (LifePercent - 1) * -1; } }

        public SplitterParticle(Vector2 position, Vector2 direction, float radius)
        {
            
            this.Position = position;
            this.Radius = radius;
            this.Velocity = direction;
            _acceleration = new Vector2(0f, 0f);
        }

        public void Update(float elapsedTime)
        {
            _timeLived += elapsedTime;
            LifePercent = _timeLived / _maxLifeTime;
            Vector2 newVelocity = UpdateParticleVelocity(elapsedTime);

            UpdateParticlePosition(elapsedTime, newVelocity);

        }

        private Vector2 UpdateParticleVelocity(float elapsedTime)
        {

            Vector2 newVelocity = new Vector2();

            newVelocity.X = elapsedTime * _acceleration.X + Velocity.X;
            newVelocity.Y = elapsedTime * _acceleration.Y + Velocity.Y;

            Velocity = newVelocity;
            return newVelocity;
        }

        private void UpdateParticlePosition(float elapsedTime, Vector2 newVelocity)
        {
            Vector2 newPosition = new Vector2();
            newPosition.X = elapsedTime * newVelocity.X + Position.X;
            newPosition.Y = elapsedTime * newVelocity.Y + Position.Y;

            Position = newPosition;

        }





    }
}