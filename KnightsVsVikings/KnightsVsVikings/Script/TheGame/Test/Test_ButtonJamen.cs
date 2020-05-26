using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class Test_ButtonJamen
    {
        Scene myScene;
        public Test_ButtonJamen(Scene myScene)
        {
            this.myScene = myScene;
        }
        public void JamenTest()
        {
            GameObject gameObject = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(SpriteContainer.Instance.Pixel);
            GUIButton button = new GUIButton(SpriteContainer.Instance.Sprite["Campaign"], SpriteContainer.Instance.Sprite["Options"],Color.White,Color.Red, sr);
            gameObject.AddComponent<CSpriteRenderer>(sr);
            gameObject.AddComponent<GUIButton>(button);
            gameObject.Transform.Scale = new Vector2(1, 1);

            myScene.Instantiate(gameObject);
        }
    }
}
