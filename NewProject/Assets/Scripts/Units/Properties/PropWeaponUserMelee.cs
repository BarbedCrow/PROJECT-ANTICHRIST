using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropWeaponUserMelee : PropWeaponUserBase
{

    public UnityEvent OnStartAttack = new UnityEvent();

    public override void Init(Transform owner)
    {
        base.Init(owner);

        currentWeapon.Disable();
    }

    #region private

    protected override void RequestStartAttackInternal()
    {
        base.RequestStartAttackInternal();

        var melee = (WeaponMelee)currentWeapon;
        if (!melee.IsAttacking())
        {
            currentWeapon.RequestStartAttack();
        }
    }

    #endregion

}
