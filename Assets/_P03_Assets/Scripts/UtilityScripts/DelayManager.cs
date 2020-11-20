using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayManager : MonoBehaviour
{
    public Coroutine DelayAction(Action action, float delayDuration)
    {
        return StartCoroutine(DelayActionRoutine(action, delayDuration));
    }

    private IEnumerator DelayActionRoutine(Action action, float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);
        action();
    }
}
