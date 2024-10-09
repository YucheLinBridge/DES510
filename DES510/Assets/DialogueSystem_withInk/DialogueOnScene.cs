using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.Events;

public class DialogueOnScene : MonoBehaviour
{
    [SerializeField] private string label;
    [SerializeField] TextAsset DialogueData;
    [SerializeField] private UnityEvent OnEnd;


    [Inject]
    private DialogueMgr Mgr;

    public void StartStory()
    {
        Mgr.StartStory(DialogueData);
        Mgr.OnDialogueEnd.AddListener(() => {
            OnEnd?.Invoke();
        });
    }
}
