using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace KnightsVsVikings
{
    public class AsmundScene : Scene
    {
        float position = 1.25f; //this is the starting position of the first created button
        public override void Initialize()
        {
            base.Initialize();

            CreateBackground();

            MakeButton("QuitGame", "QuitGameHover");
            MakeButton("Credits", "CreditsHover");
            MakeButton("Options", "OptionsHover");
            MakeButton("Campaign", "CampaignHover");


   
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
        }
        private void CreateBackground()
        {
            GameObject background = new GameObject();
            CSpriteRenderer backgroundSR = new CSpriteRenderer("Map", EOriginPosition.TopLeft, 0.01f);
            GUIImage backgroundImage = new GUIImage(backgroundSR, false, false);
            background.AddComponent<CSpriteRenderer>(backgroundSR);
            background.AddComponent<GUIImage>(backgroundImage);
            background.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2048, 1536); //2048, 1536 is the image's default size

            Instantiate(background);
        }
        private void MakeButton(string name, string hoverName)
        {
            GameObject go = new GameObject( );
            CSpriteRenderer sr = new CSpriteRenderer(name, EOriginPosition.TopLeft, 0.02f);
            CSpriteRenderer hover = new CSpriteRenderer(hoverName, EOriginPosition.TopLeft, 0.02f);
            GUIImage image = new GUIImage(sr, false, false);
            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIImage>(image);
            go.Transform.Scale = go.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(1222, 540);
            go.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X / 100, GraphicsSetting.Instance.ScreenSize.Y / position);
            
            Texture2D texture1 = sr.Sprite;
            Texture2D texture2 = hover.Sprite;
            GUIButton button = new GUIButton(sr, texture1, texture2, Color.White, Color.White);
            go.AddComponent<GUIButton>(button);

            Instantiate(go);

            position = position * 1.3f; //this is to relocate the next button further up
        }
    }
}
