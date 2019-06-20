using System;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class ColorByTeam : MonoBehaviour
    {
        [SerializeField] private Tank tank;
        
        private void Awake()
        {
            foreach (var renderer in tank.GetComponentsInChildren<Renderer>())
            {
                renderer.materials = renderer.materials.Select(m => getMat(m)).ToArray();
            }
        }

        private Material getMat(Material material)
        {
            var range = (float) tank.Team / Enum.GetValues(typeof(Team)).Length;
            material.color = material.color.TransformHSV(range * 360, 1, 1);
            return material;
        }
    }
}