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
        Texture2D _textBoxTexture;
        Texture2D _caretTexture;

        SpriteFont _font;
        private Vector2 position;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; private set; }
        public Rectangle positionRect;
        public bool Highlighted { get; set; }

        string _text = "";
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

        public TextBox(Texture2D textBoxTexture, Texture2D caretTexture, SpriteFont font)
        {
            _textBoxTexture = textBoxTexture;
            this.Width = textBoxTexture.Width;
            this.Height = textBoxTexture.Height/2;
            _caretTexture = caretTexture;
            _font = font;

            _previousMouse = Mouse.GetState();
        }

        MouseState _previousMouse;

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }


        public void Update(GameTime gameTime)
        {
            positionRect = new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            String toDraw = InputText;

            spriteBatch.Draw(_textBoxTexture, 
                new Rectangle((int)position.X, (int)position.Y, Width, Height), 
                new Rectangle(0, Highlighted ? 
                    (_textBoxTexture.Height / 2) : 0, 
                    _textBoxTexture.Width, _textBoxTexture.Height / 2), Color.White);
            Vector2 size = _font.MeasureString(toDraw);
            spriteBatch.DrawString(_font, toDraw, new Vector2((int)position.X + 3, (int)position.Y + 3), Color.DarkRed);
        }
        public void RecieveTextInput(char inputChar)
        {
            InputText = InputText + inputChar;
        }
        public void RecieveTextInput(string text)
        {
            InputText = text;
        }

        public bool Selected
        {
            get;
            set;
        }
    }
}
