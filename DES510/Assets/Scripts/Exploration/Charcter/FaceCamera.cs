using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public axis Axis;


    private static Camera _camera;

    void Start()
    {
        if (_camera==null) {
        _camera = Camera.main;  
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (Axis)
        {
            case axis.x:
                transform.right = -_camera.transform.forward;
                break;
            case axis.y:
                transform.up = -_camera.transform.forward;
                break;
            case axis.z:
                transform.forward = -_camera.transform.forward;
                break;
            default:
                break;
        }
    }

    public enum axis
    {
        x,y, z
    }
}
