using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class Passive
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Texture2D Icon { get; set; }
        public bool IsLock { get; set; }
        
        public Stats FStats { get; set; }
        public Stats PStats { get; set; }
    }
}
