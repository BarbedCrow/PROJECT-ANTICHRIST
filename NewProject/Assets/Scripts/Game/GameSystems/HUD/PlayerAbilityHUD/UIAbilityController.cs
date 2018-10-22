using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIAbilityController : UIBaseController
{
    public override void Init()
    {
        base.Init();
        var abilityUser = (PropAbilityUserPlayer)GameObject.FindWithTag(Tags.PLAYER).GetComponent<Player>().GetAbilityUser();
        abilities = abilityUser.GetAbilities();
        
        if (abilities[0])
        {
            data.isFirstExist = true;
            data.canUseFirst = true;
            abilities[0].OnAbilityAttack.AddListener(UpdateUI);
            abilities[0].OnAbilityStopReload.AddListener(UpdateUI);
        }

        if (abilities[1])
        {
            data.isSecondExist = true;
            data.canUseSecond = true;
            abilities[1].OnAbilityAttack.AddListener(UpdateUI);
            abilities[1].OnAbilityStopReload.AddListener(UpdateUI);
        }

        uiView.Init(data);
    }

    #region private

    UIAbilityViewData data = new UIAbilityViewData();
    List<AbilityLogicBase> abilities = new List<AbilityLogicBase>();

    void UpdateUI()
    {
        if (abilities[0])
        {
            data.canUseFirst = abilities[0].GetCanUse();
        }

        if (abilities[1])
        {
            data.canUseSecond = abilities[1].GetCanUse();
        }

        uiView.UpdateUI(data);
    }

    #endregion

}
