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

    public void HandleOnSeen(Transform playerPosition)
    {
        this.playerPosition = playerPosition;
        canRangeAttack = true;
    }

    public void HandleOnMiss()
    {
        playerPosition = null;
        canRangeAttack = false;
    }

    #region private

    Timer cdTimer;
    Transform playerPosition;
    bool canRangeAttack = false;

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
        canRangeAttack = false;
        Invoke("SetCanAttack", coolDownTime);
    }

    void SetCanAttack()
    {
        canRangeAttack = true;
    }

    void Update()
    {
        if (!canRangeAttack)
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