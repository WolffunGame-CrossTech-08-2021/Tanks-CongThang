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

    public string m_FireButton;
    public string m_ChangeButton;
    public bool m_Fired;

    public List<ShellType> MyShells;
    public ShellType CurrentShell;

    public BaseShootingInput CurrentShootingInput = null;

    public List<ShootingInputDictionary> ListShootingInput;

    private void Start ()
    {
        // The fire axis is based on the player number.
        m_FireButton = "Fire" + m_PlayerNumber;
        m_ChangeButton = "ChangeShell" + m_PlayerNumber;
        ChangeShell(ShellType.Explosion);
    }

    public void ChangeShell(ShellType type)
    {
        if(CurrentShootingInput != null)
        {
            //Destroy(CurrentShootingInput.gameObject);
            CurrentShootingInput = null;
        }
        CurrentShell = type;
        CurrentShootingInput = ListShootingInput.Find(x => ManagerShell.Ins.GetShell(CurrentShell).shootingType == x.keys).value;
        CurrentShootingInput.Setup();
        //CurrentShootingInput.TankShootingRef = this;
        ShellChanged.Invoke();
    }

    public void ClickChangeShellButton()
    {
        int c = MyShells.IndexOf(CurrentShell);
        c++;
        if(c == MyShells.Count)
        {
            c = 0;
        }
        ChangeShell(MyShells[c]);
    }

    private void Update ()
    {
        if (Input.GetButtonDown(m_ChangeButton))
        {
            ClickChangeShellButton();
        }
        CurrentShootingInput.ShootingUpdate();
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