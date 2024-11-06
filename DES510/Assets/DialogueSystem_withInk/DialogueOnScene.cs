using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.Events;

public class DialogueOnScene : MonoBehaviour
{
    [SerializeField] private bool StartInBeginning=false;
    [SerializeField] private string label;
    [SerializeField] TextAsset DialogueData;
    [SerializeField] private UnityEvent OnStart;
    [SerializeField] private List<UnityEvent> OnEnd;
    [SerializeField] private bool ForceAuto;
    [SerializeField] private GameObject CustomDialogue;



    [Inject]
    private DialogueMgr Mgr;

    private float timewait=0;

    private void Start()
    {
        if (StartInBeginning)
        {
            StartStory();
        }
        
    }

    public void StartStory()
    {
        if (CustomDialogue)
        {
            StartStory(CustomDialogue);
        }
        else
        {
            Mgr.StartStory(DialogueData);
            Mgr.OnDialogueEnd.AddListener(() => {
                StartCoroutine(excecuteEndEvents());
            });
        }

        if (ForceAuto)
        {
            Mgr.SetAuto();
        }
        OnStart?.Invoke();
    }

    public void StartStory(GameObject customDialogue)
    {
        Mgr.StartStory(customDialogue,DialogueData);
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
