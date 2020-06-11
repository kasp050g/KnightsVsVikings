using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.Script.TheGame.Patterns.CommandPattern;
using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class UnitCommands
    {
        private static MoveCommand moveCommand;
        private Scene myScene;

        public UnitCommands(Selector selector, TileGrid tileGrid,Scene myScene)
        {
            this.myScene = myScene;
            moveCommand = new MoveCommand(selector, tileGrid);
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(EMyMouseButtons.RightButton) && myScene.IsMouseOverUI == false)
            {
                moveCommand.Execute();
            }
        }
    }
}
