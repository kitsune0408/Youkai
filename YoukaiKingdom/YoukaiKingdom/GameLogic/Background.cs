using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YoukaiKingdom.GameLogic
{
    class Background
    {
        #region Fields

        private Texture2D mTexture;
        private int numberOfLoops;

        #endregion

        #region Constructors

        public Background(int numberOfLoops)
        {
            this.numberOfLoops = numberOfLoops;
        }

        #endregion

        #region Properties

        public int WorldWidth { get; set; }

        public int WorldHeight { get; set; }

        #endregion

        #region Methods

        public void Load(GraphicsDevice device, Texture2D backgroundTexture)
        {
            this.mTexture = backgroundTexture;
            // current world height and width
            this.WorldWidth = this.mTexture.Width * this.numberOfLoops;
            this.WorldHeight = this.mTexture.Height * this.numberOfLoops;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            var destination = new Rectangle(0, 0, this.mTexture.Width, this.mTexture.Height);


            for (int y = 0; y < this.numberOfLoops; y++)
                {
                    destination.Y = y * this.mTexture.Height;

                    for (int x = 0; x < this.numberOfLoops; x++)
                    {
                        destination.X = x * this.mTexture.Width;
                        spriteBatch.Draw(
                            this.mTexture,
                            destination,
                            null,
                            Color.White);
                    }
                }
        }

        #endregion
    }
}
