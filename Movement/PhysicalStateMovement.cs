using Plugins.FSA;
using Plugins.FSA.Components.Easy;
using UnityEngine;

namespace Movement
{
	public class PhysicalStateMovement : EasyStateBehaviour, IMovement
	{
		[SerializeField, Min(0f)] private float _speed = 1f;
		[SerializeField, Min(0f)] private float _angularSpeed = 180f;

		public Vector3 CurrentPosition => _rigidbody.position;

		public float Speed => _speed;

		public float AngularSpeed => _angularSpeed;

		public Vector3 Direction { get; set; }

		public Quaternion? RotationOverride { get; set; }

		public bool IsMoving => Direction.magnitude >= 0.01f;

		public Transform Transform => transform;

		protected override void OnFixedUpdate(float deltaTime, IFsa<EasyTrigger> fsa)
		{
			var velocity = _speed * Direction;
			SetVelocity(velocity);

			var targetRotation = GetTargetRotation();
			var deltaRotation = _angularSpeed * deltaTime;
			var newRotation = Quaternion.RotateTowards(_rigidbody.rotation, targetRotation, deltaRotation);
			_rigidbody.rotation = newRotation;
		}

		private void SetVelocity(Vector3 velocity)
		{
			velocity.y = _rigidbody.velocity.y;
			_rigidbody.velocity = velocity;
		}

		private Quaternion GetTargetRotation() => RotationOverride ??
		                                          (IsMoving
			                                          ? Quaternion.LookRotation(Direction, Vector3.up)
			                                          : _rigidbody.rotation);

		private void OnEnable()
		{
			Direction = Vector3.zero;
			RotationOverride = null;
		}

		public void Construct(Rigidbody rb) => _rigidbody = rb;

		private Rigidbody _rigidbody;
	}
}