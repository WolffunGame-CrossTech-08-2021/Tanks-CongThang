using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExplosionType
{
    Explosion,
}

[System.Serializable]
public class ExplosionDictionary
{
    public ExplosionType key;
    public Explosion value;
    public List<Explosion> pool = new List<Explosion>();
    public int amountToPool;
}

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Ins;
    public List<ExplosionDictionary> explosionDictionary;
    //public List<>
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;
        foreach (ExplosionDictionary explo in explosionDictionary)
        {
            for (int i = 0; i < explo.amountToPool; i++)
            {
                CreateAndAddToPool(explo.key);
            }
        }
    }

    public ExplosionDictionary GetFull(ExplosionType type)
    {
        return explosionDictionary[explosionDictionary.FindIndex(s => s.key == type)];
    }

    public Explosion GetExplosion(ExplosionType type)
    {
        return GetFull(type).value;
    }

    public void CreateAndAddToPool(ExplosionType type)
    {
        ExplosionDictionary full = GetFull(type);
        Explosion temp = Instantiate(full.value, transform);
        temp.gameObject.SetActive(false);
        full.pool.Add(temp);
    }

    public Explosion GetExplosionlObject(ExplosionType type)
    {
        ExplosionDictionary full = GetFull(type);
        for (int i = 0; i < full.amountToPool; i++)
        {
            if (!full.pool[i].gameObject.activeInHierarchy)
            {
                return full.pool[i];
            }
        }

        CreateAndAddToPool(type);
        full.amountToPool++;
        return full.pool[full.amountToPool - 1];
    }
}
