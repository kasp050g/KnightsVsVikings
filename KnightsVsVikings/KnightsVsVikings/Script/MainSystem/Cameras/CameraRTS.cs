using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class CameraRTS : Camera
    {
        private float moveSpeed = 500;

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

        public override void Update()
        {
            base.Update();
            MoveCamare();
        }

        public override void OnSwitchScene()
        {
            base.OnSwitchScene();
        }

        /// <summary>
        /// Move Camare with keyborde
        /// </summary>
        public void MoveCamare()
        {
            Vector2 newMove = new Vector2(0, 0);

            if (Input.GetKey(Keys.W))
            {
                newMove += new Vector2(0, -1);
            }
            if (Input.GetKey(Keys.S))
            {
                newMove += new Vector2(0, 1);
            }
            if (Input.GetKey(Keys.A))
            {
                newMove += new Vector2(-1, 0);
            }
            if (Input.GetKey(Keys.D))
            {
                newMove += new Vector2(1, 0);
            }

            if (Input.GetKey(Keys.LeftShift))
            {
                newMove *= 3;
            }

            Position += newMove * (int)(moveSpeed * Time.DeltaTime);

            Matrix position = Matrix.CreateTranslation(
                -Position.X,
                -Position.Y,
                0);

            Matrix offset = Matrix.CreateTranslation(
                GraphicsSetting.Instance.ScreenSize.X / 2,
                GraphicsSetting.Instance.ScreenSize.Y / 2,
                0);

            transform = position * offset;
        }
    }
}
