using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Net;
using Zenject;

public class TmpGrid:MonoBehaviour
{

    [SerializeField] private SpriteRenderer line, node,bg;

    [Header("Button")]
    [SerializeField] private Button btnKind;
    [SerializeField]private TextMeshProUGUI txt_button;
    [SerializeField] private Button btnRotClockwise;

    [Header("Toggles")]
    [SerializeField] private Toggle toggle_n;
    [SerializeField] private Toggle toggle_s, toggle_e, toggle_w;
    [SerializeField] private Toggle toggle_cannotRot;

    [Header("Colors")]
    [SerializeField] private Color main; 
    [SerializeField] private Color normalNode;
    [SerializeField] private Color bg_normal,bg_hollow;

    [Inject]
    private MapCreator creator;

    private bool dontResetLines = false;

    private Net.Grid.Kind kind;

    public Net.Grid.Kind KIND=>kind;
    public bool IsHollow=>kind==Net.Grid.Kind.Hollow;


    private void Start()
    {
        toggle_n.onValueChanged.AddListener((bool flag) => { updatePorts(); });
        toggle_s.onValueChanged.AddListener((bool flag) => { updatePorts(); });
        toggle_e.onValueChanged.AddListener((bool flag) => { updatePorts(); });
        toggle_w.onValueChanged.AddListener((bool flag) => { updatePorts(); });
        btnKind.onClick.AddListener(switchKind);
        btnRotClockwise.onClick.AddListener(rotate_clockwise);
    }


    public Net.Grid GetGrid() {
        List<Net.Grid.Port> ports = new List<Net.Grid.Port>();
        if (toggle_n.isOn)
        {
            ports.Add(Net.Grid.Port.N);
        }

        if (toggle_s.isOn)
        {
            ports.Add (Net.Grid.Port.S);
        }

        if (toggle_e.isOn) {
            ports.Add((Net.Grid.Port.E));
        }

        if (toggle_w.isOn) {
            ports.Add((Net.Grid.Port.W));
        }

        return new Net.Grid(kind,ports, toggle_cannotRot.isOn);
    }


    public void SetPos(Vector3 position)
    {
        transform.localPosition = position;
    }
    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }

    public void SetPorts(List<Net.Grid.Port> ports)
    {
        if (ports==null)
        {
            return;
        }

        for (int i=0;i<ports.Count;i++)
        {
            switch (ports[i])
            {
                case Net.Grid.Port.N:
                    toggle_n.isOn = true;
                    break;
                case Net.Grid.Port.E:
                    toggle_e.isOn = true;   
                    break;
                case Net.Grid.Port.S:
                    toggle_s.isOn = true;
                    break;
                case Net.Grid.Port.W:
                    toggle_w.isOn = true;
                    break;
                case Net.Grid.Port.All:
                    break;
                default:
                    break;
            }
        }

        line.sprite = Resources.Load<Sprite>($"Line_{toString()}");
    }

    public void SetKind(Net.Grid.Kind kind)
    {
        this.kind=kind;
        switch (this.kind)
        {
            case Net.Grid.Kind.Line:
                enableAllToggle(true);
                line.enabled = true;
                node.enabled = false;
                bg.color=bg_normal;
                btnRotClockwise.interactable = true;
                break;
            case Net.Grid.Kind.Main:
                enableAllToggle(true);
                line.enabled = true;
                node.enabled = true;
                node.sprite= creator.MAIN;
                node.color =main;
                bg.color = bg_normal;
                btnRotClockwise.interactable = true;
                break;
            case Net.Grid.Kind.Node:
                enableAllToggle(true);
                line.enabled = true;
                node.enabled = true;
                node.sprite = creator.NODE;
                node.color=normalNode;
                bg.color = bg_normal;
                btnRotClockwise.interactable = true;
                break;
            case Net.Grid.Kind.Hollow:
                enableAllToggle(false);
                line.enabled = false;
                node.enabled = false;
                bg.color = bg_hollow;
                btnRotClockwise.interactable = false;
                break;
            default:
                break;
        }

        txt_button.text= this.kind.ToString();
    }

    private void enableAllToggle(bool flag)
    {
        toggle_n.interactable = flag;
        toggle_s.interactable = flag; 
        toggle_e.interactable = flag; 
        toggle_w.interactable = flag;
        toggle_cannotRot.interactable = flag;
    }

    public void SetCannotRot(bool flag)
    {
        toggle_cannotRot.isOn = flag;
    }

    public void KillItself()
    {
        Destroy(gameObject);
    }

    private string toString()
    {
        if (kind==Net.Grid.Kind.Hollow)
        {
            return string.Empty;
        }

        string str = string.Empty;

        if (toggle_n.isOn)
        {
            str += "N";
        }

        if (toggle_s.isOn)
        {
            str += "S";
        }

        if (toggle_e.isOn)
        {
            str += "E";
        }

        if (toggle_w.isOn)
        {
            str += "W";
        }

        return str;
    }

    private void updatePorts()
    {
        if (dontResetLines)
        {
            return;
        }

        string str = toString();
        if (str==string.Empty)
        {
            line.sprite = null;
        }
        else
        {
            line.sprite = creator.GetLine(str);
            
        }
        
    }

    private void switchKind()
    {

        switch (kind)
        {
            case Net.Grid.Kind.Line:
                SetKind(Net.Grid.Kind.Main);
                break;
            case Net.Grid.Kind.Main:
                SetKind(Net.Grid.Kind.Node);

                break;
            case Net.Grid.Kind.Node:
                SetKind(Net.Grid.Kind.Hollow);

                break;
            case Net.Grid.Kind.Hollow:
                SetKind(Net.Grid.Kind.Line);

                break;
        }
    }

    private void rotate_clockwise()
    {
        dontResetLines = true;
        bool n=toggle_n.isOn, s=toggle_s.isOn, e=toggle_e.isOn, w=toggle_w.isOn;
        toggle_n.isOn = w;
        toggle_s.isOn = e;
        toggle_e.isOn = n;
        toggle_w.isOn = s;
        dontResetLines=false;
        updatePorts();
    }

    public class Factory : PlaceholderFactory<TmpGrid>
    {

    }

}
