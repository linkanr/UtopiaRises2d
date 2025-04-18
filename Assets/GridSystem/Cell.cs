﻿using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Cell: IDisposable
{
    
    public Cell (int x, int z, GridConstrution grid, CellTerrain _cellTerrain )
    {
        this.x = x;
        this.z = z;
        gridRef = grid;
        size = grid.cellSize;
        cellTerrain = GameObject.Instantiate( _cellTerrain);
        containingSceneObjects = new List<SceneObject>();





    }


    public int x;
    public int z;
    public float height;
    public GridConstrution gridRef;
    public string information;
    public float size;
    public CellTerrain cellTerrain; 
    public CellEffect cellEffect;
    public string cellStartingObject;
    public float heat;
    public float humidity;
    public List<Cell> neigbours;
    private bool disposed = false;
    private bool isPlayerIfluenced =false;
    public bool burning { get {  if (cellEffect == null) { return false; } else return   cellEffect.cellTerrainEnum == CellEffectEnum.Fire; } }
    public bool isPlayerInfluenced { 
        get {
            if (cellTerrain.cellTerrainEnum != CellTerrainEnum.playerTerrain)
            {
                return false;
            }
            return isPlayerIfluenced; 
            }
        set {
            if (cellTerrain.cellTerrainEnum !=CellTerrainEnum.playerTerrain)
            {
                isPlayerIfluenced = false;
            }
            else
            {
                isPlayerIfluenced = value;
            }
            
        }
    }

    public List <SceneObject> containingSceneObjects;

    

    public SceneObject containingEnvObject { get { return GetSceneObject<EnviromentObject>(); } }
    public SceneObject contingingMinorObject { get { return GetSceneObject<MinorSceneObjects>(); } }

    public bool CanBurn()
    {
        if (cellTerrain.fuel>0 )
        {
            return true;
        }
        if (containingEnvObject != null)
        {
            if (containingEnvObject.GetStats().addFuelToFire)
            {
                return true;
            }
        }
        return false;
    }
    private SceneObject GetSceneObject<T>() where T: SceneObject
    {
        foreach (SceneObject sceneObject in containingSceneObjects)
        {
            if (sceneObject is T)
            {
                return sceneObject;
            }
            
        }
        return null;
    }

    public Vector3 worldPosition { get { return gridRef.GetWorldPosition(x, z); } }
    public void CreateCellEffect(CellEffectEnum _cellEffect)
    {
        if (cellEffect != null)
        {
            cellEffect.RemoveCellEffect(); // Ensure old effect is properly removed
        }

        cellEffect = CellEffectCreator.CreateCellEffect(_cellEffect, this);

        // Log to confirm effect creation
        //Debug.Log($"Cell Effect Created: {_cellEffect} at {worldPosition}");

        // The action call triggers the update in the Tilemap
        CellActions.UpdateCellEffect.Invoke(new CellEffectUpdateArgs { cell = this, cellEffect = _cellEffect });
    }

    public void RemoveCellEffect()
    {
        cellEffect = null;
        
    }

    public void AddSceneObjects(SceneObject _sceneObject)
    {
        containingSceneObjects.Add(_sceneObject) ;
    }
    public void RemoveSceneObject(SceneObject _sceneObject)
    {
        containingSceneObjects.Remove(_sceneObject);
    }
    public bool hasSceneObejct
    {
        get
        {
            if (containingSceneObjects.Count > 0)
            {
                return true;
            }
            return false;

        }
    }
    public float GetWalkPenalty()
    {
        float walkPenalty =1f;
        walkPenalty = cellTerrain.walkPenalty;
        if (hasSceneObejct)
        {
            walkPenalty = HandleSceneObjectWalkPenalty(walkPenalty);
            
        }

        return walkPenalty;

    }

    private float HandleSceneObjectWalkPenalty(float input)
    {
        float walkPenalty = input;
        foreach (SceneObject sceneObject in containingSceneObjects)
        {

            float moveFactor =  sceneObject.GetStats().statsInfoDic.TryToGetValue<float>(StatsInfoTypeEnum.moveFactor);
            if (moveFactor != 0f)
            {
                walkPenalty = (float)moveFactor;
            }
        

            if (cellEffect != null)
            {
                walkPenalty *= cellEffect.walkPenalty;
            }
            

           
        }
        return walkPenalty;
    }

    public void BurnedTerrain()
    {
        switch (cellTerrain.cellTerrainEnum)
        {
            case CellTerrainEnum.grass:
                cellTerrain.Dispose();
                cellTerrain = GameObject.Instantiate( GridCellManager.instance.GetTerrainFromEneum(CellTerrainEnum.soil));
                
                List<Cell> celllist  = new List<Cell>();
                foreach (Cell neigbour in neigbours)
                {
                    celllist.Add(neigbour);
                }
                celllist.Add(this);
                CellActions.UpdateCells.Invoke(neigbours);

                break;
        }
    }

  

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;

        if (disposing)
        {
            // Dispose managed objects
            if (cellEffect != null)
            {
                cellEffect.Dispose();
                cellEffect = null;
            }

            if (cellTerrain != null)
            {

                cellTerrain = null;
            }

            neigbours?.Clear();
            gridRef = null;
        }

        disposed = true;
    }

    internal void CheckPlayerInfluence()
    {
        List<SceneObjectTypeEnum> list = new List<SceneObjectTypeEnum>();
        list.Add(SceneObjectTypeEnum.playerbuilding);
        list.Add(SceneObjectTypeEnum.playerConstructionBase);
        List<SceneObject> objects = new List<SceneObject>();
        objects = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(worldPosition, sceneObjectTypeEnumsList: list,maxDistance:6f);
        foreach (SceneObject sceneObject in objects)
        {

                float distance = Vector3.Distance(worldPosition, sceneObject.transform.position);
                if (distance*2 < sceneObject.GetStats().influenceRadius)
                {
                    isPlayerInfluenced = true;
                    return;
                }
                
               
            
        }
    }
        // Destructor (Finalizer)
        ~Cell()
    {
        Dispose(false);
    }
}