using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSaveMgr
{
    private const string SAVENAME = "GAME_PROGRESS";

    public void Save(string scenename) {
        PlayerPrefs.SetString(SAVENAME,scenename);
    }

    public bool CheckIfLoadable()
    {
        return PlayerPrefs.HasKey(SAVENAME);
    }

    public string Load()
    {
        if (CheckIfLoadable())
        {
            return PlayerPrefs.GetString(SAVENAME);
        }
        else
        {
            return null;
        }
    }
}
