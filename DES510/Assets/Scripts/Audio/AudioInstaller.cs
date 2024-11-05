using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class AudioInstaller : MonoInstaller
{

    [SerializeField] private int defaultMusic;
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private Transform sFXsParent;

    [Inject]
    private SFXSetting SFX_setting;

    public override void InstallBindings()
    {
        bindInstance();
        bindScripts();
        bindFactories();
    }

    private void bindInstance()
    {
        Container.BindInstance(musicPlayer).WithId("music_player");
        Container.BindInstance(defaultMusic).WithId("defaultMusic");
    }

    private void bindScripts()
    {
        Container.BindInterfacesAndSelfTo<MusicsMgr>().AsSingle();
        Container.Bind<SFXMgr>().AsSingle();
    }

    private void bindFactories()
    {
        Container.BindFactory<AudioClip, SFX_mono, SFX_mono.Factory>().FromPoolableMemoryPool(pool =>pool.FromComponentInNewPrefab(SFX_setting.SFX_prefab).UnderTransform(sFXsParent));
    }
}