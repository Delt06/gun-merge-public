using System.Text;
using Combat.Weapons.Coefficients;
using Components;
using UnityEngine;

namespace Progression.Rewards
{
	[CreateAssetMenu(menuName = AssetPath + "Critical Damage Probability")]
	public sealed class CriticalDamageProbabilityRewardConfig : RewardConfig
	{
		[SerializeField, Range(0f, 1f)] private float _extraProbability = 0.1f;

		public override void GetAcquired(IComponentProvider componentProvider)
		{
			var criticalDamage = componentProvider.Get<ConfigurableCriticalDamage>();
			criticalDamage.Probability += _extraProbability;
		}

		public override void GetDescription(StringBuilder stringBuilder)
		{
			stringBuilder.Append("Critical Hit Chance +")
				.AppendRatioAsPercentage(_extraProbability);
		}
	}
}