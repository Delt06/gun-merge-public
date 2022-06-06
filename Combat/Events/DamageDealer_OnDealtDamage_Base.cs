using System;
using UnityEngine;

namespace Combat.Events
{
	public abstract class DamageDealer_OnDealtDamage_Base : MonoBehaviour
	{
		protected virtual void OnEnable()
		{
			_damageDealer.DealtDamage += _onDealtDamage;
		}

		protected virtual void OnDisable()
		{
			_damageDealer.DealtDamage -= _onDealtDamage;
		}

		protected virtual void Awake()
		{
			_onDealtDamage = (sender, args) => OnKilled(args);
		}

		protected abstract void OnKilled(DamageArgs args);

		public void Construct(IDamageDealer damageDealer) => _damageDealer = damageDealer;

		private EventHandler<DamageArgs> _onDealtDamage;
		private IDamageDealer _damageDealer;
	}
}