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
    public class BlendTree
    {
        private string name;
        private Animation up;
        private Animation down;
        private Animation left;
        private Animation rigth;

        public BlendTree(Animation up, Animation down, Animation left, Animation rigth, string name)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.rigth = rigth;
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        public Animation Up { get => up; private set => up = value; }
        public Animation Down { get => down; private set => down = value; }
        public Animation Left { get => left; private set => left = value; }
        public Animation Rigth { get => rigth; private set => rigth = value; }

        public Animation FacingCheck(EFacingDirection facingDirection)
        {
            switch (facingDirection)
            {
                case EFacingDirection.Up:
                    return Up;
                case EFacingDirection.Down:
                    return Down;
                case EFacingDirection.Left:
                    return Left;
                case EFacingDirection.Right:
                    return Rigth;
                default:
                    return Down;
            }
        }
    }
}
