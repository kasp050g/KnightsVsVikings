using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainSystemFramework;

namespace KnightsVsVikings
{
    public class WorldEditorScene : Scene
    {
        PlaceTileWithMouse placeTile;
        TileGrid tileGrid;
        public override void Initialize()
        {
            base.Initialize();
            MouseSettings.Instance.IsMouseVisible(true);
            
            TestZone();
        }

        public override void OnSwitchAwayFromThisScene()
        {
            base.OnSwitchAwayFromThisScene();
        }

        public override void OnSwitchToThisScene()
        {
            base.OnSwitchToThisScene();
        }

        public override void Update()
        {
            base.Update();
            placeTile.Update();

            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.O))
            {
                tileGrid.ShowGridColor = !tileGrid.ShowGridColor;
                tileGrid.UpdateGrid();
            }
        }

        private void TestZone()
        {
            tileGrid = new TileGrid(this);
            tileGrid.MakeTileGrid();

            placeTile = new PlaceTileWithMouse(this, tileGrid);
            placeTile.MadeTileShow();

            //WorldEditorActionBarUI actionBarUI = new WorldEditorActionBarUI(this, placeTile);
            //actionBarUI.MakeUI();

            WorldEditorUI worldEditorUI = new WorldEditorUI(this, placeTile,tileGrid);
            worldEditorUI.MadeUI();
        }
    }
}
