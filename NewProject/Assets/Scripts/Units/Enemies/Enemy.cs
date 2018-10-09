using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{

    [SerializeField] AiPlayerDetector playerDetector;
    [SerializeField] int returnCost;

    public override void Setup(params MonoBehaviour[] args)
    {
        base.Setup(args);

        foreach(var arg in args)
        {
            if(pool == null && arg is EnemiesPool)
            {
                pool = (EnemiesPool)arg;
            }
        }
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

    #region private

    EnemiesPool pool;

    bool isChasing = false;

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
        pool.Release(gameObject);
    }

    #endregion

}
