using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class Grid3D : MonoBehaviour
{
    private int x, y;
    public List<LineRenderer> lines = new List<LineRenderer>();
    [SerializeField] private Color active, disactive;

    [Inject]
    private Grids3DMgr mgr;


    private int index = 0;
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
        mgr.Rotate_clockwise(x,y);
        transform.DORotate(transform.rotation.eulerAngles - Vector3.forward * 90f, .5f).OnComplete(() => {
            locked = false;
        });
    }

    public void SetGrid(Net.Grid grid)
    {
        resetAllLine();
        for (int i = 0; i < grid.PORTSCOUNT; i++)
        {
            addPort(grid.GetPort(i));
        }

        cannotRotate = grid.CANNOTROT;
    }
    private void resetAllLine()
    {
        for (int i=0;i< lines.Count;i++)
        {
            lines[i].enabled = false;
        }
        index = 0;
    }


    private void addPort(Net.Grid.Port port)
    {
        lines[index].enabled = true;
        switch (port)
        {
            case Net.Grid.Port.N:
                lines[index].SetPosition(1,Vector3.up*0.5f);
                break;
            case Net.Grid.Port.E:
                lines[index].SetPosition(1,Vector3.right * 0.5f);
                break;
            case Net.Grid.Port.S:
                lines[index].SetPosition(1, Vector3.down * 0.5f);
                break;
            case Net.Grid.Port.W:
                lines[index].SetPosition(1, Vector3.left * 0.5f);
                break;
            case Net.Grid.Port.All:
                break;
            default:
                break;
        }
        index++;
    }

    public void Activate(bool flag)
    {
        for (int i = 0; i < lines.Count; i++) {
            lines[i].startColor= flag ? active : disactive;
            lines[i].endColor= flag ? active : disactive;
        }
    }

    private void rotateTo(int dir)
    {
        transform.rotation=Quaternion.Euler(0,0,-90*dir);
    }

    public void SetPos(Vector3 position)
    {
        transform.position=position;
    }

    public void SetXY(int x,int y)
    {
        this.x= x;
        this.y= y;
    }

    public class Factory : PlaceholderFactory<Grid3D> {
        
    }
}
