using System;
using Combat.Weapons;
using UnityEngine;

namespace Effects
{
	public sealed class WeaponObject_OnUsed_EnableBulletFlare : MonoBehaviour
	{
		private void OnEnable()
		{
			_weapon.WasUsed += _onUsed;
		}

		private void OnDisable()
		{
			_weapon.WasUsed -= _onUsed;
		}

		private void Awake()
		{
			_weapon = GetComponentInParent<WeaponObject>();
			_bulletFlare = GetComponentInChildren<BulletFlare>();
			_onUsed = (sender, args) => _bulletFlare.Enable();
		}

		private BulletFlare _bulletFlare;
		private WeaponObject _weapon;
		private EventHandler _onUsed;
	}
}