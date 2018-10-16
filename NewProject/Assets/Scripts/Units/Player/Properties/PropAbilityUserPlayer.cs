﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAbilityUserPlayer : PropAbilityUser
{
    [SerializeField] InputType abilitySlot1InputType;
    [SerializeField] InputType abilitySlot2InputType;

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

        abilitySlot1 = (InputHold)inputsLibrary.GetInput(abilitySlot1InputType);
        abilitySlot2 = (InputHold)inputsLibrary.GetInput(abilitySlot2InputType);
    }

    #region private

    InputsLibrary inputsLibrary;
    InputHold abilitySlot1;
    InputHold abilitySlot2;
    const int SLOT_1 = 0;
    const int SLOT_2 = 1;

    public override void Enable()
    {
        base.Enable();

        abilitySlot1.OnPressed.AddListener(() => StartUse(SLOT_1));
        abilitySlot1.OnReleased.AddListener(() => StopUse(SLOT_1));
        abilitySlot2.OnPressed.AddListener(() => StartUse(SLOT_2));
        abilitySlot2.OnReleased.AddListener(() => StopUse(SLOT_2));
    }

    public override void Disable()
    {
        abilitySlot1.OnPressed.RemoveListener(() => StartUse(SLOT_1));
        abilitySlot1.OnReleased.RemoveListener(() => StopUse(SLOT_1));
        abilitySlot2.OnPressed.RemoveListener(() => StartUse(SLOT_2));
        abilitySlot2.OnReleased.RemoveListener(() => StopUse(SLOT_2));

        base.Disable();
    }
    #endregion

}
