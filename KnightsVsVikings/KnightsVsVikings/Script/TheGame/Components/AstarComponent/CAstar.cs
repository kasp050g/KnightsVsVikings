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
    // Lucas & Kasper
    public class CAstar : Component
    {
        private const byte DEFAULT_COST = 10;

        private byte tileSize = Singletons.LevelInformationSingleton.TileSize;

        private bool exit = false;

        private Thread astarThread;

        private Cell startCell = null,
                     currentCell = null,
                     pathCell = null,
                     targetCell = null;

        private CUnit owner = null;

        private Cell[,] baseGrid = null;
        private GameObject[,] tileGrid = null;

        private List<Cell> openList = new List<Cell>(),
                           closedList = new List<Cell>(),
                           pathList = new List<Cell>();

        public CAstar(CUnit owner)
        {
            this.owner = owner;

            astarThread = new Thread(AstarThread);
            astarThread.IsBackground = true;
            astarThread.Start();
        }

        private void AstarThread()
        {
            while (GameObject.IsActive)
            {
                Thread.Sleep(8);

                exit = false;

                // Henter informationer til baseGrid og tileGrid, hvis dette ikk er gjort.
                if (Singletons.AstarGlobalSingleton.BaseMapGrid != null && baseGrid == null)
                {
                    baseGrid = Singletons.AstarGlobalSingleton.BaseMapGrid.ToAstarMatrix();
                    tileGrid = Singletons.AstarGlobalSingleton.TileGrid;
                }

                if (owner.Target != null)
                {
                    if (pathList.Count == 0)
                        GetPath();
                    else
                        FollowPath();
                }
            }
        }


        /// <summary>
        /// Resets the A* algorithm.
        /// </summary>
        public void ResetAstar()
        {
            currentCell = null;
            targetCell = null;
            startCell = null;
            pathCell = null;

            pathList.Clear();
            openList.Clear();
            closedList.Clear();

            baseGrid = Singletons.AstarGlobalSingleton.BaseMapGrid.ToAstarMatrix();

            owner.Target = null;
            owner.IsMoving = false;

            GameObject.GetComponent<CMove>().Velocity = Vector2.Zero;

            exit = true;
        }

        /// <summary>
        /// Finds and return a paths.
        /// </summary>
        private void GetPath()
        {
                startCell = baseGrid[(int)GameObject.Transform.Position.X / tileSize,
                                     (int)GameObject.Transform.Position.Y / tileSize];

                targetCell = baseGrid[(int)owner.Target.Transform.Position.X / tileSize,
                                      (int)owner.Target.Transform.Position.Y / tileSize];

            if (startCell != targetCell)
            {
                // Hvis målet er blacklisted, altså ikke muligt, at nå frem til, så afslut søgningen.
                if (targetCell.BlackListed)
                    ResetAstar();

                if (startCell != null)
                    tileGrid[(int)startCell.GridPos.X, (int)startCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = false;
                else
                    exit = true;

                // Først skal vi have start cellen med på open listen.
                ModifyNeighbourCells(startCell, null);

                if (targetCell != null)
                    FindPath();
            }
        }

        /// <summary>
        /// Follows a found path.
        /// </summary>
        private void FollowPath()
        {
            Vector2 ownerGridPos = new Vector2(owner.GameObject.Transform.Position.X / tileSize,
                                               owner.GameObject.Transform.Position.Y / tileSize);

            if (pathCell == null)
            {
                owner.IsMoving = true;
                pathCell = pathList.First();
            }

            // Hvis jeg er tæt på mit mål i pathen.
            if (ownerGridPos.ApproximatelyEqual(pathCell.GridPos, 0.1f))
            {
                // Hvis mit endelig mål er blevet overtaget af en anden enhed, og hvis der ikke er en ressource på mit mål.
                if (tileGrid[(int)targetCell.GridPos.X, (int)targetCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied &&
                    !tileGrid[(int)targetCell.GridPos.X, (int)targetCell.GridPos.Y].GetComponent<CTile>().IsResourceOccupied)
                {
                    /* Her bliver der sat om der er placeret en bygning på målet, og denne enhed er en worker.
                     * Dette bliver gjort siden Workers bruger TownHall som et sted at levere fundne ressourcer.
                     * Derfor vil man ikke have noget i mod en Worker gå hen på en TownHall.                     * 
                    */
                    if (!(owner.UnitType == EUnitType.Worker) && tileGrid[(int)targetCell.GridPos.X, (int)targetCell.GridPos.Y].GetComponent<CTile>().IsCanBuildHere)
                    {
                        // Hvis overstående ikke er sandt, så finder A* algoritmen et nyt Target i nærheden af det endelige mål,
                        // og genstarter algoritmen med det nye mål.
                        GameObject storeTarget = ReadjustTarget((int)targetCell.GridPos.X, (int)targetCell.GridPos.Y);
                        ResetAstar();
                        owner.Target = storeTarget;
                    }
                }
                else if (pathCell.GridPos == targetCell.GridPos)
                {
                    // Hvis enheden er på det endelige mål, kan vi nulstille algoritmen.

                    GameObject.Transform.Position = new Vector2(pathCell.GridPos.X * tileSize, pathCell.GridPos.Y * tileSize);
                    tileGrid[(int)pathCell.GridPos.X, (int)pathCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = true;

                    pathCell.FGH = new FGH();
                    pathCell.ECellType = ECellType.Default;

                    ResetAstar();
                }
                else
                {
                    // Hvis enheden ikke er på det endelige mål, men er nået et af målene på pathen, så nulstilles det mål enheden er nået til.
                    pathList.Remove(pathCell);
                    pathCell.FGH = new FGH();
                    pathCell.ECellType = ECellType.Default;
                    GameObject.GetComponent<CMove>().Velocity = Vector2.Zero;

                    pathCell = null;
                }
            }
            else
            {
                // Enheden bevæger sig over mod det nuværende mål i pathen.
                owner.MoveToLocation(new Vector2(pathCell.GridPos.X * tileSize, pathCell.GridPos.Y * tileSize));
            }
        }

        /// <summary>
        /// Finds a path.
        /// </summary>
        private void FindPath()
        {
            /* For at finde en path, går vi i gennem et while loop,
             * indtil den nuværende celle er lig det endelige mål.
             * Hvis cellen på et tidspunkt bliver lig null, eller den nuværende cell bliver lig null
             * så stoppes loopet også. Hvis openListen bliver tom, hvilket betyder algoritmen har undersøgt hele banen,
             * så blacklistes det nuværende mål, dette betyder, at den ikke vil prøve, at finde det mål igen.
             */
            while (!exit)
            {
                Thread.Sleep(5);

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

            // Når while loopet er slut, og vi stadigt har en start celle, så returneres den fundne path.
            if (startCell != null)
            {
                tileGrid[(int)startCell.GridPos.X, (int)startCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = false;

                ReturnPath();

                // her skal pathen reverses, siden den går fra mål til enhed, når den finder pathen.
                pathList.Reverse();
            }
        }

        /// <summary>
        /// Finds the next cell to explore.
        /// </summary>
        private void GetNextCell()
        {
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

        /// <summary>
        /// Explores the neighbour cells to the current cell.
        /// </summary>
        private void SetNeighbourCells()
        {
            // X
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X + 1, (int)currentCell.GridPos.Y], currentCell); } catch { }
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X - 1, (int)currentCell.GridPos.Y], currentCell); } catch { }

            // Y
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y + 1], currentCell); } catch { }
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y - 1], currentCell); } catch { }
        }

        /// <summary>
        /// Changes the neighbour cells FGH price.
        /// </summary>
        /// <param name="cell">The cell to change the FGH of.</param>
        /// <param name="parent">The parent of the cell.</param>
        private void ModifyNeighbourCells(Cell cell, Cell parent)
        {
            if (cell == null)
            {
                exit = true;
            }
            else
            {
                // Hvis cellen er default eller ikke er occupied af en enhed.
                if (baseGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y].ECellType == ECellType.Default && !tileGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied)
                {
                    if (!closedList.Contains(cell) && !openList.Contains(cell))
                        openList.Add(cell);

                    cell.Parent = parent;
                    cell.FGH = new FGH(CalculateG(cell), CalculateH(cell.GridPos, targetCell.GridPos));
                    cell.ECellType = ECellType.Open;
                }
                // Hvis cellen allerede eksistere på openListen, så ses der om der kan gives en bedre FGH pris.
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

        /// <summary>
        /// Returns a found path.
        /// </summary>
        private void ReturnPath()
        {
            /* Dette while loop kører indtil den nuværende celle er lig start cellen.
             * Dette vil sige den finder en path fra det endelig mål til enheden.
             */
            while (!exit)
            {
                Thread.Sleep(5);

                if (currentCell != null)
                {
                    pathList.Add(currentCell);
                    if (currentCell.Parent == startCell)
                        break;

                    currentCell = currentCell.Parent;
                    currentCell.ECellType = ECellType.Path;
                }
                else
                    break;
            }
        }

        /// <summary>
        /// Explore cells near the Target cell for a new target.
        /// </summary>
        /// <param name="xTarget">X Grid Position of the Target cell.</param>
        /// <param name="yTarget">X Grid Position of the Target cell.</param>
        /// <returns>Returns a new Target.</returns>
        private GameObject ReadjustTarget(int xTarget, int yTarget)
        {
            int xStart = xTarget - 1,
                xEnd = xTarget + 1,
                xCompareMax = tileGrid.GetLength(0),
                yStart = yTarget - 1,
                yEnd = yTarget + 1,
                yCompareMax = tileGrid.GetLength(1);

            for (int x = xStart; x < xEnd; x++)
                for (int y = yStart; y < yEnd; y++)
                    if (x <= xCompareMax && x >= 0 && y <= yCompareMax && y >= 0)
                        if (!tileGrid[x, y].GetComponent<CTile>().IsUnitOccupied &&
                            tileGrid[x, y].GetComponent<CTile>().TileType == ETileType.Grass)
                            return tileGrid[x, y];

            tileGrid[xTarget, yTarget].GetComponent<CTile>().IsUnitOccupied = false;
            return tileGrid[xTarget, yTarget];
        }

        /// <summary>
        /// Calculates a cells G price from FGH.
        /// </summary>
        /// <param name="cell">Cell to calculate on.</param>
        /// <returns>Returns a new G price.</returns>
        private uint CalculateG(Cell cell)
        {
            if (cell.Parent != null)
                return cell.Parent.FGH.G + DEFAULT_COST;

            return DEFAULT_COST;
        }

        /// <summary>
        /// Calculates a cells H price from FGH.
        /// </summary>
        /// <param name="pos">Cells current position.</param>
        /// <param name="target">Targets position.</param>
        /// <returns>Returns a new H price.</returns>
        private uint CalculateH(Vector2 pos, Vector2 target)
        {
            return (uint)Math.Abs(Vector2.Distance(pos, target) * 10);
        }

    }
}
