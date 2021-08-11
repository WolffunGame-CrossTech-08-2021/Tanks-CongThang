using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShootingInputType
{
    Charge,
    Instant, //will count as keep button
    Burst,
    Scattering,
}

[System.Serializable]
public class ShootingInputDictionary
{
    public ShootingInputType keys;
    public BaseShootingInput value;
}

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;  

    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;

    public float minLaunchArrow = 15f;
    public float maxLaunchArrow = 30f;

    public float currentChargeTime = 0f;
    public float maxChargeTime = 0.75f;

    public string m_FireButton;
    public string m_ChangeButton;
    public bool m_Fired;

    public List<BaseShooting> ShootingShellList;

    public Dictionary<ShootingShellType,BaseShooting> ShootingShellDictionary = new Dictionary<ShootingShellType, BaseShooting>();
    //public ShellType CurrentShell;

    public BaseShooting CurrentShootingShell = null;

    private void Start ()
    {
        foreach(BaseShooting s in ShootingShellList)
        {
            ShootingShellDictionary.Add(s.type, s.Clone());
        }

        // The fire axis is based on the player number.
        m_FireButton = "Fire" + m_PlayerNumber;
        m_ChangeButton = "ChangeShell" + m_PlayerNumber;
        ChangeShootingShell(ShootingShellList[0].type);
    }

    public void ChangeShootingShell(ShootingShellType type)
    {
        CurrentShootingShell = ShootingShellDictionary[type];
        CurrentShootingShell.Setup(gameObject);

        ShellChanged.Invoke();
    }

    public void ClickChangeShellButton()
    {
        int c = ShootingShellList.FindIndex(x => x.type == CurrentShootingShell.type);
        c++;
        if (c == ShootingShellList.Count)
        {
            c = 0;
        }
        ChangeShootingShell(ShootingShellList[c].type);
    }

    private void Update ()
    {
        if (Input.GetButtonDown(m_ChangeButton))
        {
            ClickChangeShellButton();
        }
        CurrentShootingShell.ShootingUpdate();
    }

    //  Event Handlers --------------------------------
    public delegate void ShellChangedHandler();
    public event ShellChangedHandler ShellChanged;


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