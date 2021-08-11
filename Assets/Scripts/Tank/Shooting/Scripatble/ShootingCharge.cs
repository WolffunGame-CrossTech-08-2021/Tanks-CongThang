using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Shooting/Charge")]
public class ShootingCharge : BaseShooting
{
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;
    public ShootingInputCharge shooting;

    public override void Setup(GameObject obj)
    {
        shooting = obj.GetComponent<ShootingInputCharge>();
        shooting.maxChargeTime = maxChargeTime;
        shooting.minLaunchForce = minLaunchForce;
        shooting.maxLaunchForce = maxLaunchForce;
        shooting.shell = shell;

        shooting.force = baseForce;

        shooting.SelfSetup();
    }

    public override void ShootingUpdate()
    {
        shooting.ShootingUpdate();
    }

    public override BaseShooting Clone()
    {
        ShootingCharge s = new ShootingCharge();
        s.minLaunchForce = minLaunchForce;
        s.maxLaunchForce = maxLaunchForce;
        s.maxChargeTime = maxChargeTime;

        s.icon = icon;
        s.baseForce = baseForce;
        s.shell = shell;
        s.type = type;
        return s;
    }
}
