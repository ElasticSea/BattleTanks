using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private bool isAlive = true;
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth;

        public float CurrentHealth => currentHealth;
        public float MaxHealth => maxHealth;

        public bool IsAlive => isAlive;

        public Action OnDeath = () => { };

        public void Damage(float damage)
        {
            if (isAlive)
            {
                currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
                if (currentHealth == 0)
                {
                    isAlive = false;
                    OnDeath();
                }
            }
        }
    }
}