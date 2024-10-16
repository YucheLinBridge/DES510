using Codice.Client.Common.GameUI;
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


    private void Awake()
    {
        Instance = this;
    }


    private void generate_puzzle(int index)
    {
        gridsMgr.Generate(index);
        int puzzle_i = puzzleEndEvents.FindIndex(x=>x.index==index);


        if (puzzle_i!=-1)
        {
            gridsMgr.OnCompleted.AddListener(() => {
                puzzleEndEvents[puzzle_i].OnComplete?.Invoke();
            });

            gridsMgr.OnEnd.AddListener(() => {
                puzzleEndEvents[puzzle_i].OnEnd?.Invoke();
            });
        }

        gridsMgr.ShowGame();
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


}

[System.Serializable]
public class PuzzleEvent
{
    public int index;
    public UnityEvent OnComplete,OnEnd;
}
