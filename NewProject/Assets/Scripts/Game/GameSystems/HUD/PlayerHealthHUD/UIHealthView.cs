using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIHealthView : UIBaseView
{
    public override void Init(UIBaseViewData viewData)
    {
        base.Init();

        var hpData = (UIHealthViewData)viewData;
        UpdateUI(hpData);
    }

    public override void UpdateUI(UIBaseViewData viewData)
    {
        var hpData = (UIHealthViewData)viewData;
        var scaleX = hpData.currentHealthPoints/hpData.maxHealthPoints;
        var newScale = new Vector3(scaleX, 1, 1);
        viewElements[0].transform.localScale = newScale;
    }

    #region private

    #endregion

}
