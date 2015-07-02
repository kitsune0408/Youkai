using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.Interfaces;

namespace YoukaiKingdom.GameLogic
{
    public delegate void TextBoxEvent(TextBox sender);

    public class TextBox : IInputTextbox
    {
        private Texture2D textBoxTexture;
        private SpriteFont font;
        private Vector2 position;
        string text = "";


        public TextBox(Texture2D textBoxTexture, SpriteFont font)
        {
            this.textBoxTexture = textBoxTexture;
            this.Width = textBoxTexture.Width;
            this.Height = textBoxTexture.Height / 2;
            this.font = font;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; private set; }
        public Rectangle PositionRect { get; set; }
        public bool Highlighted { get; set; }
        public String InputText
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value ?? "";
            }
        }

        public bool Selected { get; set; }

        #region Methods
        public void SetPosition(Vector2 newPosition)
        {
            this.position = newPosition;
        }

        public void Update(GameTime gameTime)
        {
            this.PositionRect = new Rectangle((int)this.position.X, (int)this.position.Y, this.Width, this.Height);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            String toDraw = this.InputText;
            spriteBatch.Draw(this.textBoxTexture,
                new Rectangle((int)this.position.X, (int)this.position.Y, this.Width, this.Height),
                new Rectangle(0, this.Highlighted ?
                    (this.textBoxTexture.Height / 2) : 0,
                    this.textBoxTexture.Width, this.textBoxTexture.Height / 2), Color.White);
            spriteBatch.DrawString(this.font, toDraw, new Vector2((int)this.position.X + 3, (int)this.position.Y + 3), Color.DarkRed);
        }

        public void RecieveTextInput(string txt)
        {
            this.InputText = txt;
        }
        #endregion
    }
}
