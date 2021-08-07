using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    Poison,
}

[System.Serializable]
public class StatusDictionary
{
    public Status key;
    public BaseStatus value;
}

public class ManagerStatus : MonoBehaviour
{
    public static ManagerStatus Ins;
    public List<StatusDictionary> statusDictionary;
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;
    }

    public BaseStatus GetStatusPrefab(Status status)
    {
        return statusDictionary[statusDictionary.FindIndex(s => s.key == status)].value;
    }
}
