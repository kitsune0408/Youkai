﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.GameScreens;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.Logic.Models.Characters.NPCs;

namespace YoukaiKingdom.Sprites
{
    public class EnemySprite : AnimatedSprite
    {
        #region Fields

        private Vector2 mDirection = Vector2.Zero;
        private Vector2 mSpeed = Vector2.Zero;
        //private LookingPosition currentLookingPosition;
        private Rectangle patrollingArea;
        private int enemyView;
        private bool battleEngaged;
        private bool outOfPatrollingArea;
        private PlayerSprite currentPlayer;

        #endregion

        #region Constants

        private const int enemySpeed = 120;
        private const int moveUp = -1;
        private const int moveDown = 1;
        private const int moveLeft = -1;
        private const int moveRight = 1;

        #endregion

        #region Constructors

        public EnemySprite(Npc enemy, Texture2D sprite, Dictionary<AnimationKey, Animation> animation)
            : base(sprite, animation)
        {
            this.Enemy = enemy;
            this.currentLookingPosition = LookingPosition.LookDown;
            this.battleEngaged = false;
            this.outOfPatrollingArea = false;
            this.AttackingPlayer = false;
            this.Position = new Vector2((float)enemy.Location.X, (float)enemy.Location.Y);
        }

        #endregion

        #region Properties

        public Npc Enemy { get; set; }
        public Texture2D fillHealthTexture { get; set; }
        public Texture2D currentHealthTexture { get; set; }
        public bool AttackingPlayer { get; private set; }

        #endregion

        #region Methods

        public void SetPatrollingArea(int horisontal, int vertical, int eView)
        {
            this.patrollingArea = new Rectangle((int)Position.X, (int)Position.Y, horisontal, vertical);
            this.enemyView = eView;
        }

        public void Update(GameTime gameTime, GamePlayScreen mGame)
        {
            collisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, 48, 64);
            //Set position when player character stands
            this.IsAnimating = true;
            mSpeed = Vector2.Zero;
            mDirection = Vector2.Zero;

            //normal patrolling
            if (!battleEngaged)
            {
                if (!outOfPatrollingArea)
                {
                    CheckIfInPatrollingArea();
                }
                else
                {
                    GoBackToPatrolling();
                }
            }
            else //engage in battle with player 
            {
                if (Position.X > currentPlayer.Position.X)
                {
                    currentLookingPosition = LookingPosition.LookLeft;
                }
                else if ((int)Position.X >= (int)currentPlayer.Position.X)
                {
                    if (Position.Y > currentPlayer.Position.Y)
                    {
                        currentLookingPosition = LookingPosition.LookUp;
                    }
                    else if (Position.Y < currentPlayer.Position.Y)
                    {
                        currentLookingPosition = LookingPosition.LookDown;
                    }
                }
                else
                {
                    currentLookingPosition = LookingPosition.LookRight;
                }
                CheckCollision(Position, mGame, mGame.WorldWidth, mGame.WorldHeight);
                CheckIfPlayerIsInRange();
                CheckCollisionWithPlayer();
            }
            if (!AttackingPlayer)
                switch (currentLookingPosition)
                {
                    case LookingPosition.LookDown:
                        {
                            mSpeed.Y = enemySpeed;
                            mDirection.Y = moveDown;
                            base.CurrentAnimation = AnimationKey.Down;
                            break;
                        }
                    case LookingPosition.LookUp:
                        {
                            mSpeed.Y = enemySpeed;
                            mDirection.Y = moveUp;
                            base.CurrentAnimation = AnimationKey.Up;
                            break;
                        }
                    case LookingPosition.LookLeft:
                        {
                            mSpeed.X = enemySpeed;
                            mDirection.X = moveLeft;
                            base.CurrentAnimation = AnimationKey.Left;
                            break;
                        }
                    case LookingPosition.LookRight:
                        {
                            mSpeed.X = enemySpeed;
                            mDirection.X = moveRight;
                            base.CurrentAnimation = AnimationKey.Right;
                            break;
                        }
                }
            else //currently attacking player
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;
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
            this.previousPosition = this.Position;
            base.Update(gameTime, mGame, mSpeed, mDirection);
        }

        public void GoBackToPatrolling()
        {
            if (Position.X > patrollingArea.X)
            {
                currentLookingPosition = LookingPosition.LookLeft;
            }
            else if ((int)Position.X == (int)patrollingArea.X)
            {
                if (Position.Y > patrollingArea.Y)
                {
                    currentLookingPosition = LookingPosition.LookUp;
                }
                else if (Position.Y < patrollingArea.Y)
                {
                    currentLookingPosition = LookingPosition.LookDown;
                }
                else if ((int)Position.Y == (int)patrollingArea.Y)
                {
                    outOfPatrollingArea = false;
                    currentLookingPosition = LookingPosition.LookDown;
                }
            }
            else
            {
                currentLookingPosition = LookingPosition.LookRight;
            }
        }

        public void CheckOnTargets(PlayerSprite player)
        {
            Rectangle noticeArea = new Rectangle((int)patrollingArea.X - enemyView,
                (int)patrollingArea.Y - enemyView, Width + (enemyView * 2), Height + (enemyView * 2));
            if (noticeArea.Intersects(player.collisionRectangle))
            {
                battleEngaged = true;
                outOfPatrollingArea = true;
                currentPlayer = player;
            }
            else
            {
                battleEngaged = false;
            }
        }

        public void CheckIfInPatrollingArea()
        {
            if ((int)Position.Y == patrollingArea.Bottom - 64)
            {
                if ((int)Position.X == patrollingArea.Right - 48)
                {
                    currentLookingPosition = LookingPosition.LookUp;
                }
                else
                {
                    currentLookingPosition = LookingPosition.LookRight;
                }
            }
            else if ((int)Position.Y == patrollingArea.Top)
            {
                if ((int)Position.X == patrollingArea.Left)
                {
                    currentLookingPosition = LookingPosition.LookDown;
                }
                else
                {
                    currentLookingPosition = LookingPosition.LookLeft;
                }
            }
        }

        public void CheckIfPlayerIsInRange()
        {
            //create rectange for hit range
            int positionX = (int)this.Position.X - this.Enemy.HitRange * 20;
            int positionY = (int)this.Position.Y - this.Enemy.HitRange * 20;
            int rectW = 48 + this.Enemy.HitRange * 40;
            int rectH = 64 + this.Enemy.HitRange * 40;
            var rangeRect = new Rectangle(positionX, positionY, rectW, rectH);
            if (rangeRect.Intersects(currentPlayer.collisionRectangle))
            {
                battleEngaged = true;
                outOfPatrollingArea = true;
                AttackingPlayer = true;
            }
            else
            {
                battleEngaged = false;
                AttackingPlayer = false;
            }

        }

        public void CheckCollisionWithPlayer()
        {
            if (this.collisionRectangle.Intersects(currentPlayer.collisionRectangle))
            {
                this.Position = previousPosition;
                currentPlayer.Position = currentPlayer.previousPosition;
            }
        }

        #endregion

    }
}
