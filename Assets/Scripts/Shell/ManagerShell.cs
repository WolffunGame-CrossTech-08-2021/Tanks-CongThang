using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShellType
{
    Explosion,
    Straight,
    Poison,
    Time,
    Time_Extra1,
    Rapid,
    Scattering,
}

[System.Serializable]
public class ShellDictionaryItem
{
    public BaseShell prefab;
    public List<BaseShell> pool = new List<BaseShell>();
    public List<BaseShell> usingPool = new List<BaseShell>();
    public int amountToPool;
}

public class ManagerShell : MonoBehaviour
{
    public static ManagerShell Ins;
    public List<ShellDictionaryItem> shellList;
    public Dictionary<ShellType,ShellDictionaryItem> shellDictionary = new Dictionary<ShellType, ShellDictionaryItem>();
    //public List<>
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;

        foreach(ShellDictionaryItem s in shellList)
        {
            shellDictionary.Add(s.prefab.type, s);
        }
        foreach (ShellDictionaryItem shellFull in shellDictionary.Values)
        {
            for (int i = 0; i < shellFull.amountToPool; i++)
            {
                CreateAndAddToPool(shellFull.prefab.type);
            }
        }
    }

    public ShellDictionaryItem GetShellFull(ShellType type)
    {
        return shellDictionary[type];
    }

    public BaseShell GetShell(ShellType type)
    {
        return GetShellFull(type).prefab;
    }

    public void CreateAndAddToPool(ShellType type)
    {
        ShellDictionaryItem shellFull = GetShellFull(type);
        BaseShell temp = Instantiate(shellFull.prefab, transform);
        temp.gameObject.SetActive(false);
        shellFull.pool.Add(temp);
    }

    public BaseShell GetShellObject(ShellType type)
    {
        ShellDictionaryItem shellFull = GetShellFull(type);
        if (shellFull.pool.Count == 0)
        {
            CreateAndAddToPool(type);
        }
        BaseShell s = shellFull.pool[shellFull.pool.Count - 1];
        shellFull.pool.Remove(s);
        shellFull.usingPool.Add(s);
        return s;

        //for (int i = 0; i < shellFull.amountToPool; i++ )
        //{
        //    if(!shellFull.pool[i].gameObject.activeInHierarchy)
        //    {
        //        return shellFull.pool[i];
        //    }
        //}

        //CreateAndAddToPool(type);
        //shellFull.amountToPool++;
        //return shellFull.pool[shellFull.amountToPool - 1];
    }

    public void ReturnToPool(BaseShell s)
    {
        ShellDictionaryItem shellFull = GetShellFull(s.type);
        shellFull.usingPool.Remove(s);
        shellFull.pool.Add(s);
    }
}
