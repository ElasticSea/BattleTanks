using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);        
    }
}