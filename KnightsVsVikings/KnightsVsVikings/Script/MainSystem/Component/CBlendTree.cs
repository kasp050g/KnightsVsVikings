using KnightsVsVikings;
using KnightsVsVikings.Script.MainSystem.Enum;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class CBlendTree
    {
        private string name;
        private CAnimation up;
        private CAnimation down;
        private CAnimation left;
        private CAnimation rigth;
        //private EFacingDirection facingDirection;

        public CBlendTree(CAnimation up, CAnimation down, CAnimation left, CAnimation rigth, string name)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.rigth = rigth;
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        public CAnimation Up { get => up; private set => up = value; }
        public CAnimation Down { get => down; private set => down = value; }
        public CAnimation Left { get => left; private set => left = value; }
        public CAnimation Rigth { get => rigth; private set => rigth = value; }
        //public EFacingDirection FacingDirection { get => facingDirection; set => facingDirection = value; }

        public CAnimation Play(Vector2 vel, CSpriteRenderer spriteRenderer, ref EFacingDirection facingDirection)
        {
            float y = vel.Y;
            float x = vel.X;

            if (0 < Math.Abs(y))
            {
                if (0 < y)
                {
                    spriteRenderer.SpriteEffects = SpriteEffects.None;
                    facingDirection = EFacingDirection.Up;
                    return Up;
                }
                else
                {
                    spriteRenderer.SpriteEffects = SpriteEffects.None;
                    facingDirection = EFacingDirection.Down;
                    return Down;
                }
            }
            else
            {
                if (0 > x)
                {
                    spriteRenderer.SpriteEffects = SpriteEffects.FlipHorizontally;
                    facingDirection = EFacingDirection.Left;
                    return Left;
                }
                else
                {
                    spriteRenderer.SpriteEffects = SpriteEffects.None;
                    facingDirection = EFacingDirection.Rigth;
                    return Rigth;
                }
            }
        }

        public CAnimation FacingCheck(EFacingDirection facingDirection)
        {
            switch (facingDirection)
            {
                case EFacingDirection.Up:
                    return Up;
                case EFacingDirection.Down:
                    return Down;
                case EFacingDirection.Left:
                    return Left;
                case EFacingDirection.Rigth:
                    return Rigth;
                default:
                    return Down;
            }
        }
    }
}
