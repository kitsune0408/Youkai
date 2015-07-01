using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;

namespace YoukaiKingdom.GameScreens
{
    public class GameOverScreen: BaseGameScreen
    {
        //to check if mouse has been pressed already
        private MouseState lastMouseState;
        private MouseState mouse;
        private Button goBackButton;
        private Button exitButton;
        //background
        private Background mBackground;
        //button textures
        private Texture2D goBackButtonTexture;
        private Texture2D goBackButtonTextureHover;
        private Texture2D gameOverTexture;
        private Texture2D exitTextureRegular;
        private Texture2D exitTextureHover;

        public GameOverScreen(MainGame mGame) : base(mGame)
        {
        }

        protected override void LoadContent()
        {
            mBackground = new Background(1);
            var mainMenuBackground = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/MainMenuBackground");
            gameOverTexture = MGame.Content.Load<Texture2D>("Sprites/UI/UI_gameover");

            goBackButtonTexture = MGame.Content.Load<Texture2D>("Sprites/UI/UI_BackToMainMenu");
            goBackButtonTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/UI_BackToMainMenu_hover");
            goBackButton = new Button(goBackButtonTexture, goBackButtonTextureHover, this.MGame.GraphicsDevice);
            goBackButton.SetPosition(new Vector2(MGame.GraphicsDevice.Viewport.Width / 2
                - goBackButtonTexture.Width / 2, 250)); 
            exitTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_ExitButtonRegular");
            exitTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_ExitButtonHover");
            exitButton = new Button(exitTextureRegular, exitTextureHover, this.MGame.GraphicsDevice);
            exitButton.SetPosition(new Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - exitTextureRegular.Width / 2, 300));
            mBackground.Load(MGame.GraphicsDevice, mainMenuBackground);

        }

        public override void Update(GameTime gameTime)
        {
            if (MGame.GameStateScreen == GameState.GameOverState)
            {
                KeyboardState state = Keyboard.GetState();
                mouse = Mouse.GetState();
                goBackButton.Update(state, mouse, 0, 0);
                exitButton.Update(state, mouse, 0, 0);

                if (goBackButton.IsClicked)
                {
                    if (mouse.LeftButton == ButtonState.Pressed &&
                        lastMouseState.LeftButton == ButtonState.Released)
                    {
                        MGame.GameStateScreen = GameState.StartMenuScreenState;
                    }
                }
                if (exitButton.IsClicked)
                {
                    MGame.Exit();
                }
                lastMouseState = mouse; 
            }
            
        }
        public override void Draw(GameTime gameTime)
        {
            MGame.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null);
            mBackground.Draw(MGame.SpriteBatch);
            goBackButton.Draw(MGame.SpriteBatch);
            exitButton.Draw(MGame.SpriteBatch);
            MGame.SpriteBatch.Draw(gameOverTexture,
                            new Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - gameOverTexture.Width / 2, 150),
                           Color.White);
            MGame.SpriteBatch.End();
        }

    }
}
