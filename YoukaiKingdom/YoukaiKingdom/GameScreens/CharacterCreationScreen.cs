
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.Logic.Models.Characters.Heroes;
using YoukaiKingdom.Sprites;

namespace YoukaiKingdom.GameScreens
{
    using YoukaiKingdom.Logic;

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
        private Sprite confirmationSprite;
        private Sprite nameLabel;

        private Texture2D offerSelectionTexture;
        private Texture2D textBackgroundTexture;
        private Texture2D confirmationSignTexture;
        private Texture2D nameLabelTexture;

        private CharacterType currentClass;
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
        private StringBuilder descriptionSam;
        private StringBuilder descriptionMon;
        private StringBuilder descriptionNin;
        //name input
        private Texture2D nameInputTexture;
        private TextBox nameInputTextbox;
        private KeyboardInput input;

        private string typedText;

        public CharacterCreationScreen(MainGame mGame)
            : base(mGame)
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
                Vector2(MGame.GraphicsDevice.Viewport.Width / 2 - offerSelectionTexture.Width / 2, 80);

            //font
            font = MGame.Content.Load<SpriteFont>("Fonts/YoukaiFont");

            forwardReg = MGame.Content.Load<Texture2D>("Sprites/UI/CC_ForwardButton");
            forwardHover = MGame.Content.Load<Texture2D>("Sprites/UI/CC_ForwardButton_hover");
            forwardButton = new Button(forwardReg, forwardHover, this.MGame.GraphicsDevice);
            forwardButton.SetPosition(new Vector2(740, 420));
            confirmationSignTexture = MGame.Content.Load<Texture2D>("Sprites/UI/CC_ConfirmationTexture");
            confirmationSprite = new StillSprite(confirmationSignTexture) { Position = new Vector2(580, 420) };
            samButtonReg = MGame.Content.Load<Texture2D>("Sprites/UI/CC_sam_sel_reg");
            samButtonHover = MGame.Content.Load<Texture2D>("Sprites/UI/CC_sam_sel_hov");
            monButtonReg = MGame.Content.Load<Texture2D>("Sprites/UI/CC_mon_sel_reg");
            monButtonHover = MGame.Content.Load<Texture2D>("Sprites/UI/CC_mon_sel_hov");
            ninButtonReg = MGame.Content.Load<Texture2D>("Sprites/UI/CC_nin_sel_reg");
            ninButtonHover = MGame.Content.Load<Texture2D>("Sprites/UI/CC_nin_sel_hov");


            showSamurai = new Button(samButtonReg, samButtonHover, this.MGame.GraphicsDevice);
            showSamurai.SetPosition(new
                Vector2(MGame.GraphicsDevice.Viewport.Width / 4 - samButtonReg.Width / 2, 150));
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

            representation = new StillSprite(samuraiRep)
            {
                Position = new Vector2(MGame.GraphicsDevice.Viewport.Width / 4 - 60, 200)
            };

            textBackgroundTexture = MGame.Content.Load<Texture2D>("Sprites/UI/CC_text_frame");
            textBackgroundSprite = new StillSprite(textBackgroundTexture)
            {
                Position = new
                    Vector2(MGame.GraphicsDevice.Viewport.Width / 4 + samuraiRep.Width / 2 + 40, 200)
            };

            classTextVector = new
                Vector2(MGame.GraphicsDevice.Viewport.Width / 4 + samuraiRep.Width / 2 + 50, 210);

            string desc = string.Concat(
                "INITIAL STATS\n",
                "Health: {0}\n",
                "Mana: {1}\n",
                "Attack: {2}\n",
                "Armor: {3}\n",
                "Starting weapon: {4}");

            this.descriptionSam = new StringBuilder();
            this.descriptionSam.AppendLine(
                string.Format(
                    desc,
                    Samurai.DefaultSamuraiHealth,
                    Samurai.DefaultSamuraiMana,
                    Samurai.DefaultSamuraiArmor,
                    Samurai.DefaultSamuraiDamage,
                    "One-handed sword"));

            this.descriptionMon = new StringBuilder();
            this.descriptionMon.AppendLine(
               string.Format(
                   desc,
                   Monk.DefaultMonkHealth,
                   Monk.DefaultMonkMana,
                   Monk.DefaultMonkArmor,
                   Monk.DefaultMonkDamage,
                   "Staff"));

            this.descriptionNin = new StringBuilder();
            this.descriptionNin.AppendLine(
               string.Format(
                   desc,
                   Ninja.DefaultNinjaHealth,
                   Ninja.DefaultNinjaMana,
                   Ninja.DefaultNinjaArmor,
                   Ninja.DefaultNinjaDamage,
                   "One-handed dagger"));


            currentClass = CharacterType.Samurai;
            showSamurai.IsSelected = true;

            nameLabelTexture = MGame.Content.Load<Texture2D>("Sprites/UI/CC_NameLabel");
            nameLabel = new StillSprite(nameLabelTexture) { Position = new Vector2(20, 405) };
            nameInputTexture = MGame.Content.Load<Texture2D>("Sprites/UI/CC_name_input");
            nameInputTextbox = new TextBox(nameInputTexture, font);
            nameInputTextbox.SetPosition(new Vector2(107, 400));
            typedText = "";
            input = new KeyboardInput(nameInputTextbox);

        }

        public override void Update(GameTime gameTime)
        {
            if (MGame.GameStateScreen == GameState.CharacterSelectionScreenState)
            {
                nameInputTextbox.Update(gameTime);
                input.Update(gameTime, nameInputTextbox);

                KeyboardState state = Keyboard.GetState();
                MouseState mouse = Mouse.GetState();
                Point mousePoint = new Point(mouse.X, mouse.Y);
                if (nameInputTextbox.PositionRect.Contains(mousePoint))
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
                forwardButton.Update(state, mouse, 0, 0);
                showSamurai.Update(state, mouse, 0, 0);
                showMonk.Update(state, mouse, 0, 0);
                showNinja.Update(state, mouse, 0, 0);

                typedText = nameInputTextbox.InputText;

                if (showSamurai.IsSelected)
                {
                    showSamurai.IsSelected = true;
                    showMonk.IsSelected = false;
                    showNinja.IsSelected = false;
                }
                else if (showMonk.IsSelected)
                {
                    showSamurai.IsSelected = false;
                    showMonk.IsSelected = true;
                    showNinja.IsSelected = false;
                }
                else if (showNinja.IsSelected)
                {
                    showSamurai.IsSelected = false;
                    showMonk.IsSelected = false;
                    showNinja.IsSelected = true;
                }

                if (showSamurai.IsClicked)
                {
                    currentClass = CharacterType.Samurai;
                    representation.mSpriteTexture = samuraiRep;
                }
                if (showMonk.IsClicked)
                {
                    currentClass = CharacterType.Monk;
                    representation.mSpriteTexture = monkRep;
                }
                if (showNinja.IsClicked)
                {
                    currentClass = CharacterType.Ninja;
                    representation.mSpriteTexture = ninjaRep;
                }
                if (this.forwardButton.IsClicked)
                {
                    switch (this.currentClass)
                    {
                        case CharacterType.Samurai:
                            {
                                this.MGame.Engine = new GameEngine(new Samurai((this.typedText == string.Empty) ? "Nameless Hero" : this.typedText));
                                break;
                            }
                        case CharacterType.Monk:
                            {
                                this.MGame.Engine = new GameEngine(new Monk((this.typedText == string.Empty) ? "Nameless Hero" : this.typedText));
                                break;
                            }
                        case CharacterType.Ninja:
                            {
                                this.MGame.Engine = new GameEngine(new Ninja((this.typedText == string.Empty) ? "Nameless Hero" : this.typedText));
                                break;
                            }
                    }

                    MGame.heroType = currentClass;

                    MGame.GamePlayScreen = new GamePlayScreen(MGame);
                    MGame.Components.Add(MGame.GamePlayScreen);
                    MGame.GamePlayScreen.Initialize();
                    MGame.InventoryScreen = new InventoryScreen(MGame);
                    MGame.Components.Add(MGame.InventoryScreen);
                    MGame.InventoryScreen.Initialize();
                    //MGame.InventoryScreen.Enabled = false;
                    //MGame.InventoryScreen.Visible = false;
                    MGame.GameStateScreen = GameState.GameScreenState;
                }
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
            confirmationSprite.Draw(MGame.SpriteBatch);
            representation.Draw(MGame.SpriteBatch);

            switch (currentClass)
            {
                case CharacterType.Samurai:
                    {
                        MGame.SpriteBatch.DrawString(font, this.descriptionSam.ToString(), classTextVector, Color.DarkRed);
                        break;
                    }

                case CharacterType.Monk:
                    {
                        MGame.SpriteBatch.DrawString(font, descriptionMon.ToString(), classTextVector, Color.DarkRed);
                        break;
                    }
                case CharacterType.Ninja:
                    {
                        MGame.SpriteBatch.DrawString(font, descriptionNin.ToString(), classTextVector, Color.DarkRed);
                        break;
                    }
            }

            nameInputTextbox.Draw(MGame.SpriteBatch, gameTime);
            nameLabel.Draw(MGame.SpriteBatch);
            //MGame.SpriteBatch.DrawString(font, typedText, nameInputVector, Color.DarkRed);
            MGame.SpriteBatch.End();
        }

        public StringBuilder descriptionMonk { get; set; }
    }
}
