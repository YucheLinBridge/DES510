using UnityEngine;
using UnityEngine.Events;

public class ObjEvent : MonoBehaviour
{
    public UnityEvent OnShow, OnHide;

    private void OnEnable()
    {
        Debug.Log($"{name}:Enable");
        OnShow.Invoke();
    }

    private void OnDisable()
    {
        Debug.Log($"{name}:Disable");
        OnHide.Invoke();
    }

}
