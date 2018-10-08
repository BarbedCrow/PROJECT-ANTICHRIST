using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UICursorView : UIBaseView
{
    public override void Init(UIBaseViewData viewData)
    {
        base.Init();

        var data = (UICursorViewData)viewData;
        UpdateUI(data);
    }

    public override void UpdateUI(UIBaseViewData viewData)
    {
        var data = (UICursorViewData)viewData;
        viewElements[0].transform.position = Input.mousePosition;
        viewElements[0].GetComponent<Animator>().SetBool(IS_ENEMY, data.isEnemy);
    }

    #region private

    const string IS_ENEMY = "isEnemy";

    #endregion

}
