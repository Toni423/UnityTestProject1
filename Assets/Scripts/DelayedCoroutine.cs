using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedCoroutine : MonoBehaviour
{
    

    public static IEnumerator delayedCoroutine(float delay, Action action) {
        yield return new WaitForSeconds(delay);

        action.Invoke();
    }

}
