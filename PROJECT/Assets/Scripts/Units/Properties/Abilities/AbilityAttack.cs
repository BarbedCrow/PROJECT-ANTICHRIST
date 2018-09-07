using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAttack : AbilityBase
{

    public float damage;
    public DamageType damagetType;

    public override void Init(InputsLibrary inputsLibrary)
    {
        base.Init(inputsLibrary);

        propDamager = GetComponent<DamagerBase>();
        propDamager.Init();
    }

    #region private

    protected DamagerBase propDamager;

    protected override void Activate()
    {
        base.Activate();


    }

    #endregion
}
