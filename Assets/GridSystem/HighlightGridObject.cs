using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightGridObject : MonoBehaviour
{
    public Material gridMaterial;

    private void SetHightligt(List<Cell> cells)
    {
        float minx = float.MaxValue;
        float miny = float.MaxValue;
        float maxx = float.MinValue;
        float maxy = float.MinValue;
        foreach (var cell in cells) 
        {
            //Debug.Log("setting highlight");

            if (cell.x == 0)
            {
                minx = 0;
            }
            else
            {
                minx = Mathf.Min(minx, (float)cell.x / (float)cell.gridRef.sizeX); 
            }
            if (cell.y == 0)
            {
                miny = 0;
            }
            else
            {
                miny = Mathf.Min(miny, (float)cell.y / (float)cell.gridRef.sizeY);
            }
            //Debug.Log("cell.x i s" + cell.x + "cell grid ref x is " + cell.gridRef.sizeX.ToString());
            maxx = Mathf.Max(maxx, ((float)cell.x + 1f) / (float)cell.gridRef.sizeX);

            maxy = Mathf.Max(maxy, ((float)cell.y + 1f) / (float)cell.gridRef.sizeY);


        }



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
        
        if (MouseDisplayManager.instance.displayCellChange)
        {
            //Debug.Log("Triggered");

            List<Cell> cells = GridCellManager.Instance.gridConstrution.GetCellListByWorldPosition(WorldSpaceUtils.GetMouseWorldPosition(),MouseDisplayManager.instance.displaySizeX,MouseDisplayManager.instance.displaySizeY);
            if (cells.Count > 0)
            {
                SetHightligt(cells);
       
            }
                
        }
        else if (!MouseDisplayManager.instance.displayCellChange)
        {
            ResetGridMaterail();
        }

    }
    public void OnMouseExit()
    {
        ResetGridMaterail();
    }





    // Start is called before the first frame update

}
