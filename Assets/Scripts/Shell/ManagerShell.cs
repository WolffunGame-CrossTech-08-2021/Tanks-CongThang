using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShellType
{
    Explosion,
    Straight,
    Poison,
    Time,
}

[System.Serializable]
public class ShellDictionary
{
    public ShellType key;
    public BaseShell value;
}

public class ManagerShell : MonoBehaviour
{
    public static ManagerShell Ins;
    public List<ShellDictionary> shellDictionary;
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;
    }

    public BaseShell GetShell(ShellType type)
    {
        return shellDictionary[shellDictionary.FindIndex(s => s.key == type)].value;
    }
}
