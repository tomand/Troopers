using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Troopers.View
{
    internal class TrooperHitView
    {

        private string _damage;
        private Vector2 _position;
        private Camera _camera;
        private SpriteFont _font;
        private float _timeLived;
        private float _maxLifeTime = 1f;

        public bool IsAlive { get { return _timeLived <= _maxLifeTime; } }

        public TrooperHitView(Camera camera, string damage, Vector2 position, SpriteFont font, GameTime gameTime)
        {
            
            this._camera = camera;
            this._damage = damage;
            this._position = position;
            this._font = font;
            _timeLived = 0f;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _timeLived += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Color color = new Color(Color.Red.R, Color.Red.G,Color.Red.B, ((_timeLived/_maxLifeTime) -1) * -1);
            spriteBatch.DrawString(_font, _damage, _camera.Transform(_position), color);
        }

        
    }
}