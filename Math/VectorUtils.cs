using UnityEngine;

namespace Math
{
	public static class VectorUtils
	{
		public static float GetDistanceXZ(Vector3 v1, Vector3 v2)
		{
			var offset = v1 - v2;
			offset.y = 0f;
			return offset.magnitude;
		}
	}
}