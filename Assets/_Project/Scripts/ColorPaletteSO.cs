using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public enum ColorPaletteName
    {
        Light,
        Medium,
        Dark
    }

    [CreateAssetMenu(fileName = "ColorPaletteSO", menuName = "SO/ColorPaletteSO")]
    public class ColorPaletteSO : ScriptableObject
    {
        [SerializeField] private List<ColorPalette> ColorPalettes;

        public Color GetColor(ColorPaletteName name)
        {
            ColorPalette palette = ColorPalettes.Find(x => x.Name == name);
            if (palette == null)
            {
                Debug.LogError($"{this.name}: Cannot find color with name {name}, returning White instead...");
                return new Color(0, 0, 0);
            }

            return palette.Color;
        }

        // HELPERS
        [System.Serializable]
        public class ColorPalette
        {
            public ColorPaletteName Name;
            public Color Color;
        }
    }
}