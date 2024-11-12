using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class GridUI : MonoBehaviour
{
    private int x, y;
    [SerializeField] private Image line,line_bg, node,node_bg;


    [Inject]
    private GridsUIMgr mgr;

    [Inject]
    private GridUI_data database;

    [Inject]
    private SFXMgr sfxMgr;

    private bool locked = false;
    private bool cannotRotate = false;

    public void OnClicked()
    {
        if (locked || cannotRotate)
        {
            return;
        }
        
        locked = true;
        //clockwise
        mgr.Rotate_clockwise(x, y);
        transform.DORotate(transform.rotation.eulerAngles - Vector3.forward * 90f, database.ROTTIME).OnComplete(() => {
            locked = false;
        });
        sfxMgr.PlaySFX(2);
    }

    public void SetGrid(Net.Grid grid)
    {
        //Debug.Log($"Line_{grid.toString()}");
        line.sprite = database.GetLine(grid.toString());

        var bg= database.GetLineBG(grid.toString());
        if (bg)
        {
            line_bg.sprite = bg;
        }
        else
        {
            line_bg.gameObject.SetActive(false);
        }
        
        //Resources.Load<Sprite>($"Line_{}");// as Sprite;
        if (grid.IsNode())
        {
            node.enabled = true;
            node.sprite = database.NODE;
        }else if (grid.IsMain())
        {
            node.enabled = true;
            node.sprite = database.MAIN;
        }
        else
        {
            node.enabled = false;
            node_bg.enabled = false;
        }

        cannotRotate = grid.CANNOTROT;
    }


    public void Activate(bool flag)
    {
        line_bg.color = flag ? database.line_active : database.line_inactive;
        node_bg.color = flag ? database.node_active : database.node_inactive;
    }

    private void rotateTo(int dir)
    {
        transform.rotation = Quaternion.Euler(0, 0, -90 * dir);
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }

    public void SetPos(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void SetXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void KillItself()
    {
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<GridUI>
    {

    }
}
