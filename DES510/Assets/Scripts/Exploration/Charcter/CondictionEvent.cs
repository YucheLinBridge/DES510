using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CondictionEvent : MonoBehaviour
{
    [SerializeField] private bool CheckAtStart;
    [SerializeField] private List<Condiction> Condictions;



    private bool pause;
    private float waittime;

    private void Start()
    {
        if (CheckAtStart) {
            CheckCondictions();
        }
    }


    private void interact()
    {
        CheckCondictions();
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

    public void CheckCondictions()
    {
        for (int i = 0; i < Condictions.Count; i++)
        {
            if (Player.Instance.GetPlayerDataBool(Condictions[i].FlagName) == Condictions[i].Flag)
            {
                StartCoroutine(ExcecuteEventByOrder(Condictions[i].Events));
            }
        }
    }

    IEnumerator ExcecuteEventByOrder(List<UnityEvent> events)
    {
        Player.Instance.SetEnable(false);
        for (int i = 0; i < events.Count; i++)
        {
            events[i]?.Invoke();
            if (pause)
            {
                yield return new WaitForSeconds(waittime);
                pause = false;
            }
        }
        Player.Instance.SetEnable(true);
    }
}
