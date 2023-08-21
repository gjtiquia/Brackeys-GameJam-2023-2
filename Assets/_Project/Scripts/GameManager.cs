using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAssert = UnityEngine.Assertions.Assert;

namespace Project
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CircleAgent _circlePrefab;
        private CircleAgent _circleInstance;

        private bool _hasActiveCircle;

        private void Awake()
        {
            _hasActiveCircle = false;
            UnityAssert.IsNotNull(_circlePrefab);
        }

        private void Update()
        {
            if (!_hasActiveCircle && Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                Vector3 spawnPosition = InputUtilities.GetMouseWorldPoint();
                SpawnCircleAgent(spawnPosition);
            }
        }

        // PRIVATE METHODS
        private void SpawnCircleAgent(Vector3 spawnPosition)
        {
            CircleAgent circleInstance = LazyLoadCircleInstance();

            circleInstance.transform.position = InputUtilities.GetMouseWorldPoint();
            circleInstance.Initialize();

            _hasActiveCircle = true;
        }

        private CircleAgent LazyLoadCircleInstance()
        {
            if (_circleInstance == null)
            {
                _circleInstance = Instantiate(_circlePrefab);
                _circleInstance.OnDestroySelfEvent += OnDestroySelfEvent;
            }

            return _circleInstance;
        }

        private void OnDestroySelfEvent()
        {
            _hasActiveCircle = false;
            // TODO : Return to object pool
        }
    }
}

