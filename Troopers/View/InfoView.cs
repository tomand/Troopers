using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Troopers.Model;

namespace Troopers.View
{
    internal class InfoView : GameObjectView
    {
        private Camera camera;
        private Model.Info _trooperInfo;
        private SpriteFont _font
;

        public InfoView(Camera camera, Info _trooperInfo)
        :base(camera)
        {
          
            this._trooperInfo = _trooperInfo;
        }

        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("TrooperInfoFont");
        }

        internal void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _trooperInfo.GetTrooperInfo(), Camera.Transform(_trooperInfo.Position), Color.White);
        }
    }
}