using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Net
{
    public class Map
    {
        private Grid[][] grids;

        public int WIDTH(int y) => grids[y].Length;
        public int HEIGHT => grids.Length;


        HashSet<(int, int)> visited = new HashSet<(int, int)>();

        List<List<(int, int)>> linkedgrids = new List<List<(int, int)>>();

        public UnityEvent<bool> StatusCheck=new UnityEvent<bool>();

        private List<Grid> nodes=new List<Grid>();

        public Map(Grid[][] grids)
        {
            this.grids = grids;
            getAllNodes();
        }

        private void getAllNodes()
        {
            nodes.Clear();
            for (int i=0;i<grids.Length;i++)
            {
                for (int j = 0; j < grids[i].Length;j++)
                {
                    if (grids[i][j].IsNode())
                    {
                        nodes.Add(grids[i][j]);
                    }
                }
            }
        }

        private bool isWin()
        {
            foreach (var node in nodes)
            {
                if (!node.ACTIVE)
                {
                    return false;
                }
            }
            return true;
        }

        public void Rotate_Clockwise(int x, int y)
        {
            grids[y][x].Rotate_clockwise();
            Refresh();
        }

        public void Rotate_Anticlockwise(int x, int y)
        {
            grids[y][x].Rotate_anticlockwise();
            Refresh();
        }

        public void Refresh()
        {
            linkedgrids.Clear();
            visited.Clear();
            for (int i = 0; i < grids.Length; i++)//y
            {
                for (int j = 0; j < grids[i].Length; j++)
                {
                    var lst = checklinks(j, i, null, Grid.Port.All);
                    if (lst != null)
                    {
                        linkedgrids.Add(lst);
                    }
                }
            }

            List<int> mains = new List<int>();

            for (int i=0;i<linkedgrids.Count;i++)
            {
                for (int j = 0; j < linkedgrids[i].Count;j++)
                {
                    if (grids[linkedgrids[i][j].Item2][linkedgrids[i][j].Item1].IsMain())
                    {
                        mains.Add(i);
                        //Debug.Log($"Got it ({linkedgrids[i][j].Item1},{linkedgrids[i][j].Item2})");
                        break;
                    }
                    else {
                        activate(linkedgrids[i][j].Item1, linkedgrids[i][j].Item2,false);
                    }
                }
            }

            for (int i=0;i<mains.Count;i++)
            {
                foreach (var pos in linkedgrids[mains[i]])
                {
                    //Debug.Log("Activated");
                    activate(pos.Item1,pos.Item2, true);
                }
            }


            StatusCheck?.Invoke(isWin());
        }


        private List<(int, int)> checklinks(int x, int y, List<(int, int)> linked, Grid.Port port)
        {
            if (y < 0 || y >= grids.Length || x < 0 || x >= grids[y].Length || visited.Contains((x, y)))
            {
                return null;
            }

            var grid = grids[y][x];
            if (grid.IsHollow())
            {
                return null ;
            }

            if (port != Grid.Port.All && !grid.HasPort(port))
            {
                return null;
            }

            if (linked == null)
            {
                linked = new List<(int, int)>();
                //Debug.Log("OK");
            }



            linked.Add((x, y));
            visited.Add((x, y));


            for (int i = 0; i < grid.PORTSCOUNT; i++)
            {
                switch (grid.GetPort(i))
                {
                    case Grid.Port.N:
                        checklinks(x, y + 1, linked, Grid.Port.S);
                        break;
                    case Grid.Port.E:
                        checklinks(x + 1, y, linked, Grid.Port.W);
                        break;
                    case Grid.Port.S:
                        checklinks(x, y - 1, linked, Grid.Port.N);
                        break;
                    case Grid.Port.W:
                        checklinks(x - 1, y, linked, Grid.Port.E);
                        break;
                    default:
                        break;
                }
            }
            return linked;

        }


        public void Test()
        {
            Refresh();
            printLinkedGrids();
            printGrids();
        }

        private void printLinkedGrids()
        {
            string str = "LinkedGrids:\n";
            for (int i = 0; i < linkedgrids.Count; i++)
            {
                for (int j = 0; j < linkedgrids[i].Count; j++)
                {
                    str += linkedgrids[i][j];
                }
                str += "\n";
            }
            Debug.Log(str);
        }

        private void printGrids()
        {
            string str = "Grids:\n";
            for (int i = grids.Length - 1; i >= 0; i--)//y
            {
                for (int j = 0; j < grids[i].Length; j++)
                {
                    str += grids[i][j].toString() + " ";
                }
                str += "\n";
            }
            Debug.Log(str);
        }

        public Grid GetGrid(int x, int y)
        {
            return grids[y][x];
        }

        private void activate(int x,int y,bool flag)
        {
            grids[y][x].Activate(flag);
        }

    }
}

