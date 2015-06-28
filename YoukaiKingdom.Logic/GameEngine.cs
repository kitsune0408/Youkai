namespace YoukaiKingdom.Logic
{
    using System.Collections.Generic;

    using YoukaiKingdom.Logic.Models.Characters;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Inventory;

    public class GameEngine
    {
        public GameEngine(Hero heroClass)
        {
            this.Hero = heroClass;
            this.CurrentLevel = 1;
            this.Loot = Loot.Create(this.CurrentLevel);
            this.LoadDefaultInfo();
            this.Enemies = new List<Npc>();
            this.LoadBosses();
            this.GenerateTreasureChests();
        }

        public Loot Loot { get; set; }

        public Hero Hero { get; set; }

        public CharacterType HeroType
        {
            get
            {
                if (this.Hero is Monk)
                {
                    return CharacterType.Monk;
                }

                if (this.Hero is Samurai)
                {
                    return CharacterType.Samurai;
                }

                if (this.Hero is Ninja)
                {
                    return CharacterType.Ninja;
                }

                return CharacterType.None;
            }
        }

        public void Start()
        {
            //this.LoadEnemies();
        }

        public List<Npc> Enemies { get; set; }

        public List<Npc> Bosses { get; set; }

        public int CurrentLevel { get; private set; }

        public void NextLevel()
        {
            this.CurrentLevel++;
            this.Loot = Loot.Create(this.CurrentLevel);
            this.GenerateTreasureChests();
        }

        public void LoadEnemies()
        {
            if (this.CurrentLevel == 1)
            {
                this.Enemies.AddRange(
                    new List<Npc>()
                        {
                            new NpcRogue(this.CurrentLevel, "Evil Ninja", new Location(1200, 300, 200, 200, 150)), //0
                            new NpcRogue(this.CurrentLevel, "Evil Ninja", new Location(1200, 800, 200, 200, 100)), //1
                            new NpcRogue(this.CurrentLevel, "Evil Ninja", new Location(400, 900, 200, 200, 100)), //2
                            new NpcRogue(this.CurrentLevel, "Evil Ninja", new Location(2200, 0, 200, 200, 100)), //3
                            new NpcRogue(this.CurrentLevel, "Evil Ninja", new Location(2800, 800, 200, 200, 100)), //4
                            new NpcRogue(this.CurrentLevel, "Evil Ninja", new Location(1200, 2000, 200, 200, 100)), //5
                            new NpcMage(this.CurrentLevel, "Evil Mage", new Location(100, 900, 300, 300, 100)), //6
                            new NpcMage(this.CurrentLevel, "Evil Mage", new Location(1000, 1400, 200, 200, 100)), //7
                            new NpcMage(this.CurrentLevel, "Evil Mage", new Location(2000, 1200, 400, 400, 100)), //8
                            new NpcMage(this.CurrentLevel, "Evil Mage", new Location(3200, 0, 200, 200, 100)), //9   
                            new NpcMage(this.CurrentLevel, "Evil Mage", new Location(1000, 2200, 200, 200, 100)), //10  
                            new NpcWarrior(this.CurrentLevel, "Evil Samurai", new Location(1000, 1100, 200, 200, 100)),
                            //11
                            new NpcWarrior(this.CurrentLevel, "Evil Samurai", new Location(600, 1100, 200, 200, 100)),
                            //12
                            new NpcWarrior(this.CurrentLevel, "Evil Samurai", new Location(2000, 800, 200, 200, 100)),
                            //13
                            new NpcWarrior(this.CurrentLevel, "Evil Samurai", new Location(2800, 1200, 200, 200, 100)),
                            //14
                            new NpcWarrior(this.CurrentLevel, "Evil Samurai", new Location(3000, 0, 200, 200, 100)), //15
                            new NpcWarrior(this.CurrentLevel, "Evil Samurai", new Location(1400, 1800, 200, 200, 100)),
                            //15
                            //this.bosses[level-1]
                        });

            }
            else if (this.CurrentLevel == 2)
            {
                this.Enemies.Clear();
                this.Enemies.AddRange(
                    new List<Npc>()
                            {
                                new NpcMage(this.CurrentLevel, "Onryo", new Location(60, 200, 90, 400, 100)), //1
                                new NpcMage(this.CurrentLevel, "Onryo", new Location(60, 1200, 90, 400, 100)), //2
                                new NpcMage(this.CurrentLevel, "Onryo", new Location(1200, 1160, 200, 200, 100)), //3
                                new NpcRogue(this.CurrentLevel, "Evil ninja", new Location(400, 1880, 400, 90, 100)), //4
                                new NpcWarrior(this.CurrentLevel, "Evil samurai", new Location(1430, 1530, 370, 320, 100)),//5
                            });
            }
        }

        public void LoadBosses()
        {
            this.Bosses = new List<Npc>();

            this.Bosses.AddRange(new List<Npc>()
                                {
                                    new NpcWarrior(1, "Oni", 600, 100, 250, 200, new Location(3200, 2000, 200, 200, 10)),
                                    new NpcMage(2, "Ogre", 800, 600, 250, 300, new Location(100 ,100, 100, 100, 100)),
                                    new NpcRogue(3, "Goryo", 850, 100, 300, 300, new Location(100 ,100, 100, 100, 100))
                                });
        }

        private void LoadDefaultInfo()
        {
            if (this.Hero is Samurai)
            {
                this.Hero.ReplaceMainHand(this.Loot.GetOneHandedSwordById(12), CharacterType.Samurai);
                this.Hero.ReplaceBodyArmor(this.Loot.GetBodyArmorById(54));
                this.Hero.Inventory.AddItemToBag(this.Loot.GetMinorHealingPotion());
                this.Hero.Inventory.AddItemToBag(this.Loot.GetHealingPotion());
                this.Hero.Inventory.AddItemToBag(this.Loot.GetMinorManaPotion());
                //this.Hero.Inventory.AddItemToBag(this.Loot.GetTwoHandedSwordById(69));
                //this.Hero.Inventory.AddItemToBag(this.Loot.GetOneHandedSwordById(10));
            }
            else if (this.Hero is Monk)
            {
                this.Hero.ReplaceMainHand(this.Loot.GetTwoHandedStaffById(16), CharacterType.Monk);
                this.Hero.ReplaceBodyArmor(this.Loot.GetBodyArmorById(56));
                this.Hero.Inventory.AddItemToBag(this.Loot.GetHealingPotion());
                this.Hero.Inventory.AddItemToBag(this.Loot.GetMinorHealingPotion());
                this.Hero.Inventory.AddItemToBag(this.Loot.GetMinorManaPotion());
            }
            else if (this.Hero is Ninja)
            {
                this.Hero.ReplaceMainHand(this.Loot.GetDaggerWeaponById(14), CharacterType.Ninja);
                this.Hero.ReplaceBodyArmor(this.Loot.GetBodyArmorById(55));
                this.Hero.Inventory.AddItemToBag(this.Loot.GetHealingPotion());
                this.Hero.Inventory.AddItemToBag(this.Loot.GetMinorHealingPotion());
                this.Hero.Inventory.AddItemToBag(this.Loot.GetMinorManaPotion());
            }
        }
        //TODO
        private void GenerateTreasureChests()
        {
            this.Loot.ClearTreasureChests();

            if (this.CurrentLevel == 1)
            {
                this.Loot.GenerateTreasureChest(new Location(1270, 30));
                this.Loot.GenerateTreasureChest(new Location(628, 1500));
                this.Loot.GenerateTreasureChest(new Location(2081, 1809));
            }
            else if (this.CurrentLevel == 2)
            {
                this.Loot.GenerateTreasureChest(new Location(350, 130));
                this.Loot.GenerateTreasureChest(new Location(310, 1060));
                this.Loot.GenerateTreasureChest(new Location(1700, 1200));
            }
        }
    }
}
