using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class RandomEventCreator : EditorWindow
{
    private string eventName = "NewRandomEvent";
    private string eventDescription = "";
    private int minDifficulty = 0;
    private int maxDifficulty = 2;
    private Sprite eventSprite;

    private List<PoliticalAlignment> allowedAlignments = new List<PoliticalAlignment>();
    private RandomEventBase requiredPreviousEvent;

    private List<FactionsEnums> factionKeys = new List<FactionsEnums>();
    private List<int> factionValues = new List<int>();

    private Vector2 scroll;

    [MenuItem("Tools/Events/Create Random Event")]
    public static void ShowWindow()
    {
        GetWindow<RandomEventCreator>("Create Random Event");
    }

    private void OnGUI()
    {
        scroll = EditorGUILayout.BeginScrollView(scroll);

        GUILayout.Label("Create New Random Event", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        eventName = EditorGUILayout.TextField("Event Name", eventName);

        GUILayout.Label("Description");
        GUIStyle textAreaStyle = new GUIStyle(EditorStyles.textArea);
        textAreaStyle.wordWrap = true;
        eventDescription = EditorGUILayout.TextArea(eventDescription, textAreaStyle, GUILayout.MinHeight(60));

        EditorGUILayout.Space();

        minDifficulty = EditorGUILayout.IntField("Min Difficulty", minDifficulty);
        maxDifficulty = EditorGUILayout.IntField("Max Difficulty", maxDifficulty);
        eventSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", eventSprite, typeof(Sprite), false);

        EditorGUILayout.Space();
        GUILayout.Label("Allowed Alignments", EditorStyles.boldLabel);

        int alignmentCount = Mathf.Max(0, EditorGUILayout.IntField("Count", allowedAlignments.Count));
        while (allowedAlignments.Count < alignmentCount)
            allowedAlignments.Add(new PoliticalAlignment());
        while (allowedAlignments.Count > alignmentCount)
            allowedAlignments.RemoveAt(allowedAlignments.Count - 1);

        for (int i = 0; i < allowedAlignments.Count; i++)
        {
            GUILayout.Label($"Alignment {i + 1}", EditorStyles.miniBoldLabel);

            var align = allowedAlignments[i];
            align.galTan = (galTan)EditorGUILayout.EnumPopup("GalTan", align.galTan);
            align.leftRight = (leftRigt)EditorGUILayout.EnumPopup("LeftRight", align.leftRight);
            allowedAlignments[i] = align;

            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();
        GUILayout.Label("Required Previous Event", EditorStyles.boldLabel);
        requiredPreviousEvent = (RandomEventBase)EditorGUILayout.ObjectField("Previous Event", requiredPreviousEvent, typeof(RandomEventBase), false);

        EditorGUILayout.Space();
        GUILayout.Label("Required Factions In Deck", EditorStyles.boldLabel);

        int factionCount = Mathf.Max(0, EditorGUILayout.IntField("Count", factionKeys.Count));
        while (factionKeys.Count < factionCount)
        {
            factionKeys.Add(FactionsEnums.Neutral);
            factionValues.Add(1);
        }
        while (factionKeys.Count > factionCount)
        {
            factionKeys.RemoveAt(factionKeys.Count - 1);
            factionValues.RemoveAt(factionValues.Count - 1);
        }

        for (int i = 0; i < factionKeys.Count; i++)
        {
            factionKeys[i] = (FactionsEnums)EditorGUILayout.EnumPopup($"Faction {i + 1}", factionKeys[i]);
            factionValues[i] = EditorGUILayout.IntField("Required Count", factionValues[i]);
        }

        EditorGUILayout.Space();

        // Buttons
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Event"))
        {
            CreateEventAsset();
        }

        if (GUILayout.Button("Clear Fields"))
        {
            ClearFields();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();
    }

    private void CreateEventAsset()
    {
        string folderPath = "Assets/RandomEvents/SOs/Resources";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            AssetDatabase.Refresh();
        }

        var newEvent = ScriptableObject.CreateInstance<RandomEventBase>();
        newEvent.eventName = eventName;
        newEvent.description = eventDescription;
        newEvent.minDifficulty = minDifficulty;
        newEvent.maxDifficulty = maxDifficulty;
        newEvent.Sprite = eventSprite;
        newEvent.allowedAlignments = allowedAlignments.ToArray();
        newEvent.requiredPreviousEvent = requiredPreviousEvent;

        var factionDict = new Dictionary<FactionsEnums, int>();
        for (int i = 0; i < factionKeys.Count; i++)
        {
            if (!factionDict.ContainsKey(factionKeys[i]))
            {
                factionDict.Add(factionKeys[i], factionValues[i]);
            }
        }
        newEvent.requiredFactionsInDeck = factionDict;

        string assetPath = $"{folderPath}/{eventName}.asset";
        AssetDatabase.CreateAsset(newEvent, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Random Event '{eventName}' created at {assetPath}");
        Selection.activeObject = newEvent;
    }

    private void ClearFields()
    {
        eventName = "NewRandomEvent";
        eventDescription = "";
        minDifficulty = 0;
        maxDifficulty = 2;
        eventSprite = null;
        allowedAlignments.Clear();
        requiredPreviousEvent = null;
        factionKeys.Clear();
        factionValues.Clear();
    }
}
