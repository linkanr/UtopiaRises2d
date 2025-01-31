using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<T>:ScriptableObject where T : MonoBehaviour
{
    [HideInInspector]
    public T stateMachine;
    
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();
    public abstract void OnObjectDestroyed();

    private void OnDestroy()
    {
        OnObjectDestroyed();
    }



    public virtual void Init (T parent)
    {
        stateMachine = parent;
        
    }
}
