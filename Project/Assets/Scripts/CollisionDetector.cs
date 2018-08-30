using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnCollideWith : UnityEvent<Collision>
{
}

public class CollisionDetector : MonoBehaviour
{

    public EventOnCollideWith OnCollideWith = new EventOnCollideWith();

    void OnCollisionEnter(Collision collision)
    {
        OnCollideWith.Invoke(collision);
    }
}
