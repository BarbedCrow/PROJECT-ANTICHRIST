using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAbilityUser : PropBase
{

    public void StartUse()
    {

    }

    public void StopUse()
    {

    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        CreateAbilities();
    }

    #region private

    List<AbilityLogicBase> abilities;

    void CreateAbilities()
    {

    }

    public void StartUseInternal()
    {

    }

    public void StopUseInternal()
    {

    }

    #endregion

}

enum AbilitySlot
{
    SLOT_1,
    SLOT_2
}
