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
       Vector2 relativeSize = GraphicsSetting.Instance.ScreenSize;
        public override void Initialize()
        {
            base.Initialize();

            GameObject background = new GameObject();
            CSpriteRenderer backgroundSR = new CSpriteRenderer("Map", EOriginPosition.TopLeft, 0.01f);
            GUIImage backgroundImage = new GUIImage(backgroundSR, false, false);
            background.AddComponent<CSpriteRenderer>(backgroundSR);
            background.AddComponent<GUIImage>(backgroundImage);
            background.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2048, 1536); //2048, 1536 is the image's default size

            GameObject quitGame = new GameObject();
            CSpriteRenderer quitGameSR = new CSpriteRenderer("QuitGame", EOriginPosition.TopLeft, 0.02f);
            GUIImage quitGameImage = new GUIImage(quitGameSR, false, false);
            quitGame.AddComponent<CSpriteRenderer>(quitGameSR);
            quitGame.AddComponent<GUIImage>(quitGameImage);
            quitGame.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2222, 540); //2222, 340 is a tenth of the image's default size
            quitGame.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X / 100, GraphicsSetting.Instance.ScreenSize.Y / 1.2f);

            GameObject campaign = new GameObject();
            CSpriteRenderer campaignSR = new CSpriteRenderer("Campaign", EOriginPosition.TopLeft, 0.02f);
            GUIImage campaignImage = new GUIImage(campaignSR, false, false);
            campaign.AddComponent<CSpriteRenderer>(campaignSR);
            campaign.AddComponent<GUIImage>(campaignImage);
            campaign.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2222, 540); //2222, 340 is a tenth of the image's default size
            campaign.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X / 100, GraphicsSetting.Instance.ScreenSize.Y / 2.5f);

            GameObject credits = new GameObject();
            CSpriteRenderer creditsSR = new CSpriteRenderer("Credits", EOriginPosition.TopLeft, 0.02f);
            GUIImage creditsImage = new GUIImage(creditsSR, false, false);
            credits.AddComponent<CSpriteRenderer>(creditsSR);
            credits.AddComponent<GUIImage>(creditsImage);
            credits.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2222, 540); //2222, 340 is a tenth of the image's default size
            credits.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X / 100, GraphicsSetting.Instance.ScreenSize.Y / 1.4f);

            GameObject loadGame = new GameObject();
            CSpriteRenderer loadGameSR = new CSpriteRenderer("LoadGame", EOriginPosition.TopLeft, 0.02f);
            GUIImage loadGameImage = new GUIImage(loadGameSR, false, false);
            loadGame.AddComponent<CSpriteRenderer>(loadGameSR);
            loadGame.AddComponent<GUIImage>(loadGameImage);
            loadGame.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2222, 540); //2222, 340 is a tenth of the image's default size
            loadGame.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X / 100, GraphicsSetting.Instance.ScreenSize.Y / 1.0f);

            GameObject customBattles = new GameObject();
            CSpriteRenderer customBattlesSR = new CSpriteRenderer("CustomBattles", EOriginPosition.TopLeft, 0.02f);
            GUIImage customBattlesImage = new GUIImage(customBattlesSR, false, false);
            customBattles.AddComponent<CSpriteRenderer>(customBattlesSR);
            customBattles.AddComponent<GUIImage>(customBattlesImage);
            customBattles.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2222, 540); //2222, 340 is a tenth of the image's default size
            customBattles.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X / 100, GraphicsSetting.Instance.ScreenSize.Y / 2.0f);

            GameObject options = new GameObject();
            CSpriteRenderer optionsSR = new CSpriteRenderer("Options", EOriginPosition.TopLeft, 0.02f);
            GUIImage optionsImage = new GUIImage(optionsSR, false, false);
            options.AddComponent<CSpriteRenderer>(optionsSR);
            options.AddComponent<GUIImage>(optionsImage);
            options.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2222, 540); //2222, 340 is a tenth of the image's default size
            options.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X / 100, GraphicsSetting.Instance.ScreenSize.Y / 1.65f);

            Instantiate(background);
            Instantiate(quitGame);
            Instantiate(campaign);
            Instantiate(credits);
            //Instantiate(loadGame);
            Instantiate(options);
            Instantiate(customBattles);
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
    }
}
