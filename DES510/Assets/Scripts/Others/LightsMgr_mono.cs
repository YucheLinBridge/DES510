using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsMgr_mono : MonoBehaviour
{
    public List<Light> lights;


    public void TurnOff(int index)
    {
        lights[index].gameObject.SetActive(false);
    }

    public void TurnOn(int index)
    {
        lights[index].gameObject.SetActive(true);
    }


    public void TurnOffAll()
    {
        foreach (Light light in lights) {
            light.gameObject.SetActive(false);
        }
    }

    public void TurnOnAll()
    {
        foreach (Light light in lights)
        {
            light.gameObject.SetActive(true);
        }
    }
}
