using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropWeaponUserMelee : PropWeaponUserBase
{
    [HideInInspector] public UnityEvent OnStartAttack;

    public override void Init(Transform owner)
    {
        base.Init(owner);

        currentWeapon.Disable();
    }

    #region private

    protected override void RequestStartAttackInternal()
    {
        base.RequestStartAttackInternal();

        OnStartAttack.Invoke();

        var melee = (WeaponMelee)currentWeapon;
        if (!melee.IsAttacking())
        {
            currentWeapon.RequestStartAttack();
        }
    }

    #endregion

}
