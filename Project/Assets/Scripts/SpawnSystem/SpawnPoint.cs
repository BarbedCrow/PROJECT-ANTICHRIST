using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]

public class SpawnPoint : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnEnabled = new UnityEvent();

    public float closingTime;

    public void Init()
    {
        timer = GetComponent<Timer>();
        timer.Init(closingTime);
    }

    public bool GetIsEnabled()
    {
        return isEnabled;
    }

    public void Terminate()
    {
        Destroy(gameObject);
    }

    public void Disable()
    {
        isEnabled = false;
        timer.StartWork();
        timer.OnTimersFinished.AddListener(Enabled);
    }

    #region private

    bool isEnabled = true;
    Timer timer;

    void Enabled()
    {
        timer.OnTimersFinished.RemoveAllListeners();
        isEnabled = true;
        OnEnabled.Invoke();
    }

    #endregion
}
