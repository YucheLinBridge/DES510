using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform stopPoint;
    [SerializeField] private List<UnityEvent> InteractiveEvents;

    protected bool pause;
    protected float waittime;
    protected bool triggered;

    public void OnClicked()
    {
        Debug.Log($"Clicked on {this.name}");
        Player.Instance.Walk(stopPoint.position);
        Player.Instance.AddEventOnArrive(interact);
    }

    protected virtual void interact() {
        StartCoroutine(ExcecuteEventByOrder());
    }

    public void Wait(float time)
    {
        pause = true;
        waittime = time;
    }

    public void TransportPlayerTo(Transform despoint)
    {
        Player.Instance.Transport(despoint.position);
    }

    IEnumerator ExcecuteEventByOrder()
    {
        Player.Instance.SetEnable(false);
        for (int i=0;i< InteractiveEvents.Count;i++)
        {
            InteractiveEvents[i]?.Invoke();
            if (pause)
            {
                yield return new WaitForSeconds(waittime);
                pause = false;
            }
        }
        Player.Instance.SetEnable(true);
    }

}

