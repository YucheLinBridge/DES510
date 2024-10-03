using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Net {
    [CreateAssetMenu(fileName = "Grid3D_data", menuName = "Installers/Net MapData")]
    public class Data : ScriptableObjectInstaller<Data>
    {
        [SerializeField] private List<Row> rows = new List<Row>();

        public Grid[][] Format()
        {
            Grid[][] grids= new Grid[rows.Count][];
            for (int i=0;i<grids.Length;i++)
            {
                grids[i] = new Grid[rows[i].girds.Count];
                for (int j=0;j< rows[i].girds.Count;j++)
                {
                    rows[i].girds[j].Clone(out grids[i][j]);
                }
            }
            return grids;
        }

        [System.Serializable]
        public class Row{
            public List<Grid> girds = new List<Grid>();
        }

        public override void InstallBindings() {
            Container.BindInstance(this);
        }
    }
}

