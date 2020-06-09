using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class KasperMasterAstar
    {
        int tileSize = Singletons.LevelInformationSingleton.TileSize;
        private CTile start;

        private CTile goal;

        private CTile currentTile;

        private List<CTile> openList = new List<CTile>();
        private List<CTile> closedList = new List<CTile>();
        private Stack<CTile> stackTiles = new Stack<CTile>();

        public List<CTile> tiles = new List<CTile>();
        public bool runAstar = true;
        public int TileSize { get => tileSize; }

        public KasperMasterAstar()
        {

        }

        public Stack<CTile> GetAstarWay(CTile currentPosition, CTile targetPosition)
        {
            openList.Clear();
            closedList.Clear();
            stackTiles.Clear();

            start = currentPosition;
            goal = targetPosition;

            AddOpen(start, 0);

            MainLoop();

            return stackTiles;
        }
        public Stack<CTile> GetAstarWay(CTile currentPosition, CTile targetPosition, List<CTile> tilesCurrent)
        {
            tiles.Clear();
            openList.Clear();
            closedList.Clear();
            stackTiles.Clear();

            start = currentPosition;
            goal = targetPosition;
            tiles = tilesCurrent;
            AddOpen(start, 0);

            MainLoop();

            return stackTiles;
        }

        public void SetTileGrid(List<CTile> tilesCurrent)
        {
            tiles = tilesCurrent;
        }
        public void MainLoop()
        {
            while (runAstar == true)
            {
                if (openList.Count == 0)
                    break;

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
            currentTile = openList[0];
            foreach (CTile item in openList)
            {
                if (currentTile.F > item.F || currentTile.F >= item.F && currentTile.H > item.H)
                    currentTile = item;
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);
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
                    break;

                else
                {
                    if (goal.GameObject.Transform.Position.Y > y)
                        y += tileSize;

                    else
                        y -= tileSize;

                    distane += 10;
                }
            }
            // X
            while (true)
            {
                if (x == goal.GameObject.Transform.Position.X)
                    break;

                else
                {
                    if (goal.GameObject.Transform.Position.X > x)
                        x += tileSize;

                    else
                        x -= tileSize;

                    distane += 10;
                }
            }

            cell.H = distane;
            cell.G = gCost + (currentTile != null ? currentTile.G : 0);
            cell.F = cell.G + cell.H;
            cell.LastTile = currentTile;

            openList.Add(cell);
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
            if (!closedList.Contains(cell) && !openList.Contains(cell) && cell.IsBlock == false && cell.IsResourceOccupied == false && cell.IsCanBuildHere == true && cell.IsUnitOccupied == false)
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
