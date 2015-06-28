using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.Interfaces;

namespace YoukaiKingdom.GameLogic
{
    public delegate void TextBoxEvent(TextBox sender);

    public class TextBox : IInputTextbox
    {
        private Texture2D _textBoxTexture;
        private SpriteFont _font;
        private Vector2 position;
        string _text = "";
       

        public TextBox(Texture2D textBoxTexture, SpriteFont font)
        {
            _textBoxTexture = textBoxTexture;
            this.Width = textBoxTexture.Width;
            this.Height = textBoxTexture.Height/2;
            _font = font;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; private set; }
        public Rectangle PositionRect;
        public bool Highlighted { get; set; }
        public String InputText
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                if (_text == null)
                    _text = "";
            }
        }

        public bool Selected
        {
            get;
            set;
        }

        #region Methods
        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Update(GameTime gameTime)
        {
            PositionRect = new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            String toDraw = InputText;
            spriteBatch.Draw(_textBoxTexture, 
                new Rectangle((int)position.X, (int)position.Y, Width, Height), 
                new Rectangle(0, Highlighted ? 
                    (_textBoxTexture.Height / 2) : 0, 
                    _textBoxTexture.Width, _textBoxTexture.Height / 2), Color.White);
            spriteBatch.DrawString(_font, toDraw, new Vector2((int)position.X + 3, (int)position.Y + 3), Color.DarkRed);
        }
 
        public void RecieveTextInput(string text)
        {
            InputText = text;
        }
        #endregion
    }
}
