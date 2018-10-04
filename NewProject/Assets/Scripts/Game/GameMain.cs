using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMain : MonoBehaviour
{

    [SerializeField] PlayerSpawnInfo playerSpawnInfo;
    [SerializeField] GameSystems gameSystems;
    [SerializeField] List<GameArea> gameAreas;

    #region private

    Player player;

    private void Start()
    {
        foreach(var area in gameAreas)
        {
            area.Init(gameSystems.GetEnemiesPool());
        }

        player = Instantiate(playerSpawnInfo.player, playerSpawnInfo.spawnTransform.position, playerSpawnInfo.spawnTransform.rotation);
        player.Setup(gameSystems.GetInputsLibrary(), gameSystems.GetProjectilesPool());
        player.Init();

        player.Enable();
        
        InitComponents();
    }

    private void OnDestroy()
    {
        player.Terminate();

        foreach (var area in gameAreas)
        {
            area.Terminate();
        }

        TerminateComponents();
    }

    private void InitComponents()
    {
        gameSystems.Init();
    }

    private void TerminateComponents()
    {
        gameSystems.Terminate();
    }

    #endregion

}
