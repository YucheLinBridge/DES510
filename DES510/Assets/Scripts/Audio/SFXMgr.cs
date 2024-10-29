using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SFXMgr
{
    [Inject]
    private SFXSetting setting;

    [Inject]
    private SFX_mono.Factory factory;


    public void PlaySFX(int index)
    {
        var sfx_player = factory.Create(setting.GetSFX(index)); 
    }
}
