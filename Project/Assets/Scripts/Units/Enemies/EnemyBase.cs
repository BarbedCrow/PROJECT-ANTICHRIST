using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AssaultController))]
[RequireComponent(typeof(AiPlayerDetector))]
[RequireComponent(typeof(AiMovement))]
public class EnemyBase : UnitBase 
{
    [HideInInspector]
    public UnityEvent OnDeath = new UnityEvent();
    public int returnCost;

    public void Init(PoolBase pool)
    {
        base.Init();
        this.pool = pool;
    }

    public void CachePlayer(Player player)
    {

    }

    public int GetReturnCost()
    {
        return returnCost;
    }

    #region private

    PoolBase pool;
    AiMovement propMovement;
    AiPlayerDetector propPlayerDetector;
    AssaultController propAssault;

    bool isChasing;
    bool isAttacking;

    protected override void InitComponents()
    {
        base.InitComponents();
        
        propMovement = GetComponent<AiMovement>();
        propMovement.Init();

        propDamagable.OnDie.AddListener(Die);

        propAssault = GetComponent<AssaultController>();
        propAssault.Init();

        propPlayerDetector = GetComponent<AiPlayerDetector>();
        propPlayerDetector.OnMiss.AddListener(HandleOnPlayerMiss);
        propPlayerDetector.OnSeen.AddListener(HandleOnPlayerSeen);
        propPlayerDetector.Init();
    }

    protected override void TerminateComponents()
    {
        propAssault.Terminate();
        propMovement.Terminate();
        propPlayerDetector.Terminate();

        base.TerminateComponents();
    }

    void HandleOnPlayerSeen(Transform playerTransform)
    {
        isChasing = true;
        propMovement.StartChase(playerTransform);
        propAssault.StartCheck(playerTransform);
    }

    void HandleOnPlayerMiss()
    {
        isChasing = false;
        propMovement.StopChase();
        propAssault.StopCheck();
    }

    protected override void Die(DamageInfo info)
    {
        pool.Release(gameObject);
        OnDeath.Invoke();
        OnDeath.RemoveAllListeners();
    }

    #endregion

}
