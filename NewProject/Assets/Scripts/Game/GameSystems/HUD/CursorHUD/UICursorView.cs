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
        animator.speed = 0;
    }

    public override void UpdateUI(UIBaseViewData viewData)
    {
        var data = (UICursorViewData)viewData;
        viewElements[0].transform.position = Input.mousePosition;

        if (data.isEnemy)
        {
            var procentHP = data.currHP / data.maxHP;
            if (procentHP == 1.0f) procentHP = 0.99f;
            animator.Play(ANIM_DEFAULT, 0, procentHP);
        else
        {
            animator.Play(ANIM_DEFAULT,0, 0);
        }
    }

    #region private

    const string ANIM_DEFAULT = "default";

    Animator animator;

    #endregion

}
