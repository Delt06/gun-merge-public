using JetBrains.Annotations;

namespace Combat.Events
{
	public struct DamageArgs
	{
		[CanBeNull] public IDamageDealer DamageDealer;
		public float Damage;
	}
}