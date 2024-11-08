using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;

public class Hint : MonoBehaviour
{
    [SerializeField] private float maxThickness,minThickness;
    [SerializeField] private float durationOneLoop = 0.5f;
    [SerializeField] private int loops = 10;
    [SerializeField] private List<Material> outlineMats = new List<Material>();

    private bool showing;
    private float outline_thickness;

    private void Start()
    {
        end();
    }

    public void ShowHint()
    {
        if (showing)
        {
            return;
        }

        showing = true;
        outline_thickness=minThickness;
        DOTween.To(()=>outline_thickness,x=>outline_thickness=x, maxThickness, durationOneLoop).SetLoops(loops,LoopType.Yoyo).OnComplete(end);
        
    }

    private void Update()
    {
        if (showing)
        {
            for (int i = 0; i < outlineMats.Count; i++)
            {
                outlineMats[i].SetFloat("_Outline_Thickness", outline_thickness);
            }
        }
        
    }

    private void end()
    {
        for (int i=0;i<outlineMats.Count;i++)
        {
            outlineMats[i].SetFloat("_Outline_Thickness",0);
        }
        showing=false;
    }
}
