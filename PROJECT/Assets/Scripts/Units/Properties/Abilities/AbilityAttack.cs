using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAttack : AbilityBase
{

    public float damage;
    public DamageType damagetType;

    public override void Init(InputsLibrary inputsLibrary, List<string> ignoredTags)
    {
        base.Init(inputsLibrary, ignoredTags);

        this.ignoredTags = ignoredTags;
        propDamager = GetComponent<DamagerBase>();
        propDamager.Init();
    }

    #region private

    public List<string> ignoredTags;
    protected DamagerBase propDamager;

    protected override void Activate()
    {
        base.Activate();
    }

    #endregion
}
