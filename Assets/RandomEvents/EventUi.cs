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
    }
    private void OnDisable()
    {
        // Unsubscribe when this UI is disabled
        EventActions.OnDisplayEvent -= DisplayEvent;
    }

    private void DisplayEvent(RandomEventBase evt)
    {
        eventTitleText.text = evt.eventName;
        eventDescriptionText.text = evt.description;
        if (evt.Sprite != null)
            Image.sprite = evt.Sprite;
        else
            Image.sprite = Resources.Load<Sprite>("DefaultEventSprite"); // Load a default sprite if none is provided

        // Set up choice buttons based on evt.options
        for (int i = 0; i < evt.options.Length; i++)
        {
            var index = i; // capture the value for this iteration
            var option = evt.options[i];
            ButtonWithDelegate.CreateThis(() => RandomEventManager.instance.OnChoiceSelected(index), choicebuttonParent, option.description);
        }
    }
}
