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
    // Kasper  Fly
    public class Selector
    {
        private Scene myScene;
        private GameObject selectedGameObject;
        private Vector2 startSelectedPosition;
        private Vector2 endSelectedPosition;
        private Rectangle mouseRectangle;
        private List<GameObject> unitsSelected = new List<GameObject>();
        public List<GameObject> UnitsSelected { get => unitsSelected; private set => unitsSelected = value; }
        PlaceTileWithMouse placeTileWithMouse;
        public Selector(Scene myScene)
        {
            this.myScene = myScene;
        }
        public Selector(Scene myScene, PlaceTileWithMouse placeTileWithMouse)
        {
            this.myScene = myScene;
            this.placeTileWithMouse = placeTileWithMouse;
        }

        public void MakeSelectedZoneUI()
        {
            selectedGameObject = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(SpriteContainer.Instance.Pixel);

            sr.LayerDepth = 0.9f;
            sr.Color = new Color(0, 0, 0, 100);

            selectedGameObject.AddComponent<CSpriteRenderer>(sr);

            myScene.Instantiate(selectedGameObject);

            selectedGameObject.SetIsActive(false);
        }

        public void Update()
        {
            if (placeTileWithMouse != null && placeTileWithMouse.GameObjectTileMouse.IsActive == false)
            {
                if (Input.GetMouseButtonDown(EMyMouseButtons.LeftButton) && myScene.IsMouseOverUI == false)
                {
                    ClickDown();
                }
                else if (Input.GetMouseButton(EMyMouseButtons.LeftButton) && myScene.IsMouseOverUI == false)
                {
                    ClickGet();
                }
                else if (Input.GetMouseButtonUp(EMyMouseButtons.LeftButton))
                {
                    ClickUp();
                }
            }
            else
            {
                for (int i = 0; i < UnitsSelected.Count; i++)
                {
                    UnitsSelected[i].GetComponent<CSelectable>().IsSelected = false;
                }
                UnitsSelected.Clear();
            }
        }

        private void ClickDown()
        {
            selectedGameObject.SetIsActive(true);
            startSelectedPosition = MouseWorldPosition();
            endSelectedPosition = MouseWorldPosition();
            MakeSelectedZone();
        }

        private void ClickGet()
        {
            endSelectedPosition = MouseWorldPosition();
            MakeSelectedZone();
        }

        private void ClickUp()
        {
            UnitsSelected.Clear();

            for (int i = 0; i < myScene.SelectedEnabled.Count; i++)
            {
                myScene.SelectedEnabled[i].IsSelected = false;

                if (mouseRectangle.Intersects(myScene.SelectedEnabled[i].SelectedCollisionBox) && myScene.SelectedEnabled[i].GameObject.GetComponent<CStats>().Team == ETeam.Team01)
                {
                    if (UnitsSelected.Count != 5)
                    { 
                        UnitsSelected.Add(myScene.SelectedEnabled[i].GameObject);
                        myScene.SelectedEnabled[i].IsSelected = true;
                    }
                }
            }

            selectedGameObject.SetIsActive(false);
        }

        private void MakeSelectedZone()
        {
            float xS = (startSelectedPosition.X < endSelectedPosition.X ? startSelectedPosition.X : endSelectedPosition.X);
            float xE = (startSelectedPosition.X > endSelectedPosition.X ? startSelectedPosition.X : endSelectedPosition.X);
            float yS = (startSelectedPosition.Y < endSelectedPosition.Y ? startSelectedPosition.Y : endSelectedPosition.Y);
            float yE = (startSelectedPosition.Y > endSelectedPosition.Y ? startSelectedPosition.Y : endSelectedPosition.Y);

            float xSize = (xE - xS >= 1 ? xE - xS : 1);
            float ySize = (yE - yS >= 1 ? yE - yS : 1);

            selectedGameObject.Transform.Scale = new Vector2(xSize, ySize);
            selectedGameObject.Transform.Position = new Vector2(xS, yS);

            mouseRectangle = new Rectangle((int)xS, (int)yS, (int)xSize, (int)ySize);
        }

        private Vector2 MouseWorldPosition()
        {
            MouseState currentMouse = MouseSettings.Instance.GetMouseState();
            return  Vector2.Transform(new Vector2(currentMouse.X, currentMouse.Y), Matrix.Invert(SceneController.Instance.Camera.Transform));
        }
    }
}
