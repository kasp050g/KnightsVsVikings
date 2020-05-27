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

        public Dictionary<string, Texture2D> Sprite { get => sprite; set => sprite = value; }
        public Dictionary<string, TextureSheet2D> SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
        public Dictionary<string, List<Texture2D>> SpriteList { get => spriteList; set => spriteList = value; }
        public Dictionary<string, List<TextureSheet2D>> SpriteSheetList { get => spriteSheetList; set => spriteSheetList = value; }
        public SpriteFont NormalFont { get => normalFont; set => normalFont = value; }

        public Texture2D Pixel { get; set; }

        public SpriteContainer()
        {

        }

        public void LoadContent(ContentManager content)
        {
            // Normal Font
            NormalFont = content.Load<SpriteFont>("Font/NormalFont");

            // The Pixel
            AddSprite(content.Load<Texture2D>("Images/MainSystem/Pixel"), "Pixel");

            Pixel = content.Load<Texture2D>("Images/MainSystem/Pixel");

            //Interface
            AddSprite(content.Load<Texture2D>("Images/UI/Map"), "Map");
            AddSprite(content.Load<Texture2D>("Images/UI/QuitGame"), "QuitGame");
            AddSprite(content.Load<Texture2D>("Images/UI/Options"), "Options");
            AddSprite(content.Load<Texture2D>("Images/UI/Campaign"), "Campaign");
            AddSprite(content.Load<Texture2D>("Images/UI/Load Game"), "LoadGame");
            AddSprite(content.Load<Texture2D>("Images/UI/CustomBattles"), "CustomBattles");
            AddSprite(content.Load<Texture2D>("Images/UI/Credits"), "Credits");

            // Tile Sheet
            AddSprite(content.Load<Texture2D>("Images/TileSheet/ExtraObjects128x128"), "ExtraObjects128x128");
            AddSprite(content.Load<Texture2D>("Images/TileSheet/Grassland128x128"), "Grassland128x128");

            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(16 * 128, 4 * 128), new Vector2(3*128, 4*128)), "GrayTent");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(16 * 128, 8 * 128), new Vector2(3*128, 4*128)), "BlueTent");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(16 * 128, 12 * 128), new Vector2(3*128, 4*128)), "RedTent");

            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(4 * 128, 3 * 128), new Vector2(2*128, 3*128)), "WoodSign");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(8 * 128, 5 * 128), new Vector2(1*128, 1*128)), "SignIconBlacksmith");
            AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(5 * 128, 6 * 128), new Vector2(1*128, 1*128)), "SignIconTailoring");
            //AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(2 * 128, 3 * 128), new Vector2(3*128, 4*128)), "SignIcon");
            //AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(2 * 128, 3 * 128), new Vector2(3*128, 4*128)), "SignIcon");
            //AddSprite(new TextureSheet2D(sprite["ExtraObjects128x128"], new Vector2(2 * 128, 3 * 128), new Vector2(3*128, 4*128)), "SignIcon");

            AnimationsSheet(content);
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
    }
}
