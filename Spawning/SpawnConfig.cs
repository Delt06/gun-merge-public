using UnityEngine;

namespace Spawning
{
	[CreateAssetMenu]
	public class SpawnConfig : ScriptableObject
	{
		[SerializeField, Min(0f)] private float _distance = 25f;

		public float Distance => _distance;
	}
}