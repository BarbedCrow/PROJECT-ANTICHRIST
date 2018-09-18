using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagerBase))]
public class WeaponBase : MonoBehaviour
{
    public string uid;
    public float damage;


    public virtual void Init()
    {
        propDamager = GetComponent<DamagerBase>();
        propDamager.Init();
    }

    public virtual void Terminate()
    {
        Destroy(gameObject);
    }

    #region private

    protected DamagerBase propDamager;

    #endregion
}
