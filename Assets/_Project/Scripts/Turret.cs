using UnityEngine;
using _Framework.Scripts.Extensions;

namespace _Project.Scripts
{
    public class Turret : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float fireInterval = 1;
        [SerializeField] private float sheelSpeed = 1;
        [SerializeField] private float marginOfErrorDistance = 1;

        [Header("References")]
        [SerializeField] private Shell shellPrefab;
        [SerializeField] private Transform target;
        [SerializeField] private Transform fireOriginTransform;

        private float lastFireTime;
        private Vector3 calculatedVelocity;

        public Transform Target
        {
            get { return target; }
            set { target = value; }
        }

        private float Distance => target.transform.position.FromXZ().Distance(transform.position.FromXZ());

        void Update()
        {
            if (target)
            {
                var impactTime = Distance / sheelSpeed;
                calculatedVelocity = CalculateTrajectoryVelocity(fireOriginTransform.position, target.position, impactTime);

                transform.rotation = Quaternion.LookRotation(calculatedVelocity);

                if (Time.time - lastFireTime > fireInterval)
                {
                    Fire();
                    lastFireTime = Time.time;
                }
            }
            else
            {

                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.LookRotation(Vector3.forward), 0.1f);
            }
        }

        private void Fire()
        {
            var rngAngle = Random.value * Mathf.PI * 2;
            var rngDistance = Random.value * marginOfErrorDistance;

            var offsetX = Mathf.Cos(rngAngle) * rngDistance;
            var offsetZ = Mathf.Sin(rngAngle) * rngDistance;
            var offset = new Vector3(offsetX, 0, offsetZ);
            var targetPosition = target.position + offset;

            var impactTime = Distance / sheelSpeed;
            var calculatedVelocity = CalculateTrajectoryVelocity(fireOriginTransform.position, targetPosition, impactTime);

            var shellInstance = Instantiate(shellPrefab, fireOriginTransform.position, Quaternion.identity);
            var shellRb = shellInstance.GetComponent<Rigidbody>();
            shellRb.velocity = calculatedVelocity;
        }

        private Vector3 CalculateTrajectoryVelocity(Vector3 origin, Vector3 target, float t)
        {
            var vx = (target.x - origin.x) / t;
            var vz = (target.z - origin.z) / t;
            var vy = ((target.y - origin.y) - 0.5f * Physics.gravity.y * t * t) / t;
            return new Vector3(vx, vy, vz);
        }
    }
}