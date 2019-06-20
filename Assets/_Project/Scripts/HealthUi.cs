using UnityEngine;
using _Framework.Scripts.Extensions;

namespace _Project.Scripts
{
    public class HealthUi : MonoBehaviour
    {
        [SerializeField] private RectTransform bar;
        [SerializeField] private Health health;

        private void Update()
        {
            bar.anchorMax = bar.anchorMax.SetX(health.CurrentHealth / health.MaxHealth);
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
}