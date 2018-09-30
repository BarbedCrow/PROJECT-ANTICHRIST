using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHold : InputBase
{

    [HideInInspector]
    public UnityEvent OnPressed = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnReleased = new UnityEvent();

    public float duration;


    public override void Init()
    {
        timer = (Timer)gameObject.AddComponent(typeof(Timer));
        timer.Init(duration);
        timer.OnTimersFinished.AddListener(HandleOnFinished);

        base.Init();
    }

    public override void Terminate()
    {
        timer.Terminate();

        base.Terminate();
    }

    #region private

    Timer timer;
    bool isPressed = false;
    bool isUsed = false;

    protected override IEnumerator CheckInputs()
    {
        for (; ; )
        {
            foreach (string key in keys)
            {
                if (Input.GetButtonDown(key))
                {
                    OnPressed.Invoke();
                    isUsed = false;
                    isPressed = true;
                    if (duration > 0)
                    {
                        StartTimer();
                    }
                }

                if (Input.GetButtonUp(key))
                {
                    isPressed = false;
                    OnReleased.Invoke();
                    if (duration == 0)
                    {
                        OnUse.Invoke();
                    }
                    StopTimer();
                }
            }
            yield return base.CheckInputs();
        }
    }

    void StartTimer()
    {
        timer.StartWork();
    }

    void StopTimer()
    {
        timer.StopWork();
    }

    void HandleOnFinished()
    {
        isUsed = true;
        OnUse.Invoke();
    }

    #endregion

}
