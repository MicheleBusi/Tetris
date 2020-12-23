using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitThenPerformAction : MonoBehaviour
{
    [SerializeField] UnityEvent actionToPerform = default;

    public void PerformActionWithDelay(float delay = 1f)
    {
        this.Wait(delay, actionToPerform);
    }

}
