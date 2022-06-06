using Combat.Weapons;

namespace Collection.Weapons
{
	public interface IWeaponTakeHandler
	{
		void Take(WeaponConfig weaponConfig);
	}
}