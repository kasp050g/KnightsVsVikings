using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class GUI : Component
    {
        #region Fields
        private bool blockGUI = false;
        private bool isWorldUI = false;
        private bool mouseIsHovering = false;
        private CSpriteRenderer spriteRenderer;
        private float layerDepth = 0;
        #endregion

        #region Properties
        public bool BlockGUI { get => blockGUI; set => blockGUI = value; }
        public bool IsWorldUI { get => isWorldUI; set => isWorldUI = value; }
        public bool MouseIsHovering { get => mouseIsHovering; set => mouseIsHovering = value; }
        public Vector2 SceneSize { get { return GraphicsSetting.Instance.ScreenSize; } }
        public CSpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }
        public float LayerDepth { get => layerDepth; set => layerDepth = value; }

        public virtual Rectangle GUImouseBlockCollision
        {
            get
            {
                // returns a new rectangle based on the position, scale, sprite width and height.
                return new Rectangle(
                    (int)this.GameObject.Transform.Position.X - (int)(this.GameObject.Transform.Origin.X * this.GameObject.Transform.Scale.X),
                    (int)this.GameObject.Transform.Position.Y - (int)(this.GameObject.Transform.Origin.Y * this.GameObject.Transform.Scale.Y),
                    (int)((this.SpriteRenderer.SpriteSheet != null ? this.SpriteRenderer.SpriteSheet.Rectangle.Width : this.SpriteRenderer.Sprite.Width) * this.GameObject.Transform.Scale.X),
                    (int)((this.SpriteRenderer.SpriteSheet != null ? this.SpriteRenderer.SpriteSheet.Rectangle.Height : this.SpriteRenderer.Sprite.Height) * this.GameObject.Transform.Scale.Y)
                    );
            }
        }

        #endregion

        #region Constructors
        public GUI()
        {

        }
        #endregion

        #region Methods 
        public override void Awake()
        {
            base.Awake();
        }
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Destroy()
        {
            base.Destroy();
            mouseIsHovering = false;
            if (GameObject.MyScene.UIColliders.Contains(this))
                GameObject.MyScene.UIColliders.Remove(this);
        }

        public bool OnCollisionEnter(Rectangle other)
        {
            if (BlockGUI)
            {
                mouseIsHovering = false;
                if (GUImouseBlockCollision.Intersects(other))
                {
                    GameObject.MyScene.IsMouseOverUI = true;
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
