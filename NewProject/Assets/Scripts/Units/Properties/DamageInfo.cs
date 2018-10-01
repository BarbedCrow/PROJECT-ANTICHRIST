using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public PropDamagable damagable;
    public PropDamager damager;
    public float damage;
    public float prevHp;
    public float currHp;
    public DamageType damageType;
}

[System.Serializable]
public enum DamageType
{
    PHYSICAL,
    FIRE
}
