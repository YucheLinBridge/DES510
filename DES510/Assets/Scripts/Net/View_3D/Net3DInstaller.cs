using UnityEngine;
using Zenject;

public class Net3DInstaller : MonoInstaller
{
    [SerializeField] private GameObject GridPrefab;

    public override void InstallBindings()
    {
        bindScripts();
        bindFactories();
    }

    private void bindScripts() {
        Container.BindInterfacesAndSelfTo<Grids3DMgr>().AsSingle();
    }
    private void bindFactories()
    {
        Container.BindFactory<Grid3D,Grid3D.Factory>()
            .FromComponentInNewPrefab(GridPrefab);
    }

}