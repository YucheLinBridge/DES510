using Net;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapCreator : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private int width = 10;
    [SerializeField] private int height =10;
    [SerializeField] private float x_offset, y_offset,spacing;

    [Space]
    [SerializeField] private int currentIndex = 0;
    public int INDEX=>currentIndex;

    [Header("Sprites ref")]
    [SerializeField] private List<ArtData> LineSprites;
    [SerializeField] private Sprite Node, Main;

    public Sprite NODE => Node;
    public Sprite MAIN => Main;

    [Inject(Id ="grid_parent")]
    private Transform tmpgridsParent;

    [Inject]
    private Net.Data data;

    [Inject]
    private TmpGrid.Factory gridFactory;

    private MapData current_map;

    private List<List<TmpGrid>> tmpGrids;

    // Start is called before the first frame update
    void Start()
    {
        refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        current_map.rows.Clear();
        int biggest_y = 0;
        for (int i=tmpGrids.Count-1;i>=0;i--)
        {
            for (int j=0;j< tmpGrids[i].Count;j++)
            {
                if (!tmpGrids[i][j].IsHollow)
                {
                    biggest_y = i;
                    break;
                }
            }

            if (biggest_y>0)
            {
                break;
            }
        }

        //Debug.Log(biggest_y);

        for (int i=0;i<=biggest_y;i++)
        {
            current_map.rows.Add(new MapData.Row());
            int biggest_x = 0;
            for (int j = tmpGrids[i].Count - 1; j >= 0; j--)
            {
                if (!tmpGrids[i][j].IsHollow) {
                    biggest_x = j;
                    break;
                }
            }

            //Debug.Log(biggest_x);
            for (int j = 0; j <= biggest_x;j++)
            {
                current_map.rows[i].girds.Add(tmpGrids[i][j].GetGrid());
                //Debug.Log($"({j},{i})");
            }
        }
#if UNITY_EDITOR
        data.Save();
#endif
    }


    private void refresh()
    {
        current_map=data.GetMap(currentIndex);
        if (tmpGrids==null)
        {
            tmpGrids=new List<List<TmpGrid>>();
        }
        else
        {
            for (int i=0;i<tmpGrids.Count;i++)
            {
                for (int j = 0; j < tmpGrids[i].Count;j++)
                {
                    tmpGrids[i][j].KillItself();
                }
            }
            tmpGrids.Clear();
        }

        for (int i=0;i<height;i++)
        {
            tmpGrids.Add(new List<TmpGrid>());
            for (int j = 0; j < width;j++)
            {
                var tmpgrid = gridFactory.Create();
                tmpGrids[i].Add(tmpgrid);
                tmpgrid.SetParent(tmpgridsParent);
                tmpgrid.SetPos(new Vector3((j-1)*(x_offset+spacing),(i-1)*(y_offset+spacing)));

                if (tryGetGrid(j,i,out Net.Grid grid))
                {
                    tmpgrid.SetKind(grid.KIND);
                    tmpgrid.SetPorts(grid.ports);
                    tmpgrid.SetCannotRot(grid.CANNOTROT);
                }
                else
                {
                    tmpgrid.SetPorts(null);
                    tmpgrid.SetKind(Net.Grid.Kind.Hollow);
                    tmpgrid.SetCannotRot(false);
                }
            }
        }

    }

    private bool tryGetGrid(int x,int y,out Net.Grid grid)
    {
        if (y<current_map.rows.Count)
        {
            if (x < current_map.rows[y].girds.Count)
            {
                grid = current_map.rows[y].girds[x];
                return true;
            }
        }

        grid = null;
        return false;
    }


    public Sprite GetLine(string name)
    {
        int index = LineSprites.FindIndex(x => x.IsName(name));
        if (index == -1)
        {
            Debug.LogError($"There is no sprite called {name}");
            return null;
        }
        return LineSprites[index].SPRITE;
    }
}
