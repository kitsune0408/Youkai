using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace YoukaiKingdom.GameLogic
{
    class Background
    {
        #region Fields

        private Texture2D mTexture;
        private int screenheight;
        private int screenwidth;
        private int worldWidth;
        private int worldHeight;
        private int numberOfLoops;

        #endregion

        #region Constructors

        public Background(int numberOfLoops)
        {
            this.numberOfLoops = numberOfLoops;
        }

        #endregion

        #region Properties

        public int WorldWidth
        {
            get { return this.worldWidth; }
            set { this.worldWidth = value; }
        }

        public int WorldHeight
        {
            get { return this.worldHeight; }
            set { this.worldHeight = value; }
        }

        #endregion

        #region Methods

        public void Load(GraphicsDevice device, Texture2D backgroundTexture)
        {
            mTexture = backgroundTexture;
            screenheight = device.Viewport.Height;
            screenwidth = device.Viewport.Width;

            // current world height and width
            WorldWidth = mTexture.Width * numberOfLoops;
            WorldHeight = mTexture.Height * numberOfLoops;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destination = new Rectangle(0, 0, mTexture.Width, mTexture.Height);

          
                for (int y = 0; y < numberOfLoops; y++)
                {
                    destination.Y = y * mTexture.Height;

                    for (int x = 0; x < numberOfLoops; x++)
                    {
                        destination.X = x * mTexture.Width;
                        spriteBatch.Draw(
                            mTexture,
                            destination,
                            null,
                            Color.White);
                    }
                }
        }

        #endregion
    }
}
