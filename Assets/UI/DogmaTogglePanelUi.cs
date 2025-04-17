using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DogmaTogglePanelUi : MonoBehaviour
{
    [SerializeField] private GameObject togglePrefab;
    [SerializeField] private Transform contentParent;

    private Dictionary<string, Toggle> toggleMap = new();

    private void Start()
    {
        GenerateToggleList();
    }

    private void GenerateToggleList()
    {
        List<SoDogmaBase> allDogmas = DogmaManager.instance.GetAllDogmas();

        foreach (SoDogmaBase dogma in allDogmas)
        {
            GameObject toggleGO = Instantiate(togglePrefab, contentParent);
            Toggle toggle = toggleGO.GetComponent<Toggle>();
            TMP_Text label = toggleGO.GetComponentInChildren<TMP_Text>();
            if (label != null)
                label.text = !string.IsNullOrEmpty(dogma.displayName) ? dogma.displayName : dogma.name;
            else
                Debug.LogWarning($"Toggle prefab is missing a TMP_Text for dogma {dogma.name}");

            label.text = !string.IsNullOrEmpty(dogma.displayName) ? dogma.displayName : dogma.name;

            toggle.isOn = DogmaManager.instance.GetAllActiveDogmas()
                            .Exists(x => x.name == dogma.name); // match internal key

            string dogmaName = dogma.name; // this is the key used in DogmaManager
            toggle.onValueChanged.AddListener(isOn =>
            {
                if (isOn)
                    DogmaManager.Create(dogmaName);
                else
                    DogmaManager.Destroy(dogmaName);
            });

            toggleMap.Add(dogmaName, toggle);
        }
    }
}
