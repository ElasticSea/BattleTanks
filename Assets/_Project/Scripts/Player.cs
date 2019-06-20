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
            var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            var cameraAdjust = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            rigidbody.MovePosition(transform.position + cameraAdjust * input * moveSpeed * Time.deltaTime);
        }
    }
}