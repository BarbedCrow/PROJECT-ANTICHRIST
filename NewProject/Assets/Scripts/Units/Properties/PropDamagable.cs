using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropDamagable : PropBase
{

    public EventOnDie OnDie = new EventOnDie();
    public EventOnGotDamage OnGotDamage = new EventOnGotDamage();
    [HideInInspector]public UnityEvent OnHealthChanged = new UnityEvent();

    [SerializeField] float health;

    public override void Init(Transform owner)
    {
        base.Init(owner);

        currentHp = health;
    }

    public float GetCurrentHealth()
    {
        return currentHp;
    }

    public float GetMaxHealth()
    {
        return health;
    }

    public void GetDamage(DamageInfo info)
    {
        info.prevHp = currentHp;
        currentHp -= info.damage;
        info.currHp = currentHp;
        OnGotDamage.Invoke(info);

        if (currentHp <= 0)
        {   
            if(info.damager.GetComponentInParent<Player>())
                info.damager.KillDamagable(info);
            Die(info);
        }

        OnHealthChanged.Invoke();
    }

    public void RestoreHP(float hpToRestore)
    {
        if(currentHp + hpToRestore <= health)
        {
            currentHp += hpToRestore;
        }else
        {
            currentHp = health;
        }

        OnHealthChanged.Invoke();
    }

    #region private

    float currentHp;

    #endregion

    void Die(DamageInfo info)
    {
        OnDie.Invoke(info);
    }

}
