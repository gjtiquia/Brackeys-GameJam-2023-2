using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAssert = UnityEngine.Assertions.Assert;

namespace Project
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] LevelSO _levelSO;

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
            DontDestroyOnLoad(this.gameObject);
        }

        public static void LoadFirstLevel()
        {
            _currentLevel = 1;
            _instance.StartCoroutine(_instance.LoadLevelCoroutine());
        }

        public static void TryLoadNextLevel()
        {
            if (_instance == null) return;

            _currentLevel++;

            if (_instance._levelSO.LevelExists(_currentLevel))
            {
                _instance.StartCoroutine(_instance.LoadLevelCoroutine());
            }
            else
            {
                // TODO : Load finish scene? Thanks for playing?
            }
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
            string sceneName = _levelSO.GetGameSceneName(_currentLevel);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName); // Overrides all current scenes and sets itself as the active scene

            while (!asyncLoad.isDone)
                yield return null;

            UnityAssert.AreEqual(SceneManager.GetActiveScene().name, sceneName);
        }
    }
}

