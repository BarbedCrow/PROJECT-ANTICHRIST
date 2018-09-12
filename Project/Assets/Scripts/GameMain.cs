﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawnSystem))]
[RequireComponent(typeof(GameSystems))]
[RequireComponent(typeof(GlobalEventManager))]
[RequireComponent(typeof(InputsLibrary))]
public class GameMain : MonoBehaviour
{

    public PlayerSpawnInfo playerSpawnInfo;

    #region private

    GameSystems gameSystems;
    GlobalEventManager eventManager;
    InputsLibrary inputsLibrary;
    EnemySpawnSystem enemySpawnSystem;

    Player player;

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    void Init()
    {
        gameSystems = GetComponent<GameSystems>();
        gameSystems.Init();

        inputsLibrary = GetComponent<InputsLibrary>();
        inputsLibrary.Init();

        player = Instantiate(playerSpawnInfo.player, playerSpawnInfo.spawnPoint.position, playerSpawnInfo.rotation) as Player;
        player.Init(inputsLibrary);

        enemySpawnSystem = GetComponent<EnemySpawnSystem>();
        enemySpawnSystem.Init(player);

        eventManager = GetComponent<GlobalEventManager>();
        eventManager.OnGameReady.Invoke();
    }

    void Terminate()
    {
        enemySpawnSystem.Terminate();
        player.Terminate();
        inputsLibrary.Terminate();
        gameSystems.Terminate();
        Destroy(gameObject);
    }

    #endregion
}

[System.Serializable]
public class PlayerSpawnInfo
{
    public Transform spawnPoint;
    public Player player;
    public Quaternion rotation;
}
