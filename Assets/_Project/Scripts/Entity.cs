using UnityEngine;

namespace _Project.Scripts
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private Team team;

        public Team Team => team;
    }
}