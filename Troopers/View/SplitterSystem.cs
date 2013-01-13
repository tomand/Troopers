using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Troopers.View
{
    class SplitterSystem
    {
        private SplitterParticle[] particles;
        private float maxSpeed = 0.7f;

        Random rand;
        public SplitterSystem(Vector2 startPosition, float particleRadius)
        {
            rand = new Random();

            particles = new SplitterParticle[100];

            for (int i = 0; i < 100; i++)
            {
                particles[i] = new SplitterParticle(startPosition, GetRandomDirection(), particleRadius);
            }
        }

        private Vector2 GetRandomDirection()
        {
            Vector2 randomDirection = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
            //normalize to get it spherical vector with length 1.0
            randomDirection.Normalize();
            //add random length between 0 to maxSpeed
            randomDirection = randomDirection * ((float)rand.NextDouble() * maxSpeed);
            return randomDirection;
        }

        internal void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (SplitterParticle particle in particles)
            {
                UpdateParticle(elapsedTime, particle);
            }

        }

        private void UpdateParticle(float elapsedTime, SplitterParticle particle)
        {
            if (!particle.IsAlive)
            {
                return;
            }

            particle.Update(elapsedTime);
        }



        public IEnumerable<SplitterParticle> ParticlesToDraw
        {
            get
            {
                return particles.Where(p => p.IsAlive);
            }
        }

        public bool IsAlive
        {
            get
            {
                for (int i = 0; i < particles.Length; i++)
                {
                    if (particles[i].IsAlive)
                        return true;
                }
                return false;
            }
        }
    }
}