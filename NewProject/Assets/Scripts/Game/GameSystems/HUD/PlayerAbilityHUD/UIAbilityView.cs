using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIAbilityView : UIBaseView
{
    public override void Init(UIBaseViewData viewData)
    {
        base.Init();

        var abilityData = (UIAbilityViewData)viewData;
        UpdateUI(abilityData);
    }

    public override void UpdateUI(UIBaseViewData viewData)
    {
        var abilityData = (UIAbilityViewData)viewData;
        var firstAbility = viewElements[0].GetComponent<Image>();
        var secondAbility = viewElements[1].GetComponent<Image>();
        if (abilityData.isFirstExist)
        {
            if (abilityData.canUseFirst)
                firstAbility.color = greenColor;
            else
                firstAbility.color = redColor;
        }

        if(abilityData.isSecondExist)
        {
            if (abilityData.canUseSecond)
                secondAbility.color = greenColor;
            else
                secondAbility.color = redColor;
        }
    }

    #region private

    Color greenColor = new Color(0, 255, 0);
    Color redColor = new Color(255, 0, 0);

    #endregion

}
