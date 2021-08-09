using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShell : MonoBehaviour
{
    public LayerMask m_TankMask;
    public LayerMask IgnoreMask;
    public float m_MaxDamage = 50f;
    public float m_MaxLifeTime = 2f;
    public float lifeTimeCount = 0f;
    public ExplosionType explosionType;

    public TankInfo Owner;
    //public BaseShootingInput ShootingType;
    public ShootingInputType shootingType;

    public Rigidbody myRigidbody;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Destroy(gameObject, m_MaxLifeTime);
    }

    public virtual void Update()
    {
        lifeTimeCount += Time.deltaTime;
        if(lifeTimeCount > m_MaxLifeTime)
        {
            ResetShellToPool();
        }
    }

    public virtual void ResetShellToPool()
    {
        gameObject.SetActive(false);
        lifeTimeCount = 0f;
    }
}
