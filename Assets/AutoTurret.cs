using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _Framework.Scripts.Extensions;
using _Project.Scripts;

public class AutoTurret : MonoBehaviour
{
    [SerializeField] private Tank tank;
    [SerializeField] private float range;
    [SerializeField] private Turret turret;
    
    void Update()
    {
        var enemies = Physics
            .OverlapSphere(transform.position, range)
            .Select(c => c.GetComponent<Tank>())
            .Where(t => t != null && t.Team != tank.Team)
            .ToList();
        
        if (enemies.Any())
        {
            var closest = enemies.Smallest(c => c.transform.position.Distance(transform.position));
            turret.Target = closest.transform;   
        }
        else
        {
            turret.Target = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
