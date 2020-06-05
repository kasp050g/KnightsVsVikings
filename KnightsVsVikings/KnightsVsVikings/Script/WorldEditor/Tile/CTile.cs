
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
    public class CTile : Component
    {
        private Vector2 tileSize = new Vector2((int)128/2, (int)128/2);

        public Vector2 TileSize { get => tileSize; set => tileSize = value; }
        public ETileType TileType { get; set; }
        public EResourcesType ResourcesType { get; set; }
        public bool IsBlock { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsCanBuildHere { get; set; } = true;
        public bool IsUnitOccupied { get; set; }

        public CTile()
        {

        }
        public CTile(Vector2 tileSize)
        {
            this.tileSize = tileSize;
        }
        public override void Awake()
        {
            base.Awake();
            float tmp = tileSize.X / 128;
            GameObject.Transform.Scale = new Vector2(tmp, tmp);
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
