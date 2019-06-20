using UnityEngine;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float maxFireTime = 3;
        [SerializeField] private float minFireVelocity = 20;
        [SerializeField] private float maxFireVelocity = 80;
        [SerializeField] private float fireRate = 1;
        [SerializeField] private float moveSpeed = 10;
        [SerializeField] private float rotateSpeed = 90;
        [SerializeField] private Vector2 turretSpeed = new Vector2(180, 120);

        [Header("References")]
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Transform turret;
        [SerializeField] private Shell shellPrefab;
        [SerializeField] private Transform fireOriginTransform;
        
        private float firingStart;
        private bool firing;
        private float fireTime;

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

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

            TryToFire();
        }

        private void TryToFire()
        {
            if (Time.time - fireTime > fireRate)
            {
                if (Input.GetKeyDown(KeyCode.Space) && firing == false)
                {
                    firingStart = Time.time;
                    firing = true;
                }

                var timeDifference = Time.time - firingStart;

                if (Input.GetKeyUp(KeyCode.Space) && firing)
                {
                    Fire(Mathf.Lerp(minFireVelocity, maxFireVelocity, timeDifference / maxFireTime));
                    firing = false;
                }

                if (timeDifference >= maxFireTime && firing)
                {
                    Fire(maxFireVelocity);
                    firing = false;
                }
            }
        }

        private void Fire(float velocity)
        {
            fireTime = Time.time;
            var shellInstance = Instantiate(shellPrefab, fireOriginTransform.position, Quaternion.identity);
            var shellRb = shellInstance.GetComponent<Rigidbody>();
            shellRb.velocity = velocity * fireOriginTransform.forward; 
        }
    }
}