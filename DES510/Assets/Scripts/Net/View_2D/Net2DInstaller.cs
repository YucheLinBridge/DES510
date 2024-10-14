using UnityEngine;
using Zenject;

public class Net2DInstaller : MonoInstaller
{
    [SerializeField] private GameObject GridPrefab;

    public override void InstallBindings()
    {
        bindScripts();
        bindFactories();
    }

    private void bindScripts()
    {
        Container.BindInterfacesAndSelfTo<Grids2DMgr>().AsSingle();
    }
    private void bindFactories()
    {
        Container.BindFactory<Grid2D, Grid2D.Factory>()
            .FromComponentInNewPrefab(GridPrefab);
    }
}