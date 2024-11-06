using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueObj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Content;


    [Header("Setting")]
    [SerializeField] private float TypingSpeed;
    [SerializeField] private float AutoWaitTime = 1;

    [Header("Speaker")]
    [SerializeField] private Transform speakerParent;
    [SerializeField] private Transform rightPoint,centerPoint,leftPoint;
    private DialogueSpeaker speaker;


    private bool typing;
    private string texttoshow;
    private bool auto;

    public bool END => !typing;

    public void Show(string content,bool auto)
    {
        if (content==null)
        {
            gameObject.SetActive(false);
            return;
        }

        this.auto = auto;

        if (TypingSpeed==0)
        {
            txt_Content.text = content;
        }
        else
        {
            texttoshow = content;
            StartCoroutine(Type());
        }
        
        speaker?.Hide();
    }

    public void SetAuto(bool auto)
    {
        this.auto=auto;

        if (!typing)
        {
            StartCoroutine(WaitToContinue());
        }
    }

    public void SetFont(TMP_FontAsset font)
    {
        txt_Content.font = font;
    }

    IEnumerator Type()
    {
        typing=true;
        txt_Content.text = texttoshow;
        txt_Content.maxVisibleCharacters = 0;
        for (int i=0;i<= texttoshow.Length;i++)
        {
            txt_Content.maxVisibleCharacters++;
            if (typing)
            {
                yield return new WaitForSeconds(1 / TypingSpeed);
            }
            else
            {
                yield return null;
            }
            
        }
        typing = false;
        txt_Content.text = texttoshow;

        if (auto) {
            StartCoroutine(WaitToContinue());
        }
    }

    IEnumerator WaitToContinue()
    {
        yield return new WaitForSeconds(AutoWaitTime);
        dialogueContinue();
    }

    public void SetSpeed(float speed)
    {
        TypingSpeed = speed;
    }

    public void SetSpeaker(Character character)
    {
        var go = Instantiate(character.SpeakerPrefab, speakerParent);
        speaker = go.GetComponent<DialogueSpeaker>();
        speaker.SetName(character.Name_Shown);

        if (character.FontAsset)
        {
            SetFont(character.FontAsset);
        }
    }

    public void SetSpeakerLayout(int layout)
    {
        Layout flag=(Layout)layout;

        //Debug.Log(flag);

        switch (flag)
        {
            case Layout.Left:
                speaker.LOCALPOS = leftPoint.localPosition;
                break;
            case Layout.Center:
                speaker.LOCALPOS= centerPoint.localPosition;
                break;
            case Layout.Right:
                speaker.LOCALPOS= rightPoint.localPosition;
                break;
            default:
                
                break;
        }
    }

    public void SetPortrait(int portrait)
    {
        speaker.SetAnim(portrait);
    }

    public virtual void DestroyDialogue()
    {
        //TODO
        Destroy(gameObject);
    }

    private void dialogueContinue()
    {
        //Debug.Log("Continue");
        StopAllCoroutines();
        DialogueMgr.Instance.ContinueDialogue();
    }

    private void endTyping()
    {
        typing = false;
    }

    public void Clicked()
    {
        if (typing)
        {
            endTyping();
        }
        else
        {
            dialogueContinue();
        }
    }


    public enum Layout {
        Left=-1,
        Center=0,
        Right=1
    }
}
