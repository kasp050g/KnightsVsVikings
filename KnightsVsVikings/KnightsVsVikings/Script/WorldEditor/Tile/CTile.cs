
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
    public class CTile : Component
    {
        private Vector2 tileSize = new Vector2((int)128/2, (int)128/2);
        private Semaphore tileCapacity = new Semaphore(4, 4);

        public int H { get; set; }
        public int G { get; set; }
        public int F { get; set; }
        public CTile LastTile { get; set; }
        public EResourcesType ResourcesType { get; set; }
        public Vector2 TileSize { get => tileSize; set => tileSize = value; }
        public ETileType TileType { get; set; }
        public bool IsBlock { get; set; }
        public bool IsResourceOccupied { get; set; }
        public bool IsCanBuildHere { get; set; } = true;
        public bool IsUnitOccupied { get; set; }

        public CTile()
        {

        }
        public CTile(ETileType tileType)
        {
            this.TileType = tileType;
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

        public void GatherResource(CUnit worker)
        {
            tileCapacity.WaitOne();

            Thread.Sleep(1500);

            worker.ResourceAmount = 100;
            worker.LastGatheredFrom = GameObject;

            tileCapacity.Release();
        }
    }
}
