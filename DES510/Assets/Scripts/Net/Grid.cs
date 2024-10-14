using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Net {
    [System.Serializable]
    public class Grid
    {
        [SerializeField] private bool cannotRotate;
        public List<Port> ports;
        [SerializeField]private Kind kind;

        [HideInInspector]public UnityEvent<bool> OnActivated=new UnityEvent<bool>();

        public int PORTSCOUNT => ports.Count;
        public bool CANNOTROT=>cannotRotate;
        public Kind KIND=>kind;

        public Port GetPort(int index)
        {
            int realport = (_direction + (int)ports[index])%4;
            return (Port)realport;
        }

        
        private int _direction;
        private int direction
        {
            get => _direction;
            set
            {
                if (value >= 0)
                {
                    _direction = value % 4;
                }
                else
                {
                    _direction = value % 4 + 4;
                }
            }
        }
        public int DIR => direction;


        private bool active;
        public bool ACTIVE => active;

        public Grid(Kind kind, List<Port> ports,bool cannotRot)
        {
            this.kind = kind;
            this.ports = ports;
            this.direction = 0;
            this.cannotRotate = cannotRot;
        }

        public bool IsNode()
        {
            return kind == Kind.Node;
        }

        public bool IsMain()
        {
            return kind == Kind.Main;
        }

        public bool IsHollow()
        {
            return kind == Kind.Hollow;
        }

        public bool HasPort(Port port)
        {
            int index = ports.FindIndex(x => {
                int realport = (_direction + (int)x)%4;
                return realport == (int)port;
            });
            return index > -1;
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
            direction = Mathf.RoundToInt(degree / 90);
        }

        public void Activate(bool flag)
        {
            active = flag;
            OnActivated?.Invoke(flag);
        }

        public string toString()
        {
            if (IsHollow())
            {
                return "X";
            }

            string str = "";

            if (HasPort(Port.N))
            {
                str += "N";
            }

            if (HasPort(Port.S)) {
                str+= "S";
            }

            if (HasPort(Port.E))
            {
                str+= "E";
            }

            if (HasPort(Port.W))
            {
                str += "W";
            }


            return str;
        }

        public void Clone(out Grid clone)
        {
            List<Port>ports = new List<Port>();
            ports.AddRange(this.ports);
            clone = new Grid(kind,ports,cannotRotate);
        }


        public enum Kind
        {
            Line,
            Main,
            Node,
            Hollow
        }

        public enum Port
        {
            N = 0,
            E = 1,
            S = 2,
            W = 3,
            All = 4
        }


    }
}

