using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class GridUI : MonoBehaviour
{
    private int x, y;
    [SerializeField] private Image line, node;
    [SerializeField] private Color line_active, line_inactive, node_active, node_inactive;


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
        transform.DORotate(transform.rotation.eulerAngles - Vector3.forward * 90f, .5f).OnComplete(() => {
            locked = false;
        });
        sfxMgr.PlaySFX(2);
    }

    public void SetGrid(Net.Grid grid)
    {
        //Debug.Log($"Line_{grid.toString()}");
        line.sprite = database.GetLine(grid.toString());
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
        }

        cannotRotate = grid.CANNOTROT;
    }


    public void Activate(bool flag)
    {
        line.color = flag ? line_active : line_inactive;
        node.color = flag ? node_active : node_inactive;
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
