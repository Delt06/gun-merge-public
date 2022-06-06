using System;
using Combat.Teams;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat.Weapons
{
	public sealed class Bullet : MonoBehaviour
	{
		[SerializeField] private LayerMask _hittableMask = default;
		[SerializeField] private AnimationCurve _damageOverLifetime = AnimationCurve.Constant(0f, 1f, 1f);
		[SerializeField] private AnimationCurve _speedOverLifetime = AnimationCurve.Constant(0f, 1f, 1f);
		[SerializeField, Min(0f)] private float _radius = 0.1f;

		[Min(0f)] public float Speed = 100f;

		[Min(0f)] public float Damage = 10f;

		[Min(0f)] public float Lifetime = 1f;
		[Min(0f)] public float DisableTime = 10f;
		public bool IsCritical { get; set; }

		public void OnShot(ITeam team, IDamageDealer damageDealer)
		{
			_team = team;
			_damageDealer = damageDealer;
		}

		private void OnEnable()
		{
			_team = null;
			_damageDealer = null;
			_enabledTime = 0f;
			IsCritical = false;
			_canHit = true;
		}

		private void FixedUpdate()
		{
			_enabledTime += Time.fixedDeltaTime;

			if (_canHit && _enabledTime <= Lifetime)
			{
				var deltaPosition = Speed * _speedOverLifetime.Evaluate(LifetimeRatio) * Time.fixedDeltaTime;
				var motion = deltaPosition * _rigidbody.transform.forward;
				TryHit(deltaPosition);
				_rigidbody.MovePosition(_rigidbody.position + motion);
			}

			if (_enabledTime > DisableTime)
				gameObject.SetActive(false);
		}

		private float LifetimeRatio => Mathf.Clamp01(_enabledTime / Lifetime);

		private void TryHit(float maxDistance)
		{
			var origin = _rigidbody.position;
			var direction = _rigidbody.transform.forward;
			if (!Physics.SphereCast(origin, _radius, direction, out var hit, maxDistance, _hittableMask,
				QueryTriggerInteraction.Ignore)) return;

			TryDealDamageThrough(hit.collider, hit.point);
			_canHit = false;
		}

		private void TryDealDamageThrough(Collider col, Vector3 position)
		{
			var attachedRigidbody = col.attachedRigidbody;
			var args = new BulletHitArgs
			{
				Position = position,
				IsCritical = IsCritical
			};
			if (attachedRigidbody && attachedRigidbody.TryGetComponent(out IDamageTaker damageTaker) ||
			    col.TryGetComponent(out damageTaker))
				DealDamageTo(damageTaker);
			else
				args.IsCritical = false;

			Hit?.Invoke(this, args);
		}

		private void DealDamageTo(IDamageTaker damageTaker)
		{
			if (_team != null && !_team.CanAttack(damageTaker.Team)) return;
			var damage = Damage * _damageOverLifetime.Evaluate(LifetimeRatio);
			damageTaker.Take(damage, _damageDealer);
		}

		public event EventHandler<BulletHitArgs> Hit;

		public void Construct(Rigidbody rb) => _rigidbody = rb;

		private bool _canHit = true;
		[CanBeNull] private ITeam _team;
		private Rigidbody _rigidbody;
		private float _enabledTime;
		private IDamageDealer _damageDealer;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _radius);
		}
	}
}