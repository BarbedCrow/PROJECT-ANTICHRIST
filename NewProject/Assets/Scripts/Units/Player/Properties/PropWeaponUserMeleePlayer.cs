using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropWeaponUserMeleePlayer : PropWeaponUserMelee
{
    [HideInInspector] public UnityEvent OnPlayerMeleeAttackStart;
    [HideInInspector] public UnityEvent OnPlayerMeleeAttackStop;

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

    public override void Disable()
    {
        attackInput.OnUse.RemoveListener(RequestStartAttackInternal);

        base.Disable();
    }

    #region private

    InputsLibrary inputsLibrary;
    InputTap attackInput;

    protected override void RequestStartAttackInternal()
    {
        var currentMelee = (WeaponMelee)currentWeapon;
        currentMelee.OnAttackStopped.AddListener(RequestStopAttackInternal);
        OnPlayerMeleeAttackStart.Invoke();
        base.RequestStartAttackInternal();

    }

    protected override void RequestStopAttackInternal()
    {
        var currentMelee = (WeaponMelee)currentWeapon;
        currentMelee.OnAttackStopped.RemoveListener(RequestStopAttackInternal);
        OnPlayerMeleeAttackStop.Invoke();
        base.RequestStopAttackInternal();
    }

    #endregion

}
