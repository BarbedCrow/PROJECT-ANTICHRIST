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
        SubscribeComponents();
    }

    public virtual void Terminate()
    {
        UnsubscribeComponents();
        TerminateComponents();
        Destroy(hpText);
        Destroy(gameObject);
    }

    #region private

    TextMesh hpText;
    protected DamagableBase propDamagable;
    DamagerBase propDamager;

    GameObject geom;

    protected virtual void InitComponents()
    {
        propDamagable = GetComponent<DamagableBase>();
        propDamagable.Init();
        hpText = propDamagable.GetHPText();
        hpText.gameObject.SetActive(true);

        propDamager = GetComponent<DamagerBase>();
        propDamager.Init();
    }

    protected virtual void TerminateComponents()
    {
        propDamagable.Terminate();
        propDamager.Terminate();
    }

    protected virtual void SubscribeComponents()
    {
        propDamagable.OnDie.AddListener(Die);
    }

    protected virtual void UnsubscribeComponents()
    {
        propDamagable.OnDie.RemoveListener(Die);
    }

    protected virtual void Die(DamageInfo info)
    {

    }

    #endregion
}
