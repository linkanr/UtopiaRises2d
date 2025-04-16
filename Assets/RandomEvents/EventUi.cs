// (Pseudo-code) Update UI elements with event details
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventUi : MonoBehaviour
{
    public TextMeshProUGUI eventTitleText;
    public TextMeshProUGUI eventDescriptionText;
    public RectTransform choicebuttonParent;
    public Image Image;
    private void OnEnable()
    {
        // Subscribe to the event when this UI is enabled
        EventActions.OnDisplayEvent += DisplayEvent;
        EventActions.OnDisplayEventOutcome += DisplayEventOutcome;
    }
    private void OnDisable()
    {
        // Unsubscribe when this UI is disabled
        EventActions.OnDisplayEvent -= DisplayEvent;
        EventActions.OnDisplayEventOutcome -= DisplayEventOutcome;
    }

    private void DisplayEvent(RandomEventBase evt)
    {
        eventTitleText.text = GameStringParser.Parse(evt.eventName);
        eventDescriptionText.text = GameStringParser.Parse( evt.description);
        if (evt.Sprite != null)
            Image.sprite = evt.Sprite;
        else
            Image.sprite = Resources.Load<Sprite>("DefaultEventSprite"); // Load a default sprite if none is provided

        // Set up choice buttons based on evt.options
        for (int i = 0; i < evt.options.Length; i++)
        {
            var index = i; // capture the value for this iteration
            var option = evt.options[i];
            ButtonWithDelegate.CreateThis(() => RandomEventManager.instance.OnChoiceSelected(index), choicebuttonParent, option.description, false);
        }
    }
    private void DisplayEventOutcome(RandomEventOutcome outcome)
    {
        // Clear buttons
        foreach (Transform child in choicebuttonParent)
            Destroy(child.gameObject);

        // Update description text with outcome result
        eventDescriptionText.text = GameStringParser.Parse(outcome.resultText);

        // Show continue button using ButtonWithDelegate
        ButtonWithDelegate.CreateThis(() => RandomEventManager.instance.ApplyOutcome(outcome), choicebuttonParent, "Continue");
    }
}
