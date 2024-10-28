using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class MusicsMgr :IInitializable
{
    [SerializeField] private int defaultMusicIndex;

    [Inject(Id = "music_player")]
    private AudioSource music_player;


    [Inject]
    private MusicsSetting setting;

    private int playing_index = -1;
    private int premusic = -1;

    public void Initialize()
    {
        Play(defaultMusicIndex);
    }

    public void Play(int index)
    {
        if (playing_index == -1)
        {
            music_player.clip = setting.GetMusic(index);
            music_player.Play();
            music_player.volume = 0;
            DOTween.To(() => music_player.volume, x => music_player.volume=x, 1, setting.TIME_IN);
            playing_index = index;
        }
        else
        {
            DOTween.To(() => music_player.volume, x => music_player.volume = x, 0, setting.TIME_OUT).OnComplete(() => {
                music_player.clip = setting.GetMusic(index);
                music_player.Play();
                DOTween.To(() => music_player.volume, x => music_player.volume = x, 1, setting.TIME_IN);
            });
            premusic = playing_index;
            playing_index = index;
        }
    }
}
