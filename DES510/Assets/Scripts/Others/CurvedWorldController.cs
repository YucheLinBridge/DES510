using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedWorldController : MonoBehaviour
{
    [SerializeField] private Vector2 defaultSetting;
    [SerializeField] private float transitionDuration = 1f;
    [SerializeField] private List<Material> materials = new List<Material>();

    private float alpha;
    private Vector2 origin;

    // Start is called before the first frame update
    void Start()
    {
        ChangeImmediately(defaultSetting);
    }

    public void ChangeImmediately(Vector2 newSetting)
    {
        foreach (var item in materials) {
            item.SetFloat("_Sideways_Strength",newSetting.x);
            item.SetFloat("_Backwards_Strength", newSetting.y);
        }
        origin = newSetting;
    }
    public void ChangeImmediately(string str_newsetting)
    {
        var strlst = str_newsetting.Split(',');
        if (strlst.Length != 2)
        {
            Debug.LogError("There must be two params");
            return;
        }
        ChangeImmediately(new Vector2(float.Parse(strlst[0]), float.Parse(strlst[1])));
    }

    public void Change(string str_newsetting)
    {
        var strlst=str_newsetting.Split(',');
        if (strlst.Length!=2)
        {
            Debug.LogError("There must be two params");
            return;
        }
        Change(new Vector2(float.Parse(strlst[0]), float.Parse(strlst[1])));
    }


    public void Change(Vector2 newSetting)
    {
        StartCoroutine(WaitForChanging(newSetting));
    }

    IEnumerator WaitForChanging(Vector2 newSetting)
    {
        alpha = 0;
        float ratio;
        while (alpha< transitionDuration)
        {
            ratio = alpha / transitionDuration;
            foreach (var item in materials)
            {
                item.SetFloat("_Sideways_Strength", Mathf.Lerp(origin.x, newSetting.x, ratio));
                item.SetFloat("_Backwards_Strength", Mathf.Lerp(origin.y, newSetting.y, ratio));
            }
            alpha += Time.deltaTime;
            yield return null;
        }
        origin=newSetting;
    }

}
