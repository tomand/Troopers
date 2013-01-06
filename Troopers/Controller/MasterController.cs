using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Troopers.Controller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MasterController : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private const int ViewportHeight = 640;
        private const int ViewportWidth = 800;

        LevelController _levelController;
        private MainMenuController _mainMenuController;

        public MasterController()
        {
            IsMouseVisible = true;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = ViewportHeight;
            _graphics.PreferredBackBufferWidth = ViewportWidth;
            _levelController = new LevelController(ViewportWidth, ViewportHeight, GraphicsDevice, Content);
            
            _levelController.IsActive = false;
            _mainMenuController = new MainMenuController(ViewportWidth, ViewportHeight, GraphicsDevice, Content);
            _mainMenuController.IsActive = true;
           
            _mainMenuController.StartGame += MainMenuControllerOnStartGame;
            _mainMenuController.ExitGame += (sender, args) => { this.Exit(); };
            _levelController.PauseGame += (sender, args) => ShowPauseMenu();
            _levelController.LevelFinished += (sender, args) => FinishLevel();
      

        }

        private void FinishLevel()
        {
            _mainMenuController.IsActive = true;
            _levelController.IsActive = false;
        }

        private void ShowPauseMenu()
        {
            _mainMenuController.IsActive = true;
            _levelController.IsActive = false;
        }


        private void MainMenuControllerOnStartGame(object sender, EventArgs eventArgs)
        {
            _mainMenuController.IsActive = false;
            _levelController.IsActive = true;
            _levelController.StartLevel();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _levelController.LoadConent();
            _mainMenuController.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (_levelController.IsActive)
                _levelController.Update(gameTime);

            if (_mainMenuController.IsActive)
                _mainMenuController.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           

            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            if (_levelController.IsActive)
                _levelController.Draw(_spriteBatch, gameTime);

            if (_mainMenuController.IsActive)
                _mainMenuController.Draw(_spriteBatch, gameTime);
            
            _spriteBatch.End();
            
            base.Draw(gameTime);

        }
    }
}
