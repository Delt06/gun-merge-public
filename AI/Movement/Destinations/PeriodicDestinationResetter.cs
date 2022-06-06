using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI.Movement.Destinations
{
	public sealed class PeriodicDestinationResetter : MonoBehaviour
	{
		[SerializeField, MinMaxSlider(0f, 10f, true)]
		private Vector2 _period = new Vector2(0f, 1f);

		private void Update()
		{
			_timeTillNextReset -= Time.deltaTime;
			if (_timeTillNextReset > 0f) return;

			if (TryGetDestination(out var destination))
				_agent.SetDestination(destination);
			else
				_agent.ResetPath();

			_timeTillNextReset = GetRandomPeriod();
		}

		private bool TryGetDestination(out Vector3 destination)
		{
			foreach (var provider in _providers)
			{
				if (provider.TryFind(out destination))
					return true;
			}

			destination = default;
			return false;
		}

		private float GetRandomPeriod() => Random.Range(_period.x, _period.y);

		private void OnEnable()
		{
			_timeTillNextReset = 0f;
			_agent.ResetPath();
		}

		private void Awake()
		{
			_providers = GetComponentsInChildren<IDestinationProvider>();
		}

		public void Construct(NavMeshAgent agent)
		{
			_agent = agent;
		}

		private IDestinationProvider[] _providers;
		private NavMeshAgent _agent;
		private float _timeTillNextReset;

		private void OnDrawGizmos()
		{
			if (_agent == null) return;
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(_agent.destination, 0.1f);
		}
	}
}