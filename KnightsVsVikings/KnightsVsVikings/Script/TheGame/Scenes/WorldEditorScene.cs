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
        SelectedObject selectedObject;
        UnitCommands unitCommands;
        public override void Initialize()
        {
            base.Initialize();
            MouseSettings.Instance.SetMouseVisible(true);

            TestZone();

            selectedObject = new SelectedObject(this,placeTile);
            selectedObject.MakeSelectedZoneUI();
            unitCommands = new UnitCommands(selectedObject, tileGrid, this);
            unitCommands.Start();
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
            selectedObject.Update();
            unitCommands.Update();

            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.O))
            {
                tileGrid.ShowGridColor = !tileGrid.ShowGridColor;
                tileGrid.UpdateGrid();
            }
            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.I))
            {
                SceneController.Instance.CurrentScene = SceneController.Instance.SceneContainer.Scenes[0];
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
