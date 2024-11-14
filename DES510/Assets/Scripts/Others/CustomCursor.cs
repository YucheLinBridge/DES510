using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D defaultCursor;

    private void Awake()
    {
        Cursor.SetCursor(defaultCursor,Vector2.zero,CursorMode.Auto);
    }

}
