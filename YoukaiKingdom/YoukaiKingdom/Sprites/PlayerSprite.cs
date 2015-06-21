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
    public class PlayerSprite : AnimatedSprite
    {
        #region Fields

        private Vector2 mDirection = Vector2.Zero;
        private Vector2 mSpeed = Vector2.Zero;
        private Hero _hero;
        //private Rectangle playerRectangle;
        //protected Vector2 previousPosition;
        public bool isBattleEngaged;

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
            this.Hero = _hero;
            //initialize variables
            mDirection = Vector2.Zero;
            mSpeed = Vector2.Zero;
            currentLookingPosition = LookingPosition.LookDown;
        }

        #endregion

        #region Properties

        public Hero Hero
        {
            get { return this._hero; }
            set { this._hero = value; }
        }



        public LookingPosition CurrentLookingPosition
        {
            get { return currentLookingPosition; }
            set { currentLookingPosition = value; }
        }

        #endregion

        #region Methods

        public void Update(Vector2 previousPos, GameTime gameTime, GamePlayScreen mGame)
        { 
            KeyboardState state = Keyboard.GetState();
            //if (state.IsKeyDown(Keys.D1))
            //{
            //    this._hero.Hit();
            //}
            collisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, 48, 64);
            //move player
            UpdateMovement(state);
            if (base.IsAnimating)
            {
                CheckCollision(Position, mGame, mGame.worldWidth, mGame.worldHeight);
            }
            this.previousPosition = this.Position;
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

            //Set position when player character stands
            switch (currentLookingPosition)
            {
                case LookingPosition.LookDown:
                    {
                        base.CurrentAnimation = AnimationKey.Down;
                        break;
                    }
                case LookingPosition.LookUp:
                    {
                        base.CurrentAnimation = AnimationKey.Up;
                        break;
                    }
                case LookingPosition.LookLeft:
                    {
                        base.CurrentAnimation = AnimationKey.Left;
                        break;
                    }
                case LookingPosition.LookRight:
                    {
                        base.CurrentAnimation = AnimationKey.Right;
                        break;
                    }
            }


            if (state.IsKeyUp(Keys.D1) && state.IsKeyUp(Keys.D2))
            {
                if (state.IsKeyDown(Keys.Left) == true || state.IsKeyDown(Keys.A) == true)
                {
                    currentLookingPosition = LookingPosition.LookLeft;
                    mSpeed.X = playerSpeed;
                    mDirection.X = moveLeft;
                    base.CurrentAnimation = AnimationKey.Left;
                    base.IsAnimating = true;
                }
                else if (state.IsKeyDown(Keys.Right) == true || state.IsKeyDown(Keys.D) == true)
                {
                    currentLookingPosition = LookingPosition.LookRight;
                    mSpeed.X = playerSpeed;
                    mDirection.X = moveRight;
                    base.CurrentAnimation = AnimationKey.Right;
                    base.IsAnimating = true;
                }

                if (state.IsKeyDown(Keys.Up) == true || state.IsKeyDown(Keys.W) == true)
                {
                    currentLookingPosition = LookingPosition.LookUp;
                    mSpeed.Y = playerSpeed;
                    mDirection.Y = moveUp;
                    base.CurrentAnimation = AnimationKey.Up;
                    base.IsAnimating = true;
                }
                else if (state.IsKeyDown(Keys.Down) == true || state.IsKeyDown(Keys.S) == true)
                {
                    currentLookingPosition = LookingPosition.LookDown;
                    mSpeed.Y = playerSpeed;
                    mDirection.Y = moveDown;
                    base.CurrentAnimation = AnimationKey.Down;
                    base.IsAnimating = true;
                }
            }
            //Animating attacks
            //if (state.IsKeyDown(Keys.E) == true)
            else
            {
                base.IsAnimating = true;
                switch (currentLookingPosition)
                {
                    case LookingPosition.LookDown:
                        {
                            base.CurrentAnimation = AnimationKey.AttackDown;
                            break;
                        }
                    case LookingPosition.LookUp:
                        {
                            base.CurrentAnimation = AnimationKey.AttackUp;
                            break;
                        }
                    case LookingPosition.LookLeft:
                        {
                            base.CurrentAnimation = AnimationKey.AttackLeft;
                            break;
                        }
                    case LookingPosition.LookRight:
                        {
                            base.CurrentAnimation = AnimationKey.AttackRight;
                            break;
                        }
                }
            }

        }
      
        #endregion
    }
}
