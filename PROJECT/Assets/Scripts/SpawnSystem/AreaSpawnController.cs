using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AreaSpawnController : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnAllEnemiesDeath = new UnityEvent();

    public List<EnemyDesc> enemyDescs;
    public List<Transform> spawnPoints;

    public void Init(GameArea gameArea, SpawnManager spawnManager)
    {
        this.gameArea = gameArea;
        this.spawnManager = spawnManager;
        gameArea.PlayerEntered.AddListener(SpawnEnemy);
    }

    public void Terminate()
    {
        gameArea.PlayerEntered.RemoveListener(SpawnEnemy);
    }

    #region private

    GameArea gameArea;
    SpawnManager spawnManager;

    void SpawnEnemy()
    {
        spawnManager.SpawnUnit(enemyDescs[0].enemyType, spawnPoints[0]);
    }

    #endregion
}

[System.Serializable]
public class EnemyDesc
{
    public EnemyBase enemyType;
    public int count;
}
