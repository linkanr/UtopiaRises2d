using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerGlobal : MonoBehaviour
{
    [HideInInspector]
    public bool disableEdgeScrolling = true;
    [HideInInspector]
    public bool drawTargetLines = false;
    [HideInInspector]
    public bool drawCellOutlines = false;
    [HideInInspector]
    public static DebuggerGlobal instance;
    [HideInInspector]
    public bool effectInstancing;
    [HideInInspector]
    public bool drawCellInfluence;
    public Action EnabblePanel;
    [SerializeField]
    private SpriteRenderer heatmapSpriteRender;

    private GridConstrution grid;

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {

        TimeActions.OnSecondChange += SecondUpdate;
    }


    private void OnDisable()
    {

        TimeActions.OnSecondChange -= SecondUpdate;
    }
    public void ForcedUpdate()
    {
        SecondUpdate();
    }
    private void SecondUpdate()
    {
      //  Debug.Log("Second Update");
        if (drawCellInfluence)
        {
        //    Debug.Log("Cell Info Toggle is on");
            heatmapSpriteRender.enabled = true;
            if (DebuggPanelUi.instance.cellInfoDropdown.value == 0)
            {
          //      Debug.Log("Heatmap is on");
                CellActions.OnGenerateHeatTexture(GridTypeEnum.heat);
            }
            else if (DebuggPanelUi.instance.cellInfoDropdown.value == 1)
            {
            //    Debug.Log("Influence map is on");
                CellActions.OnGenerateHeatTexture(GridTypeEnum.influence);
            }

   

        }
        else
        {
            heatmapSpriteRender.enabled = false;
        }
    }

    private void Start()
    {

        SecondUpdate();
        disableEdgeScrolling = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EnableUiPanel();
        }

        if (drawCellOutlines)
        {
            DrawGridOutlines();
        }

        if (Input.GetKeyDown(KeyCode.F) && effectInstancing)
        {
            Debug.Log("keydown F");

            HandleCellEffects();
        }






    }


    public void EnableUiPanel()
    {
        EnabblePanel?.Invoke();
    }
    private void DrawGridOutlines()
    {

        if (grid == null)
        {
            grid = GridCellManager.instance.gridConstrution;
        }
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
        Vector3 offset = new Vector3(0.5f, 0.5f, 0f);

        // Original corners
        Vector3 bottomLeft = cell.worldPosition + new Vector3(-cell.size / 2, -cell.size / 2, 0);
        Vector3 bottomRight = cell.worldPosition + new Vector3(cell.size / 2, -cell.size / 2, 0);
        Vector3 topLeft = cell.worldPosition + new Vector3(-cell.size / 2, cell.size / 2, 0);
        Vector3 topRight = cell.worldPosition + new Vector3(cell.size / 2, cell.size / 2, 0);

        // Draw original green outline
        DrawLine(bottomLeft, bottomRight, Color.green);
        DrawLine(bottomRight, topRight, Color.green);
        DrawLine(topRight, topLeft, Color.green);
        DrawLine(topLeft, bottomLeft, Color.green);

        // Offset corners
        Vector3 offsetBottomLeft = bottomLeft + offset;
        Vector3 offsetBottomRight = bottomRight + offset;
        Vector3 offsetTopLeft = topLeft + offset;
        Vector3 offsetTopRight = topRight + offset;

        // Draw offset blue outline
        DrawLine(offsetBottomLeft, offsetBottomRight, Color.blue);
        DrawLine(offsetBottomRight, offsetTopRight, Color.blue);
        DrawLine(offsetTopRight, offsetTopLeft, Color.blue);
        DrawLine(offsetTopLeft, offsetBottomLeft, Color.blue);
    }


    private void HandleCellEffects()
    {
            if (grid == null)
        {
            grid = GridCellManager.instance.gridConstrution;
        }

        Cell currentCell = grid.GetCurrecntCellByMouse();
            Debug.Log("Current cell: " + currentCell);
            if (currentCell != null)
            {
                if (DebuggPanelUi.instance.effectType.value == 0)
                {
                    currentCell.CreateCellEffect(CellEffectEnum.Fire);
                }
                    
                else if (DebuggPanelUi.instance.effectType.value == 1)
                {
                    currentCell.CreateCellEffect(CellEffectEnum.Gas);
                }
                    
            }
        


    }

    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        Debug.DrawLine(start, end, color, .1f);
    }
}
