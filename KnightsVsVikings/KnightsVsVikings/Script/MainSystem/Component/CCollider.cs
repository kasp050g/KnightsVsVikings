using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    // Kasper  Fly
    public class CCollider : Component
    {
        #region Fields
        private GameEvent onCollisionEvent = new GameEvent("Collision");

        private Vector2 size = new Vector2(0, 0);
        private Vector2 offSet = new Vector2(0, 0);

        private Texture2D texture;

        private int outlineThickness = 2;

        private Color outlineColor = Color.LawnGreen;

        private bool useScaleCollider = true;
        #endregion

        #region Properties
        public bool CheckCollisionEvents { get; set; } = true;
        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)GameObject.Transform.Position.X - (int)(GameObject.Transform.Origin.X * GameObject.Transform.Scale.X) + (int)offSet.X,
                    (int)GameObject.Transform.Position.Y - (int)(GameObject.Transform.Origin.Y * GameObject.Transform.Scale.Y) + (int)offSet.Y,
                    (int)(size.X * (useScaleCollider ? GameObject.Transform.Scale.X : 1)),
                    (int)(size.Y * (useScaleCollider ? GameObject.Transform.Scale.Y : 1)));
            }
        }

        public bool UseScaleCollider { get => useScaleCollider; set => useScaleCollider = value; }
        #endregion

        #region Constructors 
        public CCollider()
        {

        }

        public CCollider(Vector2 size, Vector2 offSet)
        {
            this.size = size;
            this.offSet = offSet;
        }
        #endregion

        #region Methods 
        public void OnCollisionEnter(CCollider other)
        {
            if (CheckCollisionEvents)
            {
                if (other != this)
                {
                    if (CollisionBox.Intersects(other.CollisionBox))
                    {
                        onCollisionEvent.Notify(other);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawCollisionBox(spriteBatch);
        }

        private void DrawCollisionBox(SpriteBatch spriteBatch)
        {
            Rectangle collisionBox = CollisionBox;
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width + outlineThickness, outlineThickness);
            Rectangle rigthLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, outlineThickness, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, outlineThickness, collisionBox.Height);
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, outlineThickness);

            spriteBatch.Draw(texture, bottomLine, null, outlineColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rigthLine, null, outlineColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, outlineColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, topLine, null, outlineColor, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public override string ToString()
        {
            return "Collider";
        }

        public override void Awake()
        {
            base.Awake();
            texture = SpriteContainer.Instance.Sprite["Pixel"];

            if (size == new Vector2(0, 0))
            {
                CSpriteRenderer spriteRenderer = GameObject.GetComponent<CSpriteRenderer>();

                this.size = new Vector2(spriteRenderer.Sprite.Width, spriteRenderer.Sprite.Height);
            }

            foreach (var item in GameObject.Components.Values)
            {
                if (item is IGameListner)
                {
                    onCollisionEvent.Attach(item as IGameListner);
                }
            }
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Destroy()
        {
            base.Destroy();
            if (GameObject.MyScene.Colliders.Contains(this))
                GameObject.MyScene.Colliders.Remove(this);
        }
        #endregion
    }
}
