using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.Animations;

public class UICursorView : UIBaseView
{

    public override void Init()
    {
        base.Init();

        animator = viewElements[0].GetComponent<Animator>();
    }

    public override void UpdateUI(UIBaseViewData viewData)
    {
        var data = (UICursorViewData)viewData;
        viewElements[0].transform.position = Input.mousePosition;
        animator.SetBool(IS_ENEMY, data.isEnemy);
        //animator.speed = 0;
        //animator.Play("enemy", 0, 0);
    }

    #region private

    const string IS_ENEMY = "isEnemy";

    Animator animator;

    #endregion

}
