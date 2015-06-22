using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
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
        private Timer deathTimer;

        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;
        private Queue<string> gameLogQueue;
        private bool gameLogQueueUpdated;

        private string currentLog1;
        private string currentLog2;
        private string currentLog3;
        private Texture2D logBackgroundTexture;
        private bool heroDeathMessage;

        private SpriteFont font;
        private SpriteFont smallFont;
        //==================

        //textures
        private Texture2D playerSprite;
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
        private Texture2D manaPotionTexture;
        private Texture2D healingPotionTexture;

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
        private Background mBackground;

        Camera camera;
        public List<Rectangle> CollisionRectangles;
        // ^ add all sprites from game screen to the list here

        #endregion

        #region Constructors

        public GamePlayScreen(MainGame mGame, Hero hero)
            : base(mGame)
        {
            //this.hero = mGame.hero;
            camera = new Camera(mGame.GraphicsDevice.Viewport);
            CollisionRectangles = new List<Rectangle>();
            MGame.PauseMenuScreen = new PauseMenuScreen(MGame, this);
            MGame.Components.Add(MGame.PauseMenuScreen);
            MGame.PauseMenuScreen.Initialize();
        }

        #endregion

        #region Properties

        //for Camera
        public int WorldWidth { get; private set; }
        public int WorldHeight { get; private set; }

        #endregion

        #region Methods

        protected override void LoadContent()
        {
            this.mBackground = new Background(4);
            var background = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/Background01");
            //font setup
            this.font = MGame.Content.Load<SpriteFont>("Fonts/YoukaiFont");
            this.smallFont = MGame.Content.Load<SpriteFont>("Fonts/YoukaiFontSmall");
            this.gameLogQueue = new Queue<string>();
            this.currentLog1 = "Please, destroy the monster which threatens our village!";
            this.currentLog2 = string.Format("Welcome, {0}!", MGame.Hero.Name);
            this.currentLog3 = "";
            heroDeathMessage = true;
            this.logBackgroundTexture = MGame.Content.Load<Texture2D>("Sprites/UI/UI_LogBackground");
            this.deathTimer = new Timer(3000);

            this.healingPotionTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_HealingPotion");
            this.manaPotionTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Inv_ManaPotion");
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

            mPlayerSprite = new PlayerSprite(playerSprite, animations, MGame.Hero);
             

            //enemies
            #region Set Enemies

            var evilNinjaTexture = MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_ninja");
            evilNinjaNpc01 = new NpcRogue(1, "Mook", 400, 0, 100, 50, new Location(1200, 300));
            evilNinjaNpc01.HitRange = 1;
            evilNinjaNpc02 = new NpcRogue(1, "Mook", 500, 0, 100, 75, new Location(1200, 800));
            evilNinjaNpc01.HitRange = 1;
            evilNinjaNpc03 = new NpcRogue(1, "Mook", 400, 0, 100, 50, new Location(1200, 305));
            mEvilNinjaSprite01 = new EnemySprite(evilNinjaNpc01, evilNinjaTexture, animations);
            mEvilNinjaSprite02 = new EnemySprite(evilNinjaNpc02, evilNinjaTexture, animations);
            
            mEvilNinjaSprite01.SetPatrollingArea(200, 200, 150);
            mEvilNinjaSprite02.SetPatrollingArea(200, 200, 100);

            #endregion

            enemySprites = new List<EnemySprite> 
            {
                mEvilNinjaSprite01, 
                mEvilNinjaSprite02
            };
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
                CollisionRectangles.Add(new Rectangle
               ((int)s.Position.X, (int)s.Position.Y,
               s.mSpriteTexture.Width, s.mSpriteTexture.Height));
            }

            mBackground.Load(MGame.GraphicsDevice, background);
            WorldHeight = mBackground.WorldHeight;
            WorldWidth = mBackground.WorldWidth;
        }

        public override void Update(GameTime gameTime)
        {
            if ((!Paused) && this.MGame.gameStateScreen == GameState.GameScreenState)
            {
                if (MGame.Hero.Health <= 0)
                {
                    if (heroDeathMessage)
                    {
                        currentLog3 = currentLog2;
                        currentLog2 = currentLog1;
                        currentLog1 = string.Format("{0} died!", MGame.Hero.Name);
                        heroDeathMessage = false;
                    }
                    this.deathTimer.Elapsed += new ElapsedEventHandler(DeathTimerElapsed);
                    this.deathTimer.Enabled = true; // Enable timer
                }
                currentKeyboardState = Keyboard.GetState();
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Paused = true;
                    MGame.gameStateScreen = GameState.PauseScreenState;
                }

                foreach (var enemySprite in enemySprites)
                {
                    if (enemySprite.Enemy.Health > 0)
                    {
                        enemySprite.CheckOnTargets(mPlayerSprite);
                        enemySprite.Update(gameTime, this);
                        //hit player
                        bool heroIsHit = false;
                        int prevHeroHealth = MGame.Hero.Health;
                        if (enemySprite.AttackingPlayer)
                        {
                            enemySprite.Enemy.Hit(this.MGame.Hero);
                            EnemySprite sprite = enemySprite;
                            if (MGame.Hero.Health != prevHeroHealth)
                            {
                                heroIsHit = true;
                            }
                            if (this.MGame.Hero.Health > 0 && heroIsHit)
                            {
                                AddToGameLog(string.Format("{0} hit {1} for {2} damage!",
                                    sprite.Enemy.Name, this.MGame.Hero.Name, MGame.Hero.DamageGotten));
                                heroIsHit = false;
                                prevHeroHealth = MGame.Hero.Health;
                            }
                        }
                    }
                }

                mPlayerSprite.Update(mPlayerSprite.previousPosition, gameTime, this);
                //define current position of the player for the camera to follow
                camera.Update(gameTime, mPlayerSprite, this);

                if (this.MGame.Hero.Health > 0)
                {

                    if (this.CheckKey(Keys.D1))
                    {
                        EnemySprite enemyInVicinity = this.FindEnemy(this.mPlayerSprite.Hero.HitRange);

                        if (enemyInVicinity != null)
                        {
                            this.MGame.Hero.Hit(enemyInVicinity.Enemy);
                            this.AddToGameLog(string.Format("{0} hit {1} for {2} damage!",
                                this.MGame.Hero.Name, enemyInVicinity.Enemy.Name, enemyInVicinity.Enemy.DamageGotten));
                            if (enemyInVicinity.Enemy.Health <= 0)
                            {
                                this.AddToGameLog(string.Format("{0} is dead!", enemyInVicinity.Enemy.Name));
                            }
                        }
                    }

                    if (this.CheckKey(Keys.D2))
                    {
                        if (this.mPlayerSprite.Hero is Monk)
                        {
                            var monk = (Monk)this.mPlayerSprite.Hero;
                            EnemySprite enemyInVicinity = this.FindEnemy(monk.FireballCastRange);

                            if (enemyInVicinity != null)
                            {
                                monk.CastFireball(enemyInVicinity.Enemy);
                                this.AddToGameLog(string.Format("{0} hit {1} for {2} damage!",
                                       monk.Name, enemyInVicinity.Enemy.Name, enemyInVicinity.Enemy.DamageGotten));
                                if (enemyInVicinity.Enemy.Health <= 0)
                                {
                                    this.AddToGameLog(string.Format("{0} is dead!", enemyInVicinity.Enemy.Name));
                                }
                            }
                        }
                    }
                }

                this.lastKeyboardState = this.currentKeyboardState;
            }
        }

        private void DeathTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.deathTimer.Enabled = false;
            this.MGame.gameStateScreen = GameState.GameOverState;
        }


        private void AddToGameLog(string log)
        {
            this.gameLogQueue.Enqueue(log);
            this.gameLogQueueUpdated = true;
        }

        private EnemySprite FindEnemy(int range)
        {
            //create rectange for hit range
            //hero range is multiplied 30 times for the game screen
            int positionX = (int)this.mPlayerSprite.Position.X - range * 20;
            int positionY = (int)this.mPlayerSprite.Position.Y - range * 20;
            int rectW = 48 + range * 40;
            int rectH = 64 + range * 40;
            var rangeRect = new Rectangle(positionX, positionY, rectW, rectH);
            return this.enemySprites.Where(e => e.Enemy.Health > 0).
                FirstOrDefault(e => rangeRect.Intersects(e.collisionRectangle));
        }

        private bool CheckKey(Keys key)
        {
            return this.lastKeyboardState.IsKeyDown(key) && this.currentKeyboardState.IsKeyUp(key);
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
            //Draw player if alive
            if (MGame.Hero.Health > 0)
            {
                mPlayerSprite.Draw(gameTime, MGame.SpriteBatch);
            }
            MGame.SpriteBatch.DrawString(font, MGame.Hero.Name,
                   new Vector2((int)camera.Position.X + 5, (int)camera.Position.Y + 5), Color.White);

            MGame.SpriteBatch.Draw(fillHealthTexture, new Rectangle((int)camera.Position.X + 6,
                (int)camera.Position.Y + 26, (healthTexture.Width - 2), healthTexture.Height - 2),
                Color.Red);
            //Draw the current health level 6based on the current Health
            MGame.SpriteBatch.Draw(currentHealthTexture, new Rectangle((int)camera.Position.X + 6,
                 (int)camera.Position.Y + 26, (healthTexture.Width - 2) * MGame.Hero.Health / MGame.Hero.MaxHealth, healthTexture.Height - 2),
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
                 (int)camera.Position.Y + 47, (healthTexture.Width - 2) * MGame.Hero.Mana / MGame.Hero.MaxMana, healthTexture.Height - 2),
                 Color.LightBlue);
            //Draw the box around the mana bar
            MGame.SpriteBatch.Draw(healthTexture,
                new Vector2(camera.Position.X + 5, camera.Position.Y + 46),
               Color.White);

            MGame.SpriteBatch.Draw(logBackgroundTexture,
                new Vector2(camera.Position.X + 345, camera.Position.Y + 415),
               Color.White);

            if (gameLogQueueUpdated)
            {
                currentLog3 = currentLog2;
                currentLog2 = currentLog1;
                gameLogQueueUpdated = false;
            }
            if (gameLogQueue.Count > 0)
            {
                currentLog1 = gameLogQueue.Peek();
                gameLogQueue.Dequeue();
            }
            MGame.SpriteBatch.DrawString(smallFont, currentLog1,
                   new Vector2((int)camera.Position.X + 350, (int)camera.Position.Y + 450), Color.White);
            MGame.SpriteBatch.DrawString(smallFont, currentLog2,
                  new Vector2((int)camera.Position.X + 350, (int)camera.Position.Y + 435), Color.White);
            MGame.SpriteBatch.DrawString(smallFont, currentLog3,
                  new Vector2((int)camera.Position.X + 350, (int)camera.Position.Y + 420), Color.White);

            MGame.SpriteBatch.End();
        }

        #endregion
    }
}

