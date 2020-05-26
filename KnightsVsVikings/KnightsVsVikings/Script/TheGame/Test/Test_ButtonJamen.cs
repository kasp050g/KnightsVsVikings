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
            GUIButton button = new GUIButton(SpriteContainer.Instance.SpriteSheet["WoodSign"], SpriteContainer.Instance.SpriteSheet["WoodSign"],Color.White,Color.Red, sr);
            gameObject.AddComponent<CSpriteRenderer>(sr);
            gameObject.AddComponent<GUIButton>(button);
            gameObject.Transform.Scale = new Vector2(1, 1);

            myScene.Instantiate(gameObject);

            GameObject go = new GameObject();
            CSpriteRenderer sr01 = new CSpriteRenderer("SignIconBlacksmith",EOriginPosition.TopLeft,0.1f);
            go.AddComponent<CSpriteRenderer>(sr01);
            go.AddComponent<GUIImage>();
            go.Transform.Position = gameObject.Transform.Position + new Vector2(128, 128);
            myScene.Instantiate(go);

            button.OnHorering += () => { sr01.SetSprite("SignIconTailoring"); };
            button.OnClick += () => { sr01.SetSprite("SignIconBlacksmith"); };
        }

        public void ButtonHoveringTest(GUIButton button, CSpriteRenderer sr01)
        {
            
        }
    }
}
