using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTime : BaseShell, IShootingCharge
{
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;

    public float m_ExplosionRadius = 5f;

    public float m_MinLaunchForce = 3f;
    public float m_MaxLaunchForce = 5f;
    public float m_MaxChargeTime = 0.5f;

    public float TimeCountDown = 0f;
    public float Force = 15f;

    public ShellTimeExtraBomb extra;

    public void Setup(ShootingInputCharge i)
    {
        i.m_MinLaunchForce = m_MinLaunchForce;
        i.m_MaxLaunchForce = m_MaxLaunchForce;
        i.m_MaxChargeTime = m_MaxChargeTime;
    }

    public void Fire(float force)
    {
        TimeCountDown = force;
        GetComponent<Rigidbody>().velocity = transform.forward * Force;
    }

    private void Update()
    {
        TimeCountDown -= Time.deltaTime;
        if(TimeCountDown<0)
        {
            Explosive();
        }
    }

    protected void Explosive()
    {
        Instantiate(extra, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(new Vector3(-90, 0, 0))).Fire(Force*2/3);
        Instantiate(extra, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(new Vector3(-90, 0, 0))).Fire(Force/2);
        Instantiate(extra, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(new Vector3(-90, 0, 0))).Fire(Force/3);

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

            TankInfo targetInfo = targetRigidbody.GetComponent<TankInfo>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetInfo)
                continue;

            // Deal this damage to the tank.
            targetInfo.TankHeatlh.TakeDamage(m_MaxDamage);
        }

        // Unparent the particles from the shell.
        m_ExplosionParticles.transform.parent = null;

        // Play the particle system.
        m_ExplosionParticles.Play();

        // Play the explosion sound effect.
        m_ExplosionAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

        // Destroy the shell.
        Destroy(gameObject);
    }
}
