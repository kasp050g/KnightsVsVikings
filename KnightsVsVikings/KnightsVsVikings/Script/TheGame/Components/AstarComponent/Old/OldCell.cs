using KnightsVsVikings.Script.TheGame.Enum;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.AstarComponent
{
    public class OldCell : IComparable<OldCell>
    {
        public Vector2 GridPos { get; set; } = new Vector2();
        public ECellType ECellType { get; set; } = new ECellType();
        public OldFGH FGH { get; set; }
        public OldCell Parent { get; set; } = null;
        public bool BlackListed { get; set; } = false;

        public OldCell(ECellType eCellType)
        {
            ECellType = eCellType;
        }

        public OldCell(ECellType eCellType, bool isBlackListed) : this(eCellType)
        {
            BlackListed = isBlackListed;
        }

        public int CompareTo(OldCell other)
        {
            if (FGH.F == other.FGH.F)
                return FGH.H.CompareTo(other.FGH.H);

            return FGH.F.CompareTo(other.FGH.F);
        }
    }
}
