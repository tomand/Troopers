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
    public class MasterController : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private const int ViewportHeight = 640;
        private const int ViewportWidth = 800;

        LevelController _levelController;
        private MainMenuController _mainMenuController;
        private PauseMenuController _pauseMenuController;
        private GameOverMenuController _gameOverMenuController;
        private HelpController _helpController;
        private ControllerBase _lastController;

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
            _mainMenuController.IsActive = false;
            _pauseMenuController = new PauseMenuController(ViewportWidth, ViewportHeight, GraphicsDevice, Content);
            _pauseMenuController.IsActive = false;
            _gameOverMenuController = new GameOverMenuController(ViewportWidth, ViewportHeight, GraphicsDevice, Content);
            _gameOverMenuController.IsActive = false;
            _helpController = new HelpController(ViewportWidth, ViewportHeight, GraphicsDevice, Content);
            _helpController.IsActive = true;

            _lastController = _mainMenuController;
            _mainMenuController.StartGame += StartGame;
            _mainMenuController.ExitGame += (sender, args) => { this.Exit(); };
            _mainMenuController.ShowHelp += (sender, args) => ShowHelp(sender);

            _pauseMenuController.ExitGame += (sender, args) => { this.Exit(); };
            _pauseMenuController.RestartGame += StartGame;
            _pauseMenuController.ResumeGame += ResumeGame;
            _pauseMenuController.ShowHelp += (sender, args) => ShowHelp(sender);
            _pauseMenuController.MainMenuActivated += (sender, args) => ShowMainMenu((ControllerBase) sender);

            _gameOverMenuController.ExitGame += (sender, args) => { this.Exit(); };
            _gameOverMenuController.RestartGame += RestartGame;
            _gameOverMenuController.ContinueGame += ContinueGame;
            _gameOverMenuController.ShowHelp += (sender, args) => ShowHelp(sender);
            _gameOverMenuController.MainMenuActivated += (sender, args) => ShowMainMenu((ControllerBase)sender);
            
            _levelController.PauseGame += (sender, args) => ShowPauseMenu();
            _levelController.LevelFinished += (sender, args) => FinishLevel(args);

            _helpController.GoBack += (sender, eventArgs) => GoBackFromHelp();

        }

        private void ShowMainMenu(ControllerBase sender)
        {
            sender.IsActive = false;
            _lastController = sender;
            _mainMenuController.IsActive = true;
        }

        private void GoBackFromHelp()
        {
            _helpController.IsActive = false;
            _lastController.IsActive = true;
        }

        private void RestartGame(object sender, EventArgs e)
        {
            _mainMenuController.IsActive = false;
            _pauseMenuController.IsActive = false;
            _gameOverMenuController.IsActive = false;
            _levelController.IsActive = true;
            _levelController.StartLevel();
        }

        private void ContinueGame(object sender, EventArgs e)
        {
            _mainMenuController.IsActive = false;
            _pauseMenuController.IsActive = false;
            _gameOverMenuController.IsActive = false;
            _levelController.IsActive = true;
            _levelController.GotoNextLevel();
            _levelController.StartLevel();
        }


        private void ResumeGame(object sender, EventArgs e)
        {
            _mainMenuController.IsActive = false;
            _pauseMenuController.IsActive = false;
            _gameOverMenuController.IsActive = false;
            _levelController.IsActive = true;
        }

        private void FinishLevel(EventArgs args)
        {
            _gameOverMenuController.PlayerWon = _levelController.PlayerWon;
            _gameOverMenuController.IsActive = true;
            _mainMenuController.IsActive = false;
            _pauseMenuController.IsActive = false;
            _levelController.IsActive = false;
        }

       

        private void ShowPauseMenu()
        {
            _pauseMenuController.IsActive = true;
            _gameOverMenuController.IsActive = false;
            _mainMenuController.IsActive = false;
            _levelController.IsActive = false;
        }


        private void StartGame(object sender, EventArgs eventArgs)
        {
            _mainMenuController.IsActive = false;
            _pauseMenuController.IsActive = false;
            _gameOverMenuController.IsActive = false;

            _levelController.IsActive = true;
            var levelNumber = (LevelNumber) eventArgs;
            
            _levelController.StartLevel(levelNumber.Number);
        }

        private void ShowHelp(object sender)
        {
            var sourceController = (ControllerBase) sender;
            sourceController.IsActive = false;
            _lastController = sourceController;
            _helpController.IsActive = true;
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
            _pauseMenuController.LoadContent();
            _gameOverMenuController.LoadContent();
            _helpController.LoadContent();
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

            else if (_mainMenuController.IsActive)
                _mainMenuController.Update(gameTime);

            else if (_pauseMenuController.IsActive)
                _pauseMenuController.Update(gameTime);

            else if (_gameOverMenuController.IsActive)
                _gameOverMenuController.Update(gameTime);

            else if (_helpController.IsActive)
                _helpController.Update(gameTime);

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

            else if (_mainMenuController.IsActive)
                _mainMenuController.Draw(_spriteBatch, gameTime);

            else if (_pauseMenuController.IsActive)
                _pauseMenuController.Draw(_spriteBatch, gameTime);

            else if (_gameOverMenuController.IsActive)
                _gameOverMenuController.Draw(_spriteBatch, gameTime);

            else if (_helpController.IsActive)
                _helpController.Draw(_spriteBatch, gameTime);
            
            _spriteBatch.End();
            
            base.Draw(gameTime);

        }
    }
}
