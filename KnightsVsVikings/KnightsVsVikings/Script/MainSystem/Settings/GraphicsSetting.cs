using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    // Kasper  Fly
    public class GraphicsSetting
    {
        #region Singleton
        private static GraphicsSetting instance;
        public static GraphicsSetting Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GraphicsSetting();
                }
                return instance;
            }
        }
        #endregion

        public GraphicsDeviceManager Graphics { get; private set; }
        //public Vector2 ScreenSize { get; set; } = new Vector2(1280, 720);
        public Vector2 ScreenSize { get; set; } = new Vector2(1920, 1080);
        //public Vector2 ScreenSize { get; set; } = new Vector2(2560, 1440);
        public Vector2 ScreenScale { get { return new Vector2(ScreenSize.X / 1280, ScreenSize.Y / 720); } }
        public Vector2 GameZome { get; set; } = new Vector2(1, 1);


        public GraphicsSetting()
        {

        }

        public GraphicsSetting(int width, int height)
        {
            ScreenSize = new Vector2(width, height);
        }

        public void SetGraphics(GraphicsDeviceManager graphic)
        {
            Graphics = graphic;
            UpdateScreenSize();
        }

        public void UpdateScreenSize()
        {
            Graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            Graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
        }

        public void UpdateGraphics(int width, int height)
        {
            ScreenSize = new Vector2(width, height);
            UpdateScreenSize();
        }
    }
}
