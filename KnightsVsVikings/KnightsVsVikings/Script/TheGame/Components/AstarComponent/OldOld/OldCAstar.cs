using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.Script.TheGame.Enum;
using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.ExceptionServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.AstarComponent
{
    public class OldCAstar : Component
    {
        private const byte DEFAULT_COST = 10;

        private CUnit ownerUnit = null;
        private Vector2 ownerGridPos = new Vector2();

        private Cell currentCell = null,
                     targetCell = null,
                     pathCell = null;

        private bool targetReached = false,
                     pathFound = false;

        private List<Cell> openList = new List<Cell>(),
                           closedList = new List<Cell>(),
                           pathList = new List<Cell>(),
                           storedPathList = new List<Cell>();

        private Cell[,] astarGrid = null;// = Singletons.AstarGlobalSingleton.GlobalAstarGrid.ToAstarArray();
        private Cell[,] globalAstarGrid = null;

        public OldCAstar(CUnit ownerUnit)
        {
            this.ownerUnit = ownerUnit;
        }

        public void InitiateAstar()
        {
            astarGrid = Singletons.AstarGlobalSingleton.GlobalAstarGrid.ToAstarMatrix();
            globalAstarGrid = Singletons.AstarGlobalSingleton.GlobalAstarGrid;
        }

        public override void Update()
        {
            if (astarGrid != null)
            {
                if (ownerUnit.Target != null)
                {
                    Vector2 posToGrid = ownerUnit.Target.Transform.Position;

                    targetCell = astarGrid[(int)(posToGrid.Y / Singletons.LevelInformationSingleton.TileSize), (int)(posToGrid.X / Singletons.LevelInformationSingleton.TileSize)];

                    ownerGridPos = new Vector2(ownerUnit.GameObject.Transform.Position.Y / Singletons.LevelInformationSingleton.TileSize,
                                                ownerUnit.GameObject.Transform.Position.X / Singletons.LevelInformationSingleton.TileSize);
                    //   if (targetCell == null)
                    //       targetCell = CheckForTarget();

                    if (!targetReached)
                        CheckOpenClosedList();

                    else if (!pathFound)
                        GetPath();

                    else if (pathFound)
                        FollowPath();
                }
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
                currentCell = astarGrid[(int)(ownerUnit.GameObject.Transform.Position.Y / Singletons.LevelInformationSingleton.TileSize),
                                        (int)(ownerUnit.GameObject.Transform.Position.X / Singletons.LevelInformationSingleton.TileSize)];
                SetupNeighbourCells();
            }

            openList.Sort();

            if (openList.Count != 0)
            {
                currentCell = openList.First();

                if (currentCell.GridPos == targetCell.GridPos)
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
            try { ModifyNeighbourCells(astarGrid[(int)currentCell.GridPos.X + 1, (int)currentCell.GridPos.Y], currentCell); } catch { }
            try { ModifyNeighbourCells(astarGrid[(int)currentCell.GridPos.X - 1, (int)currentCell.GridPos.Y], currentCell); } catch { }

            try { ModifyNeighbourCells(astarGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y + 1], currentCell); } catch { }
            try { ModifyNeighbourCells(astarGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y - 1], currentCell); } catch { }
        }

        private void ModifyNeighbourCells(Cell cell, Cell parent)
        {
            if(cell.ECellType == ECellType.Default && globalAstarGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y].ECellType == ECellType.Default)
            {
                if (!closedList.Contains(cell) && !openList.Contains(cell))
                    openList.Add(cell);

                cell.Parent = parent;
                cell.FGH = new FGH(CalculateG(cell), CalculateH(cell.GridPos, targetCell.GridPos));
                cell.ECellType = ECellType.Open;
            }
            else if (openList.Contains(cell) && cell.Parent != null)//(cell.ECellType == ECellType.Open && openList.Contains(cell))//(openList.Contains(cell) && cell.Parent != null)
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

                globalAstarGrid[(int)targetCell.GridPos.X, (int)targetCell.GridPos.Y].ECellType = ECellType.Path;

                pathFound = true;

                //pathList.Sort();

                storedPathList.AddRange(pathList);

                currentCell = null;
            }
            else
            {
                globalAstarGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].ECellType = ECellType.Path;

                pathList.Add(currentCell);
                closedList.Remove(currentCell);

                if (pathList.Count == 1)
                    currentCell.ECellType = ECellType.Path;

                currentCell = currentCell.Parent;
                currentCell.ECellType = ECellType.Path;

                //Console.WriteLine($"CurrentCell: X: {currentCell.GridPos.X}, Y: {currentCell.GridPos.Y}");
                //Console.WriteLine($"Owner Pos: X: {ownerGridPos.X}, Y: {ownerGridPos.Y}");
            }
        }

        private void FollowPath()
        {
            if (pathCell == null)
            {
                ownerUnit.IsMoving = true;
                pathCell = pathList.First();
            }

            if (ownerGridPos.ApproximatelyEqual(targetCell.GridPos, 0.1f))
            {
                ownerUnit.GameObject.Transform.Position = new Vector2(pathCell.GridPos.Y * Singletons.LevelInformationSingleton.TileSize,
                                                                      pathCell.GridPos.X * Singletons.LevelInformationSingleton.TileSize);

                ResetAstar();
            }
            else if (ownerGridPos.ApproximatelyEqual(pathCell.GridPos, 0.1f))
            {
                pathList.Remove(pathCell);
                pathCell.FGH = new FGH();
                pathCell.ECellType = ECellType.Default;
                ownerUnit.GameObject.Transform.Velocity = Vector2.Zero;

                //if (pathCell.GridPos == pathList.Last().GridPos)
                //{
                //    ownerUnit.GameObject.Transform.Position = new Vector2(pathCell.GridPos.Y * Singletons.LevelInformationSingleton.TileSize,
                //                                                          pathCell.GridPos.X * Singletons.LevelInformationSingleton.TileSize);
                //    
                //    ResetAstar();
                //}

                pathCell = null;
            }
            else
            {
                ownerUnit.MoveToLocation(new Vector2(pathCell.GridPos.Y * Singletons.LevelInformationSingleton.TileSize,
                                                     pathCell.GridPos.X * Singletons.LevelInformationSingleton.TileSize));
            }
        }

        public void ResetAstar()
        {
            currentCell = null;
            targetCell = null;
            pathCell = null;

            targetReached = false;
            pathFound = false;

            openList.Clear();
            closedList.Clear();
            pathList.Clear();

            astarGrid = Singletons.AstarGlobalSingleton.BaseMapGrid.ToAstarMatrix();

            ownerUnit.Target = null;
            ownerUnit.IsMoving = false;
            ownerUnit.GameObject.Transform.Velocity = Vector2.Zero;

            if (storedPathList.Count != 0)
            {
                foreach (Cell cell in storedPathList)
                    globalAstarGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y].ECellType = ECellType.Default;

                storedPathList.Clear();
            }
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
