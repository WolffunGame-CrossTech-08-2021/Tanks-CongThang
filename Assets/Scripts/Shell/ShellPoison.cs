using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPoison : BaseShell, IShootingCharge
{
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;

    public float m_MinLaunchForce = 15f;
    public float m_MaxLaunchForce = 30f;
    public float m_MaxChargeTime = 0.75f;

    public PoisonField Field;

    public void Setup(ShootingInputCharge i)
    {
        i.m_MinLaunchForce = m_MinLaunchForce;
        i.m_MaxLaunchForce = m_MaxLaunchForce;
        i.m_MaxChargeTime = m_MaxChargeTime;
    }

    public void Fire(float force)
    {
        myRigidbody.velocity = transform.forward * force;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (1<<other.gameObject.layer == IgnoreMask.value)
        {
            return;
        }

        Explosion ex = ExplosionPool.Ins.GetExplosionlObject(explosionType);
        ex.gameObject.transform.position = transform.position;
        ex.gameObject.SetActive(true);
        ex.Play();

        Instantiate(Field, new Vector3(transform.position.x, 0.1f, transform.position.z), Quaternion.Euler(0,0,0));
        // Once the particles have finished, destroy the gameobject they are on.
        //Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        //Destroy(gameObject);
        ResetShellToPool();
    }
}
