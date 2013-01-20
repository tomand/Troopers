using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Troopers.Model;

namespace Troopers.View
{
    internal class TrooperInfoView : GameObjectView
    {
        private Camera camera;
        private Model.TrooperInfo _trooperInfo;
        private SpriteFont _font
;

        public TrooperInfoView(Camera camera, TrooperInfo _trooperInfo)
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