using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LateUpdateAstar:MonoBehaviour
{
    private void OnEnable()
    {
        BattleSceneActions.OnUpdateBounds += UpdateBound;
    }
    private void OnDisable()
    {
        BattleSceneActions.OnUpdateBounds -= UpdateBound;
    }

    public void UpdateBound(Bounds bounds)
    {
        StartCoroutine(UpdateBoundsCo(bounds));

        
    }

    private IEnumerator UpdateBoundsCo(Bounds bounds)
    {
        yield return Wait();
        yield return UpdateAstarRoutine(bounds);
    }

    private IEnumerator UpdateAstarRoutine(Bounds bounds)
    {
        AstarPath.active.UpdateGraphs(bounds);
        yield return null;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
    }
}