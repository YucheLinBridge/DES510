using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Coroutine_Zenject;
using DG.Tweening;
using UnityEngine.Events;

public class CombinationLock_mono : MonoBehaviour
{
    [SerializeField] private GameObject ConbinationLock;
    [SerializeField] private Animator Anim;
    [SerializeField] private List<Button> buttons=new List<Button>();

    [SerializeField] private Button cancel;
    [SerializeField] private TextMeshProUGUI Txt_Display;
    [SerializeField] private List<int> password;


    public UnityEvent OnComplete;


    private List<int> inputing_nums=new List<int>();
    private int index_now;

    private bool enable=true;

    private void Start()
    {
        for (int i=0;i<password.Count;i++)
        {
            inputing_nums.Add(-1);
        }

        for (int i=0;i<buttons.Count;i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => {
                click(index);
            });
        }

        cancel.onClick.AddListener(() => {
            hide();
        });

        resetNums();
    }

    public void Show()
    {
        ConbinationLock.SetActive(true);
    }

    private void hide()
    {
        Anim.SetTrigger("END");
        Coroutine_Controller.WaitToDo(() => {
            resetNums();
            ConbinationLock.SetActive(false);
        },1f);
    }

    private void click(int num)
    {
        if (!enable) { return; }

        inputing_nums[index_now] = num;
        index_now++;
        updateDisplay();

        if (index_now>=password.Count)
        {
            enable = false;
            if (checkPass())
            {
                Debug.Log("Correct");

                Txt_Display.color = Color.green;
                Coroutine_Controller.WaitToDo(() => {
                    OnComplete?.Invoke();
                    hide();
                },.5f);
            }
            else
            {
                showWrong();
            }
        }
    }

    private void updateDisplay()
    {
        string str = "";
        for (int i = 0; i < inputing_nums.Count; i++)
        {
            if (inputing_nums[i]==-1)
            {
                str += "_";
            }
            else
            {
                str+= inputing_nums[i];
            }
        }

        Txt_Display.text = str;
    }


    private void resetNums()
    {
        for (int i=0;i<inputing_nums.Count;i++)
        {
            inputing_nums[i] = -1;
        }

        index_now = 0;  

        updateDisplay();
    }

    /// <summary>
    /// Return if the password right or wrong
    /// </summary>
    /// <returns></returns>
    private bool checkPass()
    {
        for (int i = 0; i < password.Count; i++)
        {
            if (inputing_nums[i] != password[i])
            {
                return false;
            }
        }

        return true;
    }


    private void showWrong() {
        Txt_Display.color=Color.red;
        Sequence seq = DOTween.Sequence();
        seq.Append(Txt_Display.transform.DOShakePosition(1f));

        seq.AppendCallback(() => {
            resetNums();
            Txt_Display.color = Color.black;
            enable = true;
        });

    }

}
