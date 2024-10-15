using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

#if UNITY_ANDROID
#endif

public class URL : MonoBehaviour
{
    public string UrlLink;

    private const string gomi_website = "http://www.gomiigo.com/";

    public void OpenURL()
    {
        Application.OpenURL(UrlLink);
    }

    public void MoreGames()
    {
        Application.OpenURL(gomi_website);
    }

    public static void Review()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.gomiigo.gomi22");
#endif

#if UNITY_IOS
        Device.RequestStoreReview();
#endif
        Debug.Log("Review");
    }
}
