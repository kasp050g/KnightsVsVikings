using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.Script.TheGame.Enum;
using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.ExceptionServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.AstarComponent
{
    class CAstar : Component
    {
        private const byte DEFAULT_COST = 10;

        private CUnit ownerUnit = null;
        private Vector2 ownerGridPos = new Vector2();

        private Cell currentCell = null,
                     targetCell = null;

        private bool targetReached = false,
                     pathFound = false;

        private List<Cell> openList = new List<Cell>(),
                           closedList = new List<Cell>(),
                           pathList = new List<Cell>();

        private Cell[,] AstarGrid = Singletons.AstarGlobalSingleton.GlobalAstarGrid.ToAstarArray();

        public CAstar(CUnit ownerUnit)
        {
            this.ownerUnit = ownerUnit;
        }

        public override void Update()
        {
            if (ownerUnit.Target != null)
            {
                Vector2 posToGrid = ownerUnit.Target.Transform.Position;

                targetCell = AstarGrid[(int)(posToGrid.X / Singletons.LevelInformationSingleton.TILE_SIZE), (int)(posToGrid.Y / Singletons.LevelInformationSingleton.TILE_SIZE)];

                ownerGridPos = new Vector2(ownerUnit.GameObject.Transform.Position.X / Singletons.LevelInformationSingleton.TILE_SIZE,
                                            ownerUnit.GameObject.Transform.Position.Y / Singletons.LevelInformationSingleton.TILE_SIZE);
             //   if (targetCell == null)
             //       targetCell = CheckForTarget();

                if (!targetReached)
                    CheckOpenClosedList();

                else if (!pathFound)
                    GetPath();

                else if (pathFound)
                    FollowPath();
            }
            //if (targetCell == null)
            //    targetCell = CheckForTarget();
            //
            //if (!targetReached && targetCell != null)
            //    CheckOpenClosedList();
            //
            //if (targetReached && !pathFound && targetCell != null)
            //    GetPath();
            //
            //if (pathFound && targetCell != null)
            //    FollowPath();
        }

       //private Cell CheckForTarget()
       //{
       //    if (openList.Count > 0)
       //        openList.Clear();
       //
       //    if (closedList.Count > 0)
       //        closedList.Clear();
       //
       //    try
       //    {
       //        return Singletons.GameWorldSingleton.Components.Where(unit => unit.)
       //    }
       //
       //    catch
       //    { return null; }
       //}

        private void CheckOpenClosedList()
        {
            if (openList.Count == 0 && currentCell == null)
            {
                currentCell = AstarGrid[(int)(ownerUnit.GameObject.Transform.Position.Y / 128), (int)(ownerUnit.GameObject.Transform.Position.X / 128)];
                SetupNeighbourCells();
            }

            openList.Sort();

            if (openList.Count != 0)
            {
                currentCell = openList.First();

                if (currentCell == targetCell)
                    targetReached = true;

                closedList.Add(currentCell);
                currentCell.ECellType = ECellType.Closed;
                SetupNeighbourCells();
                openList.Remove(currentCell);
            }
            else
            {
                targetCell.BlackListed = true;
                ResetAstar();
            }
        }

        private void SetupNeighbourCells()
        {
            try { ModifyNeighbourCells(AstarGrid[(int)currentCell.GridPos.X + 1, (int)currentCell.GridPos.Y], currentCell); } catch { }
            try { ModifyNeighbourCells(AstarGrid[(int)currentCell.GridPos.X - 1, (int)currentCell.GridPos.Y], currentCell); } catch { }

            try { ModifyNeighbourCells(AstarGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y + 1], currentCell); } catch { }
            try { ModifyNeighbourCells(AstarGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y - 1], currentCell); } catch { }
        }

        private void ModifyNeighbourCells(Cell cell, Cell parent)
        {
            if(cell.ECellType == ECellType.Default)
            {
                if (!closedList.Contains(cell) && !openList.Contains(cell))
                    openList.Add(cell);

                cell.Parent = parent;
                cell.FGH = new FGH(CalculateG(cell), CalculateH(cell.GridPos, targetCell.GridPos));
                cell.ECellType = ECellType.Open;
            }
            else if (openList.Contains(cell) && cell.Parent != null)
            {
                Cell storeParent = cell.Parent;
                cell.Parent = parent;

                FGH comparePrice = new FGH(CalculateG(cell), CalculateH(cell.GridPos, targetCell.GridPos));

                if (comparePrice.F < cell.FGH.F)
                    cell.FGH = comparePrice;
                else
                    cell.Parent = storeParent;
            }
        }

        private void GetPath()
        {
            if (currentCell.GridPos == ownerGridPos)
            {
                pathList.Remove(currentCell);
                currentCell.ECellType = ECellType.Default;

                pathFound = true;

                pathList.OrderBy(cell => Vector2.Distance(ownerGridPos, cell.GridPos));
            }
            else
            {
                Singletons.AstarGlobalSingleton
                          .GlobalAstarGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y]
                          .ECellType = ECellType.Path;

                pathList.Add(currentCell);
                closedList.Remove(currentCell);

                if (pathList.Count == 0)
                    currentCell.ECellType = ECellType.Path;

                currentCell = currentCell.Parent;
                currentCell.ECellType = ECellType.Path;
            }
        }

        private void FollowPath()
        {
            if (currentCell == null)
                if (pathList.Count != 0)
                    currentCell = pathList.First();

            if (ownerGridPos.ApproximatelyEqual(currentCell.GridPos, 5f))
            {
                pathList.Remove(currentCell);
                currentCell.FGH = new FGH();
                currentCell.ECellType = ECellType.Default;
                ownerUnit.GameObject.Transform.Velocity = Vector2.Zero;

                if (currentCell.GridPos == pathList.Last().GridPos)
                {
                    ownerUnit.GameObject.Transform.Position = new Vector2(currentCell.GridPos.X * Singletons.LevelInformationSingleton.TILE_SIZE,
                                                                          currentCell.GridPos.Y * Singletons.LevelInformationSingleton.TILE_SIZE);
                    ResetAstar();
                }

                currentCell = null;
            }
            else
            {
                ownerUnit.MoveToLocation(new Vector2(currentCell.GridPos.X * Singletons.LevelInformationSingleton.TILE_SIZE,
                                                     currentCell.GridPos.Y * Singletons.LevelInformationSingleton.TILE_SIZE));
            }
        }

        private void ResetAstar()
        {
            currentCell = null;
            targetCell = null;

            targetReached = false;
            pathFound = false;
        }

        private uint CalculateG(Cell cell)
        {
            if (cell.Parent != null)
                return cell.Parent.FGH.G + DEFAULT_COST;

            return DEFAULT_COST;
        }

        private uint CalculateH(Vector2 pos, Vector2 target)
        {
            return (uint)Math.Abs(Vector2.Distance(pos, target) * 10);
        }
    }
}
