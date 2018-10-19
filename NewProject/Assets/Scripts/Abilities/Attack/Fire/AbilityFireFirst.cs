using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityFireFirst : AbilityAttackBase
{
    public override void StartUse()
    {
        sprite.Enable(pivot, damage, speed, damager);
    }

    public override void StopUse()
    {

    }

    #region private

    #endregion

}
