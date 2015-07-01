using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.GameScreens;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.Logic.Models.Items.Armors;
using YoukaiKingdom.Logic.Models.Items.Weapons;
using YoukaiKingdom.Sprites;

namespace YoukaiKingdom.GameLogic
{
    internal class LevelManagement
    {
        public void LoadEnvironmentLevelOne(GamePlayScreen gamePlayScreen, MainGame mGame)
        {
            var castleTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/Castle");
            var forestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest01");
            var bigForestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest_02_big");
            var vertForestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest_03_vert");
            var smallForestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest04_small");
            var longForestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest_05_long");

            var houseTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/old_house");
            var horWallTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/horisontal_wall");
            var verWallShortTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/vertical_wall_short");
            var verWallTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/vertical_wall");
            var treasureChestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/TreasureChest");
            var hauntedHouseTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/dilapidated_house");


            var castle01 = new StillSprite(castleTexture);
            var forest01 = new StillSprite(forestTexture);
            var forest02 = new StillSprite(forestTexture);
            var forest03 = new StillSprite(forestTexture);
            var forest04 = new StillSprite(forestTexture);
            var forest05 = new StillSprite(forestTexture);
            var forest06 = new StillSprite(forestTexture);
            var bigForest01 = new StillSprite(bigForestTexture);
            var bigForest02 = new StillSprite(bigForestTexture);
            var bigForest03 = new StillSprite(bigForestTexture);
            var bigForest04 = new StillSprite(bigForestTexture);
            var vertForest01 = new StillSprite(vertForestTexture);
            var vertForest02 = new StillSprite(vertForestTexture);
            var vertForest03 = new StillSprite(vertForestTexture);
            var vertForest04 = new StillSprite(vertForestTexture);
            var vertForest05 = new StillSprite(vertForestTexture);
            var vertForest06 = new StillSprite(vertForestTexture);
            var vertForest07 = new StillSprite(vertForestTexture);
            var vertForest08 = new StillSprite(vertForestTexture);
            var smallForest01 = new StillSprite(smallForestTexture);
            var smallForest02 = new StillSprite(smallForestTexture);
            var smallForest03 = new StillSprite(smallForestTexture);
            var smallForest04 = new StillSprite(smallForestTexture);
            var longForest01 = new StillSprite(longForestTexture);
            var oldHouse01 = new StillSprite(houseTexture);
            var oldHouse02 = new StillSprite(houseTexture);
            var oldHouse03 = new StillSprite(houseTexture);
            var horisontalWall01 = new StillSprite(horWallTexture);
            var horisontalWall02 = new StillSprite(horWallTexture);
            var verticalWall01 = new StillSprite(verWallTexture);
            var verticalWallShort01 = new StillSprite(verWallShortTexture);
            var verticalWallShort02 = new StillSprite(verWallShortTexture);

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
            InteractionSprite treasureChest01 = new InteractionSprite(treasureChestTexture, InteractionType.Chest);
            treasureChest01.Position = new Vector2(1270, 30);
            treasureChest01.SetCollisionRectangle();
            InteractionSprite hauntedHouseSprite = new InteractionSprite(hauntedHouseTexture, InteractionType.Entrance, "Haunted house");
            hauntedHouseSprite.Position = new Vector2(0, 2200);
            hauntedHouseSprite.SetCollisionRectangle();
            gamePlayScreen.Interactables = new List<InteractionSprite>()
            {
                treasureChest01,
                hauntedHouseSprite
            };

            foreach (var sprite in gamePlayScreen.Interactables)
            {
                if (sprite.InteractionType == InteractionType.Chest)
                {
                    // Location loc = new Location(sprite.Position.X, sprite.Position.Y, 0, 0, 0);
                    //mGame.Engine.Loot.GenerateTreasureChest(loc);
                    sprite.Treasure = mGame.Engine.Loot.Treasure;
                }
            }

            gamePlayScreen.environmentSprites = new List<Sprite>
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
             
                hauntedHouseSprite
           };
            gamePlayScreen.Interactables.Clear();
            LoadTreasureChests(gamePlayScreen, mGame, treasureChestTexture);
            gamePlayScreen.Interactables.Add(hauntedHouseSprite);
        }

        private static void LoadTreasureChests(GamePlayScreen gamePlayScreen, MainGame mGame, Texture2D treasureChestTexture)
        {
            foreach (var chest in mGame.Engine.Loot.TreasureChests)
            {
                var treasure = new InteractionSprite(treasureChestTexture, InteractionType.Chest);
                treasure.Position = new Vector2((float)chest.Location.X, (float)chest.Location.Y);
                treasure.SetCollisionRectangle();
                gamePlayScreen.Interactables.Add(treasure);
                gamePlayScreen.environmentSprites.Add(treasure);
            }
        }

        public void LoadEnvironmentLevelTwo(GamePlayScreen gamePlayScreen, MainGame mGame)
        {
            var shortWallTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_400");
            var shortWallVertTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_400_vert");
            var longWallTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_1600");
            var avgWallTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_800");
            var avgWallVertTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_800_vert");
            var vertSurroundingTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_2000_vert");
            var horSurroundingTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_2000_hor");
            var vertWall150 = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_150_vert");
            var horWall150 = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_150_hor");
            var rubbleTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/rubble");
            var lampTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wall_lamp");
            var smallForestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest04_small");
            var oldWellTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/stone_well");
            var treasureChestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/TreasureChest");
            var caveTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/cave_entrance");
            //inner yard surrounding
            var longWall01 = new StillSprite(longWallTexture);
            var longWall02 = new StillSprite(longWallTexture);
            var avgWallVert01 = new StillSprite(avgWallVertTexture);
            var avgWallVert02 = new StillSprite(avgWallVertTexture);
            var shortWall01 = new StillSprite(shortWallTexture);
            var avgWall01 = new StillSprite(avgWallTexture);

            longWall01.Position = new Vector2(200, 1000);
            longWall02.Position = new Vector2(200, 1130);
            avgWallVert01.Position = new Vector2(200, 230);
            avgWallVert02.Position = new Vector2(1800, 230);
            shortWall01.Position = new Vector2(1430, 200);
            avgWall01.Position = new Vector2(200, 200);

            //surrounding walls
            var vertSurroundingSprite01 = new StillSprite(vertSurroundingTexture);
            var vertSurroundingSprite02 = new StillSprite(vertSurroundingTexture);
            var horSurroundingSprite01 = new StillSprite(horSurroundingTexture);
            var horSurroundingSprite02 = new StillSprite(horSurroundingTexture);


            //vertical wall 150p
            var vertWallSmallSprite01 = new StillSprite(vertWall150);
            var vertWallSmallSprite02 = new StillSprite(vertWall150);
            //horisonatal wall 150

            //vertical wall 400p
            var shortVertWallSprite01 = new StillSprite(shortWallVertTexture);
            var shortVertWallSprite02 = new StillSprite(shortWallVertTexture);
            var smallWallSprite01 = new StillSprite(horWall150);
            var smallWallSprite02 = new StillSprite(horWall150);
            var smallVertWallSprite03 = new StillSprite(vertWall150);
            var smallVertWallSprite04 = new StillSprite(vertWall150);
            var shortWallSprite02 = new StillSprite(shortWallTexture);
            var avgWallSprite02 = new StillSprite(avgWallTexture);
            var shortVertWallSprite03 = new StillSprite(shortWallVertTexture);
            var shortVertWallSprite04 = new StillSprite(shortWallVertTexture);
            var smallWallSprite03 = new StillSprite(horWall150);
            var smallWallSprite04 = new StillSprite(horWall150);
            var smallVertWallSprite05 = new StillSprite(vertWall150);
            var smallVertWallSprite06 = new StillSprite(vertWall150);
            var smallVertWallSprite07 = new StillSprite(vertWall150);
            var smallVertWallSprite08 = new StillSprite(vertWall150);
            var shortWallSprite03 = new StillSprite(shortWallTexture);
            var rubble01 = new StillSprite(rubbleTexture);
            var rubble02 = new StillSprite(rubbleTexture);
            var rubble03 = new StillSprite(rubbleTexture);
            var rubble04 = new StillSprite(rubbleTexture);
            var rubble05 = new StillSprite(rubbleTexture);
            var rubble06 = new StillSprite(rubbleTexture);
            var rubble07 = new StillSprite(rubbleTexture);
            var rubble08 = new StillSprite(rubbleTexture);
            var rubble09 = new StillSprite(rubbleTexture);
            var rubble10 = new StillSprite(rubbleTexture);
            var wallLamp01 = new StillSprite(lampTexture);
            var wallLamp02 = new StillSprite(lampTexture);
            var wallLamp03 = new StillSprite(lampTexture);
            var wallLamp04 = new StillSprite(lampTexture);
            var wallLamp05 = new StillSprite(lampTexture);
            var wallLamp06 = new StillSprite(lampTexture);
            var wallLamp07 = new StillSprite(lampTexture);
            var wallLamp08 = new StillSprite(lampTexture);
            var smallForest01 = new StillSprite(smallForestTexture);
            var smallForest02 = new StillSprite(smallForestTexture);
            var smallForest03 = new StillSprite(smallForestTexture);

            vertWallSmallSprite01.Position = new Vector2(200, 1130);
            vertWallSmallSprite02.Position = new Vector2(200, 1380);
            shortVertWallSprite01.Position = new Vector2(200, 1480);
            shortVertWallSprite02.Position = new Vector2(630, 1130);
            smallWallSprite01.Position = new Vector2(230, 1500);
            smallWallSprite02.Position = new Vector2(480, 1500);
            smallVertWallSprite03.Position = new Vector2(630, 1470);
            smallVertWallSprite04.Position = new Vector2(630, 1730);
            shortWallSprite02.Position = new Vector2(230, 1850);
            avgWallSprite02.Position = new Vector2(630, 1850);
            shortVertWallSprite03.Position = new Vector2(1800, 1130);
            shortVertWallSprite04.Position = new Vector2(1800, 1480);
            smallWallSprite03.Position = new Vector2(1400, 1850);
            smallWallSprite04.Position = new Vector2(1650, 1850);
            smallVertWallSprite05.Position = new Vector2(1400, 1130);
            smallVertWallSprite06.Position = new Vector2(1400, 1380);
            smallVertWallSprite07.Position = new Vector2(1400, 1480);
            smallVertWallSprite08.Position = new Vector2(1400, 1730);
            shortWallSprite03.Position = new Vector2(1430, 1500);

            vertSurroundingSprite01.Position = new Vector2(0, 0);
            vertSurroundingSprite02.Position = new Vector2(1970, 0);
            horSurroundingSprite01.Position = new Vector2(0, 0);
            horSurroundingSprite02.Position = new Vector2(0, 1970);

            rubble01.Position = new Vector2(400, 30);
            rubble02.Position = new Vector2(440, 110);
            rubble03.Position = new Vector2(200, 1040);
            rubble04.Position = new Vector2(100, 920);
            rubble05.Position = new Vector2(1400, 1890);
            rubble06.Position = new Vector2(230, 1160);
            rubble07.Position = new Vector2(530, 1530);
            rubble08.Position = new Vector2(660, 1160);
            rubble09.Position = new Vector2(660, 1240);
            rubble10.Position = new Vector2(1420, 230);
            wallLamp01.Position = new Vector2(30, 1800);
            wallLamp02.Position = new Vector2(30, 1600);
            wallLamp03.Position = new Vector2(30, 1400);
            wallLamp04.Position = new Vector2(30, 1200);
            wallLamp05.Position = new Vector2(1830, 1800);
            wallLamp06.Position = new Vector2(1830, 1600);
            wallLamp07.Position = new Vector2(1830, 1400);
            wallLamp08.Position = new Vector2(1830, 1200);
            smallForest01.Position = new Vector2(1420, 420);
            smallForest02.Position = new Vector2(1470, 620);
            smallForest03.Position = new Vector2(800, 230);

            InteractionSprite oldWellSprite01 = new InteractionSprite(oldWellTexture, InteractionType.Well);
            oldWellSprite01.Position = new Vector2(600, 600);
            InteractionSprite oldWellSprite02 = new InteractionSprite(oldWellTexture, InteractionType.Well);
            oldWellSprite02.Position = new Vector2(1600, 850);
            InteractionSprite caveSprite = new InteractionSprite(caveTexture, InteractionType.Entrance);
            caveSprite.Position = new Vector2(230, 850);
            gamePlayScreen.Interactables = new List<InteractionSprite>
            {
                caveSprite
            };

            gamePlayScreen.environmentSprites = new List<Sprite>
            {
                vertSurroundingSprite01,
                vertSurroundingSprite02,
                horSurroundingSprite01,
                horSurroundingSprite02,
                shortWall01,
                avgWall01,
                longWall01,
                longWall02,
                avgWallVert01,
                avgWallVert02,
                vertWallSmallSprite01,
                vertWallSmallSprite02,
                shortVertWallSprite01,
                shortVertWallSprite02,
                smallWallSprite01,
                smallWallSprite02,
                smallVertWallSprite03,
                smallVertWallSprite04,
                shortWallSprite02,
                avgWallSprite02,
                shortVertWallSprite03,
                shortVertWallSprite04,
                smallWallSprite03,
                smallWallSprite04,
                smallVertWallSprite05,
                smallVertWallSprite06,
                smallVertWallSprite07,
                smallVertWallSprite08,
                shortWallSprite03,
                rubble01,
                rubble02,
                rubble03,
                rubble04,
                rubble05,
                rubble06,
                rubble07,
                rubble08,
                rubble09,
                rubble10,
                oldWellSprite01,
                oldWellSprite02,
                wallLamp01,
                wallLamp02,
                wallLamp03,
                wallLamp04,
                wallLamp05,
                wallLamp06,
                wallLamp07,
                wallLamp08,
                smallForest01,
                smallForest02,
                smallForest03,
                caveSprite
           };

            LoadTreasureChests(gamePlayScreen, mGame, treasureChestTexture);
        }

        public SaveGameData CreateSaveGameData(MainGame mGame, GamePlayScreen currentGamePlay)
        {
            SaveGameData data = new SaveGameData
            {
                PlayerType = mGame.Engine.HeroType,
                PlayerName = mGame.Engine.Hero.Name,
                PlayerLevel = mGame.Engine.Hero.Level,
                PlayerExperiencePoints = mGame.Engine.Hero.ExperiencePoints,
                MaxHealth = mGame.Engine.Hero.MaxHealth,
                MaxMana = mGame.Engine.Hero.MaxMana,
                CurrentHealth = mGame.Engine.Hero.Health,
                CurrentMana = mGame.Engine.Hero.Mana,
                AttackPoints = mGame.Engine.Hero.Damage,
                DefencePoints = mGame.Engine.Hero.Armor,
                Helmet = mGame.Engine.Hero.Inventory.Helmet,
                Gloves = mGame.Engine.Hero.Inventory.Gloves,
                Armor = mGame.Engine.Hero.Inventory.BodyArmor,
                Boots = mGame.Engine.Hero.Inventory.Boots,
                LevelNumber = currentGamePlay.LevelNumber,
                BagItems = new ArrayList(mGame.Engine.Hero.Inventory.Bag)
            };

            if (mGame.Engine.Hero.Inventory.MainHandWeapon != null)
            {
                Weapon w = mGame.Engine.Hero.Inventory.MainHandWeapon as Weapon;
                data.MainHandWeapon = w;

            }

            if (mGame.Engine.Hero.Inventory.OffHand != null)
            {
                if (mGame.Engine.Hero.Inventory.OffHand is Weapon)
                {
                    Weapon w = mGame.Engine.Hero.Inventory.OffHand as Weapon;
                    data.OffHandWeapon = w;
                }
                else
                {
                    Shield w = mGame.Engine.Hero.Inventory.OffHand as Shield;
                    data.OffhandShield = w;
                }
            }
            return data;
        }

    }
}