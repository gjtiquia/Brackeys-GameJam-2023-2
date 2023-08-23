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
        private List<CoinAgent> _coinInstances;

        private bool _hasActiveCircle;
        private int _collectedCoins;

        private void Awake()
        {
            UnityAssert.IsNotNull(_circlePrefab);
        }

        private void Start()
        {
            RegisterAllPlatforms();
            RegisterAllCoins();

            InitializeEverything();
        }

        private void InitializeEverything()
        {
            _hasActiveCircle = false;
            _collectedCoins = 0;

            InitalizeAllPlatforms();
            InitalizeAllCoins();
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
            _circleInstance.gameObject.SetActive(false);
            InitializeEverything();
        }

        private void RegisterAllPlatforms()
        {
            _platformInstances = new List<PlatformAgent>();

            GameObject[] platformGameObjects = GameObject.FindGameObjectsWithTag(Const.Tags.Platform);
            foreach (GameObject platformGameObject in platformGameObjects)
            {
                bool componentExists = platformGameObject.TryGetComponent(out PlatformAgent platformInstance);
                if (!componentExists) continue;

                _platformInstances.Add(platformInstance);
            }
        }

        private void InitalizeAllPlatforms()
        {
            foreach (PlatformAgent platform in _platformInstances)
                platform.Initialize();
        }

        private void RegisterAllCoins()
        {
            _coinInstances = new List<CoinAgent>();

            GameObject[] coinGameObjects = GameObject.FindGameObjectsWithTag(Const.Tags.Coin);
            foreach (GameObject coinGameObject in coinGameObjects)
            {
                bool componentExists = coinGameObject.TryGetComponent(out CoinAgent coinInstance);
                if (!componentExists) continue;

                coinInstance.OnCollectedEvent += OnCoinCollected;
                _coinInstances.Add(coinInstance);
            }
        }

        private void InitalizeAllCoins()
        {
            foreach (CoinAgent coin in _coinInstances)
                coin.Initialize();
        }

        private void OnCoinCollected()
        {
            _collectedCoins++;

            if (_collectedCoins == _coinInstances.Count)
            {
                Debug.Log("All coins collected!");
                _circleInstance.OnCollectedAllCoins();
                LevelManager.TryLoadNextLevel();
            }
        }
    }
}

