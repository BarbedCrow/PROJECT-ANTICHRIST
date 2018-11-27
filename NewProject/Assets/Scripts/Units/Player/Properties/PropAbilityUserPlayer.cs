using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropAbilityUserPlayer : PropAbilityUser
{
    [HideInInspector] public UnityEvent OnPlayerAbilityUseStart;
    [HideInInspector] public UnityEvent OnPlayerAbilityUseStop;

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
        abilitySlot1.OnPressed.AddListener(() => StartUse(SLOT_1));
        abilitySlot1.OnReleased.AddListener(() => StopUse(SLOT_1));
        abilitySlot2.OnPressed.AddListener(() => StartUse(SLOT_2));
        abilitySlot2.OnReleased.AddListener(() => StopUse(SLOT_2));

        base.Enable();
    }

    public override void Disable()
    {
        base.Disable();

        abilitySlot1.OnPressed.RemoveAllListeners();
        abilitySlot1.OnReleased.RemoveAllListeners();
        abilitySlot2.OnPressed.RemoveAllListeners();
        abilitySlot2.OnReleased.RemoveAllListeners();
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

    public override void StartUse(int idx)
    {
        OnPlayerAbilityUseStart.Invoke();
        base.StartUse(idx);
    }

    public override void StopUse(int idx)
    {
        OnPlayerAbilityUseStop.Invoke();
        base.StopUse(idx);
    }

    #endregion

}
