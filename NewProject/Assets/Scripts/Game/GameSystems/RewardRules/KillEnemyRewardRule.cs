using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyRewardRule : RewardRuleBase
{
    public override void Init()
    {
        base.Init();

        var player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Player>();
        player.GetDamager().onKillEnemy.AddListener(AddScore);
    }

    #region private

    void AddScore(DamageInfo info)
    {
        user.AddScore(100);
        Debug.Log(user.GetScore().ToString());
    }

    #endregion
}
