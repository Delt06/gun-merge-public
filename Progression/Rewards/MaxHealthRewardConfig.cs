using System.Text;
using Combat;
using Components;
using UnityEngine;

namespace Progression.Rewards
{
	[CreateAssetMenu(menuName = AssetPath + "Max Health")]
	public sealed class MaxHealthRewardConfig : RewardConfig
	{
		[SerializeField, Min(1f)] private float _coefficient = 1.5f;
		[SerializeField] private bool _preserveRatio = true;

		public override void GetAcquired(IComponentProvider componentProvider)
		{
			var hasHealth = componentProvider.Get<IHasHealth>();
			if (!hasHealth.IsModifiable) return;

			var ratio = hasHealth.Health / hasHealth.MaxHealth;
			hasHealth.MaxHealth *= _coefficient;

			if (_preserveRatio)
				hasHealth.Health = ratio * hasHealth.MaxHealth;
		}

		public override void GetDescription(StringBuilder stringBuilder)
		{
			stringBuilder.Append("Max Health +")
				.AppendRatioAsPercentage(_coefficient - 1f);
		}
	}
}