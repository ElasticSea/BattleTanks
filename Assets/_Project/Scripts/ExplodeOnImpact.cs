using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class ExplodeOnImpact : MonoBehaviour
    {
        [Header("Parameters")]
        public float maxDamage = 100f;
        public float explosionForce = 1000f;
        public float explosionRadius = 5f;                
        
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
                
                var targetRigibody = target.GetComponent<Rigidbody>();
                
                if (targetRigibody)
                {
                    targetRigibody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
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