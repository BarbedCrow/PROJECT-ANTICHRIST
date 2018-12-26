using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityFireSecond : AbilityAttackBase
{

    public override void StopUse()
    {
        base.StopUse();
    }

    #region private

    protected override void StartUse()
    {
        base.StartUse();
        Debug.Log("Fire2");
    }

    #endregion

}
