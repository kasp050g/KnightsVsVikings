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
    public class SelectedObject
    {
        private Scene myScene;
        private GameObject selectedGameObject;
        private Vector2 startSelectedPosition;
        private Vector2 endSelectedPosition;
        private Rectangle mouseRectangle;
        private List<GameObject> unitsSelected = new List<GameObject>();
        public List<GameObject> UnitsSelected { get => unitsSelected; private set => unitsSelected = value; }

        public SelectedObject(Scene myScene)
        {
            this.myScene = myScene;
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
            if (Input.GetMouseButtonDown(EMyMouseButtons.LeftButton))
            {
                ClickDown();                
            }
            if (Input.GetMouseButton(EMyMouseButtons.LeftButton))
            {
                ClickGet();
            }
            if (Input.GetMouseButtonUp(EMyMouseButtons.LeftButton))
            {
                ClickUp();                
            }
        }

        private void ClickDown()
        {
            selectedGameObject.SetIsActive(true);
            startSelectedPosition = MouseWorldPosition();
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
                    UnitsSelected.Add(myScene.SelectedEnabled[i].GameObject);
                    myScene.SelectedEnabled[i].IsSelected = true;
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
