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

            Texture2D texture1 = SpriteContainer.Instance.Sprite["Button_A_Long_black"];
            Texture2D texture2 = SpriteContainer.Instance.Sprite["Button_A_Long_red"];

            MakeButton(texture1, texture2, "Campaign", new Vector2(10, 20));
            MakeButton(texture1, texture2, "Options", new Vector2(10, 160));
            MakeButton(texture1, texture2, "Credits", new Vector2(10, 300));
            MakeButton(texture1, texture2, "Exit Game", new Vector2(10, 440));
        }

        private void MakeButton(Texture2D texture1, Texture2D texture2,string text,Vector2 pos)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer();

            GUIButton btn = new GUIButton(sr, texture1, texture2, Color.White, Color.White, SpriteContainer.Instance.MediaevalFont, SpriteContainer.Instance.TextColor, new Vector2(1f, 1f), text);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIButton>(btn);

            go.Transform.Scale = new Vector2(0.4f, 0.4f);
            go.Transform.Position = pos;
            sr.LayerDepth = 0.5f;
            myScene.Instantiate(go);
        }
    }
}
