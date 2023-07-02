using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorImage;
    public CursorMode cursorMode = CursorMode.ForceSoftware;

    private void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, cursorMode);
    }
}