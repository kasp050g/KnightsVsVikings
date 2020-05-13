using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class Camera
    {
        protected Matrix transform;
        private bool isFirstUpdate;

        public Vector2 Position { get; set; }
        public Matrix Transform { get => transform; private set => transform = value; }

        public Camera()
        {
            this.Transform = Matrix.CreateTranslation(0, 0, 0);            
        }

        public virtual void Update()
        {

        }

        public virtual void OnSwitchScene()
        {
            isFirstUpdate = true;
        }
    }
}
