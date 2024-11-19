using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SimpleSave_LoadBtn : MonoBehaviour
{
    [SerializeField]
    private Button btnLoad;

    [Inject]
    private SimpleSaveMgr mgr;

    // Start is called before the first frame update
    void Start()
    {
        btnLoad.interactable=mgr.CheckIfLoadable();
    }

}
