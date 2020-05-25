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
    public class PickScene : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
            MakeUIPicker();
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
            if (Input.GetKeyDown(Keys.D1))
            {
                SceneController.Instance.CurrentScene = SceneController.Instance.SceneContainer.Scenes[1];
            }
            if (Input.GetKeyDown(Keys.D2))
            {
                SceneController.Instance.CurrentScene = SceneController.Instance.SceneContainer.Scenes[2];
            }
            if (Input.GetKeyDown(Keys.D3))
            {
                SceneController.Instance.CurrentScene = SceneController.Instance.SceneContainer.Scenes[3];
            }
            if (Input.GetKeyDown(Keys.D4))
            {
                SceneController.Instance.CurrentScene = SceneController.Instance.SceneContainer.Scenes[4];
            }
            if (Input.GetKeyDown(Keys.D5))
            {
                SceneController.Instance.CurrentScene = SceneController.Instance.SceneContainer.Scenes[5];
            }
            if (Input.GetKeyDown(Keys.D6))
            {
                SceneController.Instance.CurrentScene = SceneController.Instance.SceneContainer.Scenes[6];
            }
        }

        public void MakeUIPicker()
        {
            GameObject go = new GameObject();

            string ScneNames = "";
            for (int i = 1; i < SceneController.Instance.SceneContainer.Scenes.Count; i++)
            {
                ScneNames += "\n " + i + ": " + SceneController.Instance.SceneContainer.Scenes[i].Name + " ";
            }

            go.AddComponent<GUIText>(new GUIText(SpriteContainer.Instance.NormalFont, Color.Black, new Vector2(2, 2), "Click Number to Pick Scene." +
                ScneNames));
            Instantiate(go);
        }
    }
}
