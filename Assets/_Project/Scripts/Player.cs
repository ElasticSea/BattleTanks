using UnityEngine;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float moveSpeed = 10;
        [SerializeField] private float rotateSpeed = 90;
        [SerializeField] private Vector2 turretSpeed = new Vector2(3, 2);

        [Header("References")]
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Transform turret;
        
        private void FixedUpdate()
        {
            var keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            rigidbody.MovePosition(transform.position + transform.forward * keyboardInput.y * moveSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up, rotateSpeed * keyboardInput.x * Time.deltaTime, Space.World);
            
        }
        
        private void Update()
        {
            var mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            turret.Rotate(Vector3.up, mouseInput.x * turretSpeed.x * Time.deltaTime, Space.World);
            turret.Rotate(Vector3.left, mouseInput.y * turretSpeed.y * Time.deltaTime, Space.Self);
        }
    }
}