using UnityEngine;
using UnityEngine.AI;

namespace AI.Movement
{
	public sealed class ManualMovementAgent : MonoBehaviour
	{
		private void Awake()
		{
			_agent.updatePosition = _agent.updateRotation = _agent.updateUpAxis = false;
		}

		public void Construct(NavMeshAgent agent) => _agent = agent;

		private NavMeshAgent _agent;
	}
}