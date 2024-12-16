using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightGridObject : MonoBehaviour
{
    public Material gridMaterial;
    bool IsSelected;
    private void OnEnable()
    {
        BattleSceneActions.OnCardLocked += HandleCellchange;
    }


    private void OnDisable()
    {
        BattleSceneActions.OnCardLocked -= HandleCellchange;
    }
    private void HandleCellchange(bool obj)
    {
        Debug.Log("handle cell change");
        IsSelected = obj;
        if (IsSelected == false)
        {
            ResetGridMaterail();
        }
    }


    private void SetHightligt(Cell cell)
    {
        //Debug.Log("setting highlight");
        float minx;
        float miny;
        if (cell.x == 0)
        {
            minx = 0;
        }
        else
        {
            minx = (float)cell.x / (float)cell.gridRef.sizeX;
        }
        if (cell.y ==0)
        {
            miny = 0;
        }
        else
        {
            miny = (float)cell.y / (float)cell.gridRef.sizeY;
        }
        //Debug.Log("cell.x i s" + cell.x + "cell grid ref x is " + cell.gridRef.sizeX.ToString());
        float maxx = ((float)cell.x+1f) / (float)cell.gridRef.sizeX;
        
        float maxy = ((float)cell.y + 1f) / (float)cell.gridRef.sizeY;

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
        if (IsSelected)
        {
            //Debug.Log("Triggered");

            Cell cell = GridCellManager.Instance.gridConstrution.GetCurrecntCellByMouse();
            if (cell != null)
                SetHightligt(cell);
        }

    }
    private void OnMouseExit()
    {
        ResetGridMaterail();
    }





    // Start is called before the first frame update

}
