using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityLogicBase : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnStartUse = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnStopUse = new UnityEvent();


    public virtual void Setup(params MonoBehaviour[] args)
    {

    }

    public virtual void Init()
    {

    }

    public virtual void Terminate()
    {
        //Destroy(gameObject);
    }

    public virtual void StartUse()
    {

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

    #region private

    #endregion

}
