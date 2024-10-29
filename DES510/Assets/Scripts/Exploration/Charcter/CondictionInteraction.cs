using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CondictionInteraction : Interaction
{
    [SerializeField] private List<Condiction> Condictions;

    protected override void interact()
    {
        CheckCondictions();
    }



    public void CheckCondictions()
    {
        for (int i=0;i<Condictions.Count;i++)
        {
            //Debug.Log($"{Condictions[i].FlagName}={Player.Instance.GetPlayerDataBool(Condictions[i].FlagName)}"); 
            if (Player.Instance.GetPlayerDataBool(Condictions[i].FlagName) == Condictions[i].Flag)
            {
                StartCoroutine(ExcecuteEventByOrder(Condictions[i].Events));
                break;
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

[System.Serializable]
public class Condiction
{
    public string Label;
    public string FlagName;
    public bool Flag;
    public List<UnityEvent> Events;
}
