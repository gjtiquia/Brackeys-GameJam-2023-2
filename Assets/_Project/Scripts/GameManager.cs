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
        private List<PlatformAgent> _platformInstances;
        private bool _hasActiveCircle;

        private void Awake()
        {
            _hasActiveCircle = false;
            UnityAssert.IsNotNull(_circlePrefab);
        }

        private void Start()
        {
            _platformInstances = new List<PlatformAgent>();

            GameObject[] platformGameObjects = GameObject.FindGameObjectsWithTag(Const.Tags.Platform);
            foreach (GameObject platformGameObject in platformGameObjects)
            {
                bool componentExists = platformGameObject.TryGetComponent(out PlatformAgent platformInstance);
                if (!componentExists) continue;

                _platformInstances.Add(platformInstance);
            }

            InitalizeAllPlatforms();
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

            _circleInstance.gameObject.SetActive(true);
            circleInstance.transform.position = InputUtilities.GetMouseWorldPoint();
            circleInstance.Initialize();

            _hasActiveCircle = true;
        }

        private CircleAgent LazyLoadCircleInstance()
        {
            if (_circleInstance == null)
            {
                _circleInstance = Instantiate(_circlePrefab);
                _circleInstance.OnDestroySelfEvent += OnActiveCircleDestroyedEvent;
            }

            return _circleInstance;
        }

        private void OnActiveCircleDestroyedEvent()
        {
            _hasActiveCircle = false;
            _circleInstance.gameObject.SetActive(false);

            InitalizeAllPlatforms();
        }

        private void InitalizeAllPlatforms()
        {
            foreach (PlatformAgent platform in _platformInstances)
                platform.Initialize();
        }
    }
}

