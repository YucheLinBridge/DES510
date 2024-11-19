using UnityEngine;
using Zenject;

public class SimpleSaveInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SimpleSaveMgr>().AsSingle();
    }
}