using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalsManager : MonoBehaviour
{
    public SoPlayerBaseBuilding soPlayerBaseBuilding;
    public static PlayerGlobalsManager instance;
    public Vector3 basePositions;
    public int influence { get; private set; }
    public PlayerGlobalVariables playerGlobalVariables;
    public int influenceEachTurn { get; private set; }

    public int cardAmount { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("two global managers");
        }
        playerGlobalVariables = new PlayerGlobalVariables();
        playerGlobalVariables.PoliticalSystem.SetCompass(galTan: 0, leftRight: 0);

    }

    private void OnEnable()
    {

        BattleSceneActions.setInfluence += SetInfluence; // SET influence each turn Triggered by statemachine 
        GlobalActions.BattleSceneCompleted += playerGlobalVariables.ClearBattleModifiers;

    }
    private void OnDisable()
    {

        BattleSceneActions.setInfluence -= SetInfluence;
        GlobalActions.BattleSceneCompleted -= playerGlobalVariables.ClearBattleModifiers;
    }
    private void Start()
    {

        influenceEachTurn = 3;
        cardAmount = 5;
    }
    private void SetInfluence(int obj)
    {
        influence = obj;
        BattleSceneActions.OnInfluenceChanged(influence);
    }
    public void AddInfluence(int amount)
    {
        influence += amount;
        BattleSceneActions.OnInfluenceChanged(influence);
    }

}
