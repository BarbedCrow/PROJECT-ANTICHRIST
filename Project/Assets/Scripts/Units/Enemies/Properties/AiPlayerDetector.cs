using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Scanner))]
public class AiPlayerDetector : MonoBehaviour
{

    [HideInInspector]
    public EventOnSeen OnSeen = new EventOnSeen();

    [HideInInspector]
    public UnityEvent OnMiss = new UnityEvent();

    public void Init()
    {
        scanner = GetComponent<Scanner>();
        scanner.Init();

        scanner.OnSeen.AddListener(HandleOnSeen);
        scanner.OnMiss.AddListener(HandleOnMiss);
    }

    public void Terminate()
    {
        scanner.OnSeen.RemoveListener(HandleOnSeen);
        scanner.OnMiss.RemoveListener(HandleOnMiss);
        scanner.Terminate();
    }

    #region private

    Scanner scanner;

    void HandleOnSeen(Transform playerTransform)
    {
        OnSeen.Invoke(playerTransform);
    }

    void HandleOnMiss()
    {
        OnMiss.Invoke();
    }

    #endregion

}
