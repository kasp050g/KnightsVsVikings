using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class UnitCommands
    {
        SelectedObject selectedObject;
        TileGrid tileGrid;
        GameObject gameObject;
        Scene myScene;
        int Xpos;
        int Ypos;

        public UnitCommands(SelectedObject selectedObject, TileGrid tileGrid,Scene myScene)
        {
            this.selectedObject = selectedObject;
            this.tileGrid = tileGrid;
            this.myScene = myScene;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(EMyMouseButtons.RightButton) && myScene.IsMouseOverUI == false)
            {
                MoveCommands();
            }
        }

        public void Start()
        {

        }

        public void CheckForTile()
        {
            int sizeOfTile = 128 / 2;

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
                    if (tileGrid.groundTileGrid[x, y].Transform.Position == new Vector2(positonX,positonY))
                    {
                        gameObject = tileGrid.groundTileGrid[x, y];
                        Xpos = x;
                        Ypos = y;
                    }
                }
            }
        }

        public void MoveCommands()
        {
            CheckForTile();
            if (gameObject != null)
            {
                for (int i = 0; i < selectedObject.UnitsSelected.Count; i++)
                {
                    for (int x = 0; x < tileGrid.unitTileGrid.GetLength(0); x++)
                    {
                        for (int y = 0; y < tileGrid.unitTileGrid.GetLength(1); y++)
                        {
                            if(tileGrid.unitTileGrid[x, y] == selectedObject.UnitsSelected[i])
                            {
                                tileGrid.unitTileGrid[x, y] = null;
                            }
                        }
                    }

                    //tileGrid.unitTileGrid[Xpos, Ypos] = selectedObject.UnitsSelected[i]; 
                    //selectedObject.UnitsSelected[i].Transform.Position = gameObject.Transform.Position;    
                    //selectedObject.UnitsSelected[i].GetComponent<CAstar>().ResetAstar();

                    selectedObject.UnitsSelected[i].GetComponent<C_FollowPath>().GetAstar(gameObject.GetComponent<CTile>());

                    //GameObject xb = selectedObject.UnitsSelected[i].GetComponent<CUnit>().Target;
                }
            }
        }
    }
}
