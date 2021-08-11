using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingInputCharge : BaseShootingInput
{
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;

    private float currentLaunchForce;
    private float chargeSpeed;

    public void SelfSetup()
    {
        currentLaunchForce = minLaunchForce;
        TankShootingRef.m_AimSlider.value = minLaunchForce;
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }

    public override void ShootingUpdate()
    {
        // The slider should have a default value of the minimum launch force.
        TankShootingRef.m_AimSlider.value = minLaunchForce;

        // If the max force has been exceeded and the shell hasn't yet been launched...
        if (currentLaunchForce >= maxLaunchForce && !TankShootingRef.m_Fired)
        {
            // ... use the max force and launch the shell.
            currentLaunchForce = maxLaunchForce;
            Fire();
        }
        // Otherwise, if the fire button has just started being pressed...
        else if (Input.GetButtonDown(TankShootingRef.m_FireButton))
        {
            // ... reset the fired flag and reset the launch force.
            TankShootingRef.m_Fired = false;
            currentLaunchForce = minLaunchForce;

            // Change the clip to the charging clip and start it playing.
            TankShootingRef.m_ShootingAudio.clip = TankShootingRef.m_ChargingClip;
            TankShootingRef.m_ShootingAudio.Play();
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if (Input.GetButton(TankShootingRef.m_FireButton) && !TankShootingRef.m_Fired)
        {
            // Increment the launch force and update the slider.
            currentLaunchForce += chargeSpeed * Time.deltaTime;

            TankShootingRef.m_AimSlider.value = currentLaunchForce;
        }
        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        else if (Input.GetButtonUp(TankShootingRef.m_FireButton) && !TankShootingRef.m_Fired)
        {
            // ... launch the shell.
            Fire();
        }
    }

    public void Fire()
    {
        // Set the fired flag so only Fire is only called once.
        TankShootingRef.m_Fired = true;

        //BaseShell shellInstance = Instantiate(Shell, TankShootingRef.m_FireTransform.position, TankShootingRef.m_FireTransform.rotation);
        BaseShell shellInstance = ManagerShell.Ins.GetShellObject(shell);

        shellInstance.force = currentLaunchForce;

        Transform temp = TankShootingRef.m_FireTransform;
        shellInstance.transform.position = temp.position;
        shellInstance.transform.rotation = temp.rotation;
        shellInstance.gameObject.SetActive(true);

        shellInstance.Owner = TankShootingRef.GetComponent<TankInfo>();
        shellInstance.Fire();
        // Change the clip to the firing clip and play it.
        TankShootingRef.m_ShootingAudio.clip = TankShootingRef.m_FireClip;
        TankShootingRef.m_ShootingAudio.Play();

        // Reset the launch force. This is a precaution in case of missing button events.
        currentLaunchForce = minLaunchForce;
    }
}
