using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    // Kasper 
    public class CResourceTile : Component
    {
        #region Fields
        private Texture2D sprite;
        private TextureSheet2D spriteSheet;
        private Color color = Color.White;
        private SpriteEffects spriteEffects = SpriteEffects.None;
        private float layerDepth = 0.3f;
        private Vector2 offSet = new Vector2(0, 0);
        private Rectangle rectangle;
        private int tileSize = Singletons.LevelInformationSingleton.TileSize;
        private EResourcesType resourcesType = EResourcesType.Nothing;
        private TextureSheet2D delete;
        private TextureSheet2D gold;
        private TextureSheet2D stone;
        private TextureSheet2D wood;
        private TextureSheet2D food;
        #endregion

        #region Properties 
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public Color Color { get => color; set => color = value; }
        public SpriteEffects SpriteEffects { get => spriteEffects; set => spriteEffects = value; }
        public float LayerDepth { get => layerDepth; set => layerDepth = value; }
        public Vector2 OffSet { get => offSet; set => offSet = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public EResourcesType ResourcesType { get => resourcesType; set => resourcesType = value; }
        #endregion

        public CResourceTile()
        {

        }
        public CResourceTile(EResourcesType resourcesType)
        {
            this.resourcesType = resourcesType;
        }

        public override void Awake()
        {
            base.Awake();
            delete = SpriteContainer.Instance.TileSprite.Delete;
            gold = SpriteContainer.Instance.TileSprite.Gold;
            stone = SpriteContainer.Instance.TileSprite.Stone;
            wood = SpriteContainer.Instance.TileSprite.Wood;
            food = SpriteContainer.Instance.TileSprite.Wheatfield;
            UpdateResourcesSprite(resourcesType);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(
                    // Texture2D
                    this.Sprite,
                    // Postion
                    this.GameObject.Transform.Position + OffSet,
                    // Source Rectangle
                    Rectangle,
                    // Color
                    this.Color,
                    // Rotation
                    MathHelper.ToRadians(this.GameObject.Transform.Rotation),
                    // Origin
                    this.GameObject.Transform.Origin,
                    // Scale
                    this.GameObject.Transform.Scale,
                    // SpriteEffects
                    this.SpriteEffects,
                    // LayerDepth
                    this.LayerDepth + GameObject.Transform.Position.Y / 100000 + GameObject.Transform.Position.X / 110000
                );
        }

        public void UpdateResourcesSprite(EResourcesType resourcesType)
        {
            this.resourcesType = resourcesType;

            switch (resourcesType)
            {
                case EResourcesType.Nothing:
                    SetSprite(delete);
                    OffSet = new Vector2(0 * -tileSize, 0 * -tileSize);                    
                    break;
                case EResourcesType.Gold:
                    SetSprite(gold);
                    OffSet = new Vector2(0 * -tileSize, 1 * -tileSize);
                    break;
                case EResourcesType.Stone:
                    SetSprite(stone);
                    OffSet = new Vector2(0 * -tileSize, 1 * -tileSize);
                    break;
                case EResourcesType.Wood:
                    SetSprite(wood);
                    OffSet = new Vector2(1 * -tileSize, 4 * -tileSize);
                    break;
                case EResourcesType.Food:
                    SetSprite(food);
                    OffSet = new Vector2(1 * -tileSize, 1 * -tileSize);
                    break;
                default:
                    break;
            }
        }

        private void SetSprite(TextureSheet2D spriteName)
        {
            spriteSheet = spriteName;
            sprite = spriteName.Sprite;
            rectangle = spriteName.Rectangle;
        }
    }
}
