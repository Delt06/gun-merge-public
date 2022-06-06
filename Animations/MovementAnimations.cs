using Movement;
using UnityEngine;

namespace Animations
{
	public sealed class MovementAnimations : MonoBehaviour
	{
		private void Update()
		{
			_animator.SetBool(MovingId, _movement.IsMoving);
			_animator.SetFloat(RunningSpeedId, Speed);
		}

		private float Speed => _movement.Direction.magnitude * _movement.Speed;

		public void Construct(Animator animator, IMovement movement)
		{
			_animator = animator;
			_movement = movement;
		}

		private Animator _animator;
		private IMovement _movement;
		private static readonly int MovingId = Animator.StringToHash("Moving");
		private static readonly int RunningSpeedId = Animator.StringToHash("RunningSpeed");
	}
}