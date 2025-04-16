using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DogmaManager : MonoBehaviour
{
    public static DogmaManager instance;

    [BoxGroup("Artifact Prefab")]
    [Required]
    public GameObject dogmaPrefab;

    [BoxGroup("Loaded Artifacts (auto)")]
    [ReadOnly, ShowInInspector]
    public Dictionary<string, SoDogmaBase> dogmaLookup = new();
    private SoDogmaBase[] loadedDogmas;
    private Dictionary<string, Dogma> activeDogmas = new();


    
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
        Debug.LogWarning($"Artifact '{dogmaName}' not found in active artifacts.");
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
        loadedDogmas = Resources.LoadAll<SoDogmaBase>("Artifacts");

        foreach (var artifact in loadedDogmas)
        {
            string key = artifact.name;
            if (!dogmaLookup.ContainsKey(key))
                dogmaLookup.Add(key, artifact);
            else
                Debug.LogWarning($"Duplicate artifact key found: {key}");
        }

        Debug.Log($"[ArtifactManager] Loaded {dogmaLookup.Count} artifacts from Resources/Artifacts.");
    }

    /// <summary>
    /// Create an artifact by name.
    /// </summary>
    public static void Create(string artifactName)
    {
        if (instance == null)
        {
            Debug.LogError("ArtifactManager is not initialized.");
            return;
        }

        if (!instance.dogmaLookup.TryGetValue(artifactName, out var artifactSO))
        {
            Debug.LogError($"Artifact '{artifactName}' not found.");
            return;
        }

        instance.CreateArtifact(artifactName, artifactSO);
    }

    private void CreateArtifact(string name, SoDogmaBase soArtifactBase)
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

        GameObject artifactGO = Instantiate(dogmaPrefab);
        Dogma artifact = artifactGO.GetComponent<Dogma>();

        if (artifact == null)
        {
            Debug.LogError("Artifact prefab missing Artifact script.");
            Destroy(artifactGO);
            return;
        }

        artifact.Init(soArtifactBase);
        activeDogmas.Add(name, artifact);
    }

    public static void Destroy(string artifactName)
    {
        if (instance == null || !instance.activeDogmas.TryGetValue(artifactName, out var artifact))
            return;

        Destroy(artifact.gameObject);
        instance.activeDogmas.Remove(artifactName);
    }

    [Button("Destroy All Artifacts")]
    public void DestroyAllArtifacts()
    {
        foreach (var artifact in activeDogmas.Values)
        {
            if (artifact != null)
                Destroy(artifact.gameObject);
        }

        activeDogmas.Clear();
    }
}
