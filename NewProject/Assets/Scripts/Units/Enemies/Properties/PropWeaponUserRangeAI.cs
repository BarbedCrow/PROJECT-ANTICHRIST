using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropWeaponUserRangeAI : PropWeaponUserRange
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

            if (userMeleeAI == null && arg is PropWeaponUserMeleeAI)
            {
                userMeleeAI = (PropWeaponUserMeleeAI)arg;
            }
        }        

        cdTimer = gameObject.AddComponent<Timer>();

        base.Setup(args);
    }

    public override void Init(Transform owner)
    {
        base.Init(owner);

        minRangeForAttack = userMeleeAI.GetMaxRange();
        cdTimer.Init(coolDownTime);
        playerDetector.OnSeen.AddListener(HandleOnSeen);
        playerDetector.OnMiss.AddListener(HandleOnMiss);
    }

    #region private

    float minRangeForAttack;

    Timer cdTimer;
    bool detectPlayer = false;
    AiPlayerDetector playerDetector;
    Transform playerPosition;
    bool canRangeAttack = false;

    PropWeaponUserMeleeAI userMeleeAI;

    void HandleOnSeen(Transform playerPosition)
    {
        detectPlayer = true;
        this.playerPosition = playerPosition;
        canRangeAttack = true;
    }

    void HandleOnMiss()
    {
        detectPlayer = false;
        playerPosition = null;
        canRangeAttack = false;
    }

    void TryAttack()
    {
        var rangeBetween = Vector3.Distance(gameObject.transform.position, playerPosition.position);
        if (!(rangeBetween <= maxRangeForAttack || rangeBetween > minRangeForAttack))
        {
            return;
        }

        RequestStartAttackInternal();
        cdTimer.StartWork();
        canRangeAttack = false;
        cdTimer.OnTimersFinished.AddListener(SetCanAttack);
        RequestStartAttackInternal();
    }

    void SetCanAttack()
    {
        cdTimer.OnTimersFinished.RemoveListener(SetCanAttack);
        canRangeAttack = true;
    }

    void Update()
    {
        if (!(detectPlayer && canRangeAttack))
        {
            RequestStopAttackInternal();
            return;
        }

        Ray ray = new Ray(gameObject.transform.position, transform.forward);
        RaycastHit raycastHit;

        Debug.DrawRay(gameObject.transform.position, transform.forward);

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.rigidbody.GetComponentInParent<Player>())
            {
                TryAttack();
            }
        }
        else
            RequestStopAttackInternal();
    }

    #endregion


}