using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsVsVikings.Script.MainSystem.Enum;
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
            TestUnit(EFactions.Knights, EUnitType.Worker, 0);
            TestUnit(EFactions.Knights, EUnitType.Bowman, 1);
            TestUnit(EFactions.Knights, EUnitType.Spearman, 2);
            TestUnit(EFactions.Knights, EUnitType.Footman, 3);

            TestUnit(EFactions.Vikings, EUnitType.Worker, 4);
            TestUnit(EFactions.Vikings, EUnitType.Bowman, 5);
            TestUnit(EFactions.Vikings, EUnitType.Spearman, 6);
            TestUnit(EFactions.Vikings, EUnitType.Footman, 7);
        }

        public void TestUnit(EFactions factions, EUnitType unitType,int number)
        {
            Test_JAmen test_JAmen = new Test_JAmen();
            GameObject tmp = test_JAmen.Jamen(factions, unitType);
            tmp.Transform.Position -= new Microsoft.Xna.Framework.Vector2(650 - 150 * number, 0);
            Instantiate(tmp);
        }
    }
}
