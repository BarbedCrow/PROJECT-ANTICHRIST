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

        foreach (WeaponBase weapon in weapons)
        {
            var rangeWeapon = (WeaponRange)weapon;
            rangeWeapon.Setup(projectilesPool);
        }
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        foreach(var weapon in weapons)
        {
            var range = (WeaponRange)weapon;
            range.OnShootEmpty.AddListener(RequestReload);
        }
    }

    public override void Terminate()
    {
        foreach (var weapon in weapons)
        {
            var range = (WeaponRange)weapon;
            range.OnShootEmpty.RemoveListener(RequestReload);
        }

        base.Terminate();
    }

    public WeaponBase GetWeapon()
    {
        return currentWeapon;
    }

    #region private

    protected override void SwapWeapon(int slot)
    {
        int idx;
        if (currentSlot != slot && weapons.Count > slot)
            idx = slot;
        else
            return;
        
        currentWeapon.Disable();
        currentWeapon = weapons[idx];
        currentSlot = idx;
        currentWeapon.Enable();
    }

    protected void RequestReload()
    {
        var rangeWeapon = (WeaponRange)currentWeapon;
        rangeWeapon.TryReload();
    }

    #endregion

}
