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
    public class AsmundScene : Scene
    {

        public override void Initialize()
        {
            base.Initialize();

            //MainMenuUI tmp = new MainMenuUI(this);
            //tmp.MakeUI();

            BattleUI tmp = new BattleUI(this);
            tmp.MakeUI();

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
