using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityFireSecond : AbilityAttackBase
{
    public override void StartUse()
    {
        base.StartUse();
        Debug.Log("Fire2");
    }

    public override void StopUse()
    {
        base.StopUse();
    }

    #region private

    #endregion

}
