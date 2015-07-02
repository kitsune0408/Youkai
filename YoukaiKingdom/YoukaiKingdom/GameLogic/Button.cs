namespace YoukaiKingdom.GameLogic
{
    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    class Button
    {
        #region Fields

        private Texture2D currentTexture;
        private readonly Texture2D regularTexture;
        private readonly Texture2D hoverTexture;
        private Vector2 position;

        #endregion
        
        #region Events
        public event EventHandler EnteringSelection;
        #endregion

        #region Constructors

        public Button(Texture2D regularTexture, Texture2D hoverTexture)
        {
            this.CurrentTexture = regularTexture;
            this.regularTexture = regularTexture;
            this.hoverTexture = hoverTexture;

        }
        public Button(Texture2D regularTexture)
        {
            this.CurrentTexture = regularTexture;
            this.regularTexture = regularTexture;
            this.hoverTexture = regularTexture;
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
            get { return this.currentTexture; }
            set { this.currentTexture = value; }
        }
        #endregion

        #region Methods

        public void Update(KeyboardState state, MouseState mouse, int offsetX, int offsetY)
        {
            Rectangle mouseRectangle = new Rectangle(mouse.X + offsetX, mouse.Y + offsetY, 1, 1);
            this.Rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.CurrentTexture.Width, this.CurrentTexture.Height);
            this.IsClicked = false;
            if (mouseRectangle.Intersects(this.Rectangle))
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
            if (this.IsSelected)
            {
                this.CurrentTexture = this.hoverTexture;
                if (state.IsKeyDown(Keys.Enter)|| mouse.LeftButton == ButtonState.Pressed)
                {
                    this.IsClicked = true;
                   
                }
            }
            else
            {
                this.CurrentTexture = this.regularTexture;
                this.IsClicked = false;
            }
        }

        protected void OnEnteringSelect()
        {
            if (this.EnteringSelection != null)
            {
                this.EnteringSelection(this, new EventArgs());
            }
        }

        public void SetPosition(Vector2 newPosition)
        {
            this.position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.currentTexture,
              this.position, Color.White);
        }
        #endregion
    }
}
