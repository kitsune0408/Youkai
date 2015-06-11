﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.GameScreens;

namespace YoukaiKingdom.GameLogic
{
    public class Camera
    {
        #region Fields

        private Matrix transform;
        private Vector2 position;
        private Viewport view;

        #endregion

        #region Constructors

        public Camera(Viewport view)
        {
            this.View = view;
        }

        #endregion

        #region Properties

        public Viewport View
        {
            get { return this.view;}
            set { this.view = value; }
        }

        public Matrix Transform
        {
            get { return this.transform; }
            set { this.transform = value; }
        }

        #endregion

        #region Methods

        public void Update(GameTime gameTime, GamePlayScreen game)
        {
 
            this.Transform = Matrix.CreateScale(new Vector3(1,1,0))
                * Matrix.CreateTranslation(new Vector3(-position, 0));


            position.X = (game.playerPosition.X + game.playerRectangle.Width / 2)
                                 - (view.Width / 2);
            position.Y = (game.playerPosition.Y + game.playerRectangle.Height / 2)
                            - (view.Height / 2);
            LockCamera(game.worldWidth, game.worldHeight);
            //position = (Vector2)transform.Translation.;
        }

        private void LockCamera(int worldWidth, int worldHeight)
        {
            position.X = MathHelper.Clamp(position.X,
                0,
                worldWidth - view.Width);
            position.Y = MathHelper.Clamp(position.Y,
                0,
                worldHeight - view.Height);
        }
     
        #endregion
    }
}
