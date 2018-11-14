using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIScoreView : UIBaseView
{
    public override void Init(UIBaseViewData viewData)
    {
        base.Init();

        var hpData = (UIScoreViewData)viewData;
        UpdateUI(hpData);
    }

    public override void UpdateUI(UIBaseViewData viewData)
    {
        var scoreData = (UIScoreViewData)viewData;
        var scoreText = viewElements[0].GetComponent<Text>();
        scoreText.text = scoreData.score.ToString();
    }

    #region private

    #endregion

}
