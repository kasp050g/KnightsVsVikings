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
            CSpriteRenderer sr = new CSpriteRenderer("Map", EOriginPosition.TopLeft, 0.01f);

            GUIImage image = new GUIImage(sr, false, false);
            background.AddComponent<CSpriteRenderer>(sr);
            background.AddComponent<GUIImage>(image);

            background.Transform.Scale = GraphicsSetting.Instance.ScreenSize / new Vector2(2048, 1536); //2048, 1536 is the image's default size

           Instantiate(background);
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
