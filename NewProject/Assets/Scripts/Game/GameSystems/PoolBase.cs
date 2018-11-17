using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase : MonoBehaviour
{
    public PoolObjectDesc[] descs;
    public int maxCapacity;
    

    public virtual void Init(params MonoBehaviour[] args)
    {
        foreach (PoolObjectDesc desc in descs)
        {
            List<GameObject> objects = new List<GameObject>();
            availableObjects.Add(desc.gameObject.tag, objects);
            GameObject folder = new GameObject(desc.gameObject.tag);
            folder.transform.SetParent(transform);

            for (int i = 0; i < desc.initialCount; i += 1)
            {
                GameObject gO = Instantiate(desc.gameObject, transform.position, transform.rotation);
                gO.SetActive(false);
                gO.transform.SetParent(folder.transform);

                objects.Add(gO);
                currentCapacity += 1;
            }
        }
    }

    public virtual void Terminate()
    {
        Destroy(gameObject);
    }

    public GameObject Take(string key)
    {
        GameObject objectToTake = null;
        List<GameObject> objects;
        if (!availableObjects.TryGetValue(key, out objects))
        {
            objects = new List<GameObject>();
        }

        if (objects.Count > 0)
        {
            objectToTake = objects[0];
            objectToTake.SetActive(true);
            objects.Remove(objectToTake);
            takenObjects.Add(objectToTake);
        }
        else
        {
            // create new object
        }

        return objectToTake;
    }

    public void Release(GameObject gO)
    {
        gO.SetActive(false);
        var objects = availableObjects[gO.tag];
        objects.Add(gO);
        takenObjects.Remove(gO);
    }

    #region private

    protected Dictionary<string, List<GameObject>> availableObjects = new Dictionary<string, List<GameObject>>();
    List<GameObject> takenObjects = new List<GameObject>();
    int currentCapacity = 0;

    #endregion

}

[System.Serializable]
public class PoolObjectDesc
{
    public GameObject gameObject;
    public int initialCount;
}
