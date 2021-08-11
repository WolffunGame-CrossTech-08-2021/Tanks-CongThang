using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shooting/Instant")]
public class ShootingInstant : BaseShooting
{
    public float DelayFire = 0.2f;
    public ShootingInputInstant shooting;

    public override void Setup(GameObject obj)
    {
        shooting = obj.GetComponent<ShootingInputInstant>();
        shooting.DelayFire = DelayFire;
        shooting.shell = shell;
        shooting.force = baseForce;
    }

    public override void ShootingUpdate()
    {
        shooting.ShootingUpdate();
    }

    public override BaseShooting Clone()
    {
        ShootingInstant s = new ShootingInstant();
        s.DelayFire = DelayFire;

        s.baseForce = baseForce;
        s.shell = shell;
        s.type = type;
        return s;
    }
}
