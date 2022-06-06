using UnityEngine;
using Random = UnityEngine.Random;

namespace Combat.Weapons
{
	public sealed class CompositeBulletModifier : MonoBehaviour, IBulletModifier
	{
		public void Affect(Bullet bullet)
		{
			bullet.Speed = AffectSpeed(bullet.Speed);
			bullet.Damage = AffectDamage(bullet.Damage);

			if (Random.value <= _criticalDamage.Probability)
			{
				bullet.Damage *= _criticalDamage.Coefficient;
				bullet.IsCritical = true;
			}
		}

		private float AffectSpeed(float speed)
		{
			foreach (var bulletSpeedModifier in _bulletSpeedModifiers)
			{
				speed = bulletSpeedModifier.Affect(speed);
			}

			return speed;
		}

		private float AffectDamage(float damage)
		{
			foreach (var damageModifier in _damageModifiers)
			{
				damage = damageModifier.Affect(damage);
			}

			return damage;
		}

		private void Awake()
		{
			_damageModifiers = GetComponentsInChildren<IDamageModifier>();
			_bulletSpeedModifiers = GetComponentsInChildren<IBulletSpeedModifier>();
			_criticalDamage = GetComponentInChildren<ICriticalDamage>();
		}

		private IDamageModifier[] _damageModifiers;
		private IBulletSpeedModifier[] _bulletSpeedModifiers;
		private ICriticalDamage _criticalDamage;
	}
}