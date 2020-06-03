using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class CResourceTile : Component
    {
        #region Fields
        private Texture2D sprite;
        private TextureSheet2D spriteSheet;
        private Color color = Color.White;
        private SpriteEffects spriteEffects = SpriteEffects.None;
        private float layerDepth = 0.01f;
        private EOriginPosition originPositionEnum = EOriginPosition.TopLeft;
        private Vector2 offSet = new Vector2(0, 0);
        private Rectangle rectangle;
        #endregion

        #region Properties 
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public TextureSheet2D SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
        public Color Color { get => color; set => color = value; }
        public SpriteEffects SpriteEffects { get => spriteEffects; set => spriteEffects = value; }
        public float LayerDepth { get => layerDepth; set => layerDepth = value; }
        public EOriginPosition OriginPositionEnum { get => originPositionEnum; set => originPositionEnum = value; }
        public Vector2 OffSet { get => offSet; set => offSet = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        #endregion

        int tileSize = 128 / 2;

        EResourcesType resourcesType = EResourcesType.Nothing;
        TextureSheet2D delete;
        TextureSheet2D gold;
        TextureSheet2D stone;
        TextureSheet2D wood;
        TextureSheet2D food;

        CTile tile;

        public override void Awake()
        {
            base.Awake();
            delete = SpriteContainer.Instance.TileSprite.Delete;
            gold = SpriteContainer.Instance.TileSprite.Gold;
            stone = SpriteContainer.Instance.TileSprite.Stone;
            wood = SpriteContainer.Instance.TileSprite.Wood;
            food = SpriteContainer.Instance.TileSprite.Wheatfield;            
        }

        public override void Start()
        {
            base.Start();
            tile = GameObject.GetComponent<CTile>();
            UpdateResourcesSprite(EResourcesType.Nothing);
        }

        public override void Destroy()
        {
            base.Destroy();
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

        public override void Update()
        {
            base.Update();
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

            tile.ResourcesType = resourcesType;
        }

        private void SetSprite(TextureSheet2D spriteName)
        {
            spriteSheet = spriteName;
            sprite = spriteName.Sprite;
            rectangle = spriteName.Rectangle;
        }

        private void RemoveImage()
        {
            spriteSheet = null;
            sprite = null;
        }
    }
}
