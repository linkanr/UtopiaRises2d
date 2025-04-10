using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CellSolver : MonoBehaviour
{
    private GridConstrution grid; // Reference to the grid

    public HeatMapGenerator heatMapGenerator;
    float disipation = .15f;

    public void Init()
    {
        heatMapGenerator = GetComponent<HeatMapGenerator>();
        grid = GridCellManager.instance.gridConstrution; // Get the GridConstruction instance

        foreach (Cell cell in grid.GetCellList())
        {
            cell.neigbours = new List<Cell>();
            int[,] directions = new int[,] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newX = cell.x + directions[i, 0];
                int newY = cell.z + directions[i, 1];

                if (newX >= 0 && newX < grid.sizeX && newY >= 0 && newY < grid.sizeY)
                {
                    Cell neighbour = grid.gridArray[newX, newY];
                    if (neighbour != null)
                    {
                        cell.neigbours.Add(neighbour);
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        TimeActions.OnSecondChange += RunSolver;
    }

    private void OnDisable()
    {
        TimeActions.OnSecondChange -= RunSolver;
    }

    private async void RunSolver()
    {
        await SolveHeavyCalculation();
    }

    private async Task SolveHeavyCalculation()
    {
        await Task.Run(() =>
        {
            int width = grid.sizeX;
            int height = grid.sizeY;

            // Iterate over the entire grid and update each cell
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cell cell = grid.gridArray[x, y];
                    GenereateHeat(cell);
                    GenerateHumidity(cell);
                    Diffusion(cell);
                    CheckForNewCellEffects(cell);
                    UpdateTiles(cell);
                    Dissipation(cell);
                }
            }
        });

        // Debug.Log("Solver finished updating grid cells.");
    }

    private void Dissipation(Cell cell)
    {
        if (cell.heat >= disipation)
        {
            cell.heat -= disipation;
        }
        else
        {
            cell.heat = 0;
        }
    }

    private void UpdateTiles(Cell cell)
    {

    }

    private static System.Random randomGenerator = new System.Random();

    private void CheckForNewCellEffects(Cell cell)
    {
        if (!cell.burning && cell.heat > 0.1f)
        {
            float random = (float)randomGenerator.NextDouble(); // Thread-safe random
            float mulitplier = 0f;
            Debug.Log(cell.heat + "is cell heat");
            if (cell.containingEnvObject != null)
            {
                if (cell.containingEnvObject.GetStats().addFuelToFire)
                {
                    Debug.Log("found env object");
                    mulitplier = 20f;

                 
                }
            }


             else
            {
                Debug.Log("found no env object");
                mulitplier = cell.cellTerrain.chanceOfCatchingFire;
            }

            Debug.Log($"[CellSolver] Random value: {random} for cell at ({cell.x}, {cell.z}) with heat: {cell.heat} and multiplier: {mulitplier}");

            if (random < cell.heat * mulitplier)
            {

                Debug.Log($"[CellSolver] Cell at ({cell.x}, {cell.z}) caught fire with heat: {cell.heat}");
                UnityMainThreadDispatcher.Enqueue(() =>
                {
                    cell.CreateCellEffect(CellEffectEnum.Fire); // Now runs on the main thread
                });
            }
        }

        if (cell.burning)
        {
            if (cell.heat < 0.1f)
            {
                float random = (float)randomGenerator.NextDouble();
                if (random / 10f > cell.heat)
                {
                    UnityMainThreadDispatcher.Enqueue(() =>
                    {
                        if (cell != null)
                            if (cell.cellEffect != null)
                                cell.cellEffect.RemoveCellEffect(); // Now runs on the main thread
                    });
                }
            }
        }
    }

    private void Diffusion(Cell cell)
    {
        int i = 0;
        if (cell.heat < .15f)
        {
            return;
        }

        foreach (Cell neighbour in cell.neigbours)
        {
            float transferRate = 0.2f * neighbour.cellTerrain.heatTransferAmount;
            float heatDifference = cell.heat - neighbour.heat;
            float heatTransfer = heatDifference * transferRate;
            cell.heat -= heatTransfer;
            neighbour.heat += heatTransfer;
            i++;
        }
    }

    private void GenerateHumidity(Cell cell)
    {
        if (cell.containingEnvObject == null && cell.heat>.1f)
        {
            if (cell.cellTerrain.generateHumidity > 0)
            {
                 cell.heat = Mathf.Max( cell.heat - cell.cellTerrain.generateHumidity,0f);

            }
        }
    }

    private void GenereateHeat(Cell cell)
    {
        if (cell.burning)
        {
            if (cell.containingEnvObject != null)
            {
                if (cell.containingEnvObject.GetStats().addFuelToFire)
                {
                    cell.heat += .75f;
                    return;
                }
                else
                {
                    return;
                }
            }
            else if (cell.cellTerrain.fuel > 0)
            {
                cell.heat += .5f;
                cell.cellTerrain.fuel -= .2f;

                if (cell.cellTerrain.fuel <= 0)
                {
                    cell.cellTerrain.fuel = 0;
                    UnityMainThreadDispatcher.Enqueue(() =>
                    {
                        cell.BurnedTerrain(); // Now runs on the main thread
                    });
                }

                return;
            }
            else
            {
                return;
            }
        }
    }
}
