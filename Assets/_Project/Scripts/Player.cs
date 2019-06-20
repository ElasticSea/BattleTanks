using UnityEngine;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float moveSpeed = 10;
        
        private void Update()
        {
            var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.position += input * moveSpeed * Time.deltaTime;
        }
    }
}