using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.Logic.Models.Characters.Heroes;

namespace YoukaiKingdom.GameScreens
{
    public class InventoryScreen: BaseGameScreen
    {
        //background
        Background mBackground;
        //button textures
        private Texture2D goBackTextureRegular;
        private Texture2D goBackTextureHover;
        private Texture2D inventoryGridTexture;
        private Button goBackButton;
        private Hero hero;
        public InventoryScreen(MainGame mGame) : base(mGame)
        {
            hero = MGame.hero;
        }

        protected override void LoadContent()
        {
            mBackground = new Background(1);
            Texture2D mainMenuBackground = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/MainMenuBackground");
            mBackground.Load(MGame.GraphicsDevice, mainMenuBackground);

            inventoryGridTexture = MGame.Content.Load<Texture2D>("Sprites/UI/InvScreen_Grid");
            goBackTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/CC_ForwardButton");
            goBackTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/CC_ForwardButton_hover");
            goBackButton = new Button(goBackTextureRegular, goBackTextureHover, this.MGame.GraphicsDevice);
            goBackButton.SetPosition(new Vector2(30, 420));
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            goBackButton.Update(state,mouse);
            if (goBackButton.isClicked)
            {
                MGame.gameStateScreen = GameState.PauseScreenState;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            MGame.SpriteBatch.Begin();

            mBackground.Draw(MGame.SpriteBatch);
            goBackButton.Draw(MGame.SpriteBatch);
            MGame.SpriteBatch.Draw(inventoryGridTexture,
                new Vector2(350, 90),
                Color.White);
            MGame.SpriteBatch.End();
        }
    }
}
