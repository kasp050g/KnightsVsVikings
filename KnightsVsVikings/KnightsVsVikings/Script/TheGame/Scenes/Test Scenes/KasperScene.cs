using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsVsVikings.Script.MainSystem.Enum;

using MainSystemFramework;

namespace KnightsVsVikings
{
    public class KasperScene : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
            TestZone();
            MouseSettings.Instance.IsMouseVisible(true);

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
            TestUnit(EFaction.Knights, EUnitType.Worker, 0);
            TestUnit(EFaction.Knights, EUnitType.Bowman, 1);
            TestUnit(EFaction.Knights, EUnitType.Spearman, 2);
            TestUnit(EFaction.Knights, EUnitType.Footman, 3);

            TestUnit(EFaction.Vikings, EUnitType.Worker, 4);
            TestUnit(EFaction.Vikings, EUnitType.Bowman, 5);
            TestUnit(EFaction.Vikings, EUnitType.Spearman, 6);
            TestUnit(EFaction.Vikings, EUnitType.Footman, 7);

            //NewButtonTest_Kasper btn_Test = new NewButtonTest_Kasper(this);
            //btn_Test.MadeUI();

            Test_ButtonJamen test_J = new Test_ButtonJamen(this);
            test_J.JamenTest();
        }

        public void TestUnit(EFaction factions, EUnitType unitType, int number)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sp = new CSpriteRenderer(SpriteContainer.Instance.Pixel);
            CAnimator animator = new CAnimator();
            CUnit unit = new CUnit(ETeam.Team01, unitType, factions);
            CMove move = new CMove();

            go.AddComponent<CUnit>(unit);
            go.AddComponent<CMove>(move);
            go.AddComponent<CSpriteRenderer>(sp);
            go.AddComponent<CAnimator>(animator);

            go.Transform.Position -= new Microsoft.Xna.Framework.Vector2(650 - 150 * number, 0);
            Instantiate(go);
        }
    }
}
