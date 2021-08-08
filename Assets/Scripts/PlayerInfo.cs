using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public TankInfo tankInfo;
    public GameObject currentIcon;

    void Start()
    {

    }

    public void Assign(TankInfo t)
    {
        tankInfo = t;
        tankInfo.TankShooting.ShellChanged += ChangeIcon;
        ChangeIcon();
    }

    public void ChangeIcon()
    {
        if (currentIcon)
        {
            Destroy(currentIcon);
        }
        currentIcon = Instantiate(ManagerShell.Ins.GetIcon(tankInfo.TankShooting.CurrentShell), transform);
    }
}
