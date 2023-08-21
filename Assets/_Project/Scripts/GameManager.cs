using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _dummyPrefab;

        private void Update()
        {
            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                Vector3 mouseScreenPoint = Input.mousePosition;
                Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(mouseScreenPoint);

                Vector3 spawnPoint = mouseWorldPoint;
                spawnPoint.z = 0;

                GameObject dummyInstance = Instantiate(_dummyPrefab);
                dummyInstance.transform.position = spawnPoint;
            }
        }

        // HELPERS
        enum MouseButton
        {
            // Reference: https://docs.unity3d.com/ScriptReference/Input.GetMouseButtonDown.html
            Left = 0,
            Right = 1,
            Middle = 2
        }
    }
}

