using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public List<RandomEventBase> allEvents;  // all event ScriptableObjects available
    private RandomEventBase currentEvent;
    public static RandomEventManager instance; // Singleton instance
    private HashSet<string> clearedEventNames = new ();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of RandomEventManager detected!");
        }
        allEvents = new List<RandomEventBase>();
        LoadAllEventsFromResources();
    }
    private void OnEnable()
    {
        GlobalActions.OnEventSceneLoaded += Init;
    }
    private void OnDisable()
    {
        GlobalActions.OnEventSceneLoaded -= Init;
    }
    void Init()
    {
        // Automatically begin an event when the scene loads
        TriggerRandomEvent();
    }
    private void LoadAllEventsFromResources()
    {
        // Loads all RandomEventBase ScriptableObjects from Resources folder
        Debug.Log("Loading all random events from Resources...");
        allEvents.Clear();
        var loaded = Resources.LoadAll<RandomEventBase>("RandomEvents");
        allEvents.AddRange(loaded);
        Debug.Log($"Loaded {allEvents.Count} random events from Resources.");
    }
    public void TriggerRandomEvent()
    {
        // 1. Filter events by difficulty and alignment
        int diff = 0; // default difficulty level for debugging 
        if (GameManager.instance.currentLevel != null)
        {
             diff = GameManager.instance.currentLevel.levelDiffculty; // example difficulty level
        }

        PoliticalAlignment align = PlayerGlobalsManager.instance.playerGlobalVariables.politicalAlignment;
        var validEvents = allEvents.FindAll(evt => evt.MatchesCriteria(diff, align));
        if (validEvents.Count == 0)
        {
            Debug.LogWarning("No random events match the criteria!");
            return;
        }
        // 2. Randomly pick one of the valid events
        currentEvent = validEvents[Random.Range(0, validEvents.Count)];
        // 3. Load/display the event
        EventActions.OnDisplayEvent(currentEvent);
    }



    public void OnChoiceSelected(int choiceIndex)
    {
        // Player made a choice, resolve outcome
        RandomEventOutcome outcome = currentEvent.Resolve(choiceIndex);
        ApplyOutcome(outcome);
    }

    private void ApplyOutcome(RandomEventOutcome outcome)
    {
        outcome.ApplyEffects();  // apply stat changes, rewards, etc.
        if (outcome.triggersBattle)
        {
            GameManager.instance.currentLevel = LevelListerManager.instance.GetLevelByName(outcome.battleName);
            GameManager.instance.LoadNextBattleScene();
        }
        else if (outcome.triggersAnotherEvent)
        {
            // Support chaining events (e.g., a mini scene within the event)
            currentEvent = outcome.nextEvent;
            EventActions.OnDisplayEvent(currentEvent);
            return; // don't exit yet, continue with new event
        }
        else 
        {
            GlobalActions.GoBackToMap();
        }
        // If we reach here, the event is fully resolved (no sub-battle or chaining)
        EndEvent();
    }
    public bool IsEventCleared(RandomEventBase evt)
    {
        return evt != null && clearedEventNames.Contains(evt.eventName);
    }

    public void MarkEventCleared(RandomEventBase evt)
    {
        if (evt != null && !clearedEventNames.Contains(evt.eventName))
        {
            clearedEventNames.Add(evt.eventName);
            Debug.Log($"Event marked as cleared: {evt.eventName}");
        }
    }

    private void EndEvent()
    {
        MarkEventCleared(currentEvent);

        // Clean up event UI if needed, then signal state machine to go back to map
        // GameManager.Instance.ChangeState(GameState.Map);
    }
}
