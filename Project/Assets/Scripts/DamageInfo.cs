using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public DamagableBase damagable;
    public DamagerBase damager;
    public float damage;
    public DamageType damageType;
}

[System.Serializable]
public enum DamageType
{
    PHYSICAL,
    FIRE
}
