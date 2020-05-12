using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsVsVikings;

namespace MainSystemFramework
{
    public class MouseSettings
    {
        #region Singleton
        private static MouseSettings instance;
        public static MouseSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MouseSettings();
                }
                return instance;
            }
        }
        #endregion

        public GameWorld GameWorld { get; set; }
        public bool isMouseVisible { get; private set; }

        public void IsMouseVisible(bool showMouse)
        {
            GameWorld.IsMouseVisible = showMouse;
            isMouseVisible = showMouse;
        }
    }
}
