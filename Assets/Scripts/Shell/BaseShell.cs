using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShell : MonoBehaviour
{
    public LayerMask m_TankMask;
    public float m_MaxDamage = 50f;
    public float m_MaxLifeTime = 2f;
    public TankInfo Owner;
    public ShootingInputType ShootingType;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }
}
