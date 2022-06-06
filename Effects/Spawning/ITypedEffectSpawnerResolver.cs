using Spawning;

namespace Effects.Spawning
{
	public interface ITypedEffectSpawnerResolver
	{
		public ISpawner<Effect> Get(EffectType effectType);
	}
}