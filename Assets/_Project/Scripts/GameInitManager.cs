using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAssert = UnityEngine.Assertions.Assert;

namespace Project
{
    public class GameInitManager : MonoBehaviour
    {
        public void StartGame()
        {
            LevelManager.GotoFirstLevel();
        }
    }
}

