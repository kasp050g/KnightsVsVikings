using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.AstarComponent
{
    public class OldAstarGlobal
    {
        public OldCell[,] GlobalAstarGrid { get; set; }
        public OldCell[,] BaseMapGrid { get; set; }

        public void InitializeGrids(GameObject[,] baseGrid)
        {
            GlobalAstarGrid = new OldCell[baseGrid.GetLength(0), baseGrid.GetLength(1)];

            BaseMapGrid = IntToCellArray(baseGrid);
            GlobalAstarGrid = BaseMapGrid;
        }

        private OldCell[,] IntToCellArray(GameObject[,] array)
        {
            OldCell[,] result = new OldCell[array.GetLength(0), array.GetLength(1)];

            for (int x = 0; x < array.GetLength(0); x++)
                for (int y = 0; y < array.GetLength(1); y++)
                    switch(array[x, y].GetComponent<CTile>().IsBlock)
                    {
                        case true:
                            result[x, y] = new OldCell(Enum.ECellType.Invalid);
                            break;

                        case false:
                            result[x, y] = new OldCell(Enum.ECellType.Default);
                            break;
                    }

            return result;
        }
    }
}
