using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIBulletsView : UIBaseView
{
    public override void Init(UIBaseViewData viewData)
    {
        base.Init();

        var bulletsData = (UIBulletsViewData)viewData;
        UpdateUI(bulletsData);
    }

    public override void UpdateUI(UIBaseViewData viewData)
    {
        var bulletsData = (UIBulletsViewData)viewData;
        var currentBulletsInClip = viewElements[0].GetComponent<Text>();
        var currentAllBullets = viewElements[1].GetComponent<Text>();
        currentBulletsInClip.text = bulletsData.currentBulletsInClip.ToString();
        currentAllBullets.text = bulletsData.currentAllBullets.ToString();
    }

    #region private

    #endregion

}
