using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasMgr : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();
    [SerializeField] private int defaultCamera;

    private void Start()
    {
        ChangeCamera(defaultCamera);
    }

    public void ChangeCamera(int index)
    {
        Debug.Log($"Change camera to index={index}");

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
