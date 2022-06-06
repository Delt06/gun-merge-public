using Combat.Weapons;
using UnityEngine;

namespace Collection.Weapons
{
	public sealed class WeaponMerge : MonoBehaviour, IWeaponTakeHandler
	{
		public void Take(WeaponConfig weaponConfig)
		{
			if (TryGetWeaponIndices(weaponConfig, out var mergedWeaponIndex, out var currentWeaponIndex))
			{
				if (mergedWeaponIndex > currentWeaponIndex)
					Equip(weaponConfig);
				if (mergedWeaponIndex == currentWeaponIndex &&
				    TryGetNextWeapon(currentWeaponIndex, out var nextWeaponConfig))
					Equip(nextWeaponConfig);
			}
			else
			{
				Equip(weaponConfig);
			}
		}

		private bool TryGetWeaponIndices(WeaponConfig mergedConfig, out int mergedWeaponIndex,
			out int currentWeaponIndex)
		{
			currentWeaponIndex = -1;
			return _mergeOrder.InOrder(mergedConfig, out mergedWeaponIndex) &&
			       _mergeOrder.InOrder(_weapon.Config, out currentWeaponIndex);
		}

		private bool TryGetNextWeapon(int currentWeaponIndex, out WeaponConfig nextWeaponConfig)
		{
			if (currentWeaponIndex < _mergeOrder.Count - 1)
			{
				nextWeaponConfig = _mergeOrder[currentWeaponIndex + 1];
				return true;
			}

			nextWeaponConfig = default;
			return false;
		}

		private void Equip(WeaponConfig config) => _weapon.Config = config;

		public void Construct(IWeapon weapon, IWeaponMergeOrder mergeOrder)
		{
			_weapon = weapon;
			_mergeOrder = mergeOrder;
		}

		private IWeapon _weapon;
		private IWeaponMergeOrder _mergeOrder;
	}
}