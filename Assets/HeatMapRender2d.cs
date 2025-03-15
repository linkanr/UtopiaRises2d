using UnityEngine;

public class HeatmapRenderer2D : MonoBehaviour
{
    private Texture2D heatmapTexture;
    private GridConstrution grid;
    bool on = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        grid = GridCellManager.Instance.gridConstrution;

        if (grid == null || grid.sizeX <= 0 || grid.sizeY <= 0)
        {
            Debug.LogError("Grid is not initialized properly!");
            return;
        }

        heatmapTexture = new Texture2D(grid.sizeX, grid.sizeY);
        heatmapTexture.filterMode = FilterMode.Point;
        heatmapTexture.wrapMode = TextureWrapMode.Clamp;

        spriteRenderer = GetComponent<SpriteRenderer>();

        // Create the sprite and assign it
        Sprite heatmapSprite = Sprite.Create(
            heatmapTexture,
            new Rect(0, 0, grid.sizeX, grid.sizeY),
            new Vector2(0.5f, 0.5f),
            1f
        );
        spriteRenderer.sprite = heatmapSprite;

        // 🔹 Keep local scale at (1,1) to match world space correctly
        transform.localScale = Vector3.one;

        // 🔹 Ensure heatmap covers the correct position
        transform.position = new Vector3(grid.sizeX / 2f, grid.sizeY / 2f, 0);


        UpdateOnOff(DebuggerGlobal.instance.displayHeatMap);
        UpdateHeatmap();
    }

    public void UpdateHeatmap()
    {

        if (on)
        {
            Color offColor = new Color(0, 0, 1f, .2f); 
            Color onColor = new Color(1f, 0, 0, .7f);
            //Debug.Log("Updating heatmap");
            Color[] colors = new Color[grid.sizeX * grid.sizeY];

            for (int x = 0; x < grid.sizeX; x++)
            {
                for (int y = 0; y < grid.sizeY; y++)
                {
                    Cell cell = grid.gridArray[x, y];
                    if (cell.heat > 0.05)
                    {
                        colors[y * grid.sizeX + x] = Color.Lerp(offColor, onColor, Mathf.Clamp01(cell.heat * 2f));
                    }

                    else
                    {
                        colors[y * grid.sizeX + x] = offColor;
                    }
                   
                }
                
            }

            heatmapTexture.SetPixels(colors);
            heatmapTexture.Apply();
        }

    }

   
       
    private void UpdateOnOff(bool __on)
    {
        on = __on;
        spriteRenderer.enabled = on;
    }
}

