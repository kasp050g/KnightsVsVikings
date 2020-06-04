using KnightsVsVikings.Script.TheGame.Enum;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.AstarComponent
{
    public class Cell : IComparable<Cell>
    {
        public Vector2 GridPos { get; set; } = new Vector2();
        public ECellType ECellType { get; set; } = new ECellType();
        public FGH FGH { get; set; }
        public Cell Parent { get; set; } = null;
        public bool BlackListed { get; set; } = false;

        public Cell(ECellType eCellType)
        {
            ECellType = eCellType;
        }

        public Cell(ECellType eCellType, bool isBlackListed) : this(eCellType)
        {
            BlackListed = isBlackListed;
        }

        public int CompareTo(Cell other)
        {
            if (FGH.F == other.FGH.F)
                return FGH.H.CompareTo(other.FGH.H);

            return FGH.F.CompareTo(other.FGH.F);
        }
    }
}
