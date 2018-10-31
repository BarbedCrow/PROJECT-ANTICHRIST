using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropAbilityUserPlayer : PropAbilityUser
{
    [SerializeField] InputType abilitySlot1InputType;
    [SerializeField] InputType abilitySlot2InputType;

    public override void Setup(params MonoBehaviour[] args)
    {
        foreach (MonoBehaviour arg in args)
        {
            if (inputsLibrary == null && arg is InputsLibrary)
            {
                inputsLibrary = (InputsLibrary)arg;
            }

            if (abilitiesLibrary == null && arg is AbilitiesLibrary)
            { 
                abilitiesLibrary = (AbilitiesLibrary)arg;
            }

            if (propDamager == null && arg is PropDamager)
            {
                base.Setup(args);
            }
        }
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        abilitySlot1 = (InputHold)inputsLibrary.GetInput(abilitySlot1InputType);
        abilitySlot2 = (InputHold)inputsLibrary.GetInput(abilitySlot2InputType);
    }

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

    #region private

    InputsLibrary inputsLibrary;
    AbilitiesLibrary abilitiesLibrary;
    InputHold abilitySlot1;
    InputHold abilitySlot2;
    const int SLOT_1 = 0;
    const int SLOT_2 = 1;
    AbilityInfo[] infos;

    protected override void CreateAbilities()
    {
        infos = GameObject.FindGameObjectWithTag(Tags.USER).GetComponent<User>().GetAbilityInfos();        
        abilities = new List<AbilityLogicBase>() { null, null };

        foreach (var info in infos)
        {
            abilities[(int)info.GetSlot()] = abilitiesLibrary.GetDescByUid(info.GetUid()).GetAbilityLogicByLvl(info.GetLvl());
        }
    }

    #endregion

}
