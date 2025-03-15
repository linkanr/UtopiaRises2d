using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using UnityEngine.UI;

public class CardSetBg : SerializedMonoBehaviour

{
    public Dictionary<CardType, Sprite> CardTypeBgs;

    public void Init(CardType cardType)
    {
        GetComponent<Image>().sprite = CardTypeBgs[cardType];
    }
}
