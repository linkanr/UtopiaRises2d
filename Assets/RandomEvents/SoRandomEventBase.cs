using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RandomEvents/RandomEvent")]
public class RandomEventBase : SerializedScriptableObject
{
    [Header("Basic Info")]
    public string eventName;
    [TextArea] public string description;

    [Header("Availability Criteria")]
    public int  minDifficulty;
    public int maxDifficulty;
    public Sprite Sprite; // Optional sprite for the event (for UI display)
    public PoliticalAlignment[] allowedAlignments; // if empty, all alignments allowed
    [Header("Event Dependencies")]
    public RandomEventBase requiredPreviousEvent;
    [Header("Event Choices")]
    public EventOption[] options;  // Each event can have multiple choices
    public Dictionary<FactionsEnums,int> requiredFactionsInDeck;

    // Check if this event can occur under given game settings
    public bool MatchesCriteria(int difficulty, PoliticalAlignment alignment)
    {
        if (difficulty < minDifficulty || difficulty > maxDifficulty)
            return false;

        if (requiredPreviousEvent != null && !RandomEventManager.instance.IsEventCleared(requiredPreviousEvent))
            return false;
        if (requiredFactionsInDeck != null && requiredFactionsInDeck.Count > 0)
        {
            foreach (var faction in requiredFactionsInDeck)
            {
                if (!CardManager.instance.GetContainingFactions(faction.Value).Contains(faction.Key))
                    return false;
            }
        }

        if (allowedAlignments != null && allowedAlignments.Length > 0)
        {
            bool alignmentMatch = false;
            foreach (var align in allowedAlignments)
            {
                if (align.galTan == alignment.galTan && align.leftRigt == alignment.leftRigt)
                {
                    alignmentMatch = true;
                    break;
                }
            }
            if (!alignmentMatch) return false;
        }

        return true;
    }


    // Resolve the outcome of a chosen option (returns the outcome for further handling)
    public RandomEventOutcome Resolve(int choiceIndex)
    {
        if (options == null || choiceIndex < 0 || choiceIndex >= options.Length)
        {
            Debug.LogError("Invalid choice index for event: " + eventName);
            return null;
        }
        RandomEventOutcome outcome = options[choiceIndex].outcome;
        // We could apply some immediate effects here or just return the outcome
        return outcome;
    }
}

[System.Serializable]
public class EventOption
{
    public string description;            // Text for this choice
    public RandomEventOutcome outcome;    // Result if this choice is selected
}

[System.Serializable]
public class RandomEventOutcome
{
    [Header("Outcome Result")]
    public string resultText;        // Description of what happened (for UI feedback)
    public int healthChange;         // Example effect: player health modification

    public SoCardBase newCardReward;       // Example: reward a Card (if using a Card system)

    [Header("Follow-up Actions")]
    public bool triggersBattle;
    public string battleName;        // Which battle to trigger (if triggersBattle)
    public bool triggersAnotherEvent;
    public RandomEventBase nextEvent; // Next event to trigger, if any

    public void ApplyEffects()
    {
        // Apply stat or inventory changes to the player
        if (healthChange != 0)
        {
            
        }
   
        if (newCardReward != null)
        {
            CardManager.instance.AddCard(newCardReward);
        }
       
    }
}
