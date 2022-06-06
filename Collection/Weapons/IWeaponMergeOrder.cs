using System.Collections.Generic;
using Combat.Weapons;

namespace Collection.Weapons
{
	public interface IWeaponMergeOrder : IComparer<WeaponConfig>, IReadOnlyList<WeaponConfig>
	{
		bool InOrder(WeaponConfig weapon, out int index);
	}
}