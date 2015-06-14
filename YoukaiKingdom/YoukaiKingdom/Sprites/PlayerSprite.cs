using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.GameScreens;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Logic.Models.Characters.Heroes;

namespace YoukaiKingdom.Sprites
{
    class PlayerSprite : AnimatedSprite
    {
        #region Fields

        private Vector2 mDirection = Vector2.Zero;
        private Vector2 mSpeed = Vector2.Zero;
        private Hero _hero;
        private Rectangle playerRectangle;


        #endregion

        #region Constants

        private const int playerSpeed = 160;
        private const int moveUp = -1;
        private const int moveDown = 1;
        private const int moveLeft = -1;
        private const int moveRight = 1;

        #endregion

        #region Constructors

        public PlayerSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation, Hero _hero)
            : base(sprite, animation)
        {
            this.mSpriteTexture = sprite;
            this.hero = _hero;
            //initialize variables
            mDirection = Vector2.Zero;
            mSpeed = Vector2.Zero;
        }

        #endregion

        #region Properties

        public Hero hero
        {
            get { return this._hero; }
            set { this._hero = value; }
        }

        #endregion

        #region Methods

        public void Update(GameTime gameTime, GamePlayScreen mGame)
        {
            KeyboardState state = Keyboard.GetState();

            this.playerRectangle = new Rectangle((int)Position.X, (int)Position.Y, 48, 64);
            //move player
            UpdateMovement(state);
            if (base.IsAnimating)
            {
                CheckCollision(mGame, mGame.worldWidth, mGame.worldHeight);
            }

            base.Update(gameTime, mGame, mSpeed, mDirection); 
            LockToMap(mGame.worldWidth, mGame.worldHeight);
        }

        //locks the player on map
        public void LockToMap(int worldWidth, int worldHeight)
        {
            Position.X = MathHelper.Clamp(Position.X, 0, worldWidth - 48);
            Position.Y = MathHelper.Clamp(Position.Y, 0, worldHeight - 64);
        }

        private void UpdateMovement(KeyboardState state)
        {

            mSpeed = Vector2.Zero;
            mDirection = Vector2.Zero;

            base.IsAnimating = false;


            if (state.IsKeyDown(Keys.Left) == true || state.IsKeyDown(Keys.A) == true)
            {
                mSpeed.X = playerSpeed;
                mDirection.X = moveLeft;
                base.CurrentAnimation = AnimationKey.Left;
                base.IsAnimating = true;
            }
            else if (state.IsKeyDown(Keys.Right) == true || state.IsKeyDown(Keys.D) == true)
            {
                mSpeed.X = playerSpeed;
                mDirection.X = moveRight;
                base.CurrentAnimation = AnimationKey.Right;
                base.IsAnimating = true;
            }

            if (state.IsKeyDown(Keys.Up) == true || state.IsKeyDown(Keys.W) == true)
            {
                mSpeed.Y = playerSpeed;
                mDirection.Y = moveUp;
                base.CurrentAnimation = AnimationKey.Up;
                base.IsAnimating = true;
            }
            else if (state.IsKeyDown(Keys.Down) == true || state.IsKeyDown(Keys.S) == true)
            {
                mSpeed.Y = playerSpeed;
                mDirection.Y = moveDown;
                base.CurrentAnimation = AnimationKey.Down;
                base.IsAnimating = true;
            }
        }

        private void CheckCollision(GamePlayScreen mGame, int worldWidth, int worldHeight)
        {
            foreach (var r in mGame.collisionRectangles)
                if (this.playerRectangle.Intersects(r))
                {
                    if (this.playerRectangle.Right >= r.Left && Position.X + 48 < r.Right 
                        && Position.Y + 64 < r.Bottom && Position.Y > r.Top) 
                    {
                        Position.X = MathHelper.Clamp(Position.X, 0, r.Left - 48);              
                    }
                    else if (this.playerRectangle.Left <= r.Right && Position.X > r.Left
                        && Position.Y + 64 < r.Bottom && Position.Y > r.Top)
                    {
                        Position.X = MathHelper.Clamp(Position.X, r.Right, worldWidth - 48);
                    }
                    else if (this.playerRectangle.Bottom >= r.Top && Position.Y + 64 < r.Bottom
                        && Position.X + 48 < r.Right && Position.X > r.Left)                        
                    {
                        Position.Y = MathHelper.Clamp(Position.Y, 0, r.Top - 64);
                    }
                    else if (this.playerRectangle.Top <= r.Bottom - 32
                        && Position.X + 48 < r.Right && Position.X > r.Left)
                    {
                        Position.Y = MathHelper.Clamp(Position.Y, r.Bottom - 32, worldHeight - 64);
                    }
                }
        }

        #endregion
    }
}
