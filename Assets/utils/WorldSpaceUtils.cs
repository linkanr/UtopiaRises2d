using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldSpaceUtils 
{
    
    private static Camera mainCamera;
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);


    }
    public static Vector3 GetMouseWorldPosition3D()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out RaycastHit hitData, GameSceneRef.instance.collisionLayerGrid))
        {
            return hitData.point;

        }  
        
        
        return Vector3.zero;
    }
    public static Vector3 GetRandomDirection()
    {
        return new Vector3(
            Random.Range(-1f, 1),
            Random.Range(-1f, 1f)
            ).normalized;
    }
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

}
