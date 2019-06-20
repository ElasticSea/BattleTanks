using UnityEngine;

namespace _Project.Scripts
{
    public class ExplodeOnImpact : MonoBehaviour
    {
        [Header("References")]
        public ParticleSystem particleSystem;

        private void OnTriggerEnter (Collider other)
        {
            particleSystem.transform.parent = null;
            particleSystem.Play();
            
            var mainModule = particleSystem.main;
            Destroy (particleSystem.gameObject, mainModule.duration);

            Destroy (gameObject);
        }
    }
}