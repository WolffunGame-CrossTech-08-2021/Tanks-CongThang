using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootingCharge
{
    void Setup(ShootingInputCharge i);
    void Fire(float force);
}
