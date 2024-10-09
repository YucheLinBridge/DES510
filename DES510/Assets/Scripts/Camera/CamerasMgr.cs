using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasMgr : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();


    private void Start()
    {
        ChangeCamera(0);
    }

    public void ChangeCamera(int index)
    {
        if (index >= virtualCameras.Count)
        {
            return;
        }

        for (int i=0;i<virtualCameras.Count;i++)
        {
            if (i==index)
            {
                virtualCameras[i].Priority = 100;
            }
            else
            {
                virtualCameras[i].Priority = 0;
            }
        }
    }

}
