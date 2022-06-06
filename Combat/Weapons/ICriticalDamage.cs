namespace Combat.Weapons
{
	public interface ICriticalDamage
	{
		float Coefficient { get; }
		float Probability { get; }
	}
}