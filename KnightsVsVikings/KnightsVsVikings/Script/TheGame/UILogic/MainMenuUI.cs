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
    public class MainMenuUI
    {
        Scene myScene;

        GameObject MainMenuGameObject = new GameObject();
        GameObject CampaignMenuGameObject = new GameObject();

        GUIButton quitGame;
        GUIButton credits;
        GUIButton options;
        GUIButton campaign;
        GUIButton back;
        public MainMenuUI(Scene myScene)
        {
            this.myScene = myScene;
        }

        public void MakeUI()
        {
            MouseSettings.Instance.IsMouseVisible(true);
            

            myScene.Instantiate(MainMenuGameObject);
            myScene.Instantiate(CampaignMenuGameObject);
            
            CampaignMenuGameObject.IsActive = false;

            Texture2D texture1 = SpriteContainer.Instance.Sprite["Button_A_Long_black"];
            Texture2D texture2 = SpriteContainer.Instance.Sprite["Button_A_Long_red"];
           
            MakeButton(texture1, texture2, "Campaign", new Vector2(10, 20), ref campaign, "main");
            MakeButton(texture1, texture2, "Options", new Vector2(10, 160), ref options, "main");
            MakeButton(texture1, texture2, "Credits", new Vector2(10, 300), ref credits, "main");
            MakeButton(texture1, texture2, "Exit Game", new Vector2(10, 600), ref quitGame, "main");
            
            MakeButton(texture1, texture2, "Vikings", new Vector2(280, 400), ref quitGame, "campaign");
            MakeButton(texture1, texture2, "Knights", new Vector2(620, 400), ref quitGame, "campaign");
            MakeButton(texture1, texture2, "Back", new Vector2(440, 600), ref back, "campaign");

            CreateCampaignIcons("VikingsCampaign", new Vector2(320, 180));
            CreateCampaignIcons("KnightsCampaign", new Vector2(620, 180));

            BtnDoStuff();
        }

        private void MakeButton(Texture2D texture1, Texture2D texture2, string text, Vector2 pos, ref GUIButton btn, string parent)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer();
            CreateBackground();

            btn = new GUIButton(sr, texture1, texture2, Color.White, Color.White, SpriteContainer.Instance.MediaevalFont, SpriteContainer.Instance.TextColor, new Vector2(1f, 1f), text);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIButton>(btn);

            go.Transform.Scale = new Vector2(0.3f, 0.3f);
            go.Transform.Position = pos;
            sr.LayerDepth = 0.5f;

            if (parent == "main")
            {
                go.MyParent = MainMenuGameObject;
            }
           else if (parent == "campaign")
            {
                go.MyParent = CampaignMenuGameObject;
            }

            myScene.Instantiate(go);
        }

        private void CreateBackground()
        {
            GameObject background = new GameObject();
            CSpriteRenderer backgroundSR = new CSpriteRenderer("Map", EOriginPosition.TopLeft, 0.01f);
            GUIImage backgroundImage = new GUIImage(backgroundSR, false, false);
            background.AddComponent<CSpriteRenderer>(backgroundSR);
            background.AddComponent<GUIImage>(backgroundImage);
            background.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2048, 1536); //2048, 1536 is the image's default size

            myScene.Instantiate(background);
        }
        private void CreateCampaignIcons(string name, Vector2 pos)
        {
            GameObject go = new GameObject();
            CSpriteRenderer backgroundSR = new CSpriteRenderer(name, EOriginPosition.TopLeft, 0.02f);
            go.Transform.Position = pos;
            GUIImage backgroundImage = new GUIImage(backgroundSR, false, false);
            go.AddComponent<CSpriteRenderer>(backgroundSR);
            go.AddComponent<GUIImage>(backgroundImage);
            myScene.Instantiate(go);
            go.MyParent = CampaignMenuGameObject;
        }

        private void BtnDoStuff()
        {
           //quitGame.OnClick = () => { /*TODO EXIT GAME*/ ; };
            campaign.OnClick = () => { CampaignMenuGameObject.IsActive = true; MainMenuGameObject.IsActive = false;  };
            options.OnClick = () => { MainMenuGameObject.IsActive = false; };
            back.OnClick = () => { CampaignMenuGameObject.IsActive = false; MainMenuGameObject.IsActive = true; };
            credits.OnClick = () => { MainMenuGameObject.IsActive = false; };
            quitGame.OnClick = () => { MainMenuGameObject.IsActive = false; };
            //campaign.OnClick = () => { CampaignMenuGameObject.IsActive = true; };
        }
    }
}
