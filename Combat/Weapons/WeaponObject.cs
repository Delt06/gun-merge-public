using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Combat.Weapons
{
	public class WeaponObject : MonoBehaviour
	{
		[SerializeField, Required, AssetSelector]
		private WeaponConfig _config = default;

		public WeaponConfig Config => _config;
		public IShootFrom ShootFromPoint { get; private set; }

		public void OnWasUsed() => WasUsed?.Invoke(this, EventArgs.Empty);

		public event EventHandler WasUsed;

		private void Awake()
		{
			ShootFromPoint = GetComponentInChildren<IShootFrom>();
		}
	}
}