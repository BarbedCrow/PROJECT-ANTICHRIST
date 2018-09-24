using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public void Init()
    {

    }

    public void Terminate()
    {
        Destroy(gameObject);
    }
	
}

public class PlayerSpawnInfo
{
    Player player;
    Transform spawnTransform;
}
