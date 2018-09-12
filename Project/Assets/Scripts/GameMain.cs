using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnManager))]
[RequireComponent(typeof(GameSystems))]
[RequireComponent(typeof(GlobalEventManager))]
[RequireComponent(typeof(InputsLibrary))]
public class GameMain : MonoBehaviour
{

    public PlayerSpawnInfo playerSpawnInfo;
    public List<GameArea> gameAreas;

    #region private

    GameSystems gameSystems;
    GlobalEventManager eventManager;
    InputsLibrary inputsLibrary;
    SpawnManager spawnManager;

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

        spawnManager = GetComponent<SpawnManager>();
        spawnManager.Init();

        player = Instantiate(playerSpawnInfo.player, playerSpawnInfo.spawnPoint.position, playerSpawnInfo.rotation) as Player;
        player.Init(inputsLibrary);

        eventManager = GetComponent<GlobalEventManager>();
        eventManager.OnGameReady.Invoke();

        foreach(GameArea gameArea in gameAreas)
        {
            gameArea.Init(spawnManager);
        }
    }

    void Terminate()
    {
        foreach (GameArea gameArea in gameAreas)
        {
            gameArea.Terminate();
        }

        spawnManager.Terminate();
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
