using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid
{
    [SerializeField]private List<Port> ports;
    public int PORTSCOUNT=>ports.Count;
    public Port GetPort(int index)
    {
        int realport = _direction + (int)ports[index];
        return (Port)realport;
    }

    private Kind kind;
    private int _direction;
    private int direction {
        get => _direction;
        set {
            if (value>0) {
                _direction = value % 4;
            }
            else
            {
                _direction = value % 4+4;
            }
        } 
    }
    public int DIR => direction;


    private bool active;
    public bool ACTIVE=>active;

    public Grid(Kind kind, List<Port> ports)
    {
        this.kind = kind;
        this.ports = ports;
        this.direction = 0;
    }

    public bool IsNod() {
        return kind==Kind.Nod;
    }

    public bool IsMain()
    {
        return kind==Kind.Main;
    }

    public bool HasPort(Port port)
    {
        int index = ports.FindIndex(x=> {
            int realport = _direction + (int)x; 
            return realport==(int)port;
        });
        return index>-1;
    }

    public void Rotate_clockwise()
    {
        direction++;
    }

    public void Rotate_anticlockwise()
    {
        direction--;
    }

    public void SetDirection(float degree)
    {
        direction = Mathf.RoundToInt(degree/90);
    }

    public void Activate(bool flag)
    {
        active = flag;
    }

    public string toString()
    {
        string str = "";
        for (int i=0;i<PORTSCOUNT;i++)
        {
            switch (GetPort(i))
            {
                case Port.N:
                    str += "N";
                    break;
                case Port.E:
                    str += "E";
                    break;
                case Port.S:
                    str += "S";
                    break;
                case Port.W:
                    str += "W";
                    break;
                default:
                    break;
            }
        }
        return str;
    }


    public enum Kind {
        Main,
        Nod,
        Line
    }

    public enum Port {
        N=1, 
        E =2,
        S =3,
        W=4,
        All=0
    }


}
