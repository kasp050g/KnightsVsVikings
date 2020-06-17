using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainSystemFramework;

namespace KnightsVsVikings
{
    public class MainMenuScene : Scene
    {
        MainMenuUI mainMenuUI;
        public override void Initialize()
        {
            base.Initialize();
            mainMenuUI = new MainMenuUI(this);
            mainMenuUI.MakeUI();
        }

        public override void OnSwitchAwayFromThisScene()
        {
            base.OnSwitchAwayFromThisScene();
            AudioContainer.Instance.StopSong();
        }

        public override void OnSwitchToThisScene()
        {
            base.OnSwitchToThisScene();
            AudioContainer.Instance.PlaySong("song1",0.5f);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
