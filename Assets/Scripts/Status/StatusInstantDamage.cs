using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInstantDamage : BaseStatus, IUnstacking
{
    public float damage = 50f;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        DealDamage();
    }

    void DealDamage()
    {
        if(target != null)
            target.TankHeatlh.TakeDamage(damage);
        Remove();
    }
}
