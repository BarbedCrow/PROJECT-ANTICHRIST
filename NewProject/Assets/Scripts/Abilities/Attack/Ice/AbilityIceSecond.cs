using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityIceSecond : AbilityAttackBase
{    
    public override void StartUse()
    {
        base.StartUse();
        Debug.Log("Ice2");
    }

    public override void StopUse()
    {
        base.StopUse();
    }

    #region private

    #endregion

}
