using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<SoCardBase> cardsList;
    public static CardManager Instance;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("double trouble");
        }
      
    }



    public void AddCard(SoCardBase soCardBase) // regular bases
    {
        SoCardBase newCard =  Instantiate(soCardBase);
        
        cardsList.Add(newCard);
    }

    public void AddCard(SoCardList soCardBases) // uses the custom list class
    {
        foreach (SoCardBase soCardBase in soCardBases)
        {
            SoCardBase newCard = Instantiate(soCardBase);
            cardsList.Add(newCard);
        }
    }

    internal void GetStartingCards()
    {
        AddCard(Resources.Load("startingCards") as SoCardList);
    }
}
