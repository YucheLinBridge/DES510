using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombinationLock_mono : MonoBehaviour
{
    [SerializeField] private GameObject ConbinationLock;
    [SerializeField] private Animator Anim;
    [SerializeField] private List<Button> buttons=new List<Button>();

    [SerializeField] private Button confirm, cancel;
    [SerializeField] private TextMeshProUGUI Txt_Display;
    [SerializeField] private Vector4 password;


    private int num_1,num_2,num_3,num_4;
    private int index_now;

    private void Start()
    {
        
    }

    private void click(int num)
    {

    }

}
