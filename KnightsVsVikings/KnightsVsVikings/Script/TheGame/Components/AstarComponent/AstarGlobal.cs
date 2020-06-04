using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.AstarComponent
{
    public class AstarGlobal
    {
        public Cell[,] GlobalAstarGrid { get; set; }
        public Cell[,] BaseMapGrid { get; set; }

        public void InitializeGrids(int[,] baseGrid)
        {
            GlobalAstarGrid = new Cell[baseGrid.GetLength(0), baseGrid.GetLength(1)];

            BaseMapGrid = IntToCellArray(baseGrid);
            GlobalAstarGrid = BaseMapGrid;
        }

        private Cell[,] IntToCellArray(int[,] array)
        {
            Cell[,] result = new Cell[array.GetLength(0), array.GetLength(1)];

            for (int x = 0; x < array.GetLength(0); x++)
                for (int y = 0; x < array.GetLength(1); y++)
                    switch(array[x, y])
                    {
                        case 0:
                            result[x, y] = new Cell(Enum.ECellType.Default);
                            break;

                        case 1:
                            result[x, y] = new Cell(Enum.ECellType.Invalid);
                            break;
                    }

            return result;
        }
    }
}
