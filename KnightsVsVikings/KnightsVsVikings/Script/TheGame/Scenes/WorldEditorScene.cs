using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainSystemFramework;

namespace KnightsVsVikings
{
    public class WorldEditorScene : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
            MouseSettings.Instance.IsMouseVisible(true);
            ActionBarUI actionBarUI = new ActionBarUI(this);
            actionBarUI.MakeUI();
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
