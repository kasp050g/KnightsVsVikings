using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.WorldEditor
{
    public class TileGrid
    {
        private Scene myScene;
        private GameObject[,] tileGrid = new GameObject[2,2];

        public TileGrid(Scene myScene)
        {
            this.myScene = myScene;
        }
        public void MakeTileGrid()
        {
            //for (int x = 0; x < tileGrid.GetLength(0); x++)
            //{
            //    for (int y = 0; y < tileGrid.GetLength(1); y++)
            //    {
            //        MadeTile(new Vector2(x,y));
            //    }
            //}

            MadeTile(new Vector2(0, 0), SpriteContainer.Instance.TileSprite.Grass01);
            MadeTile(new Vector2(128, 0), SpriteContainer.Instance.TileSprite.Grass01);
            MadeTile(new Vector2(0, 128), SpriteContainer.Instance.TileSprite.Grass01);
            MadeTile(new Vector2(128, 128), SpriteContainer.Instance.TileSprite.Grass01);

            MadeTile(new Vector2(0, 256), SpriteContainer.Instance.TileSprite.Grass02);
            MadeTile(new Vector2(128, 256), SpriteContainer.Instance.TileSprite.Grass02);
            MadeTile(new Vector2(0, 384), SpriteContainer.Instance.TileSprite.Grass02);
            MadeTile(new Vector2(128, 384), SpriteContainer.Instance.TileSprite.Grass02);
        }

        private void MadeTile(Vector2 pos,TextureSheet2D textureSheet)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(textureSheet);
            CTile tile = new CTile();

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CTile>(tile);


            go.Transform.Position = new Vector2((int)pos.X,(int)pos.Y) /** tile.TileSize*/;

            myScene.Instantiate(go);
        }
    }
}
