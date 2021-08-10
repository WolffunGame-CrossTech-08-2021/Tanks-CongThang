using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    InstantDamage,
    Poison,
}

[System.Serializable]
public class StatusDictionary
{
    public Status key;
    public BaseStatus value;
    public List<BaseStatus> pool = new List<BaseStatus>();
    public int amountToPool;
}

public class ManagerStatus : MonoBehaviour
{
    public static ManagerStatus Ins;
    public List<StatusDictionary> statusDictionary;
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;
        foreach (StatusDictionary full in statusDictionary)
        {
            for (int i = 0; i < full.amountToPool; i++)
            {
                CreateAndAddToPool(full.key);
            }
        }
    }

    public StatusDictionary GetFull(Status status)
    {
        return statusDictionary[statusDictionary.FindIndex(s => s.key == status)];
    }

    public BaseStatus GetStatusPrefab(Status status)
    {
        return GetFull(status).value;
    }

    public void CreateAndAddToPool(Status status)
    {
        StatusDictionary full = GetFull(status);

        BaseStatus temp = Instantiate(full.value, transform);
        temp.gameObject.SetActive(false);
        full.pool.Add(temp);
    }

    public BaseStatus GetStatusObject(Status status)
    {
        StatusDictionary full = GetFull(status);
        for (int i = 0; i < full.amountToPool; i++)
        {
            if (!full.pool[i].gameObject.activeInHierarchy)
            {
                return full.pool[i];
            }
        }

        CreateAndAddToPool(status);
        full.amountToPool++;
        return full.pool[full.amountToPool - 1];
    }
}
