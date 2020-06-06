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
        Scene myScene;
        public SelectedObject(Scene myScene)
        {
            this.myScene = myScene;
        }
        public void Update()
        {

            if (Input.GetMouseButtonDown(EMyMouseButtons.LeftButton))
            {
                OnSelectedObject();
            }
        }

        private void OnSelectedObject()
        {
            MouseState currentMouse = MouseSettings.Instance.GetMouseState();
            Rectangle mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            for (int i = 0; i < myScene.SelectedEnabled.Count; i++)
            {
                //myScene.SelectedEnabled[i].IsSelected = false;
                if (mouseRectangle.Intersects(myScene.SelectedEnabled[i].SelectedCollisionBox))
                {
                    myScene.SelectedEnabled[i].IsSelected = true;
                }
            }
        }
    }
}
