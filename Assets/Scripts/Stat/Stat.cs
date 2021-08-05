using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public float BaseHP;
    public float BaseAtk;
    public float MoveSpeed;
    public float BaseDef;

    public Dictionary<DamageType, float> DamageBonus; //0.1 for 10%
    public Dictionary<DamageType, float> DamageResist;

    public float GetDamageBonus(DamageType type)
    {
        return DamageBonus.ContainsKey(type)? DamageBonus[type] : 0f;
    }

    public float GetDamageResist(DamageType type)
    {
        return DamageResist.ContainsKey(type) ? DamageResist[type] : 0f;
    }
}
