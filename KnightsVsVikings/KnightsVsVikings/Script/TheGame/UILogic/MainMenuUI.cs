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
    // Asmund
    public class MainMenuUI
    {
        Scene myScene;

        GameObject MainMenuGO = new GameObject();
        GameObject CampaignMenuGO = new GameObject();
        GameObject VikingsCampaignGO = new GameObject();
        GameObject KnightsCampaignGO = new GameObject();

        GUIButton quitGame;
        GUIButton credits;
        GUIButton options;
        GUIButton campaign;
        GUIButton backToMain;
        GUIButton backToCampaign;
        GUIButton vikingsCampaign;
        GUIButton knightsCampaign;
        GUIButton startGame;
        public MainMenuUI(Scene myScene)
        {
            this.myScene = myScene;
        }

        public void MakeUI()
        {
           

            MouseSettings.Instance.SetMouseVisible(true);

            myScene.Instantiate(MainMenuGO);
            myScene.Instantiate(CampaignMenuGO);
            myScene.Instantiate(VikingsCampaignGO);
            myScene.Instantiate(KnightsCampaignGO);
            
            CampaignMenuGO.SetIsActive(false);
            VikingsCampaignGO.SetIsActive(false);
            KnightsCampaignGO.SetIsActive(false);

            Texture2D texture1 = SpriteContainer.Instance.Sprite["Button_A_Long_black"];
            Texture2D texture2 = SpriteContainer.Instance.Sprite["Button_A_Long_red"];
           
            //Main Menu
            MakeButton(texture1, texture2, "Campaign", new Vector2(10, 20), ref campaign, "main");
            MakeButton(texture1, texture2, "Options", new Vector2(10, 160), ref options, "main");
            MakeButton(texture1, texture2, "Credits", new Vector2(10, 300), ref credits, "main");
            MakeButton(texture1, texture2, "Exit Game", new Vector2(10, 600), ref quitGame, "main");
            
            //Campaign Menu
            MakeButton(texture1, texture2, "Vikings", new Vector2(280, 400), ref vikingsCampaign, "campaign");
            MakeButton(texture1, texture2, "Knights", new Vector2(620, 400), ref knightsCampaign, "campaign");
            MakeButton(texture1, texture2, "Back", new Vector2(440, 600), ref backToMain, "campaign");
            CreateCampaignIcons("VikingsCampaign", new Vector2(320, 180));
            CreateCampaignIcons("KnightsCampaign", new Vector2(620, 180));

            //Vikings Menu
            MakeButton(texture1, texture2, "Back", new Vector2(440, 600), ref backToCampaign, "vikings");
            MakeButton(texture1, texture2, "Chapter 1", new Vector2(100, 100), ref startGame, "vikings");
            MakeButton(texture1, texture2, "Chapter 2", new Vector2(100, 200), ref startGame, "vikings");
            MakeButton(texture1, texture2, "Chapter 3", new Vector2(100, 300), ref startGame, "vikings");
            MakeButton(texture1, texture2, "Chapter 4", new Vector2(100, 400), ref startGame, "vikings");
            MakeButton(texture1, texture2, "Chapter 5", new Vector2(100, 500), ref startGame, "vikings");

            ButtonFunctions();
            CreateBackground();
        }

        private void MakeButton(Texture2D texture1, Texture2D texture2, string text, Vector2 pos, ref GUIButton btn, string parent)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer() { LayerDepth = 0.1f };
            

            btn = new GUIButton(sr, texture1, texture2, Color.White, Color.White, SpriteContainer.Instance.MediaevalFont, SpriteContainer.Instance.TextColor, new Vector2(1f, 1f), text);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIButton>(btn);

            go.Transform.Scale = new Vector2(0.3f, 0.3f);
            go.Transform.Position = pos;

            if (parent == "main")
            {
                go.SetMyParent(MainMenuGO);
            }
           else if (parent == "campaign")
            {
                go.SetMyParent(CampaignMenuGO);
            }
            else if (parent == "vikings")
            {
                go.SetMyParent(VikingsCampaignGO);
            }
            myScene.Instantiate(go);
        }

        private void CreateBackground()
        {
            GameObject background = new GameObject();
            CSpriteRenderer backgroundSR = new CSpriteRenderer("Map", EOriginPosition.TopLeft, 0.01f);
            GUIImage backgroundImage = new GUIImage(backgroundSR, false, false);
            
            background.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2048, 1536); //2048, 1536 is the image's default size
            
            background.AddComponent<CSpriteRenderer>(backgroundSR);
            background.AddComponent<GUIImage>(backgroundImage);

            myScene.Instantiate(background);
        }
        private void CreateCampaignIcons(string name, Vector2 pos)
        {
            GameObject go = new GameObject();
            CSpriteRenderer iconSR = new CSpriteRenderer(name, EOriginPosition.TopLeft, 0.02f);
            GUIImage iconImage = new GUIImage(iconSR, false, false);

            go.Transform.Position = pos;
            
            go.AddComponent<CSpriteRenderer>(iconSR);
            go.AddComponent<GUIImage>(iconImage);
            
            go.SetMyParent(CampaignMenuGO);
            myScene.Instantiate(go);
        }

        private void ButtonFunctions()
        {
            campaign.OnClick = () => { CampaignMenuGO.SetIsActive(true) ; MainMenuGO.SetIsActive(false);  };
            backToMain.OnClick = () => { CampaignMenuGO.SetIsActive(false); MainMenuGO.SetIsActive(true); };
            backToCampaign.OnClick = () => { CampaignMenuGO.SetIsActive(true); VikingsCampaignGO.SetIsActive(false); };
            vikingsCampaign.OnClick = () => { CampaignMenuGO.SetIsActive(false); VikingsCampaignGO.SetIsActive(true); };
            knightsCampaign.OnClick = () => { CampaignMenuGO.SetIsActive(false); VikingsCampaignGO.SetIsActive(true); };
            startGame.OnClick = () => { CampaignMenuGO.SetIsActive(true); VikingsCampaignGO.SetIsActive(false); };//HERE THE GAME STARTS!
            
            options.OnClick = () => { MainMenuGO.SetIsActive(false); };
            credits.OnClick = () => { MainMenuGO.SetIsActive(false); };
            quitGame.OnClick = () => { MainMenuGO.SetIsActive(false); }; //HERE THE GAME QUITS
          
        }
    }
}
