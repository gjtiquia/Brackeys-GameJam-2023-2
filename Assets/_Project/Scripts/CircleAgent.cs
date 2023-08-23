using System;
using UnityEngine;
using UnityAssert = UnityEngine.Assertions.Assert;
using UnityRandom = UnityEngine.Random;

namespace Project
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class CircleAgent : MonoBehaviour
    {
        public event Action OnDestroySelfEvent;

        [Header("References")]
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private bool _hasFired;

        // PUBLIC METHODS
        public void Initialize()
        {
            _hasFired = false;

            _rigidbody.velocity = Vector2.zero;
            _spriteRenderer.color = _gameSettings.ColorPaletteSO.GetColor(ColorPaletteName.Light);
            _spriteRenderer.enabled = true;
            _collider.enabled = true;
        }

        public void OnCollectedAllCoins()
        {
            // TODO : some particle effect?
            // TODO : Some disappear effect?

            _rigidbody.velocity = Vector2.zero;
        }

        // MonoBehaviour INTERFACE
        private void Awake()
        {
            _spriteRenderer.enabled = false;
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();

            UnityAssert.IsNotNull(_gameSettings);
            UnityAssert.IsNotNull(_gameSettings.ColorPaletteSO);
            UnityAssert.IsNotNull(_spriteRenderer);
        }

        private void Update()
        {
            if (!_hasFired && !Input.GetMouseButton((int)MouseButton.Left))
            {
                Vector2 mouseReleasePoint = InputUtilities.GetMouseWorldPoint();
                Fire(mouseReleasePoint);
                _hasFired = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == Const.Tags.Destroy)
            {
                DestroySelf();
            }
        }

        // PRIVATE METHODS
        private void Fire(Vector2 mouseReleasePoint)
        {
            Vector2 direction = ((Vector2)transform.position - mouseReleasePoint).normalized;

            while (direction.sqrMagnitude <= 0.001f)
                direction = ((Vector2)UnityRandom.onUnitSphere).normalized;

            UnityAssert.AreApproximatelyEqual(direction.sqrMagnitude, 1f);

            _rigidbody.AddForce(_gameSettings.LaunchSpeed * direction, ForceMode2D.Impulse);
        }

        private void DestroySelf()
        {
            _spriteRenderer.enabled = false;
            _rigidbody.velocity = Vector2.zero;
            _collider.enabled = false;

            OnDestroySelfEvent?.Invoke();
        }
    }
}
