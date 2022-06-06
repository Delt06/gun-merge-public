using System;
using System.Collections.Generic;
using Components;
using Math;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityUtilities;
using Random = System.Random;

namespace Spawning
{
	public abstract class SpawnZone : MonoBehaviour
	{
		[SerializeField, Required] private SpawnConfig _config = default;
		[SerializeField, Min(0f)] private float _spawnCooldown = 15f;

		[SerializeField, MinMaxSlider(0f, 1f, true)]
		private Vector2 _spawnPointsFill = new Vector2(0.5f, 1f);

		private void Update()
		{
			SpawnIfCan();
		}

		private void SpawnIfCan()
		{
			if (_spawnedObjects.Count != 0) return;

			var distance = GetDistanceToTarget();
			var targetIsInside = distance <= _config.Distance;

			if (!targetIsInside)
			{
				if (Time.time >= _lastSpawnTime + _spawnCooldown)
					_canSpawn = true;
			}
			else if (_canSpawn)
			{
				SpawnAtPoints();
			}
		}

		private float GetDistanceToTarget() => VectorUtils.GetDistanceXZ(Origin, _spawnAnchor.Position);

		private Vector3 Origin => transform.position;

		private void SpawnAtPoints()
		{
			_canSpawn = false;
			_lastSpawnTime = Time.time;
			if (_spawnPoints.Length == 0) return;

			var usedPointsRatio = UnityEngine.Random.Range(_spawnPointsFill.x, _spawnPointsFill.y);
			var usedPointsCount = Mathf.RoundToInt(usedPointsRatio * _spawnPoints.Length);
			usedPointsCount = Mathf.Clamp(usedPointsCount, 1, _spawnPoints.Length);

			Array.Sort(_spawnPoints, _randomComparison);

			for (var i = 0; i < usedPointsCount; i++)
			{
				var spawnPoint = _spawnPoints[i];
				var spawnedObject = GetNewObject(spawnPoint.Position);
				spawnedObject.Despawned += _onDespawned;
				_spawnedObjects.Add(spawnedObject);
			}
		}

		protected abstract IDespawnable GetNewObject(Vector3 position);

		private void Awake()
		{
			_spawnPoints = FindSpawnPoints();
			_onDespawned = (sender, args) =>
			{
				var despawnable = (IDespawnable) sender;
				_spawnedObjects.Remove(despawnable);
				despawnable.Despawned -= _onDespawned;
			};

			_randomComparison = (p1, p2) => _random.Next() % 2 == 0 ? -1 : 1;
		}

		private SpawnPoint[] FindSpawnPoints() => GetComponentsInChildren<SpawnPoint>();

		public void Construct(SpawnAnchor spawnAnchor) =>
			_spawnAnchor = spawnAnchor;

		private float _lastSpawnTime = float.NegativeInfinity;
		private SpawnPoint[] _spawnPoints;
		private SpawnAnchor _spawnAnchor;
		private bool _canSpawn = true;
		private readonly HashSet<IDespawnable> _spawnedObjects = new HashSet<IDespawnable>();
		private EventHandler _onDespawned;
		private Comparison<SpawnPoint> _randomComparison;
		private readonly Random _random = new Random();

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green.With(a: 0.5f);
			Gizmos.DrawWireSphere(Origin, _config ? _config.Distance : 0f);
		}
	}
}