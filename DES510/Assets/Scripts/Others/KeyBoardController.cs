using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyBoardController : MonoBehaviour
{
    public List<KeyEvent> Keyevents = new List<KeyEvent>();
    


    private List<KeyCode> keycodesUsed = new List<KeyCode>();


    private void Start()
    {
        for (int i = 0; i < Keyevents.Count; i++)
        {
            keycodesUsed.Add(Keyevents[i].key);
        }
    }

    private void Update()
    {
        for (int i = 0; i < keycodesUsed.Count; i++)
        {
            if (Input.GetKeyDown(keycodesUsed[i]))
            {
                Keyevents[i].Down.Invoke();
            }

            if (Input.GetKeyUp(keycodesUsed[i]))
            {
                Keyevents[i].Up.Invoke();
            }
        }
    }

    public void DebugPrint()
    {
        Debug.Log($"Sucess");
    }


    public void DebugLog(string str)
    {
        Debug.Log(str);
    }

}


[System.Serializable]
public class KeyEvent {
    public string Name;
    public KeyCode key;
    public UnityEvent Down,Up;
}