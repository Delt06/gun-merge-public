using System;
using Combat.Weapons;
using Effects.Spawning;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Effects
{
	public sealed class Bullet_OnHit_SpawnEffect : MonoBehaviour
	{
		[SerializeField, Required, AssetSelector]
		private EffectType _effect = default;

		[SerializeField, Required, AssetSelector]
		private EffectType _criticalEffect = default;

		private void OnEnable()
		{
			_bullet.Hit += _onHit;
		}

		private void OnDisable()
		{
			_bullet.Hit -= _onHit;
		}

		private void Awake()
		{
			_onHit = (sender, args) =>
			{
				var effectType = args.IsCritical ? _criticalEffect : _effect;
				_resolver.Get(effectType).Spawn(args.Position, Quaternion.identity);
			};
		}

		public void Construct(Bullet bullet, ITypedEffectSpawnerResolver resolver)
		{
			_bullet = bullet;
			_resolver = resolver;
		}

		private EventHandler<BulletHitArgs> _onHit;
		private Bullet _bullet;
		private ITypedEffectSpawnerResolver _resolver;
	}
}