using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : WalkToPoint
{
    private List<PlayData_bool> playdata_bools=new List<PlayData_bool>();

    public static Player Instance;
    private bool ARRIVED => agent.isStopped;


    private void Awake()
    {
        Instance = this;
    }

    public void Transport(Vector3 des)
    {
        transform.position = des;
    }

    public bool GetPlayerDataBool(string name)
    {
        int index = playdata_bools.FindIndex(x=>x.IsName(name));
        if (index!=-1)
        {
           return  playdata_bools[index].FLAG;
        }
        else
        {
            playdata_bools.Add(new PlayData_bool(name, false));
            return false;
        }
    }

    public void SetPlayerDataBoolTrue(string name)
    {
        int index = playdata_bools.FindIndex(x => x.IsName(name));
        if (index != -1)
        {
            playdata_bools[index].SetFlag(true);
        }
        else
        {
            playdata_bools.Add(new PlayData_bool(name, true));
        }
    }

}

public struct PlayData_bool {
    private string Name;
    private bool Flag;

    public bool FLAG=>Flag;
    public void SetFlag(bool flag)
    {
        Flag=flag;
    }

    public bool IsName(string name)
    {
        return Name==name;
    }

    public PlayData_bool(string name, bool flag)
    {
        Name = name;
        Flag = flag;
    }
}
