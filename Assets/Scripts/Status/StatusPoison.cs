﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPoison : BaseStatus
{
    public float damagePerHit = 3f;
    public float timeDelayHit = 0.2f;
    public float timeDelayHit_Count = 0f;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        timeDelayHit_Count += Time.deltaTime;
        if (timeDelayHit_Count > timeDelayHit)
        {
            DealDamage();
            timeDelayHit_Count -= timeDelayHit;
        }
    }

    public void DealDamage()
    {
        target.TankHeatlh.TakeDamage(damagePerHit);
    }
}
