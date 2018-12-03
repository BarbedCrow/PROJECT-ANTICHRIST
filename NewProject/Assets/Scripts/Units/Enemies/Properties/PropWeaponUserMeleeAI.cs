using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropWeaponUserMeleeAI : PropWeaponUserMelee
{
    [HideInInspector] public UnityEvent OnAIMeleeAttackStart;
    [HideInInspector] public UnityEvent OnAIMeleeAttackStop;

    [SerializeField] float maxRangeForAttack;
    [SerializeField] float coolDownTime;

    public float GetMaxRange()
    {
        return maxRangeForAttack;
    }

    public override void Setup(params MonoBehaviour[] args)
    {
        foreach (var arg in args)
        {
            if (playerDetector == null && arg is AiPlayerDetector)
            {
                playerDetector = (AiPlayerDetector)arg;
            }
        }

        cdTimer = gameObject.AddComponent<Timer>();

        base.Setup(args);
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        cdTimer.Init(coolDownTime);
        playerDetector.OnSeen.AddListener(HandleOnSeen);
        playerDetector.OnMiss.AddListener(HandleOnMiss);
    }

    public bool GetCanMeleeAttack()
    {
        return canMeleeAttack;
    }

    #region private

    Timer cdTimer;
    bool detectPlayer = false;
    AiPlayerDetector playerDetector;
    Transform playerPosition;
    bool canMeleeAttack = false;

    void HandleOnSeen(Transform playerPosition)
    {
        detectPlayer = true;
        canMeleeAttack = true;
        this.playerPosition = playerPosition;
    }

    void HandleOnMiss()
    {
        detectPlayer = false;
        canMeleeAttack = false;
        playerPosition = null;
    }

    void TryAttack()
    {
        var rangeBetween = Vector3.Distance(gameObject.transform.position, playerPosition.position);
        if (rangeBetween > maxRangeForAttack)
        {
            OnAIMeleeAttackStop.Invoke();
            return;
        }

        RequestStartAttackInternal();
        cdTimer.StartWork();
        canMeleeAttack = false;
        cdTimer.OnTimersFinished.AddListener(SetCanAttack);
        RequestStartAttackInternal();
    }

    void SetCanAttack()
    {
        cdTimer.OnTimersFinished.RemoveListener(SetCanAttack);
        canMeleeAttack = true;
    }

    void Update()
    {
        if (detectPlayer && canMeleeAttack)
            TryAttack();
    }
    
    #endregion
}
