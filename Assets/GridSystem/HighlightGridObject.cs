using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightGridObject : MonoBehaviour
{
    public Material gridMaterial;
    GridConstrution grid;
    
    private void Start()
    {
        
        
    }
    private void SetHightligt(List<Cell> cells)
{

    if (grid == null)
        {
            grid = GridCellManager.Instance.gridConstrution;
            transform.localScale = new Vector3(grid.sizeX*100, grid.sizeY*100, 1);
        }
    gridMaterial.SetColor("_DisplayColor", MouseDisplayManager.instance.displayColor);
    float minx = float.MaxValue;
    float miny = float.MaxValue;
    float maxx = float.MinValue;
    float maxy = float.MinValue;

    foreach (var cell in cells)
    {
        float uvX = (float)cell.x / grid.sizeX;
        float uvY = (float)cell.z / grid.sizeY;

        minx = Mathf.Min(minx, uvX);
        miny = Mathf.Min(miny, uvY);
        maxx = Mathf.Max(maxx, (cell.x + 1f) / grid.sizeX);
        maxy = Mathf.Max(maxy, (cell.z + 1f) / grid.sizeY);
    }

    // Apply calculated UVs to the material
    gridMaterial.SetFloat("_xmin", minx);
    gridMaterial.SetFloat("_ymin", miny);
    gridMaterial.SetFloat("_xmax", maxx);
    gridMaterial.SetFloat("_ymax", maxy);
}

    public void ResetGridMaterail()
    {
        gridMaterial.SetFloat("_xmin", 0f);
        gridMaterial.SetFloat("_ymin", 0f);
        gridMaterial.SetFloat("_xmax", 0f);
        gridMaterial.SetFloat("_ymax", 0f);
    }
    private void OnMouseOver()
    {
        //Debug.Log("onmouse over");
        if (MouseDisplayManager.instance == null)
        {
            return;
        }
        
        if (MouseDisplayManager.instance.displayCellChange)
        {
            //Debug.Log("Displaying cell");

            List<Cell> cells = GridCellManager.Instance.gridConstrution.GetCellListByWorldPosition(WorldSpaceUtils.GetMouseWorldPosition(),MouseDisplayManager.instance.displaySizeX,MouseDisplayManager.instance.displaySizeY);
            if (cells.Count > 0)
            {
                SetHightligt(cells);
       
            }
                
        }
        else if (!MouseDisplayManager.instance.displayCellChange)
        {
            //Debug.Log("Mouse manager not displaying cell change grid mat");
            ResetGridMaterail();
        }

    }
    public void OnMouseExit()
    {
        ResetGridMaterail();
    }





    // Start is called before the first frame update

}
