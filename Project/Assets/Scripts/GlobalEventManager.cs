using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour 
{
    [HideInInspector]
    public UnityEvent OnGameReady = new UnityEvent();

}
