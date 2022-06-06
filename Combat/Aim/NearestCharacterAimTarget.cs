using Combat.Characters;
using Components;
using Plugins.FSA;
using Plugins.FSA.Components.Easy;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityUtilities;
using Random = UnityEngine.Random;

namespace Combat.Aim
{
	public sealed class NearestCharacterAimTarget : EasyStateBehaviour, IAimTarget
	{
		[SerializeField, Min(0f)] private float _maxDistance = 10f;
		[SerializeField, Range(-1f, 1f)] private float _minDot = 0.25f;

		[SerializeField, MinMaxSlider(0f, 1f, true)]
		private Vector2 _updatePeriod = new Vector2(0f, 1f);

		public IComponentProvider Target { get; private set; }

		protected override void OnUpdate(float deltaTime, IFsa<EasyTrigger> fsa)
		{
			_timeTillNextUpdate -= deltaTime;
			if (_timeTillNextUpdate > 0f && Target != null && Target.gameObject.activeSelf) return;

			Target = FindTargetOrDefault();
			_timeTillNextUpdate = Random.Range(_updatePeriod.x, _updatePeriod.y);
		}

		private IComponentProvider FindTargetOrDefault()
		{
			IComponentProvider target = null;
			Transform targetTransform = null;

			var currentPosition = _character.transform.position;

			foreach (var otherProvider in _register)
			{
				var otherCharacter = otherProvider.Get<Character>();
				if (!otherCharacter.IsAlive) continue;
				if (!_character.Team.CanAttack(otherCharacter.Team)) continue;

				var offsetTowardsOtherCharacter = otherCharacter.transform.position - currentPosition;
				var sqrDistanceToOtherCharacter = offsetTowardsOtherCharacter.sqrMagnitude;
				if (sqrDistanceToOtherCharacter > _maxDistance * _maxDistance) continue;

				var dot = Vector3.Dot(offsetTowardsOtherCharacter.normalized, _character.transform.forward);
				if (dot < _minDot) continue;

				if (ReferenceEquals(target, null))
				{
					target = otherProvider;
					targetTransform = otherProvider.Get<Transform>();
				}
				else
				{
					var sqrDistanceToCurrentTarget = (targetTransform.position - currentPosition).sqrMagnitude;
					if (sqrDistanceToOtherCharacter >= sqrDistanceToCurrentTarget) continue;

					target = otherProvider;
					targetTransform = otherProvider.Get<Transform>();
				}
			}

			return target;
		}

		private void OnEnable()
		{
			Target = null;
		}

		public void Construct(IReadOnlyCharacterRegister register, Character character)
		{
			_register = register;
			_character = character;
		}

		private IReadOnlyCharacterRegister _register;
		private Character _character;
		private float _timeTillNextUpdate;

		private void OnDrawGizmos()
		{
			var maxAngle = Mathf.Rad2Deg * Mathf.Acos(_minDot);
			var position = transform.position;
			var forward = transform.forward * _maxDistance;
			Gizmos.color = Color.green.With(a: 0.5f);
			Gizmos.DrawRay(position, Quaternion.AngleAxis(maxAngle, Vector3.up) * forward);
			Gizmos.DrawRay(position, Quaternion.AngleAxis(-maxAngle, Vector3.up) * forward);
		}
	}
}