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
        if (info.damagable.GetComponentInParent<Enemy>())
        {
            if (difficulty.GetDifficult() == DifficultyType.EASY)
            {
                user.AddScore(100);
            }
            if (difficulty.GetDifficult() == DifficultyType.MEDIUM)
            {
                user.AddScore(200);
            }
            if (difficulty.GetDifficult() == DifficultyType.HARD)
            {
                user.AddScore(300);
            }
            if (difficulty.GetDifficult() == DifficultyType.MOMMY_WILL_NOT_HELP_YOU)
            {
                user.AddScore(500);
            }
            Debug.Log(user.GetScore().ToString());
        }
    }

    #endregion
}
