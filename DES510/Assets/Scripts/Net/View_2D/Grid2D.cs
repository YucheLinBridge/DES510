using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Grid2D : MonoBehaviour
{
    private int x, y;
    [SerializeField] private SpriteRenderer line,node;
    [SerializeField] private Color line_active, line_inactive,node_active,node_inactive;


    [Inject]
    private Grids2DMgr mgr;

    private bool locked = false;
    private bool cannotRotate = false;

    private void OnMouseDown()
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
    }

    public void SetGrid(Net.Grid grid)
    {
        //Debug.Log($"Line_{grid.toString()}");
        line.sprite = Resources.Load<Sprite>($"Line_{grid.toString()}");// as Sprite;
        if (!grid.IsNode() && !grid.IsMain())
        {
            node.enabled = false;
        }
        else 
        {
            node.enabled=true;
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

    public class Factory : PlaceholderFactory<Grid2D>
    {

    }
}
