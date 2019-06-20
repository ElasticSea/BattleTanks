using UnityEngine;

namespace _Framework.Scripts.Extensions
{
    public static class CameraExtensions
    {
        public static Vector3? ViewportPointToGround(this UnityEngine.Camera camera, Vector2 viewport)
        {
            var ray = camera.ViewportPointToRay(viewport);
            Plane hPlane = new Plane(Vector3.up, Vector3.zero);
            float distance;
            if (hPlane.Raycast(ray, out distance))
            {
                return ray.GetPoint(distance);
            }
            return null;
        }

        public static void ZoomOrtho(this UnityEngine.Camera Camera, Bounds bounds)
        {
            Camera.orthographicSize = 1;

            var screenRect = Camera.WorldToScreenBounds(bounds);

            Camera.orthographicSize = Mathf.Max(screenRect.width, screenRect.height);

            Camera.transform.localPosition += Camera.transform.up * (screenRect.min.y * 2 + (-1 + screenRect.height));
            Camera.transform.localPosition += Camera.transform.right * (screenRect.min.x * 2 + (-1 + screenRect.width));
        }

        public static Rect WorldToScreenBounds(this UnityEngine.Camera cam, Bounds bounds)
        {
            Vector3 cen = bounds.center;
            Vector3 ext = bounds.extents;
            Vector2[] extentPoints = {
                cam.WorldToViewportPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z-ext.z)),
                cam.WorldToViewportPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z-ext.z)),
                cam.WorldToViewportPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z+ext.z)),
                cam.WorldToViewportPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z+ext.z)),
                cam.WorldToViewportPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z-ext.z)),
                cam.WorldToViewportPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z-ext.z)),
                cam.WorldToViewportPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z+ext.z)),
                cam.WorldToViewportPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z+ext.z))
            };
            Vector2 min = extentPoints[0];
            Vector2 max = extentPoints[0];
            foreach (Vector2 v in extentPoints)
            {
                min = Vector2.Min(min, v);
                max = Vector2.Max(max, v);
            }
            return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
        }
        
        public static T RaycastScreen<T>(this UnityEngine.Camera cam, Vector3 screenPosition, float maxDistance = float.MaxValue, int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            var ray = cam.ScreenPointToRay(screenPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance, layerMask, queryTriggerInteraction))
            {
                
                return hit.transform.GetComponent<T>();
            }

            return default(T);
        }

        public static Vector3 FocusSphere(this UnityEngine.Camera camera, GameObject go)
        {
            var bounds = go.GetCompositeRendererBounds();
            var position = bounds.center;

            // Get the radius of a sphere circumscribing the bounds
            var radius = bounds.size.magnitude / 2;

            return camera.FocusSphere(position, radius);
        }
        
        public static Vector3 FocusSphere(this UnityEngine.Camera camera, Vector3 position, float radius)
        {
            // Get the horizontal FOV, since it may be the limiting of the two FOVs to properly encapsulate the objects
            var horizontalFov = 2f * Mathf.Atan(Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2f) * camera.aspect) * Mathf.Rad2Deg;
            // Use the smaller FOV as it limits what would get cut off by the frustum        
            var fov = Mathf.Min(camera.fieldOfView, horizontalFov);

            // var distance = radius / Mathf.Tan((camera.fieldOfView * Mathf.Deg2Rad) / 2f);
            // Take sin so the whole sphere is in the view
            var distance = radius / Mathf.Sin((fov * Mathf.Deg2Rad) / 2f);

            if (camera.orthographic)
                camera.orthographicSize = radius / Mathf.Min(camera.aspect, 1);

            return position - camera.transform.forward * distance;
        }
    }
}