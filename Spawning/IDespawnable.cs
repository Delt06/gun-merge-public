using System;
using UnityEngine;

namespace Spawning
{
	public interface IDespawnable
	{
		bool IsSpawned { get; }

		void Spawn(Vector3 position, Quaternion rotation);
		void Despawn();
		event EventHandler Despawned;
	}
}