using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float maxFireTime;
        [SerializeField] private float minFireVelocity;
        [SerializeField] private float maxFireVelocity;
        [SerializeField] private float fireRate;
        [SerializeField] private Rigidbody rigibody;
        [SerializeField] private Transform turret;
        [SerializeField] private float moveSpeed = 10;
        [SerializeField] private float rotateSpeed = 90;
        [SerializeField] private Vector2 turrentRotationSpeed = new Vector2(3, 2);
        [SerializeField] private Health health;
        
        [SerializeField] private Shell shellPrefab;
        [SerializeField] private Transform fireOriginTransform;
        private float firingStart;
        private bool firing;
        private float fireTime;

        private void Awake()
        {
            health.OnDeath += () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            };
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            var delta = new Vector2( Input.GetAxis("Mouse X") ,Input.GetAxis("Mouse Y"));
   
            turret.Rotate(Vector3.up, turrentRotationSpeed.x * delta.x, Space.World);
            turret.Rotate(Vector3.left, turrentRotationSpeed.y * delta.y, Space.Self);
        
            var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            rigibody.MovePosition( rigibody.position + Time.deltaTime * input.z * transform.forward * moveSpeed);
            transform.Rotate(Vector3.up, input.x * rotateSpeed * Time.deltaTime * Mathf.Sign(input.z), Space.World);

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