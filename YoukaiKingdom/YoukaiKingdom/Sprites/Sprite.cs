﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.Interfaces;
using YoukaiKingdom.GameScreens;

namespace YoukaiKingdom.Sprites
{
    abstract class Sprite : ISprite
    {
        #region Fields

        //Current position of the sprite
        public Vector2 Position;
        //Sprite texture
        private Texture2D _mSpriteTexture;
        //size of the sprite (with scale applied)
        public Rectangle size;
        //number to increase/decrease the size of the original sprite.
        private float mScale = 1.0f;

        #endregion

        #region Properties

        public float Scale
        {
            get { return mScale; }
            set
            {
                mScale = value;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(_mSpriteTexture.Width * Scale), (int)(_mSpriteTexture.Height * Scale));
            }
        }

        public Texture2D mSpriteTexture
        {
            get  { return this._mSpriteTexture;}
            set { this._mSpriteTexture = value; }
        }

        public Rectangle Size
        {
            get { return this.size; }
            set { this.size = value; }
        }
        #endregion

        #region Constructors

        public Sprite(Texture2D sprite)
        {
            this.mSpriteTexture = sprite;
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
        }
        #endregion

        #region Methods

        //Update the sprite and change it's position based on the passed in speed, direction and elapsed time.
        public virtual void Update(GameTime gameTime, GamePlayScreen game, Vector2 speed, Vector2 direction)
        {
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;           
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mSpriteTexture, Position,
                new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
                Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
        #endregion
    }
}
