using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DogmaManager : SerializedMonoBehaviour
{
    public static DogmaManager instance;

    [BoxGroup("Dogma Prefab")]
    [Required]
    public GameObject dogmaPrefab;

    [BoxGroup("Loaded Dogmas (auto)")]
    [ReadOnly, ShowInInspector]
    public Dictionary<string, SoDogmaBase> dogmaLookup = new();
    private SoDogmaBase[] loadedDogmas;
    public Dictionary<string, Dogma> activeDogmas = new();


    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        LoadAllDogmasFromResources();
    }
    public SoDogmaBase GetDogmaBase(string dogmaName)
    {
        if (dogmaLookup.TryGetValue(dogmaName, out var artifact))
            return artifact;
        Debug.LogWarning($"Dogma '{dogmaName}' not found in active artifacts.");
        return null;
    }
    public List<SoDogmaBase> GetAllDogmas()
    {
        return new List<SoDogmaBase>(loadedDogmas);
    }
    public List<Dogma> GetAllActiveDogmas()
    {
        return new List<Dogma>(activeDogmas.Values);
    }
    private void LoadAllDogmasFromResources()
    {
        dogmaLookup.Clear();
        loadedDogmas = Resources.LoadAll<SoDogmaBase>("Dogmas");

        foreach (var artifact in loadedDogmas)
        {
            string key = artifact.name;
            if (!dogmaLookup.ContainsKey(key))
                dogmaLookup.Add(key, artifact);
            else
                Debug.LogWarning($"Duplicate dogma key found: {key}");
        }

        Debug.Log($"[ArtifactManager] Loaded {dogmaLookup.Count} dogmas from Resources/Artifacts.");
    }

    /// <summary>
    /// Create an dogma by name.
    /// </summary>
    public static void Create(string dogmaName)
    {
        if (instance == null)
        {
            Debug.LogError("DogmaManager is not initialized.");
            return;
        }

        if (!instance.dogmaLookup.TryGetValue(dogmaName, out var artifactSO))
        {
            Debug.LogError($"Dogma '{dogmaName}' not found.");
            return;
        }

        instance.CreateArtifact(dogmaName, artifactSO);
    }

    private void CreateArtifact(string name, SoDogmaBase soDogmaName)
    {
        if (dogmaPrefab == null)
        {
            Debug.LogError("Artifact prefab not assigned!");
            return;
        }

        if (activeDogmas.ContainsKey(name))
        {
            Debug.LogWarning($"Artifact '{name}' is already active.");
            return;
        }

        GameObject DogmaGo = Instantiate(dogmaPrefab, transform);
        Dogma artifact = DogmaGo.GetComponent<Dogma>();

        if (artifact == null)
        {
            Debug.LogError("Dogma prefab missing Artifact script.");
            Destroy(DogmaGo);
            return;
        }

        artifact.Init(soDogmaName);
        activeDogmas.Add(name, artifact);
    }

    public static void Destroy(string dogmaName)
    {
        if (instance == null || !instance.activeDogmas.TryGetValue(dogmaName, out var artifact))
            return;

        Destroy(artifact.gameObject);
        instance.activeDogmas.Remove(dogmaName);
    }

    [Button("Destroy All Dogmas")]
    public void DestroyAllDogmas()
    {
        foreach (var artifact in activeDogmas.Values)
        {
            if (artifact != null)
                Destroy(artifact.gameObject);
        }

        activeDogmas.Clear();
    }
}
