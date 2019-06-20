using System;
using UnityEngine;

namespace _Framework.Scripts.Extensions
{
    public static class TextMeshProExtensions
    {
        public static string Size(this string s, float sizePercent)
        {
            return $"<size={sizePercent}%>{s}</size>";
        }

        public static string Color(this string s, Color color)
        {
            return $"<color={color.colorToHex().Replace("0x", "#")}>{s}</color>";
        }
    }
}