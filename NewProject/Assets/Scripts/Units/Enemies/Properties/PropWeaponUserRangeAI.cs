using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropWeaponUserRangeAI : PropWeaponUserRange
{
    [HideInInspector] public UnityEvent OnAIRangeAttackStart;
    [HideInInspector] public UnityEvent OnAIRangeAttackStop;

    [SerializeField] float minRangeForAttack;
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
    bool canRangeAttack = false;

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
        if (rangeBetween > maxRangeForAttack || rangeBetween <= minRangeForAttack)
        {
            OnAIRangeAttackStop.Invoke();
            return;
        }

        OnAIRangeAttackStart.Invoke();
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