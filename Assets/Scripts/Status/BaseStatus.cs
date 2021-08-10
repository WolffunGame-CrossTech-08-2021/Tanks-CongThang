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

    public virtual void Start()
    {
        GameManager.Ins.RoundEndingEvent += ResetStatusToPool;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(lifeTimeCount > lifeTime)
        {
            Remove();
        }
        else
        {
            lifeTimeCount += Time.deltaTime;
        }
    }

    public virtual void Init()
    {

    }

    public virtual void Remove()
    {
        if (triggerEvent)
            if (StatusRemove != null)
                StatusRemove.Invoke();
        if(target!=null)
            target.RemoveStatus(this);

        ResetStatusToPool();
        //Destroy(this.gameObject);
    }

    public void ActiveStatus()
    {
        transform.parent = target.transform;
        gameObject.SetActive(true);
    }

    public virtual void ResetStatusToPool()
    {
        transform.parent = ManagerStatus.Ins.transform;
        gameObject.SetActive(false);
        lifeTimeCount = 0f;
        stackcount = 0;
        target = null;
    }

    //  Event Handlers --------------------------------
    public delegate void StatusRemoveHandler();
    public event StatusRemoveHandler StatusRemove;
}
