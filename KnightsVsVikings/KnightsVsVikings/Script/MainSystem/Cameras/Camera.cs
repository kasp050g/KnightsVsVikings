using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    // Kasper  Fly
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

        /// <summary>
        /// Update
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Call on scene switch
        /// </summary>
        public virtual void OnSwitchScene()
        {
            isFirstUpdate = true;
        }
    }
}
