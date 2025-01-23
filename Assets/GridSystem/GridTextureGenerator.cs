using UnityEngine;
public class GridTextureGenerater
{
    public Texture2D GenerateGridTexture(GridConstrution grid ,Texture2D texture)
    {
        // Create a new Texture2D


        for (int x = 0; x < grid.sizeX; x++)
        {
            for (int y = 0; y < grid.sizeY; y++)
            {
                Cell cell = grid.gridArray[x, y];
                Color pixelColor = new Color();

                // Red channel: Walkability (0 or 1)

                bool walkable = cell.cellTerrain.isWalkable;
                if (cell.hasSceneObejct)
                {
                    walkable = cell.containingSceneObejct.walkable;
                }
        


                pixelColor.r = walkable ? 255f : 0f;

                // Green channel: Penalty (0 to 1 based on penalty range 0-1000)
                int penalty = cell.cellTerrain.walkPenalty;
                if (cell.hasSceneObejct)
                {
                    penalty = cell.containingSceneObejct.walkPenalty;
                }

                pixelColor.g = Mathf.Clamp01(penalty / 1000f)*255f;

                // Blue and Alpha channels can be reserved for other purposes
                pixelColor.b = 0f; // Reserved
                pixelColor.a = 1f; // Fully opaque

                // Set the pixel color
                texture.SetPixel(x , y, pixelColor);
                Debug.Log ("x: " + x + " y: " + y + " pixelColor: " + pixelColor);
            }
        }

        // Apply changes to the texture
        texture.Apply();

        return texture;
    }
}

