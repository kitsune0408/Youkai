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

    public class GamePlayScreen : BaseGameScreen
    {
        #region Fields

        //pause check
        public bool Paused;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;
        //==================

        //textures
        Texture2D playerSprite;
        private Texture2D throwableTexture;
        //SPRITES
        //========================
        //player sprite
        public PlayerSprite mPlayerSprite;
        //throwable weapons
        private ThrowableSprite mThrowableSprite;
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
        private StillSprite forest03;
        private StillSprite forest04;
        private StillSprite forest05;
        private StillSprite forest06;
        private StillSprite bigForest01;
        private StillSprite bigForest02;
        private StillSprite bigForest03;
        private StillSprite bigForest04;
        private StillSprite smallForest01;
        private StillSprite smallForest02;
        private StillSprite smallForest03;
        private StillSprite smallForest04;
        private StillSprite longForest01;
        private StillSprite vertForest01;
        private StillSprite vertForest02;
        private StillSprite vertForest03;
        private StillSprite vertForest04;
        private StillSprite vertForest05;
        private StillSprite vertForest06;
        private StillSprite vertForest07;
        private StillSprite vertForest08;
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
        //private Hero hero;

        public List<Rectangle> collisionRectangles;
        // ^ add all sprites from game screen to the list here
        //later probably invent somethin better

        #endregion

        #region Constructors

        public GamePlayScreen(MainGame mGame, Hero hero)
            : base(mGame)
        {
            //this.hero = mGame.hero;
            camera = new Camera(mGame.GraphicsDevice.Viewport);
            collisionRectangles = new List<Rectangle>();
            MGame.PauseMenuScreen = new PauseMenuScreen(MGame, this);
            MGame.Components.Add(MGame.PauseMenuScreen);
            MGame.PauseMenuScreen.Initialize();
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
            Texture2D smallForestTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/forest04_small");
            Texture2D longForestTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/forest_05_long");

            Texture2D houseTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/old_house");
            Texture2D horWallTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/horisontal_wall");
            Texture2D verWallShortTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/vertical_wall_short");
            Texture2D verWallTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/vertical_wall");
            //UI
            //player health
            healthTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Game_HealthBar");
            fillHealthTexture = new Texture2D(MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            fillHealthTexture.SetData<Color>(new Color[] { Color.Red });
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
            var animation = new Animation(3, 48, 64, 0, 0);
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

            mPlayerSprite = new PlayerSprite(playerSprite, animations, MGame.hero);
            mPlayerSprite.Position = new Vector2(250, 250);

            //enemies
            var evilNinjaTexture = MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_ninja");
            evilNinjaNpc = new NpcRogue(1, "Mook", 400, 0, 50, 20, 1800);
            mEvilNinjaSprite = new EnemySprite(evilNinjaNpc, evilNinjaTexture, animations)
            {
                Position = new Vector2(1200, 300)
            };
            mEvilNinjaSprite.SetPatrollingArea(200, 200);
            enemySprites = new List<AnimatedSprite>();
            enemySprites.Add(mEvilNinjaSprite);

            //set up environment
            castle01 = new StillSprite(castleTexture);
            forest01 = new StillSprite(forestTexture);
            forest02 = new StillSprite(forestTexture);
            forest03 = new StillSprite(forestTexture);
            forest04 = new StillSprite(forestTexture);
            forest05 = new StillSprite(forestTexture);
            forest06 = new StillSprite(forestTexture);
            bigForest01 = new StillSprite(bigForestTexture);
            bigForest02 = new StillSprite(bigForestTexture);
            bigForest03 = new StillSprite(bigForestTexture);
            bigForest04 = new StillSprite(bigForestTexture);
            vertForest01 = new StillSprite(vertForestTexture);
            vertForest02 = new StillSprite(vertForestTexture);
            vertForest03 = new StillSprite(vertForestTexture);
            vertForest04 = new StillSprite(vertForestTexture);
            vertForest05 = new StillSprite(vertForestTexture);
            vertForest06 = new StillSprite(vertForestTexture);
            vertForest07 = new StillSprite(vertForestTexture);
            vertForest08 = new StillSprite(vertForestTexture);
            smallForest01 = new StillSprite(smallForestTexture);
            smallForest02 = new StillSprite(smallForestTexture);
            smallForest03 = new StillSprite(smallForestTexture);
            smallForest04 = new StillSprite(smallForestTexture);
            longForest01 = new StillSprite(longForestTexture);
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
            forest02.Position = new Vector2(1600, 600);
            forest03.Position = new Vector2(1600, 1000);
            forest04.Position = new Vector2(600, 1600);
            smallForest01.Position = new Vector2(1800, 1200);
            smallForest03.Position = new Vector2(800, 1100);
            smallForest04.Position = new Vector2(800, 1300);
            vertForest02.Position = new Vector2(1400, 1000);
            vertForest03.Position = new Vector2(2600, 0);
            vertForest04.Position = new Vector2(2600, 800);
            forest05.Position = new Vector2(1600, 1600);
            forest06.Position = new Vector2(1400, 2000);
            bigForest02.Position = new Vector2(0, 1200);
            bigForest03.Position = new Vector2(1800, 200);
            longForest01.Position = new Vector2(0, 2000);
            bigForest04.Position = new Vector2(2600, 1800);
            vertForest05.Position = new Vector2(3000, 1000);
            vertForest06.Position = new Vector2(3000, 200);
            smallForest02.Position = new Vector2(3200, 200);
            vertForest07.Position = new Vector2(3400, 600);
            vertForest08.Position = new Vector2(3400, 1400);
            environmentSprites = new List<Sprite>
            {
                castle01,
                forest01,
                oldHouse01,
                oldHouse02,
                oldHouse03,
                horisontalWall01,
                horisontalWall02,
                verticalWall01,
                verticalWallShort01,
                verticalWallShort02,
                bigForest01,
                vertForest01,
                forest02,
                vertForest02,
                bigForest02,
                bigForest03,
                vertForest03,
                forest03,
                vertForest04,
                forest04,
                longForest01,
                smallForest01,
                forest05,
                forest06,
                bigForest04,
                vertForest05,
                vertForest06,
                smallForest02,
                vertForest07,
                vertForest08,
                smallForest03,
                smallForest04
            };
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

                if (Keyboard.GetState().IsKeyDown(Keys.D1))//this.mPlayerSprite.Hero.HitRange
                {
                    this.mPlayerSprite.Hero.Hit(this.evilNinjaNpc);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    if (this.mPlayerSprite.Hero is Monk) //monk.FireballCastRange използвай на този бутон
                    {
                        var monk = (Monk)this.mPlayerSprite.Hero;
                        monk.CastFireball(this.evilNinjaNpc);
                    }
                }
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

            MGame.SpriteBatch.Draw(fillHealthTexture, new Rectangle((int)camera.Position.X + 21,
                (int)camera.Position.Y + 21, (healthTexture.Width - 2), healthTexture.Height - 2),
                //new Rectangle(0, 45, healthTexture.Width, healthTexture.Height), 
                Color.Red);

            //Draw the current health level based on the current Health
            MGame.SpriteBatch.Draw(currentHealthTexture, new Rectangle((int)camera.Position.X + 21,
                 (int)camera.Position.Y + 21, (healthTexture.Width - 2) * MGame.hero.Health / MGame.hero.MaxHealth, healthTexture.Height - 2),

                 Color.Green);

            //Draw the box around the health bar
            MGame.SpriteBatch.Draw(healthTexture,
                new Vector2(camera.Position.X + 20, camera.Position.Y + 20),
               Color.White);
            mPlayerSprite.Draw(gameTime, MGame.SpriteBatch);
            MGame.SpriteBatch.End();
        }

        #endregion
    }
}

