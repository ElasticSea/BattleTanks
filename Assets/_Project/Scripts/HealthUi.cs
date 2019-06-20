using UnityEngine;
using _Framework.Scripts.Extensions;

namespace _Project.Scripts
{
    public class HealthUi : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform bar;
        [SerializeField] private Health health;

        private void LateUpdate()
        {
            bar.anchorMax = bar.anchorMax.SetX(health.CurrentHealth / health.MaxHealth);
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
}