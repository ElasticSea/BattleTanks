using UnityEngine;

namespace _Project.Scripts
{
    public class Entity : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private Team team;

        [Header("Refenreces")]
        [SerializeField] private Health health;
        [SerializeField] private GameObject explosionPrefab;
        
        public Team Team => team;

        private void Awake()
        {
            health.OnDeath += () =>
            {
                var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosion.GetComponent<ParticleSystem>().Play();
                explosion.GetComponent<AudioSource>().Play();
                
                Destroy(gameObject);
            };
        }
    }
}