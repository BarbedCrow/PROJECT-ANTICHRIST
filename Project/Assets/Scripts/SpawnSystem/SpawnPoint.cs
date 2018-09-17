using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]

public class SpawnPoint : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnOpenSpawn = new UnityEvent();

    public float closingTime;

    public void Init()
    {
        timer = GetComponent<Timer>();
        timer.Init(closingTime);
    }

    public bool GetIsReady()
    {
        return isReady;
    }

    public void Terminate()
    {
        Destroy(gameObject);
    }

    public void CloseSpawn()
    {
        isReady = false;
        timer.StartWork();
        timer.OnTimersFinished.AddListener(OpenSpawn);
    }

    #region private

    bool isReady = true;
    Timer timer;

    void OpenSpawn()
    {
        timer.OnTimersFinished.RemoveAllListeners();
        isReady = true;
        OnOpenSpawn.Invoke();
    }

    #endregion
}
