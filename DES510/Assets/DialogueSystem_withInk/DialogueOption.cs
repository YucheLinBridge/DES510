using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class DialogueOption : DialogueObj
{
    [Header("Option Setting")]
   [SerializeField]private Button Btn;

    public void SetClicked(UnityAction action)
    {
        Btn.onClick.AddListener(action);
    }
}
