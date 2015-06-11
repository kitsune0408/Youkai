﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using YoukaiKingdom.Helpers;


namespace YoukaiKingdom.GameLogic
{
    class Background : IBackground
    {
        #region Fields

        private Texture2D mTexture;
        private int screenheight;
        private int screenwidth;
        private int worldWidth;
        private int worldHeight;

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
            WorldWidth = mTexture.Width * 4;
            WorldHeight = mTexture.Height * 4;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destination = new Rectangle(0, 0, mTexture.Width, mTexture.Height);

          
                for (int y = 0; y < 4; y++)
                {
                    destination.Y = y * mTexture.Height;

                    for (int x = 0; x < 4; x++)
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
