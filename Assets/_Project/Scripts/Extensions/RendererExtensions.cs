using UnityEngine;

namespace _Framework.Scripts.Extensions
{
	public static class RendererExtensions
	{
		/// <summary>
		/// Credits: http://wiki.unity3d.com/index.php?title=IsVisibleFrom
		/// </summary>
		public static bool IsVisibleFrom(this Renderer renderer, UnityEngine.Camera camera)
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
			return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
		}
	}
}