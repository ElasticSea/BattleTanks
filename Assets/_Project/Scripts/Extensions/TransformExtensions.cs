using UnityEngine;

namespace _Framework.Scripts.Extensions
{
    public static class TransformExtensions
    {
        public static void Activate(this Transform t)
        {
            t.gameObject.Activate();
        }

        public static void Deactivate(this Transform t)
        {
            t.gameObject.Deactivate();
        }

        /// <summary>
        /// Sets localPosition to Vector3.zero, localRotation to Quaternion.identity, and localScale to Vector3.one
        /// </summary>
        public static void Reset(this Transform t)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }

        public static void MoveHierarchyLeft(this Transform t)
        {
            if(t.parent?.parent != null)
                t.SetParent(t.parent.parent);
        }

        public static void MoveHierarchyDown(this Transform t)
        {
            if (t.parent != null)
                t.SetSiblingIndex(Mathf.Min(t.GetSiblingIndex() + 1, t.parent.childCount - 1));
        }

        public static void MoveHierarchyUp(this Transform t)
        {
            if (t.parent != null)
                t.SetSiblingIndex(Mathf.Max(t.GetSiblingIndex() - 1, 0));
        }

        public static Vector3 TransformPoint(this Transform t, Vector3 position, Transform dest)
        {
            var world = t.TransformPoint(position);
            return dest.InverseTransformPoint(world);
        }
    }
}
