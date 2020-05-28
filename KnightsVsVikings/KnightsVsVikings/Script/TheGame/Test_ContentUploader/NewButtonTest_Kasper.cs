using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class NewButtonTest_Kasper
    {
        Scene myScene;
        public NewButtonTest_Kasper(Scene myScene)
        {
            this.myScene = myScene;
        }

        public void MadeUI()
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("Button_A_Long_black");

            Texture2D tmp1 = SpriteContainer.Instance.Sprite["Button_A_Long_black"];
            Texture2D tmp2 = SpriteContainer.Instance.Sprite["Button_A_Long_red"];
            GUIButton btn = new GUIButton(sr, tmp1, tmp2, Color.White,Color.White,SpriteContainer.Instance.MediaevalFont,SpriteContainer.Instance.TextColor,new Vector2(0.5f,0.5f),"Play Game");
            go.AddComponent<CSpriteRenderer>();
            go.AddComponent<GUIButton>(btn);
            go.Transform.Scale = new Vector2(1, 1);
            myScene.Instantiate(go);
        }
    }
}
