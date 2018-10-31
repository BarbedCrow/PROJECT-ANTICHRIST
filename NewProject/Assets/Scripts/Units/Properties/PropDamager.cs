using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDamager : PropBase
{
    [HideInInspector] public EventOnKillEnemy onKillEnemy = new EventOnKillEnemy();

    public void DoDamage(DamageInfo info)
    {
        info.damagable.GetDamage(info);
    }

    public void KillDamagable(DamageInfo info)
    {
        onKillEnemy.Invoke(info);
    }
}
