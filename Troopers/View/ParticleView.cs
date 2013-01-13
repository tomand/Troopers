using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Troopers.View
{
    class ParticleView : GameObjectView
    {
         private SplitterSystem _splitterSystem;
         public ParticleView(SplitterSystem splitterSystem, Camera cam)
            : base( cam)
        {
            _splitterSystem = splitterSystem;       
        }

        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (SplitterParticle particle in _splitterSystem.ParticlesToDraw)
            {
                DrawParticle(spriteBatch, particle);    
            }

        }

        private void DrawParticle(SpriteBatch spriteBatch, SplitterParticle particle)
        {
            Color color = new Color(particle.AlphaValue, particle.AlphaValue, particle.AlphaValue, particle.AlphaValue);
           
            DestinationRectangle =
            new Rectangle(Camera.TransformX(particle.Position.X - particle.Radius)
                , (int)Camera.TransformY(particle.Position.Y - particle.Radius)
                , Camera.TransformSizeX(particle.Width)
                , Camera.TransformSizeY(particle.Height));

            spriteBatch.Draw(GameObjectTexture, DestinationRectangle, color);
        }

        

        public SplitterSystem SplitterSystem { set { _splitterSystem = value; } }

       
    }
}
