using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public static void LoadFirstLevel()
        {
            _currentLevel = 1;
            _instance.StartCoroutine(_instance.LoadLevelCoroutine());
        }

        public static void TryLoadNextLevel()
        {
            _currentLevel++;
            // TODO : Check if is the last level

            _instance.StartCoroutine(_instance.LoadLevelCoroutine());
        }

        private IEnumerator LoadLevelCoroutine()
        {
            yield return LoadLoadingSceneCoroutine();

            // TODO : Do some cool transition showing the level number
            yield return new WaitForSeconds(1);

            yield return LoadGameSceneCoroutine();
        }

        private IEnumerator LoadLoadingSceneCoroutine()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Const.Scenes.LoadingScene, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
                yield return null;
        }

        private IEnumerator LoadGameSceneCoroutine()
        {
            // TODO : Choose the correct scene based on the current level
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene-001"); // Overrides all current scenes and sets itself as the active scene

            while (!asyncLoad.isDone)
                yield return null;

            UnityAssert.AreEqual(SceneManager.GetActiveScene().name, "GameScene-001");
        }
    }
}

