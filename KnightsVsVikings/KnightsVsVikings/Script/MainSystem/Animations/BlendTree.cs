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
    // Kasper  Fly
    public class BlendTree
    {
        private string name;
        private Animation up;
        private Animation down;
        private Animation left;
        private Animation rigth;

        /// <summary>
        /// BlendTree need 4 animations to work up,down,left,rigth and a name.
        /// </summary>
        /// <param name="up">Up</param>
        /// <param name="down">Down</param>
        /// <param name="left">Left</param>
        /// <param name="rigth">Rigth</param>
        /// <param name="name">Name of this BlendTree</param>
        public BlendTree(Animation up, Animation down, Animation left, Animation rigth, string name)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.rigth = rigth;
            this.name = name;
        }

        /// <summary>
        /// Name of this BlendTree
        /// </summary>
        public string Name { get => name; set => name = value; }
        public Animation Up { get => up; private set => up = value; }
        public Animation Down { get => down; private set => down = value; }
        public Animation Left { get => left; private set => left = value; }
        public Animation Rigth { get => rigth; private set => rigth = value; }

        /// <summary>
        /// Return the aniation of the direction your facing.
        /// </summary>
        /// <param name="facingDirection">The Direction your facing</param>
        /// <returns></returns>
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
