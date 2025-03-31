using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBaseInfoBox : MonoBehaviour
{
    public List<Sprite> enemySprites;
    public GameObject enemyVizGO;
    public EnemySpawner spawner;
    public Transform canvas;
    public List<GameObject> ActiveViz;
    public TextMeshProUGUI totalEnemyCount;
   

    private void Start()
    {
        spawner = GetComponent<EnemyBase>().spawner;
        spawner.OnEnemySpawned += UpdateEnemySprite;

        UpdateEnemySprite();


    }
    private void OnDisable()
    {
        spawner.OnEnemySpawned -= UpdateEnemySprite;

    }



    private void UpdateEnemySprite()
    {
        foreach (GameObject viz in ActiveViz)
        {
            Destroy(viz);
        }
        Dictionary<Sprite,int>  enemies = spawner.GetEnemieInNextWave();
        foreach (KeyValuePair<Sprite,int> enemy in enemies)
        {
            if (enemy.Value != 0)
            {
                GameObject vizGo = Instantiate(enemyVizGO, canvas);
                ActiveViz.Add(vizGo);
                EnemyBaseEnemyUiVisualiser enemyBaseEnemyUi = vizGo.GetComponent<EnemyBaseEnemyUiVisualiser>();
                enemyBaseEnemyUi.enemyImage.sprite = enemy.Key;
                enemyBaseEnemyUi.enemyCount.text = enemy.Value.ToString();
            }
           
        }
        if (spawner.enemyBase.permanent)
        {
            totalEnemyCount.text = "∞";
            
        }
        else
        {
            totalEnemyCount.text = spawner.GetAllEnemies().Count.ToString();
        }
      
    }
}
