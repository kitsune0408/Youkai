namespace YoukaiKingdom
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using YoukaiKingdom.GameScreens;
    using YoukaiKingdom.Helpers;
    using YoukaiKingdom.Logic;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch { get; set; }
        public GameState GameStateScreen { get; set; }
        public InventoryScreen InventoryScreen { get; set; }
        public StartMenuScreen StartMenuScreen { get; set; }
        public GamePlayScreen GamePlayScreen { get; set; }
        public CharacterCreationScreen CharacterCreationScreen { get; set; }
        public PauseMenuScreen PauseMenuScreen { get; set; }
        public GameOverScreen GameOverScreen { get; set; }
        public CharacterType HeroType { get; set; }

        public GameEngine Engine { get; set; }

        public MainGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.GameStateScreen = GameState.StartMenuScreenState;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);

            //GAME SCREENS
            this.StartMenuScreen = new StartMenuScreen(this);
            this.GameOverScreen = new GameOverScreen(this);

            //...etc
            this.Components.Add(this.StartMenuScreen);
            this.StartMenuScreen.Initialize();
            this.Components.Add(this.GameOverScreen);
            this.GameOverScreen.Initialize();
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
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (this.GameStateScreen)
            {
                case (GameState.GameScreenState):
                    {
                        if (this.GamePlayScreen.IsGuideVisible)
                        {
                            this.IsMouseVisible = true;
                        }
                        else
                        {
                            this.IsMouseVisible = false;
                        }
                        this.GamePlayScreen.Draw(gameTime);
                        break;
                    }
                case (GameState.StartMenuScreenState):
                    {
                        this.IsMouseVisible = true;
                        this.StartMenuScreen.Draw(gameTime);
                        break;
                    }
                case (GameState.CharacterSelectionScreenState):
                    {
                        this.IsMouseVisible = true;
                        this.CharacterCreationScreen.Draw(gameTime);
                        break;
                    }
                case (GameState.PauseScreenState):
                    {
                        this.IsMouseVisible = true;
                        this.PauseMenuScreen.Draw(gameTime);
                        break;
                    }
                case (GameState.InventoryScreenState):
                    {
                        this.IsMouseVisible = true;
                        this.InventoryScreen.Draw(gameTime);
                        break;
                    }
                case (GameState.GameOverState):
                    {
                        this.IsMouseVisible = true;
                        this.Components.Remove(this.InventoryScreen);
                        this.Components.Remove(this.GamePlayScreen);
                        this.Components.Remove(this.CharacterCreationScreen);
                        this.GameOverScreen.Draw(gameTime);
                        break;
                    }
            }
        }
    }
}
