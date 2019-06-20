using System;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class ColorByTeam : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Entity tank;
        
        private void Awake()
        {
            foreach (var renderer in tank.gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.materials = renderer.materials.Select(CreateMaterial).ToArray();
            }
        }

        private Material CreateMaterial(Material material)
        {
            var range = (float) tank.Team / Enum.GetValues(typeof(Team)).Length;
            material.color = material.color.TransformHSV(range * 360, 1, 1);
            return material;
        }
    }
}