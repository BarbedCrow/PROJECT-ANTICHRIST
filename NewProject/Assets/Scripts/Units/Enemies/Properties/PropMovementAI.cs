using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PropMovementAI : PropMovement
{

    [SerializeField] float maxDistToPlayer = 1f;
    [SerializeField] NavMeshAgent agent;

    public void StartChasing(Transform player)
    {
        playerTransform = player;
        StartCoroutine(CHASING_COROUTINE);
    }

    public void StopChasing()
    {
        StopCoroutine(CHASING_COROUTINE);
    }

    #region private

    const int UPDATE_FREQUENCY = 1;
    const string CHASING_COROUTINE = "Chasing";

    Transform playerTransform;

    IEnumerator Chasing()
    {
        for (; ; )
        {
            var dist = Vector3.Distance(playerTransform.position, transform.position);
            OnMove.Invoke(agent.nextPosition);
            if (dist <= maxDistToPlayer && !agent.isStopped)
            {
                agent.isStopped = true;
                agent.SetDestination(transform.position);
                yield return new WaitForFixedUpdate();
            }

            agent.SetDestination(playerTransform.position);
            if (agent.isStopped)
            {
                agent.isStopped = false;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    #endregion

}
