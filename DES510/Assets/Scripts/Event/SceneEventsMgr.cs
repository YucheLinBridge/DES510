using Coroutine_Zenject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneEventsMgr : DialogueEventsMgr
{
    [SerializeField] private UnityEvent OnStart;

    [SerializeField] private List<PuzzleEvent> puzzleEndEvents;

    [Header("Map")]
    [SerializeField] private GameObject canvas_map;
    [SerializeField] private Animator connection;


    [Inject(Optional = true)]
    private GridsUIMgr gridsMgr;

    [Header("Dialogue")]
    [SerializeField] private List<DialogueEvent> DialogueEvents;

    private float timewait = 0;


    [Inject]
    private MusicsMgr musicMgr;

    [Inject]
    private SFXMgr sfxMgr;

    [Inject]
    private CurvedWorldController curvedWorldController;

    [Inject]
    private SettingsMgr settingsMgr;

    [Inject]
    private SimpleSaveMgr simpleSaveMgr;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnStart?.Invoke();
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


    /// <summary>
    /// Open the map UI and station
    /// </summary>
    /// <param name="station"></param>
    public void open_map(int station)
    {
        canvas_map.SetActive(true);
        Coroutine_Controller.WaitToDo(() => {
            connection.SetTrigger("SHOW");
        },1f);
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
    
    IEnumerator excecuteEvents_dialogues(int index) {
        for (int i = 0; i < DialogueEvents[index].Events.Count; i++)
        {
            DialogueEvents[index].Events[i].Invoke();
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

    private void excecute(int index)
    {
        StartCoroutine(excecuteEvents_dialogues(index));
    }

    public void End()
    {
        gridsMgr.End();
    }

    public void Wait(float time)
    {
        timewait = time;
    }

    public void PlayMusic(int index)
    {
        musicMgr.Play(index);
    }

    public void PlayMusicImmedialtely(int index)
    {
        musicMgr.PlayImmediately(index);
    }

    public void StopMusic()
    {
        musicMgr.Stop();
    }
    public void PlaySFX(int index)
    {
        sfxMgr.PlaySFX(index);
    }

    public void BendWorld(string newsetting)
    {
        curvedWorldController.Change(newsetting);
    }
    public void BendWorldImmediately(string newsetting)
    {
        curvedWorldController.ChangeImmediately(newsetting);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OpenSettingPage()
    {
        settingsMgr.ShowSetting();
    }

    public void ShowPauseMenu(bool flag)
    {
        if (flag)
        {
            settingsMgr.ShowPauseMenu();
        }
        else
        {
            settingsMgr.HidePauseMenu();
        }
    }

    public void LoadGame()
    {
        var target=simpleSaveMgr.Load();
        if (target!=null)
        {
            SceneManager.LoadScene(target);
        }
        else
        {
            Debug.LogError("There is no save!");
        }
    }

    public void SaveGame()
    {
        simpleSaveMgr.Save(SceneManager.GetActiveScene().name);
    }

}

[System.Serializable]
public class PuzzleEvent
{
    public int index;
    public List<UnityEvent> OnComplete,OnEnd;
}

[System.Serializable]
public class DialogueEvent {
    public List<UnityEvent> Events;
}
