using KnightsVsVikings.Script.MainSystem.Enum;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.MainSystem.In_Works_Not_Done_Animations
{
    public class BlendTree_01
    {
        private string name;
        private Animation_01 up;
        private Animation_01 down;
        private Animation_01 left;
        private Animation_01 rigth;
        //private EFacingDirection facingDirection;

        public BlendTree_01(Animation_01 up, Animation_01 down, Animation_01 left, Animation_01 rigth, string name)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.rigth = rigth;
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        public Animation_01 Up { get => up; private set => up = value; }
        public Animation_01 Down { get => down; private set => down = value; }
        public Animation_01 Left { get => left; private set => left = value; }
        public Animation_01 Rigth { get => rigth; private set => rigth = value; }
        //public EFacingDirection FacingDirection { get => facingDirection; set => facingDirection = value; }

        public Animation_01 Play(Vector2 vel, SpriteRenderer spriteRenderer, ref EFacingDirection facingDirection)
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

        public Animation_01 FacingCheck(EFacingDirection facingDirection)
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
