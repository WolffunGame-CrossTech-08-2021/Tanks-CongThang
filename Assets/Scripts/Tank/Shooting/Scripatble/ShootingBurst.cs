using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shooting/Burst")]
public class ShootingBurst : BaseShooting
{
    public int numberOfShell = 3;
    public float DelayFire = 0.2f;
    public float delayBurst = 0.05f;
    public ShootingInputBurst shooting;

    public override void Setup(GameObject obj)
    {
        shooting = obj.GetComponent<ShootingInputBurst>();
        shooting.numberOfShell = numberOfShell;
        shooting.DelayFire = DelayFire;
        shooting.delayBurst = delayBurst;
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
        ShootingBurst s = new ShootingBurst();
        s.numberOfShell = numberOfShell;
        s.DelayFire = DelayFire;
        s.delayBurst = delayBurst;

        s.icon = icon;
        s.baseForce = baseForce;
        s.shell = shell;
        s.type = type;
        return s;
    }
}
