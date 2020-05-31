﻿using System;
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
        Vector2 fontScale = new Vector2(0.6f, 0.6f);
        public BattleUI(Scene myScene)
        {
            this.myScene = myScene;
        }
        public void MakeUI()
        {
            MouseSettings.Instance.IsMouseVisible(true);

            Texture2D texture1 = SpriteContainer.Instance.Sprite["ActionBar02"];
            Texture2D gold = SpriteContainer.Instance.Sprite["Gold"];
            Texture2D iron = SpriteContainer.Instance.Sprite["Iron"];
            Texture2D wood = SpriteContainer.Instance.Sprite["Wood"];
            Texture2D food = SpriteContainer.Instance.Sprite["Food"];

            CreateBorder(new Vector2(300, 525), texture1, new Vector2(0.8f, 0.45f), 0.1f);
            CreateBorder(new Vector2(0, 460), texture1, new Vector2(0.43f, 0.6f), 0.1f);
            CreateBorder(new Vector2(980, 460), texture1, new Vector2(0.43f, 0.6f), 0.1f);
            CreateBorder(new Vector2(855, 525), texture1, new Vector2(0.18f, 0.29f), 0.1f);
            CreateBorder(new Vector2(855, 650), texture1, new Vector2(0.18f, 0.16f), 0.1f);
            
            CreateBorder(new Vector2(-50, 0), texture1, new Vector2(2.0f, 0.1f), 0.1f);
            
            CreateBorder(new Vector2(360, 0), gold, new Vector2(0.15f, 0.15f), 0.3f);
            CreateBorder(new Vector2(560, 0), iron, new Vector2(0.15f, 0.15f), 0.3f);
            CreateBorder(new Vector2(760, 0), wood, new Vector2(0.15f, 0.15f), 0.3f);
            CreateBorder(new Vector2(960, 0), food, new Vector2(0.15f, 0.15f), 0.3f);

            CreateText("Dmg:", new Vector2(350, 550));
            CreateText("Arm:", new Vector2(350, 610));
            CreateText("Life:", new Vector2(350, 670));
            CreateText("12", new Vector2(425, 550));
            CreateText("2", new Vector2(425, 610));
            CreateText("80", new Vector2(425, 670));

            CreateText("Minimap", new Vector2(90, 470));
            CreateText("Commands", new Vector2(1060, 470));
            CreateText("Portrait", new Vector2(870, 530));
            CreateText("Menu", new Vector2(880, 650));
            
            CreateText("0", new Vector2(400, 0));
            CreateText("0", new Vector2(600, 0));
            CreateText("0", new Vector2(800, 0));
            CreateText("0", new Vector2(1000, 0));
        }

        private void CreateBorder(Vector2 pos, Texture2D texture, Vector2 scale, float depth)
        {
            GameObject go = new GameObject();
            CSpriteRenderer iconSR = new CSpriteRenderer(texture);
            GUIImage iconImage = new GUIImage(iconSR, false, false);
            go.AddComponent<CSpriteRenderer>(iconSR);
            go.AddComponent<GUIImage>(iconImage);
            go.Transform.Scale = scale;
            go.Transform.Position = pos;
            myScene.Instantiate(go);
            iconSR.LayerDepth = depth;
        }
        private void CreateText(string desc, Vector2 pos)
        {
            GameObject go = new GameObject();
            GUIText text = new GUIText(SpriteContainer.Instance.MediaevalFont, SpriteContainer.Instance.TextColor, new Vector2(1f, 1f), desc, EOriginPosition.TopLeft);
            text.LayerDepth = 0.2f;
            text.FontScale = fontScale;
            go.AddComponent<GUIText>(text);
            go.Transform.Position = pos;
            myScene.Instantiate(go);
        }
    }
}
