using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponUserMelee))]
public class AssaultController : MonoBehaviour
{

    public float maxDistToAttack;
    public float timeBetweenAttack;

    public void Init()
    {
        timer = (Timer)gameObject.AddComponent(typeof(Timer));
        timer.Init(timeBetweenAttack);
        timer.OnTimersFinished.AddListener(HandleOnTimerFinished);

        propWeaponUserMelee = GetComponent<WeaponUserMelee>();
        propWeaponUserMelee.Init();
    }

    public void Terminate()
    {
        timer.OnTimersFinished.RemoveListener(HandleOnTimerFinished);
        timer.Terminate();
        propWeaponUserMelee?.Terminate();
    }

    public void StartCheck(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
        StartCoroutine(CHECK_POSSIBILITY_TO_ATTACK_COROUTINE);
    }

    public void StopCheck()
    {
        StopCoroutine(CHECK_POSSIBILITY_TO_ATTACK_COROUTINE);
        targetTransform = null;
    }

    #region private

    const string CHECK_POSSIBILITY_TO_ATTACK_COROUTINE = "CheckPossibilityToAttack";
    const float UPDATE_TIME = 0.1f;

    WeaponUserMelee propWeaponUserMelee;

    Transform targetTransform;
    bool isCanAttack = true;
    Timer timer;

    IEnumerator CheckPossibilityToAttack()
    {
        for(; ; )
        {
            var dist = Vector3.Distance(transform.position, targetTransform.position);
            if (dist <= maxDistToAttack && isCanAttack)
            {
                RequestAttack();
            }

            yield return new WaitForSeconds(UPDATE_TIME);
        }
    }

    void HandleOnTimerFinished()
    {
        isCanAttack = true;
    }

    void RequestAttack()
    {
        timer.StartWork();
        propWeaponUserMelee.RequestAttack();
        isCanAttack = false;
    }

    #endregion

}
