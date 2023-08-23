using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "SO/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private string _gameSceneNamePrefix;
        [SerializeField] private List<string> _gameSceneIDs;

        public string GetGameSceneName(int currentLevel)
        {
            string id = _gameSceneIDs[currentLevel - 1]; // Because index starts at 0 but levels start at 1
            return _gameSceneNamePrefix + id;
        }

        public bool LevelExists(int currentLevel)
        {
            if (currentLevel <= 0 || currentLevel > _gameSceneIDs.Count)
                return false;

            return true;
        }
    }
}