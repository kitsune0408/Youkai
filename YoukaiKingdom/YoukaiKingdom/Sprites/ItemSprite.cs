using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.Logic.Interfaces;
using YoukaiKingdom.Logic.Models.Items.Potions;


namespace YoukaiKingdom.Sprites
{
    class ItemSprite: StillSprite
    {
        public IItem mItem;
        public bool isSelected = false;
        public bool isClicked = false;
        public StringBuilder itemDescription;

        public event EventHandler Click;

        public ItemSprite(Texture2D sprite)
            : base(sprite)
        {
         
        }

    
        public ItemSprite(IItem item, Texture2D sprite)
            : base(sprite)
        {
            this.mItem = item;
            itemDescription = new StringBuilder();
            ShowItemDescription();
        }
    
        protected void OnClick()
        {
            if (Click != null)
            {
                this.Click(this, new EventArgs());
            }
        }

        private void ShowItemDescription()
        {
            itemDescription.Clear();
            itemDescription.AppendLine(mItem.Name);
            itemDescription.AppendLine("Level: " + mItem.Level);
            if (mItem is IWeapon)
            {
                var weapon = (IWeapon)mItem;
                itemDescription.AppendLine("Attack: " + weapon.AttackPoints);
                //itemDescription.AppendLine("Bonus attributes: " + weapon.Bonus);
            }
            else if (mItem is IArmor)
            {
                var armor = (IArmor)mItem;
                itemDescription.AppendLine("Defense: " + armor.DefensePoints);
                //itemDescription.AppendLine("Bonus attributes: " + armor.Bonus);
            }
            else if (mItem is HealingPotion)
            {
                var potion = (HealingPotion)mItem;
                itemDescription.AppendLine("Healing points: " + potion.HealingPoints);
            }
            else if (mItem is ManaPotion)
            {
                var potion = (ManaPotion)mItem;
                itemDescription.AppendLine("Mana points: " + potion.ManaPoints);
            }
        }

        public void UpdateCurrent(MouseState mouse)
        {
            Point mousePoint = new Point(mouse.X, mouse.Y);
            collisionRectangle = new Rectangle
                ((int)Position.X, (int)Position.Y, this.mSpriteTexture.Width, this.mSpriteTexture.Height);
            this.isClicked = false;
            if (this.collisionRectangle.Contains(mousePoint))
            {
                this.isSelected = true;
            }
            else
            {
                this.isSelected = false;
            }
            if (isSelected)
            { 
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    this.OnClick();
                    this.isClicked = true;
                }
            }
        }
    }
}
