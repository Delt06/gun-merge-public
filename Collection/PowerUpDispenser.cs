using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collection
{
	public sealed class PowerUpDispenser : MonoBehaviour
	{
		[SerializeField, TableList] private ProbabilityGroup[] _powerUps = default;

		[SerializeField, MinMaxSlider(0f, 100f, true)]
		private Vector2 _restoreCooldown = new Vector2(10, 25);

		private void Update()
		{
			_timeTillNextRestore -= Time.deltaTime;
			if (_timeTillNextRestore > 0f) return;

			SpawnRandom();
			_timeTillNextRestore = Random.Range(_restoreCooldown.x, _restoreCooldown.y);
		}

		private void SpawnRandom()
		{
			var randomGroupIndex = GetRandomGroupIndex();

			for (var groupIndex = 0; groupIndex < _powerUps.Length; groupIndex++)
			{
				var probabilityGroup = _powerUps[groupIndex];
				var objects = probabilityGroup.Objects;
				var spawnedObjectIndex = Random.Range(0, objects.Length);

				for (var objectIndex = 0; objectIndex < objects.Length; objectIndex++)
				{
					var isSpawned = groupIndex == randomGroupIndex && objectIndex == spawnedObjectIndex;
					objects[objectIndex].SetActive(isSpawned);
				}
			}
		}

		private int GetRandomGroupIndex()
		{
			var randomValue = Random.value;
			var cumulativeProbability = 0f;

			for (var index = 0; index < _powerUps.Length; index++)
			{
				cumulativeProbability += _powerUps[index].Probability;
				if (randomValue <= cumulativeProbability)
					return index;
			}

			return 0;
		}

		private float _timeTillNextRestore;

		[Serializable]
		private class ProbabilityGroup
		{
			[Range(0f, 1f)] public float Probability = 0.5f;
			[Required, ChildGameObjectsOnly] public GameObject[] Objects = default;
		}
	}
}