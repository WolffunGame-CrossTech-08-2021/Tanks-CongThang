using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem effect;
    public AudioSource audioSource;

    public float lifeTime;
    public float lifeTimeCount = 0f;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = effect.main.duration;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeCount += Time.deltaTime;
        if (lifeTimeCount > lifeTime)
        {
            effect.Stop();
            gameObject.SetActive(false);
            lifeTimeCount = 0f;
        }
    }

    public void Play()
    {
        // Play the particle system.
        effect.Play();

        // Play the explosion sound effect.
        audioSource.Play();
    }
}
