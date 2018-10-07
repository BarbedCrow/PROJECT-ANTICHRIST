using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIHealthView : UIBaseView
{
    public override void Init(float startHP)
    {
        base.Init();

        maxHealth = startHP;
        viewElements[0].transform.localScale = new Vector3(1, 1, 1);
    }

    public override void UpdateUI(UIBaseViewData viewData)
    {
        var data = (UIHealthViewData)viewData;
        var scaleX = data.healthPoints/maxHealth;
        var newScale = new Vector3(scaleX, 1, 1);
        viewElements[0].transform.localScale = newScale;
    }

    #region private

    float maxHealth;

    #endregion

}
