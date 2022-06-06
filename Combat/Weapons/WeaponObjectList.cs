using UnityEngine;

namespace Combat.Weapons
{
	public sealed class WeaponObjectList : MonoBehaviour
	{
		public WeaponObject EnableAndFind(WeaponConfig weaponConfig)
		{
			WeaponObject matchingWeaponObject = null;

			foreach (var weaponObject in _weaponObjects)
			{
				var configMatches = ReferenceEquals(weaponConfig, weaponObject.Config);
				weaponObject.gameObject.SetActive(configMatches);
				if (configMatches && matchingWeaponObject == null)
					matchingWeaponObject = weaponObject;
			}

			return matchingWeaponObject;
		}

		private void Awake()
		{
			_weaponObjects = GetComponentsInChildren<WeaponObject>(true);
		}

		private WeaponObject[] _weaponObjects;
	}
}