using UnityEngine;
using _Framework.Scripts.Extensions;

namespace _Project.Scripts
{
    public class FireUi : MonoBehaviour
    {
        [SerializeField] private RectTransform bar;
        [SerializeField] private Player player;

        private void Update()
        {
            print(player.Firing);
            print(player.CurrentVelocity);
            var progress = player.Firing ? Mathf.InverseLerp(player.MinFireVelocity, player.MaxFireVelocity, player.CurrentVelocity) : 0;
            bar.anchorMax = bar.anchorMax.SetX(progress);
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
}