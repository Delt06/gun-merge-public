using Components;
using UnityEngine;

namespace Collection.Power
{
	public sealed class CollectiblePower : CollectibleBase
	{
		[SerializeField, Min(0f)] private float _damageCoefficient = 2f;
		[SerializeField, Min(0f)] private float _bulletSpeedCoefficient = 2f;
		[SerializeField, Min(0f)] private float _duration = 10f;

		protected override void OnGetCollected(IComponentProvider componentProvider)
		{
			var powerModifier = componentProvider.Get<PowerModifier>();
			powerModifier.Activate(_damageCoefficient, _bulletSpeedCoefficient, _duration);
		}
	}
}