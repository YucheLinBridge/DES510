using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSpeaker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Txt_Name;
    [SerializeField] private Animator Anim;


    private const string PARAM_PORTRAIT = "Portrait";

    public Vector3 LOCALPOS {
        set {
            transform.localPosition = value;
        }
        get {
            return transform.localPosition;
        }
    }

    public void SetName(string name)
    {
        if (name == string.Empty)
        {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
            Txt_Name.text = name;
        }
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void SetAnim(int portrait)
    {
        Anim.SetInteger(PARAM_PORTRAIT, portrait);
    }

}
