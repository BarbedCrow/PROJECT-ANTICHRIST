using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UICursorController : UIBaseController
{
    [SerializeField] CursorInDetector detector;

    public override void Init()
    {
        base.Init();
        Cursor.visible = false;
        data.mousePoition = Input.mousePosition;
        uiView.Init();

        detector.OnTriggerIn.AddListener(CursorTriggerOn);
        detector.OnTriggerOut.AddListener(CursorTriggerOut);

    }

    #region private

    UICursorViewData data = new UICursorViewData();
    bool isEnemy = false;
    Enemy enemy;

    void CursorTriggerOn(Collider other)
    {
        var collider = other.GetComponent<Enemy>();
        if (collider != null)
        {
            isEnemy = true;
            enemy = collider;
            enemy.OnDie.AddListener(EnemyDead);
        }
    }

    void CursorTriggerOut(Collider other)
    {
        var collider = other.GetComponent<Enemy>();
        if (collider != null)
        {
            isEnemy = false;
            enemy.OnDie.RemoveListener(EnemyDead);
            enemy = null;
        }
    }

    void EnemyDead(DamageInfo info)
    {
        isEnemy = false;
        enemy = null;
        enemy.OnDie.RemoveListener(EnemyDead);
    }

    void Update()
    {
        data.mousePoition = Input.mousePosition;
        data.isEnemy = isEnemy;
        if (enemy)
        {
            data.currHP = enemy.GetDamagable().GetCurrentHealth();
            data.maxHP = enemy.GetDamagable().GetMaxHealth();
        }
        uiView.UpdateUI(data);
    }

    #endregion

}
