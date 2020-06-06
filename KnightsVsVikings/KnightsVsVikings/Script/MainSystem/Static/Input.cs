using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public static class Input
    {
        // Contains all current use keys
        private static Dictionary<Keys, EInputState> keyStates = new Dictionary<Keys, EInputState>();

        private static Dictionary<EMyMouseButtons, EMyInputState> myMouseStates = new Dictionary<EMyMouseButtons, EMyInputState>();

        public static void Update()
        {
            KeysUpdate();
            MouseButton();
            XboxInput();
        }

        #region Mouse Input
        public static bool GetMouseButtonDown(EMyMouseButtons monoMouseButton)
        {
            if (Input.myMouseStates.TryGetValue(monoMouseButton, out EMyInputState myInputState))
            {
                return myInputState == EMyInputState.GetMouseButtonDown;
            }
            else
            {
                return false;
            }
        }

        public static bool GetMouseButton(EMyMouseButtons monoMouseButton)
        {
            if (Input.myMouseStates.TryGetValue(monoMouseButton, out EMyInputState myInputState))
            {
                return myInputState == EMyInputState.GetMouseButtonDown || myInputState == EMyInputState.GetMouseButton;
            }
            else
            {
                return false;
            }
        }

        public static bool GetMouseButtonUp(EMyMouseButtons monoMouseButton)
        {
            if (Input.myMouseStates.TryGetValue(monoMouseButton, out EMyInputState myInputState))
            {
                return myInputState == EMyInputState.GetMouseButtonUp;
            }
            else
            {
                return false;
            }
        }

        private static void MouseButton()
        {
            MouseState monoMouseState = MouseSettings.Instance.GetMouseState();

            List<EMyMouseButtons> monoPressedMouseButtons = new List<EMyMouseButtons>();

            if (monoMouseState.LeftButton == ButtonState.Pressed) monoPressedMouseButtons.Add(EMyMouseButtons.LeftButton);
            if (monoMouseState.MiddleButton == ButtonState.Pressed) monoPressedMouseButtons.Add(EMyMouseButtons.MiddleButton);
            if (monoMouseState.RightButton == ButtonState.Pressed) monoPressedMouseButtons.Add(EMyMouseButtons.RightButton);

            MouseDown(monoPressedMouseButtons);
            MouseUp(monoPressedMouseButtons);
        }

        // Handle mouse button release
        private static void MouseDown(List<EMyMouseButtons> monoPressedMouseButtons)
        {
            var myMouseStatesNew = new Dictionary<EMyMouseButtons, EMyInputState>(Input.myMouseStates);  // Copy states

            foreach (KeyValuePair<EMyMouseButtons, EMyInputState> amaInputStatePairInLoop in Input.myMouseStates)
            {
                EMyMouseButtons monoMouseButtonInLoop = amaInputStatePairInLoop.Key;

                if (false == monoPressedMouseButtons.Contains(monoMouseButtonInLoop))
                {
                    if (amaInputStatePairInLoop.Value == EMyInputState.GetMouseButton || amaInputStatePairInLoop.Value == EMyInputState.GetMouseButtonDown)
                    {
                        myMouseStatesNew[monoMouseButtonInLoop] = EMyInputState.GetMouseButtonUp;
                    }
                    else
                    {
                        myMouseStatesNew.Remove(monoMouseButtonInLoop);
                    }
                }
            }

            Input.myMouseStates.Clear();
            Input.myMouseStates = myMouseStatesNew;
        }

        // Handle mouse button press
        private static void MouseUp(List<EMyMouseButtons> monoPressedMouseButtons)
        {
            var myMouseStatesNew = new Dictionary<EMyMouseButtons, EMyInputState>(Input.myMouseStates);  // Copy states

            foreach (EMyMouseButtons monoMouseButton in monoPressedMouseButtons)
            {
                if (Input.myMouseStates.ContainsKey(monoMouseButton))
                {
                    myMouseStatesNew[monoMouseButton] = EMyInputState.GetMouseButton;
                }
                else
                {
                    myMouseStatesNew[monoMouseButton] = EMyInputState.GetMouseButtonDown;
                }
            }

            Input.myMouseStates.Clear();
            Input.myMouseStates = myMouseStatesNew;
        }
        #endregion


        #region Key Input
        private static void KeysUpdate()
        {
            // Get current state of keyboard.
            KeyboardState keyboardState = Keyboard.GetState();
            // Check if any keys are Pressed's
            Keys[] getPressedKeys = keyboardState.GetPressedKeys();

            KeyUp(getPressedKeys);
            KeyDown(getPressedKeys);
        }

        private static void KeyUp(Keys[] getPressedKeys)
        {
            // Copy the stats of 'contains all current use keys'
            Dictionary<Keys, EInputState> myKeyStatesNew = new Dictionary<Keys, EInputState>(Input.keyStates);

            foreach (KeyValuePair<Keys, EInputState> currentKeys in Input.keyStates)
            {
                Keys keyInLoop = currentKeys.Key;

                // Here we will look if 'getPressedKeys' exists in Input.keyStates.
                // getPressedKeys is the current keys that are pressed.
                // keyInLoop is the current key in use as 'GetKey, GetKeyDown, GetKeyUp'
                // why == false, well we only want to check for GetKeyUp when you release the key.
                // TODO : why do it need ' _key => _key ==' that i dont know ¯\_(ツ)_/¯
                if (false == Array.Exists(getPressedKeys, _key => _key == keyInLoop))
                {
                    if (currentKeys.Value == EInputState.GetKey || currentKeys.Value == EInputState.GetKeyDown)
                    {
                        myKeyStatesNew[keyInLoop] = EInputState.GetKeyUp;
                    }
                    else
                    {
                        myKeyStatesNew.Remove(keyInLoop);
                    }
                }
            }

            Input.keyStates.Clear();
            Input.keyStates = myKeyStatesNew;
        }

        private static void KeyDown(Keys[] getPressedKeys)
        {
            // Copy the stats of 'contains all current use keys'
            Dictionary<Keys, EInputState> newKeyStates = new Dictionary<Keys, EInputState>(Input.keyStates);

            foreach (Keys key in getPressedKeys)
            {
                if (Input.keyStates.ContainsKey(key))
                {
                    newKeyStates[key] = EInputState.GetKey;
                }
                else
                {
                    newKeyStates[key] = EInputState.GetKeyDown;
                }
            }

            Input.keyStates.Clear();
            Input.keyStates = newKeyStates;
        }

        public static bool GetKeyDown(Keys key)
        {
            // Check if the key is in use.
            if (Input.keyStates.TryGetValue(key, out EInputState inputState))
            {
                // Check if the key is 'GetKeyDown, GetKey, GetKeyUp'
                // if it is the rigth one return true else return false.
                return inputState == EInputState.GetKeyDown;
            }
            else
            {
                return false;
            }
        }

        public static bool GetKey(Keys key)
        {
            // Check if the key is in use.
            if (Input.keyStates.TryGetValue(key, out EInputState inputState))
            {
                // Check if the key is 'GetKeyDown, GetKey, GetKeyUp'
                // if it is the rigth one return true else return false.
                return inputState == EInputState.GetKey;
            }
            else
            {
                return false;
            }
        }

        public static bool GetKeyUp(Keys key)
        {
            // Check if the key is in use.
            if (Input.keyStates.TryGetValue(key, out EInputState inputState))
            {
                // Check if the key is 'GetKeyDown, GetKey, GetKeyUp'
                // if it is the rigth one return true else return false.
                return inputState == EInputState.GetKeyUp;
            }
            else
            {
                return false;
            }
        }
        #endregion

        // TODO: Work in progress
        #region Xbox Input
        public static GamePadState gamePadState;

        public static void XboxInput()
        {
            // In Update, or some code called every frame:
            gamePadState = GamePad.GetState(PlayerIndex.One);
            // Use gamePadState to move the character
            //characterInstance.XVelocity = gamePadState.ThumbSticks.Left.X * characterInstance.MaxSpeed;
            //characterInstance.YVelocity = gamePadState.ThumbSticks.Left.Y * characterInstance.MaxSpeed;
        }
        #endregion
    }
}
