using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.GameScreens;

namespace YoukaiKingdom
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch;
        GameState _gameStateScreen;
        public InventoryScreen InventoryScreen;
        public StartMenuScreen StartMenuScreen;
        public GamePlayScreen GamePlayScreen;
        public CharacterCreationScreen CharacterCreationScreen;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            _gameStateScreen = GameState.GameScreenState;
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
            GamePlayScreen = new GamePlayScreen(this);
            //...etc

            switch (_gameStateScreen)
            {
                case (GameState.GameScreenState):
                    {
                        this.Components.Add(GamePlayScreen);
                        //GamePlayScreen.Enabled = true;
                        GamePlayScreen.Initialize();
                        break;
                    }
            }
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
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (_gameStateScreen)
            {
                case (GameState.GameScreenState):
                    {
                        GamePlayScreen.Draw(gameTime);
                        break;
                    }
            }
        }
    }
}
