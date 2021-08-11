﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTimeExtraBomb : BaseShell
{
    public float m_ExplosionForce = 50f;
    public float m_ExplosionRadius = 3f;

    public float m_MaxChargeTime = 0.75f;

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

            TankInfo targetInfo = targetRigidbody.GetComponent<TankInfo>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetInfo)
                continue;

            // Calculate the amount of damage the target should take based on it's distance from the shell.
            float damage = CalculateDamage(targetRigidbody.position);

            // Deal this damage to the tank.
            //targetInfo.TankHeatlh.TakeDamage(damage);
            StatusInstantDamage s = targetInfo.AddStatus(Status.InstantDamage) as StatusInstantDamage;
            s.damage = damage;
        }

        Explosion ex = ExplosionPool.Ins.GetExplosionlObject(explosionType);
        ex.gameObject.transform.position = transform.position;
        ex.gameObject.SetActive(true);
        ex.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        //Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

        // Destroy the shell.
        //Destroy(gameObject);
        ResetShellToPool();
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * m_MaxDamage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
