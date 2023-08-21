using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public static class InputUtilities
    {
        public static Vector2 GetMouseWorldPoint()
        {
            Vector3 mouseScreenPoint = Input.mousePosition;
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(mouseScreenPoint);

            return mouseWorldPoint;
        }
    }

    public enum MouseButton
    {
        // Reference: https://docs.unity3d.com/ScriptReference/Input.GetMouseButtonDown.html
        Left = 0,
        Right = 1,
        Middle = 2
    }
}

