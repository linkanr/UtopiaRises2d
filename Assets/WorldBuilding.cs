using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilding : MonoBehaviour
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public float mapfreq;
    public int rows;
    public int collums;

    public float bushMin;
    public float treeMin;
    public float goldMin;
    public float goldMax;
    public float stoneMin;
    public float stoneMax;

    public float threeChance;
    public float bushChance;
    public float goldChance;
    public float stoneChance;

    public List<Transform> bushes;
    public List<Transform> trees;
    public List<Transform> golds;
    public List<Transform> stones;
    void Start()
    {
        rows = (int)maxX - (int)minX;
        collums = (int)maxY - (int)minY;
        float offset = Random.Range(0, 500);
        Debug.Log("rows: " + rows + " collums " + collums);
        for (int i = 0; i<collums; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                float perlin = Mathf.PerlinNoise(((float)i / (float)rows)* mapfreq + offset, ((float)j / (float)collums)*mapfreq+ offset);
                //Debug.Log("perlin is " + perlin);

                if (perlin > treeMin)
                {
                    Vector3 position = new Vector3(i+ (float)minX, j+ (float) minY, 0);
                    float secondPerlin = Mathf.PerlinNoise(((float)i / (float)rows) * mapfreq*150+ offset, ((float)j / (float)collums) * mapfreq*150+ offset);
                    if (secondPerlin < threeChance)
                    {
                        Transform instance = Instantiate(trees[0], position, Quaternion.identity);
                        instance.SetParent(gameObject.transform);
                        continue;
                    }
                    
                    
                }



                if (perlin > bushMin)
                {
                    Vector3 position = new Vector3(i + (float)minX, j + (float)minY, 0);
                    float secondPerlin = Mathf.PerlinNoise(((float)i / (float)rows) * mapfreq * 150+ offset, ((float)j / (float)collums) * mapfreq * 150+ offset);
                    if (secondPerlin < bushChance)
                    {
                        Transform instance = Instantiate(bushes[0], position, Quaternion.identity);
                        instance.SetParent(gameObject.transform);
                        continue;
                    }

                    
                }

                if (perlin > stoneMin && perlin < stoneMax)
                {
                    Vector3 position = new Vector3(i + (float)minX, j + (float)minY, 0);
                    float secondPerlin = Mathf.PerlinNoise(((float)i / (float)rows) * mapfreq * 150+ offset, ((float)j / (float)collums) * mapfreq * 150+ offset);
                    if (secondPerlin < stoneChance)
                    {
                        Transform instance = Instantiate(stones[0], position, Quaternion.identity);
                        instance.SetParent(gameObject.transform);
                        continue;
                    }

                    
                }
                if (perlin > goldMin && perlin < goldMax)
                {
                    Vector3 position = new Vector3(i + (float)minX, j + (float)minY, 0);
                    float secondPerlin = Mathf.PerlinNoise(((float)i / (float)rows) * mapfreq * 150+ offset, ((float)j / (float)collums) * mapfreq * 150+ offset);
                    if (secondPerlin < goldChance)
                    {
                        Transform instance = Instantiate(golds[0], position, Quaternion.identity);
                        instance.SetParent(gameObject.transform);
                        continue;
                    }

                    
                }



            }
        }
    }


}
