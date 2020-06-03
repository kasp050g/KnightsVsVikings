using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class GUIImage : GUI
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public GUIImage()
        {

        }

        public GUIImage(CSpriteRenderer spriteRenderer, bool isWorldUI, bool blockUI,Color? color = null,EOriginPosition originPositionEnum = EOriginPosition.TopLeft,float layerDepth = 0)
        {
            this.SpriteRenderer = spriteRenderer;
            this.IsWorldUI = isWorldUI;
            this.BlockGUI = blockUI;
            this.SpriteRenderer.Color = color ?? Color.White;
            this.SpriteRenderer.OriginPositionEnum = originPositionEnum;
            this.SpriteRenderer.LayerDepth = layerDepth;
        }
        #endregion

        #region Methods 
        public override void Awake()
        {
            if(this.SpriteRenderer == null)
            {
                this.SpriteRenderer = GameObject.GetComponent<CSpriteRenderer>();
            }
            LayerDepth = SpriteRenderer.LayerDepth;
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
        }
        #endregion
    }
}