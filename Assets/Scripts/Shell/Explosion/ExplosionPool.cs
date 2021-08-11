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
    public Explosion value;
    public List<Explosion> pool = new List<Explosion>();
    public int amountToPool;
}

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Ins;
    public List<ExplosionDictionary> explosionList;
    public Dictionary<ExplosionType, ExplosionDictionary> explosionDictionary = new Dictionary<ExplosionType, ExplosionDictionary>();
    //public List<>
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;
        foreach (ExplosionDictionary e in explosionList)
        {
            explosionDictionary.Add(e.value.type, e);
        }

        foreach (ExplosionDictionary explo in explosionDictionary.Values)
        {
            for (int i = 0; i < explo.amountToPool; i++)
            {
                CreateAndAddToPool(explo.value.type);
            }
        }
    }

    public ExplosionDictionary GetFull(ExplosionType type)
    {
        return explosionDictionary[type];
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
        if (full.pool.Count == 0)
        {
            CreateAndAddToPool(type);
        }
        Explosion e = full.pool[full.pool.Count - 1];
        full.pool.Remove(e);
        return e;
    }

    public void ReturnExplosionToPool(Explosion explosion)
    {
        ExplosionDictionary full = GetFull(explosion.type);
        full.pool.Add(explosion);
    }
}
