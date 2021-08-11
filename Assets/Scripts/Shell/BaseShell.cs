using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShell : MonoBehaviour
{
    public ShellType type;
    
    public LayerMask m_TankMask;
    public LayerMask IgnoreMask;
    public float m_MaxDamage = 50f;
    public float m_MaxLifeTime = 2f;
    public float lifeTimeCount = 0f;
    public float force = 30f;
    public ExplosionType explosionType;

    public TankInfo Owner;
    //public BaseShootingInput ShootingType;

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
        ManagerShell.Ins.ReturnToPool(this);
        gameObject.SetActive(false);
        lifeTimeCount = 0f;
    }

    public virtual void Fire()
    {
        myRigidbody.velocity = transform.forward * force;
    }
}
