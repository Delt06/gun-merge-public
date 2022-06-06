using JetBrains.Annotations;

namespace Combat.Events
{
	public struct DeathArgs
	{
		[CanBeNull] public IDamageDealer Killer;
	}
}