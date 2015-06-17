using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.Logic.Models.Characters.Heroes;
using YoukaiKingdom.Sprites;

namespace YoukaiKingdom.GameScreens
{
    public class CharacterCreationScreen : BaseGameScreen
    {
        private SpriteFont font;
        private Button forwardButton;
        private Button showSamurai;
        private Button showMonk;
        private Button showNinja;

        //background
        private Background mBackground;

        //for selection of class of hero
        private Sprite offerSelectionSprite;
        private Sprite textBackgroundSprite;
        private Texture2D offerSelectionTexture;
        private Texture2D textBackgroundTexture;

        private NPCClass currentClass;
        private Vector2 classTextVector;
        private Sprite representation;
        private Texture2D samuraiRep;
        private Texture2D monkRep;
        private Texture2D ninjaRep;
        private Texture2D forwardReg;
        private Texture2D forwardHover;
        private Texture2D samButtonReg;
        private Texture2D samButtonHover;
        private Texture2D monButtonReg;
        private Texture2D monButtonHover;
        private Texture2D ninButtonReg;
        private Texture2D ninButtonHover;
        private StringBuilder description;

        //name input
        private Texture2D nameInputTexture;
        private Texture2D caretTexture;
        private TextBox nameInputTextbox;
        private KeyboardInput input;
        private Vector2 nameInputVector;

        private string typedText;
        private bool canTypeText;

        public CharacterCreationScreen(MainGame mGame) : base(mGame)
        {
        }

        protected override void LoadContent()
        {
            mBackground = new Background(1);
            Texture2D mainMenuBackground = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/MainMenuBackground");
            mBackground.Load(MGame.GraphicsDevice, mainMenuBackground);

            offerSelectionTexture = MGame.Content.Load<Texture2D>("Sprites/UI/CC_hero_selection_texture");
            offerSelectionSprite = new StillSprite(offerSelectionTexture);
            offerSelectionSprite.Position = new 
                Vector2(MGame.GraphicsDevice.Viewport.Width / 2  - offerSelectionTexture.Width/2, 80);

            //font
            font = MGame.Content.Load<SpriteFont>("Fonts/YoukaiFont");

            forwardReg = MGame.Content.Load<Texture2D>("Sprites/UI/CC_ForwardButton");
            forwardHover = MGame.Content.Load<Texture2D>("Sprites/UI/CC_ForwardButton_hover");
            forwardButton = new Button(forwardReg, forwardHover, this.MGame.GraphicsDevice);
            forwardButton.SetPosition(new Vector2(740, 440));

            samButtonReg = MGame.Content.Load<Texture2D>("Sprites/UI/CC_sam_sel_reg");
            samButtonHover = MGame.Content.Load<Texture2D>("Sprites/UI/CC_sam_sel_hov");
            monButtonReg = MGame.Content.Load<Texture2D>("Sprites/UI/CC_mon_sel_reg");
            monButtonHover = MGame.Content.Load<Texture2D>("Sprites/UI/CC_mon_sel_hov");
            ninButtonReg = MGame.Content.Load<Texture2D>("Sprites/UI/CC_nin_sel_reg");
            ninButtonHover = MGame.Content.Load<Texture2D>("Sprites/UI/CC_nin_sel_hov");


            showSamurai = new Button(samButtonReg, samButtonHover, this.MGame.GraphicsDevice);
            showSamurai.SetPosition(new 
                Vector2(MGame.GraphicsDevice.Viewport.Width / 4 - samButtonReg.Width/2 , 150));
            showMonk = new Button(monButtonReg, monButtonHover, this.MGame.GraphicsDevice);
            showMonk.SetPosition(new
                Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - monButtonReg.Width / 2, 150));
            showNinja = new Button(ninButtonReg, ninButtonHover, this.MGame.GraphicsDevice);
            showNinja.SetPosition(new
                Vector2(MGame.GraphicsDevice.Viewport.Width - 
                    MGame.GraphicsDevice.Viewport.Width / 4 - ninButtonReg.Width / 2, 150));

            classTextVector = new Vector2(300, 350);
            samuraiRep = MGame.Content.Load<Texture2D>("Sprites/playerClasses/Male_Samurai_Representation");
            monkRep = MGame.Content.Load<Texture2D>("Sprites/playerClasses/Male_Monk_Representation");
            ninjaRep = MGame.Content.Load<Texture2D>("Sprites/playerClasses/Female_Ninja_Representation");

            representation = new StillSprite(samuraiRep);
            representation.Position = new Vector2(MGame.GraphicsDevice.Viewport.Width/4 - 60, 200);
            
            textBackgroundTexture = MGame.Content.Load<Texture2D>("Sprites/UI/CC_text_frame");           
            textBackgroundSprite = new StillSprite(textBackgroundTexture);
            textBackgroundSprite.Position = new
               Vector2(MGame.GraphicsDevice.Viewport.Width / 4 + samuraiRep.Width / 2 + 40, 200);
            
            classTextVector = new
                Vector2(MGame.GraphicsDevice.Viewport.Width / 4 + samuraiRep.Width / 2 + 50, 210);
            description = new StringBuilder();
            description.AppendLine("Here will be description of a character");
            description.AppendLine("Health:");
            description.AppendLine("Attack:");
            description.AppendLine("Defence:");
            description.AppendLine("Starting weapon:");

            currentClass = NPCClass.Samurai;
            showSamurai.isSelected = true;

            
            nameInputTexture = MGame.Content.Load<Texture2D>("Sprites/UI/CC_name_input");
            caretTexture = MGame.Content.Load<Texture2D>("Sprites/UI/CC_text_caret");
            nameInputTextbox = new TextBox(nameInputTexture, caretTexture, font);
            nameInputTextbox.SetPosition(new Vector2(100, 400));
            nameInputVector = new Vector2(104, 404);
            typedText = "";
            input = new KeyboardInput(this, nameInputTextbox);

        }
        public override void Update(GameTime gameTime)
        {
            nameInputTextbox.Update(gameTime);
            input.Update(gameTime, nameInputTextbox);
            
            KeyboardState state = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            Point mousePoint = new Point(mouse.X, mouse.Y);
            if (nameInputTextbox.positionRect.Contains(mousePoint))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    nameInputTextbox.Selected = true;
                    nameInputTextbox.Highlighted = true;
                }
            }
            else
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    nameInputTextbox.Selected = false;
                    nameInputTextbox.Highlighted = false;
                }
            }
            forwardButton.Update(state, mouse);
            showSamurai.Update(state, mouse);
            showMonk.Update(state,mouse);
            showNinja.Update(state, mouse);

            typedText = input.printedText;

            if (showSamurai.isSelected)
            {
                showSamurai.isSelected = true;
                showMonk.isSelected = false;
                showNinja.isSelected = false;
            }
            else if (showMonk.isSelected)
            {
                showSamurai.isSelected = false;
                showMonk.isSelected = true;
                showNinja.isSelected = false;
            }
            else if (showNinja.isSelected)
            {
                showSamurai.isSelected = false;
                showMonk.isSelected = false;
                showNinja.isSelected = true;
            }

            if (showSamurai.isClicked)
            {
                currentClass = NPCClass.Samurai;
                representation.mSpriteTexture = samuraiRep;
            }
            if (showMonk.isClicked)
            {
                currentClass = NPCClass.Monk;
                representation.mSpriteTexture = monkRep;
            }
            if (showNinja.isClicked)
            {
                currentClass = NPCClass.Ninja;
                representation.mSpriteTexture = ninjaRep;
            }
            if (forwardButton.isClicked)
            {
                switch (currentClass)
                {
                    case NPCClass.Samurai:
                    {
                        MGame.hero = new Samurai(typedText);                        
                        break;
                    }
                    case NPCClass.Monk:
                    {
                        MGame.hero = new Monk(typedText);
                        break;
                    }
                    case NPCClass.Ninja:
                    {
                        MGame.hero = new Ninja(typedText);
                        break;
                    }
                }
                MGame.heroType = currentClass; 
                MGame.GamePlayScreen = new GamePlayScreen(MGame, MGame.hero);
                MGame.Components.Add(MGame.GamePlayScreen);
                MGame.GamePlayScreen.Initialize();
                MGame.gameStateScreen = GameState.GameScreenState;
            }
           
        }

        public override void Draw(GameTime gameTime)
        {
            MGame.SpriteBatch.Begin();          
            mBackground.Draw(MGame.SpriteBatch);
            offerSelectionSprite.Draw(MGame.SpriteBatch);
            textBackgroundSprite.Draw(MGame.SpriteBatch);
            forwardButton.Draw(MGame.SpriteBatch);
            showSamurai.Draw(MGame.SpriteBatch);
            showMonk.Draw(MGame.SpriteBatch);
            showNinja.Draw(MGame.SpriteBatch);
            representation.Draw(MGame.SpriteBatch);
            MGame.SpriteBatch.DrawString(font, description.ToString(), classTextVector, Color.DarkRed);
            nameInputTextbox.Draw(MGame.SpriteBatch, gameTime);
            //MGame.SpriteBatch.DrawString(font, typedText, nameInputVector, Color.DarkRed);
            MGame.SpriteBatch.End();
        }
    }
}
