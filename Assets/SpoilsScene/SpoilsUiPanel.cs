using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoilsUiPanel : MonoBehaviour
{
    public RectTransform cardParent;
    public static SpoilsUiPanel Create(RectTransform parent)
    {
        return Instantiate(Resources.Load<SpoilsUiPanel>("SpoilsUiPanel"),parent);
    }
}
