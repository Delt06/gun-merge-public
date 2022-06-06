using System;
using UnityEngine;

namespace Combat.Weapons
{
	public sealed class WeaponObjectListController : MonoBehaviour
	{
		private void OnEnable()
		{
			_weapon.LoadedConfig += _onLoaded;
			_weapon.Used += _onUsed;
		}

		private void OnDisable()
		{
			_weapon.LoadedConfig -= _onLoaded;
			_weapon.Used -= _onUsed;
		}

		private void Awake()
		{
			_onLoaded = (sender, config) =>
			{
				_weaponObject = _weaponObjectList.EnableAndFind(config);
				_weapon.ShootFrom = _weaponObject.ShootFromPoint;
			};

			_onUsed = (sender, args) => _weaponObject.OnWasUsed();
		}

		public void Construct(IWeapon weapon, WeaponObjectList weaponObjectList)
		{
			_weapon = weapon;
			_weaponObjectList = weaponObjectList;
		}

		private EventHandler _onUsed;
		private EventHandler<WeaponConfig> _onLoaded;
		private WeaponObjectList _weaponObjectList;
		private IWeapon _weapon;
		private WeaponObject _weaponObject;
	}
}