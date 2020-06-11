using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.FactoryPattern
{
    interface IFactoryPlayerCreated
    {
        GameObject Create(string type, EFaction faction, ETeam team);
    }
}
