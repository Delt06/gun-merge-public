using UnityEngine;

namespace Spawning
{
	public class SpawnPoint : MonoBehaviour
	{
		public Vector3 Position => transform.position;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(Position, 1f);
		}
	}
}