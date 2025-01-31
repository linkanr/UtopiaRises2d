using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class DebuggerGlobal : MonoBehaviour
{
    public bool drawTargetLines;          // Original functionality
    public bool debugSceneObejcts;       // Original functionality
    public bool drawCellOutlines;        // New toggle for drawing cell outlines
    public bool enableCellEffects;                // Original functionality
    public static DebuggerGlobal instance; // Singleton instance
    public Sprite spriteMouse;
    public bool clickVToGetSceneObjectsInCell;
    public bool EnemyCreation;
    public SoEnemyObject enemyObject; 

    private void Awake()
    {
        instance = this;
    }

    public static void DrawLine(Vector3 start, Vector3 end)
    {
        Debug.DrawLine(start, end, Color.blue, .1f);
    }

    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        Debug.DrawLine(start, end, color, .1f);
    }

    private void Update()
    {
        if (clickVToGetSceneObjectsInCell)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                SceneObject[] sceneObjects = GridCellManager.Instance.gridConstrution.GetCurrecntCellByMouse().containingSceneObjects;
                foreach (SceneObject s in sceneObjects)
                {
                    Debug.Log(s.GetStats().name + " is in cell");
                }
                if (sceneObjects.Length == 0)
                {
                    Debug.Log("No scene objects in cell");
                }
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                CellEffect cellEffect = GridCellManager.Instance.gridConstrution.GetCurrecntCellByMouse().cellEffect;
                if (cellEffect != null)
                {
                    Debug.Log(cellEffect.cellTerrainEnum.ToString() + " is in cell");
                }
            }


            // Check if we need to draw cell outlines
            if (drawCellOutlines)
            {
                DrawGridOutlines();
            }

            // Original functionality for debugging other aspects
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log(WorldSpaceUtils.CheckClickableType().ToString());
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(GameSceneRef.instance.inHandPile);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                PickupEffectAdd.AddEffect(PickupTypes.Slow, SceneObjectManager.Instance.sceneObjectGetter.GetSceneObject(WorldSpaceUtils.GetMouseWorldPosition()), 5);
            }

            if (debugSceneObejcts && Input.GetKeyDown(KeyCode.Z))
            {
                foreach (SceneObject s in SceneObjectManager.Instance.sceneObjectsInScene)
                {
                    Debug.Log(s.GetStats().GetString(StatsInfoTypeEnum.name) + " is in manager");
                    if (s.transform == null)
                    {
                        Debug.Log(s.GetStats().GetString(StatsInfoTypeEnum.name) + " is missing transform");
                    }
                }
            }
            if (enableCellEffects)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    GridCellManager.Instance.gridConstrution.GetCurrecntCellByMouse().CreateCellEffect(CellEffectEnum.Fire);
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    GridCellManager.Instance.gridConstrution.GetCurrecntCellByMouse().CreateCellEffect(CellEffectEnum.Gas);
                }

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
        // Safely access the active grid from GameSceneRef
        if (GridCellManager.Instance.gridConstrution != null)
        {
            GridConstrution grid = GridCellManager.Instance.gridConstrution;

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
    }

    private void DrawCellOutline(Cell cell)
    {
        Vector3 bottomLeft = cell.worldPosition + new Vector3(-cell.size / 2, -cell.size / 2, 0);
        Vector3 bottomRight = cell.worldPosition + new Vector3(cell.size / 2, -cell.size / 2, 0);
        Vector3 topLeft = cell.worldPosition + new Vector3(-cell.size / 2, cell.size / 2, 0);
        Vector3 topRight = cell.worldPosition + new Vector3(cell.size / 2, cell.size / 2, 0);

        // Draw the four edges of the cell
        DrawLine(bottomLeft, bottomRight, Color.green);
        DrawLine(bottomRight, topRight, Color.green);
        DrawLine(topRight, topLeft, Color.green);
        DrawLine(topLeft, bottomLeft, Color.green);
    }
}
