using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAbilityBase : PropBase
{

    public List<AbilityBase> abilities;
    public List<string> ignoredTags;

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        propDamager = gameObject.AddComponent<PropDamager>();
        foreach (AbilityBase ability in abilities)
        {
            ability.Setup(propDamager);
        }
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        foreach (AbilityBase ability in abilities)
        {
            ability.Init();
        }
    }

    public override void Terminate()
    {
        foreach (AbilityBase ability in abilities)
        {
            ability.Terminate();
        }

        base.Terminate();
    }

    #region private

    protected PropDamager propDamager;

    protected virtual void DoAbility(int idx)
    {
        abilities[idx].DoAbility();
    }

    #endregion
}

