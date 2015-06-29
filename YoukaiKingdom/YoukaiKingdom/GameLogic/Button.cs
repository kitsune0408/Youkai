using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        #region Fields

        private Texture2D _currentTexture;
        private readonly Texture2D _regularTexture;
        private readonly Texture2D _hoverTexture;
        private Vector2 position;

        #endregion

        public event EventHandler EnteringSelection;
        
        #region Constructors

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
            this.IsSelected = false;
            this.IsClicked = false;
        }

        #endregion

        #region Properties

        public bool IsClicked { get; set; }
        public bool IsSelected { get; set; }
        public Rectangle Rectangle { get; private set; }

        public Texture2D CurrentTexture
        {
            get { return _currentTexture; }
            set { _currentTexture = value; }
        }
        #endregion

        public void Update(KeyboardState state, MouseState mouse, int offsetX, int offsetY)
        {
            Rectangle mouseRectangle = new Rectangle(mouse.X + offsetX, mouse.Y + offsetY, 1, 1);
            Rectangle = new Rectangle((int)position.X, (int)position.Y, CurrentTexture.Width, CurrentTexture.Height);
            this.IsClicked = false;
            if (mouseRectangle.Intersects(Rectangle))
            {
                if (!this.IsSelected)
                {
                    this.OnEnteringSelect();
                }
                this.IsSelected = true;
            }
            else
            {
                this.IsSelected = false;
            }
            if (IsSelected)
            {
                this.CurrentTexture = _hoverTexture;
                if (state.IsKeyDown(Keys.Enter)|| mouse.LeftButton == ButtonState.Pressed)
                {
                    this.IsClicked = true;
                   
                }
            }
            else
            {
                this.CurrentTexture = _regularTexture;
                IsClicked = false;
            }
        }

        protected void OnEnteringSelect()
        {
            if (EnteringSelection != null)
            {
                this.EnteringSelection(this, new EventArgs());
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
