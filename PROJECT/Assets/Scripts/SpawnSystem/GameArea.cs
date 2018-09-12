using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AreaSpawnController))]
[RequireComponent(typeof(BoxCollider))]

public class GameArea : MonoBehaviour
{
    public SpawnManager spawnManager;

    public void Start()
    {
        areaSpawnController = GetComponent<AreaSpawnController>();
        collider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            spawnManager.SpawnUnits(areaSpawnController);
            collider.isTrigger = false;
        }    
    }

    #region private

    AreaSpawnController areaSpawnController;
    Collider collider;

    #endregion
}
