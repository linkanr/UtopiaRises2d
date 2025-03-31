using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/PreScene")]
public class SoBattleScenePreScene : BaseState<BattleSceneStateMachine>

{
    public override void OnStateEnter()
    {


        stateMachine.StartCoroutine(Initilize());







    }
    public override void OnObjectDestroyed()
    {

    }




    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        // DO NOTHING

    }
    
    public IEnumerator MoveToNextState()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.SetState(typeof(SoBattleSceneStateSceneStarting));
    }

    public IEnumerator InitializeEnemyManager()
    {

        yield return EnemyManager.Instance.Init();
        PlayerGlobalsManager.instance.soPlayerBaseBuilding.Init(PlayerGlobalsManager.instance.basePositions);


        yield return new WaitForEndOfFrame();
    }
    public IEnumerator Initilize()
    {
        if (GameManager.instance.currentLevel == null) 
        {
            Debug.LogWarning("No level loaded");
            GameManager.instance.currentLevel = LevelListerManager.instance.GetRandomLevel(0);
        }
           


        Debug.Log("Initilizing");
        yield return GridCellManager.instance.GenerateGrid();
        yield return GridCellManager.instance.InitCellSolver();
        yield return BattleSceneBuilder.instance.BuildScene();
        yield return InitializeEnemyManager();
        
        AstarPath.active.Scan();
        BattleSceneActions.setInfluence(3);
        Instantiate(Resources.Load("mouseDisplayManager") as GameObject, stateMachine.transform);
        stateMachine.StartCoroutine(MoveToNextState());
      
    }
}