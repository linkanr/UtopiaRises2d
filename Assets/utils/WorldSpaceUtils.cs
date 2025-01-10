using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldSpaceUtils 
{
    
    private static Camera mainCamera;
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Vector3 r = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        r.z = 0;
        return r;


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
    public static Vector3 GetRandomDirection(float xScale, float yScale , float zScale)
    {
        Vector3 nV = new Vector3(
            Random.Range(-1f, 1),
            Random.Range(-1f, 1f)
            ).normalized;
        nV.x *= xScale;
        nV.y *= yScale;
        nV.z *= zScale;
        return nV;
    }
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

    public static ClickableType CheckClickableType()
    {
        Vector3 pos = GetMouseWorldPosition();
        var collisions = Physics2D.OverlapPointAll(pos);
        IClickableObject clickableObject;
        foreach (Collider2D col in collisions)
        {
            if (col.GetComponent<IClickableObject>()!=null)
            {
                clickableObject = col.GetComponent<IClickableObject>();
                if (clickableObject.GetClickableType()!= ClickableType.notFound)
                return clickableObject.GetClickableType();
            }
        }
        return ClickableType.notFound;
 
    }
}
