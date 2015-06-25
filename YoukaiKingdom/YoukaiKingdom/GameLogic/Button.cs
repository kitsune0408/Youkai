﻿using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace YoukaiKingdom.GameLogic
{
    class Button
    {
        private Texture2D _currentTexture;
        private readonly Texture2D _regularTexture;
        private readonly Texture2D _hoverTexture;
        private Vector2 position;
        public Rectangle rectangle;

        public Button(Texture2D regularTexture, Texture2D hoverTexture, GraphicsDevice graphics)
        {
            this.CurrentTexture = regularTexture;
            this._regularTexture = regularTexture;
            this._hoverTexture = hoverTexture;

        }
        public Button(Texture2D regularTexture, GraphicsDevice graphics)
        {
            this.CurrentTexture = regularTexture;
            this._regularTexture = regularTexture;
            this._hoverTexture = regularTexture;
            this.isSelected = false;
            this.isClicked = false;
        }

        public bool isClicked;
        public bool isSelected;
        public event EventHandler Click;

        public Texture2D CurrentTexture
        {
            get { return _currentTexture; }
            set { _currentTexture = value; }
        }

        protected void OnClick()
        {
            if (Click != null)
            {
                this.Click(this, new EventArgs());
            }
        }


        public void Update(KeyboardState state, MouseState mouse, int offsetX, int offsetY)
        {
            Rectangle mouseRectangle = new Rectangle(mouse.X + offsetX, mouse.Y + offsetY, 1, 1);
            rectangle = new Rectangle((int)position.X, (int)position.Y, CurrentTexture.Width, CurrentTexture.Height);
            this.isClicked = false;
            if (mouseRectangle.Intersects(rectangle))
            {
                this.isSelected = true;
            }
            else
            {
                this.isSelected = false;
            }
            if (isSelected)
            {
                this.CurrentTexture = _hoverTexture;
                if (state.IsKeyDown(Keys.Enter)|| mouse.LeftButton == ButtonState.Pressed)
                {
                    this.OnClick();
                    this.isClicked = true;
                }
            }
            else
            {
                this.CurrentTexture = _regularTexture;
                isClicked = false;
            }
        }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_currentTexture,
              position, Color.White);
        }
    }
}
