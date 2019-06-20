using UnityEngine;

namespace _Project.Scripts
{
    public class Shell : MonoBehaviour
    {
        [Header("Refenreces")]
        [SerializeField] private Rigidbody rigidbody;
    
        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(rigidbody.velocity);        
        }
    }
}