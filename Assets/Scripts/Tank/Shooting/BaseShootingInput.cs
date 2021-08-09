using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShootingInput: MonoBehaviour
{
    public TankShooting TankShootingRef;
    public BaseShell Shell;
    public virtual void ShootingUpdate()
    {

    }

    public virtual void Setup()
    {

    }
}
