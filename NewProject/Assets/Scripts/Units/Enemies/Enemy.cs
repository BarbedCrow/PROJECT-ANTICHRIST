using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    [SerializeField] AiPlayerDetector playerDetector;
    [SerializeField] int returnCost;

    public override void Setup(params MonoBehaviour[] args)
    {
        foreach(var arg in args)
        {
            if(enemiesPool == null && arg is EnemiesPool)
            {
                enemiesPool = (EnemiesPool)arg;
            }

            if (projectilesPool == null && arg is ProjectilesPool)
            {
                projectilesPool = (ProjectilesPool)arg;
            }
        }

        base.Setup(args);
    }

    public override void Enable()
    {
        base.Enable();

        playerDetector.OnSeen.AddListener(HandleOnSeenPlayer);
        playerDetector.OnMiss.AddListener(HandleOnMissPlayer);
        playerDetector.Enable();
    }

    public override void Disable()
    {
        playerDetector.OnSeen.RemoveListener(HandleOnSeenPlayer);
        playerDetector.OnMiss.RemoveListener(HandleOnMissPlayer);

        base.Disable();
    }

    public int GetReturnCost()
    {
        return returnCost;
    }

    public AiPlayerDetector GetPlayerDetector()
    {
        return playerDetector;
    }

    #region private

    EnemiesPool enemiesPool;
    ProjectilesPool projectilesPool;

    bool isChasing = false;

    protected override void SetupComponents()
    {
        base.SetupComponents();

        foreach (PropWeaponUserBase user in propWeaponUsers)
        {
            user.Setup(damager, playerDetector, projectilesPool);
        }

        propAbilityUser.Setup(damager, playerDetector);
    }

    protected override void InitComponents()
    {
        base.InitComponents();

        playerDetector.Init(transform);

        var propWeaponUserRangeAI = (PropWeaponUserRangeAI)propWeaponUsers[0];
        var propWeaponUserMeleeAI = (PropWeaponUserMeleeAI)propWeaponUsers[1];
        //var propAbilityUserAI = (PropAbilityUserAI)propAbilityUser;

        propWeaponUserRangeAI.OnAIRangeAttackStart.AddListener(RangeAttackStart);
        propWeaponUserRangeAI.OnAIRangeAttackStop.AddListener(RangeAttackStop);

        //propWeaponUserMeleeAI.OnAIMeleeAttackStart.AddListener(MeleeAttackStart);
        propWeaponUserMeleeAI.OnAIMeleeAttackStop.AddListener(MeleeAttackStop);

        //propAbilityUserAI.OnAIAbilityUseStart.AddListener(AbilityUseStart);
        //propAbilityUserAI.OnAIAbilityUseStop.AddListener(AbilityUseStop);
    }

    protected override void TerminateComponents()
    {
        playerDetector.OnSeen.RemoveListener(HandleOnSeenPlayer);
        playerDetector.OnMiss.RemoveListener(HandleOnMissPlayer);
        playerDetector.Terminate();

        base.TerminateComponents();
    }

    void HandleOnSeenPlayer(Transform player)
    {
        isChasing = true;
        var aiProp = (PropMovementAI)propMovement;
        aiProp.StartChasing(player);
    }

    void HandleOnMissPlayer()
    {
        isChasing = false;
        var aiProp = (PropMovementAI)propMovement;
        aiProp.StopChasing();
    }

    protected override void Die(DamageInfo info)
    {
        base.Die(info);
        enemiesPool.Release(gameObject);
    }

    protected void RangeAttackStart()
    {
        propWeaponUsers[0].Enable();
        propWeaponUsers[1].Disable();
    }

    protected void RangeAttackStop()
    {
        propWeaponUsers[0].Disable();
        propWeaponUsers[1].Enable();
    }

    /*protected void MeleeAttackStart()
    {
        propWeaponUsers[0].Disable();
    }*/

    protected void MeleeAttackStop()
    {
        propWeaponUsers[0].Enable();
        propWeaponUsers[1].Disable();
    }

    /*protected void AbilityUseStart()
    {
        propWeaponUsers[0].Disable();
        propWeaponUsers[1].Disable();
    }

    protected void AbilityUseStop()
    {
        propWeaponUsers[0].Enable();
        propWeaponUsers[1].Enable();
    }*/

    #endregion

}
