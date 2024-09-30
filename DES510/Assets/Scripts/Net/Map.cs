using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Map
{
    private Grid[][] grids;


    HashSet<(int,int)> visited = new HashSet<(int, int)>();

    List<List<(int, int)>> linkedgrids = new List<List<(int, int)>>();


    public Map(Grid[][] grids)
    {
        this.grids = grids;
    }

    public void Rotate_Clockwise(int x, int y)
    {
        grids[y][x].Rotate_clockwise();
    }
    
    public void Rotate_Anticlockwise(int x, int y)
    {
        grids[y][x].Rotate_anticlockwise();
    }

    private void refresh()
    {
        linkedgrids.Clear();
        for (int i=0;i<grids.Length;i++)//y
        {
            for (int j = 0; j < grids[i].Length;j++) {
                var lst = checklinks(j, i, null, Grid.Port.All);
                if (lst!=null) {
                    linkedgrids.Add(lst);
                }
            }
        }
    }


    private List<(int, int)> checklinks(int x,int y,List<(int,int)> linked,Grid.Port port) {
        if (y<0||y>=grids.Length|| x<0 || x>=grids[0].Length|| visited.Contains((x,y)))
        {
            return null;
        }

        var grid = grids[y][x];
        if (port != Grid.Port.All && !grid.HasPort(port))
        {
            return null;
        }

        if (linked==null)
        {
            linked = new List<(int, int)>();
            //Debug.Log("OK");
        }

        

        linked.Add((x, y));
        visited.Add((x, y));

        
        for (int i=0;i<grid.PORTSCOUNT;i++)
        {
            switch (grid.GetPort(i))
            {
                case Grid.Port.N:
                    checklinks(x,y+1, linked, Grid.Port.S);
                    break;
                case Grid.Port.E:
                    checklinks(x+1,y, linked, Grid.Port.W);
                    break;
                case Grid.Port.S:
                    checklinks(x, y- 1, linked, Grid.Port.N);
                    break;
                case Grid.Port.W:
                    checklinks(x-1, y, linked, Grid.Port.E);
                    break;
                default:
                    break;
            }
        }
        return linked;
        
    }
    

    public void Test()
    {
        refresh();
        printLinkedGrids();
        printGrids();
    }

    private void printLinkedGrids()
    {
        string str = "LinkedGrids:\n";
        for (int i=0;i<linkedgrids.Count;i++)
        {
            for (int j = 0; j < linkedgrids[i].Count;j++)
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
        for (int i = grids.Length-1; i >=0 ; i--)//y
        {
            for (int j = 0; j < grids[i].Length; j++)
            {
                str+= grids[i][j].toString()+" ";
            }
            str += "\n";
        }
        Debug.Log(str);
    }
}
