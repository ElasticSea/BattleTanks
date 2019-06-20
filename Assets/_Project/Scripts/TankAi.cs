using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using _Framework.Scripts.Extensions;

namespace _Project.Scripts
{
    public class TankAi : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float idleRadius = 5;
        [SerializeField] private float agroRadius = 10;
        
        [Header("References")]
        [SerializeField] private Patrol patrol;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Entity tank;
        [SerializeField] private Turret turret;
        
        private void Update()
        {
            var radius = isAgro ? agroRadius : idleRadius;
            
            var enemies = Physics
                .OverlapSphere(transform.position, radius)
                .Select(c => c.GetComponent<Entity>())
                .Where(t => t != null && t.Team != tank.Team)
                .ToList();
        
            if (enemies.Any())
            {
                var closest = enemies.Smallest(c => c.transform.position.Distance(transform.position));
                
                patrol.enabled = false;
                agent.destination = closest.transform.position;
                turret.Target = closest.transform;
            }
            else
            {
                patrol.enabled = true;
                turret.Target = null;
            }
        }
        
        private bool isAgro => turret.Target != null;

        private void OnDrawGizmos()
        {
            var radius = isAgro ? agroRadius : idleRadius;
            var color = isAgro ? Color.red : Color.yellow;
            
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}