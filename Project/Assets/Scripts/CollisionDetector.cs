using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnCollideWith : UnityEvent<Collider>
{

}

public class CollisionDetector : MonoBehaviour
{

    public EventOnCollideWith OnCollideWith = new EventOnCollideWith();

    public void AddIgnoredTags(List<string> ignoredTags)
    {
        this.ignoredTags = ignoredTags;
    }

    #region private

    List<string> ignoredTags;

    void OnCollisionEnter(Collision collision)
    {
        if (ignoredTags != null && ignoredTags.Contains(collision.gameObject.tag))
        {
            return;
        }

        OnCollideWith.Invoke(collision.collider);
    }

    void OnTriggerEnter(Collider other)
    {
        if (ignoredTags != null && ignoredTags.Contains(other.gameObject.tag))
        {
            return;
        }

        OnCollideWith.Invoke(other);
    }

    #endregion
}
