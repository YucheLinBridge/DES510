using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Zenject;

public class WalkToPoint : MonoBehaviour
{
    [SerializeField]protected NavMeshAgent agent;
    
    [HideInInspector]public UnityEvent OnArrived = new UnityEvent();
    private bool moving;
    private Vector3 des;
    private bool enable=true;

    [Inject(Optional =true)]
    private FX_WalkTarget_mono.Factory fx_factory;

    private FX_WalkTarget_mono fx_target;

   public void Walk(Vector3 des)
   {
        if (!enable)
        {
            return;
        }else if (moving &&fx_target!=null)
        {
            fx_target.Stop();
        }


        OnArrived.RemoveAllListeners();
        agent.SetDestination(des);
        this.des=des;
        this.des.y = transform.position.y;
        //Debug.Log($"{name} walk to {des}");
        moving = true;


        if (fx_factory!=null)
        {
            fx_target = fx_factory.Create();
            fx_target.POS = des;
        }
        
   }

    private void Update()
    {
        if (moving) {
            if ((des-transform.position).sqrMagnitude<=.1f)
            {
                //Debug.Log("Arrived");
                moving = false;
                //agent.enabled=false;
                OnArrived?.Invoke();
                OnArrived.RemoveAllListeners();
                //agent.enabled = true;

                if (fx_target!=null)
                {
                    fx_target.Stop();
                }
            }
        }
    }

    public void AddEventOnArrive(Action action)
    {
        OnArrived.AddListener(() => {
            action?.Invoke();
        });
    }

    public void SetEnable(bool flag) {
        enable = flag;
        agent.enabled = flag;
        Debug.Log($"Enable:{enable}");
    }


}
