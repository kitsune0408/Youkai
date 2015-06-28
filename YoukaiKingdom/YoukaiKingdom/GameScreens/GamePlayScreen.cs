using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.Logic.Interfaces;
using YoukaiKingdom.Logic.Models.Characters;
using YoukaiKingdom.Logic.Models.Characters.NPCs;
using YoukaiKingdom.Logic.Models.Inventory;
using YoukaiKingdom.Logic.Models.Items;
using YoukaiKingdom.Logic.Models.Items.Armors;
using YoukaiKingdom.Logic.Models.Items.Potions;
using YoukaiKingdom.Logic.Models.Items.Weapons;
using YoukaiKingdom.Sprites;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.Helpers;

namespace YoukaiKingdom.GameScreens
{

    using YoukaiKingdom.Logic.Models.Characters.Heroes;


    public class GamePlayScreen : BaseGameScreen
    {

        #region Fields

        public LevelNumber LevelNumber;
        private LevelManagement lme;
        public bool IsGuideVisible = false;
        private Timer deathTimer;

        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;
        private MouseState mouse;
        private MouseState lastMouseState;
        private Queue<string> gameLogQueue;
        private bool gameLogQueueUpdated;
        private bool isBossDead;

        private string currentLog1;
        private string currentLog2;
        private string currentLog3;
        private Texture2D logBackgroundTexture;
        private bool heroDeathMessage;

        private SpriteFont font;
        private SpriteFont smallFont;
        //==================
        //player
        private Texture2D playerSprite;
        private Texture2D throwableTexture;
        public PlayerSprite mPlayerSprite;
        //throwable weapons
        //private ThrowableSprite mThrowableSprite;
        //UI textures
        private Texture2D healthTexture;//player's health
        private Texture2D fillHealthTexture;
        private Texture2D currentHealthTexture;
        private Texture2D fillManaTexture;
        private Texture2D currentManaTexture;

        //treasure chests
        //private InteractionSprite treasureChest01;
        //private InteractionSprite treasureChest02;
        //private InteractionSprite treasureChest03;
        //private InteractionSprite treasureChest04;
        //private InteractionSprite hauntedHouseSprite;
        private Texture2D lootTexture;
        private List<string> lootList;

        private Treasure currentTreasure;
        private Texture2D takeButtonTexture;
        private Texture2D takeButtonHoverTexture;
        private Texture2D throwButtonTexture;
        private Texture2D throwButtonHoverTexture;
        private Texture2D cancelButtonTexture;
        private Texture2D cancelButtonHoverTexture;
        private Texture2D enterButtonTexture;
        private Texture2D enterButtonHoverTexture;
        private Button lootButton1;
        private Button lootButton2;
        private Button lootButton3;
        private Button lootButton4;
        private Button lootButton5;
        private Button cancelButton;
        private Button throwButton;
        private Button enterButton;
        private string messageText;

        private SpecialEffectSprite fireballSprite;
        private SpecialEffectSprite equalizerSprite;
        private SpecialEffectSprite protectingShadowSprite;
        private Texture2D fireballTexture;
        private Texture2D equaizerTexture;
        private Texture2D protectingShadowTexture;
        private SpecialEffectSprite enemySpellSprite;
        private Texture2D enemySpellTexture;

        //background
        private Background mBackground;

        public Camera Camera;

        private Texture2D guideBackgroundTexture;

        public List<EnemySprite> enemySprites;
        public List<Rectangle> CollisionRectangles;
        public List<Sprite> environmentSprites;
        public List<InteractionSprite> Interactables;

        // ^ add all sprites from game screen to the list here

        private Dictionary<AnimationKey, Animation> animations;
        private Dictionary<AnimationKey, Animation> bossAnimations;
        private InteractionType currentInteractionType;
        private InteractionSprite currentInteractionSprite;

        #endregion

        #region Constructors

        public GamePlayScreen(MainGame mGame)
            : base(mGame)
        {
            this.LevelNumber = LevelNumber.One;
            this.Interactables = new List<InteractionSprite>();
            Camera = new Camera(mGame.GraphicsDevice.Viewport);
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
            lme = new LevelManagement();
            this.LoadBackground(this.LevelNumber);
            //font setup
            this.font = MGame.Content.Load<SpriteFont>("Fonts/YoukaiFont");
            this.smallFont = MGame.Content.Load<SpriteFont>("Fonts/YoukaiFontSmall");
            this.gameLogQueue = new Queue<string>();
            if (this.LevelNumber == LevelNumber.One)
            {
                this.currentLog1 = "Please, destroy the monster which threatens our village!";
                this.currentLog2 = string.Format("Welcome, {0}!", this.MGame.Engine.Hero.Name);
                this.currentLog3 = "";
            }
            heroDeathMessage = true;
            this.logBackgroundTexture = MGame.Content.Load<Texture2D>("Sprites/UI/UI_LogBackground");
            this.deathTimer = new Timer(3000);

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
            //guide screen
            this.guideBackgroundTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Guide_message_field");
            this.takeButtonTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Guide_TakeButton");
            this.takeButtonHoverTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Guide_TakeButton_hover");
            this.throwButtonTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Guide_ThrowButton");
            this.throwButtonHoverTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Guide_ThrowButton_hover");
            this.cancelButtonTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Guide_CancelButton");
            this.cancelButtonHoverTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Guide_CancelButton_hover");
            this.enterButtonTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Guide_EnterButton");
            this.enterButtonHoverTexture = MGame.Content.Load<Texture2D>("Sprites/UI/Guide_EnterButton_hover");
            this.lootButton1 = new Button(takeButtonTexture, takeButtonHoverTexture, MGame.GraphicsDevice);
            this.lootButton2 = new Button(takeButtonTexture, takeButtonHoverTexture, MGame.GraphicsDevice);
            this.lootButton3 = new Button(takeButtonTexture, takeButtonHoverTexture, MGame.GraphicsDevice);
            this.lootButton4 = new Button(takeButtonTexture, takeButtonHoverTexture, MGame.GraphicsDevice);
            this.lootButton5 = new Button(takeButtonTexture, takeButtonHoverTexture, MGame.GraphicsDevice);
            this.throwButton = new Button(throwButtonTexture, throwButtonHoverTexture, MGame.GraphicsDevice);
            this.cancelButton = new Button(cancelButtonTexture, cancelButtonHoverTexture, MGame.GraphicsDevice);
            this.enterButton = new Button(enterButtonTexture, enterButtonHoverTexture, MGame.GraphicsDevice);
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

            mPlayerSprite = new PlayerSprite(playerSprite, animations, this.MGame.Engine.Hero);
            if (this.LevelNumber == LevelNumber.Two)
            {
                mPlayerSprite.Position = new Vector2(1000, 1900);
            }

            //Spells
            fireballTexture = MGame.Content.Load<Texture2D>("Sprites/Spells/Spell_Fireball");
            var spellAnimation = new Animation(2, 50, 50, 0, 0);
            fireballSprite = new SpecialEffectSprite(fireballTexture, spellAnimation);
            enemySpellTexture = MGame.Content.Load<Texture2D>("Sprites/Spells/Spell_Lightningball");
            enemySpellSprite = new SpecialEffectSprite(enemySpellTexture, spellAnimation);
            equaizerTexture = MGame.Content.Load<Texture2D>("Sprites/Spells/Spell_Equalizer");
            equalizerSprite = new SpecialEffectSprite(equaizerTexture, spellAnimation);
            protectingShadowTexture = MGame.Content.Load<Texture2D>("Sprites/Spells/Spell_ProtectingShadow");
            var protectionAnimation = new Animation(2, 48, 64, 0, 0);
            protectingShadowSprite = new SpecialEffectSprite(protectingShadowTexture, protectionAnimation);

            //Loot
            lootTexture = MGame.Content.Load<Texture2D>("Sprites/Inventory/Int_Loot");
            lootList = new List<string>();

            //enemies
            this.MGame.Engine.Start();
            var levCallDelegate = new LevelCallDelegate(LoadBackground);
            levCallDelegate += LoadEnvironment;
            levCallDelegate += LoadEnemySprites;
            levCallDelegate(this.LevelNumber);
        }

        private void LoadBackground(LevelNumber levelNumber)
        {
            switch (levelNumber)
            {
                case LevelNumber.One:
                    {
                        this.mBackground = new Background(4);
                        var background = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/Background01");
                        mBackground.Load(MGame.GraphicsDevice, background);
                    }
                    break;
                case LevelNumber.Two:
                    {
                        this.mBackground = new Background(1);
                        var background = MGame.Content.Load<Texture2D>("Sprites/Backgrounds/hauntedHouseBackground");
                        mBackground.Load(MGame.GraphicsDevice, background);
                    }
                    break;
            }
            WorldHeight = mBackground.WorldHeight;
            WorldWidth = mBackground.WorldWidth;
        }

        private void LoadEnvironment(LevelNumber levelNumber)
        {
            if (levelNumber == LevelNumber.One)
            {
                lme.LoadEnvironmentLevelOne(this, this.MGame);
            }
            else if (levelNumber == LevelNumber.Two)
            {
                lme.LoadEnvironmentLevelTwo(this, this.MGame);
            }

            //add environment to the list of collisions
            this.CollisionRectangles.Clear();
            foreach (var s in environmentSprites)
            {
                if (s.collisionRectangle == Rectangle.Empty)
                {
                    s.SetCollisionRectangle();
                }
                CollisionRectangles.Add(s.collisionRectangle);
            }
        }

        private void LoadEnemySprites(LevelNumber levelNumber)
        {
            this.MGame.Engine.LoadEnemies();
            this.enemySprites = new List<EnemySprite>();
            var evilNinjaTexture = this.MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_ninja");
            var evilMonkTexture = this.MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_monk");
            var evilSamuraiTexture = this.MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_samurai");
            var onryoTexture = this.MGame.Content.Load<Texture2D>("Sprites/Enemies/evil_onryo");
            var bossOniTexture = this.MGame.Content.Load<Texture2D>("Sprites/Enemies/Boss_Oni");

            foreach (var enemy in this.MGame.Engine.Enemies)
            {
                if (enemy is NpcMage)
                {
                    this.enemySprites.Add(new EnemySprite(enemy, evilMonkTexture, this.animations, 48, 64, false));
                }
                else if (enemy is NpcRogue)
                {
                    this.enemySprites.Add(new EnemySprite(enemy, evilNinjaTexture, this.animations, 48, 64, false));
                }
                else if (enemy is NpcWarrior)
                {
                    this.enemySprites.Add(new EnemySprite(enemy, evilSamuraiTexture, this.animations, 48, 64, false));
                }
            }

            foreach (var enemy in this.MGame.Engine.Bosses)
            {
                if (enemy.Name == "Oni")
                {
                    this.enemySprites.Add(new EnemySprite(enemy, bossOniTexture, this.bossAnimations, 74, 90, true));
                    break;
                }

                if (enemy.Name == "Onryo")
                {
                    this.enemySprites.Add(new EnemySprite(enemy, onryoTexture, this.bossAnimations, 74, 90, true));
                    break;
                }
            }

            foreach (var e in this.enemySprites)
            {
                //enemy health
                e.fillHealthTexture = new Texture2D(this.MGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                e.fillHealthTexture.SetData<Color>(new Color[] { Color.Red });
                e.currentHealthTexture = new Texture2D(this.MGame.GraphicsDevice, 1, 1, false,
                    SurfaceFormat.Color);
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

            this.bossAnimations = new Dictionary<AnimationKey, Animation>();
            //walk animations
            this.bossAnimations.Add(AnimationKey.Down, new Animation(3, 74, 89, 0, 0));
            this.bossAnimations.Add(AnimationKey.Left, new Animation(3, 74, 89, 0, 89));
            this.bossAnimations.Add(AnimationKey.Right, new Animation(3, 74, 89, 0, 178));
            this.bossAnimations.Add(AnimationKey.Up, new Animation(3, 74, 89, 0, 267));

            //attack animations
            this.bossAnimations.Add(AnimationKey.AttackDown, new Animation(2, 74, 89, 222, 0));
            this.bossAnimations.Add(AnimationKey.AttackLeft, new Animation(2, 74, 89, 222, 89));
            this.bossAnimations.Add(AnimationKey.AttackRight, new Animation(2, 74, 89, 222, 178));
            this.bossAnimations.Add(AnimationKey.AttackUp, new Animation(2, 74, 89, 222, 267));
        }

        public override void Update(GameTime gameTime)
        {
            if (this.MGame.GameStateScreen == GameState.GameScreenState)
            {
                if (IsGuideVisible)
                {
                    currentKeyboardState = Keyboard.GetState();
                    mouse = Mouse.GetState();
                    if (CheckKey(Keys.Enter))
                    {
                        if (currentInteractionSprite.Treasure.Items.Count > 0)
                        {
                            currentInteractionSprite.BeenInteractedWith = false;
                        }
                        else
                        {
                            currentInteractionSprite.BeenInteractedWith = true;
                            if (currentInteractionSprite.InteractionType == InteractionType.Loot)
                            {
                                this.Interactables.Remove(currentInteractionSprite);
                            }
                            currentInteractionSprite = null;
                        }
                        IsGuideVisible = false;

                    }
                    if (currentInteractionType != InteractionType.Entrance)
                    {
                        throwButton.Update(currentKeyboardState, mouse, (int)this.Camera.Position.X,
                            (int)this.Camera.Position.Y);
                        cancelButton.Update(currentKeyboardState, mouse, (int)this.Camera.Position.X,
                            (int)this.Camera.Position.Y);
                        if (throwButton.isClicked)
                        {
                            if (CheckMouse())
                            {
                                if (currentInteractionSprite != null)
                                {
                                    currentInteractionSprite.Treasure.Items.Clear();
                                    DrawLootList(currentInteractionSprite);
                                }
                            }
                        }

                        if (cancelButton.isClicked)
                        {
                            if (CheckMouse())
                            {
                                if (currentInteractionSprite != null && currentInteractionSprite.Treasure.Items.Count > 0)
                                {
                                    currentInteractionSprite.BeenInteractedWith = false;
                                }
                                else
                                {
                                    if (currentInteractionSprite != null)
                                    {
                                        currentInteractionSprite.BeenInteractedWith = true;
                                        if (currentInteractionSprite.InteractionType == InteractionType.Loot)
                                        {
                                            this.Interactables.Remove(currentInteractionSprite);
                                        }
                                    }
                                    currentInteractionSprite = null;
                                }
                                IsGuideVisible = false;
                            }
                        }

                        if (lootList.Count >= 1)
                        {
                            lootButton1.Update(currentKeyboardState, mouse, (int)this.Camera.Position.X,
                                (int)this.Camera.Position.Y);
                            if (lootButton1.isClicked)
                            {
                                if (CheckMouse())
                                    UpdateLootList(0);
                            }
                            if (lootList.Count >= 2)
                            {
                                lootButton2.Update(currentKeyboardState, mouse, (int)this.Camera.Position.X,
                                    (int)this.Camera.Position.Y);
                                if (lootButton2.isClicked)
                                {
                                    if (CheckMouse())
                                        UpdateLootList(1);
                                }
                                if (lootList.Count >= 3)
                                {
                                    lootButton3.Update(currentKeyboardState, mouse, (int)this.Camera.Position.X,
                                        (int)this.Camera.Position.Y);
                                    if (lootButton3.isClicked)
                                    {
                                        if (CheckMouse())
                                            UpdateLootList(2);
                                    }
                                    if (lootList.Count >= 4)
                                    {
                                        lootButton4.Update(currentKeyboardState, mouse, (int)this.Camera.Position.X,
                                            (int)this.Camera.Position.Y);
                                        if (lootButton4.isClicked)
                                        {
                                            if (CheckMouse())
                                                UpdateLootList(3);
                                        }
                                        if (lootList.Count == 5)
                                        {
                                            lootButton5.Update(currentKeyboardState, mouse, (int)this.Camera.Position.X,
                                                (int)this.Camera.Position.Y);
                                            if (lootButton5.isClicked)
                                            {
                                                if (CheckMouse())
                                                    UpdateLootList(4);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (isBossDead)
                        {
                            enterButton.Update(currentKeyboardState, mouse, (int)this.Camera.Position.X,
                                (int)this.Camera.Position.Y);
                            if (enterButton.isClicked)
                            {
                                if (CheckMouse())
                                {
                                    StartNextLevel(LevelNumber + 1);
                                }
                                IsGuideVisible = false;
                            }
                        }
                        cancelButton.Update(currentKeyboardState, mouse, (int)this.Camera.Position.X,
                                 (int)this.Camera.Position.Y);
                        if (cancelButton.isClicked)
                        {
                            if (CheckMouse())
                            {

                                if (currentInteractionSprite != null) currentInteractionSprite.BeenInteractedWith = false;
                                IsGuideVisible = false;
                            }
                        }
                    }
                    this.lastKeyboardState = this.currentKeyboardState;
                    this.lastMouseState = this.mouse;
                }
                else
                {
                    if (!Paused)
                    {
                        #region Hero Died
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
                        #endregion

                        currentKeyboardState = Keyboard.GetState();
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            Paused = true;
                            MGame.GameStateScreen = GameState.PauseScreenState;
                        }

                        if (CheckKey(Keys.I))
                        {
                            Paused = true;
                            MGame.InventoryScreen.CalledWithFastButton = true;
                            MGame.GameStateScreen = GameState.InventoryScreenState;
                        }

                        //if (CheckKey(Keys.R))
                        //{
                        //   StartNextLevel(this.LevelNumber+1);
                        //}

                        #region Enemy Attacks Hero

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
                                    if (enemySprite.Enemy.GetType().Name == "NpcMage")
                                    {
                                        this.enemySpellSprite.StartTimer(1000);
                                        this.enemySpellSprite.Position =
                                            new Vector2(mPlayerSprite.Position.X, mPlayerSprite.Position.Y + 10);
                                    }
                                    EnemySprite sprite = enemySprite;
                                    if (this.MGame.Engine.Hero.Health != prevHeroHealth)
                                    {
                                        heroIsHit = true;
                                    }
                                    if (this.MGame.Engine.Hero.Health > 0 && heroIsHit)
                                    {
                                        AddToGameLog(string.Format("{0} hit {1} for {2} damage!",
                                            sprite.Enemy.Name, this.MGame.Engine.Hero.Name, this.MGame.Engine.Hero.DamageGotten));
                                    }
                                }
                            }
                        }
                        #endregion

                        mPlayerSprite.Update(mPlayerSprite.previousPosition, gameTime, this);
                        //define current position of the player for the camera to follow
                        Camera.Update(gameTime, mPlayerSprite, this);

                        this.fireballSprite.Update(gameTime);
                        this.enemySpellSprite.Update(gameTime);
                        this.equalizerSprite.Update(gameTime);
                        this.protectingShadowSprite.Position = new Vector2(mPlayerSprite.Position.X, mPlayerSprite.Position.Y);
                        this.protectingShadowSprite.Update(gameTime);

                        if (this.MGame.Engine.Hero.Health > 0)
                        {
                            if (CheckKey(Keys.E))
                            {
                                CheckInteractables();
                            }

                            #region Hero Attacks Enemy
                            if (this.CheckKey(Keys.D1))
                            {
                                EnemySprite enemyInVicinity = this.FindEnemy(this.mPlayerSprite.Hero.HitRange);

                                if (enemyInVicinity != null)
                                {
                                    //hit player
                                    bool enemyIsHit = false;
                                    int prevEnemyHealth = enemyInVicinity.Enemy.Health;
                                    this.MGame.Engine.Hero.Hit(enemyInVicinity.Enemy);
                                    if (enemyInVicinity.Enemy.Health != prevEnemyHealth)
                                    {
                                        enemyIsHit = true;
                                    }
                                    if (enemyInVicinity.Enemy.Health > 0 && enemyIsHit)
                                    {
                                        this.AddToGameLog(string.Format("{0} hit {1} for {2} damage!",
                                            this.MGame.Engine.Hero.Name, enemyInVicinity.Enemy.Name,
                                            enemyInVicinity.Enemy.DamageGotten));
                                    }
                                    if (enemyInVicinity.Enemy.Health <= 0)
                                    {
                                        if (enemyInVicinity.IsBoss)
                                        {
                                            this.mPlayerSprite.Hero.CheckLevelUp(5);
                                            isBossDead = true;
                                        }
                                        else
                                        {
                                            this.mPlayerSprite.Hero.CheckLevelUp(1);
                                        }
                                        DropLoot(enemyInVicinity);
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
                                        if (monk.FireballIsReady && monk.CastFireball(enemyInVicinity.Enemy))
                                        {
                                            this.fireballSprite.StartTimer(1000);
                                            this.fireballSprite.Position =
                                                new Vector2(enemyInVicinity.Position.X, enemyInVicinity.Position.Y + 10);
                                            this.AddToGameLog(string.Format("{0} cast FIREBALL and hit {1} for {2} damage!",
                                                   monk.Name, enemyInVicinity.Enemy.Name, enemyInVicinity.Enemy.DamageGotten));
                                        }

                                        if (enemyInVicinity.Enemy.Health <= 0)
                                        {
                                            if (enemyInVicinity.IsBoss)
                                            {
                                                this.mPlayerSprite.Hero.CheckLevelUp(5);
                                                isBossDead = true;
                                            }
                                            else
                                            {
                                                this.mPlayerSprite.Hero.CheckLevelUp(1);
                                            }
                                            DropLoot(enemyInVicinity);
                                            this.AddToGameLog(string.Format("{0} is dead!", enemyInVicinity.Enemy.Name));
                                        }
                                    }

                                }
                                if (this.mPlayerSprite.Hero is Samurai)
                                {
                                    var samurai = (Samurai)this.mPlayerSprite.Hero;
                                    EnemySprite enemyInVicinity = this.FindEnemy(samurai.MagicHitCastRange);

                                    if (enemyInVicinity != null)
                                    {
                                        if (samurai.EqualizerIsReady && samurai.CastЕqualizer(enemyInVicinity.Enemy))
                                        {
                                            this.equalizerSprite.StartTimer(1000);
                                            this.equalizerSprite.Position =
                                                new Vector2(enemyInVicinity.Position.X, enemyInVicinity.Position.Y + 10);
                                            this.AddToGameLog(string.Format("{0} used EQUALIZER and hit {1} for {2} damage!",
                                             samurai.Name, enemyInVicinity.Enemy.Name, enemyInVicinity.Enemy.DamageGotten));
                                        }
                                        if (enemyInVicinity.Enemy.Health <= 0)
                                        {
                                            if (enemyInVicinity.IsBoss)
                                            {
                                                this.mPlayerSprite.Hero.CheckLevelUp(5);
                                                isBossDead = true;
                                            }
                                            else
                                            {
                                                this.mPlayerSprite.Hero.CheckLevelUp(1);
                                            }
                                            DropLoot(enemyInVicinity);
                                            this.AddToGameLog(string.Format("{0} is dead!", enemyInVicinity.Enemy.Name));
                                        }
                                    }
                                }

                                if (this.mPlayerSprite.Hero is Ninja)
                                {
                                    var ninja = (Ninja)this.mPlayerSprite.Hero;

                                    if (this.protectingShadowSprite.IsOver && ninja.ProtectedOfDamageIsReady && ninja.CastProtectedOfDamage())
                                    {
                                        this.protectingShadowSprite.StartTimer(5000);
                                        // this.protectingShadowSprite.Position =
                                        //     new Vector2(enemyInVicinity.Position.X, enemyInVicinity.Position.Y + 10);
                                        this.AddToGameLog(string.Format("{0} used PROTECTING SHADOW!",
                                         ninja.Name));
                                    }

                                }
                            }
                            #endregion
                        }
                        this.lastKeyboardState = this.currentKeyboardState;
                        this.lastMouseState = this.mouse;
                    }
                }
            }
        }

        public void UpdateLootList(int i)
        {
            if (!mPlayerSprite.Hero.Inventory.IsFull)
            {
                Item takenItem = currentTreasure.Items[i];
                mPlayerSprite.Hero.Inventory.AddItemToBag(takenItem);
                MGame.InventoryScreen.FillBag();
                currentTreasure.Items.Remove(takenItem);
                DrawLootList(currentInteractionSprite);
            }
        }

        public void CheckInteractables()
        {
            var interRect = new Rectangle((int)this.mPlayerSprite.Position.X - 20,
                (int)this.mPlayerSprite.Position.Y - 20,
                68, 84);
            foreach (var sprite in this.Interactables)
            {
                if (interRect.Intersects(sprite.collisionRectangle) && !sprite.BeenInteractedWith)
                {
                    lootList.Clear();
                    sprite.BeenInteractedWith = true;
                    this.IsGuideVisible = true;
                    currentInteractionType = sprite.InteractionType;
                    currentInteractionSprite = sprite;

                    if (sprite.InteractionType != InteractionType.Entrance)
                    {
                        if (sprite.InteractionType == InteractionType.Chest)
                        {
                            this.currentTreasure =
                            this.MGame.Engine.Loot.TreasureChests.FirstOrDefault(
                                x => x.Location.X == sprite.collisionRectangle.X && x.Location.Y == sprite.collisionRectangle.Y);
                        }
                        else if (sprite.InteractionType == InteractionType.Loot)
                        {
                            this.currentTreasure = this.MGame.Engine.Loot.Treasure;
                        }

                        if (this.currentTreasure != null)
                        {
                            sprite.Treasure = this.currentTreasure;
                            messageText = "You found loot! Choose what to take.";
                            lootButton1.SetPosition(new Vector2((int)Camera.Position.X + 550,
                                (int)Camera.Position.Y + 130));
                            lootButton2.SetPosition(new Vector2((int)Camera.Position.X + 550,
                                (int)Camera.Position.Y + 165));
                            lootButton3.SetPosition(new Vector2((int)Camera.Position.X + 550,
                                (int)Camera.Position.Y + 200));
                            lootButton4.SetPosition(new Vector2((int)Camera.Position.X + 550,
                                (int)Camera.Position.Y + 235));
                            lootButton5.SetPosition(new Vector2((int)Camera.Position.X + 550,
                                (int)Camera.Position.Y + 270));
                            throwButton.SetPosition(new Vector2((int)Camera.Position.X + 370,
                                (int)Camera.Position.Y + 340));
                            cancelButton.SetPosition(new Vector2((int)Camera.Position.X + 470,
                                (int)Camera.Position.Y + 340));
                            DrawLootList(sprite);
                        }
                    }
                    else
                    {
                        if (isBossDead)
                        {
                            messageText = string.Format("Do you want to leave this place \nand enter {0}?", sprite.Name);
                        }
                        else
                        {
                            messageText = "You cannot leave this place \nuntil the youkai is alive!";
                        }
                        enterButton.SetPosition(new Vector2((int)Camera.Position.X + 370,
                           (int)Camera.Position.Y + 340));
                        cancelButton.SetPosition(new Vector2((int)Camera.Position.X + 470,
                            (int)Camera.Position.Y + 340));
                    }
                }
            }
        }

        private void StartNextLevel(LevelNumber level)
        {
            this.MGame.Engine.NextLevel();
            this.LevelNumber = level;
            this.CollisionRectangles.Clear();
            this.environmentSprites.Clear();
            this.enemySprites.Clear();
            //load new
            //this.LoadBackground();
            //this.LoadEnvironment();  
            this.LoadContent();
        }

        private void DrawLootList(InteractionSprite sprite)
        {
            lootList.Clear();
            for (int i = 0; i < sprite.Treasure.Items.Count; i++)
            {
                var t = sprite.Treasure.Items[i];

                if (t is IWeapon)
                {
                    Weapon temp = (Weapon)t;
                    lootList.Add(String.Format("{0} \"{1}\": damage {2}",
                        temp.GetType().Name, temp.Name, temp.AttackPoints));
                }
                if (t is IArmor)
                {
                    Armor temp = (Armor)t;
                    lootList.Add(String.Format("{0} \"{1}\": defence {2}",
                        temp.GetType().Name, temp.Name, temp.DefensePoints));
                }
                if (t is HealingPotion)
                {
                    HealingPotion temp = (HealingPotion)t;
                    lootList.Add(String.Format("{0} \"{1}\": healing points {2}",
                        temp.GetType().Name, temp.Name, temp.HealingPoints));
                }

                if (t is ManaPotion)
                {
                    ManaPotion temp = (ManaPotion)t;
                    lootList.Add(String.Format("{0} \"{1}\": mana points {2}",
                        temp.GetType().Name, temp.Name, temp.ManaPoints));
                }
            }
        }

        private void DropLoot(EnemySprite deadEnemy)
        {
            Location deadEnemyLocation = new Location(deadEnemy.Position.X, deadEnemy.Position.Y);
            this.MGame.Engine.Loot.GenerateTreasureBag(deadEnemyLocation);
            if (MGame.Engine.Loot.HasLoot)
            {
                InteractionSprite lootSprite = new InteractionSprite(lootTexture, InteractionType.Loot);
                lootSprite.Position = new Vector2(deadEnemy.Position.X, deadEnemy.Position.Y);
                lootSprite.Treasure = MGame.Engine.Loot.Treasure;//.TreasureBags.ToArray()[droppedLoot];
                Interactables.Add(lootSprite);
                lootSprite.collisionRectangle = new Rectangle((int)lootSprite.Position.X, (int)lootSprite.Position.Y,
                    lootTexture.Width / 2, lootTexture.Height);
                environmentSprites.Add(lootSprite);
                //droppedLoot += 1;
            }
        }

        private void DeathTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.deathTimer.Enabled = false;
            this.MGame.GameStateScreen = GameState.GameOverState;
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
        private bool CheckMouse()
        {
            return this.mouse.LeftButton == ButtonState.Pressed
                && this.lastMouseState.LeftButton == ButtonState.Released;
        }

        public override void Draw(GameTime gameTime)
        {
            MGame.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);
            mBackground.Draw(MGame.SpriteBatch);
            //things are drawn according to their order: i.e if castle is drawn before player, player will walk over it
            foreach (var s in environmentSprites)
            {
                if (s is InteractionSprite)
                {
                    InteractionSprite intS = s as InteractionSprite;
                    if (intS.InteractionType == InteractionType.Loot && !intS.BeenInteractedWith)
                    {
                        intS.Draw(MGame.SpriteBatch);
                    }
                }
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
            if (this.MGame.Engine.Hero.Health > 0)
            {
                mPlayerSprite.Draw(gameTime, MGame.SpriteBatch);
            }

            //Draw spell effects when its' active
            fireballSprite.Draw(gameTime, MGame.SpriteBatch);
            equalizerSprite.Draw(gameTime, MGame.SpriteBatch);
            enemySpellSprite.Draw(gameTime, MGame.SpriteBatch);
            protectingShadowSprite.Draw(gameTime, MGame.SpriteBatch);

            MGame.SpriteBatch.DrawString(font, this.MGame.Engine.Hero.Name,
                   new Vector2((int)Camera.Position.X + 5, (int)Camera.Position.Y + 5), Color.White);

            MGame.SpriteBatch.Draw(fillHealthTexture, new Rectangle((int)Camera.Position.X + 6,
                (int)Camera.Position.Y + 26, (healthTexture.Width - 2), healthTexture.Height - 2),
                Color.Red);
            //Draw the current health level based on the current Health
            MGame.SpriteBatch.Draw(currentHealthTexture, new Rectangle((int)Camera.Position.X + 6,
                 (int)Camera.Position.Y + 26, (healthTexture.Width - 2) * this.MGame.Engine.Hero.Health / this.MGame.Engine.Hero.MaxHealth, healthTexture.Height - 2),
                 Color.Green);
            //Draw the box around the health bar
            MGame.SpriteBatch.Draw(healthTexture,
                new Vector2(Camera.Position.X + 5, Camera.Position.Y + 25),
               Color.White);
            //MANA
            MGame.SpriteBatch.Draw(fillManaTexture, new Rectangle((int)Camera.Position.X + 6,
              (int)Camera.Position.Y + 47, (healthTexture.Width - 2), healthTexture.Height - 2),
              Color.DimGray);
            //Draw the current mana level based on the current Mana
            MGame.SpriteBatch.Draw(currentManaTexture, new Rectangle((int)Camera.Position.X + 6,
                 (int)Camera.Position.Y + 47, (healthTexture.Width - 2) * this.MGame.Engine.Hero.Mana / this.MGame.Engine.Hero.MaxMana, healthTexture.Height - 2),
                 Color.LightBlue);
            //Draw the box around the mana bar
            MGame.SpriteBatch.Draw(healthTexture,
                new Vector2(Camera.Position.X + 5, Camera.Position.Y + 46),
               Color.White);
            MGame.SpriteBatch.DrawString(font, "Level: " + this.MGame.Engine.Hero.Level,
                  new Vector2((int)Camera.Position.X + 5, (int)Camera.Position.Y + 68), Color.White);


            MGame.SpriteBatch.Draw(logBackgroundTexture,
                new Vector2(Camera.Position.X + 345, Camera.Position.Y + 415),
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
                   new Vector2((int)Camera.Position.X + 350, (int)Camera.Position.Y + 450), Color.White);
            MGame.SpriteBatch.DrawString(smallFont, currentLog2,
                  new Vector2((int)Camera.Position.X + 350, (int)Camera.Position.Y + 435), Color.White);
            MGame.SpriteBatch.DrawString(smallFont, currentLog3,
                   new Vector2((int)Camera.Position.X + 350, (int)Camera.Position.Y + 420), Color.White);

            if (this.IsGuideVisible)
            {
                MGame.SpriteBatch.Draw(guideBackgroundTexture,
                new Vector2(Camera.Position.X + 250, Camera.Position.Y + 90),
                Color.White);
                MGame.SpriteBatch.DrawString(font, messageText,
                                   new Vector2((int)Camera.Position.X + 290,
                                       (int)Camera.Position.Y + 100), Color.White);
                //MGame.SpriteBatch.DrawString(font, "Press ENTER to continue. \nAll remaining items will be discarded!",
                //                   new Vector2((int)Camera.Position.X + 290,
                //                       (int)Camera.Position.Y + 310), Color.White);

                if (currentInteractionSprite.InteractionType != InteractionType.Entrance)
                {
                    for (int i = 0; i < lootList.Count; i++)
                    {
                        MGame.SpriteBatch.DrawString(smallFont, lootList[i],
                                        new Vector2((int)Camera.Position.X + 270,
                                            (int)Camera.Position.Y + 135 + i * 35), Color.White);
                        //lootButtons[i].Draw(MGame.SpriteBatch);
                    }
                    if (lootList.Count >= 1)
                    {
                        lootButton1.Draw(MGame.SpriteBatch);
                        if (lootList.Count >= 2)
                        {
                            lootButton2.Draw(MGame.SpriteBatch);
                            if (lootList.Count >= 3)
                            {
                                lootButton3.Draw(MGame.SpriteBatch);
                                if (lootList.Count >= 4)
                                {
                                    lootButton4.Draw(MGame.SpriteBatch);
                                    if (lootList.Count == 5)
                                    {
                                        lootButton5.Draw(MGame.SpriteBatch);
                                    }
                                }
                            }
                        }
                    }
                    throwButton.Draw(MGame.SpriteBatch);
                }
                else
                {
                    enterButton.Draw(MGame.SpriteBatch);
                }
                cancelButton.Draw(MGame.SpriteBatch);
            }

            MGame.SpriteBatch.End();
        }

        #endregion
    }
}

