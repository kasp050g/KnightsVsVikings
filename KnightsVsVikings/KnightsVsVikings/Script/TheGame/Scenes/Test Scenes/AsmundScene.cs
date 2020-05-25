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
        Scene myScene;
        public override void Initialize()
        {
            base.Initialize();

            GameObject background = new GameObject();
            SpriteRenderer sr = new SpriteRenderer("Map", EOriginPosition.TopLeft, 0.01f);

            ImageGUI image = new ImageGUI(sr, false, false);
            background.AddComponent<SpriteRenderer>(sr);
            background.AddComponent<ImageGUI>(image);

            myScene.Instantiate(background);
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
