using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class Health : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private bool isAlive = true;
        [SerializeField] private float currentHealth = 100;
        [SerializeField] private float maxHealth = 100;

        public float CurrentHealth => currentHealth;
        public float MaxHealth => maxHealth;

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