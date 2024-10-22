using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsMgr_mono : MonoBehaviour
{
    public List<Light> lights;


    public void TurnOff(int index)
    {
        lights[index].enabled = false;
    }

    public void TurnOn(int index)
    {
        lights[index].enabled = true;
    }


    public void TurnOffAll()
    {
        foreach (Light light in lights) {
            light.enabled = false;
        }
    }

    public void TurnOnAll()
    {
        foreach (Light light in lights) {
            light.enabled = true;
        }
    }
}
