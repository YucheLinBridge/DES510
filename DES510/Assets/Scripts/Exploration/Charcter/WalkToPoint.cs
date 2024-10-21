using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class WalkToPoint : MonoBehaviour
{
    [SerializeField]protected NavMeshAgent agent;
    
    [HideInInspector]public UnityEvent OnArrived = new UnityEvent();
    private bool moving;
    private Vector3 des;
    private bool enable=true;

   public void Walk(Vector3 des)
   {
        if (!enable)
        {
            return;
        }
        OnArrived.RemoveAllListeners();
        agent.SetDestination(des);
        this.des=des;
        this.des.y = transform.position.y;
        //Debug.Log($"{name} walk to {des}");
        moving = true;
   }

    private void Update()
    {
        if (moving) {
            if ((des-transform.position).sqrMagnitude<=.1f)
            {
                //Debug.Log("Arrived");
                moving = false;
                agent.enabled = false;
                OnArrived?.Invoke();
                OnArrived.RemoveAllListeners();
                agent.enabled=true;
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
    }

}
