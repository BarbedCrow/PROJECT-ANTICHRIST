using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagableBase))]
[RequireComponent(typeof(DamagerBase))]
public class UnitBase : MonoBehaviour
{

    public virtual void Init()
    {
        InitComponents();
    }

    public virtual void Terminate()
    {
        TerminateComponents();
        Destroy(gameObject);
    }

    #region private

    DamagableBase propDamagable;
    DamagerBase propDamager;

    GameObject geom;

    protected virtual void InitComponents()
    {
        propDamagable = GetComponent<DamagableBase>();
        propDamagable.Init();
        propDamager = GetComponent<DamagerBase>();
        propDamager.Init();
    }

    protected virtual void TerminateComponents()
    {
        propDamagable.Terminate();
        propDamager.Terminate();
    }

    #endregion
}
