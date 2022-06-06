using System;
using Combat.Events;
using Combat.Teams;
using JetBrains.Annotations;

namespace Combat
{
	public interface IDamageTaker : ITeamMember
	{
		void Take(float damage, [CanBeNull] IDamageDealer damageDealer = null);
		event EventHandler<DamageArgs> Took;
	}
}