using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public PlayerSpawnInfo playerSpawnInfo;
    public GameSystems gameSystems;

    #region private



    private void Start()
    {
        InitComponents();
    }

    private void OnDestroy()
    {
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

public class 
