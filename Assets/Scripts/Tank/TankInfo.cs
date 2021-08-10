using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankInfo : MonoBehaviour
{
    public TankHealth TankHeatlh;
    public TankMovement TankMovement;
    public TankShooting TankShooting;
    public Rigidbody Rigidbody;

    public List<BaseStatus> StatusList = new List<BaseStatus>();

    public BaseStatus AddStatus(Status status)
    {
        BaseStatus s;
        if (ManagerStatus.Ins.GetStatusPrefab(status) is IUnstacking || !Have(status))
        {
            //BaseStatus s = Instantiate(ManagerStatus.Ins.GetStatusPrefab(status), transform.position, transform.rotation, transform);
            s = ManagerStatus.Ins.GetStatusObject(status);
            s.transform.position = transform.position;
            s.transform.rotation = transform.rotation;
            s.Init();
            StatusList.Add(s);
            s.target = this;
            s.ActiveStatus();
        }
        else
        {
            s = GetBaseStatus(status);
            s.stackcount = System.Math.Min(s.stackcount++, s.maxStack);
            s.lifeTimeCount = 0;
        }
        return s;
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
