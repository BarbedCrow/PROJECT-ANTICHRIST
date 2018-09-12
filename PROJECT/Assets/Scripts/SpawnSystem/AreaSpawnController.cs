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
    public int maxSpawnCurrency;
    public int currentSpawnCurrency;

    public void Init(GameArea gameArea, SpawnManager spawnManager)
    {
        currentSpawnCurrency = maxSpawnCurrency;
        this.gameArea = gameArea;
        this.spawnManager = spawnManager;
        gameArea.PlayerEntered.AddListener(SpawnEnemies);
        OnAllEnemiesDeath.AddListener(AllDead);
    }

    public void Terminate()
    {
        gameArea.PlayerEntered.RemoveListener(SpawnEnemies);
    }

    #region private

    //int currentSpawnCurrency;
    GameArea gameArea;
    SpawnManager spawnManager;

    void AllDead()
    {
        OnAllEnemiesDeath.RemoveAllListeners();
        Debug.Log("GGWP");
    }

    void SpawnEnemies()
    {
        List<EnemyBase> bestSet = new List<EnemyBase>();
        List<EnemyBase> allEnemies = new List<EnemyBase>();

        int currentEnemies = 0;
        foreach(EnemyDesc desc in enemyDescs)
        {
            for(int i = 0; i < desc.count; i++)
            {
                var enemy = desc.enemyType;
                enemy.cost = desc.cost;
                allEnemies.Add(enemy);
            }
            currentEnemies += desc.count;
        }

        if (currentEnemies == 0)
        {
            OnAllEnemiesDeath.Invoke();
        }

        Pack pack = new Pack();
        bestSet = pack.ReturnBestSet(allEnemies, currentSpawnCurrency);

        int j = 0;
        foreach(EnemyBase enemy in bestSet)
        {
            var prefab = spawnManager.SpawnUnit(enemy, spawnPoints[j]);
            j++;
            
            prefab.OnEnemyDeath.AddListener(() => EnemyDeath(prefab));
            currentSpawnCurrency -= enemy.cost;

            foreach (EnemyDesc desc in enemyDescs)
            {
                if(desc.enemyType == enemy)
                {
                    desc.count--;
                }
            }
        }
    }

    void EnemyDeath(EnemyBase enemy)
    {
        enemy.OnEnemyDeath.RemoveAllListeners();
        currentSpawnCurrency += enemy.ReturnCost();
        enemy.gameObject.SetActive(false);
        SpawnEnemies();
    }

    #endregion
}

[System.Serializable]
public class EnemyDesc
{
    public EnemyBase enemyType;
    public int count;
    public int cost;
}

class Pack
{
    List<EnemyBase> bestSet = new List<EnemyBase>();
    private int maxCost;
    private int bestAmount;

    public List<EnemyBase> ReturnBestSet(List<EnemyBase> enemies, int maxCost)
    {
        this.maxCost = maxCost;
        MakeAllSets(enemies);
        return bestSet;
    }

    private int CalcCost(List<EnemyBase> items)
    {
        int sumCost = 0;

        foreach (EnemyBase i in items)
        {
            sumCost += i.cost;
        }

        return sumCost;
    }

    private int CalcAmount(List<EnemyBase> items)
    {
        return items.Count;
    }

    private void CheckSet(List<EnemyBase> items)
    {
        if (bestSet == null)
        {
            if (CalcCost(items) <= maxCost)
            {
                bestSet = items;
                bestAmount = CalcAmount(items);
            }
        }
        else
        {
            if (CalcCost(items) <= maxCost && CalcAmount(items) > bestAmount)
            {
                bestSet = items;
                bestAmount = CalcAmount(items);
            }
        }

    }

    private void MakeAllSets(List<EnemyBase> items)
    {
        if (items.Count > 0)
            CheckSet(items);

        for (int i = 0; i < items.Count; i++)
        {
            List<EnemyBase> newSet = new List<EnemyBase>(items);

            newSet.RemoveAt(i);

            MakeAllSets(newSet);
        }
    }
}