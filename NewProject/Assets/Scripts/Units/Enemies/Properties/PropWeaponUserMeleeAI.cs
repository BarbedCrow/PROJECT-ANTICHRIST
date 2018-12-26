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

    public void HandleOnSeen(Transform playerPosition)
    {
        canMeleeAttack = true;
        this.playerPosition = playerPosition;
    }

    public void HandleOnMiss()
    {
        canMeleeAttack = false;
        playerPosition = null;
    }

    #region private

    Transform playerPosition;
    bool canMeleeAttack = false;

    void TryAttack()
    {
        var rangeBetween = Vector3.Distance(gameObject.transform.position, playerPosition.position);
        if (rangeBetween > maxRangeForAttack)
        {
            OnAIMeleeAttackStop.Invoke();
            return;
        }

        RequestStartAttackInternal();
        canMeleeAttack = false;
        Invoke("SetCanAttack", coolDownTime);
    }

    void SetCanAttack()
    {
        canMeleeAttack = true;
    }

    void Update()
    {
        if (canMeleeAttack)
            TryAttack();
    }
    
    #endregion
}
