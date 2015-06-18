﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;

namespace YoukaiKingdom.GameScreens
{
    public class PauseMenuScreen: BaseGameScreen
    {
        //current gameplay
        private GamePlayScreen currentGamePlay;

        private Button returnToGameButton;
        private Button saveButton;
        private Button loadButton;
        private Button inventoryButton;
        private Button exitButton;
        
        //background
        Background mBackground;
        //button textures
        private Texture2D returnToGameTextureRegular;
        private Texture2D returnToGameTextureHover;
        private Texture2D loadTextureRegular;
        private Texture2D loadTextureHover;
        private Texture2D exitTextureRegular;
        private Texture2D exitTextureHover;
        private Texture2D inventoryTextureRegular;
        private Texture2D inventoryTextureHover;
        private Texture2D saveTextureRegular;
        private Texture2D saveTextureHover;     

        public PauseMenuScreen(MainGame mGame, GamePlayScreen currentGamePlay) : base(mGame)
        {
            this.currentGamePlay = currentGamePlay;
        }

        protected override void LoadContent()
        {
            mBackground = new Background(1);
            Texture2D mainMenuBackground = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/MainMenuBackground");

            returnToGameTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/PauseScreen_ReturnToGame");
            returnToGameTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/PauseScreen_ReturnToGame_hover");
            loadTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_LoadButtonRegular");
            loadTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_LoadButtonHover");
            exitTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_ExitButtonRegular");
            exitTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/MainMenu_ExitButtonHover");
            saveTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/PauseScreen_SaveGame");
            saveTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/PauseScreen_SaveGame_hover");
            inventoryTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/PauseScreen_Inventory");
            inventoryTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/PauseScreen_Inventory_hover");
            
            loadButton = new Button(loadTextureRegular, loadTextureHover, this.MGame.GraphicsDevice);
            exitButton = new Button(exitTextureRegular, exitTextureHover, this.MGame.GraphicsDevice);
            returnToGameButton = new Button(returnToGameTextureRegular, returnToGameTextureHover, this.MGame.GraphicsDevice);
            saveButton = new Button(saveTextureRegular, saveTextureHover, this.MGame.GraphicsDevice);
            inventoryButton = new Button(inventoryTextureRegular, inventoryTextureHover, this.MGame.GraphicsDevice);

            returnToGameButton.SetPosition(new Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - returnToGameTextureRegular.Width / 2, 150));
            saveButton.SetPosition(new Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - saveTextureRegular.Width / 2, 200));
            loadButton.SetPosition(new Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - loadTextureRegular.Width / 2, 250));
            inventoryButton.SetPosition(new Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - inventoryTextureRegular.Width / 2, 300));
            exitButton.SetPosition(new Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - exitTextureRegular.Width / 2, 350));

            mBackground.Load(MGame.GraphicsDevice, mainMenuBackground);
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState state = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            returnToGameButton.Update(state, mouse);
            exitButton.Update(state,mouse);
            loadButton.Update(state,mouse);
            inventoryButton.Update(state,mouse);
            saveButton.Update(state,mouse);

            if (returnToGameButton.isClicked)
            {
                currentGamePlay.Paused = false;
                MGame.gameStateScreen = GameState.GameScreenState;
            }

            if (exitButton.isClicked)
            {
                MGame.Exit();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            MGame.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null);
            mBackground.Draw(MGame.SpriteBatch);
            returnToGameButton.Draw(MGame.SpriteBatch);
            saveButton.Draw(MGame.SpriteBatch);
            loadButton.Draw(MGame.SpriteBatch);
            inventoryButton.Draw(MGame.SpriteBatch);
            exitButton.Draw(MGame.SpriteBatch);
            MGame.SpriteBatch.End();
        }

    }
}
