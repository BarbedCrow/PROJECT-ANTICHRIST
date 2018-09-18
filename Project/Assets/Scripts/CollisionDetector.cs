using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnCollideWith : UnityEvent<Collision>
{

}

public class CollisionDetector : MonoBehaviour
{

    public List<string> ignoredTags;

    public EventOnCollideWith OnCollideWith = new EventOnCollideWith();

    void OnCollisionEnter(Collision collision)
    {
        if (ignoredTags.Contains(collision.gameObject.tag))
        {
            return;
        }

        OnCollideWith.Invoke(collision);
    }
}
