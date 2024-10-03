using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Net;

public class Grids3DMgr : IInitializable
{
    [Inject]
    private Data data;
    [Inject]
    private Grid3D_data data_3d;

    [Inject]
    private Grid3D.Factory factory;

    private Map map;


    public void Initialize()
    {
        var tmp = data.Format();
        map = new Map(tmp);

        //Debug.Log($"Map height={tmp.Length}");
        for (int i=0;i<map.HEIGHT;i++)//y
        {
            for (int j=0;j<map.WIDTH(i);j++)//x
            {
                var grid = map.GetGrid(j, i);
                if (!grid.IsHollow()) {
                    var grid3d = factory.Create();
                    grid3d.SetPos(new Vector3((j - 1) * (data_3d.WIDTH + data_3d.SPACING), (i - 1) * (data_3d.HEIGHT + data_3d.SPACING)));
                    grid3d.SetXY(j, i);

                    grid.OnActivated.AddListener(grid3d.Activate);
                    grid3d.SetGrid(grid);
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
