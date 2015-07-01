using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.Logic;
using YoukaiKingdom.Logic.Interfaces;
using YoukaiKingdom.Logic.Models.Characters.Heroes;
using YoukaiKingdom.Logic.Models.Items;

namespace YoukaiKingdom.GameScreens
{
    public class StartMenuScreen: BaseGameScreen
    {
        //to check if mouse has been pressed already
        private MouseState lastMouseState = new MouseState();
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
        //load saves
        private IAsyncResult result;
        private SaveGameData loadedData;

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
           
            startButton.EnteringSelection += PlaySound;
            loadButton.EnteringSelection += PlaySound;
            exitButton.EnteringSelection += PlaySound;
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

                if (loadButton.IsClicked)
                {
                    if (mouse.LeftButton == ButtonState.Pressed &&
                        lastMouseState.LeftButton == ButtonState.Released)
                    {
                        result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
                        StorageDevice device = StorageDevice.EndShowSelector(result);
                        if (device != null && device.IsConnected)
                        {
                            LoadSaveData(device);
                            StartLoadedGame(loadedData);
                        }
                    }
                }
                lastMouseState = mouse;
            }
        }

        public void StartLoadedGame(SaveGameData data)
        {
            MGame.HeroType = data.PlayerType;
            switch (data.PlayerType)
            {
                case CharacterType.Samurai:
                    {
                        this.MGame.Engine = GameEngine.Start(new Samurai(data.PlayerName), (int)data.LevelNumber+1);
                        break;

                    }
                case CharacterType.Monk:
                    {
                        this.MGame.Engine = GameEngine.Start(new Monk(data.PlayerName), (int)data.LevelNumber + 1);
                        break;
                    }
                case CharacterType.Ninja:
                    {
                        this.MGame.Engine = GameEngine.Start(new Ninja(data.PlayerName), (int)data.LevelNumber + 1);
                        break;
                    }
            }

            this.MGame.Engine.Hero.MaxHealth = data.MaxHealth;
            this.MGame.Engine.Hero.MaxMana = data.MaxMana;
            this.MGame.Engine.Hero.Health = data.CurrentHealth;
            this.MGame.Engine.Hero.Mana = data.CurrentMana;
            this.MGame.Engine.Hero.Damage = data.AttackPoints;
            this.MGame.Engine.Hero.Armor = data.DefencePoints;
            this.MGame.Engine.Hero.ExperiencePoints = data.PlayerExperiencePoints;
            this.MGame.Engine.Hero.Level = data.PlayerLevel;
            this.MGame.Engine.Hero.Inventory.ClearInventory();

            if (data.Armor != null)
            {
                this.MGame.Engine.Hero.Inventory.EquipArmor(data.Armor);
            }
            if (data.MainHandWeapon != null)
            {
                this.MGame.Engine.Hero.Inventory.EquipMainHand(data.MainHandWeapon);
            }
            if (data.OffhandShield != null)
            {
                this.MGame.Engine.Hero.Inventory.EquipOffHand(data.OffhandShield);
            }
            if (data.OffHandWeapon != null)
            {
                this.MGame.Engine.Hero.Inventory.EquipOffHand((IOffhand)data.OffHandWeapon);
            }
            if (data.Gloves != null)
            {
                this.MGame.Engine.Hero.Inventory.EquipArmor(data.Gloves);
            }
            if (data.Helmet != null)
            {
                this.MGame.Engine.Hero.Inventory.EquipArmor(data.Helmet);
            }
            if (data.Boots != null)
            {
                this.MGame.Engine.Hero.Inventory.EquipArmor(data.Boots);
            }

            foreach (Item item in data.BagItems)
            {
                this.MGame.Engine.Hero.Inventory.AddItemToBag(item);
            }
            MGame.GameStateScreen = GameState.GameScreenState;
            MGame.GamePlayScreen = new GamePlayScreen(MGame);
            MGame.GamePlayScreen.LevelNumber = data.LevelNumber;
            MGame.Engine.SetLevel((int)data.LevelNumber + 1);
            MGame.Components.Add(MGame.GamePlayScreen);
            MGame.GamePlayScreen.Initialize();
            MGame.InventoryScreen = new InventoryScreen(MGame);
            MGame.Components.Add(MGame.InventoryScreen);
            MGame.InventoryScreen.Initialize();
            
        }

        public void LoadSaveData(StorageDevice device)
        {

            result = device.BeginOpenContainer("Storage", null, null);
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            result.AsyncWaitHandle.Close();
            string filename = "savegame.sav";

            // Check to see whether the save exists.
            if (!container.FileExists(filename))
            {
                // If not, dispose of the container and return.
                container.Dispose();
                return;
            }
            //Open file.
            Stream stream = container.OpenFile(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            loadedData = (SaveGameData)serializer.Deserialize(stream);
            //close file
            stream.Close();
            container.Dispose();
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
