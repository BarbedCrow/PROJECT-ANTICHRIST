using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AiMovement : MonoBehaviour
{



    public void Init()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Terminate()
    {

    }

    public void StartChase(Transform playerTransform)
    {
        Debug.Log("StartChase");
        this.playerTransform = playerTransform;
        StartCoroutine(UPDATE_CHASE_COROUTINE);
    }

    public void StopChase()
    {
        navMeshAgent.SetDestination(transform.position);
        StopCoroutine(UPDATE_CHASE_COROUTINE);
    }

    #region private

    const int UPDATE_FREQUENCY = 1;
    const string UPDATE_CHASE_COROUTINE = "UpdateChase";

    Transform playerTransform;
    NavMeshAgent navMeshAgent;

    IEnumerator UpdateChase()
    {
        for(; ; )
        {
            navMeshAgent.SetDestination(playerTransform.position);
            yield return new WaitForFixedUpdate();
        }
    }

    #endregion

}
