using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTerminalMaterial : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_Renderer;
    [SerializeField] private Material On, Off;

    public void TurnOff()
    {
        m_Renderer.materials[1] = Off;
    }


    public void TurnOn()
    {
        m_Renderer.materials[1] = On;
    }
}
