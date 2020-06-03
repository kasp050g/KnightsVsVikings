using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsVsVikings.Script.WorldEditor;
using MainSystemFramework;

namespace KnightsVsVikings
{
    public class WorldEditorScene : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
            MouseSettings.Instance.IsMouseVisible(true);
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

        private void TestZone()
        {
            WorldEditorActionBarUI actionBarUI = new WorldEditorActionBarUI(this);
            actionBarUI.MakeUI();

            TileGrid tileGrid = new TileGrid(this);
            tileGrid.MakeTileGrid();
        }
    }
}
