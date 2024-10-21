using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class BlackScreen_mono : MonoBehaviour
{
    [SerializeField] private CanvasGroup panel;

    [Header("Fade in")]
    [SerializeField] private bool FadeInStart;
    [SerializeField] private float FadeInTime;
    [SerializeField] private float FadeInAlpha_start, FadeInAlpha_end;
    [SerializeField] private Ease FadeInEase;
    [SerializeField] private UnityEvent OnFadeIn;


    [Header("Fade out")]
    [SerializeField] private float FadeOutTime;
    [SerializeField] private float FadeOutAlpha_start, FadeOutAlpha_end;
    [SerializeField] private Ease FadeOutEase;
    [SerializeField] private UnityEvent OnFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        if (FadeInStart)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        panel.alpha = FadeInAlpha_start;
        panel.blocksRaycasts = true;
        panel.DOFade(FadeInAlpha_end, FadeInTime).SetEase(FadeInEase).OnComplete(() => {
            OnFadeIn?.Invoke();
            panel.blocksRaycasts = false;
        });
    }

    public void FadeOut()
    {
        panel.alpha = FadeOutAlpha_start;
        panel.blocksRaycasts = true;
        panel.DOFade(FadeOutAlpha_end, FadeOutTime).SetEase(FadeOutEase).OnComplete(() => {
            OnFadeOut?.Invoke();
            panel.blocksRaycasts = false;
        });
    }
}
