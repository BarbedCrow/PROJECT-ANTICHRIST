using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDamager : PropBase
{

    public void DoDamage(DamageInfo info)
    {
        info.damagable.GetDamage(info);
    }
}
