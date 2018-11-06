using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIScoreController : UIBaseController
{
    public override void Init()
    {
        base.Init();
        user = GameObject.FindGameObjectWithTag(Tags.USER).GetComponent<User>();
        data.score = user.GetScore();
        uiView.Init(data);

        user.OnScoreChange.AddListener(UpdateUI);
    }

    #region private

    UIScoreViewData data = new UIScoreViewData();
    User user;

    void UpdateUI()
    {
        data.score = user.GetScore();
        uiView.UpdateUI(data);
    }

    #endregion

}
