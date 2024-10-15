using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Net;

public class GridsUIMgr : IInitializable
{
    [Inject]
    private Data data;
    [Inject]
    private GridUI_data data_ui;

    [Inject]
    private GridUI.Factory factory;

    [Inject(Id = "net_panel")]
    private RectTransform net_panel;

    [Inject(Id = "grid_parent")]
    private RectTransform grid_parent;

    private Map map;
    private List<GridUI> grids;


    public void Initialize()
    {
        Generate(0);
    }

    public void Generate(int index)
    {
        var tmp = data.GetMap(index).Format();
        map = new Map(tmp);

        if (grids==null) {
            grids=new List<GridUI>();
        }
        else
        {
            clearOldGrids();
        }

        for (int i = 0; i < map.HEIGHT; i++)//y
        {
            for (int j = 0; j < map.WIDTH(i); j++)//x
            {
                var grid = map.GetGrid(j, i);
                if (!grid.IsHollow())
                {
                    var gridUI = factory.Create();
                    gridUI.SetParent(grid_parent);
                    gridUI.SetPos(new Vector3((j - 1) * (data_ui.WIDTH + data_ui.SPACING), (i - 1) * (data_ui.HEIGHT + data_ui.SPACING)));
                    gridUI.SetXY(j, i);
                    grid.OnActivated.AddListener(gridUI.Activate);
                    gridUI.SetGrid(grid);
                    grids.Add(gridUI);
                }

            }
        }
        map.Refresh();
    }

    public void ShowGame()
    {
        net_panel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        net_panel.gameObject.SetActive(false);
    }

    public void Rotate_clockwise(int x, int y)
    {
        map.Rotate_Clockwise(x, y);
    }
    public void Rotate_anticlockwise(int x, int y)
    {
        map.Rotate_Anticlockwise(x, y);
    }

    private void clearOldGrids()
    {
        for (int i= grids.Count; i>=0; i--)
        {
            grids[i].KillItself();
        }
        grids.Clear();
    }

}
