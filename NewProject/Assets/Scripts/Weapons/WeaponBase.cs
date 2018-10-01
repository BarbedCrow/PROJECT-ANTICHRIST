using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBase : MonoBehaviour
{

    [HideInInspector]
    public UnityEvent OnAttackStared = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnAttackStopped = new UnityEvent();

    [SerializeField] protected Transform pivot;

    [Header("Characteristics")]
    [SerializeField] protected float damage;

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
        currentDamage = damage;

        Disable();
    }

    public void Terminate()
    {
        //Destroy(gameObject);
    }

    public void RequestStartAttack()
    {
        RequestStartAttackInternal();
    }

    public void RequestStopAttack()
    {
        RequestStopAttackInternal();
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

    protected virtual void RequestStartAttackInternal()
    {

    }

    protected virtual void RequestStopAttackInternal()
    {

    }

    #endregion

}
