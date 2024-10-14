using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Net;

public class Grids2DMgr : IInitializable
{
    [Inject]
    private Data data;
    [Inject]
    private Grid2D_data data_2d;

    [Inject]
    private Grid2D.Factory factory;

    [Inject(Id = "grid_parent")]
    private Transform grid_parent;

    private Map map;

    public void Initialize()
    {
        var tmp = data.GetTest().Format();
        map = new Map(tmp);
    }

    public void ShowGame()
    {
        for (int i = 0; i < map.HEIGHT; i++)//y
        {
            for (int j = 0; j < map.WIDTH(i); j++)//x
            {
                var grid = map.GetGrid(j, i);
                if (!grid.IsHollow())
                {
                    var grid2d = factory.Create();
                    grid2d.SetParent(grid_parent);
                    grid2d.SetPos(new Vector3((j - 1) * (data_2d.WIDTH + data_2d.SPACING), (i - 1) * (data_2d.HEIGHT + data_2d.SPACING)));
                    grid2d.SetXY(j, i);
                    grid.OnActivated.AddListener(grid2d.Activate);
                    grid2d.SetGrid(grid);
                }

            }
        }
        map.Refresh();
    }

    public void Rotate_clockwise(int x, int y)
    {
        map.Rotate_Clockwise(x, y);
    }
    public void Rotate_anticlockwise(int x, int y)
    {
        map.Rotate_Anticlockwise(x, y);
    }

}
