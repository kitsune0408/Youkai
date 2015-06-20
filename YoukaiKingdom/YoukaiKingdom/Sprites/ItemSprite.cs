using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.Logic.Interfaces;


namespace YoukaiKingdom.Sprites
{
    class ItemSprite: StillSprite
    {
        public IItem mItem;
        private bool isSelected = false;
        public bool isClicked = false;


        public event EventHandler Click;

        public ItemSprite(Texture2D sprite)
            : base(sprite)
        {
        }

    
        public ItemSprite(IItem item, Texture2D sprite)
            : base(sprite)
        {
            this.mItem = item;
        }
    
        protected void OnClick()
        {
            if (Click != null)
            {
                this.Click(this, new EventArgs());
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
