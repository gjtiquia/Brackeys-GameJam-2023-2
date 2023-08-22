using System;
using UnityEngine;
using UnityAssert = UnityEngine.Assertions.Assert;
using UnityRandom = UnityEngine.Random;

namespace Project
{
    public class PlatformAgent : MonoBehaviour
    {
        const int MAX_HEALTH = 2;

        [SerializeField] private GameSettingsSO _gameSettingsSO;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _boxCollider;

        private int _health
        {
            get => m_health;
            set
            {
                m_health = value;
                OnHealthChanged();
            }
        }
        private int m_health;

        // MonoBehaviour INTERFACE
        private void Awake()
        {
            UnityAssert.IsNotNull(_gameSettingsSO);
            UnityAssert.IsNotNull(_spriteRenderer);
            UnityAssert.IsNotNull(_boxCollider);
        }

        public void Initialize()
        {
            _spriteRenderer.color = _gameSettingsSO.ColorPaletteSO.GetColor(ColorPaletteName.Light); // TODO : Maybe tween the color?
            _boxCollider.enabled = true;
            _health = MAX_HEALTH;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _health -= 1;
        }

        // PRIVATE METHODS
        private void OnHealthChanged()
        {
            switch (_health)
            {
                case MAX_HEALTH:
                    _spriteRenderer.color = _gameSettingsSO.ColorPaletteSO.GetColor(ColorPaletteName.Light);
                    return;

                case MAX_HEALTH - 1:
                    _spriteRenderer.color = _gameSettingsSO.ColorPaletteSO.GetColor(ColorPaletteName.Medium);
                    return;

                case MAX_HEALTH - 2:
                    _spriteRenderer.color = _gameSettingsSO.ColorPaletteSO.GetColor(ColorPaletteName.Dark);
                    _boxCollider.enabled = false;
                    return;
            }
        }
    }
}
