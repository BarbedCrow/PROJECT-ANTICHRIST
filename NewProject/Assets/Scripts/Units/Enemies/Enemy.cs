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
    Transform playerPosition;

    bool isChasing = false;
    bool isRangeAttacking = false;

    protected override void SetupComponents()
    {
        base.SetupComponents();

        propWeaponUserMelee.Setup(damager, playerDetector, projectilesPool);
        propWeaponUserRange.Setup(damager, playerDetector, projectilesPool);
        propAbilityUser.Setup(damager, playerDetector);
    }

    protected override void InitComponents()
    {
        base.InitComponents();

        playerDetector.Init(transform);

        var propWeaponUserRangeAI = (PropWeaponUserRangeAI)propWeaponUserRange;
        var propWeaponUserMeleeAI = (PropWeaponUserMeleeAI)propWeaponUserMelee;
        var propAbilityUserAI = (PropAbilityUserAI)propAbilityUser;

        propWeaponUserRangeAI.OnAIRangeAttackStart.AddListener(RangeAttackStart);
        propWeaponUserRangeAI.OnAIRangeAttackStop.AddListener(RangeAttackStop);
        
        propWeaponUserMeleeAI.OnAIMeleeAttackStop.AddListener(MeleeAttackStop);

        propAbilityUserAI.OnAIAbilityUseStart.AddListener(AbilityUseStart);
        propAbilityUserAI.OnAIAbilityUseStop.AddListener(AbilityUseStop);
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

        var userRangeAI = (PropWeaponUserRangeAI)propWeaponUserRange;
        var userMeleeAI = (PropWeaponUserMeleeAI)propWeaponUserMelee;
        var userAbilityAI = (PropAbilityUserAI)propAbilityUser;

        userRangeAI.HandleOnSeen(player);
        userMeleeAI.HandleOnSeen(player);
        userAbilityAI.HandleOnSeen(player);

        userRangeAI.Enable();
    }

    void HandleOnMissPlayer()
    {
        isChasing = false;
        var aiProp = (PropMovementAI)propMovement;
        aiProp.StopChasing();

        var userRangeAI = (PropWeaponUserRangeAI)propWeaponUserRange;
        var userMeleeAI = (PropWeaponUserMeleeAI)propWeaponUserMelee;
        var userAbilityAI = (PropAbilityUserAI)propAbilityUser;

        userRangeAI.HandleOnMiss();
        userMeleeAI.HandleOnMiss();
        userAbilityAI.HandleOnMiss();

        userRangeAI.Disable();
    }

    protected override void Die(DamageInfo info)
    {
        base.Die(info);
        enemiesPool.Release(gameObject);
    }

    protected void RangeAttackStart()
    {
        isRangeAttacking = true;
        propWeaponUserRange.Enable();
        propWeaponUserMelee.Disable();
    }

    protected void RangeAttackStop()
    {
        isRangeAttacking = false;
        propWeaponUserRange.Disable();
        propWeaponUserMelee.Enable();

    }

    protected void MeleeAttackStop()
    {
        isRangeAttacking = true;
        propWeaponUserRange.Enable();
        propWeaponUserMelee.Disable();
    }

    protected void AbilityUseStart()
    {
        propWeaponUserRange.Disable();
        propWeaponUserMelee.Disable();
    }

    protected void AbilityUseStop()
    {
        if (isRangeAttacking)
        {
            propWeaponUserRange.Enable();
            propWeaponUserMelee.Disable();
        }
        else
        {
            propWeaponUserRange.Disable();
            propWeaponUserMelee.Enable();
        }
    }

    #endregion

}
