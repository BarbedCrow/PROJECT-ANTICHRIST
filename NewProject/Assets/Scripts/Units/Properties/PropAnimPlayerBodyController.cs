using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAnimPlayerBodyController : PropAnimController
{

    public override void Init(Transform owner)
    {
        base.Init(owner);

        userRange = owner.GetComponent<Player>().GetWeaponUserRange();
        weapon = (WeaponRange)userRange.GetWeapon();
        weapon.OnAttackStared.AddListener(StartShoot);
        weapon.OnAttackStopped.AddListener(StopShoot);
    }

    public override void Terminate()
    {
        weapon.OnAttackStared?.RemoveListener(StartShoot);
        weapon.OnAttackStopped?.RemoveListener(StopShoot);

        base.Terminate();
    }

    #region private

    const string IS_SHOOTING = "isShooting";

    PropWeaponUserRange userRange;
    WeaponRange weapon;

    void StartShoot()
    {
        animator.SetBool(IS_SHOOTING, true);
    }

    void StopShoot()
    {
        animator.SetBool(IS_SHOOTING, false);
    }

    #endregion

}
