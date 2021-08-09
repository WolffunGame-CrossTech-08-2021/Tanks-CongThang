using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingInputCharge : BaseShootingInput
{
    public float m_MinLaunchForce = 15f;
    public float m_MaxLaunchForce = 30f;
    public float m_MaxChargeTime = 0.75f;

    private float m_CurrentLaunchForce;
    private float m_ChargeSpeed;

    private void Start()
    {
        
    }

    public override void Setup()
    {
        base.Setup();
        Shell = ManagerShell.Ins.GetShell(TankShootingRef.CurrentShell);
        (Shell as IShootingCharge).Setup(this);

        m_CurrentLaunchForce = m_MinLaunchForce;
        TankShootingRef.m_AimSlider.value = m_MinLaunchForce;
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    public override void ShootingUpdate()
    {
        // The slider should have a default value of the minimum launch force.
        TankShootingRef.m_AimSlider.value = m_MinLaunchForce;

        // If the max force has been exceeded and the shell hasn't yet been launched...
        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !TankShootingRef.m_Fired)
        {
            // ... use the max force and launch the shell.
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }
        // Otherwise, if the fire button has just started being pressed...
        else if (Input.GetButtonDown(TankShootingRef.m_FireButton))
        {
            // ... reset the fired flag and reset the launch force.
            TankShootingRef.m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;

            // Change the clip to the charging clip and start it playing.
            TankShootingRef.m_ShootingAudio.clip = TankShootingRef.m_ChargingClip;
            TankShootingRef.m_ShootingAudio.Play();
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if (Input.GetButton(TankShootingRef.m_FireButton) && !TankShootingRef.m_Fired)
        {
            // Increment the launch force and update the slider.
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

            TankShootingRef.m_AimSlider.value = m_CurrentLaunchForce;
        }
        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        else if (Input.GetButtonUp(TankShootingRef.m_FireButton) && !TankShootingRef.m_Fired)
        {
            // ... launch the shell.
            Fire();
        }
    }

    private void Fire()
    {
        // Set the fired flag so only Fire is only called once.
        TankShootingRef.m_Fired = true;

        //BaseShell shellInstance = Instantiate(Shell, TankShootingRef.m_FireTransform.position, TankShootingRef.m_FireTransform.rotation);
        BaseShell shellInstance = ManagerShell.Ins.GetShellObject(TankShootingRef.CurrentShell);
        Transform temp = TankShootingRef.m_FireTransform;
        shellInstance.transform.position = temp.position;
        shellInstance.transform.rotation = temp.rotation;
        shellInstance.gameObject.SetActive(true);

        shellInstance.Owner = TankShootingRef.GetComponent<TankInfo>();
        if (shellInstance is IShootingCharge)
        {
            (shellInstance as IShootingCharge).Fire(m_CurrentLaunchForce);
        }

        // Change the clip to the firing clip and play it.
        TankShootingRef.m_ShootingAudio.clip = TankShootingRef.m_FireClip;
        TankShootingRef.m_ShootingAudio.Play();

        // Reset the launch force.  This is a precaution in case of missing button events.
        m_CurrentLaunchForce = m_MinLaunchForce;
    }
}
