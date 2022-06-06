using Spawning.Specific;
using UnityEngine;

namespace Spawning
{
	public class EnemySpawnZone : SpawnZone
	{
		protected override IDespawnable GetNewObject(Vector3 position) =>
			_enemySpawner.Spawn(position, Quaternion.identity).Get<IDespawnable>();

		public void Construct(IEnemySpawner enemySpawner) => _enemySpawner = enemySpawner;

		private IEnemySpawner _enemySpawner;
	}
}