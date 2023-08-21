using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "GameSettingsSO", menuName = "SO/GameSettingsSO")]
    public class GameSettingsSO : ScriptableObject
    {
        public float LaunchSpeed;
        public ColorPaletteSO ColorPaletteSO;
    }
}