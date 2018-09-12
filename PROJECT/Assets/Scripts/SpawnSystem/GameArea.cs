using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AreaSpawnController))]
[RequireComponent(typeof(BoxCollider))]

public class GameArea : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent PlayerEntered = new UnityEvent();

    public SpawnManager spawnManager;

    public void Init(SpawnManager spawnManager)
    {
        areaSpawnController = GetComponent<AreaSpawnController>();
        areaSpawnController.Init(this, spawnManager);

        myCollider = GetComponent<BoxCollider>();
    }

    public void Terminate()
    {
        Destroy(gameObject);
    }
    
    #region private

    AreaSpawnController areaSpawnController;
    Collider myCollider;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(GlobalConstants.TAG_PLAYER))
        {
            
            myCollider.isTrigger = false;
            myCollider.enabled = false;
            PlayerEntered.Invoke();
        }
    }

    #endregion
}
