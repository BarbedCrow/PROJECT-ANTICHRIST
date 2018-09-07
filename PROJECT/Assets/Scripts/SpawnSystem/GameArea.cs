using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameArea : MonoBehaviour
{
    public AreaSpawnController areaSpawnController;
    public SpawnManager spawnManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            spawnManager.SpawnUnits(areaSpawnController);
        }
        //Destroy(gameObject);
    }
}

[System.Serializable]
public class EnemyDesc
{
    public EnemyBase enemyType;
    public int count;
}

[System.Serializable]
public class AreaSpawnController
{
    public List<EnemyDesc> enemyDescs;
    public List<Transform> spawnPoints;
}
