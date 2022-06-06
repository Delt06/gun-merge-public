using Movement;
using UnityEngine;
using UnityEngine.AI;
using UnityUtilities;

namespace AI.Movement
{
	public sealed class AgentMovementControls : MonoBehaviour
	{
		[SerializeField, Range(0f, 1f)] private float _agentStabilization = 1f;

		private void FixedUpdate()
		{
			_agent.speed = _movement.Speed;
			_agent.angularSpeed = _movement.AngularSpeed;

			var velocity = _agent.velocity;
			var direction = velocity.With(y: 0f);
			if (direction.sqrMagnitude > 1)
				direction.Normalize();
			_movement.Direction = direction;

			SnapIfTooFar();
		}

		private void SnapIfTooFar()
		{
			var currentPosition = _movement.CurrentPosition;
			var delta = _agent.nextPosition - currentPosition;

			if (delta.magnitude > _agent.radius)
				_agent.nextPosition -= delta * _agentStabilization;
		}

		public void Construct(NavMeshAgent agent, IMovement movement)
		{
			_agent = agent;
			_movement = movement;
		}

		private NavMeshAgent _agent;
		private IMovement _movement;
	}
}