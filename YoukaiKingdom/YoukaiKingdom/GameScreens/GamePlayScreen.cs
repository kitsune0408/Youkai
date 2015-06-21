using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.Logic.Interfaces;
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
        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;
        private Queue<string> gameLogQueue;
        private bool gameLogQueueUpdated = false;

        private string currentLog1;
        private string currentLog2;
        private string currentLog3;
        private Texture2D logBackgroundTexture;

        private SpriteFont font;
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
        private Texture2D fillManaTexture;
        private Texture2D currentManaTexture;
        private Texture2D ManaPotionTexture;
        private Texture2D HealingPotionTexture;

        //enemy sprite
        private EnemySprite mEvilNinjaSprite01;
        private EnemySprite mEvilNinjaSprite02;
        private EnemySprite mEvilNinjaSprite03;
        //enemies has numbers for better distinguishment
        private Npc evilNinjaNpc01;
        private Npc evilNinjaNpc02;
        private Npc evilNinjaNpc03;
        private List<EnemySprite> enemySprites;

        //environment sprites
        #region List of Environment sprites
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
        #endregion

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
            //font
            font = MGame.Content.Load<SpriteFont>("Fonts/YoukaiFont");
            gameLogQueue = new Queue<string>();
            currentLog1 = "Please, destroy the monster, threatening our village!";
            currentLog2 = string.Format("Welcome, {0}!", MGame.hero.Name);
            currentLog3 = "";
            logBackgroundTexture = MGame.Content.Load<Texture2D>("Sprites/UI/UI_LogBackground");
            //gameLogQueue.Enqueue(currentLog1);
            HealingPotionTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_HealingPotion");
            ManaPotionTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_ManaPotion");
            #region Environment Textures
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
            #endregion

            //UI
            //player health
            healthTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Game_HealthBar");
            fillHealthTexture = new Texture2D(MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            fillHealthTexture.SetData<Color>(new Color[] { Color.Red });
            currentHealthTexture = new Texture2D(MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            currentHealthTexture.SetData<Color>(new Color[] { Color.GreenYellow });
            //player mana
            fillManaTexture = new Texture2D(MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            fillManaTexture.SetData<Color>(new Color[] { Color.DimGray });
            currentManaTexture = new Texture2D(MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            currentManaTexture.SetData<Color>(new Color[] { Color.SkyBlue });

            //PLAYER
            #region PLAYER
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
            #endregion

            //start animation dictionary
            #region Animation Dictionaries
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
            #endregion
            //end animation dictionary

            mPlayerSprite = new PlayerSprite(playerSprite, animations, MGame.hero);
            mPlayerSprite.Position = new Vector2(250, 250);

            //enemies
            #region Set Enemies

            var evilNinjaTexture = MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_ninja");
            evilNinjaNpc01 = new NpcRogue(1, "Mook", 400, 0, 100, 50, 1800);
            evilNinjaNpc02 = new NpcRogue(1, "Mook", 500, 0, 100, 75, 1800);
            evilNinjaNpc03 = new NpcRogue(1, "Mook", 400, 0, 100, 50, 1800);
            mEvilNinjaSprite01 = new EnemySprite(evilNinjaNpc01, evilNinjaTexture, animations)
            {
                Position = new Vector2(1200, 300)
            };
            mEvilNinjaSprite02 = new EnemySprite(evilNinjaNpc02, evilNinjaTexture, animations)
            {
                Position = new Vector2(1200, 800)
            };
            mEvilNinjaSprite01.SetPatrollingArea(200, 200);
            mEvilNinjaSprite02.SetPatrollingArea(200, 200);

            #endregion

            enemySprites = new List<EnemySprite>();
            enemySprites.Add(mEvilNinjaSprite01);
            enemySprites.Add(mEvilNinjaSprite02);
            foreach (var e in enemySprites)
            {
                //enemy health
                e.fillHealthTexture = new Texture2D(MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                e.fillHealthTexture.SetData<Color>(new Color[] { Color.Red });
                e.currentHealthTexture = new Texture2D(MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                e.currentHealthTexture.SetData<Color>(new Color[] { Color.GreenYellow });
            }

            //set up environment
            #region Environment Setup
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
            #endregion

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
                currentKeyboardState = Keyboard.GetState();
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    //MGame.Exit();
                    Paused = true;
                    MGame.gameStateScreen = GameState.PauseScreenState;
                }

                foreach (var enemySprite in enemySprites)
                {
                    if (enemySprite.Enemy.Health > 0)
                    {
                        enemySprite.CheckOnTargets(mPlayerSprite);
                        enemySprite.Update(gameTime, this);
                    }
                }

                mPlayerSprite.Update(mPlayerSprite.previousPosition, gameTime, this);
                //define current position of the player for the camera to follow
                camera.Update(gameTime, mPlayerSprite, this);

                if (CheckKey(Keys.D1)) //this.mPlayerSprite.Hero.HitRange
                {
                    EnemySprite enemyInVicinity = FindEnemy(mPlayerSprite.Hero.HitRange);

                    if (enemyInVicinity != null)
                    {
                        this.mPlayerSprite.Hero.Hit(enemyInVicinity.Enemy);
                        AddToGameLog(string.Format("{0} hit {1} for {2} damage!",
                            MGame.hero.Name, enemyInVicinity.Enemy.Name, enemyInVicinity.Enemy.DamageGotten));
                        if (enemyInVicinity.Enemy.Health <= 0)
                        {
                            AddToGameLog(string.Format("{0} is dead!", enemyInVicinity.Enemy.Name));
                        }
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    if (this.mPlayerSprite.Hero is Monk) //monk.FireballCastRange
                    {
                        var monk = (Monk)this.mPlayerSprite.Hero;
                        EnemySprite enemyInVicinity = FindEnemy(monk.FireballCastRange);
                        if (enemyInVicinity != null)
                        {
                            monk.CastFireball(enemyInVicinity.Enemy);
                        }
                    }
                }
                lastKeyboardState = currentKeyboardState;
            }
        }

        private void AddToGameLog(string log)
        {
            gameLogQueue.Enqueue(log);
            gameLogQueueUpdated = true;
        }


        private EnemySprite FindEnemy(int range)
        {
            //create rectange for hit range
            //hero range is multiplied 30 times for the game screen
            int positionX = (int)mPlayerSprite.Position.X - MGame.hero.HitRange * 20;
            int positionY = (int)mPlayerSprite.Position.Y - MGame.hero.HitRange * 20;
            int rectW = 48 + MGame.hero.HitRange * 40;
            int rectH = 64 + MGame.hero.HitRange * 40;
            Rectangle rangeRect = new Rectangle(positionX, positionY, rectW, rectH);
            foreach (var e in enemySprites)
            {
                if (e.Enemy.Health > 0)
                {
                    if (rangeRect.Intersects(e.collisionRectangle))
                    {
                        return (EnemySprite)e;
                    }
                }
            }
            return null;
        }

        private bool CheckKey(Keys key)
        {
            return lastKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyUp(key);
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
                if (e.Enemy.Health > 0)
                {
                    e.Draw(gameTime, MGame.SpriteBatch);
                    MGame.SpriteBatch.Draw(e.fillHealthTexture, new Rectangle((int)e.Position.X,
                        (int)e.Position.Y - 5, 48, 3), Color.Red);
                    //Draw the current health level based on the current Health
                    MGame.SpriteBatch.Draw(e.currentHealthTexture, new Rectangle((int)e.Position.X,
                         (int)e.Position.Y - 5, 48 * e.Enemy.Health / e.Enemy.MaxHealth, 3),
                         Color.Green);
                }
            }

            mPlayerSprite.Draw(gameTime, MGame.SpriteBatch);
            MGame.SpriteBatch.DrawString(font, MGame.hero.Name,
                   new Vector2((int)camera.Position.X + 5, (int)camera.Position.Y + 5), Color.White);

            MGame.SpriteBatch.Draw(fillHealthTexture, new Rectangle((int)camera.Position.X + 6,
                (int)camera.Position.Y + 26, (healthTexture.Width - 2), healthTexture.Height - 2),
                Color.Red);
            //Draw the current health level 6based on the current Health
            MGame.SpriteBatch.Draw(currentHealthTexture, new Rectangle((int)camera.Position.X + 6,
                 (int)camera.Position.Y + 26, (healthTexture.Width - 2) * MGame.hero.Health / MGame.hero.MaxHealth, healthTexture.Height - 2),
                 Color.Green);
            //Draw the box around the health bar
            MGame.SpriteBatch.Draw(healthTexture,
                new Vector2(camera.Position.X + 5, camera.Position.Y + 25),
               Color.White);
            //MANA
            MGame.SpriteBatch.Draw(fillManaTexture, new Rectangle((int)camera.Position.X + 6,
              (int)camera.Position.Y + 47, (healthTexture.Width - 2), healthTexture.Height - 2),
              Color.DimGray);
            //Draw the current mana level based on the current Mana
            MGame.SpriteBatch.Draw(currentManaTexture, new Rectangle((int)camera.Position.X + 6,
                 (int)camera.Position.Y + 47, (healthTexture.Width - 2) * MGame.hero.Mana / MGame.hero.MaxMana, healthTexture.Height - 2),
                 Color.LightBlue);
            //Draw the box around the mana bar
            MGame.SpriteBatch.Draw(healthTexture,
                new Vector2(camera.Position.X + 5, camera.Position.Y + 46),
               Color.White);

            MGame.SpriteBatch.Draw(logBackgroundTexture,
                new Vector2(camera.Position.X + 5, camera.Position.Y + 385),
               Color.White);

            if (gameLogQueueUpdated)
            {
                currentLog3 = currentLog2;
                currentLog2 = currentLog1;
                gameLogQueueUpdated = false;
            } if (gameLogQueue.Count > 0)
            {
                currentLog1 = gameLogQueue.Peek();
                gameLogQueue.Dequeue();
            }
            MGame.SpriteBatch.DrawString(font, currentLog1,
                   new Vector2((int)camera.Position.X + 10, (int)camera.Position.Y + 450), Color.White);
            MGame.SpriteBatch.DrawString(font, currentLog2,
                  new Vector2((int)camera.Position.X + 10, (int)camera.Position.Y + 420), Color.White);
            MGame.SpriteBatch.DrawString(font, currentLog3,
                  new Vector2((int)camera.Position.X + 10, (int)camera.Position.Y + 390), Color.White);

            MGame.SpriteBatch.End();
        }

        #endregion
    }
}

