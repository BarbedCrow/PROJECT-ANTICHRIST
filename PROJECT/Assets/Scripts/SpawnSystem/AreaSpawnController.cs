using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AreaSpawnController : MonoBehaviour
{
    //[HideInInspector]
    //public UnityEvent OnAllEnemiesDeath = new UnityEvent();

    public List<EnemyDesc> enemyDescs;
    public List<Transform> spawnPoints;
}

[System.Serializable]
public class EnemyDesc
{
    public EnemyBase enemyType;
    public int count;
}
