using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonField : MonoBehaviour
{
    public LayerMask TankMask;
    public float PoisonRadius = 6f;
    public float PoisonFieldLast = 5f;
    public Status status;

    public void Start()
    {
        GameManager.Ins.RoundEndingEvent += Remove;
    }

    public void Remove()
    {
        GameManager.Ins.RoundEndingEvent -= Remove;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        PoisonFieldLast -= Time.deltaTime;
        if (PoisonFieldLast < 0)
        {
            Remove();
        }
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, PoisonRadius, TankMask);

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

            // Aplly effect on Tank
            targetInfo.AddStatus(status);
        }
    }
}
