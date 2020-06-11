using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Patterns.CommandPattern
{
    class MoveCommand : ICommand
    {
        private GameObject gameObject;
        private Selector selector;
        private TileGrid tileGrid;

        //private int Xpos, Ypos;

        public MoveCommand(Selector selector, TileGrid tileGrid)
        {
            this.selector = selector;
            this.tileGrid = tileGrid;
        }

        public void Execute()
        {
            CheckForTile();
            if (gameObject != null)
            {
                for (int i = 0; i < selector.UnitsSelected.Count; i++)
                {
                    for (int x = 0; x < tileGrid.unitTileGrid.GetLength(0); x++)
                    {
                        for (int y = 0; y < tileGrid.unitTileGrid.GetLength(1); y++)
                        {
                            if (tileGrid.unitTileGrid[x, y] == selector.UnitsSelected[i])
                            {
                                tileGrid.unitTileGrid[x, y] = null;
                            }
                        }
                    }

                    // Lucas --
                    if (selector.UnitsSelected[i].GetComponent<CUnit>().UnitType == EUnitType.Worker)
                    {
                        if (gameObject.GetComponent<CTile>().IsResourceOccupied)
                            selector.UnitsSelected[i].GetComponent<CUnit>().LastGatheredFrom = gameObject;

                        if (!gameObject.GetComponent<CTile>().IsBlock)
                        {
                            selector.UnitsSelected[i].GetComponent<CAstar>().ResetAstar();
                            selector.UnitsSelected[i].GetComponent<CUnit>().Target = gameObject;
                        }
                    }
                    else
                    {
                        if (!gameObject.GetComponent<CTile>().IsBlock && !gameObject.GetComponent<CTile>().IsResourceOccupied)
                        {
                            selector.UnitsSelected[i].GetComponent<CAstar>().ResetAstar();
                            selector.UnitsSelected[i].GetComponent<CUnit>().Target = gameObject;
                        }
                    }
                    // -- Lucas
                }
            }
        }

        private void CheckForTile()
        {
            int sizeOfTile = Singletons.LevelInformationSingleton.TileSize;

            MouseState mouse = MouseSettings.Instance.GetMouseState();
            Vector2 newPosition = new Vector2(mouse.X, mouse.Y);
            Vector2 worldPosition = Vector2.Transform(newPosition, Matrix.Invert(SceneController.Instance.Camera.Transform));

            int positonX = (int)(worldPosition.X / sizeOfTile) * sizeOfTile;
            int positonY = (int)(worldPosition.Y / sizeOfTile) * sizeOfTile;

            if (positonX < 0)
            {
                positonX -= sizeOfTile;
            }
            if (positonY < 0)
            {
                positonY -= sizeOfTile;
            }

            if (worldPosition.X > -sizeOfTile && worldPosition.X < 0.0f)
            {
                positonX = -sizeOfTile;
            }
            if (worldPosition.Y > -sizeOfTile && worldPosition.Y < 0.0f)
            {
                positonY = -sizeOfTile;
            }

            for (int x = 0; x < tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    if (tileGrid.groundTileGrid[x, y].Transform.Position == new Vector2(positonX, positonY))
                    {
                        gameObject = tileGrid.groundTileGrid[x, y];
                        //Xpos = x;
                        //Ypos = y;
                    }
                }
            }
        }
    }
}
