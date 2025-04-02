using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMapButton : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    private void OnDisable()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.RemoveListener(OnClick);
    }
    public void OnClick()
    {
        GlobalActions.OnDebugCreateMap();
    }
}
