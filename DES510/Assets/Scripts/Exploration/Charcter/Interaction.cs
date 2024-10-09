using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform stopPoint;

    [SerializeField] private List<UnityEvent> InteractiveEvents;

    private bool pause;
    private float waittime;
    private bool triggered;

    public void OnClicked()
    {
        Player.Instance.Walk(stopPoint.position);
        Player.Instance.AddEventOnArrive(interact);
    }

    private void interact() {
        StartCoroutine(ExcecuteEventByOrder());
    }

    public void Wait(float time)
    {
        pause = true;
        waittime = time;
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
