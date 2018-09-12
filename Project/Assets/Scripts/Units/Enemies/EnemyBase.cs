using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AiPlayerDetector))]
[RequireComponent(typeof(AiMovement))]
public class EnemyBase : UnitBase 
{
    public void CachePlayer(Player player)
    {

    }

    #region private

    AiMovement propMovement;
    AiPlayerDetector propPlayerDetector;
    
    protected override void InitComponents()
    {
        base.InitComponents();

        propMovement = GetComponent<AiMovement>();
        propMovement.Init();

        propPlayerDetector = GetComponent<AiPlayerDetector>();
        propPlayerDetector.OnMiss.AddListener(HandleOnPlayerMiss);
        propPlayerDetector.OnSeen.AddListener(HandleOnPlayerSeen);
        propPlayerDetector.Init();

    }

    public void Die()
    {
        
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
