using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventsManager : MonoBehaviour
{
    public List<AnimationEvent> AnimationEvents;

    private Dictionary<string, UnityEvent> eventDict;

    private void Awake()
    {
        eventDict = new Dictionary<string, UnityEvent>();
        foreach (AnimationEvent ae in AnimationEvents)
        {
            eventDict.Add(ae.Name,ae.Event);
        }
    }

    public void InvokeEvent(string eventname)
    {
        eventDict[eventname].Invoke();
    }
    public void InvokeEvent(int index)
    {
        AnimationEvents[index].Event.Invoke();
    }
}
[System.Serializable]
public class AnimationEvent{
    public string Name;
    public UnityEvent Event;
}
