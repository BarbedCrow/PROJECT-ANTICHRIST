using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropWeaponUserMeleePlayer : PropWeaponUserMelee
{

    [SerializeField] InputType attackInputType;

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        foreach (MonoBehaviour arg in args)
        {
            if (inputsLibrary == null && arg is InputsLibrary)
            {
                inputsLibrary = (InputsLibrary)arg;
            }
        }
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        attackInput = (InputTap)inputsLibrary.GetInput(attackInputType);
    }

    public override void Enable()
    {
        base.Enable();

        attackInput.OnUse.AddListener(RequestStartAttackInternal);
    }

    #region private

    InputsLibrary inputsLibrary;
    InputTap attackInput;

    protected override void RequestStartAttackInternal()
    {
        base.RequestStartAttackInternal();

        OnStartAttack.Invoke();
    }

    #endregion

}
