namespace Effects.Spawning
{
	public interface ITypedEffectSpawner : IEffectSpawner
	{
		EffectType Type { get; }
	}
}