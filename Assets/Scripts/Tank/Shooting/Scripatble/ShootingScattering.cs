using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shooting/Scattering")]
public class ShootingScattering : BaseShooting
{
    public int numberOfShell = 3;
    public float DelayFire = 0.2f;
    public float scatteringAngle = 15f;
    public ShootingInputScattering shooting;

    public override void Setup(GameObject obj)
    {
        shooting = obj.GetComponent<ShootingInputScattering>();
        shooting.numberOfShell = numberOfShell;
        shooting.DelayFire = DelayFire;
        shooting.scatteringAngle = scatteringAngle;
        shooting.shell = shell;

        shooting.force = baseForce;
    }

    public override void ShootingUpdate()
    {
        shooting.ShootingUpdate();
    }

    public override BaseShooting Clone()
    {
        ShootingScattering s = new ShootingScattering();
        s.numberOfShell = numberOfShell;
        s.DelayFire = DelayFire;
        s.scatteringAngle = scatteringAngle;

        s.baseForce = baseForce;
        s.shell = shell;
        s.type = type;
        return s;
    }
}
