using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : WalkToPoint
{
    public static Player Instance;
    private bool ARRIVED => agent.isStopped;


    private void Awake()
    {
        Instance = this;
    }

}
