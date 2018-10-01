using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{

    [HideInInspector]
    public UnityEvent OnTimersFinished = new UnityEvent();

    public void Init(float duration)
    {
        this.duration = duration;
    }

    public void Terminate()
    {
        StopWork();
    }

    public void StartWork()
    {
        isRunning = true;
        timeToStop = Time.time + duration;
        StartCoroutine(CHECK_TIME_COROUTINE);
    }

    public void StopWork()
    {
        isRunning = false;
        timeToStop = 0;
        StopCoroutine(CHECK_TIME_COROUTINE);
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    #region private

    const string CHECK_TIME_COROUTINE = "CheckTime";

    float duration;
    float timeToStop;

    bool isRunning = false;

    IEnumerator CheckTime()
    {
        for (; ; )
        {
            if (Time.time >= timeToStop)
            {
                StopWork();
                OnTimersFinished.Invoke();
            }
            yield return new WaitForFixedUpdate();
        }
    }

    #endregion
}
