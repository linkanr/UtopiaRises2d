using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldSpaceUtils 
{
    
    private static Camera mainCamera;
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Plane at z = 0
        float t = -ray.origin.z / ray.direction.z;

        return ray.origin + t * ray.direction; // Intersection point with z = 0 plane
    }
    public static Vector3 GetMouseWorldPosition3D()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float maxDistance = 100f; // Set a reasonable max distance for the raycast
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 1f);
        if (Physics.Raycast(ray, out RaycastHit hitData, maxDistance, GameSceneRef.instance.collisionLayerGrid))
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
    public static bool CrossedBorder(Vector2 a, Vector2 b)
    {
        // Compute min/max bounds for x and y
        float minX = Mathf.Min(a.x, b.x);
        float maxX = Mathf.Max(a.x, b.x);
        float minY = Mathf.Min(a.y, b.y);
        float maxY = Mathf.Max(a.y, b.y);

        // Check if it crossed a vertical border (x = n + 0.5)
        for (float x = Mathf.Floor(minX) + 0.5f; x <= maxX; x += 1.0f)
        {
            if (x > minX && x <= maxX)
                return true;
        }

        // Check if it crossed a horizontal border (y = m + 0.5)
        for (float y = Mathf.Floor(minY) + 0.5f; y <= maxY; y += 1.0f)
        {
            if (y > minY && y <= maxY)
                return true;
        }

        return false;
    }
}
