using System;
using Combat.Characters.Events;
using Combat.Events;

namespace Combat
{
	public interface IDamageDealer
	{
		void OnDealtDamage(in DamageArgs args);
		event EventHandler<DamageArgs> DealtDamage;

		void OnKilled(in CharacterDeathArgs args);
		event EventHandler<CharacterDeathArgs> Killed;
	}
}