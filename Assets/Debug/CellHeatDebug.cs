using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CellHeatDebug : MonoBehaviour
{
    public static CellHeatDebug instance;
    private GridConstrution grid;
    private Dictionary<Cell, TextMeshProUGUI> heatTextObjects = new Dictionary<Cell, TextMeshProUGUI>();

    public GameObject heatTextPrefab; // Assign a TextMeshPro prefab in Inspector
    public Transform parent;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {


        // Subscribe to cell effect updates
     
    }

    private void OnDestroy()
    {
      
    }

    private void OnEnable()
    {
        TimeActions.OnSecondChange += OnCellEffectUpdated;
    }
    private void OnDisable()
    {
        TimeActions.OnSecondChange -= OnCellEffectUpdated;
    }
    private void CreateHeatTextObjects()
    {
        foreach (Cell cell in grid.gridArray)
        {
            if (cell == null) continue;

            // Instantiate TextMeshProUGUI inside the World Space Canvas
            GameObject textObj = Instantiate(heatTextPrefab, parent);
            TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();

            // Ensure proper TextMeshPro settings
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = 24;  // Adjust for readability
            tmp.color = Color.white;
            tmp.text = ""; // Set empty to avoid "T" issues
            tmp.gameObject.SetActive(false); // Ensure it's hidden at start

            // Convert world position to screen space
            textObj.transform.position = Camera.main.WorldToScreenPoint(cell.worldPosition + Vector3.up * 0.2f);

            heatTextObjects[cell] = tmp;
        }
    }

    private void OnCellEffectUpdated()
    {
        if (grid == null)
        {
            grid = GridCellManager.Instance.gridConstrution;
            CreateHeatTextObjects();

        }



        foreach (Cell cell in grid.gridArray)
        {
            if (cell == null) continue;
            TextMeshProUGUI textMesh = heatTextObjects[cell];
            if (cell.heat >= 0.05f) // Show only if heat is significant
            {
                Debug.Log("Heat is significant");
                textMesh.text = $"{cell.heat:F1}";
                textMesh.gameObject.SetActive(true);
            }
            else if (textMesh.text != "")
            {
                Debug.Log("Heat is not significant removing tesxt" );
                textMesh.text = ""; // Clear text to avoid "T" issue
                textMesh.gameObject.SetActive(false);
                
            }
            Debug.Log("Heat is not significant and cell is inactive");
        }
        


    }
}
