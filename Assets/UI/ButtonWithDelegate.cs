using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithDelegate : MonoBehaviour
{

    private Button button;
    public static void CreateThis(Action actionToPreform, RectTransform parent, string text)
    {
        //Debug.Log(actionToPreform.ToString());
        GameObject preFab = Resources.Load("ButtonDelegate") as GameObject;
        
        
        

        GameObject instaciated =  Instantiate(preFab, parent);
        ButtonWithDelegate buttonWithDelegate = instaciated.GetComponent<ButtonWithDelegate>();
        instaciated.GetComponentInChildren<TextMeshProUGUI>().text = text;
        buttonWithDelegate.button = buttonWithDelegate.GetComponentInChildren<Button> ();
        buttonWithDelegate.button.onClick.AddListener(new UnityEngine.Events.UnityAction(actionToPreform));

        buttonWithDelegate.button.onClick.AddListener(() => Destroy(instaciated));
        buttonWithDelegate.button.onClick.AddListener(() => buttonWithDelegate.button.onClick.RemoveAllListeners());
    }

}
