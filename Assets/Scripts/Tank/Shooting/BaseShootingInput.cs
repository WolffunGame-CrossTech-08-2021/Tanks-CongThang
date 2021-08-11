using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShootingInput : MonoBehaviour
{
    public TankShooting TankShootingRef;
    public ShellType shell;
    public float force;

    public abstract void ShootingUpdate();
}
