﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankInfo : MonoBehaviour
{
    public TankHealth TankHeatlh;
    public TankMovement TankMovement;
    public TankShooting TankShooting;
    public Rigidbody Rigidbody;

    public List<BaseStatus> StatusList = new List<BaseStatus>();

    public void AddStatus(Status status)
    {
        if (Have(status))
        {
            BaseStatus s = GetBaseStatus(status);
            s.stackcount = System.Math.Min(s.stackcount++, s.maxStack);
            s.lifeTimeCount = 0;
        }
        else
        {
            BaseStatus s = Instantiate(ManagerStatus.Ins.GetStatusPrefab(status), transform.position, transform.rotation, transform);
            s.Init();
            StatusList.Add(s);
            s.target = this;
        }
    }

    public void Update()
    {
        UnityEngine.Profiling.Profiler.BeginSample("---transform");

        for (int i = 0; i < 1000; i++)
        {
            transform.position = transform.position;
        }

        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("cache---transform");
        var cachetransform = transform;
        for (int i = 0; i < 1000; i++)
        {
            cachetransform.position = cachetransform.position;
        }

        UnityEngine.Profiling.Profiler.EndSample();
    }

    public BaseStatus GetBaseStatus(Status status)
    {
        return StatusList.Find(s => s.id == status);
    }

    public bool Have(Status status)
    {
        return StatusList.FindAll(s => s.id == status).Count > 0;
    }

    public void RemoveStatus(BaseStatus status)
    {
        StatusList.Remove(status);
    }
}
