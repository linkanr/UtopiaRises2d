using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Playables;


public abstract class BaseStateMachine<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    public List<BaseState<T>> states;
    public List<BaseState<T>> instanciatedStates;
    public readonly Dictionary<Type, BaseState<T>> _stateByType = new();
    public BaseState<T> activeState;
   
    protected bool stopped = false;
    public void Stopp()
    {
        activeState.OnStateExit();
        stopped = true;
    }

    protected virtual void Start()
    {
        foreach (BaseState<T> state in states)
        {
            instanciatedStates.Add(Instantiate(state));

        }

        foreach (BaseState<T> state in instanciatedStates)
        {
            _stateByType.Add(state.GetType(), state);
            
        }
        Init();
        SetState(instanciatedStates[0].GetType());



    }
    /// <summary>
    /// This is called on start and just adds additions to the start function
    /// </summary>
    protected abstract void Init();//


    public void SetState(Type newStateType)
    {
       
        if (activeState != null)
        {

            activeState.OnStateExit();
        }
        //Debug.Log("to new state " + newStateType.Name);
        activeState = _stateByType[newStateType];
        activeState.Init(GetComponent<T>());
        activeState.OnStateEnter();
    }
    protected void Update()
    {
        if (stopped) return;
        activeState.OnStateUpdate();
        
    }

    public Type GetActiveStateType() 
    {
        return activeState.GetType();
    }
    public BaseState<T> GetActiveState()
    {
        return _stateByType[activeState.GetType()];
    }
    protected virtual void OnDestroy()
    {
        if (activeState != null)
        {
            activeState.OnStateExit();
        }
        // Notify all states about destruction
        foreach (var state in instanciatedStates)
        {
            state.OnObjectDestroyed();
            Destroy(state); // Destroy ScriptableObject instances
        }

        // Clear lists and dictionaries
        instanciatedStates.Clear();
        _stateByType.Clear();
        stopped = true;
    }

}
