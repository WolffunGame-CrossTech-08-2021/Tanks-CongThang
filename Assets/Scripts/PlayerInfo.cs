using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public TankInfo tankInfo;
    public Image currentIcon;

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
        if (currentIcon != null)
        {
            Destroy(currentIcon.gameObject);
        }
        currentIcon = Instantiate(ManagerShell.Ins.GetIcon(tankInfo.TankShooting.CurrentShell), transform);
    }
}
