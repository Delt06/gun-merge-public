using Combat.Weapons;
using UnityEngine;

namespace Combat.Characters.Events.Builders
{
	public sealed class DeathArgsBuilder_Weapon : MonoBehaviour, IDeathArgsBuilder
	{
		public void Build(ref CharacterDeathArgs args) => args.WeaponConfig = _weapon.Config;

		public void Construct(IWeapon weapon) => _weapon = weapon;

		private IWeapon _weapon;
	}
}