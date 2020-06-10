using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.Script.TheGame.Enum;
using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.AstarComponent
{
    public class CAstar : Component
    {
        private const byte DEFAULT_COST = 10;

        private byte tileSize = Singletons.LevelInformationSingleton.TileSize;

        private bool pathBlocked = false,
                     exit = false;

        private Thread astarThread;
        //private readonly PerformanceCounter currentCPUUsage = Singletons.GlobalPropertiesSingleton.CPUUsage;//new PerformanceCounter("Processor", "% Processor Time", "_Total");

        private Cell startCell = null,
                     currentCell = null,
                     nextCell = null,
                     pathCell = null,
                     targetCell = null;

        private CUnit owner = null;

        private Cell[,] baseGrid = null;
        private GameObject[,] tileGrid = null;

        private List<Cell> openList = new List<Cell>(),
                           closedList = new List<Cell>(),
                           pathList = new List<Cell>(),
                           pathCloneList = new List<Cell>();

        // private Stack<Cell> pathList = new Stack<Cell>();

        public CAstar(CUnit owner)
        {
            this.owner = owner;

            astarThread = new Thread(AstarThread);
            astarThread.IsBackground = true;
            astarThread.Start();
        }

        private void AstarThread()//public override void Update()
        {
            while (GameObject.IsActive)
            {
                //if (currentCPUUsage.NextValue() == 100f)
                //    Thread.Sleep(20);
                //else
                Thread.Sleep(8);

                exit = false;

                //Console.WriteLine(currentCPUUsage.NextValue());

                if (Singletons.AstarGlobalSingleton.BaseMapGrid != null && baseGrid == null)
                {
                    baseGrid = Singletons.AstarGlobalSingleton.BaseMapGrid.ToAstarMatrix();
                    tileGrid = Singletons.AstarGlobalSingleton.TileGrid;
                    //baseGrid.Print2DArray();
                    //Console.ReadKey();
                }

                if (owner.Target != null)// && pathCell == null)
                {
                    if (pathList.Count == 0)
                        GetPath();
                    else
                        FollowPath();
                }
            }
        }

        public void ResetAstar()
        {
            //if (currentCell != null)
            //{
            //    owner.GameObject.Transform.Position = new Vector2(currentCell.GridPos.X * tileSize, currentCell.GridPos.Y * tileSize);
            //    tileGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = true;
            //}
            //Singletons.AstarGlobalSingleton.GlobalAstarGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].ECellType = ECellType.Default;

            currentCell = null;
            targetCell = null;
            startCell = null;
            pathCell = null;

            pathBlocked = false;

            pathList.Clear();
            pathCloneList.Clear();
            openList.Clear();
            closedList.Clear();

            //if (pathCloneList.Count > 0)
            //{
            //    foreach (Cell cell in pathCloneList)
            //        Singletons.AstarGlobalSingleton.GlobalAstarGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y] = Singletons.AstarGlobalSingleton.BaseMapGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y];
            //
            //    pathCloneList.Clear();
            //}

            baseGrid = Singletons.AstarGlobalSingleton.BaseMapGrid.ToAstarMatrix();

            //baseGrid.Print2DArray();

            //runAstar = false;
            //cMove.Velocity = new Vector2(0, 0);
            //CurrentTile.IsUnitOccupied = true;
            owner.Target = null;
            owner.IsMoving = false;

            //if (pathCell != null)
            //{
            //    GameObject.Transform.Position = new Vector2(pathCell.GridPos.X * tileSize, pathCell.GridPos.Y * tileSize);
            //    tileGrid[(int)pathCell.GridPos.X, (int)pathCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = true;
            //}


            GameObject.GetComponent<CMove>().Velocity = Vector2.Zero;

            exit = true;
            //pathCell = null;
        }

        private void GetPath()
        {
                startCell = baseGrid[(int)GameObject.Transform.Position.X / tileSize,
                                       (int)GameObject.Transform.Position.Y / tileSize];

                targetCell = baseGrid[(int)owner.Target.Transform.Position.X / tileSize,
                                      (int)owner.Target.Transform.Position.Y / tileSize];

            if (startCell != targetCell)
            {
                if (targetCell.BlackListed)
                    ResetAstar();

                if (startCell != null)
                    tileGrid[(int)startCell.GridPos.X, (int)startCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = false;
                else
                    exit = true;
                //tileGrid[(int)targetCell.GridPos.X, (int)targetCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = false;

                ModifyNeighbourCells(startCell, null);

                if (targetCell != null)
                    FindPath();
            }
        }

        private void FollowPath()
        {
            Vector2 ownerGridPos = new Vector2(owner.GameObject.Transform.Position.X / tileSize,
                                               owner.GameObject.Transform.Position.Y / tileSize);

            //if (pathCell == targetCell)
            //    ResetAstar();

            if (pathCell == null)
            {
                owner.IsMoving = true;
                pathCell = pathList.First();
                //pathList.Remove(pathCell);
            }
            
            if (ownerGridPos.ApproximatelyEqual(pathCell.GridPos, 0.1f))
            {
                if (tileGrid[(int)targetCell.GridPos.X, (int)targetCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied &&
                    !tileGrid[(int)targetCell.GridPos.X, (int)targetCell.GridPos.Y].GetComponent<CTile>().IsResourceOccupied)
                {
                    if (!(owner.UnitType == EUnitType.Worker) && tileGrid[(int)targetCell.GridPos.X, (int)targetCell.GridPos.Y].GetComponent<CTile>().IsCanBuildHere)
                    {
                        GameObject storeTarget = ReadjustTarget((int)targetCell.GridPos.X, (int)targetCell.GridPos.Y);
                        ResetAstar();
                        owner.Target = storeTarget;
                    }
                    //if (storeTarget != null)// && pathCloneList.Count > 8)
                    //    owner.Target = storeTarget;
                    //else
                    //    owner.Target = null;
                }
                else if (pathCell.GridPos == targetCell.GridPos)
                {
                    GameObject.Transform.Position = new Vector2(pathCell.GridPos.X * tileSize, pathCell.GridPos.Y * tileSize);
                    tileGrid[(int)pathCell.GridPos.X, (int)pathCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = true;

                    ResetAstar();
                }
                else
                {
                    pathList.Remove(pathCell);
                    pathCell.FGH = new FGH();
                    pathCell.ECellType = ECellType.Default;
                    GameObject.GetComponent<CMove>().Velocity = Vector2.Zero;

                    //if (pathCell.GridPos == pathList.Last().GridPos)
                    //{
                    //    ownerUnit.GameObject.Transform.Position = new Vector2(pathCell.GridPos.Y * Singletons.LevelInformationSingleton.TileSize,
                    //                                                          pathCell.GridPos.X * Singletons.LevelInformationSingleton.TileSize);
                    //    
                    //    ResetAstar();
                    //}

                    //if (pathCell.GridPos == targetCell.GridPos)
                    //    ResetAstar();
                    //else
                    pathCell = null;
                }
                }
                else
                {
                    owner.MoveToLocation(new Vector2(pathCell.GridPos.X * tileSize, pathCell.GridPos.Y * tileSize));
                }
        }

        //private void FollowPath()
        //{
        //    //while (pathStack.Count != 0)
        //    //{
        //    Vector2 ownerGridPos = new Vector2(owner.GameObject.Transform.Position.X / tileSize,
        //                                       owner.GameObject.Transform.Position.Y / tileSize);
        //
        //    //if (ownerGridPos.ApproximatelyEqual(targetCell.GridPos, 0.05f))
        //    //    ResetAstar();
        //    if (ownerGridPos.ApproximatelyEqual(currentCell.GridPos, 0.05f))
        //    {
        //        owner.IsMoving = true;
        //
        //        CheckNextCell();
        //
        //        //tileGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = true;
        //
        //        if (pathList.Count > 0)
        //        {
        //            currentCell = pathList.First();//pathList.Pop();
        //            pathList.Remove(currentCell);
        //        }
        //
        //        if (currentCell != null)
        //        {
        //            Singletons.AstarGlobalSingleton.GlobalAstarGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].ECellType = ECellType.Default;
        //            tileGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = false;
        //        }
        //
        //        //if (currentCell == targetCell)
        //        //{
        //        //    if (tileGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied)
        //        //    {
        //        //        pathList.Add(pathCloneList.ElementAt(pathCloneList.IndexOf(currentCell) - 1));
        //        //        targetCell = pathList.Last();
        //        //    }
        //        //    else
        //        //    {
        //        //        tileGrid[(int)targetCell.GridPos.X, (int)targetCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = true;
        //        //        ResetAstar();
        //        //    }
        //        //}
        //        if (currentCell == targetCell || tileGrid[(int)targetCell.GridPos.X, (int)targetCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied)
        //        {
        //                ResetAstar();
        //        }
        //
        //        //if (pathBlocked)
        //        //{
        //        //    ResetAstar();
        //        //    owner.Target = tileGrid[(int)nextCell.GridPos.X, (int)nextCell.GridPos.Y];
        //        //    nextCell = null;
        //        //}
        //    }
        //    else
        //        owner.MoveToLocation(new Vector2(currentCell.GridPos.X * tileSize,
        //                                         currentCell.GridPos.Y * tileSize));
        //    //}
        //}

        private void FindPath()
        {
            while (!exit)//while (true)
            {
                //if (currentCPUUsage.NextValue() > 5)
                //    Thread.Sleep(50);
                //else
                Thread.Sleep(5);
                //Task.Delay(1);
                if (openList.Count == 0)
                {
                    if (targetCell != null)
                    {
                        targetCell.BlackListed = true;
                        break;
                    }
                    else
                        break;
                }

                GetNextCell();

                if (currentCell != null)
                    if (currentCell.GridPos == targetCell.GridPos)
                        break;

                SetNeighbourCells();
            }

            if (startCell != null)
            {
                tileGrid[(int)startCell.GridPos.X, (int)startCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = false;

                ReturnPath();

                pathList.Reverse();
                pathCloneList = pathList.ToList();
            }

            //owner.IsMoving = true;
            //while (currentCell != targetCell && openList.Count == 0)
            //    GetNextCell();
            //
            //
            //pathCloneStack = pathStack.ToArray();
            //
            //ReturnPath();
        }

        private void GetNextCell()
        {
            //currentCell = openList.First();
            if (openList.Count != 0)
            {
                openList.Sort();
                currentCell = openList.First();

                openList.Remove(currentCell);
                closedList.Add(currentCell);
                currentCell.ECellType = ECellType.Closed;

                SetNeighbourCells();
            }
        }

        private void SetNeighbourCells()
        {
            // X
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X + 1, (int)currentCell.GridPos.Y], currentCell); } catch { }
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X - 1, (int)currentCell.GridPos.Y], currentCell); } catch { }

            // Y
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y + 1], currentCell); } catch { }
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y - 1], currentCell); } catch { }
        }

        private void ModifyNeighbourCells(Cell cell, Cell parent)
        {
            //Thread.Sleep(2);
            if (cell == null)
            {
                exit = true;
            }
            else
            {
                if (baseGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y].ECellType == ECellType.Default && !tileGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied)
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
        }

        private void ReturnPath()
        {
            while (!exit)//(true)
            {
                //if (currentCPUUsage.NextValue() == 100f)
                //    Thread.Sleep(50);
                //else
                Thread.Sleep(5);
                //Thread.Sleep(6);
                //MyEventWaitHandler
                if (currentCell != null)
                {
                    pathList.Add(currentCell);//pathList.Push(currentCell);
                    if (currentCell.Parent == startCell)
                        break;

                    currentCell = currentCell.Parent;
                    currentCell.ECellType = ECellType.Path;
                }
                else
                    break;
            }
        }

        private void CheckNextCell()
        {
            //if (tileGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied)
            //{
            //    ResetAstar();
            //
            //}

            nextCell = pathList.ElementAt(pathList.IndexOf(currentCell) + 1);

            if (tileGrid[(int)nextCell.GridPos.X, (int)nextCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied)
            {
                int index = pathList.IndexOf(nextCell) - 1;

                if (index > pathCloneList.Count)
                {
                    pathList.Add(pathCloneList.ElementAt(pathList.IndexOf(nextCell) - 1));
                    targetCell = pathList.Last();
                }
            }

            //if (nextCell == null && pathList.Count >= 2)
            //    nextCell = pathList.ElementAt(1);

            //if (pathCloneList.Count != 0)
            //{
            //    while (tileGrid[(int)nextCell.GridPos.X, (int)nextCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied)
            //    {
            //        if (pathCloneList.Count != 0)
            //        {
            //            pathCloneList.Remove(pathCloneList.Last());
            //            nextCell = pathCloneList.Last();

            //            pathBlocked = true;
            //        }
            //        //targetCell = pathCloneList.Last();

            //        //pathList.Remove(pathList.Last());
            //        //targetCell = pathList.Last();
            //        //pathList.Reverse();
            //        //targetCell = pathList.Pop();
            //        //pathList = pathList.Reverse();
            //    }
            //}
            //else
            //{
            //    Cell backupCell = startCell;
            //    ResetAstar();
            //    owner.Target = tileGrid[(int)backupCell.GridPos.X, (int)backupCell.GridPos.Y];
            //}

            //if (pathStack.Peek() != null)
            //{
            //    //int x = pathCloneStack.Count;
            //    bool newTarget = false;

            //    while (tileGrid[(int)pathStack.Peek().GridPos.X, (int)pathStack.Peek().GridPos.Y].GetComponent<CTile>().IsUnitOccupied)
            //    {
            //        newTarget = true;
            //        //if (pathStack.Count == 1)
            //        //{
            //        //    pathStack.Push(pathCloneStack.ElementAt(x));
            //        //    x--;
            //        //}
            //        //else
            //        pathStack.Pop();
            //    }

            //    if (newTarget)
            //        targetCell = pathStack.Pop();
            //}
        }

        private GameObject ReadjustTarget(int xTarget, int yTarget)
        {
            int xStart = xTarget - 1,
                xEnd = xTarget + 1,
                xCompareMax = tileGrid.GetLength(0),
                yStart = yTarget - 1,
                yEnd = yTarget + 1,
                yCompareMax = tileGrid.GetLength(1);

            for (int x = xStart; x < xEnd; x++)//(int x = xTarget - 1; x < x + 2; x++)
                for (int y = yStart; y < yEnd; y++)//(int y = yTarget - 1; y < y + 2; y++)
                    if (x <= xCompareMax && x >= 0 && y <= yCompareMax && y >= 0)
                    //try
                    //{
                        if (!tileGrid[x, y].GetComponent<CTile>().IsUnitOccupied &&
                            tileGrid[x, y].GetComponent<CTile>().TileType == ETileType.Grass)
                            return tileGrid[x, y];
            //}
            //catch { }
            //}
            //else
            //{
            //    break;
            //}

            tileGrid[xTarget, yTarget].GetComponent<CTile>().IsUnitOccupied = false;
            return tileGrid[xTarget, yTarget];
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
