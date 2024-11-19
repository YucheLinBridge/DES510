using UnityEngine;
using Zenject;

public class FXInstaller : MonoInstaller
{
    [SerializeField] private Transform FXParent;
    [SerializeField] private GameObject WalkTargetFX;

    public override void InstallBindings()
    {
        bindFactories();
    }

    private void bindFactories()
    {
        Container.BindFactory<FX_WalkTarget_mono,FX_WalkTarget_mono.Factory>().FromPoolableMemoryPool(pool=>pool.FromComponentInNewPrefab(WalkTargetFX).UnderTransform(FXParent));
    }
}