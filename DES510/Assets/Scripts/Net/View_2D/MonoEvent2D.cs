using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonoEvent2D : MonoBehaviour
{
    [Inject]
    private Grids2DMgr Grids2DMgr;

    public void ShowGame()
    {
        Grids2DMgr.ShowGame();
    }
}
