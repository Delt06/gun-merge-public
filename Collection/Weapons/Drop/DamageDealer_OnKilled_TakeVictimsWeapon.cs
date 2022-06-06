using Combat.Characters.Events;
using Combat.Events;

namespace Collection.Weapons.Drop
{
	public sealed class DamageDealer_OnKilled_TakeVictimsWeapon : DamageDealer_OnKilled_Base
	{
		protected override void OnKilled(CharacterDeathArgs args)
		{
			_weaponTakeHandler.Take(args.WeaponConfig);
		}

		public void Construct(IWeaponTakeHandler weaponTakeHandler) => _weaponTakeHandler = weaponTakeHandler;

		private IWeaponTakeHandler _weaponTakeHandler;
	}
}