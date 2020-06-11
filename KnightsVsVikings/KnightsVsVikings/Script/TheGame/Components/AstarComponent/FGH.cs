using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.AstarComponent
{
    // Lucas
    public struct FGH
    {
        public uint F { get => G + H; }
        public uint G { get; set; }
        public uint H { get; set; }

        public FGH(uint g, uint h)
        {
            G = g;
            H = h;
        }
    }
}
