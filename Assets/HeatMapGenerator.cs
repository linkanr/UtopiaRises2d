using UnityEngine;

public class HeatMapGenerator : MonoBehaviour
{
    public Texture2D heatMapTexture;

    public Texture2D GenerateHeatMapTexture(GridConstrution grid)
    {
        if (grid == null || grid.gridArray == null)
        {
            Debug.LogError("Grid or GridArray is null");
            return null;
        }

        int width = grid.sizeX;
        int height = grid.sizeY;
        if (heatMapTexture == null)
        {
            heatMapTexture = new Texture2D(width, height);
            heatMapTexture.filterMode = FilterMode.Point;
            heatMapTexture.wrapMode = TextureWrapMode.Clamp;
        }
         

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid.gridArray[x, y];
                if (cell != null)
                {
                    float heatValue = Mathf.Clamp01(cell.heat); // Ensure the heat is between 0 and 1
                    Color heatColor = GetHeatColor(heatValue);
                    heatMapTexture.SetPixel(x, y, heatColor);
                }
                else
                {
                    heatMapTexture.SetPixel(x, y, Color.black); // Default to black if cell is null
                }
            }
        }

        heatMapTexture.Apply();
        Debug.Log("Heatmap texture generated.");
        CellActions.updatedTexture?.Invoke(heatMapTexture);
        return heatMapTexture;
    }

    private Color GetHeatColor(float heat)
    {
        // Maps the heat value to a color gradient (Blue -> Green -> Yellow -> Red)
        return Color.Lerp(Color.black, Color.white, heat);
    }
}
