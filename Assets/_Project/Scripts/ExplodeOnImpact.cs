using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class ExplodeOnImpact : MonoBehaviour
    {
        [Header("Parameters")]
        public float maxDamage = 100f;                    // The amount of damage done if the explosion is centred on a tank.
        public float explosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.
        
        [Header("References")]
        public ParticleSystem particleSystem;

        private void OnTriggerEnter (Collider other)
        {
            HitTargets();
            
            particleSystem.transform.parent = null;
            particleSystem.Play();
            
            var mainModule = particleSystem.main;
            Destroy (particleSystem.gameObject, mainModule.duration);

            Destroy (gameObject);
        }

        private void HitTargets()
        {
            var targets = Physics.OverlapSphere(transform.position, explosionRadius)
                .Where(c => c.GetComponent<Health>())
                .Select(c => c.GetComponent<Health>());

            foreach (var target in targets)
            {
                var damage = CalculateDamage(target.transform.position);
                target.Damage(damage);
            }
        }

        private float CalculateDamage (Vector3 targetPosition)
        {
            var explosionToTarget = targetPosition - transform.position;
            var explosionDistance = explosionToTarget.magnitude;
            var relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
            var damage = relativeDistance * maxDamage;
            damage = Mathf.Max (0f, damage);
            return damage;
        }
    }
}