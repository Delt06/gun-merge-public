using System;
using Combat.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Collection.Weapons.Visuals
{
	public sealed class CollectibleWeaponModel : MonoBehaviour
	{
		[SerializeField, TableList] private WeaponModel[] _weaponModels = default;

		private void Start()
		{
			Refresh();
		}

		private void OnEnable()
		{
			Refresh();
			_collectibleWeapon.ChangedConfig += _onChangedItem;
		}

		private void OnDisable()
		{
			_collectibleWeapon.ChangedConfig -= _onChangedItem;
		}

		private void Awake()
		{
			_onChangedItem = (sender, args) => Refresh();
		}

		private void Refresh()
		{
			foreach (var weaponModel in _weaponModels)
			{
				var weaponMatches = ReferenceEquals(weaponModel.Weapon, _collectibleWeapon.Config);
				weaponModel.Model.SetActive(weaponMatches);
			}
		}

		public void Construct(CollectibleWeapon collectibleWeapon) => _collectibleWeapon = collectibleWeapon;

		private EventHandler _onChangedItem;
		private CollectibleWeapon _collectibleWeapon;

		[Serializable]
		private class WeaponModel
		{
			[Required, AssetSelector] public WeaponConfig Weapon = default;

			[Required, ChildGameObjectsOnly(IncludeSelf = false)]
			public GameObject Model = default;
		}
	}
}