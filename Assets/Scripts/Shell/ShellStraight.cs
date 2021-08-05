using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellStraight : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float m_MaxDamage = 50f;
    public float m_ExplosionForce = 120f;
    public float m_MaxLifeTime = 2f;
    public float m_ExplosionRadius = 3f;
    public float m_ScanRadius = 6f;
    public TankHealth Owner;
    public Vector3 accelarate = Vector3.zero;
    public Vector3 accelarate_rotation = Vector3.zero;

    public float m_Force = 15f;


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }

    public void Shoot(float force)
    {
        m_Force = force;
        GetComponent<Rigidbody>().velocity = transform.forward * m_Force;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ScanRadius, m_TankMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            // Find the TankHealth script associated with the rigidbody.
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth || targetHealth.gameObject == Owner?.gameObject)
                continue;

            // Calculate the new velocity vector
            Vector3 target = targetHealth.gameObject.transform.position - transform.position;
            target.Set(target.x, 0, target.z);
            //Debug.Log(target.x + " " + target.y + " " + target.z);
            GetComponent<Rigidbody>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody>().velocity, target / target.magnitude * m_Force, ref accelarate, 0.1f);
            //transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(transform.rotation.eulerAngles, -target, ref accelarate_rotation, 0.1f));
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
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
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
                continue;

            // Calculate the amount of damage the target should take based on it's distance from the shell.
            float damage = CalculateDamage(targetRigidbody.position);

            // Deal this damage to the tank.
            targetHealth.TakeDamage(damage);
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
