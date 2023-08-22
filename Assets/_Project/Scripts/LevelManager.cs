using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAssert = UnityEngine.Assertions.Assert;

namespace Project
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager _instance;
        private static int _currentLevel;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
        }

        public static void GotoFirstLevel()
        {
            _currentLevel = 1;

            // TODO : Additive Loading Scene
            // Const.Scenes.LoadingScene
            // TODO : Change to first level game scene
        }

        public static void GotoNextLevel()
        {
            _currentLevel++; // TODO : Check if is the last level

            // TODO : Additive Loading Scene
            // TODO : Change to next level game scene
        }
    }
}

