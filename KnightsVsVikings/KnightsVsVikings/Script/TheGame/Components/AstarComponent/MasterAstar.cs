using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class MasterAstar
    {
        public bool runAstar = true;
        int tileSize = 128 / 2;
        CTile start;
        CTile goal;

        CTile currentTile;

        List<CTile> open = new List<CTile>();
        List<CTile> close = new List<CTile>();

        public List<CTile> tiles = new List<CTile>();
        Stack<CTile> stackTiles = new Stack<CTile>();

        public int TileSize { get => tileSize; set => tileSize = value; }
        public MasterAstar()
        {

        }

        public Stack<CTile> GetAstarWay(CTile _myPosition, CTile _endPosition)
        {
            open.Clear();
            close.Clear();
            stackTiles.Clear();

            start = _myPosition;
            goal = _endPosition;

            AddOpen(start, 0);

            MainLoop();

            return stackTiles;
        }
        public Stack<CTile> GetAstarWay(CTile _myPosition, CTile _endPosition, List<CTile> _tiles)
        {
            tiles.Clear();
            open.Clear();
            close.Clear();
            stackTiles.Clear();

            start = _myPosition;
            goal = _endPosition;
            tiles = _tiles;
            AddOpen(start, 0);

            MainLoop();

            return stackTiles;
        }

        public void SetTileGrid(List<CTile> _tiles)
        {
            tiles = _tiles;
        }
        public void MainLoop()
        {
            while (runAstar == true)
            {
                if (open.Count == 0)
                {
                    break;
                }

                FindLowerCostCell();

                if (currentTile == goal)
                {
                    GoHome();
                    break;
                }

                CellroundTarget(currentTile);
            }
        }

        public void FindLowerCostCell()
        {
            currentTile = open[0];
            foreach (CTile item in open)
            {
                if (currentTile.F > item.F || currentTile.F >= item.F && currentTile.H > item.H)
                {
                    currentTile = item;
                }
            }

            open.Remove(currentTile);
            close.Add(currentTile);
        }

        public void AddOpen(CTile cell, int gCost)
        {

            int y = (int)cell.GameObject.Transform.Position.Y;
            int x = (int)cell.GameObject.Transform.Position.X;

            int distane = 0;

            // Y
            while (true)
            {
                if (y == goal.GameObject.Transform.Position.Y)
                {
                    break;
                }
                else
                {
                    if (goal.GameObject.Transform.Position.Y > y)
                    {
                        y += tileSize;
                    }
                    else
                    {
                        y -= tileSize;
                    }
                    distane += 10;
                }
            }
            // X
            while (true)
            {
                if (x == goal.GameObject.Transform.Position.X)
                {
                    break;
                }
                else
                {
                    if (goal.GameObject.Transform.Position.X > x)
                    {
                        x += tileSize;
                    }
                    else
                    {
                        x -= tileSize;
                    }
                    distane += 10;
                }
            }

            cell.H = distane;
            cell.G = gCost + (currentTile != null ? currentTile.G : 0);
            cell.F = cell.G + cell.H;
            cell.LastTile = currentTile;

            open.Add(cell);
        }


        public void CellroundTarget(CTile target)
        {

            foreach (CTile item in tiles)
            {
                // - - - Y bot - - - 
                // Y+1 X+1
                if (item.GameObject.Transform.Position.X + tileSize == target.GameObject.Transform.Position.X && item.GameObject.Transform.Position.Y + tileSize == target.GameObject.Transform.Position.Y)
                {
                    //BeforOpenAdd(item, 14);
                }
                // Y+1 X 0
                else if (item.GameObject.Transform.Position.X == target.GameObject.Transform.Position.X && item.GameObject.Transform.Position.Y + tileSize == target.GameObject.Transform.Position.Y)
                {
                    BeforOpenAdd(item, 10);
                }
                // Y+tileSize X-tileSize
                else if (item.GameObject.Transform.Position.X - tileSize == target.GameObject.Transform.Position.X && item.GameObject.Transform.Position.Y + tileSize == target.GameObject.Transform.Position.Y)
                {
                    //BeforOpenAdd(item, tileSize4);
                }
                // - - - Y mid - - - 
                // Y+0 X+tileSize
                if (item.GameObject.Transform.Position.X + tileSize == target.GameObject.Transform.Position.X && item.GameObject.Transform.Position.Y == target.GameObject.Transform.Position.Y)
                {
                    BeforOpenAdd(item, 10);
                }
                // Y+0 X-tileSize
                else if (item.GameObject.Transform.Position.X - tileSize == target.GameObject.Transform.Position.X && item.GameObject.Transform.Position.Y == target.GameObject.Transform.Position.Y)
                {
                    BeforOpenAdd(item, 10);
                }
                // - - - Y Top - - - 
                // Y-tileSize X+tileSize
                if (item.GameObject.Transform.Position.X + tileSize == target.GameObject.Transform.Position.X && item.GameObject.Transform.Position.Y - tileSize == target.GameObject.Transform.Position.Y)
                {
                    //BeforOpenAdd(item, tileSize4);
                }
                // Y-tileSize X 0
                else if (item.GameObject.Transform.Position.X == target.GameObject.Transform.Position.X && item.GameObject.Transform.Position.Y - tileSize == target.GameObject.Transform.Position.Y)
                {
                    BeforOpenAdd(item, 10);
                }
                // Y-tileSize X-tileSize
                else if (item.GameObject.Transform.Position.X - tileSize == target.GameObject.Transform.Position.X && item.GameObject.Transform.Position.Y - tileSize == target.GameObject.Transform.Position.Y)
                {
                    //BeforOpenAdd(item, 14);
                }
            }
        }

        public void BeforOpenAdd(CTile cell, int gCost)
        {
            if (!close.Contains(cell) && !open.Contains(cell) && cell.IsBlock == false && cell.IsResourceOccupied == false && cell.IsCanBuildHere == true && cell.IsUnitOccupied == false)
            {
                AddOpen(cell, gCost);
            }
        }

        public void GoHome()
        {
            if (runAstar == true)
            {
                runAstar = false;
                while (true)
                {
                    if (currentTile != null)
                    {
                        stackTiles.Push(currentTile);
                        if (currentTile.LastTile == start)
                        {
                            runAstar = true;
                            break;
                        }
                        currentTile = currentTile.LastTile;
                    }
                    else
                    {
                        runAstar = true;
                        break;
                    }
                }
            }
        }
    }
}
