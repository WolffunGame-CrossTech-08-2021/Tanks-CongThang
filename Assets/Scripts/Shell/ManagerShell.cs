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
}

[System.Serializable]
public class ShellDictionary
{
    public ShellType key;
    public BaseShell value;
    public Image icon;
    public List<BaseShell> pool = new List<BaseShell>();
    public int amountToPool;
}

public class ManagerShell : MonoBehaviour
{
    public static ManagerShell Ins;
    public List<ShellDictionary> shellDictionary;
    //public List<>
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;
        foreach (ShellDictionary shellFull in shellDictionary)
        {
            for (int i = 0; i < shellFull.amountToPool; i++)
            {
                CreateAndAddToPool(shellFull.key);
            }
        }
    }

    public ShellDictionary GetShellFull(ShellType type)
    {
        return shellDictionary[shellDictionary.FindIndex(s => s.key == type)];
    }

    public BaseShell GetShell(ShellType type)
    {
        return GetShellFull(type).value;
    }

    public Image GetIcon(ShellType type)
    {
        return GetShellFull(type).icon;
    }

    public void CreateAndAddToPool(ShellType type)
    {
        ShellDictionary shellFull = GetShellFull(type);
        BaseShell temp = Instantiate(shellFull.value, transform);
        temp.gameObject.SetActive(false);
        shellFull.pool.Add(temp);
    }

    public BaseShell GetShellObject(ShellType type)
    {
        ShellDictionary shellFull = GetShellFull(type);
        for (int i = 0; i < shellFull.amountToPool; i++ )
        {
            if(!shellFull.pool[i].gameObject.activeInHierarchy)
            {
                return shellFull.pool[i];
            }
        }

        CreateAndAddToPool(type);
        shellFull.amountToPool++;
        return shellFull.pool[shellFull.amountToPool - 1];
    }
}
