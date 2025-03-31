using UnityEngine;

public class HeatMapGenerator : MonoBehaviour
{
    public Texture2D heatMapTexture;
    private bool OnOff;
    GridConstrution grid;

    private void OnEnable()
    {
        CellActions.OnGenerateHeatTexture += GenerateHeatMapTexture;
    }
    private void OnDisable()
    {
        CellActions.OnGenerateHeatTexture -= GenerateHeatMapTexture;
    }


    public void GenerateHeatMapTexture(GridTypeEnum gridType)
    {
        if (grid == null )
        {
            grid = GridCellManager.instance.gridConstrution;

        }
        if (grid.gridArray == null)
        {
            Debug.LogError("Grid array is null");
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
                float heatValue;
                Cell cell = grid.gridArray[x, y];
                if (cell != null)
                {
                   
                    switch (gridType)
                    {
                        case GridTypeEnum.heat:
                            heatValue = Mathf.Clamp01(cell.heat); // Ensure the heat is between 0 and 1
                            break;
                        case GridTypeEnum.influence:
                            if (cell.isPlayerInfluenced)
                            {
                                heatValue = 1f;
                            }
                            else
                            {
                                heatValue = 0f;
                            }
                            break;
                        default:
                            heatValue = 0f;
                            break;
                    }
       
                    Color heatColor = GetHeatColor(heatValue, Color.black, Color.white);
                    heatMapTexture.SetPixel(x, y, heatColor);
                }
                else
                {
                    heatMapTexture.SetPixel(x, y, Color.black); // Default to black if cell is null
                }
            }
        }

        heatMapTexture.Apply();
        CellActions.UpdatedTexture?.Invoke(heatMapTexture);
   
    }

    private Color GetHeatColor(float heat, Color on, Color off)
    {
        Color a = new Color(0, 0, 0, 0);
        Color c = new Color(0.2f, 0.2f, 0, .1f);
        Color b = new Color(1, 1, 1, .2f);
       
        if (heat< 0.1f)
        {
            return Color.Lerp(a, b, heat);
        }
        return Color.Lerp(c, b, heat);
    }
}

public enum GridTypeEnum
{
    heat,
    influence,
}