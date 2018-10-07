using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropWeaponUserRangePlayer : PropWeaponUserRange {

    [SerializeField] InputType attackInputType;
    [SerializeField] InputType reloadInputType;

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
        reloadInput = (InputTap)inputsLibrary.GetInput(reloadInputType);
    }

    #region private

    InputsLibrary inputsLibrary;
    InputHold attackInput;
    InputTap reloadInput;

    public override void Enable()
    {
        base.Enable();

        reloadInput.OnUse.AddListener(RequestReload);
        attackInput.OnPressed.AddListener(RequestStartAttackInternal);
        attackInput.OnReleased.AddListener(RequestStopAttackInternal);
    }

    public override void Disable()
    {
        reloadInput.OnUse.RemoveListener(RequestReload);
        attackInput.OnPressed.RemoveListener(RequestStartAttackInternal);
        attackInput.OnReleased.RemoveListener(RequestStopAttackInternal);

        base.Disable();
    }

    #endregion

}
