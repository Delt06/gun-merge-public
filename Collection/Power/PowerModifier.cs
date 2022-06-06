using System.Collections.Generic;
using Combat.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace Collection.Power
{
	public sealed class PowerModifier : MonoBehaviour, IDamageModifier, IBulletSpeedModifier
	{
		[SerializeField] private UnityEvent _onActivated = default;

		public void Activate(float damageCoefficient, float bulletSpeedCoefficient, float duration)
		{
			var powerUp = new PowerUp(duration, damageCoefficient, bulletSpeedCoefficient);
			_activePowerUps.Add(powerUp);
			_onActivated.Invoke();
		}

		float IDamageModifier.Affect(float damage)
		{
			foreach (var powerUp in _activePowerUps)
			{
				damage *= powerUp.DamageCoefficient;
			}

			return damage;
		}

		float IBulletSpeedModifier.Affect(float bulletSpeed)
		{
			foreach (var powerUp in _activePowerUps)
			{
				bulletSpeed *= powerUp.BulletSpeedCoefficient;
			}

			return bulletSpeed;
		}

		private void Update()
		{
			for (var index = _activePowerUps.Count - 1; index >= 0; index--)
			{
				var powerUp = _activePowerUps[index];
				powerUp.RemainingTime -= Time.deltaTime;

				if (powerUp.RemainingTime > 0f)
					_activePowerUps[index] = powerUp;
				else
					_activePowerUps.RemoveAt(index);
			}
		}

		private void OnEnable()
		{
			_activePowerUps.Clear();
		}

		private readonly List<PowerUp> _activePowerUps = new List<PowerUp>();

		private struct PowerUp
		{
			public float RemainingTime;
			public readonly float DamageCoefficient;
			public readonly float BulletSpeedCoefficient;

			public PowerUp(float remainingTime, float damageCoefficient, float bulletSpeedCoefficient)
			{
				RemainingTime = remainingTime;
				DamageCoefficient = damageCoefficient;
				BulletSpeedCoefficient = bulletSpeedCoefficient;
			}
		}
	}
}