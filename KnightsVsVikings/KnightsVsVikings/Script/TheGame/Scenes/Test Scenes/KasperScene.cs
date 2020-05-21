using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsVsVikings.Script.TheGame.Test;
using MainSystemFramework;

namespace KnightsVsVikings
{
    public class KasperScene : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
            TestZone();
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


        public void TestZone()
        {
            Test_JAmen test_JAmen = new Test_JAmen();
            Instantiate(test_JAmen.Jamen());
        }
    }
}
