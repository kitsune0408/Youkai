using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.Logic.Interfaces;
using YoukaiKingdom.Logic.Models.Items.Potions;


namespace YoukaiKingdom.Sprites
{
    class ItemSprite : StillSprite
    {
        public IItem mItem;
        public bool IsSelected = false;
        public bool IsClicked = false;
        public bool ShowContextMenu = false;
        public StringBuilder ItemDescription;


        public ItemSprite(Texture2D sprite)
            : base(sprite)
        {

        }

        public ItemSprite(IItem item, Texture2D sprite)
            : base(sprite)
        {
            this.mItem = item;
            ItemDescription = new StringBuilder();
            ShowItemDescription();
        }

        public bool BigDescription { get; private set; }


        private void ShowItemDescription()
        {
            ItemDescription.Clear();
            ItemDescription.AppendLine(mItem.Name);
            ItemDescription.AppendLine("Level: " + mItem.Level);
            if (mItem is IWeapon)
            {
                var weapon = (IWeapon)mItem;
                ItemDescription.AppendLine("Attack: " + weapon.AttackPoints);
                //itemDescription.AppendLine("Bonus attributes: " + weapon.Bonus);
            }
            else if (mItem is IArmor)
            {
                var armor = (IArmor)mItem;
                ItemDescription.AppendLine("Defense: " + armor.DefensePoints);
                if (armor.Bonus != null)
                    if (armor.Bonus.HasBonuses)
                    {
                        this.BigDescription = true;
                        if (armor.Bonus.АdditionalHealth > 0)
                        {
                            this.ItemDescription.AppendLine("Bonus health points: " + armor.Bonus.АdditionalHealth);
                        }

                        if (armor.Bonus.АdditionalMana > 0)
                        {
                            this.ItemDescription.AppendLine("Bonus mana points: " + armor.Bonus.АdditionalMana);
                        }

                        if (armor.Bonus.АdditionalDamage > 0)
                        {
                            this.ItemDescription.AppendLine("Bonus attack points: " + armor.Bonus.АdditionalDamage);
                        }

                        if (armor.Bonus.АdditionalArmor > 0)
                        {
                            this.ItemDescription.AppendLine("Bonus defence points: " + armor.Bonus.АdditionalArmor);
                        }
                    }
                //itemDescription.AppendLine("Bonus attributes: " + armor.Bonus);
            }
            else if (mItem is HealingPotion)
            {
                var potion = (HealingPotion)mItem;
                ItemDescription.AppendLine("Healing points: " + potion.HealingPoints);
            }
            else if (mItem is ManaPotion)
            {
                var potion = (ManaPotion)mItem;
                ItemDescription.AppendLine("Mana points: " + potion.ManaPoints);
            }
        }

        public void UpdateCurrent(MouseState mouse)
        {
            Point mousePoint = new Point(mouse.X, mouse.Y);
            collisionRectangle = new Rectangle
                ((int)Position.X, (int)Position.Y, this.mSpriteTexture.Width, this.mSpriteTexture.Height);
            this.IsClicked = false;
            if (this.collisionRectangle.Contains(mousePoint))
            {
                this.IsSelected = true;
            }
            else
            {
                this.IsSelected = false;
            }
            if (IsSelected)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    this.IsClicked = true;
                }
                if (mouse.RightButton == ButtonState.Pressed)
                {
                    this.ShowContextMenu = true;
                }
            }
        }
    }
}
