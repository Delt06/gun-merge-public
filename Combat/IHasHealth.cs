using System;

namespace Combat
{
	public interface IHasHealth
	{
		bool IsModifiable { get; }
		float Health { get; set; }
		float MaxHealth { get; set; }
		event EventHandler HealthChanged;
		event EventHandler MaxHealthChanged;
	}
}