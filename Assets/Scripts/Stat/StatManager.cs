using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Shell, //damage when shell hit tank directly
    Explosive, //damage when tank is hit by explode
    Collision, //damage when tank slam at other tank, both will take this damage
}

public class StatManager : MonoBehaviour
{
    public static StatManager Ins;
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public float CaculateBonus(Stat dealer, Stat target, float damage, DamageType type)
    //{
    //    return damage * (1 + dealer.GetDamageBonus(type) - target.GetDamageResist(type));
    //}
}
