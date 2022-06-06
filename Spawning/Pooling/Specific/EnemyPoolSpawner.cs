using Combat.Characters;
using Components;
using Spawning.Specific;

namespace Spawning.Pooling.Specific
{
	public sealed class EnemyPoolSpawner : PoolSpawner<IComponentProvider>, IEnemySpawner { }
}