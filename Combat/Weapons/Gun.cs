using System;
using Combat.Teams;
using Sirenix.OdinInspector;
using Spawning;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Combat.Weapons
{
	public class Gun : MonoBehaviour, IWeapon
	{
		[SerializeField] private UnityEvent _onShot = default;

		[SerializeField, ReadOnly] private WeaponConfig _weaponConfig = default;

		private void Update()
		{
			_timeTillNextShot -= Time.deltaTime;
		}

		[AssetSelector, Required, ShowInInspector]
		public WeaponConfig Config
		{
			get => _weaponConfig;
			set
			{
				if (!value) throw new ArgumentNullException(nameof(value));
				LoadConfig(value);
			}
		}

		private void LoadConfig(WeaponConfig weaponConfig)
		{
			_weaponConfig = weaponConfig;
			LoadedConfig?.Invoke(this, weaponConfig);
		}

		public IShootFrom ShootFrom { get; set; } = new NullShootFromPoint();

		public bool TryUse()
		{
			if (!isActiveAndEnabled) return false;
			if (_timeTillNextShot > 0f) return false;

			for (var i = 0; i < Config.BulletsPerShot; i++)
			{
				var rotationOffset = Quaternion.Euler(RandomRecoilAngle, RandomRecoilAngle, 0f);
				var bullet = _bulletSpawner.Spawn(ShootFrom.Position, ShootFrom.Rotation * rotationOffset);
				bullet.Damage = Config.BulletDamage;
				_bulletModifier.Affect(bullet);
				bullet.OnShot(_teamMember.Team, _damageDealer);
			}

			_timeTillNextShot = 1f / Config.ShotsPerSecond;
			_onShot.Invoke();
			Used?.Invoke(this, EventArgs.Empty);
			return true;
		}

		private float RandomRecoilAngle => Random.Range(-Config.MaxRecoilAngle, Config.MaxRecoilAngle);

		public event EventHandler Used;

		private void Start()
		{
			LoadConfig(_weaponConfig);
		}

		private void OnEnable()
		{
			_timeTillNextShot = float.NegativeInfinity;
		}

		public event EventHandler<WeaponConfig> LoadedConfig;

		public void Construct(ITeamMember teamMember, IDamageDealer damageDealer, IBulletModifier bulletModifier,
			ISpawner<Bullet> bulletSpawner)
		{
			_teamMember = teamMember;
			_damageDealer = damageDealer;
			_bulletModifier = bulletModifier;
			_bulletSpawner = bulletSpawner;
		}

		private float _timeTillNextShot;
		private ITeamMember _teamMember;
		private IDamageDealer _damageDealer;
		private IBulletModifier _bulletModifier;
		private ISpawner<Bullet> _bulletSpawner;
	}
}