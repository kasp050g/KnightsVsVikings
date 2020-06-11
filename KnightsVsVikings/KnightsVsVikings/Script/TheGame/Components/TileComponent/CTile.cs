
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
    // Kasper og Lukas
    public class CTile : Component
    {
        private Vector2 tileSize = new Vector2(Singletons.LevelInformationSingleton.TileSize, Singletons.LevelInformationSingleton.TileSize);

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
    }
}
