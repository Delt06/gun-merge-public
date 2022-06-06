using System;
using Combat.Characters.Events;
using UnityEngine;

namespace Combat.Events
{
	public abstract class DamageDealer_OnKilled_Base : MonoBehaviour
	{
		protected virtual void OnEnable()
		{
			_damageDealer.Killed += _onKilled;
		}

		protected virtual void OnDisable()
		{
			_damageDealer.Killed -= _onKilled;
		}

		protected virtual void Awake()
		{
			_onKilled = (sender, args) => OnKilled(args);
		}

		protected abstract void OnKilled(CharacterDeathArgs args);

		public void Construct(IDamageDealer damageDealer) => _damageDealer = damageDealer;

		private EventHandler<CharacterDeathArgs> _onKilled;
		private IDamageDealer _damageDealer;
	}
}