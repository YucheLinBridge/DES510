using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneEventsMgr : MonoBehaviour
{
    public static SceneEventsMgr Instance;


    [Inject(Optional = true)]
    private GridsUIMgr uimgr;


    private void Awake()
    {
        Instance = this;
    }


    private void generate_puzzle(int index)
    {
        uimgr.Generate(index);
        uimgr.ShowGame();
    }

}
