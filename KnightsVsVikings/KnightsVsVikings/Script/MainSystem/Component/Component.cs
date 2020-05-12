using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public abstract class Component
    {
        #region Fields
        #endregion

        #region Methods 
        public bool IsEnabled { get; set; } = true;
        public GameObject GameObject { get; set; }
        #endregion

        #region Constructors 
        #endregion

        #region Methods 
        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Destroy()
        {

        }

        #region Instantiate And Destroy
        /// <summary>
        /// Will instantiate a new gameObject in to the game.
        /// </summary>
        /// <param name="gameObject">The GameObject to be add to game.</param>
        public void Instantiate(GameObject gameObject)
        {
            this.GameObject.MyScene.Instantiate(gameObject);
        }
        /// <summary>
        /// Will destroy this gameobject
        /// </summary>
        /// <param name="gameObject">destroy this gameobject</param>
        public void Destroy(GameObject gameObject)
        {
            this.GameObject.MyScene.Destroy(gameObject);
        }
        #endregion
        #endregion
    }
}
