using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonoEventUI : MonoBehaviour
{
    [Inject]
    private GridsUIMgr gridsUIMgr;

    public void ShowGame()
    {
        gridsUIMgr.ShowGame();
    }

}
