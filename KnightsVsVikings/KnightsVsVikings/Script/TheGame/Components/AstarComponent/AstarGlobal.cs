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
    public class AstarGlobal
    {
        public Cell[,] GlobalAstarGrid { get; set; } = null;
        public Cell[,] BaseMapGrid { get; private set; } = null;
        public GameObject[,] TileGrid { get; private set; } = null;

        public void InitializeGrids(GameObject[,] tileGrid)
        {
            GlobalAstarGrid = new Cell[tileGrid.GetLength(0), tileGrid.GetLength(1)];

            BaseMapGrid = IntMatrixToCellMatrix(tileGrid);
            GlobalAstarGrid = BaseMapGrid;
            TileGrid = tileGrid;
        }

        private Cell[,] IntMatrixToCellMatrix(GameObject[,] array)
        {
            Cell[,] result = new Cell[array.GetLength(0), array.GetLength(1)];

            for (int x = 0; x < array.GetLength(0); x++)
                for (int y = 0; y < array.GetLength(1); y++)
                    switch(array[x, y].GetComponent<CTile>().TileType)
                    {
                        case ETileType.Grass:
                            result[x, y] = new Cell(Enum.ECellType.Default);
                            break;

                        default:
                            result[x, y] = new Cell(Enum.ECellType.Invalid);
                            break;
                    }

            return result;
        }
    }
}
