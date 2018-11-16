using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropWeaponUserMeleeAI : PropWeaponUserMelee
{
    [SerializeField] float maxRangeForAttack;
    [SerializeField] float coolDownTime;

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

    #region private

    Timer cdTimer;
    bool detectPlayer = false;
    AiPlayerDetector playerDetector;
    Transform playerPosition;
    bool canAttack = true;

    void HandleOnSeen(Transform playerPosition)
    {
        detectPlayer = true;
        this.playerPosition = playerPosition;
        TryAttack();
    }

    void HandleOnMiss()
    {
        detectPlayer = false;
        playerPosition = null;
    }

    void TryAttack()
    {
        var rangeBetween = Vector3.Distance(gameObject.transform.position, playerPosition.position);
        Debug.Log(rangeBetween.ToString());
        if (rangeBetween <= maxRangeForAttack)
        {
            RequestStartAttackInternal();
            cdTimer.StartWork();
            canAttack = false;
            cdTimer.OnTimersFinished.AddListener(SetCanAttack);
            RequestStartAttackInternal();
        }
    }

    void SetCanAttack()
    {
        cdTimer.OnTimersFinished.RemoveListener(SetCanAttack);
        canAttack = true;
    }

    void Update()
    {
        if(detectPlayer && canAttack)
            TryAttack();
    }



    #endregion
}
