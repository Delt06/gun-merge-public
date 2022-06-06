using System;
using Combat.Events;

namespace Combat
{
	public interface ICanDie
	{
		bool IsAlive { get; }
		event EventHandler<DeathArgs> Died;
	}
}