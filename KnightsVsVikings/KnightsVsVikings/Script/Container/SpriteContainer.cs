using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class SpriteContainer
    {
        #region Singleton
        private static SpriteContainer instance;
        public static SpriteContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpriteContainer();
                }
                return instance;
            }
        }
        #endregion

        private Dictionary<string, Texture2D> sprite = new Dictionary<string, Texture2D>();
        private Dictionary<string, TextureSheet2D> spriteSheet = new Dictionary<string, TextureSheet2D>();
        private Dictionary<string, List<Texture2D>> spriteList = new Dictionary<string, List<Texture2D>>();
        private Dictionary<string, List<TextureSheet2D>> spriteSheetList = new Dictionary<string, List<TextureSheet2D>>();
        private SpriteFont normalFont;
        private SpriteFont mediaevalFont;
        private TileSprite tileSprite = new TileSprite();
        private UnitImageSprite unitImageSprite = new UnitImageSprite();

        public Dictionary<string, Texture2D> Sprite { get => sprite; set => sprite = value; }
        public Dictionary<string, TextureSheet2D> SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
        public Dictionary<string, List<Texture2D>> SpriteList { get => spriteList; set => spriteList = value; }
        public Dictionary<string, List<TextureSheet2D>> SpriteSheetList { get => spriteSheetList; set => spriteSheetList = value; }
        public SpriteFont NormalFont { get => normalFont;private set => normalFont = value; }
        public SpriteFont MediaevalFont { get => mediaevalFont;private set => mediaevalFont = value; }

        public Texture2D Pixel { get; set; }
        public Color TextColor { get; } = new Color(200, 182, 157);
        public TileSprite TileSprite { get => tileSprite; set => tileSprite = value; }
        public UnitImageSprite UnitImageSprite { get => unitImageSprite; set => unitImageSprite = value; }

        public SpriteContainer()
        {

        }

        public void LoadContent(ContentManager content)
        {
            // Normal Font
            NormalFont = content.Load<SpriteFont>("Font/NormalFont");
            MediaevalFont = content.Load<SpriteFont>("Font/MediaevalFont");

            // The Pixel
            AddSprite(content.Load<Texture2D>("Images/MainSystem/Pixel"), "Pixel");

            Pixel = content.Load<Texture2D>("Images/MainSystem/Pixel");

            //Interface
            AddSprite(content.Load<Texture2D>("Images/UI/Map"), "Map");      

            // Tile Sheet
            AddSprite(content.Load<Texture2D>("Images/TileSheet/ExtraObjects128x128"), "ExtraObjects128x128");
            AddSprite(content.Load<Texture2D>("Images/TileSheet/Grassland128x128"), "Grassland128x128");


            Build(content);
            UI(content);
            AnimationsSheet(content);
            Tiles();
            UnitImage();
            RandomImages(content);
        }

        private void AddSprite(Texture2D texture2D, string name)
        {
            Sprite.Add(name, texture2D);
        }
        private void AddSprite(TextureSheet2D textureSheet2D, string name)
        {
            SpriteSheet.Add(name, textureSheet2D);
        }

        private void AddSpriteList(List<Texture2D> texture2Ds, string name)
        {
            SpriteList.Add(name, texture2Ds);
        }
        private void AddSpriteList(List<TextureSheet2D> TextureSheet2Ds, string name)
        {
            SpriteSheetList.Add(name, TextureSheet2Ds);
        }

        private void RandomImages(ContentManager content)
        {
            AddSprite(content.Load<Texture2D>("Images/Characters/UnitStuff/IsSelected"), "IsSelected");
            AddSprite(content.Load<Texture2D>("Images/Characters/UnitStuff/shadow"), "Shadow");
        }

        private void Build(ContentManager content)
        {
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(16 * 128, 4 * 128), new Vector2(3 * 128, 4 * 128)), "GrayTent");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(16 * 128, 8 * 128), new Vector2(3 * 128, 4 * 128)), "BlueTent");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(16 * 128, 12 * 128), new Vector2(3 * 128, 4 * 128)), "RedTent");

            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(4 * 128, 3 * 128), new Vector2(2 * 128, 3 * 128)), "WoodSign");

            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(6 * 128, 4 * 128), new Vector2(1 * 128, 1 * 128)), "SignIcon_Axe");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(7 * 128, 4 * 128), new Vector2(1 * 128, 1 * 128)), "SignIcon_Pot");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(8 * 128, 4 * 128), new Vector2(1 * 128, 1 * 128)), "SignIcon_Food");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(6 * 128, 5 * 128), new Vector2(1 * 128, 1 * 128)), "SignIcon_AM");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(7 * 128, 5 * 128), new Vector2(1 * 128, 1 * 128)), "SignIcon_Book");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(8 * 128, 5 * 128), new Vector2(1 * 128, 1 * 128)), "SignIcon_BS");

            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(5 * 128, 6 * 128), new Vector2(1 * 128, 1 * 128)), "SignIconTailoring");
        }

        private void UI(ContentManager content)
        {
            // The Game Icon
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_157"), "WorkerIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_63"), "FootmanIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_09"), "BowmanIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_100"), "SpearmanIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_88"), "VikingFootmanIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_63"), "KnightFootmanIcon");

            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_10"), "ArcheryRangeIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_121"), "BlacksmithIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_136"), "TowerIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_159"), "TownHallIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_19"), "BarracksIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_49"), "GatheringStationIcon");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/Game_Icon/SGI_51"), "FieldIcon");

            // UI Icon
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/UI_Icon/UI_Icon_Play"), "ArrowRigth");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/UI_Icon/UI_Icon_PlayBackwards"), "ArrowLeft");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/UI_Icon/UI_Icon_SoundOff"), "UI_Icon_SoundOff");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/UI_Icon/UI_Icon_SoundOn"), "UI_Icon_SoundOn");

            AddSprite(content.Load<Texture2D>("Images/UI/Icon/UI_Icon/VikingsCampaign"), "VikingsCampaign");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/UI_Icon/KnightsCampaign"), "KnightsCampaign");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/UI_Icon/Gold"), "Gold");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/UI_Icon/Iron"), "Iron");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/UI_Icon/Wood"), "Wood");
            AddSprite(content.Load<Texture2D>("Images/UI/Icon/UI_Icon/Food"), "Food");

            // UI Button
            AddSprite(content.Load<Texture2D>("Images/UI/Button/Button_A_Long_active"), "Button_A_Long_red");
            AddSprite(content.Load<Texture2D>("Images/UI/Button/Button_A_Long_aimed"), "Button_A_Long_black");
            AddSprite(content.Load<Texture2D>("Images/UI/Button/Button_tiny_ready"), "Button_tiny_black");
            AddSprite(content.Load<Texture2D>("Images/UI/Button/Button_tiny_red"), "Button_tiny_red");

            // UI World Editor 
            AddSprite(content.Load<Texture2D>("Images/UI/ActionBar/ActionBar"), "ActionBar");
            AddSprite(content.Load<Texture2D>("Images/UI/ActionBar/ActionBar02"), "ActionBar02");
            AddSprite(content.Load<Texture2D>("Images/UI/ActionBar/Slot"), "Slot");
            AddSprite(content.Load<Texture2D>("Images/UI/ActionBar/SlotNameBar"), "SlotNameBar");

        }

        private void AnimationsSheet(ContentManager content)
        {

            // Knights Bowman
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Bowman/KnightsBowmanDown"), "KnightsBowmanDown");
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Bowman/KnightsBowmanSide"), "KnightsBowmanSide");
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Bowman/KnightsBowmanUp"), "KnightsBowmanUp");
            // Knights Footman
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Footman/KnightsFootmanDown"), "KnightsFootmanDown");
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Footman/KnightsFootmanSide"), "KnightsFootmanSide");
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Footman/KnightsFootmanUp"), "KnightsFootmanUp");
            // Knights Spear
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Spearman/KnightsSpearmanDown"), "KnightsSpearmanDown");
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Spearman/KnightsSpearmanSide"), "KnightsSpearmanSide");
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Spearman/KnightsSpearmanUp"), "KnightsSpearmanUp");
            // Knights Worker
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Worker/KnightsWorkerDown"), "KnightsWorkerDown");
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Worker/KnightsWorkerSide"), "KnightsWorkerSide");
            AddSprite(content.Load<Texture2D>("Images/Characters/Knights/Worker/KnightsWorkerUp"), "KnightsWorkerUp");



            // Vikings Bowman
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Bowman/VikingsBowmanDown"), "VikingsBowmanDown");
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Bowman/VikingsBowmanSide"), "VikingsBowmanSide");
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Bowman/VikingsBowmanUp"), "VikingsBowmanUp");
            // Vikings Footman
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Footman/VikingsFootmanDown"), "VikingsFootmanDown");
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Footman/VikingsFootmanSide"), "VikingsFootmanSide");
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Footman/VikingsFootmanUp"), "VikingsFootmanUp");
            // Vikings Spear
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Spearman/VikingsSpearmanDown"), "VikingsSpearmanDown");
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Spearman/VikingsSpearmanSide"), "VikingsSpearmanSide");
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Spearman/VikingsSpearmanUp"), "VikingsSpearmanUp");
            // Vikings Worker
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Worker/VikingsWorkerDown"), "VikingsWorkerDown");
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Worker/VikingsWorkerSide"), "VikingsWorkerSide");
            AddSprite(content.Load<Texture2D>("Images/Characters/Vikings/Worker/VikingsWorkerUp"), "VikingsWorkerUp");
        }

        private void Tiles()
        {
            Vector2 tileSize = new Vector2((int)(1 * 128), (int)(1 * 128));
            Texture2D tileSheet = sprite["Grassland128x128"];
            Texture2D objectSheet = sprite["ExtraObjects128x128"];

            // Delete
            tileSprite.Delete = new TextureSheet2D(objectSheet, RetrunPos(10, 5), tileSize);
            // Grass Tile
            TileSprite.Grass01 = new TextureSheet2D(tileSheet, RetrunPos(2,2), tileSize);
            TileSprite.Grass02 = new TextureSheet2D(tileSheet, RetrunPos(3,2), tileSize);
            TileSprite.Grass03 = new TextureSheet2D(tileSheet, RetrunPos(4,2), tileSize);
            // Greass-Water tile
            TileSprite.GrassWater01 = new TextureSheet2D(tileSheet, RetrunPos(11, 8), tileSize);
            TileSprite.GrassWater02 = new TextureSheet2D(tileSheet, RetrunPos(11, 9), tileSize);
            TileSprite.GrassWater03 = new TextureSheet2D(tileSheet, RetrunPos(11, 10), tileSize);
            TileSprite.GrassWater04 = new TextureSheet2D(tileSheet, RetrunPos(11, 11), tileSize);
            TileSprite.GrassWater05 = new TextureSheet2D(tileSheet, RetrunPos(14, 8), tileSize);
            TileSprite.GrassWater06 = new TextureSheet2D(tileSheet, RetrunPos(14, 9), tileSize);
            TileSprite.GrassWater07 = new TextureSheet2D(tileSheet, RetrunPos(14, 10), tileSize);
            TileSprite.GrassWater08 = new TextureSheet2D(tileSheet, RetrunPos(14, 11), tileSize);
            TileSprite.GrassWater09 = new TextureSheet2D(tileSheet, RetrunPos(12, 8), tileSize);
            TileSprite.GrassWater10 = new TextureSheet2D(tileSheet, RetrunPos(13, 8), tileSize);
            TileSprite.GrassWater11 = new TextureSheet2D(tileSheet, RetrunPos(12, 11), tileSize);
            TileSprite.GrassWater12 = new TextureSheet2D(tileSheet, RetrunPos(13, 11), tileSize);
            TileSprite.GrassWater13 = new TextureSheet2D(tileSheet, RetrunPos(15, 8), tileSize);
            TileSprite.GrassWater14 = new TextureSheet2D(tileSheet, RetrunPos(16, 8), tileSize);
            TileSprite.GrassWater15 = new TextureSheet2D(tileSheet, RetrunPos(15, 9), tileSize);
            TileSprite.GrassWater16 = new TextureSheet2D(tileSheet, RetrunPos(16, 9), tileSize);
            // Water Tile
            TileSprite.Water01 = new TextureSheet2D(tileSheet, RetrunPos(13, 9), tileSize);
            TileSprite.Water02 = new TextureSheet2D(tileSheet, RetrunPos(12, 9), tileSize);
            TileSprite.Water03 = new TextureSheet2D(tileSheet, RetrunPos(12, 10), tileSize);
            TileSprite.Water04 = new TextureSheet2D(tileSheet, RetrunPos(13, 10), tileSize);
            // Bridge Tile
            TileSprite.BridgeLeft01 = new TextureSheet2D(tileSheet, RetrunPos(8, 4), new Vector2(128,2*128));
            TileSprite.BridgeLeft02 = new TextureSheet2D(tileSheet, RetrunPos(9, 4), new Vector2(128,2*128));
            TileSprite.BridgeLeft03 = new TextureSheet2D(tileSheet, RetrunPos(10, 4), new Vector2(128,2*128));
            // Resources
            TileSprite.Gold = new TextureSheet2D(objectSheet, RetrunPos(16, 11), new Vector2(1*128,2*128));
            TileSprite.Stone = new TextureSheet2D(objectSheet, RetrunPos(16, 13), new Vector2(1*128,2*128));
            TileSprite.Wheatfield = new TextureSheet2D(objectSheet, RetrunPos(14, 6), new Vector2(3*128,3*128));
            TileSprite.Wood = new TextureSheet2D(objectSheet, RetrunPos(6, 12), new Vector2(3*128,5*128));

        }

        private void UnitImage()
        {
            Vector2 tileSize = new Vector2((int)(1 * 256), (int)(1 * 256));
            Texture2D knightBowmanSheet = sprite["KnightsBowmanDown"];
            Texture2D knightFootmanSheet = sprite["KnightsFootmanDown"];
            Texture2D knightSpearSheet = sprite["KnightsSpearmanDown"];
            Texture2D knightWorkerSheet = sprite["KnightsWorkerDown"];

            Texture2D vikingBowmanSheet = sprite["VikingsBowmanDown"];
            Texture2D vikingFootmanSheet = sprite["VikingsFootmanDown"];
            Texture2D vikingSpearSheet = sprite["VikingsSpearmanDown"];
            Texture2D vikingWorkerSheet = sprite["VikingsWorkerDown"];

            unitImageSprite.KnightBowman = new TextureSheet2D(knightBowmanSheet, RetrunPos(1, 1), tileSize);
            unitImageSprite.KnightFootman = new TextureSheet2D(knightFootmanSheet, RetrunPos(1, 1), tileSize);
            unitImageSprite.KnightSpear = new TextureSheet2D(knightSpearSheet, RetrunPos(1, 1), tileSize);
            unitImageSprite.KnightWorker = new TextureSheet2D(knightWorkerSheet, RetrunPos(1, 1), tileSize);

            unitImageSprite.VikingBowman = new TextureSheet2D(vikingBowmanSheet, RetrunPos(1, 1), tileSize);
            unitImageSprite.VikingFootman = new TextureSheet2D(vikingFootmanSheet, RetrunPos(1, 1), tileSize);
            unitImageSprite.VikingSpear = new TextureSheet2D(vikingSpearSheet, RetrunPos(1, 1), tileSize);
            unitImageSprite.VikingWorker = new TextureSheet2D(vikingWorkerSheet, RetrunPos(1, 1), tileSize);
        }

        private Vector2 RetrunPos(int x, int y)
        {
            return new Vector2((int)((x - 1) * 128), (int)((y - 1) * 128));
        }
    }
}
