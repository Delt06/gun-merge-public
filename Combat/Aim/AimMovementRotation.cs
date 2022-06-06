using Combat.Weapons;
using Movement;
using Plugins.FSA;
using Plugins.FSA.Components.Easy;
using UnityEngine;

namespace Combat.Aim
{
	public class AimMovementRotation : EasyStateBehaviour
	{
		[SerializeField, Range(0f, 1f)] private float _lookahead = 0f;

		protected override void OnUpdate(float deltaTime, IFsa<EasyTrigger> fsa)
		{
			_movement.RotationOverride = GetRotationOverrideOrDefault(deltaTime);
		}

		private Quaternion? GetRotationOverrideOrDefault(float deltaTime)
		{
			if (ReferenceEquals(_aimTarget.Target, null)) return null;

			var targetPosition = _aimTarget.Target.Get<Transform>().position;
			targetPosition += GetLookaheadOffset(deltaTime);
			var offsetTowardsTarget = targetPosition - _movement.CurrentPosition;
			if (offsetTowardsTarget.sqrMagnitude < 0.01f) return null;

			return ShootFromRotationOffset * Quaternion.LookRotation(offsetTowardsTarget, Vector3.up);
		}

		private Vector3 GetLookaheadOffset(float deltaTime) =>
			deltaTime * _lookahead * _aimTarget.Target.Get<Rigidbody>().velocity;

		private Quaternion ShootFromRotationOffset
		{
			get
			{
				var shootFromForward = _weapon.ShootFrom.Rotation * Vector3.forward;
				var shootFromForwardLocal = _movement.Transform.InverseTransformVector(shootFromForward);
				shootFromForwardLocal.y = 0f;
				shootFromForwardLocal.Normalize();
				var shootFromRotationLocal = Quaternion.LookRotation(shootFromForwardLocal, Vector3.up);
				return Quaternion.Inverse(shootFromRotationLocal);
			}
		}

		public void Construct(IAimTarget aimTarget, IMovement movement, IWeapon weapon)
		{
			_aimTarget = aimTarget;
			_movement = movement;
			_weapon = weapon;
		}

		private IAimTarget _aimTarget;
		private IMovement _movement;
		private IWeapon _weapon;
	}
}