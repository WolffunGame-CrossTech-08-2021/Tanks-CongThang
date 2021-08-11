using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootingShellType
{
    ExplosionCharge,
    PoisonCharge,
    RapidBurst,
    ExplosionScattering,
    StraightInstant,
}

public abstract class BaseShooting: ScriptableObject
{
    public ShootingShellType type;
    public ShellType shell;
    public float baseForce;

    public abstract void Setup(GameObject obj);
    public abstract void ShootingUpdate();
    public abstract BaseShooting Clone();
}
