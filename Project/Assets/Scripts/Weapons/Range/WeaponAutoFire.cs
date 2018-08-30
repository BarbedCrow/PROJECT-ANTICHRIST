using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAutoFire : WeaponRange
{

    public override void RequestAttack()
    {
        base.RequestAttack();

        pauseShotsTimer.OnTimersFinished.AddListener(WaitForNextShoot);
    }

    public override void RequestStopAttack()
    {
        base.RequestStopAttack();

        pauseShotsTimer.OnTimersFinished.RemoveListener(WaitForNextShoot);
    }

    public override void Terminate()
    {
        pauseShotsTimer.OnTimersFinished.RemoveListener(WaitForNextShoot);

        base.Terminate();
    }

    public override void Disable()
    {
        pauseShotsTimer.OnTimersFinished.RemoveListener(WaitForNextShoot);

        base.Disable();    }

    #region private

    protected void WaitForNextShoot()
    {
        pauseShotsTimer.OnTimersFinished.RemoveListener(WaitForNextShoot);

        if (isReloading)
        {
            return;
        }

        if (countOfBullets <= NO_BULLETS)
        {
            OnShootEmpty.Invoke();
            return;
        }

        if (!isShooting)
        {
            return;
        }
        
        Shoot();
        pauseShotsTimer.StartWork();
        pauseShotsTimer?.OnTimersFinished.AddListener(WaitForNextShoot);
    }

    protected override void HandleOnReloaded()
    {
        base.HandleOnReloaded();

        WaitForNextShoot();
    }

    #endregion
}
