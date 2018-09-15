using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    #region private

    PoolBase pool;
    AiMovement propMovement;
    AiPlayerDetector propPlayerDetector;

    protected override void InitComponents()
    {
        base.InitComponents();
        
        propMovement = GetComponent<AiMovement>();
        propMovement.Init();

        propDamagable.OnDie.AddListener(Die);

        propPlayerDetector = GetComponent<AiPlayerDetector>();
        propPlayerDetector.OnMiss.AddListener(HandleOnPlayerMiss);
        propPlayerDetector.OnSeen.AddListener(HandleOnPlayerSeen);
        propPlayerDetector.Init();

    }

    public void Die(DamageInfo info)
    {
        pool.Release(gameObject);
        OnDeath.Invoke();
        OnDeath.RemoveAllListeners();
    }

    public int GetReturnCost()
    {
        return returnCost;
    }

    protected override void TerminateComponents()
    {
        propMovement.Terminate();
        propPlayerDetector.Terminate();

        base.TerminateComponents();
    }

    void HandleOnPlayerSeen(Transform playerTransform)
    {
        propMovement.StartChase(playerTransform);
    }

    void HandleOnPlayerMiss()
    {
        propMovement.StopChase();
    }

    #endregion

}
