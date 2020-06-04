using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class Transform
    {
        private Vector2 position;
        private Vector2 origin;
        private Vector2 scale = new Vector2(1, 1);
        private Vector2 drawOffSet = new Vector2(0, 0);
        private Vector2 velocity = new Vector2(0, 0);
        private float rotation;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Origin { get { return origin; } set { origin = value; } }
        public Vector2 Scale { get { return scale; } set { scale = value; } }
        public Vector2 DrawOffSet { get { return drawOffSet; } set { drawOffSet = value; } }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        //public Vector2 Velocity { get; set; } = new Vector2();
        public float Rotation { get { return rotation; } set { rotation = value; } }



        public Vector2 ForwardVector
        {
            get
            {
                return new Vector2(
                    Helper.Cos(this.Rotation),
                    Helper.Sin(this.Rotation)
                );
            }
        }

        public Vector2 BackwardVector
        {
            get
            {
                return new Vector2(
                    Helper.Cos(this.Rotation + 180),
                    Helper.Sin(this.Rotation + 180)
                );
            }
        }

        public Vector2 RightVector
        {
            get
            {
                return new Vector2(
                    Helper.Cos(this.Rotation + 90),
                    Helper.Sin(this.Rotation + 90)
                );
            }
        }

        public Vector2 LeftVector
        {
            get
            {
                return new Vector2(
                    Helper.Cos(this.Rotation + 270),
                    Helper.Sin(this.Rotation + 270)
                );
            }
        }



        public Transform()
        {

        }

        //public void LookAt(Vector2 positionToLookAt)
        //{
        //    // TODO: Implement Transform.LookAt (Vector2 positionToLookAt)
        //}

        //public void LookAt(GameObject gameObjectToLookAt)
        //{
        //    // TODO: Implement Transform.LookAt (GameObject gameObjectToLookAt)
        //}
    }
}
