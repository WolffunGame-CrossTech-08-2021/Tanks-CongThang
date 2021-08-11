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
    public BaseStatus prefab;
    public List<BaseStatus> pool = new List<BaseStatus>();
    public int amountToPool;
}

public class ManagerStatus : MonoBehaviour
{
    public static ManagerStatus Ins;
    public List<StatusDictionary> statusList;
    public Dictionary<Status, StatusDictionary> statusDictionary = new Dictionary<Status, StatusDictionary>();
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;

        foreach (StatusDictionary s in statusList)
        {
            statusDictionary.Add(s.prefab.id, s);
        }

        foreach (StatusDictionary full in statusDictionary.Values)
        {
            for (int i = 0; i < full.amountToPool; i++)
            {
                CreateAndAddToPool(full.prefab.id);
            }
        }
    }

    public StatusDictionary GetFull(Status status)
    {
        return statusDictionary[status];
    }

    public BaseStatus GetStatusPrefab(Status status)
    {
        return GetFull(status).prefab;
    }

    public void CreateAndAddToPool(Status status)
    {
        StatusDictionary full = GetFull(status);

        BaseStatus temp = Instantiate(full.prefab, transform);
        temp.gameObject.SetActive(false);
        full.pool.Add(temp);
    }

    public BaseStatus GetStatusObject(Status status)
    {
        StatusDictionary full = GetFull(status);

        if (full.pool.Count == 0)
        {
            CreateAndAddToPool(status);
        }

        BaseStatus s = full.pool[full.pool.Count - 1];
        full.pool.Remove(s);
        return s;
    }

    public void ReturnStatusToPool(BaseStatus bs)
    {
        StatusDictionary full = GetFull(bs.id);
        full.pool.Add(bs);
    }
}
