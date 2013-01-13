using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Troopers.View
{
    public class KilledTrooperView : GameObjectView
    {
        private SplitterSystem _splitterSystem;
        private ParticleView _particleView;
        public bool IsAlive { get; set; }

        public KilledTrooperView(Camera camera)
        :base(camera)
        {
            
        }


        public void Play(Vector2 position)
        {
            IsAlive = true;
            
            _splitterSystem = new SplitterSystem(position, 0.05f);
            _particleView = new ParticleView(_splitterSystem, Camera);
            _particleView.GameObjectTexture = GameObjectTexture;
        }

        public void Update(GameTime gameTime)
        {
            _splitterSystem.Update(gameTime);
         
            IsAlive = (_splitterSystem.IsAlive);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            _particleView.Draw(gameTime, spriteBatch);
            

        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("blood");

        }
    }
}