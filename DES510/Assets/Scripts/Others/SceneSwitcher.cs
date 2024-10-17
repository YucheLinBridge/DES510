using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GoTo(string targetname)
    {
        SceneManager.LoadScene(targetname);
    }
}
