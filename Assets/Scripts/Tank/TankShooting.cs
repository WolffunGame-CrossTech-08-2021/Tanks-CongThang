using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ShootingInputType
{
    Instant,
    Charge,
    Trigger,
}

[System.Serializable]
public class ShootingInputDictionary
{
    public ShootingInputType key;
    public BaseShootingInput value;
}

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;  
    
    public ShellExplosion m_Shell_Explosion;
    public ShellStraight m_Shell_Straight;

    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

    public string m_FireButton;               
    public bool m_Fired;

    public ShellType CurrentShell;

    public List<ShootingInputDictionary> ListShootingInput;
    public BaseShootingInput CurrentShootingInput;

    private void OnEnable()
    {
        ChangeShell(ShellType.Explosion);
    }

    private void Start ()
    {
        // The fire axis is based on the player number.
        m_FireButton = "Fire" + m_PlayerNumber;
        ChangeShell(ShellType.Explosion);
    }

    public void ChangeShell(ShellType type)
    {
        CurrentShell = type;
        CurrentShootingInput = GetShootingInput(ManagerShell.Ins.GetShell(CurrentShell).ShootingType);
        CurrentShootingInput.TankShootingRef = this;
    }

    public BaseShootingInput GetShootingInput(ShootingInputType type)
    {
        return ListShootingInput[ListShootingInput.FindIndex(s => s.key == type)].value;
    }

    private void Update ()
    {
        CurrentShootingInput.ShootingUpdate();
    }


    //private void Fire ()
    //{
    //    // Set the fired flag so only Fire is only called once.
    //    m_Fired = true;

    //    Debug.Log(m_Shell_Explosion.m_MaxDamage);

    //    switch (ShellWheel)
    //    {
    //        case ShellType.Explosion:
    //            // Create an instance of the shell and store a reference to it's rigidbody.
    //            ShellExplosion shellInstance = Instantiate(m_Shell_Explosion, m_FireTransform.position, m_FireTransform.rotation);
    //            shellInstance.Fire(m_CurrentLaunchForce);
    //            break;
    //        case ShellType.Straight:
    //            Quaternion rotationTemp = Quaternion.Euler(0, m_FireTransform.rotation.eulerAngles.y, m_FireTransform.rotation.eulerAngles.z);
    //            ShellStraight shellStraight = Instantiate(m_Shell_Straight, m_FireTransform.position, rotationTemp);
    //            shellStraight.Owner = GetComponent<TankInfo>();
    //            shellStraight.Fire(m_CurrentLaunchForce);
    //            break;
    //    }


    //    // Change the clip to the firing clip and play it.
    //    m_ShootingAudio.clip = m_FireClip;
    //    m_ShootingAudio.Play ();

    //    // Reset the launch force.  This is a precaution in case of missing button events.
    //    m_CurrentLaunchForce = m_MinLaunchForce;
    //}
}