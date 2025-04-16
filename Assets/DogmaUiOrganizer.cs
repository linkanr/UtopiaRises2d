using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogmaUiOrganizer : MonoBehaviour
{
    public GameObject dogmaLayoutPrefab;


    public void Init()
    {
        foreach (Dogma dogma in DogmaManager.instance.GetAllActiveDogmas())
        {
            GameObject dogmaLayout = Instantiate(dogmaLayoutPrefab, transform);
            
        }
    }
}
