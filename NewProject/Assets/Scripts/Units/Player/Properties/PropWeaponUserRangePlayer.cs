using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropWeaponUserRangePlayer : PropWeaponUserRange {

    [HideInInspector] public UnityEvent OnSwapWeapon = new UnityEvent();

    [SerializeField] InputType attackInputType;
    [SerializeField] InputType reloadInputType;
    [SerializeField] InputType swap1InputType;
    [SerializeField] InputType swap2InputType;
    [SerializeField] InputType swap3InputType;

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
        swap1 = (InputTap)inputsLibrary.GetInput(swap1InputType);
        swap2 = (InputTap)inputsLibrary.GetInput(swap2InputType);
        swap3 = (InputTap)inputsLibrary.GetInput(swap3InputType);
    }

    #region private

    InputsLibrary inputsLibrary;
    InputHold attackInput;
    InputTap reloadInput;
    InputTap swap1;
    InputTap swap2;
    InputTap swap3;

    public override void Enable()
    {
        base.Enable();

        reloadInput.OnUse.AddListener(RequestReload);
        attackInput.OnPressed.AddListener(RequestStartAttackInternal);
        attackInput.OnReleased.AddListener(RequestStopAttackInternal);
        swap1.OnUse.AddListener(() => SwapWeapon(SlotType.SLOT_1));
        swap2.OnUse.AddListener(() => SwapWeapon(SlotType.SLOT_2));
        swap3.OnUse.AddListener(() => SwapWeapon(SlotType.SLOT_3));
    }

    public override void Disable()
    {
        reloadInput.OnUse.RemoveListener(RequestReload);
        attackInput.OnPressed.RemoveListener(RequestStartAttackInternal);
        attackInput.OnReleased.RemoveListener(RequestStopAttackInternal);

        base.Disable();
    }

    protected override void SwapWeapon(SlotType slot)
    {
        base.SwapWeapon(slot);
        OnSwapWeapon.Invoke();
    }
    #endregion

}
