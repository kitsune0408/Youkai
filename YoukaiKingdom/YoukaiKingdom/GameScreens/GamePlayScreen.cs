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

        private List<EnemySprite> enemySprites;

        //treasure chests
        private InteractionSprite treasureChest01;
        private InteractionSprite treasureChest02;
        private InteractionSprite treasureChest03;
        private InteractionSprite treasureChest04;

        private SpecialEffectSprite fireballSprite;
        private Texture2D fireballTexture;

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
        public List<InteractionSprite> Interactables;
        // ^ add all sprites from game screen to the list here

        private Dictionary<AnimationKey, Animation> animations;

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
            this.currentLog2 = string.Format("Welcome, {0}!", this.MGame.Engine.Hero.Name);
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
            Texture2D treasureChestTexture = MGame.Content.Load<Texture2D>("Sprites/Environment/TreasureChest");
            #endregion

            this.LoadAnimations();

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
            switch (this.MGame.heroType)
            {
                case CharacterType.Samurai:
                    {
                        this.playerSprite = this.MGame.Content.Load<Texture2D>("Sprites/PlayerClasses/Male_Samurai");
                        break;
                    }
                case CharacterType.Monk:
                    {
                        this.playerSprite = this.MGame.Content.Load<Texture2D>("Sprites/PlayerClasses/Male_Monk");
                        break;
                    }
                case CharacterType.Ninja:
                    {
                        this.playerSprite = this.MGame.Content.Load<Texture2D>("Sprites/PlayerClasses/Female_Ninja");
                        break;
                    }
            }
            #endregion

            //start animation dictionary
            #region Animation Dictionaries

            #endregion
            //end animation dictionary

            mPlayerSprite = new PlayerSprite(playerSprite, animations, this.MGame.Engine.Hero);

            fireballTexture = MGame.Content.Load<Texture2D>("Sprites/Spells/Spell_Fireball");
            var spellAnimation = new Animation(2, 50, 50, 0, 0);
            fireballSprite = new SpecialEffectSprite(fireballTexture, spellAnimation);

            //enemies
            #region Set Enemies

            this.MGame.Engine.Start();

            this.LoadEnemieSprites();

            #endregion

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

            //treasure chest
            treasureChest01 = new InteractionSprite(treasureChestTexture);
            treasureChest01.Position = new Vector2(1270, 30);
            treasureChest01.SetCollisionRectangle();
            Interactables = new List<InteractionSprite>()
            {
                (InteractionSprite) treasureChest01
            };

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
                smallForest04,
                treasureChest01
            };
            #endregion

            //add environment to the list of collisions
            foreach (var s in environmentSprites)
            {

                if (s.collisionRectangle == Rectangle.Empty)
                {
                    s.SetCollisionRectangle();
                }
                CollisionRectangles.Add(s.collisionRectangle);
            }

            mBackground.Load(MGame.GraphicsDevice, background);
            WorldHeight = mBackground.WorldHeight;
            WorldWidth = mBackground.WorldWidth;
        }

        private void LoadEnemieSprites()
        {
            this.enemySprites = new List<EnemySprite>();

            var evilNinjaTexture = this.MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_ninja");
            var evilMonkTexture = this.MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_onryo");
            var evilSamuraiTexture = this.MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_samurai");

            foreach (var enemy in this.MGame.Engine.Enemies)
            {
                if (enemy is NpcMage)
                {
                    this.enemySprites.Add(new EnemySprite(enemy, evilMonkTexture, this.animations));
                }
                else if (enemy is NpcRogue)
                {
                    this.enemySprites.Add(new EnemySprite(enemy, evilNinjaTexture, this.animations));
                }
                else if (enemy is NpcWarrior)
                {
                    this.enemySprites.Add(new EnemySprite(enemy, evilSamuraiTexture, this.animations));
                }
            }

            foreach (var e in this.enemySprites)
            {
                //enemy health
                e.fillHealthTexture = new Texture2D(this.MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                e.fillHealthTexture.SetData<Color>(new Color[] { Color.Red });
                e.currentHealthTexture = new Texture2D(this.MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                e.currentHealthTexture.SetData<Color>(new Color[] { Color.GreenYellow });
            }
        }

        private void LoadAnimations()
        {
            this.animations = new Dictionary<AnimationKey, Animation>();

            //walk animations
            this.animations.Add(AnimationKey.Down, new Animation(3, 48, 64, 0, 0));
            this.animations.Add(AnimationKey.Left, new Animation(3, 48, 64, 0, 64));
            this.animations.Add(AnimationKey.Right, new Animation(3, 48, 64, 0, 128));
            this.animations.Add(AnimationKey.Up, new Animation(3, 48, 64, 0, 192));

            //attack animations
            this.animations.Add(AnimationKey.AttackDown, new Animation(2, 48, 64, 144, 0));
            this.animations.Add(AnimationKey.AttackLeft, new Animation(2, 48, 64, 144, 64));
            this.animations.Add(AnimationKey.AttackRight, new Animation(2, 48, 64, 144, 128));
            this.animations.Add(AnimationKey.AttackUp, new Animation(2, 48, 64, 144, 192));
        }

        public override void Update(GameTime gameTime)
        {
            if ((!Paused) && this.MGame.gameStateScreen == GameState.GameScreenState)
            {
                if (this.MGame.Engine.Hero.Health <= 0)
                {
                    if (heroDeathMessage)
                    {
                        currentLog3 = currentLog2;
                        currentLog2 = currentLog1;
                        currentLog1 = string.Format("{0} died!", this.MGame.Engine.Hero.Name);
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
                        int prevHeroHealth = this.MGame.Engine.Hero.Health;
                        if (enemySprite.AttackingPlayer)
                        {
                            enemySprite.Enemy.Hit(this.MGame.Engine.Hero);
                            EnemySprite sprite = enemySprite;
                            if (this.MGame.Engine.Hero.Health != prevHeroHealth)
                            {
                                heroIsHit = true;
                            }
                            if (this.MGame.Engine.Hero.Health > 0 && heroIsHit)
                            {
                                AddToGameLog(string.Format("{0} hit {1} for {2} damage!",
                                    sprite.Enemy.Name, this.MGame.Engine.Hero.Name, this.MGame.Engine.Hero.DamageGotten));
                                heroIsHit = false;
                                prevHeroHealth = this.MGame.Engine.Hero.Health;
                            }
                        }
                    }
                }
                mPlayerSprite.Update(mPlayerSprite.previousPosition, gameTime, this);
                //define current position of the player for the camera to follow
                camera.Update(gameTime, mPlayerSprite, this);

                this.fireballSprite.Update(gameTime);

                if (this.MGame.Engine.Hero.Health > 0)
                {
                    if (this.CheckKey(Keys.D1))
                    {
                        EnemySprite enemyInVicinity = this.FindEnemy(this.mPlayerSprite.Hero.HitRange);

                        if (enemyInVicinity != null)
                        {
                            this.MGame.Engine.Hero.Hit(enemyInVicinity.Enemy);

                            this.AddToGameLog(string.Format("{0} hit {1} for {2} damage!",
                                this.MGame.Engine.Hero.Name, enemyInVicinity.Enemy.Name, enemyInVicinity.Enemy.DamageGotten));

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

                            if (enemyInVicinity != null && monk.FireballIsReady)
                            {
                                monk.CastFireball(enemyInVicinity.Enemy);
                                this.fireballSprite.IsOver = false;
                                this.fireballSprite.STimer = new Timer(1000);
                                this.fireballSprite.Position =
                                    new Vector2(enemyInVicinity.Position.X, enemyInVicinity.Position.Y + 10);

                                this.AddToGameLog(string.Format("{0} cast FIREBALL and hit {1} for {2} damage!",
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

            //Draw fireball when it's active
            fireballSprite.Draw(gameTime, MGame.SpriteBatch);

            //Draw player if alive
            if (this.MGame.Engine.Hero.Health > 0)
            {
                mPlayerSprite.Draw(gameTime, MGame.SpriteBatch);
            }
            MGame.SpriteBatch.DrawString(font, this.MGame.Engine.Hero.Name,
                   new Vector2((int)camera.Position.X + 5, (int)camera.Position.Y + 5), Color.White);

            MGame.SpriteBatch.Draw(fillHealthTexture, new Rectangle((int)camera.Position.X + 6,
                (int)camera.Position.Y + 26, (healthTexture.Width - 2), healthTexture.Height - 2),
                Color.Red);
            //Draw the current health level based on the current Health
            MGame.SpriteBatch.Draw(currentHealthTexture, new Rectangle((int)camera.Position.X + 6,
                 (int)camera.Position.Y + 26, (healthTexture.Width - 2) * this.MGame.Engine.Hero.Health / this.MGame.Engine.Hero.MaxHealth, healthTexture.Height - 2),
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
                 (int)camera.Position.Y + 47, (healthTexture.Width - 2) * this.MGame.Engine.Hero.Mana / this.MGame.Engine.Hero.MaxMana, healthTexture.Height - 2),
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

