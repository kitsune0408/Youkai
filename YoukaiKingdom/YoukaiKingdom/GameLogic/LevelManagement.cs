using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.GameScreens;
using YoukaiKingdom.Sprites;

namespace YoukaiKingdom.GameLogic
{
    internal class LevelManagement
    {

        public void LoadEnvironmentLevelOne(GamePlayScreen gamePlayScreen, MainGame mGame)
        {
            Texture2D castleTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/Castle");
            Texture2D forestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest01");
            Texture2D bigForestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest_02_big");
            Texture2D vertForestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest_03_vert");
            Texture2D smallForestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest04_small");
            Texture2D longForestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/forest_05_long");

            Texture2D houseTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/old_house");
            Texture2D horWallTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/horisontal_wall");
            Texture2D verWallShortTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/vertical_wall_short");
            Texture2D verWallTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/vertical_wall");
            Texture2D treasureChestTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/TreasureChest");
            Texture2D hauntedHouseTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/dilapidated_house");


            StillSprite castle01 = new StillSprite(castleTexture);
            StillSprite forest01 = new StillSprite(forestTexture);
            StillSprite forest02 = new StillSprite(forestTexture);
            StillSprite forest03 = new StillSprite(forestTexture);
            StillSprite forest04 = new StillSprite(forestTexture);
            StillSprite forest05 = new StillSprite(forestTexture);
            StillSprite forest06 = new StillSprite(forestTexture);
            StillSprite bigForest01 = new StillSprite(bigForestTexture);
            StillSprite bigForest02 = new StillSprite(bigForestTexture);
            StillSprite bigForest03 = new StillSprite(bigForestTexture);
            StillSprite bigForest04 = new StillSprite(bigForestTexture);
            StillSprite vertForest01 = new StillSprite(vertForestTexture);
            StillSprite vertForest02 = new StillSprite(vertForestTexture);
            StillSprite vertForest03 = new StillSprite(vertForestTexture);
            StillSprite vertForest04 = new StillSprite(vertForestTexture);
            StillSprite vertForest05 = new StillSprite(vertForestTexture);
            StillSprite vertForest06 = new StillSprite(vertForestTexture);
            StillSprite vertForest07 = new StillSprite(vertForestTexture);
            StillSprite vertForest08 = new StillSprite(vertForestTexture);
            StillSprite smallForest01 = new StillSprite(smallForestTexture);
            StillSprite smallForest02 = new StillSprite(smallForestTexture);
            StillSprite smallForest03 = new StillSprite(smallForestTexture);
            StillSprite smallForest04 = new StillSprite(smallForestTexture);
            StillSprite longForest01 = new StillSprite(longForestTexture);
            StillSprite oldHouse01 = new StillSprite(houseTexture);
            StillSprite oldHouse02 = new StillSprite(houseTexture);
            StillSprite oldHouse03 = new StillSprite(houseTexture);
            StillSprite horisontalWall01 = new StillSprite(horWallTexture);
            StillSprite horisontalWall02 = new StillSprite(horWallTexture);
            StillSprite verticalWall01 = new StillSprite(verWallTexture);
            StillSprite verticalWallShort01 = new StillSprite(verWallShortTexture);
            StillSprite verticalWallShort02 = new StillSprite(verWallShortTexture);

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
            InteractionSprite treasureChest01 = new InteractionSprite(treasureChestTexture);
            treasureChest01.Position = new Vector2(1270, 30);
            treasureChest01.SetCollisionRectangle();
            InteractionSprite hauntedHouseSprite = new InteractionSprite(hauntedHouseTexture);
            hauntedHouseSprite.Position = new Vector2(0, 2200);
            hauntedHouseSprite.SetCollisionRectangle();
            gamePlayScreen.Interactables = new List<InteractionSprite>()
            {
                treasureChest01,
                hauntedHouseSprite
            };

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
                treasureChest01,
                hauntedHouseSprite
            };
        }

        public void LoadEnvironmentLevelTwo(GamePlayScreen gamePlayScreen, MainGame mGame)
        {
            Texture2D shortWallTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_400");
            Texture2D longWallTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_1600");
            Texture2D avgWallTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_800");
            Texture2D shortWallVertTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_400_vert");
            Texture2D avgWallVertTexture = mGame.Content.Load<Texture2D>("Sprites/Environment/wooden_wall_800_vert");


            StillSprite longWall01 = new StillSprite(longWallTexture);
            longWall01.Position = new Vector2(200, 1000);
            StillSprite longWall02 = new StillSprite(longWallTexture);
            longWall02.Position = new Vector2(200, 1800);
            StillSprite avgWallVert01 = new StillSprite(avgWallVertTexture);
            avgWallVert01.Position = new Vector2(200, 230);
            StillSprite avgWallVert02 = new StillSprite(avgWallVertTexture);
            avgWallVert02.Position = new Vector2(1800, 230);

            StillSprite shortWall01 = new StillSprite(shortWallTexture);
            shortWall01.Position = new Vector2(1200, 200);
            StillSprite avgWall01 = new StillSprite(shortWallTexture);
            avgWall01.Position = new Vector2(200, 200);
            gamePlayScreen.environmentSprites = new List<Sprite>
            {
                shortWall01,
                avgWall01,
                longWall01,
                longWall02,
                avgWallVert01,
                avgWallVert02
                
            };
        }

    }
}