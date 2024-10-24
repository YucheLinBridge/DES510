using Codice.Client.Common.GameUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SceneEventsMgr : DialogueEventsMgr
{
    [SerializeField] private List<PuzzleEvent> puzzleEndEvents;


    [Inject(Optional = true)]
    private GridsUIMgr gridsMgr;

    private float timewait = 0;


    private void Awake()
    {
        Instance = this;
    }


    public void generate_puzzle(int index)
    {
        gridsMgr.Generate(index);
        int puzzle_i = puzzleEndEvents.FindIndex(x=>x.index==index);


        if (puzzle_i!=-1)
        {
            gridsMgr.OnCompleted.AddListener(() => {
                StartCoroutine(excecuteEvents_completed(puzzle_i));
            });

            gridsMgr.OnEnd.AddListener(() => {
                StartCoroutine(excecuteEvents_end(puzzle_i));
            });
        }

        gridsMgr.ShowGame();
    }

    IEnumerator excecuteEvents_completed(int index) {
        for (int i=0;i< puzzleEndEvents[index].OnComplete.Count;i++)
        {
            puzzleEndEvents[index].OnComplete[i].Invoke();
            if (timewait>0)
            {
                yield return new WaitForSeconds(timewait);
            }
            
            timewait = 0;
        }
    }

    IEnumerator excecuteEvents_end(int index) {
        for (int i = 0; i < puzzleEndEvents[index].OnEnd.Count; i++)
        {
            puzzleEndEvents[index].OnEnd[i].Invoke();
            if (timewait > 0)
            {
                yield return new WaitForSeconds(timewait);
            }
            timewait = 0;
        }
    }


    private void addRelationshipLv((int,int)rawdata) //Because I need to call this method with SendMessage
    {
        int character = rawdata.Item1;
        int value= rawdata.Item2;
    }



    public void End()
    {
        gridsMgr.End();
    }

    public void Wait(float time)
    {
        timewait = time;
    }


}

[System.Serializable]
public class PuzzleEvent
{
    public int index;
    public List<UnityEvent> OnComplete,OnEnd;
}
