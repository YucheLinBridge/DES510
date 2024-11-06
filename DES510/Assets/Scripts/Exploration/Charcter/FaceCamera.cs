using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public axis Axis;
    [SerializeField] private Camera SpecificCamera;

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
                if (SpecificCamera) {
                    transform.right = -SpecificCamera.transform.forward;
                } else {
                    transform.right = -_camera.transform.forward;
                }
                
                break;
            case axis.y:
                if (SpecificCamera) {
                    transform.up = -SpecificCamera.transform.forward;
                } else {
                    transform.up = -_camera.transform.forward;
                }
                break;
            case axis.z:
                if (SpecificCamera) {
                    transform.forward = -SpecificCamera.transform.forward;
                } else {
                    transform.forward = -_camera.transform.forward;
                }
                
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
