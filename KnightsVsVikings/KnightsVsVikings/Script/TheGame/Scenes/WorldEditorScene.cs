using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;

namespace KnightsVsVikings
{
    // Kasper  Fly
    public class WorldEditorScene : Scene
    {
        PlaceTileWithMouse placeTile;
        TileGrid tileGrid = null;
        Selector selectedObject;
        UnitCommands unitCommands;

        public override void Initialize()
        {
            base.Initialize();
            MouseSettings.Instance.SetMouseVisible(true);

            MadeStuffInStart();


        }

        public override void OnSwitchAwayFromThisScene()
        {
            base.OnSwitchAwayFromThisScene();
            AudioContainer.Instance.StopSong();
        }

        public override void OnSwitchToThisScene()
        {
            base.OnSwitchToThisScene();
            AudioContainer.Instance.PlaySong("song2", 0.5f);
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

        private void MadeStuffInStart()
        {
            tileGrid = new TileGrid(this);
            tileGrid.MakeTileGrid();

            placeTile = new PlaceTileWithMouse(this, tileGrid);
            placeTile.MadeTileShow();

            //WorldEditorActionBarUI actionBarUI = new WorldEditorActionBarUI(this, placeTile);
            //actionBarUI.MakeUI();

            WorldEditorUI worldEditorUI = new WorldEditorUI(this, placeTile,tileGrid);
            worldEditorUI.MadeUI();

            selectedObject = new Selector(this, placeTile);
            selectedObject.MakeSelectedZoneUI();

            unitCommands = new UnitCommands(selectedObject, tileGrid, this);
            //unitCommands.Start();
        }
    }
}
