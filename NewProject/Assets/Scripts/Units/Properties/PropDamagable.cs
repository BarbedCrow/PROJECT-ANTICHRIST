using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDamagable : PropBase
{

    public EventOnDie OnDie = new EventOnDie();
    public EventOnGotDamage OnGotDamage = new EventOnGotDamage();

    [SerializeField] float health;

    public override void Init(Transform owner)
    {
        base.Init(owner);

        currentHp = health;
    }

    public float GetHealth()
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
            Die(info);
        }

    }

    #region private

    float currentHp;

    #endregion

    void Die(DamageInfo info)
    {
        OnDie.Invoke(info);
    }

}
