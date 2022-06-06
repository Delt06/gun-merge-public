using Combat.Characters;
using Combat.Weapons;
using Plugins.FSA;
using Plugins.FSA.Components.Easy;
using UnityEngine;

namespace Combat.Aim
{
	public sealed class AimTargetWeaponAutoUsage : EasyStateBehaviour
	{
		[SerializeField, Range(-1f, 1f)] private float _minDot = 0.75f;
		[SerializeField, Min(0f)] private float _maxDistance = 10f;
		[SerializeField] private LayerMask _blockerMask = default;
		[SerializeField, Min(1)] private int _blockerUpdateFrequencyInFrames = 5;

		protected override void OnUpdate(float deltaTime, IFsa<EasyTrigger> fsa)
		{
			if (_blocked == null || Time.frameCount % _blockerUpdateFrequencyInFrames == 0) UpdaterBlockerInfo();
			if (_blocked == true) return;

			if (ReferenceEquals(_aimTarget.Target, null)) return;

			var offsetTowardsTarget = _aimTarget.Target.Get<Transform>().position - _character.transform.position;
			if (offsetTowardsTarget.sqrMagnitude > _maxDistance * _maxDistance) return;

			var dot = Vector3.Dot(offsetTowardsTarget.normalized, _weapon.ShootFrom.Rotation * Vector3.forward);
			if (dot < _minDot) return;

			_weapon.TryUse();
		}

		private void UpdaterBlockerInfo()
		{
			var characterTransform = _character.transform;
			var ray = new Ray(characterTransform.position, characterTransform.forward);
			_blocked = Physics.Raycast(ray, _maxDistance, _blockerMask, QueryTriggerInteraction.Ignore);
		}

		public void Construct(IAimTarget aimTarget, Character character, IWeapon weapon)
		{
			_aimTarget = aimTarget;
			_character = character;
			_weapon = weapon;
		}

		private void OnEnable()
		{
			_blocked = null;
		}

		private bool? _blocked;
		private IAimTarget _aimTarget;
		private Character _character;
		private IWeapon _weapon;
	}
}