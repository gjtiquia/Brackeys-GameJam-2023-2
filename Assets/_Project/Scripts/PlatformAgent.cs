using System;
using UnityEngine;
using UnityAssert = UnityEngine.Assertions.Assert;
using UnityRandom = UnityEngine.Random;

namespace Project
{
    public class PlatformAgent : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO _gameSettingsSO;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _boxCollider;

        private void Awake()
        {
            UnityAssert.IsNotNull(_gameSettingsSO);
            UnityAssert.IsNotNull(_spriteRenderer);
            UnityAssert.IsNotNull(_boxCollider);

            Initialize();
        }

        public void Initialize()
        {
            // TODO : Should be referenced by GameManager to initialize whenever player dies. Maybe on Start, find all game objects with tag? 

            _spriteRenderer.color = _gameSettingsSO.ColorPaletteSO.GetColor(ColorPaletteName.Light); // TODO : Maybe tween the color?
            _boxCollider.enabled = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // TODO : Disappear in stages, need to add Medium color palette

            _spriteRenderer.color = _gameSettingsSO.ColorPaletteSO.GetColor(ColorPaletteName.Dark);
            _boxCollider.enabled = false;
        }
    }
}
