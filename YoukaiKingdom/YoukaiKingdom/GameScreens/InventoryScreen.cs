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
using YoukaiKingdom.Logic.Models.Items.Potions;
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
        private StringBuilder currentItemDescription;
        private bool descriptionVisible;
        private Vector2 descriptionPosition;
        private Texture2D descriptionBackgroundTexture;
        private Texture2D descriptionBackgroundTextureBig;
        private bool bigDescription;
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
            hero = this.MGame.Engine.Hero;
        }

        protected override void LoadContent()
        {

            //Interface
            mBackground = new Background(1);
            Texture2D mainMenuBackground = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/MainMenuBackground");
            mBackground.Load(MGame.GraphicsDevice, mainMenuBackground);
            //font
            font = MGame.Content.Load<SpriteFont>("Fonts/YoukaiFontSmall");
            descriptionBackgroundTexture = MGame.Content.Load<Texture2D>("Sprites/UI/InvScreen_DescBackground");
            descriptionBackgroundTextureBig = MGame.Content.Load<Texture2D>("Sprites/UI/InvScreen_DescBackground_Big");
            currentItemDescription = new StringBuilder();

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
                IWeapon mWeapon = hero.Inventory.MainHandWeapon;
                string mwName = hero.Inventory.MainHandWeapon.GetType().Name;
                try
                {
                    if (mWeapon.AttackPoints <= 60)
                    {
                        mainHandTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_Tier1_" + mwName);
                    }
                    if (mWeapon.AttackPoints > 60 && mWeapon.AttackPoints <= 100)
                    {
                        mainHandTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_Tier2_" + mwName);
                    }
                    else if (mWeapon.AttackPoints > 100)
                    {
                        mainHandTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_Tier3_" + mwName);
                    }
                    else
                    {
                        mainHandTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_Tier1_" + mwName);
                    }            
                }
                catch
                {
                    mainHandTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_PlaceHolder");
                }
                mainHandSprite = new ItemSprite((Item)hero.Inventory.MainHandWeapon, mainHandTexture) { Position = new Vector2(190, 300) };
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
                offHandSprite = new ItemSprite((Item)hero.Inventory.OffHand, offHandTexture) { Position = new Vector2(190, 360) };
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
                bodyArmorSprite = new ItemSprite((Item)hero.Inventory.BodyArmor, bodyArmorTexture) { Position = new Vector2(190, 60) };
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
                helmetSprite = new ItemSprite((Item)hero.Inventory.Helmet, helmeTexture) { Position = new Vector2(190, 120) };
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
                glovesSprite = new ItemSprite((Item)hero.Inventory.Gloves, glovesTexture) { Position = new Vector2(190, 240) };
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
                bootsSprite = new ItemSprite((Item)hero.Inventory.Boots, bootsTexture) { Position = new Vector2(190, 180) };
            }
        }

        public void FillBag()
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
                    if (it is Weapon)
                    {
                        Weapon itTier = it as Weapon;
                        if (itTier.AttackPoints <= 60)
                        {
                            itemTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_Tier1_" + itName);
                        }
                        if (itTier.AttackPoints > 60 && itTier.AttackPoints <= 100)
                        {
                            itemTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_Tier2_" + itName);
                        }
                        else if (itTier.AttackPoints > 100)
                        {
                            itemTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_Tier3_" + itName);
                        }
                        else
                        {
                            itemTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_Tier1_" + itName);
                        }
                    }
                    else
                    {
                        itemTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_" + itName);
                    }
                    
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
                            this.hero.RemoveMainHand(this.MGame.Engine.HeroType);
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
                hero.ReplaceMainHand((Item)spr.mItem, this.MGame.Engine.HeroType);
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
            else if (spr.mItem is HealingPotion)
            {
                var hp = (HealingPotion)spr.mItem;
                hero.ApplyHealthPoints(hp);
                hero.Inventory.RemoveItemFromBag((Item)spr.mItem);
            }
            else if (spr.mItem is ManaPotion)
            {
                var mp = (ManaPotion)spr.mItem;
                hero.ApplyManaPoints(mp);
                hero.Inventory.RemoveItemFromBag((Item)spr.mItem);
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
                descriptionVisible = false;
                //equip if equippable
                for (int i = 0; i < bagItemsVisualization.Count; i++)
                {
                    var itSprite = bagItemsVisualization[i];
                    itSprite.UpdateCurrent(mouse);
                    if (itSprite.IsSelected)
                    {
                        currentItemDescription = itSprite.ItemDescription;
                        bigDescription = itSprite.BigDescription;
                        descriptionVisible = true;
                        descriptionPosition = new Vector2(mouse.X + 10, mouse.Y + 10);
                    }
                    if (itSprite.IsClicked)
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
                #region Update Equippables

                if (hero.Inventory.MainHandWeapon != null)
                {
                    mainHandSprite.UpdateCurrent(mouse);
                    if (mainHandSprite.IsSelected)
                    {
                        currentItemDescription = mainHandSprite.ItemDescription;
                        descriptionVisible = true;
                        descriptionPosition = new Vector2(mouse.X + 10, mouse.Y + 10);
                    }
                    if (mainHandSprite.IsClicked)
                    {
                        MoveToInventory(EquipmentType.MainHand);
                    }
                }
                if (hero.Inventory.BodyArmor != null)
                {
                    bodyArmorSprite.UpdateCurrent(mouse);
                    if (bodyArmorSprite.IsSelected)
                    {
                        currentItemDescription = bodyArmorSprite.ItemDescription;
                        descriptionVisible = true;
                        descriptionPosition = new Vector2(mouse.X + 10, mouse.Y + 10);
                    }
                    if (bodyArmorSprite.IsClicked)
                    {
                        MoveToInventory(EquipmentType.Armor);
                    }
                }
                if (hero.Inventory.OffHand != null)
                {
                    offHandSprite.UpdateCurrent(mouse);
                    if (offHandSprite.IsSelected)
                    {
                        currentItemDescription = offHandSprite.ItemDescription;
                        descriptionVisible = true;
                        descriptionPosition = new Vector2(mouse.X + 10, mouse.Y + 10);
                    }
                    if (offHandSprite.IsClicked)
                    {
                        MoveToInventory(EquipmentType.OffHand);
                    }
                }
                if (hero.Inventory.Helmet != null)
                {
                    helmetSprite.UpdateCurrent(mouse);
                    if (helmetSprite.IsSelected)
                    {
                        currentItemDescription = helmetSprite.ItemDescription;
                        descriptionVisible = true;
                        descriptionPosition = new Vector2(mouse.X + 10, mouse.Y + 10);
                    }
                    if (helmetSprite.IsClicked)
                    {
                        MoveToInventory(EquipmentType.Helmet);
                    }
                }
                if (hero.Inventory.Gloves != null)
                {
                    glovesSprite.UpdateCurrent(mouse);
                    if (glovesSprite.IsSelected)
                    {
                        currentItemDescription = glovesSprite.ItemDescription;
                        descriptionVisible = true;
                        descriptionPosition = new Vector2(mouse.X + 10, mouse.Y + 10);
                    }
                    if (glovesSprite.IsClicked)
                    {
                        MoveToInventory(EquipmentType.Gloves);
                    }
                }
                if (hero.Inventory.Boots != null)
                {
                    bootsSprite.UpdateCurrent(mouse);
                    if (bootsSprite.IsSelected)
                    {
                        currentItemDescription = bootsSprite.ItemDescription;
                        descriptionVisible = true;
                        descriptionPosition = new Vector2(mouse.X + 10, mouse.Y + 10);
                    }
                    if (bootsSprite.IsClicked)
                    {
                        MoveToInventory(EquipmentType.Boots);
                    }
                }

                #endregion
                //END update equippables

                goBackButton.Update(state, mouse);
                goBackButton.Update(state, mouse, 0, 0);
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
            if (descriptionVisible)
            {
                MGame.SpriteBatch.Draw(bigDescription ? descriptionBackgroundTextureBig : descriptionBackgroundTexture,
                    descriptionPosition, Color.White);
                MGame.SpriteBatch.DrawString(font, currentItemDescription.ToString(),
                      descriptionPosition, Color.White);
            }
            MGame.SpriteBatch.End();
        }
    }
}
