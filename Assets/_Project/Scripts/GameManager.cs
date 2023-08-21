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
                GameObject dummyInstance = Instantiate(_dummyPrefab);
                dummyInstance.transform.position = InputUtilities.GetMouseWorldPoint();
            }
        }
    }
}

