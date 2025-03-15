using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerGlobal : MonoBehaviour
{
    public bool drawTargetLines;
    public bool debugSceneObejcts;
    public bool drawCellOutlines;
    public bool enableCellEffects;
    public static DebuggerGlobal instance;
    public Sprite spriteMouse;
    public bool clickVToGetSceneObjectsInCell;
    public bool EnemyCreation;
    public SoEnemyObject enemyObject;
    public bool displayHeatMap;  // Toggle for enabling/disabling heat text
    public bool dontLoadSystems;

    private GridConstrution grid;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        grid = GridCellManager.Instance.gridConstrution;
    }

    private void Update()
    {
        if (clickVToGetSceneObjectsInCell)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                SceneObject[] sceneObjects = grid.GetCurrecntCellByMouse().containingSceneObjects.ToArray();
                foreach (SceneObject s in sceneObjects)
                {
                    Debug.Log(s.GetStats().name + " is in cell");
                }
                if (sceneObjects.Length == 0)
                {
                    Debug.Log("No scene objects in cell");
                }
            }

            if (drawCellOutlines)
            {
                DrawGridOutlines();
            }

            if (enableCellEffects)
            {
                HandleCellEffects();
            }

            if (EnemyCreation)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EnemyCreator.CreateEnemy(enemyObject, WorldSpaceUtils.GetMouseWorldPosition());
                }
            }
        }
    }

    private void DrawGridOutlines()
    {
        if (grid == null) return;

        for (int x = 0; x < grid.sizeX; x++)
        {
            for (int y = 0; y < grid.sizeY; y++)
            {
                Cell cell = grid.gridArray[x, y];
                if (cell != null)
                {
                    DrawCellOutline(cell);
                }
            }
        }
    }

    private void DrawCellOutline(Cell cell)
    {
        Vector3 bottomLeft = cell.worldPosition + new Vector3(-cell.size / 2, -cell.size / 2, 0);
        Vector3 bottomRight = cell.worldPosition + new Vector3(cell.size / 2, -cell.size / 2, 0);
        Vector3 topLeft = cell.worldPosition + new Vector3(-cell.size / 2, cell.size / 2, 0);
        Vector3 topRight = cell.worldPosition + new Vector3(cell.size / 2, cell.size / 2, 0);

        DrawLine(bottomLeft, bottomRight, Color.green);
        DrawLine(bottomRight, topRight, Color.green);
        DrawLine(topRight, topLeft, Color.green);
        DrawLine(topLeft, bottomLeft, Color.green);
    }

    private void HandleCellEffects()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Cell currentCell = grid.GetCurrecntCellByMouse();
            if (currentCell != null)
            {
                currentCell.CreateCellEffect(CellEffectEnum.Fire);
                Debug.Log($"Applied Fire effect to cell at {currentCell.x}, {currentCell.z}");
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Cell currentCell = grid.GetCurrecntCellByMouse();
            if (currentCell != null)
            {
                currentCell.CreateCellEffect(CellEffectEnum.Gas);
                Debug.Log($"Applied Gas effect to cell at {currentCell.x}, {currentCell.z}");
            }
        }
    }

    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        Debug.DrawLine(start, end, color, .1f);
    }
}
