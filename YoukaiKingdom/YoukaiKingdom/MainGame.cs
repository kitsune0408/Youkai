using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.GameScreens;

namespace YoukaiKingdom
{
    using YoukaiKingdom.Logic;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch;
        public GameState gameStateScreen;
        public InventoryScreen InventoryScreen;
        public StartMenuScreen StartMenuScreen;
        public GamePlayScreen GamePlayScreen;
        public CharacterCreationScreen CharacterCreationScreen;
        public PauseMenuScreen PauseMenuScreen;
        public GameOverScreen GameOverScreen;
        public Hero Hero;
        public CharacterType heroType;

        private GameEngine engine;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.engine = new GameEngine(new Monk("CoolMonk"));
            this.engine.Start();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameStateScreen = GameState.StartMenuScreenState;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            //GAME SCREENS
            StartMenuScreen = new StartMenuScreen(this);
            GameOverScreen = new GameOverScreen(this);
            
            //...etc
            this.Components.Add(StartMenuScreen);
            StartMenuScreen.Initialize();            
            this.Components.Add(GameOverScreen);
            GameOverScreen.Initialize();
          
            //this.Components.Add(GamePlayScreen);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (gameStateScreen)
            {
                case (GameState.GameScreenState):
                    {

                        this.IsMouseVisible = false;
                        GamePlayScreen.Draw(gameTime);
                        break;
                    }
                case (GameState.StartMenuScreenState):
                {
                        this.IsMouseVisible = true;
                        StartMenuScreen.Draw(gameTime);
                        break;
                    }
                case (GameState.CharacterSelectionScreenState):
                    {
                        this.IsMouseVisible = true;
                        CharacterCreationScreen.Draw(gameTime);
                        break;
                    }
                case (GameState.PauseScreenState):
                    {
                        this.IsMouseVisible = true;
                        PauseMenuScreen.Draw(gameTime);
                        break;
                    }
                case (GameState.InventoryScreenState):
                    {
                        this.IsMouseVisible = true;
                        InventoryScreen.Draw(gameTime);       
                        break;
                    }
                case (GameState.GameOverState):
                    {
                        this.IsMouseVisible = true;
                        this.Components.Remove(InventoryScreen);
                        this.Components.Remove(GamePlayScreen);
                        this.Components.Remove(CharacterCreationScreen);
                        GameOverScreen.Draw(gameTime);
                        break;
                    }
            }
        }
    }
}
