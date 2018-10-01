using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropWeaponUserRangePlayer : PropWeaponUserRange {

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

        attackInput = (InputHold)inputsLibrary.GetInput(attackInputType);
    }

    #region private

    InputsLibrary inputsLibrary;
    InputHold attackInput;

    public override void Enable()
    {
        base.Enable();

        attackInput.OnPressed.AddListener(RequestStartAttackInternal);
        attackInput.OnReleased.AddListener(RequestStopAttackInternal);
    }

    public override void Disable()
    {
        attackInput.OnPressed.RemoveListener(RequestStartAttackInternal);
        attackInput.OnReleased.RemoveListener(RequestStopAttackInternal);

        base.Disable();
    }

    #endregion

}
