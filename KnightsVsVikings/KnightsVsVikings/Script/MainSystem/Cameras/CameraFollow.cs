using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class CameraFollow : Camera
    {
        public Vector2 Destination { get; set; }
        public GameObject MyTarget { get; set; }
        public float MovementWeight { get; set; }

        public CameraFollow()
        {
            this.MovementWeight = 0.90f;
        }

        public override void Update()
        {
            base.Update();
            FollowMyTarget();
        }

        public override void OnSwitchScene()
        {
            base.OnSwitchScene();
            this.Position = this.Destination;
        }

        private void FollowMyTarget()
        {
            if (this.MyTarget != null)
            {
                this.Destination = this.MyTarget.Transform.Position;
            }

            this.Position = this.Position * (this.MovementWeight) + (1 - this.MovementWeight) * this.Destination;

            var position = Matrix.CreateTranslation(
                -this.Position.X,
                -this.Position.Y,
                0);

            var offset = Matrix.CreateTranslation(
                GraphicsSetting.Instance.ScreenSize.X / 2,
                GraphicsSetting.Instance.ScreenSize.Y / 2,
                0);

            this.transform = position * offset;
        }
    }
}
