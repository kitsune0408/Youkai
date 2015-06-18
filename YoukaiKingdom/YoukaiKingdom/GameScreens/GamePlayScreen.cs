﻿
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.Logic.Models.Characters;
using YoukaiKingdom.Logic.Models.Characters.NPCs;
using YoukaiKingdom.Sprites;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;

namespace YoukaiKingdom.GameScreens
{
    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    public class GamePlayScreen: BaseGameScreen
    {
        #region Fields

        //pause check
        public bool Paused = false;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;
        //==================

        //textures
        Texture2D playerSprite;
        //SPRITES
        //========================
        //player sprite
        public PlayerSprite mPlayerSprite;
        //UI textures
        private Texture2D healthTexture;//player's health
        private Texture2D fillHealthTexture;
        private Texture2D currentHealthTexture;

        //enemy sprite
        private EnemySprite mEvilNinjaSprite;
        private Npc evilNinjaNpc;
        private List<AnimatedSprite> enemySprites;
        //environment sprites
        private StillSprite castle01;
        private StillSprite forest01;
        private StillSprite forest02;
        private StillSprite bigForest01;
        private StillSprite bigForest02;
        private StillSprite vertForest01;
        private StillSprite vertForest02;
        private StillSprite oldHouse01;
        private StillSprite oldHouse02;
        private StillSprite oldHouse03;
        private StillSprite verticalWall01;
        private StillSprite verticalWallShort01;
        private StillSprite verticalWallShort02;
        private StillSprite horisontalWall01;
        private StillSprite horisontalWall02;
        private List<Sprite> environmentSprites;
            
        //background
        Background mBackground;
        
        Camera camera;
        //for Camera
        //public Vector2 playerPosition;
        public int worldWidth;
        public int worldHeight;
        public bool battleOngoing;
        //main player variable Hero
        private Hero hero;
        
        public List<Rectangle> collisionRectangles;
        // ^ add all sprites from game screen to the list here
        //later probably invent somethin better

        

        #endregion

        #region Constructors

        public GamePlayScreen(MainGame mGame, Hero hero):base(mGame)
        {
            this.hero = mGame.hero;
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
            Texture2D bigForestTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/forest_02_big");
            Texture2D vertForestTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/forest_03_vert");
            Texture2D houseTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/old_house");
            Texture2D horWallTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/horisontal_wall");
            Texture2D verWallShortTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/vertical_wall_short");
            Texture2D verWallTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/vertical_wall");
            //UI
            //player health
            healthTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Game_HealthBar");
            fillHealthTexture = new Texture2D(MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            fillHealthTexture.SetData<Color>(new Color[] {Color.Red});
            currentHealthTexture = new Texture2D(MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            currentHealthTexture.SetData<Color>(new Color[] { Color.GreenYellow });
            //PLAYER
            switch (MGame.heroType)
            {
                case NPCClass.Samurai:
                {
                    playerSprite = MGame.Content.Load<Texture2D>("Sprites/PlayerClasses/Male_Samurai");
                    break;
                }
                case NPCClass.Monk:
                {
                    playerSprite = MGame.Content.Load<Texture2D>("Sprites/PlayerClasses/Male_Monk");
                    break;
                }
                case NPCClass.Ninja:
                {
                    playerSprite = MGame.Content.Load<Texture2D>("Sprites/PlayerClasses/Female_Ninja");
                    break;
                }
            }
            
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

            mPlayerSprite = new PlayerSprite(playerSprite, animations, hero);
            mPlayerSprite.Position = new Vector2(250, 250);

            //enemies
            Texture2D evilNinjaTexture = MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_ninja");
            evilNinjaNpc = new NpcRogue("Mook",400,0,50,20);
            mEvilNinjaSprite = new EnemySprite(evilNinjaNpc,evilNinjaTexture,animations);
            mEvilNinjaSprite.Position = new Vector2(1200, 300);
            mEvilNinjaSprite.setPatrollingArea(200, 200);
            enemySprites = new List<AnimatedSprite>();
            enemySprites.Add(mEvilNinjaSprite);
            //set up environment
            castle01 = new StillSprite(castleTexture);
            forest01 = new StillSprite(forestTexture);
            forest02 = new StillSprite(forestTexture);
            bigForest01 = new StillSprite(bigForestTexture);
            bigForest02 = new StillSprite(bigForestTexture);
            vertForest01 = new StillSprite(vertForestTexture);
            vertForest02 = new StillSprite(vertForestTexture);
            oldHouse01 = new StillSprite(houseTexture);
            oldHouse02 = new StillSprite(houseTexture);
            oldHouse03 = new StillSprite(houseTexture);
            horisontalWall01 = new StillSprite(horWallTexture);
            horisontalWall02 = new StillSprite(horWallTexture);
            verticalWall01 = new StillSprite(verWallTexture);
            verticalWallShort01 = new StillSprite(verWallShortTexture);
            verticalWallShort02 = new StillSprite(verWallShortTexture);
           
            castle01.Position = new Vector2(50, 50);
            
            oldHouse01.Position = new Vector2(60, 320);
            oldHouse02.Position = new Vector2(60, 500);
            oldHouse03.Position = new Vector2(260, 50);
            horisontalWall01.Position = new Vector2(0, 0);
            horisontalWall02.Position = new Vector2(0, 850);
            verticalWall01.Position = new Vector2(0, 50);
            verticalWallShort01.Position = new Vector2(550, 50);
            verticalWallShort02.Position = new Vector2(550, 500);

            forest01.Position = new Vector2(600, 0);
            bigForest01.Position = new Vector2(600, 500);
            vertForest01.Position = new Vector2(1400, 0);
            environmentSprites = new List<Sprite>();
            environmentSprites.Add(castle01);
            environmentSprites.Add(forest01);
            environmentSprites.Add(oldHouse01);
            environmentSprites.Add(oldHouse02);
            environmentSprites.Add(oldHouse03);
            environmentSprites.Add(horisontalWall01);
            environmentSprites.Add(horisontalWall02);
            environmentSprites.Add(verticalWall01);
            environmentSprites.Add(verticalWallShort01);
            environmentSprites.Add(verticalWallShort02);
            environmentSprites.Add(bigForest01);
            environmentSprites.Add(vertForest01);
            //add environment to the list of collisions
            foreach (var s in environmentSprites)
            {
                collisionRectangles.Add(new Rectangle
               ((int)s.Position.X, (int)s.Position.Y,
               s.mSpriteTexture.Width, s.mSpriteTexture.Height));
            }

            mBackground.Load(MGame.GraphicsDevice, background);
            worldHeight = mBackground.WorldHeight;
            worldWidth = mBackground.WorldWidth;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Paused)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    //MGame.Exit();
                    Paused = true;
                    MGame.gameStateScreen = GameState.PauseScreenState;
                }

                mEvilNinjaSprite.CheckOnTargets(mPlayerSprite);
                mEvilNinjaSprite.Update(gameTime, this);
                mPlayerSprite.Update(mPlayerSprite.previousPosition, gameTime, this);
                //define current position of the player for the camera to follow
                camera.Update(gameTime, mPlayerSprite, this);
                //for testing 
                // if (battleOngoing)
                // {
                //     hero.Health -= 1;
                // }
                // if (hero.Health <= 0)
                // {
                //     hero.Health = hero.MaxHealth;
                // }
            }
           
        }

        public override void Draw(GameTime gameTime)
        {            
            MGame.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            mBackground.Draw(MGame.SpriteBatch);
            //things are drawn according to their order: i.e if castle is drawn before player, player will walk over it
            foreach (var s in environmentSprites)
            {
                s.Draw(MGame.SpriteBatch);
            }
            foreach (var e in enemySprites)
            {
                e.Draw(gameTime, MGame.SpriteBatch);
            }

            //this one works
           // MGame.SpriteBatch.Draw(healthTexture, 
           //     new Vector2(camera.Position.X+ 20, camera.Position.Y + 20), 
           //     Color.DarkRed);

            MGame.SpriteBatch.Draw(fillHealthTexture, new Rectangle((int)camera.Position.X + 21,
                (int)camera.Position.Y + 21, (healthTexture.Width-2), healthTexture.Height-2),
                //new Rectangle(0, 45, healthTexture.Width, healthTexture.Height), 
                Color.Red);

          //  hero.MaxHealth/hero.Health

            //Draw the current health level based on the current Health
            MGame.SpriteBatch.Draw(currentHealthTexture, new Rectangle((int)camera.Position.X + 21,
                 (int)camera.Position.Y + 21, (healthTexture.Width-2) * hero.Health/hero.MaxHealth, healthTexture.Height-2),
                // new Rectangle(0, 45, healthTexture.Width, healthTexture.Height),
                 Color.Green);

            //Draw the box around the health bar
            MGame.SpriteBatch.Draw(healthTexture, 
                new Vector2(camera.Position.X+ 20, camera.Position.Y + 20), 
               Color.White);

            //mBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width / 2 - mHealthBar.Width / 2,

            //    30, mHealthBar.Width, 44), new Rectangle(0, 0, mHealthBar.Width, 44), Color.White);

            //Draw the current health level based on the current Health
    //        MGame.SpriteBatch.Draw(healthTexture,
    //            new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 30),
    //            Color.Green);
    //        //Draw the box around the health bar
    //        MGame.SpriteBatch.Draw(healthTexture,
    //            new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 30),
   //             Color.White);

            mPlayerSprite.Draw(gameTime, MGame.SpriteBatch);
            MGame.SpriteBatch.End();
        }

        #endregion
    }
}
