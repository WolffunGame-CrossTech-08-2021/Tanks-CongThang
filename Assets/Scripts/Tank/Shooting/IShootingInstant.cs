using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootingInstant
{
    void Setup(ShootingInputInstant i);
    void Fire();
}
