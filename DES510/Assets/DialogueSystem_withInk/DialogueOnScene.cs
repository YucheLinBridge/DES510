using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.Events;

public class DialogueOnScene : MonoBehaviour
{
    [SerializeField] private string label;
    [SerializeField] TextAsset DialogueData;
    [SerializeField] private List<UnityEvent> OnEnd;


    [Inject]
    private DialogueMgr Mgr;

    private float timewait=0;

    public void StartStory()
    {
        Mgr.StartStory(DialogueData);
        Mgr.OnDialogueEnd.AddListener(() => {
            StartCoroutine(excecuteEndEvents());
        });
    }

    public void Wait(float time)
    {
        timewait = time;
    }

    IEnumerator excecuteEndEvents()
    {
        for (int i= 0;i<OnEnd.Count;i++)
        {
            OnEnd[i]?.Invoke();
            if (timewait>0)
            {
                yield return new WaitForSeconds(timewait);
                timewait = 0;
            }
        }
    }
}
