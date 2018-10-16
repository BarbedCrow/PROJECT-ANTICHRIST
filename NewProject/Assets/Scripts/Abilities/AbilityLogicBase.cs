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

    [SerializeField] protected Transform pivot;


    public virtual void Setup(params MonoBehaviour[] args)
    {
        foreach(var arg in args)
        {
            if (damager == null && arg is PropDamager)
            {
                damager = (PropDamager)arg;
            }
        }
    }

    public virtual void Init()
    {

    }

    public void Terminate()
    {
        //Destroy(gameObject);
    }

    public void StartUse()
    {
        Debug.Log("vfdfsgdrger");
    }

    public void StopUse()
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

    protected PropDamager damager;
    protected float currentDamage;

    #endregion

}
