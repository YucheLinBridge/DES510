using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMgr : MonoBehaviour
{
    [SerializeField] private GameObject settingPage;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider mainSlider, musicSlider, sfxSlider;
    [SerializeField] private float defaultVolume = .5f;

    private const string PARAM_MAIN = "MainVolume";
    private const string PARAM_MUSIC = "MusicVolume";
    private const string PARAM_SFX = "SFXVolume";

    private void Awake()
    {
        mainSlider.onValueChanged.AddListener(setMainVolume);
        musicSlider.onValueChanged.AddListener(setMusicVolume);
        sfxSlider.onValueChanged.AddListener(setSFXVolume);

        refesh();
    }

    private void setMainVolume(float val)
    {
        audioMixer.SetFloat(PARAM_MAIN, Mathf.Log10(val)*20);
        PlayerPrefs.SetFloat(PARAM_MAIN, val);
    }

    private void setMusicVolume(float val)
    {
        audioMixer.SetFloat (PARAM_MUSIC, Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat(PARAM_MUSIC, val);
    }

    private void setSFXVolume(float val)
    {
        audioMixer.SetFloat(PARAM_SFX, Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat(PARAM_SFX, val);
    }

    private void refesh()
    {
        if (!PlayerPrefs.HasKey(PARAM_MAIN))
        {
            PlayerPrefs.SetFloat(PARAM_MAIN, defaultVolume);
            mainSlider.value = defaultVolume;
        }
        else
        {
            mainSlider.value = PlayerPrefs.GetFloat(PARAM_MAIN);
        }

        if (!PlayerPrefs.HasKey(PARAM_MUSIC))
        {
            PlayerPrefs.SetFloat(PARAM_MUSIC, defaultVolume);
            musicSlider.value = defaultVolume;
        }
        else
        {
            musicSlider.value = PlayerPrefs.GetFloat(PARAM_MUSIC);
        }

        if (!PlayerPrefs.HasKey(PARAM_SFX))
        {
            PlayerPrefs.SetFloat(PARAM_SFX, defaultVolume);
            sfxSlider.value = defaultVolume;
        }
        else
        {
            sfxSlider.value = PlayerPrefs.GetFloat(PARAM_SFX);
        }
    }

    public void ShowSetting()
    {
        settingPage.SetActive(true);
    }

    public void HideSetting()
    {
        settingPage.SetActive(false);
    }
}
