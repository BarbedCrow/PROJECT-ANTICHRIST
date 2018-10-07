using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropWeaponUserRange : PropWeaponUserBase
{

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        PoolBase projectilesPool = null;
        foreach (MonoBehaviour arg in args)
        {
            if (projectilesPool == null && arg is PoolBase)
            {
                projectilesPool = (PoolBase)arg;
            }
        }

        foreach(WeaponBase weapon in weapons)
        {
            weapon.Setup(projectilesPool);
        }
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        var rangeWeapon = (WeaponRange)currentWeapon;
        rangeWeapon.OnShootEmpty.AddListener(RequestReload);
    }

    public WeaponBase GetWeapon()
    {
        return currentWeapon;
    }

    #region private

    protected void RequestReload()
    {
        var rangeWeapon = (WeaponRange)currentWeapon;
        rangeWeapon.TryReload();
    }

    #endregion

}
