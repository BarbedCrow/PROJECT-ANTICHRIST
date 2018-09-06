using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    public GameObject particlesPrefab;

    public virtual void Init(Transform transform)
    {
        spawnPoint = transform;
    }

    public virtual void Terminate()
    {
    }

    public virtual void Attack()
    {
    }

    #region private
    
    protected Transform spawnPoint;

    #endregion
}