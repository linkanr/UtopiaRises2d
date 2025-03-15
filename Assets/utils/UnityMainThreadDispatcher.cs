using System;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static readonly Queue<Action> executionQueue = new Queue<Action>();

    private static UnityMainThreadDispatcher instance;

    public static void Enqueue(Action action)
    {
        if (action == null) return;

        lock (executionQueue)
        {
            Debug.Log("Enqueueing action");
            executionQueue.Enqueue(action);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        while (executionQueue.Count > 0)
        {
            Action action;
            lock (executionQueue)
            {
                action = executionQueue.Dequeue();
            }
            action?.Invoke();
        }
    }
}
