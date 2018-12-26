using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityLogicBase : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnAbilityStartUse = new UnityEvent();
    [HideInInspector] public UnityEvent OnAbilityStopReload = new UnityEvent();

    [SerializeField] protected float timeToReload;

    [HideInInspector]
    public UnityEvent OnStartUse = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnStopUse = new UnityEvent();


    public virtual void Setup(params MonoBehaviour[] args)
    {

    }

    public virtual void Init()
    {
        timerToReload = gameObject.AddComponent<Timer>();
        timerToReload.Init(timeToReload);
        canUse = true;
    }

    public virtual void Terminate()
    {
        //Destroy(gameObject);
    }

    public virtual void TryStartUse()
    {
        if (!canUse)
            return;
        StartUse();
    }

    public virtual void StopUse()
    {

    }
    
    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }

    public bool CanUse()
    {
        return canUse;
    }

    #region private

    Timer timerToReload;
    bool canUse;

    protected virtual void StartUse()
    {
        timerToReload.StartWork();
        canUse = false;
        timerToReload.OnTimersFinished.AddListener(StopReloadAbility);
        OnAbilityStartUse.Invoke();
    }

    void StopReloadAbility()
    {
        timerToReload.OnTimersFinished.RemoveListener(StopReloadAbility);
        canUse = true;
        OnAbilityStopReload.Invoke();
    }



    #endregion

}
