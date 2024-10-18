using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TesterUI : MonoBehaviour
{
    [Inject]
    private GridsUIMgr gridsUIMgr;

    [Inject]
    private MapCreator mapCreator;

    public void GenerateTest()
    {
        gridsUIMgr.Generate(mapCreator.INDEX);
    }
}
