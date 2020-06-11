using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsVsVikings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MainSystemFramework
{
    // Kasper  Fly
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

        private MouseState MouseState;
        public GameWorld GameWorld { get; set; }
        public bool isMouseVisible { get; private set; }

        public void SetMouseVisible(bool showMouse)
        {
            GameWorld.IsMouseVisible = showMouse;
            isMouseVisible = showMouse;
        }
        public void SetMouseState(MouseState currentMouse)
        {
            MouseState = currentMouse;
        }
        public MouseState GetMouseState()
        {
            return MouseState;
        }
    }
}
