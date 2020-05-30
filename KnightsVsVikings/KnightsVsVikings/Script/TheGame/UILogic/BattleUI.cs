using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsVsVikings
{
    
    public class BattleUI
    {
        Scene myScene;
        public BattleUI(Scene myScene)
        {
            this.myScene = myScene;
        }
        public void MakeUI()
        {
            MouseSettings.Instance.IsMouseVisible(true);

            Texture2D texture1 = SpriteContainer.Instance.Sprite["Button_A_Long_black"];

            CreateStuff("\n       Damage:", new Vector2(620, 180), texture1);
        }

        private void CreateStuff(string desc, Vector2 pos, Texture2D texture)
        {
            GameObject go = new GameObject();
            CSpriteRenderer iconSR = new CSpriteRenderer(texture);
            go.Transform.Position = pos;
            GUIText text = new GUIText(SpriteContainer.Instance.MediaevalFont, SpriteContainer.Instance.TextColor, new Vector2(1f, 1f), desc, EOriginPosition.TopLeft);
            GUIImage iconImage = new GUIImage(iconSR, false, false);
            go.AddComponent<CSpriteRenderer>(iconSR);
            go.AddComponent<GUIImage>(iconImage);
            go.Transform.Scale = new Vector2(0.5f, 0.5f);
            go.AddComponent<GUIText>(text);
            myScene.Instantiate(go);
        }
    }
}
