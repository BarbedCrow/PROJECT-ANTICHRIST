using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{

    public List<EnemySpawnInfo> enemySpawnInfos;

    public void Init(Player player)
    {
        this.player = player;
        foreach (EnemySpawnInfo enemySpawnInfo in enemySpawnInfos)
        {
            var enemy = (EnemyBase)Instantiate(enemySpawnInfo.enemy, enemySpawnInfo.spawnPoint.position, enemySpawnInfo.rotation);
            enemy.CachePlayer(player);
            enemy.Init();
        }
    }

    public void Terminate()
    {

    }

    #region private

    List<EnemyBase> enemies = new List<EnemyBase>();
    Player player;

    #endregion

}

[System.Serializable]
public class EnemySpawnInfo
{
    public Transform spawnPoint;
    public EnemyBase enemy;
    public Quaternion rotation;
}
