using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScattering : BaseShell, IShootingScattering
{
    public float m_Force = 30f;
    public int numberOfShell = 3;
    public float scatteringAngle = 30f;

    public void Fire()
    {
        myRigidbody.velocity = transform.forward * m_Force;
    }

    public override void Update()
    {
        base.Update();
    }

    public void Setup(ShootingInputScattering i)
    {
        i.numberOfShell = numberOfShell;
        i.scatteringAngle = scatteringAngle;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == IgnoreMask.value)
        {
            return;
        }

        // Go through all the colliders...
        TankInfo targetInfo = other.GetComponent<TankInfo>();

        // If they don't have a rigidbody, go on to the next collider.
        if (targetInfo)
        {
            // Calculate the amount of damage the target should take based on it's distance from the shell.
            float damage = m_MaxDamage;

            // Deal this damage to the tank.
            StatusInstantDamage s = targetInfo.AddStatus(Status.InstantDamage) as StatusInstantDamage;
            s.damage = damage;
            //targetInfo.TankHeatlh.TakeDamage(damage);
        }

        Explosion ex = ExplosionPool.Ins.GetExplosionlObject(explosionType);
        ex.gameObject.transform.position = transform.position;
        ex.gameObject.SetActive(true);
        ex.Play();

        //Destroy(gameObject);
        ResetShellToPool();
    }
}
