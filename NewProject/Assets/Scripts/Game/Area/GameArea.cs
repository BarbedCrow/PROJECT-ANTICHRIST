using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameArea : MonoBehaviour
{

    [HideInInspector]
    public UnityEvent OnPlayerEntered = new UnityEvent();

    [SerializeField] AreaSpawnController spawnController;

    public void Init(EnemiesPool enemiesPool)
    {
        this.enemiesPool = enemiesPool;
        spawnController.Init(enemiesPool, this);
    }

    public void Terminate()
    {
        spawnController.Terminate();
        Destroy(gameObject);
    }

    public void Enable()
    {

    }

    public void Disable()
    {

    }

    #region private

    EnemiesPool enemiesPool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.PLAYER)
        {
            OnPlayerEntered.Invoke();
            HandleOnPlayerEntered(other.gameObject.transform);
        }
    }

    void HandleOnPlayerEntered(Transform player)
    {

    }

    #endregion

}
