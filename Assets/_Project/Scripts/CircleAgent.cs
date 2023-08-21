using UnityAssert = UnityEngine.Assertions.Assert;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CircleAgent : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Rigidbody2D _rigidbody;
        private bool _hasFired;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _hasFired = false;

            UnityAssert.IsNotNull(_gameSettings);
            UnityAssert.IsNotNull(_gameSettings.ColorPaletteSO);
            UnityAssert.IsNotNull(_spriteRenderer);

            _spriteRenderer.color = _gameSettings.ColorPaletteSO.GetColor(ColorPaletteName.Light);
        }

        private void Update()
        {
            if (!_hasFired && !Input.GetMouseButton((int)MouseButton.Left))
            {
                Vector2 mouseWorldPoint = InputUtilities.GetMouseWorldPoint();

                Vector2 direction = ((Vector2)transform.position - mouseWorldPoint).normalized;

                while (direction.sqrMagnitude <= 0.001f)
                    direction = ((Vector2)Random.onUnitSphere).normalized;

                UnityAssert.AreApproximatelyEqual(direction.sqrMagnitude, 1f);

                _rigidbody.AddForce(_gameSettings.LaunchSpeed * direction, ForceMode2D.Impulse);
                _hasFired = true;
            }
        }
    }
}
