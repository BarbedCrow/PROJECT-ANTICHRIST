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
            user.Setup(damager, playerDetector, projectilesPool, propWeaponUsers[0], propWeaponUsers[1]);
        }

        propAbilityUser.Setup(damager, playerDetector);
    }

    protected override void InitComponents()
    {
        base.InitComponents();

        playerDetector.Init(transform);
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

    #endregion

}
