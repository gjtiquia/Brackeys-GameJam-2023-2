using System;
using UnityEngine;
using UnityAssert = UnityEngine.Assertions.Assert;
using UnityRandom = UnityEngine.Random;

namespace Project
{
    [RequireComponent(typeof(Collider2D))]
    public class CoinAgent : MonoBehaviour
    {
        public event Action OnCollectedEvent;

        [SerializeField] private GameObject _displayGameObject;

        private Collider2D _collider;

        // MonoBehaviour INTERFACE
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();

            UnityAssert.IsNotNull(_displayGameObject);
            UnityAssert.IsNotNull(_collider);

            _collider.isTrigger = true;
        }

        public void Initialize()
        {
            _displayGameObject.SetActive(true);
            _collider.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            GetCollected();
        }

        // PRIVATE METHODs
        private void GetCollected()
        {
            _displayGameObject.SetActive(false);
            _collider.enabled = false;

            OnCollectedEvent?.Invoke();
        }
    }
}
