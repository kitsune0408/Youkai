
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.Logic.Models.Characters;
using YoukaiKingdom.Sprites;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;

namespace YoukaiKingdom.GameScreens
{
    public class GamePlayScreen: BaseGameScreen
    {
        #region Fields

        //textures
        Texture2D playerSprite;
        //sprites
        PlayerSprite mPlayer;
        private StillSprite castle01;
        private StillSprite forest01;
        private StillSprite oldHouse01;
        //background
        Background mBackground;
        
        Camera camera;
        //for Camera
        public Vector2 playerPosition;
        public Rectangle playerRectangle;
        public int worldWidth;
        public int worldHeight;
        
        public List<Rectangle> collisionRectangles;
        // ^ add all sprites from game screen to the list here
        //later probably invent somethin better

        #endregion

        #region Constructors

        public GamePlayScreen(MainGame mGame):base(mGame)
        {
            camera = new Camera(mGame.GraphicsDevice.Viewport);
            collisionRectangles = new List<Rectangle>();
        }

        #endregion

        #region Methods

        protected override void LoadContent()
        {
            mBackground = new Background(4);
            Texture2D background = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/Background01");
            Texture2D castleTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/Castle");
            Texture2D forestTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/forest01");
            Texture2D houseTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/old_house");
            //PLAYER
            playerSprite = MGame.Content.Load<Texture2D>("Sprites/PlayerClasses/Male_Samurai");
            //right now only this one, 
            //later will be selected depending of class

            Dictionary<AnimationKey, Animation> animations = new Dictionary<AnimationKey, Animation>();

            //walk animations
            Animation animation = new Animation(3, 48, 64, 0, 0);
            animations.Add(AnimationKey.Down, animation);

            animation = new Animation(3, 48, 64, 0, 64);
            animations.Add(AnimationKey.Left, animation);

            animation = new Animation(3, 48, 64, 0, 128);
            animations.Add(AnimationKey.Right, animation);

            animation = new Animation(3, 48, 64, 0, 192);
            animations.Add(AnimationKey.Up, animation);

            //attack animations
            animation = new Animation(2, 48, 64, 144, 0);
            animations.Add(AnimationKey.AttackDown, animation);

            animation = new Animation(2, 48, 64, 144, 64);
            animations.Add(AnimationKey.AttackLeft, animation);

            animation = new Animation(2, 48, 64, 144, 128);
            animations.Add(AnimationKey.AttackRight, animation);

            animation = new Animation(2, 48, 64, 144, 192);
            animations.Add(AnimationKey.AttackUp, animation);
            //end animation dictionary

            mPlayer = new PlayerSprite(playerSprite, animations, new Samurai("Sam", 200, 0, 50, 30));

            //set up castle
            castle01 = new StillSprite(castleTexture);
            forest01 = new StillSprite(forestTexture);
            oldHouse01 = new StillSprite(houseTexture);

            mPlayer.Position = new Vector2(200, 400);
            castle01.Position = new Vector2(100, 100);
            forest01.Position = new Vector2(300, 700);
            oldHouse01.Position = new Vector2(300, 500);
            //add environment to the list of collisions
            collisionRectangles.Add(new Rectangle
                ((int)castle01.Position.X, (int)castle01.Position.Y,
                castleTexture.Width, castleTexture.Height));
            collisionRectangles.Add(new Rectangle
                ((int)forest01.Position.X, (int)forest01.Position.Y,
                forestTexture.Width, forestTexture.Height));
            collisionRectangles.Add(new Rectangle
                ((int)oldHouse01.Position.X, (int)oldHouse01.Position.Y,
                houseTexture.Width, houseTexture.Height));

            mBackground.Load(MGame.GraphicsDevice, background);
            worldHeight = mBackground.WorldHeight;
            worldWidth = mBackground.WorldWidth;
        }

        public override void Update(GameTime gameTime)
        {// Allows the game to exit

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                MGame.Exit();

            mPlayer.Update(gameTime, this);
            //define current position of the player for the camera to follow
            playerPosition = mPlayer.Position;
            playerRectangle = new Rectangle((int)playerPosition.X, (int)playerPosition.Y,
                48, 64);
            camera.Update(gameTime, this);
        }

        public override void Draw(GameTime gameTime)
        {            
            MGame.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            mBackground.Draw(MGame.SpriteBatch);
            //things are drawn according to their order: i.e if castle is drawn before player, player will walk over it
            castle01.Draw(MGame.SpriteBatch);
            forest01.Draw(MGame.SpriteBatch);
            oldHouse01.Draw(MGame.SpriteBatch);
            mPlayer.Draw(gameTime, MGame.SpriteBatch);
            MGame.SpriteBatch.End();
        }

        #endregion
    }
}
