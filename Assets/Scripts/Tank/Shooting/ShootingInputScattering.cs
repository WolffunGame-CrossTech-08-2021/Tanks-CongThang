using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingInputScattering : BaseShootingInput
{
    public int numberOfShell;
    //public int numberOfShell_Count;
    private float DelayFire = 0.2f; //make sure no autoclick spam attack
    private float DelayFire_Count = 0f;

    public float scatteringAngle = 0f;

    public override void Setup()
    {
        base.Setup();
        // The slider should have a default value of the minimum launch force.
        TankShootingRef.m_AimSlider.value = 15f;

        Shell = ManagerShell.Ins.GetShell(TankShootingRef.CurrentShell);
        (Shell as IShootingScattering).Setup(this);
    }

    public override void ShootingUpdate()
    {
        if (TankShootingRef.m_Fired)
        {
            DelayFire_Count += Time.deltaTime;
            if (DelayFire_Count > DelayFire)
            {
                DelayFire_Count -= DelayFire;
                TankShootingRef.m_Fired = false;
            }
        }
        if (Input.GetButtonDown(TankShootingRef.m_FireButton) && !TankShootingRef.m_Fired)
        {
            for(int i = 0; i < numberOfShell; i++)
            {
                Fire(scatteringAngle * (i - (float) (numberOfShell - 1) / 2f));
            }
          
            TankShootingRef.m_Fired = true;
        }
    }

    private void Fire(float angle)
    {
        // Set the fired flag so only Fire is only called once.
        TankShootingRef.m_Fired = true;

        Transform temp = TankShootingRef.m_FireTransform;
        Quaternion rotationTemp = Quaternion.Euler(0, temp.rotation.eulerAngles.y + angle, temp.rotation.eulerAngles.z);
        //BaseShell shellInstance = Instantiate(Shell, TankShootingRef.m_FireTransform.position, rotationTemp);
        BaseShell shellInstance = ManagerShell.Ins.GetShellObject(TankShootingRef.CurrentShell);
        shellInstance.transform.position = temp.position;
        shellInstance.transform.rotation = rotationTemp;
        shellInstance.gameObject.SetActive(true);

        shellInstance.Owner = TankShootingRef.GetComponent<TankInfo>();
        if (shellInstance is IShootingScattering)
        {
            (shellInstance as IShootingScattering).Fire();
        }
        // Change the clip to the firing clip and play it.
        TankShootingRef.m_ShootingAudio.clip = TankShootingRef.m_FireClip;
        TankShootingRef.m_ShootingAudio.Play();
    }
}
