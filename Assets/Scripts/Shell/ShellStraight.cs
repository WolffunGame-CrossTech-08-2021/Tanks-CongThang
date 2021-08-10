using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellStraight : BaseShell, IShootingInstant
{
    public float m_ExplosionForce = 120f;
    public float m_ExplosionRadius = 2f;
    public float m_ScanRadius = 3f;
    public Vector3 accelarate = Vector3.zero;
    public Vector3 accelarate_rotation = Vector3.zero;

    public TankInfo targetTank;

    public float DelayFire = 0.4f;

    public float m_Force = 30f;

    public void Setup(ShootingInputInstant i)
    {
        i.DelayFire = DelayFire;
    }

    public void Fire()
    {
        myRigidbody.velocity = transform.forward * m_Force;
    }

    public override void Update()
    {
        base.Update();
        if(targetTank != null)
        {
            // Calculate the new velocity vector
            Vector3 target = targetTank.gameObject.transform.position - transform.position;
            target.Set(target.x, 0, target.z);
            //Debug.Log(target.x + " " + target.y + " " + target.z);
            myRigidbody.velocity = Vector3.SmoothDamp(myRigidbody.velocity, target / target.magnitude * m_Force, ref accelarate, 0.1f);
            float angleBetween = Vector3.Angle(transform.forward, target);
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + angleBetween, transform.rotation.eulerAngles.z));
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, m_ScanRadius, m_TankMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
                if (!targetRigidbody)
                    continue;

                TankInfo targetInfo = targetRigidbody.GetComponent<TankInfo>();
                if (!targetInfo || targetInfo.gameObject == Owner?.gameObject)
                    continue;

                targetTank = targetInfo;
            }
        }
    }


    protected void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == IgnoreMask.value)
        {
            return;
        }
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        // Go through all the colliders...
        for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            // Add an explosion force.
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            // Find the TankHealth script associated with the rigidbody.
            TankInfo targetInfo = targetRigidbody.GetComponent<TankInfo>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetInfo)
                continue;

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

        // Once the particles have finished, destroy the gameobject they are on.
        //Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        //m_ExplosionParticles.transform.parent = transform;

        // Destroy the shell.
        //Destroy(gameObject);
        ResetShellToPool();
    }

    public override void ResetShellToPool()
    {
        targetTank = null;
        base.ResetShellToPool();
    }
}
