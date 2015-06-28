using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;

namespace YoukaiKingdom.GameScreens
{
    public class StartMenuScreen: BaseGameScreen
    {
        Button startButton;
        Button loadButton;
        Button exitButton;
        //background
        Background mBackground;
        //button textures
        private Texture2D startTextureRegular;
        private Texture2D startTextureHover;
        private Texture2D loadTextureRegular;
        private Texture2D loadTextureHover;
        private Texture2D exitTextureRegular;
        private Texture2D exitTextureHover;
        

        public StartMenuScreen(MainGame mGame) : base(mGame)
        {
        }

        protected override void LoadContent()
        {
            mBackground = new Background(1);
            var mainMenuBackground = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/MainMenuBackground");
            startTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_StartButtonRegular");
            startTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_StartButtonHover");
            loadTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_LoadButtonRegular");
            loadTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_LoadButtonHover");
            exitTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_ExitButtonRegular");
            exitTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_ExitButtonHover");
            startButton = new Button(startTextureRegular, startTextureHover, this.MGame.GraphicsDevice);
            loadButton = new Button(loadTextureRegular, loadTextureHover, this.MGame.GraphicsDevice);
            exitButton = new Button(exitTextureRegular, exitTextureHover, this.MGame.GraphicsDevice);
            startButton.SetPosition(new Vector2(MGame.GraphicsDevice.Viewport.Width/2-startTextureRegular.Width/2, 200));
            loadButton.SetPosition(new Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - loadTextureRegular.Width / 2, 250));
            exitButton.SetPosition(new Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - exitTextureRegular.Width / 2, 300));
            mBackground.Load(MGame.GraphicsDevice, mainMenuBackground);
           
            startButton.Select += PlaySound;
            loadButton.Select += PlaySound;
            exitButton.Select += PlaySound;
        }

        public override void Update(GameTime gameTime)
        {
            if (MGame.GameStateScreen == GameState.StartMenuScreenState)
            {
                KeyboardState state = Keyboard.GetState();
                MouseState mouse = Mouse.GetState();
                startButton.Update(state, mouse, 0, 0);
                loadButton.Update(state, mouse, 0, 0);
                exitButton.Update(state, mouse, 0, 0);

                if (startButton.IsClicked)
                {
                    MGame.CharacterCreationScreen = new CharacterCreationScreen(MGame);
                    MGame.Components.Add(MGame.CharacterCreationScreen);
                    MGame.CharacterCreationScreen.Initialize();
                    MGame.GameStateScreen = GameState.CharacterSelectionScreenState;
                }

                if (exitButton.IsClicked)
                {
                    MGame.Exit();
                }
            }
        }

        private void PlaySound(object sender, EventArgs e)
        {
            
        }


        public override void Draw(GameTime gameTime)
        {
            MGame.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null);
            mBackground.Draw(MGame.SpriteBatch);
            startButton.Draw(MGame.SpriteBatch);
            loadButton.Draw(MGame.SpriteBatch);
            exitButton.Draw(MGame.SpriteBatch);
            MGame.SpriteBatch.End();
        }
    }
}
