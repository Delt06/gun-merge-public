using System;
using Combat.Weapons;
using Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Collection.Weapons
{
	public sealed class CollectibleWeapon : CollectibleBase
	{
		[SerializeField, Required, AssetSelector]
		private WeaponConfig _config = default;

		public WeaponConfig Config
		{
			get => _config;
			set
			{
				_config = value;
				ChangedConfig?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler ChangedConfig;

		protected override void OnGetCollected(IComponentProvider componentProvider)
		{
			var weaponTakeHandler = componentProvider.Get<IWeaponTakeHandler>();
			weaponTakeHandler.Take(Config);
		}
	}
}