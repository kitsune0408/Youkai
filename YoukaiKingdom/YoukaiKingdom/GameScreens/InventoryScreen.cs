using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.Logic.Interfaces;
using YoukaiKingdom.Logic.Models.Characters.Heroes;
using YoukaiKingdom.Logic.Models.Items;
using YoukaiKingdom.Logic.Models.Items.Armors;
using YoukaiKingdom.Logic.Models.Items.Weapons;
using YoukaiKingdom.Sprites;

namespace YoukaiKingdom.GameScreens
{
    enum EquipmentType
    {
        MainHand,
        OffHand,
        Armor,
        Gloves,
        Boots,
        Helmet,
        BagItem
    }

    public class InventoryScreen : BaseGameScreen
    {
        //to check if mouse has been pressed already
        private MouseState lastMouseState = new MouseState();

        //background
        Background mBackground;
        //button textures
        private Texture2D goBackTextureRegular;
        private Texture2D goBackTextureHover;
        private Texture2D inventoryGridTexture;
        private Button goBackButton;
        private SpriteFont font;
        private Hero hero;

        //slots
        private string slotDescription;
        private Texture2D slotsTexture;
        private ItemSprite mainHandSprite;
        private Texture2D mainHandTexture;
        private ItemSprite offHandSprite;
        private Texture2D offHandTexture;
        private ItemSprite bodyArmorSprite;
        private Texture2D bodyArmorTexture;
        private ItemSprite glovesSprite;
        private Texture2D glovesTexture;
        private ItemSprite helmetSprite;
        private Texture2D helmeTexture;
        private ItemSprite bootsSprite;
        private Texture2D bootsTexture;

        private List<ItemSprite> bagItemsVisualization;


        public InventoryScreen(MainGame mGame)
            : base(mGame)
        {
            hero = MGame.hero;
        }

        protected override void LoadContent()
        {

            //Interface
            mBackground = new Background(1);
            Texture2D mainMenuBackground = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/MainMenuBackground");
            mBackground.Load(MGame.GraphicsDevice, mainMenuBackground);

            inventoryGridTexture = MGame.Content.Load<Texture2D>("Sprites/UI/InvScreen_Grid");
            goBackTextureRegular = MGame.Content.Load<Texture2D>("Sprites/UI/CC_ForwardButton");
            goBackTextureHover = MGame.Content.Load<Texture2D>("Sprites/UI/CC_ForwardButton_hover");
            goBackButton = new Button(goBackTextureRegular, goBackTextureHover, this.MGame.GraphicsDevice);
            goBackButton.SetPosition(new Vector2(30, 420));
            slotsTexture = MGame.Content.Load<Texture2D>("Sprites/UI/InvScreen_Slots");
            //End inteface  

            //Equippables
            FillEquippables();
            //items and slots
            bagItemsVisualization = new List<ItemSprite>();
            FillBag();
        }

        private void FillEquippables()
        {
            if (hero.Inventory.MainHandWeapon != null)
            {
                string mwName = hero.Inventory.MainHandWeapon.GetType().Name;
                try
                {
                    mainHandTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_" + mwName);
                }
                catch
                {
                    mainHandTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_PlaceHolder");
                }
                mainHandSprite = new ItemSprite(mainHandTexture) { Position = new Vector2(190, 300) };
            }
            if (hero.Inventory.OffHand != null)
            {
                string mwName = hero.Inventory.OffHand.GetType().Name;
                try
                {
                    offHandTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_" + mwName);
                }
                catch
                {
                    offHandTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_PlaceHolder");
                }
                offHandSprite = new ItemSprite(offHandTexture) { Position = new Vector2(190, 360) };
            }
            if (hero.Inventory.BodyArmor != null)
            {
                string mwName = hero.Inventory.BodyArmor.GetType().Name;
                try
                {
                    bodyArmorTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_" + mwName);
                }
                catch
                {
                    bodyArmorTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_PlaceHolder");
                }
                bodyArmorSprite = new ItemSprite(bodyArmorTexture) { Position = new Vector2(190, 60) };
            }
            if (hero.Inventory.Helmet != null)
            {
                string mwName = hero.Inventory.Helmet.GetType().Name;
                try
                {
                    helmeTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_" + mwName);
                }
                catch
                {
                    helmeTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_PlaceHolder");
                }
                helmetSprite = new ItemSprite(helmeTexture) { Position = new Vector2(190, 120) };
            }
            if (hero.Inventory.Gloves != null)
            {
                string mwName = hero.Inventory.Gloves.GetType().Name;
                try
                {
                    glovesTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_" + mwName);
                }
                catch
                {
                    glovesTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_PlaceHolder");
                }
                glovesSprite = new ItemSprite(glovesTexture) { Position = new Vector2(190, 240) };
            }
            if (hero.Inventory.Boots != null)
            {
                string mwName = hero.Inventory.Boots.GetType().Name;
                try
                {
                    bootsTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_" + mwName);
                }
                catch
                {
                    bootsTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_PlaceHolder");
                }
                bootsSprite = new ItemSprite(bootsTexture) { Position = new Vector2(190, 180) };
            }
        }

        private void FillBag()
        {
            bagItemsVisualization.Clear();
            int currentlyInBag = hero.Inventory.Bag.Count;
            for (int i = 0; i < currentlyInBag; i++)
            {
                Item it = hero.Inventory.Bag[i];
                string itName = it.GetType().Name;
                Texture2D itemTexture;
                try
                {
                    itemTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_" + itName);
                }
                catch
                {
                    itemTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_PlaceHolder");
                }
                ItemSprite itemSprite = new ItemSprite(it, itemTexture);
                int row = i / 6;
                int col = i % 6;
                itemSprite.Position = new Vector2(350 + col * 60, 60 + (row * 60));
                bagItemsVisualization.Add(itemSprite);
            }
        }

        private void MoveToInventory(EquipmentType type)
        {
            if (!hero.Inventory.IsFull)
            {
                switch (type)
                {
                    case EquipmentType.MainHand:
                        {
                            this.hero.RemoveMainHand();
                            break;
                        }
                    case EquipmentType.Armor:
                        {
                            this.hero.RemoveBodyArmor();
                            break;
                        }
                    case EquipmentType.Helmet:
                        {
                            this.hero.RemoveHelmet();
                            break;
                        }
                    case EquipmentType.OffHand:
                        {
                            this.hero.RemoveOffHand();
                            break;
                        }
                    case EquipmentType.Boots:
                        {
                            this.hero.RemoveBoots();
                            break;
                        }
                    case EquipmentType.Gloves:
                        {
                            this.hero.RemoveGloves();
                            break;
                        }
                }
                FillBag();
            }
        }

        private void EquipSlot(ItemSprite spr)
        {
            bagItemsVisualization.Clear();
            if (spr.mItem is IWeapon)
            {
                hero.ReplaceMainHand((Item)spr.mItem);
            }
            else if (spr.mItem is BodyArmor)
            {
                hero.ReplaceBodyArmor((Item)spr.mItem);
            }
            else if (spr.mItem is Helmet)
            {
                hero.ReplaceHelmet((Item)spr.mItem);
            }
            else if (spr.mItem is IOffhand)
            {
                hero.ReplaceOffHand((Item)spr.mItem);
            }
            else if (spr.mItem is Boots)
            {
                hero.ReplaceHelmet((Item)spr.mItem);
            }
            else if (spr.mItem is Gloves)
            {
                hero.ReplaceGloves((Gloves)spr.mItem);
            }
            FillBag();
            FillEquippables();

        }

        public override void Update(GameTime gameTime)
        {
            if (MGame.gameStateScreen == GameState.InventoryScreenState)
            {
                KeyboardState state = Keyboard.GetState();
                MouseState mouse = Mouse.GetState();

                //equip if equippable
                for (int i = 0; i < bagItemsVisualization.Count; i++)
                //(var itSprite in bagItemsVisualization)
                {
                    var itSprite = bagItemsVisualization[i];

                    itSprite.UpdateCurrent(mouse);
                    if (itSprite.isClicked)
                    {
                        if (mouse.LeftButton == ButtonState.Pressed &&
                            lastMouseState.LeftButton == ButtonState.Released)
                        //Will be true only if the user is currently clicking, but wasn't on the previous call.
                        {
                            EquipSlot(itSprite);   
                        }       
                    }
                }
                lastMouseState = mouse;
                //update equippables
                if (hero.Inventory.MainHandWeapon != null)
                {
                    mainHandSprite.UpdateCurrent(mouse);
                    if (mainHandSprite.isClicked)
                    {
                        MoveToInventory(EquipmentType.MainHand);
                    }
                }
                if (hero.Inventory.BodyArmor != null)
                {
                    bodyArmorSprite.UpdateCurrent(mouse);
                    if (bodyArmorSprite.isClicked)
                    {
                        MoveToInventory(EquipmentType.Armor);
                    }
                }
                if (hero.Inventory.OffHand != null)
                {
                    offHandSprite.UpdateCurrent(mouse);
                    if (offHandSprite.isClicked)
                    {
                        MoveToInventory(EquipmentType.OffHand);
                    }
                }
                if (hero.Inventory.Helmet != null)
                {
                    helmetSprite.UpdateCurrent(mouse);
                    if (helmetSprite.isClicked)
                    {
                        MoveToInventory(EquipmentType.Helmet);
                    }
                }
                if (hero.Inventory.Gloves != null)
                {
                    glovesSprite.UpdateCurrent(mouse);
                    if (glovesSprite.isClicked)
                    {
                        MoveToInventory(EquipmentType.Gloves);
                    }
                }
                if (hero.Inventory.Boots != null)
                {
                    bootsSprite.UpdateCurrent(mouse);
                    if (bootsSprite.isClicked)
                    {
                        MoveToInventory(EquipmentType.Boots);
                    }
                }
                //END update equippables

                goBackButton.Update(state, mouse);
                if (goBackButton.isClicked)
                {
                    MGame.gameStateScreen = GameState.PauseScreenState;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            MGame.SpriteBatch.Begin();

            mBackground.Draw(MGame.SpriteBatch);
            goBackButton.Draw(MGame.SpriteBatch);
            MGame.SpriteBatch.Draw(inventoryGridTexture,
                new Vector2(350, 60),
                Color.White);
            MGame.SpriteBatch.Draw(slotsTexture,
                new Vector2(50, 60),
                Color.White);
            if (hero.Inventory.MainHandWeapon != null)
            {
                mainHandSprite.Draw(MGame.SpriteBatch);
            }
            if (hero.Inventory.OffHand != null)
            {
                offHandSprite.Draw(MGame.SpriteBatch);
            }
            if (hero.Inventory.BodyArmor != null)
            {
                bodyArmorSprite.Draw(MGame.SpriteBatch);
            }
            if (hero.Inventory.Helmet != null)
            {
                helmetSprite.Draw(MGame.SpriteBatch);
            }
            if (hero.Inventory.Gloves != null)
            {
                glovesSprite.Draw(MGame.SpriteBatch);
            }
            if (hero.Inventory.Boots != null)
            {
                bootsSprite.Draw(MGame.SpriteBatch);
            }

            foreach (var itSprite in bagItemsVisualization)
            {
                itSprite.Draw(MGame.SpriteBatch);
            }

            MGame.SpriteBatch.End();
        }
    }
}
