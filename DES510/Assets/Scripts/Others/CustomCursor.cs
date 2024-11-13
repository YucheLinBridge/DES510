using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Sprite defaultCursor;

    private void Awake()
    {
        Cursor.SetCursor(defaultCursor.texture,Vector2.zero,CursorMode.Auto);
    }

}
