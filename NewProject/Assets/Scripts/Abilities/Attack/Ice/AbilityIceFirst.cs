using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityIceFirst : AbilityAttackBase
{
    public override void StartUse()
    {
        base.StartUse();
        Debug.Log("Ice1");
    }

    public override void StopUse()
    {
        base.StopUse();
    }

    #region private

    #endregion

}
