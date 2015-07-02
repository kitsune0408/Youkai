namespace YoukaiKingdom.GameLogic
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using YoukaiKingdom.GameScreens;
    using YoukaiKingdom.Sprites;

    public class Camera
    {
        #region Fields

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

        public Matrix Transform { get; set; }
         
        public Vector2 Position
        {
            get { return this.position; }
        }

        #endregion

        #region Methods

        public void Update(GameTime gameTime, PlayerSprite player, GamePlayScreen game)
        {
 
            this.Transform = Matrix.CreateScale(new Vector3(1,1,0))
                * Matrix.CreateTranslation(new Vector3(-this.position, 0));


            this.position.X = (player.Position.X + player.collisionRectangle.Width / 2)
                                 - (this.view.Width / 2);
            this.position.Y = (player.Position.Y + player.collisionRectangle.Height / 2)
                            - (this.view.Height / 2);
            this.LockCamera(game.WorldWidth, game.WorldHeight);

        }

        private void LockCamera(int worldWidth, int worldHeight)
        {
            this.position.X = MathHelper.Clamp(this.position.X,
                0,
                worldWidth - this.view.Width);
            this.position.Y = MathHelper.Clamp(this.position.Y,
                0,
                worldHeight - this.view.Height);
        }
     
        #endregion
    }
}
