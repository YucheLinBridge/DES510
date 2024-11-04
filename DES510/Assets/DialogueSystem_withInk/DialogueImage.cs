using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueImage : MonoBehaviour
{
    [SerializeField] private Image Image;

    public void Set(Sprite img)
    {
        Image.sprite = img;
    }

    public void DestroyImage()
    {
        Destroy(this.gameObject);
    }
}
