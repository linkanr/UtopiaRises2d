using System;
using UnityEngine;

public class TimeLimterSceneObject : MonoBehaviour
{
     
    private float timeToLiveInternal;


    public Action OnLifeEnds;
    public TimeStruct timeToLive { get { return TimeCalc.TimeToTimeStruct(timeToLiveInternal); }  }
    private void OnEnable()
    {
        BattleSceneActions.GlobalTimeChanged += TimeTick;
    }
    private void OnDisable()
    {
        BattleSceneActions.GlobalTimeChanged -= TimeTick;
    }


    public void Init(IHasLifeSpan lifeSpanObject)
    {
        timeToLiveInternal = TimeCalc.TimeStructToTime(lifeSpanObject.getBirthLifeSpan());
        OnLifeEnds += lifeSpanObject.OnLifeUp;
        

    }
    private void TimeTick(BattleSceneTimeArgs args)
    {
        
        timeToLiveInternal -= args.deltaTime;
        if (timeToLiveInternal < 0) 
        {
            OnLifeEnds();
        }

    }


}