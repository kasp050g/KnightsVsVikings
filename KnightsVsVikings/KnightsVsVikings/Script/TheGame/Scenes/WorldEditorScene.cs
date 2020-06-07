﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;

namespace KnightsVsVikings
{
    public class WorldEditorScene : Scene
    {
        PlaceTileWithMouse placeTile;
        TileGrid tileGrid = null;
        SelectedObject selectedObject;
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

            selectedObject = new SelectedObject(this, placeTile);
            selectedObject.MakeSelectedZoneUI();

            unitCommands = new UnitCommands(selectedObject, tileGrid, this);
            unitCommands.Start();

            List<CTile> tmp = new List<CTile>();

            for (int x = 0; x < tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    tmp.Add(tileGrid.groundTileGrid[x, y].GetComponent<CTile>());
                }
            }

            _Astar_Test.Instance.SetTileGrid(tmp);
        }
    }
}
