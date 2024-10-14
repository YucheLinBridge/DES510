using UnityEngine;
using Zenject;

public class MapCreatorInstaller : MonoInstaller
{
    [SerializeField] private GameObject GridPrefab;

    public override void InstallBindings()
    {
        bindFactory();
    }

    private void bindFactory()
    {
        Container.BindFactory<TmpGrid, TmpGrid.Factory>()
            .FromComponentInNewPrefab(GridPrefab);
    }
}