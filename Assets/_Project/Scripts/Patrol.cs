using UnityEngine;
using UnityEngine.AI;
using _Framework.Scripts.Extensions;

namespace _Project.Scripts
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float closeRadius;
        [SerializeField] private Transform[] points;

        private Transform target;
        private int? i;
        
        private void Update()
        {
            if (i == null)
            {
                i = points.IndexOf(points.Smallest(t => t.position.Distance(transform.position)));
            }

            if (agent.remainingDistance < agent.stoppingDistance + .01f)
            {
                i = (i + 1) % points.Length;
            }
            
            agent.destination = points[i.Value].position;
        }
    }
}