using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SFXSetting", menuName = "Installers/SFXSetting")]
public class SFXSetting : ScriptableObjectInstaller<SFXSetting>
{
    public GameObject SFX_prefab;
    [SerializeField] private List<AudioClip> SFXs = new List<AudioClip>();

    public AudioClip GetSFX(int index) {
        return SFXs[index];
    }

    public override void InstallBindings()
    {
        Container.BindInstance(this);

    }
}