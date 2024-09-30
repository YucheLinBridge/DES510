using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<TestRow> rows;

    // Start is called before the first frame update
    void Start()
    {
        Grid[][] mapGrids = new Grid[rows.Count][];
        for (int i=0;i<mapGrids.Length;i++)
        {
            mapGrids[i] = new Grid[rows[i].grids.Count];
            for (int j=0;j< rows[i].grids.Count;j++)
            {
                mapGrids[i][j] = rows[i].grids[j];
            }
        }

        Map map=new Map(mapGrids);

        map.Test();

    }

}

[System.Serializable]
public class TestRow
{
    public List<Grid> grids;
}
