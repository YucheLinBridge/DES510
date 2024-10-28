using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MusicsSetting", menuName = "Installers/MusicsSetting")]
public class MusicsSetting : ScriptableObjectInstaller<MusicsSetting>
{
    [SerializeField] private float timeFadeIn = 0.5f, timeFadeOut = .5f;
    [SerializeField] private List<AudioClip> musics=new List<AudioClip>();


    public float TIME_IN => timeFadeIn;
    public float TIME_OUT => timeFadeOut;

    public AudioClip GetMusic(int index) {
        return musics[index];
    }

    public override void InstallBindings()
    {
        Container.BindInstance(this);

    }
}