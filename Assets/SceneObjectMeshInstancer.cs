using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SceneObjectMeshInstancer : MonoBehaviour
{
    public GameObject mesh;
    [SerializeField] float scaleMin;
    [SerializeField] float scaleMax;
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(4, 7);
        for (int i =0; i < r; i++) 
        {

            float zrotation = Random.Range(0, 360);
 
            Vector3 pos = WorldSpaceUtils.GetRandomDirection() * Random.Range(0.2f, 0.8f);
            pos.z = 0;
            GameObject tree = Instantiate(mesh, transform.position + pos, Quaternion.identity);
            tree.transform.parent = transform;


            tree.transform.Rotate(0, 0, zrotation);
            float scale = Random.Range(scaleMin, scaleMax);
            tree.transform.localScale = new Vector3(scale, scale, scale);

            tree.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
