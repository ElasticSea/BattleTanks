using UnityEngine;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float moveSpeed = 10;

        [Header("References")]
        [SerializeField] private Rigidbody rigidbody;
        
        private void FixedUpdate()
        {
            var keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            rigidbody.MovePosition(transform.position + transform.forward * keyboardInput.y * moveSpeed * Time.deltaTime);
        }
    }
}