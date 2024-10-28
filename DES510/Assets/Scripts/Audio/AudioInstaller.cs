using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class AudioInstaller : MonoInstaller
{
    [SerializeField] private AudioSource musicPlayer;

    public override void InstallBindings()
    {
        bindInstance();
        bindScripts();
    }

    private void bindInstance()
    {
        Container.BindInstance(musicPlayer).WithId("music_player");
    }

    private void bindScripts()
    {
        Container.BindInterfacesAndSelfTo<MusicsMgr>().AsSingle();
    }
}