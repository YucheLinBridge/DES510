using UnityEngine;
using Zenject;

public class NetUIInstaller : MonoInstaller
{
    //[SerializeField] private GameObject GridPrefab;

    [Inject]
    private GridUI_data database;

    public override void InstallBindings()
    {
        bindScripts();
        bindFactories();
    }

    private void bindScripts()
    {
        Container.BindInterfacesAndSelfTo<GridsUIMgr>().AsSingle();
    }
    private void bindFactories()
    {
        Container.BindFactory<GridUI, GridUI.Factory>()
            .FromComponentInNewPrefab(database.Prefab);
    }
}