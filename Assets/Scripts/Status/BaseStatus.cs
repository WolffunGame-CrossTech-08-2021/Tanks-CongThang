using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : MonoBehaviour
{
    public Status id;

    public float lifeTime;
    public float lifeTimeCount = 0f;
    public TankInfo target;
    public bool triggerEvent;

    public int maxStack = 1;
    public int stackcount;

    public string Name;
    public string Description;

    // Update is called once per frame
    protected virtual void Update()
    {
        if(lifeTimeCount > lifeTime)
        {
            RemoveStatus();
        }
        else
        {
            lifeTimeCount += Time.deltaTime;
        }
    }

    public virtual void Init()
    {

    }

    public virtual void RemoveStatus()
    {
        if (triggerEvent)
            if (StatusRemove != null)
                StatusRemove.Invoke();
        target.RemoveStatus(this);
        Destroy(this.gameObject);
    }

    //  Event Handlers --------------------------------
    public delegate void StatusRemoveHandler();
    public event StatusRemoveHandler StatusRemove;
}
