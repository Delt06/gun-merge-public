using System;

namespace Combat.Weapons
{
	public interface IWeapon
	{
		IShootFrom ShootFrom { get; set; }
		WeaponConfig Config { get; set; }
		bool TryUse();
		event EventHandler Used;
		event EventHandler<WeaponConfig> LoadedConfig;
	}
}