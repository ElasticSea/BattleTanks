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
        public bool Firing { get; private set; }
        private float fireTime;
        private float timeDifference;

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
                var isDown = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
                var isPressed = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
                var isUp = Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0);
                
                if ((isDown || isPressed) && Firing == false)
                {
                    firingStart = Time.time;
                    Firing = true;
                }

                timeDifference = Time.time - firingStart;

                if (isUp && Firing)
                {
                    Fire(CurrentVelocity);
                    Firing = false;
                }

                if (timeDifference >= maxFireTime && Firing)
                {
                    Fire(CurrentVelocity);
                    Firing = false;
                }
            }
        }

        public float CurrentVelocity => Mathf.Lerp(minFireVelocity, maxFireVelocity, timeDifference / maxFireTime);

        public float MaxFireVelocity => maxFireVelocity;
        public float MinFireVelocity => minFireVelocity;

        private void Fire(float velocity)
        {
            fireTime = Time.time;
            var shellInstance = Instantiate(shellPrefab, fireOriginTransform.position, Quaternion.identity);
            var shellRb = shellInstance.GetComponent<Rigidbody>();
            shellRb.velocity = velocity * fireOriginTransform.forward; 
        }
    }
}