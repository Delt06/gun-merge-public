using Sirenix.OdinInspector;
using Spawning;
using Spawning.Pooling;
using UnityEngine;

namespace Effects.Spawning
{
	public sealed class TypedEffectPoolSpawner : PoolSpawner<Effect>, ITypedEffectSpawner
	{
		[SerializeField, Required, AssetSelector]
		private EffectType _type = default;

		public EffectType Type => _type;
	}
}